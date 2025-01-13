using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal2 : MonoBehaviour
{
    // Numele scenei următoare
    public string nextLevelName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifică dacă obiectul care a intrat în portal este jucătorul
        if (other.CompareTag("Player"))
        {
            // Încarcă următorul nivel
            SceneManager.LoadScene("Win");
        }
    }
}
