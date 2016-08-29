namespace Entitas {

    public static class GroupExtension {

        /// Creates a GroupObserver for this group.
        public static GroupObserver<TEntity> CreateObserver<TEntity>(this Group<TEntity> group, GroupEventType eventType = GroupEventType.OnEntityAdded) where TEntity : class, IEntity, new() {
            return new GroupObserver<TEntity>(group, eventType);
        }
    }
}

