namespace Entitas {

    public interface IMatcher : IMatcher<Entity> { }

    public partial class Matcher : Matcher<Entity>, IMatcher {
    }
}

