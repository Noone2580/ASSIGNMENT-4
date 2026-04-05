using System.Numerics;
using System.Runtime.CompilerServices;
using MohawkGame2D;

public class Shotgun : BaseWeapon
{
    int NumPel = 5;
    float spred = .5f;

    public override void CustomSetup()
    {
        base.CustomSetup();
        FireRate = .4f;
        InventorySpriteLocation = new Vector2(72, 72);
        Damage = 20;
        MaxAmmo = 8;
        Ammo = MaxAmmo;
    }


    public override void UseItem(Vector2 position, Vector2 direction)
    {
        if (GetGame == null || Owner == null) return;

        if (IsTimerDone(0))
        {
            SetTimer(0, FireRate);

            for (int i = 0; i < NumPel; i++)
            {
                BaseProjectile Bullet = new BaseProjectile();
                Vector2 Shot = direction + Random.Vector2(-spred, spred);
                Shot = Vector2.Normalize(Shot);

                Bullet.Setup(GetGame, Owner, Damage / NumPel, Position, Shot * ProSpeed);
            }
        }
    }
}
