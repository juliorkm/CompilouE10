using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimesUpText : MonoBehaviour {

    public Font number;
    public Color numberColor;
    public Font greek;
    public Color greekColor;



    public void defeat()
    {
        Text texto = gameObject.GetComponent<Text>();
        texto.text = "DEFEAT!";
        texto.font = greek;
        texto.fontStyle = FontStyle.Normal;
        texto.color = greekColor;

    }
    public void stageClear()
    {
        Text texto = gameObject.GetComponent<Text>();
        texto.text = "CLEAR!!";
        texto.font = greek;
        texto.fontStyle = FontStyle.Normal;
        texto.color = greekColor;

    }
}
