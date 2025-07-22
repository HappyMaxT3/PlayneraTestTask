using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class BookController : MonoBehaviour
{
    public List<GameObject> pageGameObjects = new List<GameObject>();
    public float pageFlipDuration = 0.5f;

    private int currentPageIndex = 0;

    void Start()
    {
        if (pageGameObjects.Count == 0)
        {
            Debug.LogError("No page GameObjects assigned in BookController!");
            return;
        }

        HideAllPages();
        UpdatePageDisplay();
    }

    public void TurnPageRight()
    {
        if (currentPageIndex < pageGameObjects.Count - 1)
        {
            currentPageIndex++;
            StartCoroutine(AnimatePageFlip());
        }
        else
        {
            Debug.Log("Already on the last page!");
        }
    }

    public void TurnPageLeft()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            StartCoroutine(AnimatePageFlip());
        }
        else
        {
            Debug.Log("Already on the first page!");
        }
    }

    private void UpdatePageDisplay()
    {
        HideAllPages();

        if (currentPageIndex >= 0 && currentPageIndex < pageGameObjects.Count)
        {
            pageGameObjects[currentPageIndex].SetActive(true);
        }
    }

    private void HideAllPages()
    {
        foreach (GameObject pageGO in pageGameObjects)
        {
            if (pageGO != null)
            {
                pageGO.SetActive(false);
            }
        }
    }

    private IEnumerator AnimatePageFlip()
    {
        GameObject oldPageGO = null;
        if (currentPageIndex < pageGameObjects.Count)
        {
             oldPageGO = pageGameObjects[currentPageIndex];
        }
        
        GameObject newPageGO = pageGameObjects[currentPageIndex];
        SpriteRenderer newPageRenderer = null;
        if (newPageGO != null) newPageRenderer = newPageGO.GetComponent<SpriteRenderer>();

        HideAllPages(); 
        
        if (newPageGO != null) newPageGO.SetActive(true);

        if (newPageRenderer != null)
        {
            float timer = 0f;
            Color startColor = new Color(newPageRenderer.color.r, newPageRenderer.color.g, newPageRenderer.color.b, 0f);
            Color endColor = new Color(newPageRenderer.color.r, newPageRenderer.color.g, newPageRenderer.color.b, 1f);
            newPageRenderer.color = startColor;

            while (timer < pageFlipDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / pageFlipDuration;
                newPageRenderer.color = Color.Lerp(startColor, endColor, progress);
                yield return null;
            }
            newPageRenderer.color = endColor;
        }
    }
}