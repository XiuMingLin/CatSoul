using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clones : MonoBehaviour
{

    public static bool isHave = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!isHave)
        {
            isHave = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
