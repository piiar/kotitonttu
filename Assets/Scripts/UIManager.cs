using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    Camera cam;

    public Dictionary<string, RectTransform> actionIndicatorCache = new Dictionary<string, RectTransform>();
    public RectTransform[] actionIndicators;

    [SerializeField]
    int indicatorCount;
    [SerializeField]
    int nextIndex;

    private static UIManager _instance;
    public static UIManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Object.FindObjectOfType<UIManager>();

                //Tell unity not to destroy this object when loading a new scene!
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        cam = Camera.main;
        indicatorCount = actionIndicators.Length;

        for (int i = 0; i < indicatorCount; i++) actionIndicators[i].gameObject.SetActive(false);
    }

    public void OpenThinkBubble(Transform t, ItemType type)
    {

        actionIndicatorCache.TryGetValue(t.name, out RectTransform rect);

        if(rect) {
            rect.gameObject.SetActive(true);
            Vector3 screenPos = cam.WorldToScreenPoint(t.position);
            rect.anchoredPosition = new Vector2(screenPos.x + 5, screenPos.y + 5);
            rect.GetComponent<ActionIndicator>().SetAction(type);
        }
        else {
            if(nextIndex < indicatorCount) {
                actionIndicatorCache[t.name] = actionIndicators[nextIndex++];
                
                RectTransform _rect = actionIndicatorCache[t.name];
                _rect.gameObject.SetActive(true);

                Vector3 screenPos = cam.WorldToScreenPoint(t.position);
                _rect.anchoredPosition = new Vector2(screenPos.x + 5, screenPos.y + 5);
                _rect.GetComponent<ActionIndicator>().SetAction(type);
            }
        }
    }

    public void CloseThinkBubble(string name)
    {
        actionIndicatorCache.TryGetValue(name, out RectTransform rect);
        if (rect)
        {
            rect.gameObject.SetActive(false);
        }
    }

    public void UpdateThinkBubble(Transform t)
    {
        actionIndicatorCache.TryGetValue(t.name, out RectTransform rect);
        if (rect) {
            Vector3 screenPos = cam.WorldToScreenPoint(t.position);
            rect.anchoredPosition = new Vector2(screenPos.x + 5, screenPos.y + 5);
        }
    }
}
