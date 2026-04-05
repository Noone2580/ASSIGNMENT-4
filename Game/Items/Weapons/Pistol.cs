using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MohawkGame2D;

public class Pistol : BaseWeapon
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        FireRate = .2f;
        InventorySpriteLocation = new Vector2(0, 72);
        Damage = 5;
        MaxAmmo = 12;
        Ammo = MaxAmmo;
    }
}
