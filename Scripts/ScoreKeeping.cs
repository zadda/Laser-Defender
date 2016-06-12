﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreKeeping : MonoBehaviour
{
    public static int score = 0;
    private Text myText;

    void Start()
    {
       myText = GetComponent<Text>();
    }

    public void Score(int points)
    {

        score += points;
        myText.text = score.ToString();
    }

    public static void Reset()
    {   
        score = 0;
        //myText.text = score.ToString();
    }
}
