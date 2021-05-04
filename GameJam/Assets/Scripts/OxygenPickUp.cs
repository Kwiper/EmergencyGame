using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(){
            FindObjectOfType<OyxgenManager>().oxygenRegenPickuo();
            Destroy(gameObject);
            
    }


}
