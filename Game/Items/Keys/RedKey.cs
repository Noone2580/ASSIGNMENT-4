using System;
using System.Numerics;

public class RedKey : BaseKeys
{
    public override void CustomSetup()
    {
        base.CustomSetup();

        InventorySpriteLocation = new Vector2(72 * 2, 72 * 2);

        RoomCode = 3;
    }
}