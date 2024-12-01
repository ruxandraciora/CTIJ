using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Slime"))
        {
           
            Destroy(collision.gameObject);

            
            Destroy(gameObject);
        }
        else
        {
            
            Destroy(gameObject, 2f); 
        }
    }
}
