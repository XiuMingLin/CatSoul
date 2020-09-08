using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerWelcome : MonoBehaviour
{

    public static PlayerWelcome _instance;
    
    public float moveSpeed = 0;
    public float jumpForce = 0;
    public float fallMultiplier = 2.5f;
    public float lowMultiplier = 2f;
    private float horizontalMove = 0;

    public Transform groundCheck;
    public LayerMask ground;

    public bool isGround;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        _animator.SetBool("isGround", isGround);
    }

    private void FixedUpdate()
    {
        GroundMovement();
        Jump();
        BestJump();
    }

    private void GroundMovement()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        _rigidbody2D.velocity = new Vector2(horizontalMove * moveSpeed, _rigidbody2D.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
        _animator.SetFloat("Move", Math.Abs(_rigidbody2D.velocity.x));
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGround)
        {
            isGround = false;
            _animator.SetTrigger("Jump");
            _animator.SetBool("isGround", false);
            // _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
            _rigidbody2D.velocity = Vector2.up * jumpForce;
        }
    }

    private void BestJump()
    {
        if (_rigidbody2D.velocity.y < 0)
        {
            _rigidbody2D.velocity += Vector2.up * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }else if (_rigidbody2D.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rigidbody2D.velocity += Vector2.up * (lowMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Exit"))
        {
            Debug.Log("quit");
            Application.Quit();
        }
    }
}
