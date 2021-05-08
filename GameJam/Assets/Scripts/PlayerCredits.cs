using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCredits : MonoBehaviour
{

    private RectTransform rt;

    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        rt.Translate(30f * Time.deltaTime, 10f * Time.deltaTime, 0);
        rt.Rotate(0, 0, -2f * Time.deltaTime);
    }
}
