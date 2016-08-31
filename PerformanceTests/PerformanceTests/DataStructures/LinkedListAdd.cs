using Entitas;
using System.Collections.Generic;

public class LinkedListAdd : IPerformanceTest {
    const int n = 100000;
    LinkedList<Entity> _ll;

    public void Before() {
        _ll = new LinkedList<Entity>();
    }

    public void Run() {
        for (int i = 0; i < n; i++) {
            var entity = new Entity();
            entity.Setup(0, CP.NumComponents, null);
            _ll.AddLast(entity);
        }
    }
}

