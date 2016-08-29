namespace Entitas {

    public static class IMatcherExtension {

        /// Convenience method to create a new TriggerOnEvent. Commonly used in IReactiveSystem and IMultiReactiveSystem.
        public static TriggerOnEvent<TEntity> OnEntityAdded<TEntity>(this IMatcher<TEntity> matcher) where TEntity : class, IEntity, new() {
            return new TriggerOnEvent<TEntity>(matcher, GroupEventType.OnEntityAdded);
        }

        /// Convenience method to create a new TriggerOnEvent. Commonly used in IReactiveSystem and IMultiReactiveSystem.
        public static TriggerOnEvent<TEntity> OnEntityRemoved<TEntity>(this IMatcher<TEntity> matcher) where TEntity : class, IEntity, new() {
            return new TriggerOnEvent<TEntity>(matcher, GroupEventType.OnEntityRemoved);
        }

        /// Convenience method to create a new TriggerOnEvent. Commonly used in IReactiveSystem and IMultiReactiveSystem.
        public static TriggerOnEvent<TEntity> OnEntityAddedOrRemoved<TEntity>(this IMatcher<TEntity> matcher) where TEntity : class, IEntity, new() {
            return new TriggerOnEvent<TEntity>(matcher, GroupEventType.OnEntityAddedOrRemoved);
        }
    }
}

