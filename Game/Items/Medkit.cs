using System;
using System.Numerics;

public class Medkit : BaseWeapon
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        FireRate = .4f;
        InventorySpriteLocation = new Vector2(0, 72 * 4);
        Damage = -20;
        MaxAmmo = 2;
        Ammo = MaxAmmo;
        AmmoType = 8;
    }

    public override void UseItem(Vector2 position, Vector2 direction)
    {
        //base.UseItem(position, direction);
        Owner.TakeDamage(Damage, new Vector2(0));
    }
}
