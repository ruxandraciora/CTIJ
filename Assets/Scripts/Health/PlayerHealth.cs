using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;             // Numărul maxim de vieți
    private int currentLives;            // Numărul curent de vieți

    public Image[] heartImages;          // Array cu imaginile inimilor din UI
    public float fallLimitY = -10f;      // Limita sub care jucătorul este considerat căzut

    private bool isGameOver = false;     // Indicator pentru starea de Game Over
    private Vector3 initialPosition;     // Poziția inițială a jucătorului

    private GameManager gameManager;     // Referință către GameManager

    private bool isInvulnerable = false;     // Indicator dacă player-ul este invulnerabil
    public float invulnerabilityDuration = 1f; // Durata invulnerabilității în secunde
    private PlayerController playerController;


    private void Start()
    {
        initialPosition = transform.position;
        currentLives = maxLives;
        UpdateHeartsUI();
        gameManager = FindObjectOfType<GameManager>();

        // Asigură-te că ai o referință validă la PlayerController
        playerController = GetComponent<PlayerController>();
    }


    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            if (gameManager != null)
                gameManager.RestartGame();
            else
                Debug.LogWarning("GameManager is not set in PlayerHealth!");
        }

        // Verifică dacă jucătorul a căzut sub limită
        if (transform.position.y <= fallLimitY && !isGameOver)
        {
            FallOffPlatform();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slime"))
        {
            TakeDamage();
        }
    }

    public void TakeDamage()
    {
        // Dacă scutul este activ sau player-ul este invulnerabil, nu ia damage
        if (playerController != null && (playerController.isShieldActive || isInvulnerable))
        {
            Debug.Log("Damage blocked: Shield or invulnerability is active.");
            return;
        }

        if (currentLives > 0)
        {
            currentLives--;          // Scade o viață
            UpdateHeartsUI();        // Actualizează UI-ul inimilor
            StartCoroutine(InvulnerabilityCoroutine()); // Activează invulnerabilitatea temporară
        }

        if (currentLives <= 0)
        {
            Die();
        }
    }



    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration); // Așteaptă durata invulnerabilității
        isInvulnerable = false;
    }



    private void UpdateHeartsUI()
    {
        // Dezactivează inimile rămase în funcție de viețile curente
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentLives;
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        isGameOver = true;

        // Oprește timpul de joc
        Time.timeScale = 0f;

        // Afișează un mesaj de Game Over sau UI (dacă ai unul)
        ShowGameOverUI();
    }

    private void FallOffPlatform()
    {
        Debug.Log("Player fell off the platform!");

        // Scade o viață
        TakeDamage();

        if (currentLives > 0)
        {
            // Repoziționează jucătorul la poziția inițială dacă mai are vieți
            transform.position = initialPosition;
        }
    }
   


    private void ShowGameOverUI()
    {
        Debug.Log("Game Over! Press R to restart.");
        // Poți afișa un UI personalizat pentru Game Over aici
    }

    public void ResetPlayer()
    {
        Debug.Log("Resetting player...");
        isGameOver = false;
        currentLives = maxLives;     // Resetează numărul de vieți
        UpdateHeartsUI();            // Actualizează UI-ul inimilor
        transform.position = initialPosition;  // Repoziționează jucătorul la poziția inițială
        Time.timeScale = 1f;         // Reia timpul de joc
    }
}
