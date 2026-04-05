using System;
using System.Numerics;

public class BlueKey : BaseKeys
{
    public override void CustomSetup()
    {
        base.CustomSetup();
        InventorySpriteLocation = new Vector2(72, 72 * 2);
        RoomCode = 2;
    }
}