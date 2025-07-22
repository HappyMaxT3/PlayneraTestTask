using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class HistoryPanelAnimator : MonoBehaviour
{
    private RectTransform panelRectTransform;

    [Header("Animation Settings")]
    public float startPosX = 200f; 
    public float endPosX = 0f;
    public float appearDuration = 1.0f;
    public float stayDuration = 2.0f;
    public float disappearDuration = 1.0f;

    [Header("Text Content")]
    public TextMeshProUGUI historyText;
    [TextArea(3, 10)]
    public string storyContent;

    void Awake()
    {
        panelRectTransform = GetComponent<RectTransform>();
        panelRectTransform.anchoredPosition = new Vector2(startPosX, panelRectTransform.anchoredPosition.y);

        if (historyText != null)
        {
            historyText.text = storyContent;
        }
    }

    void Start()
    {
        StartCoroutine(AnimatePanel());
    }

    private IEnumerator AnimatePanel()
    {
        float timer = 0f;
        Vector2 currentPosition = panelRectTransform.anchoredPosition;
        Vector2 targetPosition = new Vector2(endPosX, currentPosition.y);

        while (timer < appearDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / appearDuration;
            panelRectTransform.anchoredPosition = Vector2.Lerp(currentPosition, targetPosition, progress);
            yield return null;
        }
        panelRectTransform.anchoredPosition = targetPosition;

        yield return new WaitForSeconds(stayDuration);

        timer = 0f;
        currentPosition = panelRectTransform.anchoredPosition;
        targetPosition = new Vector2(startPosX, currentPosition.y);

        while (timer < disappearDuration)
        {
            timer += Time.deltaTime;
            float progress = timer / disappearDuration;
            panelRectTransform.anchoredPosition = Vector2.Lerp(currentPosition, targetPosition, progress);
            yield return null;
        }
        panelRectTransform.anchoredPosition = targetPosition;

        gameObject.SetActive(false);
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}