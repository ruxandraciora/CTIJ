using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject Spell; 
    public Transform launchPoint;      
    public float launchForce = 10f;    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(Spell,launchPoint.position,launchPoint.rotation);
    }

  
   
}
