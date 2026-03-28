using MohawkGame2D;
using System;

using System.Numerics;

public class Zombie : BaseEnemy
{
    public override void CustomSetup()
    {
        MovementSpeed = MohawkGame2D.Random.Float(30f, 80f);
        AttackRange = 45f;
        TargetSlowdown = 6f;
        Position = MohawkGame2D.Random.Vector2(new Vector2(0), new Vector2(400));
    }

    public override void Render()
    {
        Target = GetClosetPlayer();
        BaseAI KeepAway = GetClosetAI();
        Direction = Target.Position - Position;

        //Console.WriteLine($"{Target} {Time.SecondsElapsed} {Direction}");

        base.Render();
        Move(Direction);

        if (Vector2.Distance(KeepAway.Position, Position) <= HitBoxSize)
        {
            Vector2 DirPos = (Position - KeepAway.Position) * (HitBoxSize) ;

            Position += DirPos * Time.DeltaTime;
        }

        if (Vector2.Distance(Target.Position, Position) <= AttackRange)
        {
            Target.Velocity -= Target.Velocity * TargetSlowdown * Time.DeltaTime;
            Velocity -= (Velocity * TargetSlowdown * 1.4f) * Time.DeltaTime;

            if (IsTimerDone(0))
            {
                SetTimer(0, AttackCooldown);
                DealDamage(Target, 1, Vector2.Zero);
            }
        }
    }
}
