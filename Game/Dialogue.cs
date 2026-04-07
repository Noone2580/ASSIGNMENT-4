using Microsoft.VisualBasic;
using System;
using System.Numerics;
using System.Threading;

// Double \n\n for new lines. The font is kinda small. 
// Most of this is self expanetory. These are string arrays that have a bunch of dialogue saved that can be called within the game.cs 
// when needed. 

public class TextBoxDialogue
{
    public string[] PlayerText =
        [
        //The player characters dialogue
        //Plays at the start of the game, and when player defeats the boss
        "Player:\n\n*mumbles*",
        "Player:\n\n*mumble mumble*",
        //Plays in response to the player acquiring Raph's waterbottle. 
        "Player:\n\nThe worst virus I've faced today,\n\nnot the zombies.\n\nFinally something to drink.",
        //Final line of dialogue in the game
        "Player:\n\nScrew you,\n\nI am going to go play\n\nMinecrap.",
        ];

    public string[] NarrationText =
        [
        //Narrator Text
        //Plays at the start of the game
        "Narrator:\n\nWelcome to 'The Wrath of Raph'.\n\nOur silly Resident Evil, Legend of Zelda, \n\nSignalis inspired game for assignment 4.",
        "Narrator:\n\nYou must find the keys scattered across\n\nthe ruins of mohawk campus,\n\nfight off hordes of zombies,\n\nand defeat the evil wizard\n\nRaph.",
        //Plays after defeating the Boss, after player character text.
        "Narrator:\n\nCongratulations!",
        "Narrator:\n\nYou're adopted!",
        "Narrator:\n\nAlso you've stopped the evil wizard I guess.",
        "Narrator:\n\nAnd acquired 'Raphs Famous Waterbottle'!",
        //Second last line of dialogue.
        "Narrator:\n\nTook you long enough.",

        ];

    public string[] SystemText =
        [
        //Error Text. Mostly memes.
        "You've encountered an Error",
        "You've avoided an Error",
        "There is an Error Amogus",
        "Bank Error in your favour, collect $200",

        ];

    public  string[] BossRaphText =
       [    
            //Boss Dialogue
            //Game Start Boss Dialogue. Plays after narrator dialogue.   0-2
            "Raph-am:\n\nAhhh, come to stop the great lord of \n\ndarkness?",
            "Raph-am:\n\nWhat a Fool you are!",
            "Raph-am:\n\nChoosing to not answer me.\n\nCowardly!\n\nNo matter, none can stop the great Raph-am!",
            //Dialogue after claiming the first key                     3-4
            "Raph-am:\n\nFoolish!\n\nYou dare try in vain to stop me?\n\nContinue further and you shall meet\n\n a swift end.",
            "Raph-am:\n\nIf not to my undead minions,\n\nthen perhaps to the next wave\n\nof undead minions!",
            //After claiming the second key                             5-6
            "Raph-am:\n\nAnother?!",
            "Raph-am:\n\nMinions! Tear this impudent insect limb from limb",
            //After Claiming the third key                              7
            "Raph-am:\n\nThis is your last chance gi.\n\nYour college administration has abandoned\n\nyou gi.\n\nIt is wise to leave a sinking ship gi.",
            //Dialogue when you enter Raph's Lair                       8-10
            "Raph-am:\n\nFoolish, FOOLISH! You should have fled when\n\nyou had the chance! \n\nNo mere mortal is capable of withstanding\n\n the Wrath of Raphael Ambrosius Costeau!",
            "Raph-am:\n\nThe great shapeshifting master of evil,\n\nthe duke of your demise,\n\nthe wrathful wizard\n\nRaph-am!",
            "Raph-am:\n\nFace your Doom!",
            //Dialogue for Phase 2                                      11-12
            "Raph-am:\n\nI have tried to accomodate your anti-social\n\n behaviour thus far, but it seems you\n\nmust join your fellow weaklings DIRECTLY!",
            "Raph-am:\n\nMINIONS of Raph-am, arrise!\n\nArise once more to serve my evil purpose!",
            //Dialogue for Phase 3                                      13-15
            "Raph-am:\n\nUrk... Damn you!\n\nMedlesome mortal...",
            "Raph-am:\n\nFor your insolance, you shall face",
            "Raph-am:\n\n\n\n                 the Wrath of Raph\n\n\n\n\n\n                                                      -am!",
        ];

}

