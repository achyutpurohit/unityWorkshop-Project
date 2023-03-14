using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    public float playerSpeed = 2;
    public float jumpForce = 2;
    public float raycastLength = 2;
    public bool isGrounded;
    public LayerMask groundLayerMask;
    public Transform respawnpoint;
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        respawnpoint.position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2( horizontal * playerSpeed, rb.velocity.y);
        
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, y: jumpForce);
            
        }
        

        if (rb.velocity.x != 0)
        {
            anim.SetBool(name: "isMoving",true);
        }
        else
        {
            anim.SetBool(name: "isMoving",false);
        }

        if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        isGrounded = Physics2D.Raycast(origin: (Vector2)transform.position, direction: Vector2.down, distance: raycastLength, (int)groundLayerMask);
        Debug.DrawRay(start: transform.position, Vector3.down * raycastLength, Color.green);

        // to animate jumping
        anim.SetBool(name: "isGrounded", isGrounded);

        // To collect coins
        
    
     }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "coin")
        {
            Destroy(other.gameObject);
        }

        if (other.tag == "respawn")
        {
            respawn();
        }
     }
    void respawn()
    {
        transform.position = respawnpoint.position;
    }
}
