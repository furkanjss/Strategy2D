using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfiniteScroll : MonoBehaviour
{
   private ScrollRect scrollRect;
    [SerializeField] private float margin;
    
    private float offset;
    private List<RectTransform> items = new List<RectTransform>();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnScroll);
        PopulateItemsList();
        if (items.Count > 1)
        {
            offset = items[1].anchoredPosition.y - items[0].anchoredPosition.y;
            margin = offset * items.Count / 2;
        }
        else
        {
            Debug.LogWarning("Not enough items to perform infinite scrolling.");
        }
    }

    private void PopulateItemsList()
    {
        items.Clear();
        for (int i = 0; i < scrollRect.content.childCount; i++)
        {
            RectTransform item = scrollRect.content.GetChild(i).GetComponent<RectTransform>();
            if (item != null)
            {
                items.Add(item);
            }
            else
            {
                Debug.LogWarning($"Child at index {i} is missing RectTransform component.");
            }
        }
    }

    private void OnScroll(Vector2 pos)
    {
        for (int i = 0; i < items.Count; i++)
        {
            RectTransform item = items[i];
            Vector2 itemPosition = scrollRect.transform.InverseTransformPoint(item.position);

            if (itemPosition.y > -margin)
            {
                ShiftItemUp(item);
            }
            else if (itemPosition.y < margin)
            {
                ShiftItemDown(item);
            }
        }
    }

    private void ShiftItemUp(RectTransform item)
    {
        Vector2 tmpAnchoredPosition = item.anchoredPosition;
        tmpAnchoredPosition.y += items.Count * offset;
        item.anchoredPosition = tmpAnchoredPosition;
    }

    private void ShiftItemDown(RectTransform item)
    {
        Vector2 tmpAnchoredPosition = item.anchoredPosition;
        tmpAnchoredPosition.y -= items.Count * offset;
        item.anchoredPosition = tmpAnchoredPosition;
    }
}
