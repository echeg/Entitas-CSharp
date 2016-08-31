using Entitas;
using Entitas.CodeGenerator;

[Pool("Core")]
[SingleEntity, CustomPrefix("My")]
public class CustomPrefixComponent : IComponent {
}
