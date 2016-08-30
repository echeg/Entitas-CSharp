namespace Entitas {

    public class Group : Group<Entity> {

        public Group(IMatcher<Entity> matcher) : base(matcher) {
        }
    }
}

