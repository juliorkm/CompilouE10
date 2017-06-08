using UnityEngine;
using System.Collections;

public class TornadoFade : MonoBehaviour {

    private float tornadoDuration;
    private float maxOpacity;
    private float clock = 0;
    private SpriteRenderer thisColor;

	// Use this for initialization
	void Start () {
        thisColor = GetComponent<SpriteRenderer>();
        maxOpacity = thisColor.color.a;
        thisColor.color = new Color (1, 1, 1, 0);
        tornadoDuration = GameObject.Find("Skills").GetComponent<Skills>().duration1;
	}
	
	// Update is called once per frame
	void Update () {
	    if (clock >= 0 && clock <= tornadoDuration / 6)
            thisColor.color = new Color(1, 1, 1, thisColor.color.a + maxOpacity * Time.deltaTime * 6 / tornadoDuration);
        else if (clock >= tornadoDuration * 5 / 6)
            thisColor.color = new Color (1, 1, 1, thisColor.color.a - maxOpacity * Time.deltaTime * 6 / tornadoDuration);

        clock += Time.deltaTime;
	}
}
