using MohawkGame2D;
using System;

using System.Numerics;

public class Zombie : BaseEnemy
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        MovementSpeed = MohawkGame2D.Random.Float(30f, 80f);
        MaxHP = 25f;
        HP = MaxHP;
        AttackDamage = 1f;
        AttackRange = 45f;
        TargetSlowdown = 6.5f;
        BodyTextureLocation = "../../../Assets/Textures/Zombie.png";

    }

    public override void Render()
    {
        Target = GetClosetPlayer();

        if (InRoom && Target != null)
        {
            BaseAI? KeepAway = GetClosetAI();

            if(GetClosetAI() != null)
                KeepAway = GetClosetAI();

            Direction = Target.Position - Position;

            base.Render();
            Move(Direction);

            if (KeepAway != null && Vector2.Distance(KeepAway.Position, Position) <= HitBoxSize)
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
                GridPosition = GetGame.Grid.CurrentRoomPosition;
            }
        }
    }
}
