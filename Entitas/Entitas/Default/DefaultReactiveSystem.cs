namespace Entitas {

    public interface IReactiveSystem : IReactiveSystem<Entity> { }
    public interface IMultiReactiveSystem : IMultiReactiveSystem<Entity> { }
    public interface IGroupObserverSystem : IGroupObserverSystem<Entity> { }

    public interface IEnsureComponents : IEnsureComponents<Entity> { }
    public interface IExcludeComponents : IExcludeComponents<Entity> { }

    public class ReactiveSystem : ReactiveSystem<Entity> {

        public ReactiveSystem(Pool<Entity> pool, IReactiveSystem<Entity> subSystem) :
            base(pool, subSystem) {
        }

        public ReactiveSystem(Pool<Entity> pool, IMultiReactiveSystem<Entity> subSystem) :
            base(pool, subSystem) {
        }

        public ReactiveSystem(IGroupObserverSystem<Entity> subSystem) : base(subSystem) {
        }
    }
}

