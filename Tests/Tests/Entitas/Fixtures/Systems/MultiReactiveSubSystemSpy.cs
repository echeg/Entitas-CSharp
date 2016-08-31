using Entitas;

public class MultiReactiveSubSystemSpy : ReactiveSubSystemSpyBase, IMultiReactiveSystem {

    public TriggerOnEvent<Entity>[] triggers { get { return _triggers; } }

    readonly TriggerOnEvent<Entity>[] _triggers;

    public MultiReactiveSubSystemSpy(TriggerOnEvent<Entity>[] triggers) {
        _triggers = triggers;
    }
}

public class MultiReactiveEnsureSubSystemSpy : MultiReactiveSubSystemSpy, IEnsureComponents {

    public IMatcher<Entity> ensureComponents { get { return _ensureComponents; } }

    readonly IMatcher<Entity> _ensureComponents;

    public MultiReactiveEnsureSubSystemSpy(TriggerOnEvent<Entity>[] triggers, IMatcher<Entity> ensureComponents) :
        base(triggers) {
        _ensureComponents = ensureComponents;
    }
}

public class MultiReactiveExcludeSubSystemSpy : MultiReactiveSubSystemSpy, IExcludeComponents {

    public IMatcher<Entity> excludeComponents { get { return _excludeComponents; } }

    readonly IMatcher<Entity> _excludeComponents;

    public MultiReactiveExcludeSubSystemSpy(TriggerOnEvent<Entity>[] triggers, IMatcher<Entity> excludeComponents) :
        base(triggers) {
        _excludeComponents = excludeComponents;
    }
}

public class MultiReactiveEnsureExcludeSubSystemSpy : MultiReactiveSubSystemSpy, IEnsureComponents, IExcludeComponents {

    public IMatcher<Entity> ensureComponents { get { return _ensureComponents; } }
    public IMatcher<Entity> excludeComponents { get { return _excludeComponents; } }

    readonly IMatcher<Entity> _ensureComponents;
    readonly IMatcher<Entity> _excludeComponents;

    public MultiReactiveEnsureExcludeSubSystemSpy(TriggerOnEvent<Entity>[] triggers, IMatcher<Entity> ensureComponents, IMatcher<Entity> excludeComponents) :
        base(triggers) {
        _ensureComponents = ensureComponents;
        _excludeComponents = excludeComponents;
    }
}

