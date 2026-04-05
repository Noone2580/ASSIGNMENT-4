using System;
using System.Numerics;
using MohawkGame2D;

public class BaseEnemy : BaseAI
{

    public float AttackDamage = 1;
    public float AttackCooldown = .7f;
    public float AttackRange = 20f;
    public float TargetSlowdown = .8f;

    public override void CustomSetup()
    {
        base.CustomSetup();

        MovementSpeed = 100f;
    }

    public override void RenderNoUpdate()
    {
        if (InRoom)
        {
            base.RenderNoUpdate();
        }
    }

    public override void Render()
    {
        base.Render();
    }
}
