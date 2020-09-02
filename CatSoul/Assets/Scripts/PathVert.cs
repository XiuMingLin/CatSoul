using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathVert : MonoBehaviour
{

    private Transform PlatformVert;

    public float moveSpeed = 0;

    public Transform start;
    public Transform end;
    
    // Start is called before the first frame update
    void Start()
    {
        PlatformVert = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        float movement = Mathf.PingPong(Time.time * moveSpeed, 1);
        PlatformVert.position = Vector3.Lerp(new Vector3(0, 1.5f, 0), new Vector3(0, -1.5f, 0), movement);
    }
}
