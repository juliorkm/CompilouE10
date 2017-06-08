using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {

    public bool isPaused;
    public GameObject pauseImage;
	
    public void PauseGame()
    {
        isPaused = !isPaused;
            if (isPaused)
            {
                Time.timeScale = 0;
                pauseImage.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                pauseImage.SetActive(false);
            }
    }

	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7)) {
            PauseGame();
        }
	}
}
