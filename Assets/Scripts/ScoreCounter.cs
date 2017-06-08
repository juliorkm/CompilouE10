using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

    public int playerScore;
    public Text text;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("Score"))
            playerScore = PlayerPrefs.GetInt("Score");
        else
            playerScore = 0;

        PlayerPrefs.DeleteKey("Score");
	}
	
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
            playerScore++;
        */

        text.text = "SCORE: " + playerScore.ToString("0000000");

	}
}
