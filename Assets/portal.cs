using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // Numele scenei următoare
    public string nextLevelName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Setează poziția jucătorului înainte de a încărca noul nivel
            other.transform.position = new Vector3(0, 5, 0); // Poziția dorită pentru noul nivel

            // Încarcă următorul nivel
            SceneManager.LoadScene("Level2");
        }
    }
}
