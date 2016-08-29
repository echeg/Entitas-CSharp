namespace Entitas {

    /// Implement this interface if you want to create a system which needs a reference to a pool.
    /// Recommended way to create systems in general: pool.CreateSystem(new MySystem());
    /// Calling pool.CreateSystem(new MySystem()) will automatically inject the pool if ISetPool is implemented.
    /// It's recommended to pass in the pool as a dependency using ISetPool rather than using Pools.sharedInstance.pool directly within the system to avoid tight coupling.
    public interface ISetPool<TEntity> where TEntity : class, IEntity, new() {
        void SetPool(Pool<TEntity> pool);
    }

    /// Implement this interface if you want to create a system which needs a reference to pools.
    /// Recommended way to create systems in general: pool.CreateSystem(new MySystem());
    /// Calling pool.CreateSystem(new MySystem()) will automatically inject the pools if ISetPools is implemented.
    /// It's recommended to pass in the pools as a dependency using ISetPools rather than using Pools.sharedInstance directly within the system to avoid tight coupling.
    public interface ISetPools {
        void SetPools(Pools pools);
    }

    public static class PoolExtension {

        /// Returns all entities matching the specified matcher.
        public static TEntity[] GetEntities<TEntity>(this Pool<TEntity> pool, IMatcher<TEntity> matcher) where TEntity : class, IEntity, new() {
            return pool.GetGroup(matcher).GetEntities();
        }

        /// This is the recommended way to create systems.
        /// It will inject the pool if ISetPool is implemented.
        /// It will inject the Pools.sharedInstance if ISetPools is implemented.
        /// It will automatically create a ReactiveSystem if it is a IReactiveSystem or IMultiReactiveSystem.
        public static ISystem CreateSystem<TEntity>(this Pool<TEntity> pool, ISystem system) where TEntity : class, IEntity, new() {
            return CreateSystem(pool, system, Pools.sharedInstance);
        }

        /// This is the recommended way to create systems.
        /// It will inject the pool if ISetPool is implemented.
        /// It will inject the pools if ISetPools is implemented.
        /// It will automatically create a ReactiveSystem if it is a IReactiveSystem or IMultiReactiveSystem.
        public static ISystem CreateSystem<TEntity>(this Pool<TEntity> pool, ISystem system, Pools pools) where TEntity : class, IEntity, new() {
            var poolSystem = system as ISetPool<TEntity>;
            if (poolSystem != null) {
                poolSystem.SetPool(pool);
            }
            var poolsSystem = system as ISetPools;
            if (poolsSystem != null) {
                poolsSystem.SetPools(pools);
            }
            var reactiveSystem = system as IReactiveSystem<TEntity>;
            if (reactiveSystem != null) {
                return new ReactiveSystem<TEntity>(pool, reactiveSystem);
            }
            var multiReactiveSystem = system as IMultiReactiveSystem<TEntity>;
            if (multiReactiveSystem != null) {
                return new ReactiveSystem<TEntity>(pool, multiReactiveSystem);
            }
            var groupObserverSystem = system as IGroupObserverSystem<TEntity>;
            if (groupObserverSystem != null) {
                return new ReactiveSystem<TEntity>(groupObserverSystem);
            }

            return system;
        }

        /// Creates a GroupObserver which observes all specified pools.
        /// This is useful when you want to create a GroupObserver for multiple pools
        /// which can be used with IGroupObserverSystem.

        // TODO

        //public static GroupObserver<IEntity> CreateGroupObserver(this Pool<IEntity>[] pools, IMatcher<IEntity> matcher, GroupEventType eventType = GroupEventType.OnEntityAdded) {
        //    var groups = new Group[pools.Length];
        //    var eventTypes = new GroupEventType[pools.Length];

        //    for (int i = 0; i < pools.Length; i++) {
        //        groups[i] = pools[i].GetGroup(matcher);
        //        eventTypes[i] = eventType;
        //    }

        //    return new GroupObserver(groups, eventTypes);
        //}
    }
}

