namespace Entitas {

    public class EntityIndex<TKey> : EntityIndex<Entity, TKey> {

        public EntityIndex(Group<Entity> group, System.Func<Entity, IComponent, TKey> getKey) : base(group, getKey) {
        }
    }

    public class PrimaryEntityIndex<TKey> : PrimaryEntityIndex<Entity, TKey> {

        public PrimaryEntityIndex(Group<Entity> group, System.Func<Entity, IComponent, TKey> getKey) : base(group, getKey) {
        }
    }
}

