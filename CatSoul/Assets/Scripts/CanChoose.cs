using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanChoose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._instance.isRuning)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
        if (!GameManager._instance.isRuning)
        {
            this.GetComponent<SpriteRenderer>().enabled = true;
            GameManager._instance.canCreate = false;
            Debug.Log("canCreate");
        }
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
        if (!GameManager._instance.isRuning)
        {
            this.GetComponent<SpriteRenderer>().enabled = false;
            GameManager._instance.canCreate = true;
            Debug.Log("cantCreate");
        }
    }
}
