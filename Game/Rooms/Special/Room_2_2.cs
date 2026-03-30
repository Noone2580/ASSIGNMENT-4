using MohawkGame2D;
using System.Numerics;

public class Room_2_2 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "F";
        MapPosition = new Vector2(2, 2);

        Doors = new BaseDoor[2];
        ConectedRooms = new BaseRoom[2];

        AddDoor(0,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new Room_2_3());

        AddDoor(1,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Room_2_1());
    }
}
