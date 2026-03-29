using MohawkGame2D;
using System;

using System.Numerics;

public class Z_Tank : BaseEnemy
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        MovementSpeed = MohawkGame2D.Random.Float(20f, 40f);
        MaxHP = 20f;
        HP = MaxHP;
        AttackDamage = 5f;
        AttackRange = 45f;
        AttackCooldown = 2f;
        TargetSlowdown = 8f;
        Position = MohawkGame2D.Random.Vector2(new Vector2(0), new Vector2(400));
    }

    public override void Render()
    {
        if (InRoom)
        {
            Target = GetClosetPlayer();
            BaseAI KeepAway = GetClosetAI();
            Direction = Target.Position - Position;

            //Console.WriteLine($"{Target} {Time.SecondsElapsed} {Direction}");

            base.Render();
            Move(Direction);

            if (Vector2.Distance(KeepAway.Position, Position) <= HitBoxSize)
            {
                Vector2 DirPos = (Position - KeepAway.Position) * (HitBoxSize);

                Position += DirPos * Time.DeltaTime;
            }

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
        }
        else 
        {
            if (IsTimerDone(0)) 
            {
                Velocity = Vector2.Zero;
                Position = EnterRoomDoor;
                InRoom = true;
                RoomName = $"{GetGame.CurrentRoom}";
            }
        }
    }
}
