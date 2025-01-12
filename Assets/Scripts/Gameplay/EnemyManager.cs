using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject[] enemies;  // Array pentru inamicii existenți în scenă

    void Start()
    {
        // Găsește toți inamicii cu tag-ul "Slime" din scenă
        enemies = GameObject.FindGameObjectsWithTag("Slime");
    }

    public void ResetEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Slime");

        
        foreach (GameObject enemy in enemies)
        {
            enemyPatrol patrol = enemy.GetComponent<enemyPatrol>();
            //Debug.Log("Enemy position after reset: " + enemy.transform.position);
            if (patrol != null)
            {
                patrol.ResetEnemy();
                Debug.Log("Enemy reset: " + enemy.name);
            }
        }
    }



    public void DestroyEnemies()
    {
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.SetActive(false);  // Dezactivează inamicul în loc să-l distrugă
            }
        }
    }

}
