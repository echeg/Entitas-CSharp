using System.Collections.Generic;
using Entitas;

public class TestReactiveSystem : IReactiveSystem<Entity> {

    public TriggerOnEvent<Entity> trigger { get { return Matcher.AllOf(0).OnEntityAdded(); } }

    public void Execute(List<Entity> entities) {
    }
}

