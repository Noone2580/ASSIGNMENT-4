using System;
using System.Numerics;
using MohawkGame2D;

public class BaseEnemy : BaseAI
{

    public BaseCharacter Target;
    public float AttackCooldown = .7f;
    public float AttackRange = 20f;
    public float TargetSlowdown = .8f;

    public override void CustomSetup()
    {
        base.CustomSetup();

        MovementSpeed = 100f;
    }

    public override void Render()
    {
        base.Render();
    }
}
