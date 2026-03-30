using MohawkGame2D;
using System.Numerics;

public class Room_4_2 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "I";
        MapPosition = new Vector2(4, 2);

        Doors = new BaseDoor[2];
        ConectedRooms = new BaseRoom[2];

        AddDoor(0,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Room_4_1());

        AddDoor(1,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new Room_4_3());
    }
}
