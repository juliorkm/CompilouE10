using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    public GameObject mapa;

    public GameObject mainCamera;
    private Vector3 position;

    public float speed;

    public AudioClip[] audioClips;

    public bool podeAndar = true;

    private Rigidbody2D rb;
    private AudioSource AS;

    void movimentoHorizontal()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (transform.position.x <= -9.7f)
        {
            transform.position = new Vector2(-9.7f, transform.position.y);
        }
        else if (transform.position.x >= 9.7f)
        {
            transform.position = new Vector2(9.7f, transform.position.y);
        }

        //if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        //    moveHorizontal = -1;

        //if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        //    moveHorizontal = 1;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        this.rb.velocity = movement * speed;
    }

    void movimentoVertical() //Usado para o movimento diferente de câmera
    {
        position = new Vector3(this.transform.position.x, mainCamera.transform.position.y - 8.5f, this.transform.position.x);
        transform.position = position;
    }

    void moveMapa()
    {
        mapa.GetComponent<Slider>().value = mainCamera.transform.position.y / 4 - 3;
    }

    public void PlayAudio (int clip)
    {
        AS.PlayOneShot(audioClips[clip]);
    }

    public void PlayAudio (int clip, float vol)
    {
        AS.PlayOneShot(audioClips[clip], vol);
    }

    // Use this for initialization
    void Start () {

       rb = GetComponent<Rigidbody2D>();
       AS = GetComponent<AudioSource>();

	}

    // Update is called once per frame
    void Update()
    {
        if (podeAndar)
        {
            movimentoHorizontal();
            moveMapa();
        }
            
    }
}

