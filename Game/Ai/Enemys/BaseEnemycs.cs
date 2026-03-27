using System;
using System.Numerics;
using MohawkGame2D;

public class BaseEnemy : BaseAI
{

    BaseCharacter Target;

    public override void CustomSetup()
    {
        base.CustomSetup();

        MovementSpeed = 100f;
    }

    public override void Render()
    {
        Target = GetClosetPlayer();
        Direction = Vector2.Normalize(Target.Position - Position);

        base.Render();
        Move(Direction);

    }
}
