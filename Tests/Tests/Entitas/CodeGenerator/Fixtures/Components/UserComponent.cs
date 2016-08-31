using System;
using Entitas;
using Entitas.CodeGenerator;

[Pool("Core")]
[SingleEntity]
public class UserComponent : IComponent {
    public DateTime timestamp;
    public bool isLoggedIn;
}
