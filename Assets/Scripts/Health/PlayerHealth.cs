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

    private void Start()
    {
        // Salvează poziția inițială a jucătorului
        initialPosition = transform.position;

        // Inițializează numărul de vieți și actualizează inimile
        currentLives = maxLives;
        UpdateHeartsUI();

        // Găsește GameManager-ul în scenă
        gameManager = FindObjectOfType<GameManager>();
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
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Slime"))
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {
        if (currentLives > 0)
        {
            currentLives--;          // Scade o viață
            UpdateHeartsUI();        // Actualizează UI-ul inimilor
        }

        if (currentLives <= 0)
        {
            Die();
        }
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

    private void ShowGameOverUI()
    {
        Debug.Log("Game Over! Press R to restart.");
        // Poți afișa un UI personalizat pentru Game Over aici
    }
}
