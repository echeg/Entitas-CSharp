namespace Entitas {

    public class GroupObserver : GroupObserver<Entity> {
        
        public GroupObserver(Group<Entity> group, GroupEventType eventType) :
            base(group, eventType) {
        }

        public GroupObserver(Group<Entity>[] groups, GroupEventType[] eventTypes) :
            base(groups, eventTypes) {
        }
    }
}

