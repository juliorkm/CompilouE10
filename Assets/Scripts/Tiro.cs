using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tiro : MonoBehaviour
{

    private Vector3 position;

    public float speed;
    public float damage;
    public int movementType;

    public int pointUp;
    public int pointDown;

    public ProgressionBar progressionBar;
    public GameObject player;
    public Muralha zimbunker;
    public ScoreCounter score;
    public GameObject rageMeter;
    public float rageUp;


    public float senoideSpeed = 10.0f;
    public float senoidSize = 3.0f;

    public Vector3 target;

    private Vector3 axis;

    private Vector3 pos;

    // Use this for initialization
    void Start()
    {

        progressionBar = GameObject.Find("ProgressionBar").GetComponent<ProgressionBar>();
        player = GameObject.Find("Player");
        zimbunker = GameObject.Find("Zimbunker").GetComponent<Muralha>();
        score = GameObject.Find("Score").GetComponent<ScoreCounter>();
        rageMeter = GameObject.Find("Skill1");

        axis = transform.right;
        pos = transform.position;

        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        moveTiro();
        checkOutofScreen();

    }

    void moveTiro()
    {
        if (movementType == 0)
        {
            position -= transform.up * Time.deltaTime * speed;
        }
        else if (movementType == 1)
        {
            pos -= transform.up * Time.deltaTime * speed;
            position = pos + axis * Mathf.Sin(Time.time * senoideSpeed) * senoidSize;
        }
        else if (movementType == 2)
        {
            position = Vector3.MoveTowards(position, target, speed * Time.deltaTime);
        }
        
        transform.position = position;
    }

    void checkOutofScreen()
    {
        if (speed < 0)
        {
            if (this.gameObject.transform.position.y >= player.transform.position.y + 28)
            {
                //if (score.playerScore >= 2)
                //    score.playerScore -= 2;
                //progressionBar.moveSlider("down");
                Destroy(gameObject);
            }
        }
        else
        {
            if (this.gameObject.transform.position.y <= player.transform.position.y - 7)
            {
                if (score.playerScore >= pointDown)
                    score.playerScore -= pointDown;
                if (movementType == 2)
                {
                    GameObject.Find("Timer").GetComponent<Timer>().blinkRed(damage);
                }
                else
                    if (progressionBar != null && !progressionBar.inTransition)
                        progressionBar.moveSlider("down");
                Destroy(gameObject);
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetComponent<Animator>().GetInteger("State") == 0)
        {
            if (collision.gameObject.CompareTag("Bunker"))
            {
                if (score.playerScore >= pointDown)
                    score.playerScore -= pointDown;
                if ((zimbunker.health >= 75 && zimbunker.health - damage < 75) ||
                    (zimbunker.health >= 50 && zimbunker.health - damage < 50) ||
                    (zimbunker.health >= 25 && zimbunker.health - damage < 25) ||
                    (zimbunker.health >= 10 && zimbunker.health - damage < 10) ||
                    (zimbunker.health > 0 && zimbunker.health - damage <= 0))
                        player.GetComponent<Player>().PlayAudio(3, 2);
                zimbunker.health -= damage;
                if (!progressionBar.inTransition)
                    progressionBar.moveSlider("down");
                player.GetComponent<Player>().PlayAudio(2, .1f);
                StartCoroutine(destroyTiro());
            }

            else if (collision.gameObject.CompareTag("Player"))
            {
                score.playerScore += pointUp;
                if (progressionBar != null && !progressionBar.inTransition)
                    progressionBar.moveSlider("up");
                if (rageMeter != null && rageMeter.activeSelf) GameObject.Find("Fill1").GetComponent<Image>().fillAmount += rageUp;
                player.GetComponent<Player>().PlayAudio(2, .1f);
                StartCoroutine(destroyTiro());
            }

            else if (collision.gameObject.CompareTag("Boss") && movementType != 2)
            {
                //score.playerScore += (int) damage;
                //GameObject.Find("Fill1").GetComponent<Image>().fillAmount += damage / 20;
                collision.gameObject.GetComponent<Boss>().health -= damage;
                player.GetComponent<Player>().PlayAudio(2, .1f);
                StartCoroutine(destroyTiro());
            }
        }
        
    }

    public IEnumerator destroyTiro()
    {
        if (GetComponent<Animator>().GetInteger("State") == 0)
        {
            if (gameObject != null)
            {
                GetComponent<Animator>().SetInteger("State", 1);
                speed = 0;
                senoidSize = 0;
                pos = transform.position;
                GetComponent<BoxCollider2D>().enabled = false;
                yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);

                if (gameObject != null)  Destroy(gameObject);
            }
        }
    }
}
