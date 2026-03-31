using Microsoft.VisualBasic;
using System;
using System.Numerics;
using System.Threading;
using MohawkGame2D;
using System.Security.Cryptography.X509Certificates;



public static class TextBox
{
    
    public static Font BitCount;

    public static void initialize()
    {
        BitCount = Text.LoadFont("..\\..\\..\\Game\\Fonts\\Bitcount - Regular.ttf");
    }

    public static void Write(string text)
    {
        Draw.FillColor = Color.Black;
        Draw.Rectangle(190,690,720,220);
        Draw.FillColor = Color.White;
        Draw.Rectangle(200, 700, 700, 200);
        Text.Draw(text, new Vector2(200,700), BitCount);
    }
}

