using MohawkGame2D;
using System;

using System.Numerics;

public class Z_Tank : BaseEnemy
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        MovementSpeed = MohawkGame2D.Random.Float(20f, 40f);
        MaxHP = 40f;
        HP = MaxHP;
        AttackDamage = 5f;
        AttackRange = 45f;
        AttackCooldown = 2f;
        TargetSlowdown = 8f;
        BodyTextureLocation = "../../../Assets/Textures/Tank.png";
    }

    public override void Render()
    {
        if (InRoom)
        {
            Target = GetClosetPlayer();
            BaseAI? KeepAway = GetClosetAI();

            if (GetClosetAI() != null)
                KeepAway = GetClosetAI();
            if (Target != null)
                Direction = Target.Position - Position;

            base.Render();
            Move(Direction);

            if (KeepAway != null && Vector2.Distance(KeepAway.Position, Position) <= HitBoxSize)
            {
                Vector2 DirPos = (Position - KeepAway.Position) * (HitBoxSize);
                Position += DirPos * Time.DeltaTime;
            }

            if (Target != null && Vector2.Distance(Target.Position, Position) <= AttackRange)
            {
                Target.Velocity -= Target.Velocity * TargetSlowdown * Time.DeltaTime;
                Velocity -= (Velocity * TargetSlowdown * 1.4f) * Time.DeltaTime;
                if (IsTimerDone(4))
                {
                    SetTimer(4, AttackCooldown);
                    DealDamage(Target, AttackDamage, Vector2.Zero);
                }
            }
            else
                SetTimer(4, AttackCooldown);
        }
        else
        {
            TryEnterRoom();
        }
    }
}
