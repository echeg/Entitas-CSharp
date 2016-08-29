namespace Entitas {

    public partial class Pools {

        public static Pools sharedInstance {
            get {
                if(_sharedInstance == null) {
                    _sharedInstance = new Pools();
                }

                return _sharedInstance;
            }
            set { _sharedInstance = value; }
        }

        static Pools _sharedInstance;

        public static Pool<TEntity> CreatePool<TEntity>(string poolName, int totalComponents, string[] componentNames, System.Type[] componentTypes) where TEntity : class, IEntity, new() {
            var pool = new Pool<TEntity>(totalComponents, 0, new PoolMetaData(poolName, componentNames, componentTypes));
            #if(!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
            if (UnityEngine.Application.isPlaying) {
                var poolObserver = new Entitas.Unity.VisualDebugging.PoolObserver(pool);
                UnityEngine.Object.DontDestroyOnLoad(poolObserver.entitiesContainer);
            }
            #endif

            return pool;
        }
    }
}
