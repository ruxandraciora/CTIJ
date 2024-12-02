using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float maxDistance = 10f;
    public Rigidbody2D rb;
    private Vector2 startPosition;

    private void Start()
    {

        startPosition = transform.position;
        rb.linearVelocity=transform.right*speed;
    }
    private void Update()
    {
        // Check if the projectile has traveled farther than maxDistance
        if (Vector2.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);  // Destroy the projectile if it traveled the max distance
        }
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
