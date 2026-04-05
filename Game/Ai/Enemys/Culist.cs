using MohawkGame2D;
using System.Buffers.Text;
using System.Numerics;

public class Cultist : BaseEnemy
{
    BaseCharacter? HealTarget;
    bool ProtectingTarget = false;

    public override void CustomSetup()
    {
        base.CustomSetup();
        MovementSpeed = 20;
        MaxHP = 50f;
        HP = MaxHP;
        BodyTextureLocation = "../../../Assets/Textures/Culist.png";
    }

    public void FindHealTarget()
    {
        if (GetGame == null) return;

        if (GetGame.TheBoss != null)
        {
            HealTarget = GetGame.TheBoss;
            return;
        }

        BaseCharacter character = GetGame.GetAllAis()[Random.Integer(0, GetGame.GetAllAis().Length - 1)];

        if (character != null && character != this)
        {
            HealTarget = character;
        }
    }

    public void ProtectTarget()
    {
        if (HealTarget == null || ProtectingTarget) return;
        ProtectingTarget = true;
        HealTarget.DamageResistance -= 0.5f;
        MovementSpeed = HealTarget.MovementSpeed * 0.7f ;
    }

    public override void Render()
    {
        base.Render();

        if (HealTarget == null) { FindHealTarget(); return; }

        ProtectTarget();
        if (HealTarget != GetGame.TheBoss || GetGame.TheBoss == null)
        {
            Direction = HealTarget.Position - Position;
            Move(Direction);
        }

    }
}
