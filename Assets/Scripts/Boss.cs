using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public Timer timer;

    public float health;
    public Image healthBar;

    public float positionY = 70;
    public float waveLenght;
    public float initialFreq;
    public float maxFreq;
    public float freqUp;
    public float standingDuration;
    public Vector3[] positions;
    public GameObject obj;
    public Transform player;

    private Vector3 ObjectSpawnPosition;
    private float period;
    private float nextActionTime;
    private float clock = 0;    //waves
    private float clock2;    //standing
    public int posicaoAnterior = 1;
    public int posicaoAtual;

    public Vector2 range1;
    public Vector2 range2;
    public Vector2 range3;

    public int rand2;


    private Animator anim;
    private float maxHealth;

    // Use this for initialization
    void Start () {
        maxHealth = health;
        anim = GetComponent<Animator>();

        clock2 = standingDuration;

        posicaoAtual = 1;
    }
	
	// Update is called once per frame
	void Update () {
        //if (Input.GetKeyDown(KeyCode.Z))
        //    GameObject.Find("Fill1").GetComponent<Image>().fillAmount = 1;      //debug!!

        healthBar.fillAmount = health / maxHealth;
        anim.SetFloat("Health", health);

        if (health > 0 && timer.timer > 0)
        {
            if (clock2 > 0)
            {
                anim.SetInteger("State", 0);
                clock2 -= Time.deltaTime;
            }
            else if (clock2 >= -.1f && clock2 < 0 && anim.GetInteger("State") == 0)
            {
                transform.position = positions[posicaoAtual];
                anim.SetInteger("State", 1);
            }
            else
            {
                if (clock > waveLenght)
                {
                    clock = 0;
                    posicaoAnterior = posicaoAtual;
                    posicaoAtual = Random.Range(0, 3);

                    if (posicaoAtual != posicaoAnterior)
                    {
                        anim.SetInteger("State", 0);
                        clock2 = standingDuration;
                    }
                    else
                    {
                    }

                    initialFreq -= freqUp;

                    if (initialFreq <= maxFreq)
                    {
                        initialFreq = maxFreq;
                    }
                }

                if (anim.GetInteger("State") == 2 && health > 0)
                {
                    createTiro();
                    clock += Time.deltaTime;
                }
                else if (anim.GetInteger("State") == 0 && health > 0)
                {
                    clock2 -= Time.deltaTime;
                }
            }
        }

        

        



    }

    void createTiro()
    {
        period = initialFreq;
        waveSimples();
    }

    void waveSimples()
    {
        float x;
        GameObject tiro;
        if (Time.time > nextActionTime)
        {
            nextActionTime = Time.time + period;
            if (posicaoAtual == 0 || posicaoAtual == 3)
            {
                x = Random.Range(range1.x, range1.y);
                ObjectSpawnPosition = new Vector3(transform.position.x, transform.position.y + positionY, 0);
                tiro = Instantiate(obj, ObjectSpawnPosition, Quaternion.identity) as GameObject;
                tiro.GetComponent<Tiro>().target = new Vector3(x, player.position.y - 7, 0);

                if (rand2 == 1 || rand2 == 11)
                {
                    x = Random.Range(-10f, 0f);
                    ObjectSpawnPosition = new Vector3(transform.position.x, transform.position.y + positionY, 0);
                    tiro = Instantiate(obj, ObjectSpawnPosition, Quaternion.identity) as GameObject;
                    tiro.GetComponent<Tiro>().target = new Vector3(x, player.position.y - 7, 0);
                }
            }

            if (posicaoAtual == 1 || posicaoAtual == 4)
            {
                x = Random.Range(range2.x, range2.y);
                ObjectSpawnPosition = new Vector3(transform.position.x, transform.position.y + positionY, 0);
                tiro = Instantiate(obj, ObjectSpawnPosition, Quaternion.identity) as GameObject;
                tiro.GetComponent<Tiro>().target = new Vector3(x, player.position.y - 7, 0);

                if (rand2 == 1 || rand2 == 11)
                {
                    x = Random.Range(-6f, 6f);
                    ObjectSpawnPosition = new Vector3(transform.position.x, transform.position.y + positionY, 0);
                    tiro = Instantiate(obj, ObjectSpawnPosition, Quaternion.identity) as GameObject;
                    tiro.GetComponent<Tiro>().target = new Vector3(x, player.position.y - 7, 0);
                }
            }

            if (posicaoAtual == 2 || posicaoAtual == 5)
            {
                x = Random.Range(range3.x, range3.y);
                ObjectSpawnPosition = new Vector3(transform.position.x, transform.position.y + positionY, 0);
                tiro = Instantiate(obj, ObjectSpawnPosition, Quaternion.identity) as GameObject;
                tiro.GetComponent<Tiro>().target = new Vector3(x, player.position.y - 7, 0);

                if (rand2 == 1 || rand2 == 11)
                {
                    x = Random.Range(0f, 10f);
                    ObjectSpawnPosition = new Vector3(transform.position.x, transform.position.y + positionY, 0);
                    tiro = Instantiate(obj, ObjectSpawnPosition, Quaternion.identity) as GameObject;
                    tiro.GetComponent<Tiro>().target = new Vector3(x, player.position.y - 7, 0);
                }
            }
        }
}

    void changeToAttack()
    {
        GetComponent<Animator>().SetInteger("State", 2);
    }

}
