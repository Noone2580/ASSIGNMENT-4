using MohawkGame2D;
using System.Numerics;

public class Room_4_4 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "J";
        MapPosition = new Vector2(4, 4);

        Doors = new BaseDoor[2];
        ConectedRooms = new BaseRoom[2];

        AddDoor(0,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Room_4_3());

        AddDoor(1,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new Room_4_5());
    }
}
