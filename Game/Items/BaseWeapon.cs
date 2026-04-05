using System;
using System.Numerics;
using MohawkGame2D;

public class BaseWeapon : BaseItem
{
    public int AmmoType = 0;
    public int Ammo = 5;
    public int MaxAmmo = 5;
    public float ProSpeed = 950f;
    public float Damage = 20;
    public float Range = 10;
    public float FireRate = .2f;

    public override void CustomSetup()
    {
        base.CustomSetup();
    }

    public override void UseItem(Vector2 position, Vector2 direction)
    {
        if (GetGame == null || Owner == null) return;

        if (IsTimerDone(0) && Ammo > 0)
        {
            SetTimer(0, FireRate);
            BaseProjectile Bullet = new BaseProjectile();
            Bullet.Setup(GetGame, Owner, Damage, Position, direction * ProSpeed);
            Ammo--;
        }
    }


    public override void UseItemSpacl(Vector2 position, Vector2 direction)
    {
        base.UseItemSpacl(position, direction);
    }
}
