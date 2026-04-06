using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MohawkGame2D;

public class Shotgun : BaseWeapon
{
    int NumPel = 10;
    float spred = 20f;

    public override void CustomSetup()
    {
        base.CustomSetup();
        FireRate = .4f;
        InventorySpriteLocation = new Vector2(72, 72);
        Damage = 40;
        MaxAmmo = 8;
        Ammo = MaxAmmo;
        AmmoType = 1;
    }


    public override void UseItem(Vector2 position, Vector2 direction)
    {
        if (GetGame == null || Owner == null) return;

        if (IsTimerDone(0) && Ammo > 0 )
        {
            SetTimer(0, FireRate);

            for (int i = 0; i < NumPel; i++)
            {
                float Rotation;

                float RotationAngle = MathF.Atan2(direction.X, direction.Y) * -1f; // Gets an angle form Direction
                Rotation = float.RadiansToDegrees(RotationAngle) + 90 + MohawkGame2D.Random.Float(-spred, spred); // Turns that angle into Degrees and adds 90 Degrees and random spride
                RotationAngle = float.DegreesToRadians(Rotation); // Turns it back into a angle

                Vector2 Shot = new Vector2(float.Cos(RotationAngle), float.Sin(RotationAngle));

                BaseProjectile Bullet = new BaseProjectile();
                Bullet.Setup(GetGame, Owner, Damage / NumPel, Position, Shot * ProSpeed);
            }
            Ammo--;
        }
    }
}
