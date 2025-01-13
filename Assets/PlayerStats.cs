using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public int health = 100;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject); // Previi distrugerea obiectului între nivele
        }
        else
        {
            Destroy(gameObject); // Evită existența a mai multor instanțe
        }
    }
}
