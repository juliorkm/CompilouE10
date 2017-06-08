using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour {

    public int position = 0;

    public AudioClip[] audioClips;

    public Sprite titleOFF;
    public Sprite titleON;

    public Sprite exitOFF;
    public Sprite exitON;

    public GameObject title;
    public GameObject exit;


    private SpriteRenderer titleSprite;
    private SpriteRenderer exitSprite;

    public Text score;
    public Text highScore;

    private int currentScore;
    private int currentHighscore;

    private bool inputStuff = true;

    private AudioSource AS;

    IEnumerator Title()
    {
        StartCoroutine(fadeMusic(.8f));
        float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(0);
    }

    IEnumerator Exit()
    {
        StartCoroutine(fadeMusic(.8f));
        float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.Quit();
    }

    // Use this for initialization
    void Start()
    {
        PlayerPrefs.DeleteKey("Muralha");
        currentScore = PlayerPrefs.GetInt("Score") + (int)PlayerPrefs.GetFloat("Timer") * 100;
        PlayerPrefs.DeleteKey("Timer");

        //title = GameObject.FindGameObjectWithTag("Title");
        titleSprite = title.GetComponent<SpriteRenderer>();

        //exit = GameObject.FindGameObjectWithTag("Exit");
        exitSprite = exit.GetComponent<SpriteRenderer>();

        currentHighscore = PlayerPrefs.GetInt("Highscore");

        score.text = currentScore.ToString("0000000");
        if (PlayerPrefs.HasKey("Highscore"))
        {
            if (currentScore > currentHighscore)
            {
                PlayerPrefs.SetInt("Highscore", currentScore);
                PlayerPrefs.DeleteKey("Score");
                PlayerPrefs.Save();
            }
            else
                PlayerPrefs.DeleteKey("Score");
        }
        else
            PlayerPrefs.SetInt("Highscore", currentScore);
        highScore.text = PlayerPrefs.GetInt("Highscore").ToString("0000000");

        AS = GetComponent<AudioSource>();

        StartCoroutine(fadeInMusic(1f, 2f));
    }

    public IEnumerator fadeMusic(float time)
    {
        while (AS.volume > 0.0f)
        {
            AS.volume -= Time.deltaTime / time;
            yield return null;
        }
        if (AS.volume == 0.0f)
            yield break;

    }

    public IEnumerator fadeInMusic(float volume, float time)
    {
        while (AS.volume < volume)
        {
            AS.volume += Time.deltaTime / time;
            yield return null;
        }
        if (AS.volume >= volume)
        {
            AS.volume = volume;
            yield break;
        }
    }

    public void PlayAudio(int clip)
    {
        AS.PlayOneShot(audioClips[clip]);
    }

    public void PlayAudio(int clip, float vol)
    {
        AS.PlayOneShot(audioClips[clip], vol);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") == -1)
            if (inputStuff)
            {
                PlayAudio(0);
                position = (position + 1) % 2;
                inputStuff = false;
            }

        if (Input.GetAxisRaw("Vertical") == 1)
            if (inputStuff)
            {
                PlayAudio(0);
                if (position == 0)
                    position = 1;
                else
                    position = (position - 1) % 2;
                inputStuff = false;
            }

        if (Input.GetAxisRaw("Vertical") == 0)
            inputStuff = true;

        if (position == 0)
        {
            titleSprite.sprite = titleON;
            titleSprite.transform.localScale = new Vector3(0.7f, 0.7f, 1);
            exitSprite.sprite = exitOFF;
            exitSprite.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        }
        else
        {
            titleSprite.sprite = titleOFF;
            titleSprite.transform.localScale = new Vector3(0.6f, 0.6f, 1);
            exitSprite.sprite = exitON;
            exitSprite.transform.localScale = new Vector3(0.7f, 0.7f, 1);
        }
        

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            PlayAudio(1);
            if (position == 0)
                StartCoroutine(Title());
            else if (position == 1)
            {
                StartCoroutine(Exit());
            }

        }
    }
}
