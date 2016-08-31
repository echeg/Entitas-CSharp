using Entitas;

public class ReactiveSubSystemSpy : ReactiveSubSystemSpyBase, IReactiveSystem {

    public TriggerOnEvent<Entity> trigger { get { return new TriggerOnEvent(_matcher, _eventType); } }

    readonly IMatcher<Entity> _matcher;
    readonly GroupEventType _eventType;

    public ReactiveSubSystemSpy(IMatcher<Entity> matcher, GroupEventType eventType) {
        _matcher = matcher;
        _eventType = eventType;
    }
}

public class ClearReactiveSubSystemSpy : ReactiveSubSystemSpy, IClearReactiveSystem {
    public ClearReactiveSubSystemSpy(IMatcher<Entity> matcher, GroupEventType eventType) :
        base(matcher, eventType) {
    }
}

public class ReactiveEnsureSubSystemSpy : ReactiveSubSystemSpy, IEnsureComponents {

    public IMatcher<Entity> ensureComponents { get { return _ensureComponent; } }

    readonly IMatcher<Entity> _ensureComponent;

    public ReactiveEnsureSubSystemSpy(IMatcher<Entity> matcher, GroupEventType eventType, IMatcher<Entity> ensureComponent) :
        base(matcher, eventType) {
        _ensureComponent = ensureComponent;
    }
}

public class ReactiveExcludeSubSystemSpy : ReactiveSubSystemSpy, IExcludeComponents {

    public IMatcher<Entity> excludeComponents { get { return _excludeComponent; } }

    readonly IMatcher<Entity> _excludeComponent;

    public ReactiveExcludeSubSystemSpy(IMatcher<Entity> matcher, GroupEventType eventType, IMatcher<Entity> excludeComponent) :
        base(matcher, eventType) {
        _excludeComponent = excludeComponent;
    }
}

public class ReactiveEnsureExcludeSubSystemSpy : ReactiveSubSystemSpy, IEnsureComponents, IExcludeComponents {

    public IMatcher<Entity> ensureComponents { get { return _ensureComponent; } }
    public IMatcher<Entity> excludeComponents { get { return _excludeComponent; } }

    readonly IMatcher<Entity> _ensureComponent;
    readonly IMatcher<Entity> _excludeComponent;

    public ReactiveEnsureExcludeSubSystemSpy(IMatcher<Entity> matcher, GroupEventType eventType, IMatcher<Entity> ensureComponent, IMatcher<Entity> excludeComponent) :
        base(matcher, eventType) {
        _ensureComponent = ensureComponent;
        _excludeComponent = excludeComponent;
    }
}

