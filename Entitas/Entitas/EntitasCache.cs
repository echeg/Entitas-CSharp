using System.Collections.Generic;

namespace Entitas {

    public static class EntitasCache {

        public static List<IComponent> reusableIComponentList { get { _reusableIComponentList.Clear(); return _reusableIComponentList; } }
        static readonly List<IComponent> _reusableIComponentList = new List<IComponent>();

        public static List<int> reusableIntList { get { _reusableIntList.Clear(); return _reusableIntList; } }
        static readonly List<int> _reusableIntList = new List<int>();

        public static HashSet<int> reusableIntHashSet { get { _reusableIntHashSet.Clear(); return _reusableIntHashSet; } }
        static readonly HashSet<int> _reusableIntHashSet = new HashSet<int>();
    }
}

