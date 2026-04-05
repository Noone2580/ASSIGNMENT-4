using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MohawkGame2D;

public class AssulitRifle : BaseWeapon
{
    bool Fire = false;
    Vector2 Direction = Vector2.Zero;

    public override void CustomSetup()
    {
        base.CustomSetup();
        FireRate = .1f;
        InventorySpriteLocation = new Vector2(72*2, 72);
        Damage = 1;
        MaxAmmo = 25;
        Ammo = MaxAmmo;
    }

    public override void UseItemFrame(Vector2 position, Vector2 direction)
    {
        if (GetGame == null || Owner == null) return;

        if (IsTimerDone(0) && Ammo > 0 )
        {
            SetTimer(0, FireRate);
            BaseProjectile Bullet = new BaseProjectile();
            Bullet.Setup(GetGame, Owner, Damage, Position, direction * ProSpeed);
            Ammo--;
        }
    }

    public override void UseItem(Vector2 position, Vector2 direction)
    {
    }

    public override void StopUsingItem(Vector2 position, Vector2 direction)
    {
    }

    public override void RenderHolding(float rotation, Vector2 position)
    {
        base.RenderHolding(rotation, position);
    }

}
