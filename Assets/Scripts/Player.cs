using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 30.0f;
    public float jumpVelocity = 50f;
    float horizontalSpeed;


    [SerializeField] LayerMask layermask;
    Rigidbody2D rigidbody2d;
    BoxCollider2D boxCollider2D;

    bool facingRight = true;
    int allowedDash;
    public int maxDash = 1;
    float dashDistance = 3f;
    bool dashPressed = false;
    bool jumpPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
        rigidbody2d = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        allowedDash = maxDash;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            dashPressed = true;
        }

        if(Input.GetButtonDown("Jump")) {
            jumpPressed = true;
        }

        horizontalSpeed = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        //Vector2 position = rigidbody2d.position;
        //position.x += speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        
        if(isGrounded() && jumpPressed)
        {
            jumpPressed = false;
            jump(1);
        }
        //rigidbody2d.MovePosition(position);
        handleMovement();

    }

    public void jump(int multiplicator)
    {
        rigidbody2d.velocity = Vector2.up * jumpVelocity * multiplicator;
    }

    public void handleMovement()
    {
        if (dashPressed && allowedDash !=0) {
            dashPressed = false;
            allowedDash--;
            rigidbody2d.velocity = Vector2.zero;
            rigidbody2d.MovePosition(new Vector2(rigidbody2d.position.x + (facingRight ? dashDistance : -dashDistance), rigidbody2d.position.y));

        } else if (horizontalSpeed < 0)
        {
            rigidbody2d.velocity = new Vector2(-walkSpeed, rigidbody2d.velocity.y);
            facingRight = false;
        } else if (horizontalSpeed > 0)
        {
            facingRight = true;
            rigidbody2d.velocity = new Vector2(walkSpeed, rigidbody2d.velocity.y);
        } else
        {
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
        }
    }

    public void iceMovement()
    {
        if (Input.GetAxis("Horizontal") < 0)
        {
            rigidbody2d.velocity = new Vector2(-walkSpeed, rigidbody2d.velocity.y);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            rigidbody2d.velocity = new Vector2(walkSpeed, rigidbody2d.velocity.y);
        }
        else
        {
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
        }
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, layermask);
        if(raycastHit2D.collider != null)
        {
            allowedDash = maxDash;
            return true;
        }
        return false;
    }

}
