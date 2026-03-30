using MohawkGame2D;
using System.Numerics;

public class Room_2_3 : BaseRoom
{
    public override void CustomSetup()
    {
        RoomName = "B";
        MapPosition = new Vector2(2, 3);
        RoomCode = 1;


        Doors = new BaseDoor[3];
        ConectedRooms = new BaseRoom[3];

        AddDoor(0,
            new Vector2(Window.Width / 2, Window.Height),
            new Vector2(Window.Width / 2, 100),
            new Room_2_4());

        AddDoor(1,
            new Vector2(Window.Width, Window.Height / 2),
            new Vector2(100, Window.Height / 2),
            new CenterRoom());

        AddDoor(2,
            new Vector2(Window.Width / 2, 0),
            new Vector2(Window.Width / 2, Window.Height - 100),
            new Room_2_2());
    }
}
