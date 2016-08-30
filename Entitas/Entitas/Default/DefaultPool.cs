namespace Entitas {

    public partial class Pool : Pool<Entity> {

        public Pool(int totalComponents) : base(totalComponents) {
        }

        public Pool(int totalComponents, int startCreationIndex, PoolMetaData metaData) :
            base(totalComponents, startCreationIndex, metaData) {
        }
    }
}

