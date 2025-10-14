using UnityEngine;

public class Player : MonoBehaviour
{
    float xvel, yvel;
    Rigidbody2D rb;
    bool isGrounded;
    public Animator anim;
    public GameObject weapon;
    public LayerMask groundLayerMask;
    public LayerMask enemyLayerMask;
    HelperScript helper;
    bool isFacingRight;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        enemyLayerMask = LayerMask.GetMask("Enemy");
        groundLayerMask = LayerMask.GetMask("Ground");
        helper = gameObject.AddComponent<HelperScript>();
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
            helper.FlipObject(true);
            isFacingRight = false;
        }
        if (Input.GetKey("d"))
        {
            xvel = 5;
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
            helper.FlipObject(false);
            isFacingRight = true;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            yvel = 10;
            anim.SetBool("isIdle", false);
        }



        rb.linearVelocity = new Vector2(xvel, yvel);

        GroundCheck();
        ShootBone();

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

    void ShootBone()
    {
        if (Input.GetKeyDown("f"))
        {

            if (isFacingRight == true)
            {
                GameObject clone;
                clone = Instantiate(weapon, transform.position, transform.rotation);
                Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                rb.linearVelocity = new Vector2(15, 0);

                rb.transform.position = new Vector3(transform.position.x + 1, transform.position.y + 1);
            }
            else
            {
                GameObject clone;
                clone = Instantiate(weapon, transform.position, transform.rotation);
                Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
                rb.linearVelocity = new Vector2(-15, 0);

                rb.transform.position = new Vector3(transform.position.x - 1, transform.position.y + 1);
            }
        }
    }
}
