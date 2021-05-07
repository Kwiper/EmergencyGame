using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class OffBlock : MonoBehaviour
{
    private bool recState; //recorded state, added to reduce load of script
    private bool changed = false;
    private Tilemap tilemap;
    private Collider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        collider = GetComponent<CompositeCollider2D>();
        collider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (recState != FindObjectOfType<GameManager>().state)
        {
            changed = false;
            recState = FindObjectOfType<GameManager>().state;
        }
        if (!changed)
        {
            collider.isTrigger = recState;
            if (!recState)
            {
                tilemap.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
                gameObject.layer = LayerMask.NameToLayer("Ground");
            }
            else
            {
                tilemap.color = new Color(1.0f, 0.0f, 0.0f, 0.2f);
                gameObject.layer = LayerMask.NameToLayer("Inactive");
            }
            changed = true;
        }
    }
}
