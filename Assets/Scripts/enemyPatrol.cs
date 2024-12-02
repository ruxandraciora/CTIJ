using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform[] patrolPoints;
    public float moveSpeed;
    public int patrolDestination;
  

    // Update is called once per frame
    void Update()
    {
        if(patrolDestination == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[0].position,moveSpeed*Time.deltaTime);
            if (Vector2.Distance(transform.position, patrolPoints[0].position)<.2f)
            {
                transform.localScale=new Vector3(0.5f,0.5f,1);
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
}
