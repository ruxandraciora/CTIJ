using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private GameObject[] enemies;      // Toți inamicii din scenă
    private Vector3[] initialPositions;  // Pozițiile inițiale ale inamicilor
    private GameObject[] initialEnemies;  // Copia inițială a inamicilor


    void Start()
    {
        // Găsește toți inamicii din scenă
        enemies = GameObject.FindGameObjectsWithTag("Slime");

        // Inițializează array-urile pentru inamici și poziții
        initialEnemies = new GameObject[enemies.Length];
        initialPositions = new Vector3[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            initialPositions[i] = enemies[i].transform.position;

            // Fă o copie a fiecărui inamic inițial
            initialEnemies[i] = Instantiate(enemies[i], initialPositions[i], Quaternion.identity);
            initialEnemies[i].SetActive(false);  // Dezactivează copia pentru a nu apărea în scenă la început
        }
    }


    public void ResetEnemies()
    {
        Debug.Log("ResetEnemies called");

        // Distruge toți inamicii existenți în scenă
        foreach (var enemy in enemies)
        {
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }

        // Recreează inamicii folosind copiile inițiale
        enemies = new GameObject[initialEnemies.Length];
        for (int i = 0; i < initialEnemies.Length; i++)
        {
            enemies[i] = Instantiate(initialEnemies[i], initialPositions[i], Quaternion.identity);
            enemies[i].SetActive(true);  // Activează inamicul
            Debug.Log("Enemy recreated: " + enemies[i].name);
        }
    }



    public void DestroyEnemies()
    {
        // Dezactivează toți inamicii existenți
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(false);  // Dezactivează inamicul
                Debug.Log("Enemy deactivated: " + enemies[i].name);
            }
        }
    }


    private void DebugEnemiesState()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                Debug.Log($"Enemy {i}: {enemies[i].name}, Active: {enemies[i].activeSelf}, Position: {enemies[i].transform.position}");
            }
            else
            {
                Debug.LogWarning($"Enemy {i} is null.");
            }
        }
    }

}
