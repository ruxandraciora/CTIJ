using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Transform playerTransform;
    public EnemyManager enemyManager;

    void Start()
    {
        playerTransform = GameObject.FindWithTag("Player")?.transform;

        if (playerTransform == null)
        {
            Debug.LogError("Player not found in the scene!");
        }
    }

    public void LoadNextLevel(string nextLevelName)
    {
        if (playerTransform != null)
        {
            Destroy(playerTransform.gameObject); // Distruge player-ul existent
        }

        SceneManager.LoadScene("Level2"); // Încarcă următorul nivel
    }


    public void RestartGame()
    {
        Debug.Log("Restarting game...");

        Time.timeScale = 1f;

        if (playerTransform != null)
        {
            playerTransform.GetComponent<PlayerHealth>().ResetPlayer();
        }
        else
        {
            Debug.LogError("Player transform is not assigned!");
        }

        if (enemyManager != null)
        {
            Debug.Log("Calling ResetEnemies...");
            enemyManager.ResetEnemies();
        }
        else
        {
            Debug.LogError("EnemyManager is not assigned!");
        }

        Debug.Log("Game restarted!");
    }
}
