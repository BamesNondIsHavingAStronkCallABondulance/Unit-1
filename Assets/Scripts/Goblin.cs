using UnityEditor;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    HelperScript helper;

    public LayerMask groundLayerMask;

    Rigidbody2D rb;

    float xvel, yvel;

    public Animator anim;

    int health = 3;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        xvel = 1;

        anim = GetComponent<Animator>();
        helper = gameObject.AddComponent<HelperScript>();
        groundLayerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        yvel = rb.linearVelocity.y;

        if (xvel < 0)
        {
            if (ExtendedRayCollisionCheck(-0.95f, 0) == false)
            {
                xvel = -xvel;
                helper.FlipObject(false);
            }
        }
        else if (xvel > 0)
        {
            if (ExtendedRayCollisionCheck(0.95f, 0) == false)
            {
                xvel = -xvel;
                helper.FlipObject(true);
            }
        }

        HealthCheck();

        rb.linearVelocity = new Vector2(xvel, yvel);
    }

    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 0.2f;
        bool hitSomething = false;

        Vector3 offset = new Vector3(xoffs, yoffs, 0);

        RaycastHit2D hit;

        hit = Physics2D.Raycast(transform.position + offset, Vector2.down, rayLength, groundLayerMask);

        if (hit.collider != null)
        {
            hitSomething = true;
        }

        return hitSomething;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if ( col.gameObject.tag == "Bone")
        {
            health -= 1;
        }
    }

    void HealthCheck()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
