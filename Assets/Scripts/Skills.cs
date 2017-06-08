using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Skills : MonoBehaviour
{

    public GameObject skill1;
    public Image fill1;
    public GameObject X1;
    public GameObject skill2;
    public Image fill2;
    public GameObject X2;
    public GameObject skill3;
    public Image fill3;
    public GameObject X3;
    public GameObject skill4;
    public Image fill4;
    public GameObject X4;
    public GameObject Player;
    public GameObject Hero;
    public GameObject spawnedBunker;
    public GameObject playerShot;

    public float cooldown1;
    public float cooldown2;
    public float cooldown3;
    public float minRage;
    public float rageRate;

    public float duration1;
    public float duration2;

    public float dashSpeed;
    public float dashRange;

    private Vector4 cooldowns;
    private int dash = 0;
    private float dashDestination;

    private float ObjectSpawnPosition;

    public bool skillsTravadas = false;


    // Use this for initialization
    void Start()
    {
        cooldowns = new Vector4(cooldown1, cooldown2, cooldown3, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.Find("PauseMenu").GetComponent<Pause>().isPaused)
        {
            if (skillsTravadas)
            {
                X1.SetActive(true);
                X2.SetActive(true);
                X3.SetActive(true);
                X4.SetActive(true);
            }

            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton2)) && skill1.activeSelf)
            {
                if (cooldowns.w > .25f && !skillsTravadas)
                {
                    Hero.GetComponent<Hero>().spellSize = cooldowns.w;

                    cooldowns.w = 0;
                    fill1.fillAmount = 0;
                    //skill1.GetComponent<Image>().color = new Color(1f, .58f, .58f, 1);
                    X1.SetActive(true);

                    Hero.GetComponent<Animator>().SetInteger("State", 1);
                    Hero.GetComponent<Hero>().spell = 1;
                    Hero.GetComponent<Hero>().spellSpawnLocation = new Vector3(Player.transform.position.x + 1f, Player.transform.position.y + 3f, 0);
                    StartCoroutine(castUltimate());
                }
                else
                {
                    Player.GetComponent<Player>().PlayAudio(1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.JoystickButton3))
            {
                if (cooldowns.x == cooldown1 && !skillsTravadas)
                {
                    cooldowns.x = 0;
                    fill2.fillAmount = 0;
                    //skill2.GetComponent<Image>().color = Color.cyan;
                    //X2.SetActive(true);

                    Hero.GetComponent<Animator>().SetInteger("State", 1);
                    StartCoroutine(castSpell1());

                    ObjectSpawnPosition = Player.transform.position.x;
                    Hero.GetComponent<Hero>().spell = 0;
                    Hero.GetComponent<Hero>().spellSpawnLocation = new Vector3(ObjectSpawnPosition, Player.transform.position.y + 3.5f, 0);
                }
                else
                {
                    Player.GetComponent<Player>().PlayAudio(1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.JoystickButton1))
            {
                if (cooldowns.y == cooldown2 && !skillsTravadas)
                {
                    cooldowns.y = 0;
                    fill3.fillAmount = 0;
                    //skill3.GetComponent<Image>().color = Color.cyan;
                    //X3.SetActive(true);

                    Player.GetComponent<Animator>().SetInteger("State", 1);
                    //Player.GetComponent<BoxCollider2D>().size = new Vector2(3f, .6f);
                    StartCoroutine(changeToBig());

                }
                else
                {
                    Player.GetComponent<Player>().PlayAudio(1);
                }
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                if (cooldowns.z == cooldown3 && !skillsTravadas)
                {
                    //Hero.GetComponent<Animator>().SetInteger("State", 2);

                    cooldowns.z = 0;
                    fill4.fillAmount = 0;
                    //skill4.GetComponent<Image>().color = Color.cyan;
                    //X4.SetActive(true);

                    Player.GetComponent<Player>().podeAndar = false;
                    if (Player.GetComponent<Rigidbody2D>().velocity.x < 0)
                        dash = -1;
                    else
                        dash = 1;

                    dashDestination = Player.transform.position.x + dash * dashRange;
                    if (dashDestination > 9.7f) dashDestination = 9.7f;
                    if (dashDestination < -9.7f) dashDestination = -9.7f;


                }
                else
                {
                    Player.GetComponent<Player>().PlayAudio(1);
                }
            }
        }
        

        if (skill1.activeSelf)
        {
            //cooldowns.w += Time.deltaTime;
            cooldowns.w = fill1.fillAmount;
            if (cooldowns.w >= 1)
                cooldowns.w = 1;
                //skill1.GetComponent<Image>().color = Color.white;
            if (cooldowns.w >= minRage && !skillsTravadas)
                X1.SetActive(false);
            else
                X1.SetActive(true);

            fill1.fillAmount -= rageRate * Time.deltaTime;
        }
        if (cooldowns.x < cooldown1)
        {
            cooldowns.x += Time.deltaTime;
            fill2.fillAmount += Time.deltaTime / cooldown1;
        }

        if (cooldowns.x >= cooldown1 && !skillsTravadas)
        {
            cooldowns.x = cooldown1;
            //skill2.GetComponent<Image>().color = Color.white;
            X2.SetActive(false);
        }
        else
            X2.SetActive(true);

        if (cooldowns.y < cooldown2)
        {
            cooldowns.y += Time.deltaTime;
            fill3.fillAmount += Time.deltaTime / cooldown2;
        }

        if (cooldowns.y >= cooldown2 && !skillsTravadas)
        {
            cooldowns.y = cooldown2;
            //skill3.GetComponent<Image>().color = Color.white;
            X3.SetActive(false);
        }
        else
            X3.SetActive(true);

        if (cooldowns.z < cooldown3)
        {
            cooldowns.z += Time.deltaTime;
            fill4.fillAmount += Time.deltaTime / cooldown3;
        }

        if (cooldowns.z >= cooldown3 && !skillsTravadas)
        {
            cooldowns.z = cooldown3;
            //skill4.GetComponent<Image>().color = Color.white;
            X4.SetActive(false);
        }
        else
            X4.SetActive(true);


        if (cooldowns.x < duration1 + 0.02 && cooldowns.x > duration1 - 0.02)
        {
            GameObject aux = GameObject.Find("SummonedBunker(Clone)");
            if (aux != null) Destroy(aux);
        }

        if (cooldowns.y < duration2 + 0.02 && cooldowns.y > duration2 - 0.02)
        {
            Player.GetComponent<Animator>().SetInteger("State", 4);
            StartCoroutine(changeToSmall());
        }

        if (dash != 0)
        {
            useDash();
            if ((Player.transform.position.x >= dashDestination - 0.01f && dash > 0) || (Player.transform.position.x <= dashDestination + 0.01f && dash < 0))
            {
                dash = 0;
                Player.GetComponent<Player>().podeAndar = true;
                Hero.GetComponent<Hero>().isDashing = false;
                //Hero.GetComponent<Animator>().SetInteger("State", 0);
            }
        }

    }

    private void useDash()
    {
        float eixox;
        Hero.GetComponent<Hero>().isDashing = true;

        if (!GameObject.Find("PauseMenu").GetComponent<Pause>().isPaused)
        {
            eixox = Mathf.Lerp(Player.transform.position.x, dashDestination, dashSpeed / 20);
            if (eixox >= dashDestination - 0.1f && dash > 0)
            {
                eixox = dashDestination;
            }
            if (eixox <= dashDestination + 0.1f && dash < 0)
            {
                eixox = dashDestination;
            }
            Player.transform.position = new Vector3(eixox, Player.transform.position.y, 0);
            Hero.transform.position = new Vector3(eixox + .33f, Player.transform.position.y - .78f, 0);
        }
            
    }

    private IEnumerator changeToBig()
    {
        yield return new WaitForSeconds(Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Player.GetComponent<Animator>().SetInteger("State", 3);

    }
    private IEnumerator changeToSmall()
    {
        yield return new WaitForSeconds(Player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Player.GetComponent<Animator>().SetInteger("State", 0);
        //Player.GetComponent<BoxCollider2D>().size = new Vector2(1.8f, .6f);

    }
    private IEnumerator castSpell1()
    {
        skillsTravadas = true;
        yield return new WaitForSeconds(Hero.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Hero.GetComponent<Animator>().SetInteger("State", 0);
        skillsTravadas = false;


    }
    private IEnumerator castUltimate()
    {
        skillsTravadas = true;
        yield return new WaitForSeconds(Hero.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        Hero.GetComponent<Animator>().SetInteger("State", 0);
        skillsTravadas = false;


    }
}
