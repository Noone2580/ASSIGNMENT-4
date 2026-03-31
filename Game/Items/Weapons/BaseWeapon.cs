using System;
using System.Numerics;
using MohawkGame2D;

public class BaseWeapon : BaseItem
{
	public int Ammo = 0;
	public int MaxAmmo = 5;


    public override void UseItem(Vector2 position, Vector2 direction)
    {
        base.UseItem(position, direction);
    }
    public override void UseItemSpacl(Vector2 position, Vector2 direction)
    {
        base.UseItemSpacl(position, direction);
    }
}
