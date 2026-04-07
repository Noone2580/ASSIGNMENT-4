using System;
using System.Numerics;
using MohawkGame2D;


public class Boss : BaseEnemy
{
    int Phase = 0;
    float FireBallRate = 1f;
    float FireBallDamage = 10f;
    float ProSpeed = 400f;
    float[] FireAngle = [-70f, -56, -42, -28, -14, 0, 14, 28, 42, 56, 70f];
    bool Die1 = true;
    bool Die2 = true;

    public override void CustomSetup()
    {
        base.CustomSetup();
        BodyTextureLocation = "../../../Assets/Textures/Boss.png";
        MaxHP = 200;
        HP = 200;
    }

    public override void TakeDamage(float Damage, Vector2 HitForce)
    {
        base.TakeDamage(Damage, HitForce);
        if (HP <= MaxHP * .75f)
        {
            if (HP <= MaxHP * .25f)
            {
                Phase = 2;
                return;
            }
            Phase = 1;
        }
    }

    public override void Die()
    {
        base.Die();
        GetGame.EnterNewRoom(GetGame.StartGrid, new Vector2(0), new Vector2(0));
    }

    public override void Render()
    {
        DamageResistance = float.Clamp(DamageResistance, 0.2f, 1f);

        base.Render();
        switch (Phase)
        {
            case 0:
                Target = GetClosetPlayer();
                Direction = Vector2.Normalize(Target.Position - Position);
                FireBallRate = 2f;

                if (IsTimerDone(3))
                {
                    SetTimer(3, FireBallRate);

                    for (int i = 0; i < FireAngle.Length; i++)
                    {
                        float Rotation;

                        float RotationAngle = MathF.Atan2(Direction.X, Direction.Y) * -1f; // Gets an angle form Direction
                        Rotation = float.RadiansToDegrees(RotationAngle) + 90 + MohawkGame2D.Random.Float(-40, 40); // Turns that angle into Degrees and adds 90 Degrees and random spride
                        RotationAngle = float.DegreesToRadians(Rotation); // Turns it back into a angle

                        Vector2 Shot = new Vector2(float.Cos(RotationAngle), float.Sin(RotationAngle));

                        BaseProjectile Bullet = new BaseProjectile();
                        Bullet.Setup(GetGame, this, FireBallDamage, Position, Shot * ProSpeed);
                    }
                }
                break;

            case 1:
                Target = GetClosetPlayer();
                Direction = Vector2.Normalize(Target.Position - Position);
                FireBallRate = .8f;

                if(Die1) { GetGame.StartDialogue(GetGame.GetDialoguePersonally.BossRaphText[11], 5f); Die1 = false; }

                if (IsTimerDone(3))
                {
                    SetTimer(3, FireBallRate);

                    for (int i = 0; i < FireAngle.Length; i++)
                    {
                        float Rotation;

                        float RotationAngle = MathF.Atan2(Direction.X, Direction.Y) * -1f; // Gets an angle form Direction
                        Rotation = float.RadiansToDegrees(RotationAngle) + 90 + FireAngle[i]; // Turns that angle into Degrees and adds 90 Degrees and random spride
                        RotationAngle = float.DegreesToRadians(Rotation); // Turns it back into a angle

                        Vector2 Shot = new Vector2(float.Cos(RotationAngle), float.Sin(RotationAngle));

                        BaseProjectile Bullet = new BaseProjectile();
                        Bullet.Setup(GetGame, this, FireBallDamage, Position, Shot * ProSpeed);
                    }
                }

                break;

            case 2:
                Target = GetClosetPlayer();

                if (Die1) { GetGame.StartDialogue(GetGame.GetDialoguePersonally.BossRaphText[13], 5f); Die2 = false; }


                if (Target != null)
                {
                    Direction = Target.Position - Position;
                    Move(Direction);
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
                break;
        }

    }
}
