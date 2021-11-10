using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 30.0f;
    public float jumpVelocity = 100000f;
    Rigidbody2D rigidbody2d;
    float horizontal;

    // Start is called before the first frame update
    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 10;
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x += speed * Input.GetAxis("Horizontal") * Time.deltaTime;
        
        if(Input.GetButton("Jump"))
        {
            jump(1);
        }
        rigidbody2d.MovePosition(position);
    }

    public void jump(int multiplicator)
    {
        Debug.Log("Jumped");
        rigidbody2d.velocity = new Vector2(0,10000f);
    }

}
