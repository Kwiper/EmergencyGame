using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class EndCutsceneTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other) {
	    if (other.gameObject.CompareTag("Player")) {
            Debug.Log("I happen");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

	    }

    }
}
