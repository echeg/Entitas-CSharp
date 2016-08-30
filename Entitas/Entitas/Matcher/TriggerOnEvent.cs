namespace Entitas {

    public class TriggerOnEvent<TEntity> where TEntity : class, IEntity, new() {

        public IMatcher<TEntity> trigger;
        public GroupEventType eventType;

        public TriggerOnEvent(IMatcher<TEntity> trigger, GroupEventType eventType) {
            this.trigger = trigger;
            this.eventType = eventType;
        }
    }
}

