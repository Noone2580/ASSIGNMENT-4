using MohawkGame2D;
using System.Numerics;

public class Room_1_5 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "N";
        MapPosition = new Vector2(1, 5);

        Doors = new BaseDoor[2];
        ConectedRooms = new BaseRoom[2];

        AddDoor(0,
            new Vector2(Window.Width, Window.Height / 2),
            new Vector2(100, Window.Height / 2),
            new Room_2_5());

        AddDoor(1,
            new Vector2(0, Window.Height / 2),
            new Vector2(Window.Width - 100, Window.Height / 2),
            new Room_0_5());
    }
}
