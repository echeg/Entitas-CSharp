using System.Collections.Generic;

namespace Entitas {

    /// Implement this interface if you want to create a reactive system which is triggered by the specified trigger.
    public interface IReactiveSystem<TEntity> : IReactiveExecuteSystem<TEntity> where TEntity : class, IEntity, new() {
        TriggerOnEvent<TEntity> trigger { get; }
    }

    /// Implement this interface if you want to create a reactive system which is triggered by any of the specified triggers.
    public interface IMultiReactiveSystem<TEntity> : IReactiveExecuteSystem<TEntity> where TEntity : class, IEntity, new() {
        TriggerOnEvent<TEntity>[] triggers { get; }
    }

    /// Implement this interface if you want to create a reactive system which is triggered by a GroupObserver.
    /// This is useful when you want to react to changes in multiple groups from different pools.
    public interface IGroupObserverSystem<TEntity> : IReactiveExecuteSystem<TEntity> where TEntity : class, IEntity, new() {
        GroupObserver<TEntity> groupObserver { get; }
    }

    /// Not meant to be implemented. Use either IReactiveSystem, IMultiReactiveSystem or IGroupObserverSystem.
    public interface IReactiveExecuteSystem<TEntity> : ISystem where TEntity : class, IEntity, new() {
        void Execute(List<TEntity> entities);
    }

    /// Not meant to be implemented. Use either IReactiveSystem, IMultiReactiveSystem or IGroupObserverSystem.
    public interface IReactiveSystemWrapper : IExecuteSystem {
        void Activate();
        void Deactivate();
        void Clear();
    }

    /// Implement this interface in combination with IReactiveSystem or IMultiReactiveSystem.
    /// It will ensure that all entities will match the specified matcher.
    /// This is useful when a component triggered the reactive system, but once the system gets executed the component already has been removed.
    /// Implementing IEnsureComponents can filter these enities.
    public interface IEnsureComponents<TEntity> where TEntity : class, IEntity, new() {
        IMatcher<TEntity> ensureComponents { get; }
    }

    /// Implement this interface in combination with IReactiveSystem or IMultiReactiveSystem.
    /// It will exclude all entities which match the specified matcher.
    /// To exclude multiple components use Matcher.AnyOf(ComponentX, ComponentY, ComponentZ).
    public interface IExcludeComponents<TEntity> where TEntity : class, IEntity, new() {
        IMatcher<TEntity> excludeComponents { get; }
    }

    /// Implement this interface in combination with IReactiveSystem or IMultiReactiveSystem.
    /// If a system changes entities which in turn would trigger itself consider implementing IClearReactiveSystem
    /// which will ignore the changes made by the system.
    public interface IClearReactiveSystem {
    }
}

