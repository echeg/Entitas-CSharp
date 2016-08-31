using System.Collections.Generic;
using Entitas;

public class ListAdd : IPerformanceTest {
    const int n = 100000;
    List<Entity> _l;

    public void Before() {
        _l = new List<Entity>();
    }

    public void Run() {
        for (int i = 0; i < n; i++) {
            var entity = new Entity();
            entity.Setup(0, CP.NumComponents, null);
            _l.Add(entity);
        }
    }
}

