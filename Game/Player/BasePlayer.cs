using System;
using System.Numerics;
using MohawkGame2D;

public class BasePlayer : BaseCharacter
{
    public BaseItem[] Items = new BaseItem[0];

    public int PlayerIndex = 0;

    // Inventory HUD
    const int InventorySlotCount = 6;
    int InventoryIndex = 0;

    public override void CustomSetup()
    {
        base.CustomSetup();
        Items = new BaseItem[InventorySlotCount];
    }

    /// <summary>
    ///     Draws the Hud to the screen.
    /// </summary>
    public void DrawInventoryHud()
    {
        float slotWidth = 72;
        float slotHeight = 72;
        float gap = 12;
        float totalWidth = (InventorySlotCount * slotWidth) + ((InventorySlotCount - 1) * gap);

        float startX = (Window.Width - totalWidth) / 2;
        float y = 20;

        for (int i = 0; i < InventorySlotCount; i++)
        {
            float x = startX + (i * (slotWidth + gap));

            Draw.FillColor = new Color(20, 20, 20, 190);
            Draw.LineColor = Color.Black;
            Draw.LineSize = 2;
            Draw.Rectangle(x, y, slotWidth, slotHeight);
            if (Items[i] != null)
            {
                Items[i].RenderInv(new Vector2(x, y));
            }

            if (i == InventoryIndex)
            {
                Draw.FillColor = Color.Clear;
                Draw.LineColor = Color.Yellow;
                Draw.LineSize = 4;
                Draw.Rectangle(x - 3, y - 3, slotWidth + 6, slotHeight + 6);
            }

            Text.Draw($"{i + 1}", (int)(x + 30), (int)(y + 24));
        }

        Text.Draw("Inventory", (int)(startX), (int)(y - 22));
    }

    /// <summary>
    ///     Used for geting Inputs for the player.
    /// </summary>
    public virtual void Controlls()
    {
        // Keybored controlls
        if (PlayerIndex == 0)
        {
            // Movement Input
            if (Input.IsKeyboardKeyDown(KeyboardInput.W))
                Move(new Vector2(0, -1));
            if (Input.IsKeyboardKeyDown(KeyboardInput.S))
                Move(new Vector2(0, 1));
            if (Input.IsKeyboardKeyDown(KeyboardInput.A))
                Move(new Vector2(-1, 0));
            if (Input.IsKeyboardKeyDown(KeyboardInput.D))
                Move(new Vector2(1, 0));

            // Look Input
            Direction = Vector2.Normalize(Input.GetMousePosition() - Position);

            // Use item
            if (Input.IsMouseButtonPressed(MouseInput.Left))
            {
                if (Items[InventoryIndex] == null)
                    return;
                Items[InventoryIndex].UseItem(Position, Direction);
            }
            // Use Item Spacl
            if (Input.IsKeyboardKeyPressed(KeyboardInput.R))
            {
                if (Items[InventoryIndex] == null)
                    return;
                Items[InventoryIndex].UseItemSpacl(Position, Direction);
            }

            // Use malee
            if (Input.IsMouseButtonPressed(MouseInput.Right))
            {
                if (GetGame == null)
                    return;
                GetGame.DamageAllInRadius(this, Position, 100f, 1f, Direction * 600f);
            }

            // Inventory Input
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Q))
            {
                InventoryIndex--;
                if (InventoryIndex < 0)
                    InventoryIndex = InventorySlotCount - 1;
            }

            if (Input.IsKeyboardKeyPressed(KeyboardInput.E))
            {
                InventoryIndex++;
                if (InventoryIndex >= InventorySlotCount)
                    InventoryIndex = 0;
            }

            if (Input.IsKeyboardKeyPressed(KeyboardInput.F))
            {
                BaseItem PickItem = GetGame.PickupItem(Position);

                if (PickItem != null)
                {
                    Items[InventoryIndex] = PickItem;

                    Items[InventoryIndex].Position = Vector2.Zero;
                }
            }

            if (Input.IsKeyboardKeyPressed(KeyboardInput.One))
                InventoryIndex = 0;
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Two))
                InventoryIndex = 1;
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Three))
                InventoryIndex = 2;
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Four))
                InventoryIndex = 3;
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Five))
                InventoryIndex = 4;
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Six))
                InventoryIndex = 5;

        }
    }

    public override void Render()
    {
        Controlls();
        base.Render();
        Text.Draw($"{HP}", new Vector2(Position.X, Position.Y + 10));
    }
}
