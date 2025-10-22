using UnityEngine;

public class Chestpoint : MonoBehaviour
{
    private Respawn respawn;
    public Animator anim;

    private void Awake()
    {
    }
    void Start()
    {
        anim = GetComponent<Animator>();

        respawn = GameObject.FindGameObjectWithTag("ChestPoint").GetComponent<Respawn>();

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ManagerScript.spawnPoint = transform.position;
        }
    }
}
