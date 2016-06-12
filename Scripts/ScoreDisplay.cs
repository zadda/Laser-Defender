using UnityEngine;
using System.Collections;
using UnityEngine.UI; // toegang tot text

public class ScoreDisplay : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        Text myText = GetComponent<Text>();
        myText.text = ScoreKeeping.score.ToString();
        ScoreKeeping.Reset();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
