using System;
using System.Numerics;
using MohawkGame2D;

public class BaseWeapon : BaseItem
{
    public int AmmoIndex = 0;
	public int Ammo = 0;
	public int MaxAmmo = 5;
    public float ProSpeed = 600f;

    public override void UseItem(Vector2 position, Vector2 direction)
    {
        //base.UseItem(position, direction);

        BaseProjectile Bullet = new BaseProjectile();
        Bullet.Setup(GetGame, Owner, Position, direction * ProSpeed);
    }
    public override void UseItemSpacl(Vector2 position, Vector2 direction)
    {
        base.UseItemSpacl(position, direction);
    }
}
