using System.Collections;
using UnityEngine;
using UnityEngine.UI; // Sau TMPro dacă folosești TextMeshPro
using TMPro;

public class LevelIntro : MonoBehaviour
{
    public TextMeshProUGUI levelText; // Sau TextMeshProUGUI dacă folosești TextMeshPro
    public string levelMessage = "Level 1";
    public float displayDuration = 3f;

    void Start()
    {
        StartCoroutine(ShowLevelText());
    }

    IEnumerator ShowLevelText()
    {
        levelText.text = levelMessage;
        levelText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        levelText.gameObject.SetActive(false);
    }
}
