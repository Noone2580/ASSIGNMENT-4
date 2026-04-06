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

    public override void CustomSetup()
    {
        base.CustomSetup();
        BodyTextureLocation = "../../../Assets/Textures/Boss.png";
    }

    public override void TakeDamage(float Damage, Vector2 HitForce)
    {
        base.TakeDamage(Damage, HitForce);

    }

    public override void Render()
    {
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
        }

    }
}
