using System;
using System.Collections.Generic;

namespace Entitas {

    public interface IEntityIndex {
        void Deactivate();
    }

    public abstract class AbstractEntityIndex<TEntity, TKey> : IEntityIndex where TEntity : class, IEntity, new() {

        protected readonly Group<TEntity> _group;
        protected readonly Func<TEntity, IComponent, TKey> _getKey;

        protected AbstractEntityIndex(Group<TEntity> group, Func<TEntity, IComponent, TKey> getKey) {
            _group = group;
            _getKey = getKey;

            group.OnEntityAdded += onEntityAdded;
            group.OnEntityRemoved += onEntityRemoved;
        }

        public virtual void Deactivate() {
            _group.OnEntityAdded -= onEntityAdded;
            _group.OnEntityRemoved -= onEntityRemoved;
            clear();
        }

        protected void indexEntities(Group<TEntity> group) {
            var entities = group.GetEntities();
            for (int i = 0; i < entities.Length; i++) {
                addEntity(entities[i], null);
            }
        }

        protected void onEntityAdded(Group<TEntity> group, TEntity entity, int index, IComponent component) {
            addEntity(entity, component);
        }

        protected void onEntityRemoved(Group<TEntity> group, TEntity entity, int index, IComponent component) {
            removeEntity(entity, component);
        }

        protected abstract void addEntity(TEntity entity, IComponent component);

        protected abstract void removeEntity(TEntity entity, IComponent component);

        protected abstract void clear();

        ~AbstractEntityIndex () {
            Deactivate();
        }
    }

    public class PrimaryEntityIndex<TEntity, TKey> : AbstractEntityIndex<TEntity, TKey> where TEntity : class, IEntity, new() {

        readonly Dictionary<TKey, TEntity> _index;

        public PrimaryEntityIndex(Group<TEntity> group, Func<TEntity, IComponent, TKey> getKey) : base(group, getKey) {
            _index = new Dictionary<TKey, TEntity>();
            indexEntities(group);
        }

        public bool HasEntity(TKey key) {
            return _index.ContainsKey(key);
        }

        public TEntity GetEntity(TKey key) {
            var entity = TryGetEntity(key);
            if (entity == null) {
                throw new EntityIndexException("Entity for key '" + key + "' doesn't exist!",
                    "You should check if an entity with that key exists before getting it.");
            }

            return entity;
        }

        public TEntity TryGetEntity(TKey key) {
            TEntity entity;
            _index.TryGetValue(key, out entity);
            return entity;
        }

        protected override void clear() {
            foreach (var entity in _index.Values) {
                entity.Release(this);
            }

            _index.Clear();
        }

        protected override void addEntity(TEntity entity, IComponent component) {
            var key = _getKey(entity, component);
            if (_index.ContainsKey(key)) {
                throw new EntityIndexException("Entity for key '" + key + "' already exists!",
                    "Only one entity for a primary key is allowed.");
            }

            _index.Add(key, entity);
            entity.Retain(this);
        }

        protected override void removeEntity(TEntity entity, IComponent component) {
            _index.Remove(_getKey(entity, component));
            entity.Release(this);
        }
    }

    public class EntityIndex<TEntity, TKey> : AbstractEntityIndex<TEntity, TKey> where TEntity : class, IEntity, new() {

        readonly Dictionary<TKey, HashSet<TEntity>> _index;

        public EntityIndex(Group<TEntity> group, Func<TEntity, IComponent, TKey> getKey) : base(group, getKey) {
            _index = new Dictionary<TKey, HashSet<TEntity>>();
            indexEntities(group);
        }

        public HashSet<TEntity> GetEntities(TKey key) {
            HashSet<TEntity> entities;
            if (!_index.TryGetValue(key, out entities)) {
                entities = new HashSet<TEntity>(EntityEqualityComparer<TEntity>.comparer);
                _index.Add(key, entities);
            }

            return entities;
        }

        protected override void clear() {
            foreach (var entities in _index.Values) {
                foreach (var entity in entities) {
                    entity.Release(this);
                }
            }

            _index.Clear();
        }

        protected override void addEntity(TEntity entity, IComponent component) {
            GetEntities(_getKey(entity, component)).Add(entity);
            entity.Retain(this);
        }

        protected override void removeEntity(TEntity entity, IComponent component) {
            GetEntities(_getKey(entity, component)).Remove(entity);
            entity.Release(this);
        }
    }

    public class EntityIndexException : EntitasException {
        public EntityIndexException(string message, string hint) :
            base(message, hint) {
        }
    }
}