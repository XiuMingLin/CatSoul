using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public static Player _instance;

    public float GBScale = 0;
    public float moveSpeed = 0;
    public float jumpForce = 0;
    public float fallMultiplier = 2.5f;
    public float lowMultiplier = 2f;
    private float horizontalMove = 0;
    public float springForce = 0;

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
        if (GameManager._instance.isRuning)
        {
            GroundMovement();
            Jump();
            BestJump();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("GB"))
        {
            this.GetComponent<Rigidbody2D>().gravityScale = GBScale;
            Debug.Log("Enter GB");
        }
        if(collision.CompareTag("GF"))
        {
            Debug.Log("Enter GF");
            this.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(this.GetComponent<Rigidbody2D>().velocity, Vector2.zero, 0.8f);
            this.GetComponent<Rigidbody2D>().gravityScale = -this.GetComponent<Rigidbody2D>().gravityScale;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("GB"))
        {
            Debug.Log("Exit GB");
            this.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Spring"))
        {
            isGround = false;
            other.gameObject.GetComponent<Animator>().SetTrigger("Spring");
            this._animator.SetTrigger("Jump");
            this._rigidbody2D.AddForce(transform.up * springForce);
        }
        if(other.collider.CompareTag("Boom"))
        {
            other.transform.GetComponent<Animator>().SetBool("Boom", true);
        }

        if (other.collider.CompareTag("Goal"))
        {
            GameManager._instance.isWin = true;
            other.transform.parent.GetChild(2).GetComponent<ParticleSystem>().Play();
            other.transform.parent.GetChild(1).GetComponent<ParticleSystem>().Pause();
            GameManager._instance.isRuning = false;
            _rigidbody2D.velocity = Vector2.zero;
            Invoke("nextScene", 3.0f);
        }

        if (other.collider.CompareTag("Trap"))
        {
            // Time.timeScale = 0;
            Debug.Log("GameOver!!!");
            ResetPlayer();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Col"))
        {
            ResetPlayer();
        }
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

    private void nextScene()
    {
        GameManager.curLevel++;
        Debug.Log("GameWin");
        if (GameManager.curLevel >= 4)
        {
            UIManager._instance.SendMessage("GameWin");
            Time.timeScale = 0;
        }
        else
        {
            GameManager._instance.isRuning = false;
            GameManager._instance.isWin = false;
            GameManager._instance.canCreate = true;
            GameManager._instance.oneTimesItems.Add(Resources.Load(GameManager._instance.playerPath, typeof(GameObject)) as GameObject);
            GameManager._instance.oneTimesItems.Add(Resources.Load(GameManager._instance.goalPath, typeof(GameObject)) as GameObject);
            SceneManager.LoadScene("Main");
        }
    }

    private void ResetPlayer()
    {
        this.gameObject.transform.position = GameManager._instance.playerPos;
    }
}
