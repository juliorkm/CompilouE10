using UnityEngine;
using System.Collections;

public class CloudLoop : MonoBehaviour {

    public RectTransform Cloud0;
    public RectTransform Cloud1;
    public RectTransform Cloud2;

    public float speed = .1f;


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Cloud0.transform.position = new Vector3(Cloud0.transform.position.x + speed * Time.deltaTime, 0, 0);
        Cloud1.transform.position = new Vector3(Cloud1.transform.position.x + speed * Time.deltaTime, 0, 0);
        Cloud2.transform.position = new Vector3(Cloud2.transform.position.x + speed * Time.deltaTime, 0, 0);

        if (Cloud0.localPosition.x > 1200)
        {
            Cloud0.transform.position = new Vector3(-Cloud0.transform.position.x, 0, 0);
        }
        if (Cloud1.localPosition.x > 1200)
        {
            Cloud1.transform.position = new Vector3(-Cloud1.transform.position.x, 0, 0);
        }
        if (Cloud2.localPosition.x > 1200)
        {
            Cloud2.transform.position = new Vector3(-Cloud2.transform.position.x, 0, 0);
        }

    }
}
