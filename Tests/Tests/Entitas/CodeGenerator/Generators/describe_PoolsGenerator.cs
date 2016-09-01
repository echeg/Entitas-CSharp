using System;
using Entitas.CodeGenerator;
using NSpec;

class describe_PoolsGenerator : nspec {

    const bool logResults = false;

    const string metaPool = @"namespace Entitas {

    public partial class Pools {

        public MetaPool meta;

        public void SetAllPools() {
            meta = new MetaPool();
        }
    }
}

public partial class MetaPool : Entitas.Pool<Meta> {
    public MetaPool() : base(MetaComponentIds.TotalComponents) { }
    public MetaPool(int startCreationIndex) : base(MetaComponentIds.TotalComponents, startCreationIndex, new Entitas.PoolMetaData(""Meta Pool"", MetaComponentIds.componentNames, MetaComponentIds.componentTypes)) { }
}
";

    const string coreMetaPool = @"namespace Entitas {

    public partial class Pools {

        public MetaPool meta;
        public CorePool core;

        public void SetAllPools() {
            meta = new MetaPool();
            core = new CorePool();
        }
    }
}

public partial class MetaPool : Entitas.Pool<Meta> {
    public MetaPool() : base(MetaComponentIds.TotalComponents) { }
    public MetaPool(int startCreationIndex) : base(MetaComponentIds.TotalComponents, startCreationIndex, new Entitas.PoolMetaData(""Meta Pool"", MetaComponentIds.componentNames, MetaComponentIds.componentTypes)) { }
}

public partial class CorePool : Entitas.Pool<Core> {
    public CorePool() : base(CoreComponentIds.TotalComponents) { }
    public CorePool(int startCreationIndex) : base(CoreComponentIds.TotalComponents, startCreationIndex, new Entitas.PoolMetaData(""Core Pool"", CoreComponentIds.componentNames, CoreComponentIds.componentTypes)) { }
}
";

    void generates(string[] poolNames, string expectedFileContent) {
        try {




        expectedFileContent = expectedFileContent.ToUnixLineEndings();

        var files = new PoolsGenerator().Generate(poolNames);
        files.Length.should_be(1);
        var file = files[0];

        #pragma warning disable
        if(logResults) {
            Console.WriteLine("should:\n" + expectedFileContent);
            Console.WriteLine("was:\n" + file.fileContent);
        }

        file.fileName.should_be("Pools");
        file.fileContent.should_be(expectedFileContent);



        } catch(System.Exception ex) {
            System.Console.WriteLine(ex);
            throw ex;
        }

    }

    void when_generating() {

        it["generates one pool"] = () => generates(new[] { "Meta" }, metaPool);
        it["generates multiple pools"] = () => generates(new[] { "Meta", "Core" }, coreMetaPool);
    }
}
