using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
    
    public float timer;
    float minutos;
    float segundos;
    float milisegundos;

    public float comeback;
    
    public Text text;
    public GameObject timesUp;
    public bool freeze = false;

    private Color originalColor;
    private bool isBlinking;
    private float clock = 0;
    private int blinkToWhat;

    // Use this for initialization
    void Start () {
        originalColor = text.color;

        if (PlayerPrefs.HasKey("Timer"))
        {
            timer += PlayerPrefs.GetFloat("Timer");
            PlayerPrefs.DeleteKey("Timer");
        }

        minutos = Mathf.Floor(timer / 60);
        segundos = timer - minutos * 60;
        if (segundos >= 60)
        {
            segundos -= 60;
            minutos++;
        }
            

        text.text = minutos.ToString("F0") + ":" + Mathf.Floor(segundos).ToString("00");
    }
	
	// Update is called once per frame
	void Update () {
        if (clock >= 0)
        {
            if (blinkToWhat == 1)
            {
                if (clock >= .5f || (clock <= .3f && clock > .1f))
                    text.color = Color.green;
                else
                    text.color = originalColor;

                clock -= Time.deltaTime;
            }

            else if (blinkToWhat == 2)
            {
                if (clock >= .5f || (clock <= .3f && clock > .1f))
                    text.color = Color.red;
                else
                    text.color = originalColor;

                clock -= Time.deltaTime;
            }
            
        }

        if (!freeze)
        {
            if (timer <= 0)
            {
                timer = 0;
                //text.text = "0:00:00";

                if (!timesUp.activeSelf)
                    timesUp.SetActive(true);

                //if (Input.GetKey(KeyCode.Return))
                //    SceneManager.LoadScene(0);
            }
            else if (timer > 0)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                    timer = 0;

                minutos = Mathf.Floor(timer / 60);
                segundos = Mathf.Floor(timer - minutos * 60);
                milisegundos = (timer - minutos * 60 - segundos) * 100;

                if (segundos >= 60)
                {
                    segundos -= 60;
                    minutos++;
                }

                text.text = minutos.ToString("0") + ":" + segundos.ToString("00") + ":" + Mathf.Floor(milisegundos).ToString("00");
            }
        }
        
    }

    public void blinkGreen()
    {
        timer += comeback;
        clock = .7f;
        blinkToWhat = 1;
    }

    public void blinkRed(float damage)
    {
        timer -= damage;

        if (timer <= 0)
            timer = 0;

        minutos = Mathf.Floor(timer / 60);
        segundos = Mathf.Floor(timer - minutos * 60);
        milisegundos = (timer - minutos * 60 - segundos) * 100;

        if (segundos >= 60)
        {
            segundos -= 60;
            minutos++;
        }

        text.text = minutos.ToString("0") + ":" + segundos.ToString("00") + ":" + Mathf.Floor(milisegundos).ToString("00");

        clock = .7f;
        blinkToWhat = 2;
    }
    
}
