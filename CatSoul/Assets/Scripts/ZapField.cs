using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapField : MonoBehaviour
{

    private Animator _animator;
    private BoxCollider2D _boxCollider2D;

    public float recoverCD = 2.0f;
    private float curTime = 2.0f;
    public bool isRun = false;     //用于计时器
    // Start is called before the first frame update
    void Start()
    {
        _animator = this.GetComponent<Animator>();
        _boxCollider2D = this.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            curTime -= Time.deltaTime;
            if (curTime <= 0)
            {
                _animator.SetBool("isStay", false);
                isRun = false;
                curTime = recoverCD;
            }
        }
    }

    public void StartRun()
    {
        isRun = true;
        _animator.SetBool("isStay", true);
        _boxCollider2D.enabled = true;
    }
    
    public void EndRun()
    {
        // isRun = true;
        // _animator.SetBool("isStay", true);
        _boxCollider2D.enabled = false;
    }
}
