using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 激光类
/// </summary>
public class LaserShoot : MonoBehaviour
{
    
    public float ShootCD = 5;    //激光射击CD
    private float CD;

    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        CD = ShootCD;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._instance.isRuning)
        {
            CD -= Time.deltaTime;
            if (CD <= 0)
            {
                anim.SetTrigger("Shoot");
                CD = ShootCD;
            }
        }
    }

}
