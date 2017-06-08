using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlagHealth : MonoBehaviour {

    public GameObject Zimbunker;

    public Sprite Flag0;
    public Sprite Flag1;
    public Sprite Flag2;
    public Sprite Flag3;
    public Sprite Flag4;

    public Sprite Bunker0;
    public Sprite Bunker1;
    public Sprite Bunker2;
    public Sprite Bunker3;
    public Sprite Bunker4;

    private Muralha muralha;

	// Use this for initialization
	void Start () {
        muralha = Zimbunker.GetComponent<Muralha>();

	}
	
	// Update is called once per frame
	void Update () {
	
        muralha = Zimbunker.GetComponent<Muralha>();

        if (muralha.health > 75)
        {
            GetComponent<Image>().sprite = Flag0;
            muralha.GetComponent<SpriteRenderer>().sprite = Bunker0;
            
        }
        else if (muralha.health > 50)
        {
            GetComponent<Image>().sprite = Flag1;
            muralha.GetComponent<SpriteRenderer>().sprite = Bunker1;
        }
        else if (muralha.health > 25)
        {
            GetComponent<Image>().sprite = Flag2;
            muralha.GetComponent<SpriteRenderer>().sprite = Bunker2;
        }
        else if (muralha.health > 10)
        {
            GetComponent<Image>().sprite = Flag3;
            muralha.GetComponent<SpriteRenderer>().sprite = Bunker3;
        }
        else
        {
            GetComponent<Image>().sprite = Flag4;
            muralha.GetComponent<SpriteRenderer>().sprite = Bunker4;
        }


	}
}
