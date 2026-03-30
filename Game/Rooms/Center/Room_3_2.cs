using MohawkGame2D;
using System.Numerics;

public class Room_3_2 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "D";
        MapPosition = new Vector2(3, 2);

        Doors = new BaseDoor[2];
        ConectedRooms = new BaseRoom[2];

        AddDoor(0,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new CenterRoom());

        AddDoor(1,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Room_3_1());
    }
}
