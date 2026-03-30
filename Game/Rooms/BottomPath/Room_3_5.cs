using MohawkGame2D;
using System.Numerics;

public class Room_3_5 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "L";
        MapPosition = new Vector2(3, 5);

        Doors = new BaseDoor[3];
        ConectedRooms = new BaseRoom[3];

        AddDoor(0,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Room_3_4());

        AddDoor(1,
            new Vector2(0, Window.Height / 2),
            new Vector2(Window.Width - 100, Window.Height / 2),
            new Room_2_5());

        AddDoor(2,
            new Vector2(Window.Width, Window.Height / 2),
            new Vector2(100, Window.Height / 2),
            new Room_4_5());
    }
}
