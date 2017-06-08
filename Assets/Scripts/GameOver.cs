using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public GameObject Zimbunker;
    public GameObject Timer;
    public GameObject Score;
    public GameObject TimesUp;

    private Muralha bunkerStats;
    private Timer timerStats;

    private bool timerSoundPlayed = false;


    IEnumerator ToMenu()
    {
        float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(0);
    }

    IEnumerator ToGameOver()
    {
        if (timerStats.timer == 0)
        {
            GameObject.Find("Skills").GetComponent<Skills>().skillsTravadas = true;
            PlayerPrefs.SetInt("Score", Score.GetComponent<ScoreCounter>().playerScore);

            GameObject player = GameObject.Find("Player");

            //TODO Sound Effect
            if (!timerSoundPlayed)
            {
                GameObject spawner = GameObject.Find("TiroSpawner");
                if (spawner != null) spawner.SetActive(false);

                GameObject[] tiros = GameObject.FindGameObjectsWithTag("Tiro");
                for (int i = 0; i < tiros.Length; i++)
                {
                    tiros[i].GetComponent<Tiro>().speed = 0;
                    tiros[i].GetComponent<Tiro>().movementType = 0;
                }
                StartCoroutine(GameObject.Find("HeroCharacter").GetComponent<Hero>().fadeMusic(.8f));
                player.GetComponent<Player>().PlayAudio(0);
                timerSoundPlayed = true;
            }

            player.GetComponent<Rigidbody2D>().velocity = new Vector2 (0, 0);
            player.GetComponent<Player>().podeAndar = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
            GameObject.Find("PauseMenu").GetComponent<Pause>().isPaused = true;

            yield return new WaitForSeconds(player.GetComponent<Player>().audioClips[0].length * 2);
            float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene(2);
        }
        else if (bunkerStats.health <= 0)
        {
            GameObject.Find("Skills").GetComponent<Skills>().skillsTravadas = true;
            PlayerPrefs.SetInt("Score", Score.GetComponent<ScoreCounter>().playerScore);

            GameObject player = GameObject.Find("Player");

            TimesUp.SetActive(true);
            TimesUp.GetComponent<TimesUpText>().defeat(); 

            if (!timerSoundPlayed)
            {
                GameObject.Find("TiroSpawner").SetActive(false);
                GameObject[] tiros = GameObject.FindGameObjectsWithTag("Tiro");
                for (int i = 0; i < tiros.Length; i++)
                {
                    tiros[i].GetComponent<Tiro>().speed = 0;
                    tiros[i].GetComponent<Tiro>().movementType = 0;
                }
                StartCoroutine(GameObject.Find("HeroCharacter").GetComponent<Hero>().fadeMusic(.8f));
                player.GetComponent<Player>().PlayAudio(4);
                timerSoundPlayed = true;
            }

            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            GameObject.Find("Timer").GetComponent<Timer>().freeze = true;
            player.GetComponent<Player>().podeAndar = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);
            GameObject.Find("PauseMenu").GetComponent<Pause>().isPaused = true;

            yield return new WaitForSeconds(player.GetComponent<Player>().audioClips[0].length * 2);

            timerStats.freeze = true;
            //TODO Animation
            yield return new WaitForSeconds(0f); //Sound Effect duration
            float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            SceneManager.LoadScene(2);
        }
        
    }

    // Use this for initialization
    void Start () {
        bunkerStats = Zimbunker.GetComponent<Muralha>();
        timerStats = Timer.GetComponent<Timer>();

	}
	
	// Update is called once per frame
	void Update () {

        StartCoroutine(ToGameOver());


        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Escape))
            StartCoroutine(ToMenu());
    }
}
