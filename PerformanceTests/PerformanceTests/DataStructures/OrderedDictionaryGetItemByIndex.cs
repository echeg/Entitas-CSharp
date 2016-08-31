using Entitas;
using System;
using System.Collections.Specialized;

public class OrderedDictionaryGetItemByIndex : IPerformanceTest {
    const int n = 100000;
    Random _random;
    OrderedDictionary _dict;

    public void Before() {
        _random = new Random();
        _dict = new OrderedDictionary();
        for (int i = 0; i < n; i++) {
            var e = new Entity();
            e.Setup(0, CP.NumComponents, null);
            _dict.Add(i, e);
        }
    }

    public void Run() {
        for (int i = 0; i < n; i++) {
            var e = _dict[_random.Next(0, n)];
        }
    }
}

