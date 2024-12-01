using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform launchPoint;      
    public float launchForce = 10f;    

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.X))
        {
            LaunchProjectile();
        }
    }

    void LaunchProjectile()
    {
        if (projectilePrefab != null && launchPoint != null)
        {
            
            GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, Quaternion.identity);

           
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                GameObject enemy = FindClosestEnemy(); 
                if (enemy != null)
                {
                    
                    Vector3 direction = (enemy.transform.position - launchPoint.position).normalized;

                    
                    rb.AddForce(direction * launchForce, ForceMode.Impulse);
                }
            }
        }
    }

    GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Slime");
        GameObject closest = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closest = enemy;
            }
        }
        return closest;
    }
}
