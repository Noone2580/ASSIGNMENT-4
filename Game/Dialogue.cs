using Microsoft.VisualBasic;
using System;
using System.Numerics;
using System.Threading;

// Double \n\n for new lines. The font is kinda small. 

public class TextBoxDialogue
{
    public string[] PlayerText =
        [
        "*mumbles*",
        "*mumble mumble*",
        "The worst virus I've faced today,\n\nnot the zombies.\n\nFinally something to drink.",

        ];

    public string[] NarrationText =
        [
        "Welcome to 'The Wrath of Raph'.\n\nOur silly Resident Evil, Legend of Zelda, \n\nSignalis inspired game for assignment 4.",
        "You must find the keys scattered across the ruins of mohawk campus,\n\nfight off hordes of zombies, and defeat the evil wizard\n\nRaph.",
        "Congratulations!",
        "You're adopted!",
        "Also you've stopped the evil wizard I guess.",

        ];

    public static string[] SystemText =
        [
        "You've encountered an Error",
        "You've avoided an Error",
        "There is an Error Amogus",
        "Bank Error in your favour, collect $200",

        ];

    public static string[] BossRaphText =
       [
            //Game Start Boss Dialogue.
            "Ahhh, come to stop my minions of darkness?",
            "What a Fool you are!",
            "Choosing to noot answer me.\n\nCowardly!\n\nNo matter, none can stop the great Raph-am!",
            //Dialogue after claiming the first key
            "I sense a presence in the building"


            //Dialogue when you enter Raph's Lair 
            "Foolish, FOOLISH! You should have feld when  \n\nNo mere mortal is capable of withstanding\n\n the Wrath of Raphael Ambrosius Costeau!",
            "The great shapeshifting master of evil,\n\nthe duke of your demise,\n\nthe wrathful wizard\n\nRaph-am!",
        ];

}

