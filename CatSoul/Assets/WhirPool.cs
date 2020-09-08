using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhirPool : MonoBehaviour
{

    public Transform target;

    public float force = 0;
    
    public enum GameMode
    {
        SingleGame,
        NetGame,
        Editor
    }

    public GameMode gameMode;

    public float timeToStart = 0;
    private float curentTime = 0;

    public bool isIn = false;
    
    // Start is called before the first frame update
    void Start()
    {
        curentTime = timeToStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (isIn)
        {
            curentTime -= Time.deltaTime;
            if (curentTime <= 0)
            {
                if (gameMode == GameMode.SingleGame)
                {
                    SceneManager.LoadScene("Main");
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isIn = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("xiru");
            Vector2 dir = target.position - other.transform.position;
            other.GetComponent<Rigidbody2D>().AddForce(dir * force);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            isIn = false;
            curentTime = timeToStart;
        }
    }
}
