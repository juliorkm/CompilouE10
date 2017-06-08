using UnityEngine;
using System.Collections;

public class Muralha : MonoBehaviour {

    public float health;

	// Use this for initialization
	void Start () {
        if (PlayerPrefs.HasKey("Muralha"))
        {
            health = PlayerPrefs.GetFloat("Muralha");
            PlayerPrefs.DeleteKey("Muralha");
        }
	}
	
	// Update is called once per frame
	void Update () {
	   
        /*if (Input.GetKeyDown(KeyCode.B))
        {
            health -= 8;
        }*/
	}
}
