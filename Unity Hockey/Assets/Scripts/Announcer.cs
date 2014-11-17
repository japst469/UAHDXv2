using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


/// <summary>
/// Handles playing the Announcer's sounds on command.
/// </summary>
public class Announcer : MonoBehaviour
{
    public __GameData GD;
    public GameObject Helper;        //Invisible scoring helper thinker gameobject    
    public int[] Score = new int[5]; //Array of scores (0th is a dummy, in order to align with Color enum) 

    void Start()
    {
        Helper = this.gameObject;
        GD = GameObject.Find("  gameData").GetComponent<__GameData>();
    }

    void Update()
    {
        int i;
        string[] scores = new string[5];    //Array to hold string versions of scores

        Score[0] = 0;
        scores[0] = string.Empty;
        for (i = 1; i <= 3; i++)
        {
            Score[i] = GD.Players[i-1].GetComponent<Player>().Score;
            scores[i] = Score[i].ToString();
        }

        //Convert all scores to string inside the array
        //Log the current scores
        Debug.Log("Score = " + scores[1] + " " + scores[2] + " " + scores[3] + " " + scores[4]);
    }

    public void AnnouncerPowerup(__GameData.AirHockeyPowerups ID)
    {
        string[] Sounds=
        {
            "Sounds/Announcer/Powerups/Hockey/vArmy",
            "Sounds/Announcer/Powerups/Pinball/vBall_Saver",
            "Sounds/Announcer/Powerups/Hockey/vBig Stick",
            "Sounds/Announcer/Powerups/Shared/vBlockade",
            "Sounds/Announcer/Powerups/Shared/vBumpers",
            "Sounds/Announcer/Powerups/Shared/vFastmo",
            "Sounds/Announcer/Powerups/Shared/vGravity",
            "Sounds/Announcer/Powerups/Shared/vHotPot",
            "Sounds/Announcer/Powerups/Shared/vInvis",
            "Sounds/Announcer/Powerups/Shared/vKill",
            "Sounds/Announcer/Powerups/Hockey/vMultipuck",
            "Sounds/Announcer/Powerups/Shared/vPortals",
            "Sounds/Announcer/Powerups/Shared/vRamps",
            "Sounds/Announcer/Powerups/Shared/vShield",
            "Sounds/Announcer/Powerups/Shared/vSlowmo",
            "Sounds/Announcer/Powerups/Shared/vSwap Place"
        };

        if (GD.Mode==__GameData.AirHockeyMode.Battle)
        {
           Sounds[10]="Sounds/Announcer/Powerups/Pinball/vMultiball";
        }

        Helper.audio.clip = (AudioClip)(Resources.Load(Sounds[(int)ID]));
        Helper.audio.Play();
    }

    //Routine to speak the score, who is tied, and gameover conditions
    public void AnnouncerScore()
    {
        string sentence = string.Empty; //String log of sentence to speak
        int i;        //Generic for loop counter
        int Hi;        //The ID of highest score player OR HiScore value; usage varies in code
        bool[] HiBitField =  //Bitfield determining who is tied. 0th field is True if no ties, otherwize false
        {
            false,false,false,false,false
        };

        string[] words = new string[19];    //Words in sentence to speak
        //Each slot in the words array corresponds
        //to a particular usage. If not used, is NOP'd

        float[] times = new float[20];      //Time in between each word

        //Initz everything to null string words and 0.0f times
        for (i = 0; i <= 18; i++)
        {
            words[i] = string.Empty;
            times[i] = 0.0f;
        }

        //AudioClip strings of #s
        string[] nums =                      
        {
            "Audio/Sounds/Announcer/Scoring/v0",
            "Audio/Sounds/Announcer/Scoring/v1",
            "Audio/Sounds/Announcer/Scoring/v2",
            "Audio/Sounds/Announcer/Scoring/v3",
            "Audio/Sounds/Announcer/Scoring/v4",
            "Audio/Sounds/Announcer/Scoring/v5",
            "Audio/Sounds/Announcer/Scoring/v6",
            "Audio/Sounds/Announcer/Scoring/v7",
            "Audio/Sounds/Announcer/Scoring/v8",
            "Audio/Sounds/Announcer/Scoring/v9",
            "Audio/Sounds/Announcer/Scoring/v10",
            "Audio/NULL"
        };
        //Coressponding times
        float[] numsTime =
        {
            1.5f,
            0.3f,
            0.5f,
            0.5f,
            0.6f,
            1.0f,
            0.4f,
            0.4f,
            0.3f,
            0.5f,
            0.3f,
            0.5f
        };

        //Team name AudioClip strings
        //Entries correspond to enum Colors
        string[] teams =
        {
            "Audio/NULL", //Dummy
            "Audio/Sounds/Announcer/Scoring/vRed",
            "Audio/Sounds/Announcer/Scoring/vGreen",
            "Audio/Sounds/Announcer/Scoring/vBlue",
            "Audio/Sounds/Announcer/Scoring/vYellow"
        };
        //Corresponding times
        float[] teamsTime =
        {
            0.1f,
            0.5f,
            0.5f,
            0.7f,
            0.4f
        };

        //AudioClip strings of win conditions/misc
        string[] other =
        {
            "Audio/NULL",
            "Audio/Sounds/Announcer/Scoring/vto",
            "Audio/Sounds/Announcer/Scoring/vTie",
            "Audio/Sounds/Announcer/Scoring/vWins",
            "Audio/Sounds/Announcer/Scoring/vGameover",
            "Audio/Sounds/Environment/Shared/Buzzer"
        };

        //Corresponding times
        float[] otherTime =
        {
            0.1f,
            0.2f,
            0.5f,
            0.5f,
            1.5f,
            1.5f
        };

        words[0] = nums[Score[1]];  //Red Score
        words[1] = other[1];        //"to"
        //Corresponding times
        times[0] = numsTime[Score[1]];
        times[1] = otherTime[1];

        //If NOT Battle mode, skip Green player
        if (GD.Mode == __GameData.AirHockeyMode.Battle)
        {
            words[2] = nums[Score[2]];  //Green Score
            words[3] = other[1];        //"to"
            times[2] = numsTime[Score[2]];
            times[3] = otherTime[1];
        }
        else
        {
            words[2] = nums[11];         //nop
            words[3] = other[0];        //nop
            times[2] = numsTime[11];
            times[3] = otherTime[0];
        }

        words[4] = nums[Score[3]];  //Blue Score
        times[4] = numsTime[Score[3]];

        //If NOT Battle mode, skip Yellow Player
        if (GD.Mode == __GameData.AirHockeyMode.Battle)
        {
            words[5] = other[1];        //"to"
            words[6] = nums[Score[4]];  //Yellow Score
            times[5] = otherTime[1];
            times[6] = numsTime[Score[4]];
        }
        else
        {
            words[5] = other[0];
            words[6] = nums[11];
            times[5] = otherTime[0];
            times[6] = numsTime[11];
        }

        Hi = FindHiScore(true);   //Find the high score # value
        FindTies(ref Score, ref HiBitField, Hi);    //Using that #, find the tie(s)

        //Process tie/tie win conditions
        for (i = 1; i <= 4; i++)
        {
            //If a tie...
            if (HiBitField[0] == false)
            {

                //Use bit field to announce tied teams
                if (HiBitField[i] == true)
                {
                    words[6 + i] = teams[i];    //Team color name of a tied team
                    times[6 + i] = teamsTime[i];
                }
                else
                {
                    words[6 + i] = teams[0];
                    times[6 + i] = teamsTime[0];
                }
            }

            else
            {
                words[6 + i] = teams[0];
                times[6 + i] = teamsTime[0];
            }
        }

        //If ties, then
        if (HiBitField[0] == false)
        {
            words[11] = other[2];       //"Tie"
            times[11] = otherTime[2];

            //Gameover TIE condition

            //See if the highscore is >=10 (Gameover condition)
            Hi = FindHiScore(true); //Find the highest score among the players
            if (Hi >= 10)
            {
                words[12] = other[4];       //"Gameover"
                times[12] = otherTime[4];

                words[13] = words[7];       //Say up 2 to 4 teams names that could be tied, by copying elements from tie processing
                words[14] = words[8];
                words[15] = words[9];
                words[16] = words[10];
                times[13] = times[7];
                times[14] = times[8];
                times[15] = times[9];
                times[16] = times[10];

                words[17] = other[3];   //"Wins"
                words[18] = other[5];   //Buzzer sfx
                times[17] = otherTime[3];
                times[18] = otherTime[5];
            }
            else
            {
                words[12] = other[0];
                times[12] = otherTime[0];

                words[13] = other[0];
                words[14] = other[0];
                words[15] = other[0];
                words[16] = other[0];
                times[13] = otherTime[0];
                times[14] = otherTime[0];
                times[15] = otherTime[0];
                times[16] = otherTime[0];

                words[17] = other[0];
                words[18] = other[0];
                times[17] = otherTime[0];
                times[18] = otherTime[0];
            }
        }
        else
        {
            words[11] = teams[FindHiScore(false)];  //Say currently winning team
            times[11] = teamsTime[FindHiScore(false)];

            Hi = FindHiScore(true);    //Get the highest score #
            if (Hi >= 10)
            {
                words[12] = other[4];       //"Gameover"
                words[13] = words[11];      //Some team name
                words[14] = other[0];
                words[15] = other[0];
                words[16] = other[0];
                words[17] = other[3];   //"Wins"
                words[18] = other[5];   //Buzzer sfx

                times[12] = otherTime[4];
                times[13] = times[11];
                times[14] = otherTime[0];
                times[15] = otherTime[0];
                times[16] = otherTime[0];
                times[17] = otherTime[3];
                times[18] = otherTime[5];
            }
        }

        //Say each word in the sentence
        for (i = 0; i <= 18; i++)
        {
            sentence += words[i] + " ";
            Helper.audio.clip = (AudioClip)(Resources.Load(words[i]));
            Helper.audio.PlayDelayed(times[i]);
        }

        Debug.Log(sentence);
    }

    //Routine to find the highest score value OR the index of the player with that highest score
    //Input: Boolean
    //  If true, get the highest score value; otherwise, get the index of HiScorer

    public int FindHiScore(bool action)
    {
        int Hi = 0;  //Byte of high score

        //Get index of highest player
        if (Score[1] >= Score[2])
        {
            if (Score[1] >= Score[3])
            {
                if (Score[1] >= Score[4])
                {
                    Hi = 1;
                }
            }
        }

        if (Score[2] >= Score[1])
        {
            if (Score[2] >= Score[3])
            {
                if (Score[2] >= Score[4])
                {
                    Hi = 2;
                }
            }
        }

        if (Score[3] >= Score[1])
        {
            if (Score[3] >= Score[2])
            {
                if (Score[3] >= Score[3])
                {
                    Hi = 3;
                }
            }
        }

        if (Score[4] >= Score[1])
        {
            if (Score[4] >= Score[2])
            {
                if (Score[4] >= Score[3])
                {
                    Hi = 4;
                }
            }
        }

        //If action==true, get the score value instead of index
        if (action == true)
        {
            Hi = Score[Hi];
        }

        return Hi;  //Return that value
    }

    //Routine to determine if there are any ties, and, if so, who is tying
    //Inputs/Outputs (by ref): Array of Scores, bitarray of who is tied, highest score value to compare for ties
    public void FindTies(ref int[] Scores, ref bool[] Bitfield, int High)
    {
        int i; //Generic FOR counter
        bool ZeroTies = true;   //Flag to determine if there are any ties. Is false if ties, otherwize, true

        //Find ties, using highest score
        for (i = 1; i <= 4; i++)
        {
            if (Scores[i] == High)
            {
                ZeroTies = false;
                Bitfield[i] = true;
            }
        }
        Bitfield[0] = ZeroTies;
    }
}

