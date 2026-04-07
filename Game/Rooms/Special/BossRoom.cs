using MohawkGame2D;
using System.Numerics;

/// <summary>
///     AllWays Check to see if there is a room next to it in any direction.
/// </summary>
public class BossRoom : BaseRoom
{
    Vector2[] CultistSpawn = [ new Vector2(150), new Vector2(Window.Width -150, 150)]; 

    public override void CustomSetup()
    {
        RoomName = "BossRoom";
        Doors = new BaseDoor[4];
        IsBossRoom = true;

        GetGame.StartDialogue(GetGame.GetDialoguePersonally.BossRaphText[8], 5f);

        GetGame.SpawnBoss();
        GetGame.AddEnemy(new Cultist(), GridPosition, CultistSpawn[0]);
        GetGame.AddEnemy(new Cultist(), GridPosition, CultistSpawn[1]);
        
    }
}