using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float GBScale = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "GB")
        {
            this.GetComponent<Rigidbody2D>().gravityScale = GBScale;
        }
        else if(collision.tag == "GF")
        {
            this.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(this.GetComponent<Rigidbody2D>().velocity, Vector2.zero, 0.8f);
            this.GetComponent<Rigidbody2D>().gravityScale = -this.GetComponent<Rigidbody2D>().gravityScale;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "GB")
        {
            this.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        }
    }

}
