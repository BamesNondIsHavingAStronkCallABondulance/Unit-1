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
    public LayerMask barrierLayerMask;
    HelperScript helper;
    bool isFacingRight;
    public int health;
    public int currentBoneAmmo;
    public int maxBoneAmmo;
    float invFrames = 2;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        enemyLayerMask = LayerMask.GetMask("Enemy");
        groundLayerMask = LayerMask.GetMask("Ground");
        barrierLayerMask = LayerMask.GetMask("DeathBarrier");
        helper = gameObject.AddComponent<HelperScript>();
        health = 3;
        currentBoneAmmo = 4;
        maxBoneAmmo = 12;
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

        if (Input.GetMouseButtonDown(0))
        {
            if (currentBoneAmmo > 0)
            {
                ShootBone();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            yvel = 8;
            anim.SetBool("isIdle", false);
        }

        anim.SetBool("isJumping", false);

        if (yvel != 0 && isGrounded == false)
        {
            anim.SetBool("isJumping", true);
        }



        rb.linearVelocity = new Vector2(xvel, yvel);

        GroundCheck();
        DeathBarrierCheck();
        EnemyCheck(0.5f, 1);




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
        float distance = 0.4f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayerMask);
        if (hit.collider != null)
        {
            isGrounded = true;
            anim.SetBool("isGrounded", true);
        }
        Color hitColor = Color.burlywood;

        Debug.DrawRay(position, Vector2.up * distance, hitColor);
    }

    void EnemyCheck(float xoffs, float yoffs)
    {

        if(invFrames > 0)
        {
            invFrames -= Time.deltaTime;
            return;
        }


        Vector2 position = transform.position;
        Vector2 direction = Vector2.left;
        Vector2 offset = new Vector2(xoffs, yoffs);
        float distance = 0.3f;

        RaycastHit2D hitleft = Physics2D.Raycast(position + offset, direction, distance, enemyLayerMask);

        Vector2 directionr = Vector2.right;

        RaycastHit2D hitright = Physics2D.Raycast(position + offset, directionr, distance, enemyLayerMask);

        if (hitleft.collider != null || hitright.collider != null)
        {
            health -= 1;
            invFrames = 1;
        }

        Color hitColor = Color.burlywood;

        Debug.DrawRay(position + offset, Vector2.left * distance, hitColor);
        Debug.DrawRay(position + offset, Vector2.right * distance, hitColor);
    }

    void DeathBarrierCheck()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 0.2f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, barrierLayerMask);
        if (hit.collider != null)
        {
            health -= 1;
            transform.position = new Vector2(-177, -109);
        }
    }

    void ShootBone()
    {
           if (isFacingRight == true)
           {
               GameObject clone;
               clone = Instantiate(weapon, transform.position, transform.rotation);
               Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
               rb.linearVelocity = new Vector2(15, 0);

               rb.transform.position = new Vector3(transform.position.x + 1, transform.position.y + 1.5f);
               currentBoneAmmo -= 1;
           }
           else
           {
               GameObject clone;
               clone = Instantiate(weapon, transform.position, transform.rotation);
               Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
               rb.linearVelocity = new Vector2(-15, 0);

               rb.transform.position = new Vector3(transform.position.x - 1, transform.position.y + 1.5f);
               currentBoneAmmo -= 1;
           }
    }

    public void AddAmmo(int ammoAmount)
    {
        currentBoneAmmo += ammoAmount;
        if(currentBoneAmmo > maxBoneAmmo)
        {
            currentBoneAmmo = maxBoneAmmo;
        }
    }
}
