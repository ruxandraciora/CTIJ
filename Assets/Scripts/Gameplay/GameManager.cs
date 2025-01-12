using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform playerTransform;           // Referință la transform-ul jucătorului
    public Vector3 initialPosition;             // Poziția inițială a jucătorului
    public EnemyManager enemyManager;           // Referință la EnemyManager
    private Vector3 initialCameraPosition;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player transform is not assigned in the Inspector!");
            playerTransform = GameObject.FindWithTag("Player")?.transform;
        }

        initialPosition = playerTransform.position;
        initialCameraPosition = Camera.main.transform.position;  // Salvează poziția inițială a camerei
    }


    public void RestartGame()
{
    Debug.Log("Restarting game...");
        Debug.Log("Camera position: " + Camera.main.transform.position);
        // Resetează timpul de joc
        Time.timeScale = 1f;
       // Camera.main.transform.position = initialCameraPosition;
        playerTransform.GetComponent<PlayerHealth>().ResetPlayer();

        // Resetează poziția jucătorului la poziția inițială
        if (playerTransform != null)
    {
        playerTransform.position = initialPosition;
    }
    else
    {
        Debug.LogError("Player transform is not assigned!");
    }

    // Resetează inamicii existenți
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
