using MohawkGame2D;
using System;
using System.Numerics;

public class StartingRoom : BaseRoom
{
    public override void CustomSetup()
    {

        Doors = new BaseDoor[1];
        ConectedRooms = new BaseRoom[1];

        for (int i = 0; i < Doors.Length; i++)
        {
            Doors[i] = new BaseDoor();

            switch (i)
            {
                case 0:
                    Doors[i].Position = new Vector2(Window.Width, Window.Height / 2);
                    Doors[i].EndPosition = new Vector2(0, Window.Height / 2);
                    ConectedRooms[i] = new Hallway1();
                    break;
            }
            Doors[i].Setup();
        }


    }

    public override void Render()
    {
        base.Render();

        Text.Draw("1", Window.Width / 2, Window.Height / 2);
    }
}
