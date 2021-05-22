using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public static UIScript script;
    public TMPro.TMP_Text scoreText;

    public TMPro.TMP_Text tempText;
    void Awake()
    {
        script = this;
    }

    // Update is called once per frame
    public void ChangeScore(int value)
    {
        scoreText.text = value.ToString();
    }
    public void Lose()
    {
        tempText.text = "Tap to Restart";
    }
}
