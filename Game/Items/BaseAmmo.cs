using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using MohawkGame2D;

public class BaseAmmo : BaseItem
{
    public int AmmoType = 0;
    public int MaxAmmo = 20;
    public int Ammo = 20;

    public override void CustomSetup()
    {
        base.CustomSetup();
        InventorySpriteLocation = new Vector2(0, 72 * 3);
    }

    public int TakeAmmo(int ammo)
    {
        Ammo = int.Clamp(Ammo - ammo, 0, MaxAmmo);
        if (Ammo <= 0)
        {
            for (int i = 0; i < OwnerAsPlayer.Items.Length; i++)
            {
                if (OwnerAsPlayer.Items[i] == this) 
                {
                    OwnerAsPlayer.Items[i] = null;
                }
            }
        }
        return Ammo;
    }

    public override void RenderInv(Vector2 position)
    {
        base.RenderInv(position);

        Text.Color = Color.White;
        Text.Draw($"{Ammo}", position + (new Vector2(35, 40)));
        Text.Color = Color.Black;
    }
}

public class Ammo_Pistol : BaseAmmo
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        AmmoType = 0;
        MaxAmmo = 25;
        Ammo = MaxAmmo;
        InventorySpriteLocation = new Vector2(0, 72 * 3);
    }
}
public class Ammo_Shotgun : BaseAmmo
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        AmmoType = 1;
        MaxAmmo = 30;
        Ammo = MaxAmmo;
        InventorySpriteLocation = new Vector2(72, 72 * 3);
    }
}
public class Ammo_AssulitRifle : BaseAmmo
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        AmmoType = 2;
        MaxAmmo = 60;
        Ammo = MaxAmmo;
        InventorySpriteLocation = new Vector2(72 * 2, 72 * 3);
    }
}
