using MohawkGame2D;
using System.Numerics;

public class Room_1_4 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "1";
        MapPosition = new Vector2(1, 4);

        Doors = new BaseDoor[3];
        ConectedRooms = new BaseRoom[3];

        AddDoor(0,
            new Vector2(0, Window.Height / 2),
            new Vector2(Window.Width - 100, Window.Height / 2),
            new StartingRoom());

        AddDoor(1,
            new Vector2(Window.Width, Window.Height / 2),
            new Vector2(100, Window.Height / 2),
            new Room_2_4());

        AddDoor(2,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Room_1_3());
    }
}
