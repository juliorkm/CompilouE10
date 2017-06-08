using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScreen : MonoBehaviour {

    public float fadeTime;


    public Sprite newgameOFF;
    public Sprite newgameON;

    public Sprite instructionsOFF;
    public Sprite instructionsON;

    public Sprite exitOFF;
    public Sprite exitON;

    public GameObject newgame;
    public GameObject instructions;
    public GameObject exit;
    public GameObject titulo;

    public SpriteRenderer[] instructionImgs;

    public int position = 0;
    public int positionX = 0;

    public AudioClip[] audioClips;

    private SpriteRenderer newgameSprite;
    private SpriteRenderer instructionsSprite;
    private SpriteRenderer exitSprite;
    private Text tituloText;

    private bool inputStuff = true;
    private bool inInstructions = false;
    private bool inTransition;
    private int instruDirection;

    private float clock = 0;

    private AudioSource AS;

    IEnumerator NewGame()
    {
        StartCoroutine(fadeMusic(.8f));
        float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(1);
    }

    IEnumerator bossbattle()
    {
        StartCoroutine(fadeMusic(.8f));
        float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(4);
    }

    IEnumerator Exit()
    {
        StartCoroutine(fadeMusic(.8f));
        float fadeTime = GetComponent<FadeInOut>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        Application.Quit();
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

    // Use this for initialization
    void Start ()
    {

        //newgame = GameObject.FindGameObjectWithTag("NewGame");
        newgameSprite = newgame.GetComponent<SpriteRenderer>();

        //instructions = GameObject.FindGameObjectWithTag("Instructions");
        instructionsSprite = instructions.GetComponent<SpriteRenderer>();

        //exit = GameObject.FindGameObjectWithTag("Exit");
        exitSprite = exit.GetComponent<SpriteRenderer>();

        tituloText = titulo.GetComponent<Text>();
        AS = GetComponent<AudioSource>();

        StartCoroutine(fadeInMusic(1f, 2f));


    }
	
	// Update is called once per frame
	void Update () {

        if (clock > 0)
        {
            if (clock <= 0.05f) clock = 0;
            if (instruDirection == 0)
            {
                if (!inTransition)
                    instructionImgs[positionX].color = new Color(1, 1, 1, clock / fadeTime);
                else
                {
                    newgameSprite.color = new Color(1, 1, 1, 1 - clock / fadeTime);
                    instructionsSprite.color = new Color(1, 1, 1, 1 - clock / fadeTime);
                    exitSprite.color = new Color(1, 1, 1, 1 - clock / fadeTime);
                    tituloText.color = new Color(tituloText.color.r, tituloText.color.g, tituloText.color.b, 1 - clock / fadeTime);
                }
            }

            if (positionX == -1)
            {
                if (!inTransition)
                    instructionImgs[positionX + 1].color = new Color(1, 1, 1, clock / fadeTime);
                else
                {
                    newgameSprite.color = new Color(1, 1, 1, 1 - clock / fadeTime);
                    instructionsSprite.color = new Color(1, 1, 1, 1 - clock / fadeTime);
                    exitSprite.color = new Color(1, 1, 1, 1 - clock / fadeTime);
                    tituloText.color = new Color(tituloText.color.r, tituloText.color.g, tituloText.color.b, 1 - clock / fadeTime);
                }
            }


            if (positionX == 0)
                if (instruDirection == 1)
                {
                    if (!inTransition)
                    {
                        newgameSprite.color = new Color(1, 1, 1, clock / fadeTime);
                        instructionsSprite.color = new Color(1, 1, 1, clock / fadeTime);
                        exitSprite.color = new Color(1, 1, 1, clock / fadeTime);
                        tituloText.color = new Color(tituloText.color.r, tituloText.color.g, tituloText.color.b, clock / fadeTime);
                    }
                    else
                        instructionImgs[positionX].color = new Color(1, 1, 1, 1 - clock / fadeTime);
                }
                else if (instruDirection == -1)
                {
                    if (!inTransition)
                        instructionImgs[positionX + 1].color = new Color(1, 1, 1, clock / fadeTime);
                    else
                        instructionImgs[positionX].color = new Color(1, 1, 1, 1 - clock / fadeTime);

                }

            if (positionX >= 1)
                if (instruDirection == 1)
                {
                    if (!inTransition)
                        instructionImgs[positionX - 1].color = new Color(1, 1, 1, clock / fadeTime);
                    else
                        instructionImgs[positionX].color = new Color(1, 1, 1, 1 - clock / fadeTime);
                }
                else if (instruDirection == -1)
                {
                    if (!inTransition)
                        instructionImgs[positionX + 1].color = new Color(1, 1, 1, clock / fadeTime);
                    else
                        instructionImgs[positionX].color = new Color(1, 1, 1, 1 - clock / fadeTime);

                }


            clock -= Time.deltaTime;
            if (clock <= 0 && !inTransition)
            {
                inTransition = true;
                clock = fadeTime;
            }

            if (clock <= 0 && inTransition && (positionX == -1 || instruDirection == 0)) inInstructions = false;
            if (clock <= 0 && inTransition) inTransition = false;
        }

        else
        {
            if (!inInstructions)
            {
                /*
                if ((Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.B)) || (Input.GetKey(KeyCode.RightShift) && Input.GetKeyDown(KeyCode.B)))
                {
                    StartCoroutine(bossbattle());
                } //BOSS DEBUG!!
                */

                if (Input.GetAxisRaw("Vertical") == -1)
                    if (inputStuff)
                    {
                        PlayAudio(0);
                        position = (position + 1) % 3;
                        inputStuff = false;
                    }

                if (Input.GetAxisRaw("Vertical") == 1)
                    if (inputStuff)
                    {
                        PlayAudio(0);
                        if (position == 0)
                            position = 2;
                        else
                            position = (position - 1) % 3;
                        inputStuff = false;
                    }

                if (Input.GetAxisRaw("Vertical") == 0)
                    inputStuff = true;

                if (position == 0)
                {
                    newgameSprite.sprite = newgameON;
                    newgameSprite.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                    instructionsSprite.sprite = instructionsOFF;
                    instructionsSprite.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    exitSprite.sprite = exitOFF;
                    exitSprite.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                }
                else if (position == 1)
                {
                    newgameSprite.sprite = newgameOFF;
                    newgameSprite.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    instructionsSprite.sprite = instructionsON;
                    instructionsSprite.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                    exitSprite.sprite = exitOFF;
                    exitSprite.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                }
                else
                {
                    newgameSprite.sprite = newgameOFF;
                    newgameSprite.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    instructionsSprite.sprite = instructionsOFF;
                    instructionsSprite.transform.localScale = new Vector3(0.6f, 0.6f, 1);
                    exitSprite.sprite = exitON;
                    exitSprite.transform.localScale = new Vector3(0.7f, 0.7f, 1);
                }

                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.JoystickButton7))
                {
                    PlayAudio(1);
                    if (position == 0)
                        StartCoroutine(NewGame());
                    else if (position == 1)
                    {
                        inInstructions = true;
                        //newgameSprite.color = new Color(1, 1, 1, 0);
                        //instructionsSprite.color = new Color(1, 1, 1, 0);
                        //exitSprite.color = new Color(1, 1, 1, 0);
                        //tituloText.color = new Color(tituloText.color.r, tituloText.color.g, tituloText.color.b, 0);
                        clock = fadeTime;
                        positionX = 0;
                        instruDirection = 1;
                        //TODO Instructions Scene
                    }
                    else if (position == 2)
                        StartCoroutine(Exit());

                }

                if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && Input.GetKey(KeyCode.Delete))
                {
                    PlayAudio(2);
                    PlayerPrefs.DeleteAll();
                }
            }

            else    //Instructions Scene
            {
                if (Input.GetAxisRaw("Horizontal") == 1)
                    if (inputStuff && positionX < instructionImgs.Length - 1)
                    {
                        PlayAudio(0);
                        positionX = (positionX + 1);
                        clock = fadeTime;
                        instruDirection = 1;
                        inputStuff = false;
                    }

                if (Input.GetAxisRaw("Horizontal") == -1)
                    if (inputStuff)
                    {
                        PlayAudio(0);
                        positionX = (positionX - 1);
                        clock = fadeTime;
                        instruDirection = -1;
                        inputStuff = false;
                    }

                if (Input.GetAxisRaw("Horizontal") == 0)
                    inputStuff = true;

                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton6) || Input.GetKeyDown(KeyCode.JoystickButton1))
                {
                    PlayAudio(2);
                    instruDirection = 0;
                    clock = fadeTime;
                    inputStuff = false;
                }



            }
        }

       

        
    }
}
