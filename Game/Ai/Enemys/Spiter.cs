using MohawkGame2D;
using System;

using System.Numerics;

public class Spiter : BaseEnemy
{
    Vector2 MoveTo = Vector2.Zero;
    float SpitDamage = 10;
    float SpitRate = 2;

    public override void CustomSetup()
    {
        base.CustomSetup();
        MovementSpeed = MohawkGame2D.Random.Float(60f, 90f);
        MaxHP = 35f;
        HP = MaxHP;
        AttackDamage = 1f;
        AttackRange = 45f;
        TargetSlowdown = 6.5f;
        BodyTextureLocation = "../../../Assets/Textures/Spitter.png";
    }

    public override void Render()
    {

        if (InRoom)
        {
            BaseAI? KeepAway = GetClosetAI();
            Target = GetClosetPlayer();

            if (GetClosetAI() != null)
                KeepAway = GetClosetAI();

            base.Render();

            if (KeepAway != null && Vector2.Distance(KeepAway.Position, Position) <= HitBoxSize)
            {
                Vector2 DirPos = (Position - KeepAway.Position) * (HitBoxSize);
                Position += DirPos * Time.DeltaTime;
            }

            if (Target != null && Vector2.Distance(Target.Position, Position) <= 100f)
            {
                SetTimer(3, SpitRate);
                Direction = Target.Position - Position;
                Move(Direction);

                if (Vector2.Distance(Target.Position, Position) <= AttackRange)
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
                return;
            }
            if (Target == null) { return; }

            MoveTo = Vector2.Normalize((Target.Position + Target.Direction * 300f) - Position);
            Direction = Vector2.Normalize(Target.Position - Position);

            Move(MoveTo);

            if (IsTimerDone(3))
            {
                BaseProjectile projectile = new BaseProjectile();
                projectile.Setup(GetGame, this, SpitDamage, Position, Direction * 800f);
                SetTimer(3, SpitRate);
            }
        }
        else
        {
            TryEnterRoom();
        }
    }
}
