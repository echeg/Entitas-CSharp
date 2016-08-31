using System.Collections.Generic;
using Entitas;
using NSpec;

public static class TestExtensions {

    public static void Fail(this nspec spec) {
        "but did".should_be("should not happen");
    }

    public static Entity CreateEntity(this nspec spec) {
        var entity = new Entity();
        entity.Setup(0, CID.TotalComponents, new Stack<IComponent>[CID.TotalComponents]);
        return entity;
    }
}

