using UnityEngine;

public class Goblin : MonoBehaviour
{

    public Animator anim;

    public LayerMask groundLayerMask;

    bool hitSomething;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();

        groundLayerMask = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GroundRayCollisionCheck(float xoffs, float yoffs)
    {
        anim.SetBool("isGrounded", false);

        Vector2 position = transform.position;
        Vector2 offset = new Vector2 (xoffs, yoffs);
        Vector2 direction = Vector2.down;
        float distance = 0.75f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayerMask);
        if (hit.collider != null)
        {
            anim.SetBool("isGrounded", true);
        }
        Color hitColor = Color.burlywood;

        Debug.DrawRay(position, Vector2.up * distance, hitColor);

        return hitSomething;
    }
}
