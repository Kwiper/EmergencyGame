using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextFade : MonoBehaviour
{
    public TextMeshProUGUI text;
    private float alpha = 0;
    private float timer = 20f;

    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(255, 255, 255, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0 && alpha < 255) {
            alpha += 5 * Time.deltaTime;
        }

        text.color = new Color(255, 255, 255, alpha);
        


    }
}
