using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
    public float speed;

    //public int time;

    public float timer = 70f;
    private RectTransform rt;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer);
        timer-=Time.deltaTime;
        rt.Translate(0,speed,0);
        if(timer < 0) SceneManager.LoadScene(0);
    }


}
