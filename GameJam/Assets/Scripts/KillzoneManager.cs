using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillzoneManager : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D()
    {
        FindObjectOfType<OyxgenManager>().oxygen = -1;
    }
}
