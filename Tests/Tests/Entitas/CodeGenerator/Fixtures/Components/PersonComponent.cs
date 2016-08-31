using Entitas;
using Entitas.CodeGenerator;

[Pool("Core")]
public class PersonComponent : IComponent {
    public int age;
    public string name;
}
