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
        
    }

    private void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
        this.GetComponent<SpriteRenderer>().enabled = true;
    }

    private void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
        this.GetComponent<SpriteRenderer>().enabled = false;
    }
}
