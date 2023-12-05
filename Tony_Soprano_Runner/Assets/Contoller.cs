using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Contoller : MonoBehaviour

{
    private float movement;
    public float speed = 1.5f;
    private bool isFacingRight = true;
    public float jumpVel = 20.0f;
    public float jumpingSpeed = 0.8f;
    public float jumpMaxSpeed = 5f;

    private bool isGrounded;
    private int currentJumps;
    public int maxJumps = 2;
    private Rigidbody2D player;
    private bool isJumping;
    private int collisionCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentJumps = 0;
        player = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movement = Input.GetAxisRaw("Horizontal");

        if (!isJumping)
        {
            player.velocity = new Vector2(movement * speed, player.velocity.y);
        }
        else if (isJumping && currentJumps == 2)
        {
            player.AddForce(new Vector2(movement * jumpingSpeed, 0f), ForceMode2D.Force);
        }

        if (movement < 0 && isFacingRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            isFacingRight = false;
        }
        else if (movement > 0 && !isFacingRight)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            isFacingRight = true;
        }

        if (player.velocity.x > jumpMaxSpeed)
        {
            player.velocity = new Vector2(jumpMaxSpeed, player.velocity.y);
        }
        else if (player.velocity.x < -jumpMaxSpeed)
        {
            player.velocity = new Vector2(-jumpMaxSpeed, player.velocity.y);

        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        collisionCounter++;
        if (other.gameObject.tag == "Jump")
        {
            foreach (ContactPoint2D hitPos in other.contacts)
            {
                // if(hitPos.normal.x!=0){} //zid
                if (hitPos.normal.y > 0)    //od gore
                {
                    currentJumps = 0;
                    isGrounded = true;
                    isJumping = false;
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        collisionCounter--;
    }

    private void Update()
    {
        if (collisionCounter == 0)
        {
            isGrounded = false;
        }
        if (!isGrounded && currentJumps < maxJumps - 1)
        {
            currentJumps = maxJumps - 1;
        }
        if (Input.GetButtonDown("Jump") && currentJumps < maxJumps)
        {
            currentJumps++;
            isJumping = true;
            player.velocity = new Vector2(player.velocity.x, jumpVel / currentJumps);
        }
    }
}
