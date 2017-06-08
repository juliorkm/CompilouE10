using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {

    public Transform shield;
    public bool podeAndar;
    public bool isDashing = false;

    public int maxPlayerDamage;

    public int spell;
    public Vector3 spellSpawnLocation;
    public float spellSize;

    public GameObject spawnedBunker;
    public GameObject playerShot;
    private AudioSource AS;


    void Start()
    {
        AS = GetComponent<AudioSource>();
        StartCoroutine(fadeInMusic(1f, 2.5f));
        InvokeRepeating("SpawnTrail", 0, 0.07f); // replace 0.2f with needed repeatRate
    }

    void SpawnTrail()
    {
        GameObject trailPart = new GameObject();
        SpriteRenderer trailPartRenderer = trailPart.AddComponent<SpriteRenderer>();

        if (isDashing)
        {
            trailPartRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
            trailPart.transform.position = transform.position;
            trailPart.transform.localScale = transform.localScale;
        }
        

        if (trailPart != null)
            Destroy(trailPart, 0.4f); // replace 0.5f with needed lifeTime

        StartCoroutine("FadeTrailPart", trailPartRenderer);
    }

    IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        Color color = trailPartRenderer.color;
        color.r -= 0.5f;
        color.g -= 0.5f;
        color.a -= 0.5f; // replace 0.5f with needed alpha decrement
        trailPartRenderer.color = color;

        yield return new WaitForEndOfFrame();
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

    void returnToIdle()
    {
        GetComponent<Animator>().SetInteger("State", 0);
        if (spell == 0)
        {
            GameObject bunkerTransform = Instantiate(spawnedBunker, spellSpawnLocation, Quaternion.identity) as GameObject;
            Physics2D.IgnoreCollision(bunkerTransform.GetComponent<BoxCollider2D>(), shield.gameObject.GetComponent<BoxCollider2D>());
        }

        if (spell == 1)
        {
            playerShot.GetComponent<Tiro>().damage = maxPlayerDamage * spellSize;
            playerShot.transform.localScale = new Vector3(10 * spellSize, 10 * spellSize, 1);
            Instantiate(playerShot, new Vector3(transform.position.x + 1f, transform.position.y + 3f, 0), Quaternion.identity);
            //GameObject.Find("Skills").GetComponent<Skills>().skillsTravadas = false;

        }
    }

    // Update is called once per frame
    void Update () {
        transform.position = new Vector3 (shield.position.x + .33f, shield.position.y - .78f, 0f);
	}
}
