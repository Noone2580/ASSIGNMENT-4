using System;
using System.Numerics;
using MohawkGame2D;

public class RoomGrid
{
    Game? GetGame;
    public int GridSize = 50;
    public Vector4[] Rooms = new Vector4[0];
    public Vector2 CurrentRoomPosition = Vector2.Zero;
    protected BaseRoom[] RoomTypes = new BaseRoom[0];

    /// <summary>
    ///		For adding rooms. it takes a Grid Postion, a Room Type, and a Room Code.
	///		If the Room Code is grender then 0 then the door is locked.
	///		Room Types are what kind of room is it. And goes as follows.
	///		<list type="number">-|- AllWay
	///      <item> |" TopLeftCorner</item>
	///		 <item>"|  TopRightCorner</item>
	///		 <item>|_  BottomLeftCorner</item>
	///		 <item>_| BottomRightCorner</item>
	///		 <item>|- ThreeWayRight</item>
	///		 <item>-| ThreeWayLeft</item>
	///		 <item>_|_ ThreeWayTop</item>
	///		 <item>"|" ThreeWayBottom</item>
	///		 <item>| HallwayVertical</item>
	///		 <item>-- HallwayHorizontal</item>
	///		 <item>-|- BossRoom</item>
	///		</list>
    /// </summary>
	/// <remarks><em><b>Returns</b></em> the index of the new Room</remarks>
    /// <param name="GridPostion"></param>
    /// <param name="RoomType"></param>
    /// <param name="RoomCode"></param>
    /// <returns></returns>
    public int AddRoom(Vector2 GridPostion, int RoomType, int RoomCode)
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            if (Rooms[i] == Vector4.Zero)
            {
                Rooms[i] = new Vector4(GridPostion, RoomType, RoomCode);
                return i;
            }
        }
        return -1;
    }


    public BaseRoom GetRoomClassAtGrid(Vector2 GridPosition)
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            if (GridPosition == new Vector2(Rooms[i].X, Rooms[i].Y))
            {
                return RoomTypes[((int)Rooms[i].Z)];
            }
        }

        return null;
    }

    public Vector4 GetRoomAtGrid(Vector2 GridPosition)
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            if (GridPosition == new Vector2(Rooms[i].X, Rooms[i].Y))
            {
                return Rooms[i];
            }
        }

        return Vector4.Zero;
    }

    public int GetIndexAtGrid(Vector2 GridPosition)
    {
        for (int i = 0; i < Rooms.Length; i++)
        {
            if (GridPosition == new Vector2(Rooms[i].X, Rooms[i].Y))
            {
                return i;
            }
        }

        return -1;
    }

    public Vector4 GetRoomAtIndex(int index)
    {
        index = int.Clamp(index, 0, Rooms.Length - 1);
        return Rooms[index];
    }

    public Vector4 SetRoomAtIndex(int index, Vector4 newRoom)
    {
        index = int.Clamp(index, 0, Rooms.Length - 1);
        Rooms[index] = newRoom;
        return Rooms[index];
    }

    public void Setup(Game game)
    {
        GetGame = game;
        Rooms = new Vector4[GridSize];
        RoomTypes =
            [
            new AllWay(),
            new TopLeftCorner(),
            new TopRightCorner(),
            new BottomLeftCorner(),
            new BottomRightCorner(),
            new ThreeWayRight(),
            new ThreeWayLeft(),
            new ThreeWayTop(),
            new ThreeWayBottom(),
            new HallwayVertical(),
            new HallwayHorizontal(),
            new BossRoom()
            ];


        // This is where the Map is created. 
        // Vector 2 controls grid coordinates. 
        // The third number controls where the doors are generated, and the fourth number controls the keycard level required. 
        // Below is a frame of reference for what the map should look like. 
        // Numbers represent the required level of Keycard to proceed. 
        //  0   1   2   3   4   5   6   7   8   9   10
        //  1      Key1 x
        //  2           |   Gt -|       |   SG
        //  3       |   |       2       x       x
        //  4       |-  -   -   -       2       x  Key2
        //  5 Start x           1   -   x       x
        //  6                   |  Boss |       x
        //  7           x   2   x   x   x   x   1
        //  8   x   3   x       x           3
        //  9           x                   x
        //  10         Key3                 AR

        //Row 1
        AddRoom(new Vector2(2, 1), 0, 0);
        GetGame.AddItem(new Key(), new Vector2(2, 1), new Vector2(Window.Width / 2, Window.Height / 2));
        

        AddRoom(new Vector2(3, 1), 2, 0);

        //Row 2
        AddRoom(new Vector2(3, 2), 9, 0);
        AddRoom(new Vector2(4, 2), 0, 0);
        GetGame.AddItem(new Guitar(), new Vector2(4, 2), new Vector2(Window.Width / 2, Window.Height / 2));

        AddRoom(new Vector2(5, 2), 2, 0);

        AddRoom(new Vector2(7, 2), 1, 0);
        AddRoom(new Vector2(8, 2), 0, 0);
            GetGame.AddItem(new Shotgun(), new Vector2(8, 2), new Vector2(Window.Width / 2, Window.Height / 2));

        //Row 3
        AddRoom(new Vector2(2, 3), 9, 0);
            GetGame.AddItem(new Pistol(), new Vector2(2, 3), new Vector2(Window.Width / 2, Window.Height / 2));

        AddRoom(new Vector2(3, 3), 9, 0);
        AddRoom(new Vector2(5, 3), 9, 2);
        AddRoom(new Vector2(7, 3), 0, 0);
        AddRoom(new Vector2(9, 3), 0, 0);

        //Row 4
        AddRoom(new Vector2(2, 4), 5, 0);
            GetGame.AddEnemy(new Zombie(), new Vector2(2, 4), new Vector2(Window.Width / 2, Window.Height / 2));
        AddRoom(new Vector2(3, 4), 7, 0);
        AddRoom(new Vector2(4, 4), 0, 0);
        AddRoom(new Vector2(5, 4), 0, 0);

        AddRoom(new Vector2(7, 4), 0, 2);

        AddRoom(new Vector2(9, 4), 0, 0);
        AddRoom(new Vector2(10, 4), 0, 1);
            GetGame.AddItem(new BlueKey(), new Vector2(10, 4), new Vector2(Window.Width / 2, Window.Height / 2));


        //Row 5
        AddRoom(new Vector2(1, 5), 0, 0);
        AddRoom(new Vector2(2, 5), 0, 0);
            GetGame.AddItem(new Kinfe(), new Vector2(2, 5), new Vector2(Window.Width / 2, Window.Height / 2));
        

        AddRoom(new Vector2(5, 5), 0, 1);
        AddRoom(new Vector2(6, 5), 10, 0);
        AddRoom(new Vector2(7, 5), 0, 0);

        AddRoom(new Vector2(9, 5), 0, 0);

        //Row 6 
        AddRoom(new Vector2(5, 6), 9, 0);
        AddRoom(new Vector2(6, 6), 11, 3);// Boss Room
        AddRoom(new Vector2(7, 6), 9, 0);

        AddRoom(new Vector2(9, 6), 0, 0);

        //Row 7
        AddRoom(new Vector2(3, 7), 0, 0);
        AddRoom(new Vector2(4, 7), 0, 2);
        AddRoom(new Vector2(5, 7), 0, 0);
        AddRoom(new Vector2(6, 7), 0, 0);
        AddRoom(new Vector2(7, 7), 0, 0);
        AddRoom(new Vector2(8, 7), 0, 0);
        AddRoom(new Vector2(9, 7), 0, 1);

        //Row 8
        AddRoom(new Vector2(1, 8), 0, 0);
            //Probably should be an ammo stash or something. 
            GetGame.AddItem(new Ammo_Pistol(),new Vector2(1,8),new Vector2(Window.Width / 2,Window.Height / 1.5f));
        AddRoom(new Vector2(2, 8), 0, 3);
        AddRoom(new Vector2(3, 8), 0, 0);

        AddRoom(new Vector2(5, 8), 0, 0);

        AddRoom(new Vector2(8, 8), 0, 3);

        //Row 9 
        AddRoom(new Vector2(3, 9), 0, 0);

        AddRoom(new Vector2(8, 9), 0, 0);

        //Row 10
        AddRoom(new Vector2(3, 10), 0, 0);
            GetGame.AddItem(new RedKey(), new Vector2(3, 10), new Vector2(Window.Width / 2, Window.Height / 2));

        AddRoom(new Vector2(8, 10), 0, 0);
            GetGame.AddItem(new AssulitRifle(), new Vector2(8, 10), new Vector2(Window.Width / 2, Window.Height / 2));


    }
}
