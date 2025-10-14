using UnityEngine;

public class Bones : MonoBehaviour
{
    float lifespan = 3;

    void Update()
    {
        lifespan -= Time.deltaTime;
        if ( lifespan <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if ( col.gameObject.tag == "Ground" || col.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
