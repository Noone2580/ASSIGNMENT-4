using Microsoft.VisualBasic;
using System;
using System.Numerics;
using System.Threading;
using MohawkGame2D;
using System.Security.Cryptography.X509Certificates;



public class TextBox
{
    
    public Font BitCount;

    public void initialize()
    {
        BitCount = Text.LoadFont("..\\..\\..\\Game\\Fonts\\Bitcount - Regular.ttf");
    }

    public void Write(string text)
    {
        Text.Font = BitCount;
    }
}

