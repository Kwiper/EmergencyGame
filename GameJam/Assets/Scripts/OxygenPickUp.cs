using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D player){
        if(player.CompareTag("Player")){  
            FindObjectOfType<OyxgenManager>().oxygenRegenPickuo();
            Destroy(gameObject);
        }
    }


}
