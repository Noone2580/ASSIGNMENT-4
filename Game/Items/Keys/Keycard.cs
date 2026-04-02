using System;

public class Keycard : BaseKeys
{
    public override void CustomSetup()
    {
        base.CustomSetup();

        InventoryTextureLocation = "../../../Assets/Textures/Keycard.png";
        RoomCode = 1;
    }
}
