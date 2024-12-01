using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
   
    private void Start()
    {
        rb.linearVelocity=transform.right*speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.CompareTag("Slime"))
        {
            // Destroy the projectile
            Debug.Log(hitInfo.tag);
            Destroy(hitInfo.gameObject);
            Destroy(gameObject);
        }
    }
}
