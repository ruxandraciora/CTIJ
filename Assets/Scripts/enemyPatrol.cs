using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;

    private Vector3 initialPosition;  // Poziția inițială a inamicului

    void Start()
    {
        initialPosition = transform.position;  // Salvează poziția inițială
    }

    void Update()
    {
        if (patrolPoints.Length < 2)
        {
            Debug.LogWarning("Not enough patrol points assigned!");
            return; // Oprește execuția dacă nu sunt suficiente puncte
        }

        if (patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position) < .2f)
            {
                transform.localScale = new Vector3(0.5f, 0.5f, 1);
                patrolDestination = 1;
            }
        }

        if (patrolDestination == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[1].position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[1].position) < .2f)
            {
                transform.localScale = new Vector3(-0.5f, 0.5f, 1);
                patrolDestination = 0;
            }
        }
    }


    public void ResetEnemy()
    {
        transform.position = initialPosition; // Resetează poziția
        gameObject.SetActive(true);           // Reactivează inamicul (dacă era dezactivat)
        Debug.Log("Enemy reset to initial position.");
    }
}
