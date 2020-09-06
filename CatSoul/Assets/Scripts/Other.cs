using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Other : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isOn()
    {
        this.gameObject.SetActive(true);
    }

    public void isOff()
    {
        this.gameObject.SetActive(false);
    }
}
