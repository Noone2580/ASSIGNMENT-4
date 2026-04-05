using System;
using System.Numerics;
using MohawkGame2D;

public class BasePlayer : BaseCharacter
{
    public BaseItem[] Items = new BaseItem[0];

    public int PlayerIndex = 0;
    public bool CanUseItems = true;

    // Inventory HUD
    const int InventorySlotCount = 6;
    public int InventoryIndex = 0;

    public override void CustomSetup()
    {
        base.CustomSetup();
        Items = new BaseItem[InventorySlotCount];
    }

    public virtual void Interact()
    {
        if (GetGame == null) { return; }

        BaseItem PickItem = GetGame.PickupItem(Position);
        if (PickItem != null)
        {
            if (Items[InventoryIndex] != null)
            {
                Items[InventoryIndex].Drop(Position, GetGame.Grid.CurrentRoomPosition);
            }

            Items[InventoryIndex] = PickItem;
            Items[InventoryIndex].Owner = this;
            Items[InventoryIndex].Position = Vector2.Zero;
            return;
        }

        GetGame.TryOpenDoor();
    }

    /// <summary>
    ///     Draws the Hud to the screen.
    /// </summary>
    public void DrawHud()
    {
        float WindowGap = 5;
        float slotWidth = 72;
        float slotHeight = 72;
        float gap = 12;
        float totalWidth = (InventorySlotCount * slotWidth) + ((InventorySlotCount - 1) * gap);

        float startX = (75);
        float startY = (75);
        float y = WindowGap;
        int OffsetIndex = 0;

        switch (PlayerIndex)
        {
            case 0:
                for (int i = 0; i < InventorySlotCount; i++)
                {

                    if (i < InventorySlotCount / 2)
                    {
                        float x = startX + (i * (slotWidth + gap));

                        Draw.FillColor = new Color(20, 20, 20, 190);
                        Draw.LineColor = Color.Black;
                        Draw.LineSize = 2;
                        Draw.Rectangle(x, y, slotWidth, slotHeight);
                        if (Items[i] != null)
                        {
                            Items[i].RenderInv(new Vector2(x, y));
                            Items[i].Position = Position;
                        }
                        if (i == InventoryIndex)
                        {
                            Draw.FillColor = Color.Clear;
                            Draw.LineColor = Color.Yellow;
                            Draw.LineSize = 4;
                            Draw.Rectangle(x - 3, y - 3, slotWidth + 6, slotHeight + 6);
                            if (Items[i] != null)
                                Items[i].RenderHolding(Rotation, Position);
                        }
                    }

                    else
                    {
                        y = startY + (OffsetIndex * (slotWidth + gap));
                        float x = WindowGap;
                        OffsetIndex += 1;

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
                            if (Items[i] != null)
                                Items[i].RenderHolding(Rotation, Position);
                        }
                    }


                }
                break;
        }
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

            if (Input.IsMouseButtonDown(MouseInput.Left))
            {
                if (Items[InventoryIndex] == null)
                    return;
                Items[InventoryIndex].UseItemFrame(Position, Direction);
            }

            if (Input.IsMouseButtonReleased(MouseInput.Left))
            {
                if (Items[InventoryIndex] == null)
                    return;
                Items[InventoryIndex].StopUsingItem(Position, Direction);
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
                GetGame.DamageAllInRadiusButSelf(this, Position, 50f, 1f, Direction * 600f);
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
                Interact();
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
