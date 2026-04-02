using System;

public class Key : BaseKeys
{
    public override void CustomSetup()
    {
        base.CustomSetup();

        InventoryTextureLocation = "../../../Assets/Textures/Key.png";
        RoomCode = 0;
    }
}