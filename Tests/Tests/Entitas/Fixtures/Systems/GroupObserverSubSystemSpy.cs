using Entitas;

public class GroupObserverSubSystemSpy : ReactiveSubSystemSpyBase, IGroupObserverSystem {

    public GroupObserver<Entity> groupObserver { get { return _groupObserver; } }

    readonly GroupObserver<Entity> _groupObserver;

    public GroupObserverSubSystemSpy(GroupObserver<Entity> groupObserver) {
        _groupObserver = groupObserver;
    }
}
