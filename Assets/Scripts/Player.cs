using UnityEngine;

public class Player : MonoBehaviour
{
    float xvel, yvel;
    Rigidbody2D rb;
    bool isGrounded;
    public Animator anim;

    public LayerMask groundLayerMask;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        groundLayerMask = LayerMask.GetMask("Ground");
    }

    void Update()
    {
        xvel = 0;
        yvel = rb.linearVelocity.y;

        anim.SetBool("isIdle", true);
        anim.SetBool("isWalking", false);

        if (Input.GetKey("a"))
        {
            xvel = -5;
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }
        if (Input.GetKey("d"))
        {
            xvel = 5;
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            yvel = 10;
            anim.SetBool("isIdle", false);
        }



        rb.linearVelocity = new Vector2(xvel, yvel);

        GroundCheck();

        anim.SetBool("isJumping", false);

        if (yvel != 0 && isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }
    }

    void GroundCheck()
    {
        isGrounded = false;
        anim.SetBool("isGrounded", false);

        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.75f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayerMask);
        if (hit.collider != null)
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
        Color hitColor = Color.burlywood;

        Debug.DrawRay(position, Vector2.up * distance, hitColor);
    }
}
