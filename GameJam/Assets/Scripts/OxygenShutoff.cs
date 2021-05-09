using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenShutoff : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
	    if (other.gameObject.CompareTag("Player")) {
            Debug.Log("I happen");
            // turn off oxygen counting down to prevent player from resetting to checkpoint and subsequently going to ending if player is low enough to die.
            FindObjectOfType<OyxgenManager>().oxygenCountdownToggle = false;
	    }

    }
}
