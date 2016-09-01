using System.Linq;

namespace Entitas.CodeGenerator {

    public class PoolsGenerator : IPoolCodeGenerator {

        const string CLASS_TEMPLATE = @"namespace Entitas {{

    public partial class Pools {{

{0}

        public void SetAllPools() {{
{1}
        }}
    }}
}}
{2}";

        const string POOL_FIELD = "        public {0}Pool {1};";
        const string SET_POOL   = "            {0} = new {1}Pool();";
        const string POOL_TEMPLATE = @"
public partial class {0}Pool : Entitas.Pool<{0}> {{
    public {0}Pool() : base({0}ComponentIds.TotalComponents) {{ }}
    public {0}Pool(int startCreationIndex) : base({1}.TotalComponents, startCreationIndex, new Entitas.PoolMetaData(""{0} Pool"", {1}.componentNames, {1}.componentTypes)) {{ }}
}}
";

        public CodeGenFile[] Generate(string[] poolNames) {

            var poolFields = string.Join("\n", poolNames.Select(poolName =>
                string.Format(POOL_FIELD, poolName, poolName.LowercaseFirst())).ToArray());
            
            var setAllPools = string.Join("\n", poolNames.Select(poolName =>
                string.Format(SET_POOL, poolName.LowercaseFirst(), poolName)).ToArray());

            var pools = poolNames.Aggregate(string.Empty, (acc, poolName) =>
                acc + string.Format(POOL_TEMPLATE, poolName, poolName + CodeGenerator.DEFAULT_COMPONENT_LOOKUP_TAG)
            );

            return new[] { new CodeGenFile(
                "Pools",
                string.Format(CLASS_TEMPLATE, poolFields, setAllPools, pools),
                GetType().FullName
            )};
        }
    }
}
