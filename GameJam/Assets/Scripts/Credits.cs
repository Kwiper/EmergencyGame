using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
    public float speed;

    public int time;
    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(time);
        rt.Translate(0,speed,0);
        time--;
        if(time == 0) SceneManager.LoadScene(0);
    }


}
