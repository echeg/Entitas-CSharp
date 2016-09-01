using Entitas;
using Entitas.CodeGenerator;

[Pool("SomePool")]
[DontGenerate(false)]
public class DontGenerateIndexComponent : IComponent {
}

