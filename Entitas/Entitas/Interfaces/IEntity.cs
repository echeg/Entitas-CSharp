using System;
using System.Collections.Generic;

namespace Entitas {

    public delegate void EntityChanged(Entity entity, int index, IComponent component);
    public delegate void ComponentReplaced(Entity entity, int index, IComponent previousComponent, IComponent newComponent);
    public delegate void EntityReleased(Entity entity);

    public interface IEntity {

        /// Occurs when a component gets added.
        /// All event handlers will be removed when the entity gets destroyed by the pool.
        event EntityChanged OnComponentAdded;

        /// Occurs when a component gets removed.
        /// All event handlers will be removed when the entity gets destroyed by the pool.
        event EntityChanged OnComponentRemoved;

        /// Occurs when a component gets replaced.
        /// All event handlers will be removed when the entity gets destroyed by the pool.
        event ComponentReplaced OnComponentReplaced;

        /// Occurs when an entity gets released and is not retained anymore.
        /// All event handlers will be removed when the entity gets destroyed by the pool.
        event EntityReleased OnEntityReleased;

        /// The total amount of components an entity can possibly have.
        int totalComponents { get; }

        /// Each entity has its own unique creationIndex which will be set by the pool when you create the entity.
        int creationIndex { get; }

        /// The pool manages the state of an entity. Active entities are enabled, destroyed entities are not.
        bool isEnabled { get; }

        /// componentPools is set by the pool which created the entity and is used to reuse removed components.
        /// Removed components will be pushed to the componentPool.
        /// Use entity.CreateComponent(index, type) to get a new or reusable component from the componentPool.
        /// Use entity.GetComponentPool(index) to get a componentPool for a specific component index.
        Stack<IComponent>[] componentPools { get; }

        /// The poolMetaData is set by the pool which created the entity and contains information about the pool.
        /// It's used to provide better error messages.
        PoolMetaData poolMetaData { get; }

        /// Adds a component at the specified index. You can only have one component at an index.
        /// Each component type must have its own constant index.
        /// The prefered way is to use the generated methods from the code generator.
        Entity AddComponent(int index, IComponent component);

        /// Removes a component at the specified index. You can only remove a component at an index if it exists.
        /// The prefered way is to use the generated methods from the code generator.
        Entity RemoveComponent(int index);

        /// Replaces an existing component at the specified index or adds it if it doesn't exist yet.
        /// The prefered way is to use the generated methods from the code generator.
        Entity ReplaceComponent(int index, IComponent component);

        /// Returns a component at the specified index. You can only get a component at an index if it exists.
        /// The prefered way is to use the generated methods from the code generator.
        IComponent GetComponent(int index);

        /// Returns all added components.
        IComponent[] GetComponents();

        /// Returns all indices of added components.
        int[] GetComponentIndices();

        /// Determines whether this entity has a component at the specified index.
        bool HasComponent(int index);

        /// Determines whether this entity has components at all the specified indices.
        bool HasComponents(int[] indices);

        /// Determines whether this entity has a component at any of the specified indices.
        bool HasAnyComponent(int[] indices);

        /// Removes all components.
        void RemoveAllComponents();

        /// Returns the componentPool for the specified component index.
        /// componentPools is set by the pool which created the entity and is used to reuse removed components.
        /// Removed components will be pushed to the componentPool.
        /// Use entity.CreateComponent(index, type) to get a new or reusable component from the componentPool.
        Stack<IComponent> GetComponentPool(int index);

        /// Returns a new or reusable component from the componentPool for the specified component index.
        IComponent CreateComponent(int index, Type type);

        /// Returns a new or reusable component from the componentPool for the specified component index.
        T CreateComponent<T>(int index) where T : new();

        #if ENTITAS_FAST_AND_UNSAFE

        /// Returns the number of objects that retain this entity.
        int retainCount { get; }

        #else

        /// Returns the number of objects that retain this entity.
        int retainCount { get; }

        /// Returns all the objects that retain this entity.
        HashSet<object> owners { get; }

        #endif
    }
}