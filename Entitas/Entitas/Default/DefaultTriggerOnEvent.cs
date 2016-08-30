namespace Entitas {

    public class TriggerOnEvent : TriggerOnEvent<Entity> {

        public TriggerOnEvent(IMatcher<Entity> trigger, GroupEventType eventType) :
            base(trigger, eventType) {
        }
    }
}

