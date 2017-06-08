using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProgressionBar : MonoBehaviour {

    public GameObject mainCamera;
    public GameObject player;
    public GameObject hero;
    
    private Vector3 proxPosicaoCamera;

    public int dist;

    public float BarUp = .8f;
    public float BarDown = 1f;

    public GameObject mapa;

    private float posMapa;

    public bool inTransition = false;

    Slider slider;

    private bool comeback = false;

    //versao para teste
    /*
    public void moveSliderTeste()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            slider.value += .8f;
        if (Input.GetKeyDown(KeyCode.X))
            slider.value -= .8f;

    }
    */


    //versao acessada pelo script Tiro
    public void moveSlider(string sentido)
    {
        if (sentido == "up")
        {
            slider.value += BarUp;
        }
        else //sentido == "down"
        {
            slider.value -= BarDown;
        }
    }

    void processaSlider()
    {
        Slider mapSlider = mapa.GetComponent<Slider>();

        if (slider.value >= slider.maxValue && mapSlider.value < mapSlider.maxValue - 0.01) {
            //posicaoCamera = mainCamera.GetComponent<Transform>().position.y;
            proxPosicaoCamera = new Vector3(0, mainCamera.transform.position.y + 4 * dist, -10);
            //proxPosicaoCamera = new Vector3(0, (mapa.GetComponent<Slider>().value + 1) * 4 + 12, -10);

            if (comeback)
            {
                GameObject.Find("Timer").GetComponent<Timer>().blinkGreen();
                comeback = false;
            }

            //player.GetComponent<Player>().PlayAudio(2, .4f);
            DestroyTiros();

            inTransition = true;
        }
        else if (slider.value <= slider.minValue && mapSlider.value > mapSlider.minValue + 0.01)
        {
        
            //posicaoCamera = mainCamera.GetComponent<Transform>().position.y;
            proxPosicaoCamera = new Vector3 (0, mainCamera.transform.position.y - 4 * dist, -10);
            //proxPosicaoCamera = new Vector3(0, (mapa.GetComponent<Slider>().value - 1) * 4 + 12, -10);

            //StartCoroutine(DestroyTiros());

            //comeback = true;
            GameObject.Find("TiroSpawner").GetComponent<TiroSpawner>().mapaDesceu = true;

            inTransition = true;
        }

    }

    void consertaSlider()
    {
        float eixoy;
        if (!GameObject.Find("PauseMenu").GetComponent<Pause>().isPaused)
        {
            eixoy = Mathf.Lerp(mainCamera.transform.position.y, proxPosicaoCamera.y, 0.05f);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, eixoy, -10);
            player.transform.position = new Vector3(player.transform.position.x, eixoy - 8.5f, 0);
            hero.transform.position = new Vector3(hero.transform.position.x, eixoy - 9.28f, 0);
        }
            

        if (inTransition)
        {
            

            if (slider.value > 0)
            {
                if (slider.value < 0.3)
                    slider.value = 0;
                slider.value -= 0.3f;
            }
                

            else if (slider.value < 0)
            {
                if (slider.value > -0.3)
                    slider.value = 0;
                slider.value += 0.3f;
            }
                
            else
                inTransition = false;

        }
    }

    void DestroyTiros()
    {
        GameObject[] tiros = GameObject.FindGameObjectsWithTag("Tiro");
        for (int i = 0; i < tiros.Length; i++)
        {
            if (tiros[i].transform.position.y <= player.transform.position.y + 45 && tiros[i].GetComponent<Animator>().GetInteger("State") == 0)
            {
                if (tiros[i] != null) StartCoroutine(tiros[i].GetComponent<Tiro>().destroyTiro());
            }
            
        }
        if (tiros.Length > 0)
            player.GetComponent<Player>().PlayAudio(2, .1f);
    }

    // Use this for initialization
    void Start () {
        slider = this.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {

        posMapa = mapa.GetComponent<Slider>().value;

        if (!inTransition)
            processaSlider();
        consertaSlider();
        //if (!inTransition)
            //moveSliderTeste();
	}
}
