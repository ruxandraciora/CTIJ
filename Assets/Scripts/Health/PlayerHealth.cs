using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    public Image[] heartImages;
    public float fallLimitY = -10f;

    private bool isGameOver = false;
    private Vector3 initialPosition;

    private GameManager gameManager;

    private bool isInvulnerable = false;
    public float invulnerabilityDuration = 1f;
    private PlayerController playerController;

    private void Start()
    {
        initialPosition = transform.position;
        currentLives = maxLives;
        UpdateHeartsUI();
        gameManager = FindObjectOfType<GameManager>();

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
        if (playerController != null && (playerController.isShieldActive || isInvulnerable))
        {
            Debug.Log("Damage blocked: Shield or invulnerability is active.");
            return;
        }

        if (currentLives > 0)
        {
            currentLives--;
            UpdateHeartsUI();
            StartCoroutine(InvulnerabilityCoroutine());
        }

        if (currentLives <= 0)
        {
            Die();
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    private void UpdateHeartsUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < currentLives;
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        isGameOver = true;
        Time.timeScale = 0f;
        ShowGameOverUI();
    }

    private void FallOffPlatform()
    {
        Debug.Log("Player fell off the platform!");

        TakeDamage();

        if (currentLives > 0)
        {
            transform.position = initialPosition;
        }
    }

    private void ShowGameOverUI()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ResetPlayer()
    {
        Debug.Log("Resetting player...");
        isGameOver = false;
        currentLives = maxLives;
        UpdateHeartsUI();
        transform.position = initialPosition;
        Time.timeScale = 1f;
    }
}
