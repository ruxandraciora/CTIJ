using UnityEngine;
using System.Collections;
using Platformer.Mechanics; // Importă namespace-ul unde se află PlayerController

public class ShieldSpawner : MonoBehaviour
{
    public GameObject shieldPrefab;   // Prefab-ul scutului
    public float shieldDuration = 5f; // Durata scutului în secunde
    public float cooldownTime = 10f;  // Timpul de așteptare până când scutul poate fi reactivat
    public float shieldScaleFactor = 10f; // Factorul de scalare al scutului față de player

    private bool isOnCooldown = false; // Indicator pentru cooldown
    private PlayerController playerController; // Referința către PlayerController

    void Awake()
    {
        // Obține referința către PlayerController
        playerController = GetComponent<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController nu a fost găsit pe obiectul ShieldSpawner!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isOnCooldown)
        {
            StartCoroutine(ActivateShield());
        }
    }

    IEnumerator ActivateShield()
    {
        isOnCooldown = true;

        // Activează scutul
        if (playerController != null)
        {
            playerController.isShieldActive = true;
        }

        // Creează scutul la poziția player-ului
        GameObject spawnedShield = Instantiate(shieldPrefab, transform.position, Quaternion.identity);

        // Setează scutul ca fiind copil al player-ului și resetează transformările
        spawnedShield.transform.SetParent(transform);
        spawnedShield.transform.localPosition = Vector3.zero;
        spawnedShield.transform.localRotation = Quaternion.identity;
        spawnedShield.transform.localScale = Vector3.one * shieldScaleFactor; // Scalare mai mare decât player-ul

        spawnedShield.SetActive(true);

        // Așteaptă durata scutului
        yield return new WaitForSeconds(shieldDuration);

        // Dezactivează scutul
        if (spawnedShield != null)
        {
            Destroy(spawnedShield);
        }

        if (playerController != null)
        {
            playerController.isShieldActive = false;
        }

        // Așteaptă cooldown-ul de 10 secunde
        yield return new WaitForSeconds(cooldownTime);

        isOnCooldown = false;
    }
}
