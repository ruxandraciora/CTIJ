using UnityEngine;

public class Resettable : MonoBehaviour
{
    private Vector3 initialPosition;   // Poziția inițială a obiectului

    private void Start()
    {
        // Salvează poziția inițială a obiectului
        initialPosition = transform.position;
    }

    public void ResetObject()
    {
        // Resetează poziția obiectului la cea inițială
        transform.position = initialPosition;

        // Dacă obiectul are un Rigidbody2D, îi resetează viteza
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        // Reactivează obiectul în caz că a fost dezactivat
        gameObject.SetActive(true);
    }
}
