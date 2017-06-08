using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TiroSpawner : MonoBehaviour {


    public float positionY = 70;
    public float waveLenght;
    public float initialFreq;
    public float maxFreq;
    public float freqUp;
    public float freqDown;
    public GameObject obj;
    public GameObject obj2;
    public Transform player;
    public bool mapaDesceu = false;

    private Vector3 ObjectSpawnPosition;
    private float period;
    private float nextActionTime;
    private float clock = 0;
    private int rand = -1;       //Regiao do Mapa
    public int rand2 = -1;      //Wave Dobrada

    // Use this for initialization
    void Start () {

        rand = Random.Range(0, 3);
        rand2 = 5; //Random.Range(1, 11);
    }
	
	// Update is called once per frame
	void Update () {


        if (mapaDesceu)
        {
            initialFreq += freqDown;
            mapaDesceu = false;
        }

        if (clock > waveLenght)
        {
            clock = 0;
            rand = Random.Range(0, 3);
            rand2 = Random.Range(1, 11);
            initialFreq -= freqUp;

            if (initialFreq <= maxFreq)
            {
                initialFreq = maxFreq;
            }
        }

        createTiro();

        clock += Time.deltaTime;

    }



    void createTiro()
    {
        period = initialFreq;
        waveSimples();

        /*
        if (Input.GetKeyDown(KeyCode.C))
        {
            float x = Random.Range(minX, maxX);
            ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
            Instantiate(obj, ObjectSpawnPosition, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            float x = Random.Range(minX, maxX);
            ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
            Instantiate(obj2, ObjectSpawnPosition, Quaternion.identity);
        }
        */
    }

    void waveSimples()
    {
        float x;
        if (Time.time > nextActionTime)
        {
            float rand3;

            nextActionTime = Time.time + period;
            if (rand == 0 || rand == 3)
            {
                rand3 = Random.Range(0, 10);
                if (rand3 >= 1)
                {
                    x = Random.Range(-9.7f, 0f);
                    ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                    Instantiate(obj, ObjectSpawnPosition, Quaternion.identity);
                }
                else
                {
                    x = Random.Range(-9.7f + obj2.GetComponent<Tiro>().senoidSize, 0f);
                    ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                    Instantiate(obj2, ObjectSpawnPosition, Quaternion.identity);
                }

                if (rand2 == 1 || rand2 == 11)
                {
                    rand3 = Random.Range(0, 10);
                    if (rand3 >= 1)
                    {
                        x = Random.Range(-9.7f, 0f);
                        ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                        Instantiate(obj, ObjectSpawnPosition, Quaternion.identity);
                    }
                    else
                    {
                        x = Random.Range(-9.7f + obj2.GetComponent<Tiro>().senoidSize, 0f);
                        ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                        Instantiate(obj2, ObjectSpawnPosition, Quaternion.identity);
                    }
                }
            }

            if (rand == 1 || rand == 4)
            {
                rand3 = Random.Range(0, 10);
                if (rand3 >= 1)
                {
                    x = Random.Range(-5f, 5f);
                    ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                    Instantiate(obj, ObjectSpawnPosition, Quaternion.identity);
                }
                else
                {
                    x = Random.Range(-5f, 5f);
                    ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                    Instantiate(obj2, ObjectSpawnPosition, Quaternion.identity);
                }

                if (rand2 == 1 || rand2 == 11)
                {
                    rand3 = Random.Range(0, 10);
                    if (rand3 >= 1)
                    {
                        x = Random.Range(-5f, 5f);
                        ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                        Instantiate(obj, ObjectSpawnPosition, Quaternion.identity);
                    }
                    else
                    {
                        x = Random.Range(-5f, 5f);
                        ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                        Instantiate(obj2, ObjectSpawnPosition, Quaternion.identity);
                    }
                }
            }

            if (rand == 2 || rand == 5)
            {
                rand3 = Random.Range(0, 10);
                if (rand3 >= 1)
                {
                    x = Random.Range(0f, 9.7f);
                    ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                    Instantiate(obj, ObjectSpawnPosition, Quaternion.identity);
                }
                else
                {
                    x = Random.Range(0f, 9.7f - obj2.GetComponent<Tiro>().senoidSize);
                    ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                    Instantiate(obj2, ObjectSpawnPosition, Quaternion.identity);
                }
                if (rand2 == 1 || rand2 == 11)
                {
                    rand3 = Random.Range(0, 10);
                    if (rand3 >= 1)
                    {
                        x = Random.Range(0f, 9.7f);
                        ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                        Instantiate(obj, ObjectSpawnPosition, Quaternion.identity);
                    }
                    else
                    {
                        x = Random.Range(0f, 9.7f - obj2.GetComponent<Tiro>().senoidSize);
                        ObjectSpawnPosition = new Vector3(x, player.position.y + positionY, 0);
                        Instantiate(obj2, ObjectSpawnPosition, Quaternion.identity);
                    }
                }
            }
        }
    }
}
