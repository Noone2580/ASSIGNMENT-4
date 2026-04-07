using MohawkGame2D;
using System.Numerics;

/// <summary>
///     AllWays Check to see if there is a room next to it in any direction.
/// </summary>
public class BossRoom : BaseRoom
{
    Vector2[] CultistSpawn = [new Vector2(150), new Vector2(Window.Width - 150, 150)];

    public override void CustomSetup()
    {
        RoomName = "BossRoom";
        Doors = new BaseDoor[4];
        IsBossRoom = true;

        GetGame.StartDialogue(GetGame.GetDialoguePersonally.BossRaphText[8], 5f);

        GetGame.SpawnBoss();
        GetGame.AddEnemy(new Cultist(), GridPosition, CultistSpawn[0]);
        GetGame.AddEnemy(new Cultist(), GridPosition, CultistSpawn[1]);
        GetGame.SetTimer(9, 5f);
    }

    public override void Render()
    {
        base.Render();

        if (GetGame.TheBoss.Phase >= 1 && GetGame.IsTimerDone(8)) 
        {
            GetGame.SetTimer(8,7f);
            for (int i = 0; i < Random.Integer(0, 15); i++)
            {
                int EE = Random.Integer(0, 4);
                switch (EE)
                {
                    case 0:
                        GetGame.AddEnemy(new Zombie(), GridPosition, Random.Vector2(new Vector2(RightWallCal, BottomWallCal), new Vector2(LeftWallCal, TopWallCal)));
                        break;
                    case 1:
                        GetGame.AddEnemy(new Z_Tank(), GridPosition, Random.Vector2(new Vector2(RightWallCal, BottomWallCal), new Vector2(LeftWallCal, TopWallCal)));
                        break;
                    case 2:
                        GetGame.AddEnemy(new Spiter(), GridPosition, Random.Vector2(new Vector2(RightWallCal, BottomWallCal), new Vector2(LeftWallCal, TopWallCal)));
                        break;
                    case 3:
                        GetGame.AddEnemy(new Cultist(), GridPosition, Random.Vector2(new Vector2(RightWallCal, BottomWallCal), new Vector2(LeftWallCal, TopWallCal)));
                        break;
                }
            }
        }

        if (GetGame.IsTimerDone(9))
        {
            GetGame.SetTimer(9, 10f);

            switch (Random.Integer(0, 3))
            {
                case 0:
                    GetGame.AddItem(new Ammo_Pistol(), GetGame.Grid.CurrentRoomPosition, new Vector2(Window.Width /2, Window.Height - 200));
                    break;
                case 1:
                    GetGame.AddItem(new Ammo_Shotgun(), GetGame.Grid.CurrentRoomPosition, new Vector2(Window.Width /2, Window.Height - 200));
                    break;
                case 2:
                    GetGame.AddItem(new Ammo_AssulitRifle(), GetGame.Grid.CurrentRoomPosition, new Vector2(Window.Width / 2, Window.Height - 200));
                    break;
                case 3:
                    GetGame.AddItem(new Medkit(), GetGame.Grid.CurrentRoomPosition, new Vector2(Window.Width / 2, Window.Height - 200));
                    break;
            }
        }
    }
}