using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinCondition : MonoBehaviour {

    public GameObject Player;
    public Slider ProgressionBarSlider;
    public ProgressionBar Progression;
    public Slider Mapa;
    public GameObject Score;
    public Boss Boss;
    public GameObject TimesUp;

    private bool timerSoundPlayed = false;

    private bool advance;


    IEnumerator ToVictory()
    {
        if (ProgressionBarSlider.value >= ProgressionBarSlider.maxValue && Mapa.value >= Mapa.maxValue - 0.01)
        {
            GameObject.Find("Skills").GetComponent<Skills>().skillsTravadas = true;
            GameObject player = GameObject.Find("Player");

            if (!PlayerPrefs.HasKey("Wins"))
            {
                TimesUp.SetActive(true);
                TimesUp.GetComponent<TimesUpText>().stageClear();
            }
            

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
                if (!PlayerPrefs.HasKey("Wins"))
                    player.GetComponent<Player>().PlayAudio(5);
                else { }    //outro som?
                timerSoundPlayed = true;
            }

            GameObject.Find("Timer").GetComponent<Timer>().freeze = true;
            Player.GetComponent<Player>().podeAndar = false;
            Player.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);

            PlayerPrefs.SetInt("Score", Score.GetComponent<ScoreCounter>().playerScore);
            PlayerPrefs.SetFloat("Timer", GameObject.Find("Timer").GetComponent<Timer>().timer);
            PlayerPrefs.SetFloat("Muralha", GameObject.Find("Zimbunker").GetComponent<Muralha>().health);
            //TODO Sound Effect
            yield return new WaitForSeconds(1f); //Sound Effect duration
            if (PlayerPrefs.HasKey("Wins"))
            {
                float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
                yield return new WaitForSeconds(fadeTime);
                SceneManager.LoadScene(4);

            }
            else
            {
                advance = true;
                float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
                yield return new WaitForSeconds(fadeTime);
                PlayerPrefs.SetInt("Wins", 1);
                PlayerPrefs.Save();
                SceneManager.LoadScene(3);
            }
        }

        if (Boss != null && Boss.health <= 0)
        {
            GameObject.Find("Skills").GetComponent<Skills>().skillsTravadas = true;
            GameObject player = GameObject.Find("Player");
           
            TimesUp.SetActive(true);
            TimesUp.GetComponent<TimesUpText>().stageClear();


            if (!timerSoundPlayed)
            {
                GameObject[] tiros = GameObject.FindGameObjectsWithTag("Tiro");
                for (int i = 0; i < tiros.Length; i++)
                {
                    tiros[i].GetComponent<Tiro>().speed = 0;
                    tiros[i].GetComponent<Tiro>().movementType = 0;
                }
                StartCoroutine(GameObject.Find("HeroCharacter").GetComponent<Hero>().fadeMusic(.8f));
                player.GetComponent<Player>().PlayAudio(5);
                timerSoundPlayed = true;
            }

            GameObject.Find("Timer").GetComponent<Timer>().freeze = true;
            Player.GetComponent<Player>().podeAndar = false;
            Player.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0f, 0f);

            PlayerPrefs.SetInt("Score", Score.GetComponent<ScoreCounter>().playerScore);
            PlayerPrefs.SetFloat("Timer", GameObject.Find("Timer").GetComponent<Timer>().timer);
            PlayerPrefs.SetFloat("Muralha", GameObject.Find("Zimbunker").GetComponent<Muralha>().health);
            //TODO Sound Effect
            yield return new WaitForSeconds(1f); //Sound Effect duration
            float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
            yield return new WaitForSeconds(fadeTime);
            PlayerPrefs.SetInt("Wins", PlayerPrefs.GetInt("Wins") + 1);
            SceneManager.LoadScene(3);
        }

    }

    void consertaSlider()
    {
        //float eixoy = Mathf.Lerp(Player.transform.position.y, Player.transform.position.y + 4 * Progression.dist, 0.1f);
        //Player.transform.position = new Vector3(Player.transform.position.x, eixoy - 8.5f, 0);

        Player.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 20f, 0f);

        if (ProgressionBarSlider.value > 0)
        {
            if (ProgressionBarSlider.value < 0.3)
                ProgressionBarSlider.value = 0;
            ProgressionBarSlider.value -= 0.3f;
        }

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(ToVictory());

        if (advance)
        {
            Player.GetComponent<Player>().podeAndar = false;
            consertaSlider();
        }
    }
}
