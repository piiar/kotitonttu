using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Camera cam;

    public RectTransform panel;

    public Dictionary<string, RectTransform> actionIndicatorCache = new Dictionary<string, RectTransform>();
    public RectTransform[] actionIndicators;

    public Dictionary<string, ProgressIndicator> healthIndicatorCache = new Dictionary<string, ProgressIndicator>();

    int actionIndicatorCount;
    int nextActionIndicatorIndex;

    public GameObject healthIndicatorPrefab;

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
        actionIndicatorCount = actionIndicators.Length;

        for (int i = 0; i < actionIndicatorCount; i++) actionIndicators[i].gameObject.SetActive(false);

        Item[] items = FindObjectsOfType<Item>().Where(item => item.isGoal && item.currentValue > 0).ToArray();

        foreach(Item item in items){
            healthIndicatorCache[item.name] = Instantiate(healthIndicatorPrefab).GetComponent<ProgressIndicator>();
            healthIndicatorCache[item.name].gameObject.transform.SetParent(panel);
            Vector3 screenPos = cam.WorldToScreenPoint(item.transform.position);
            healthIndicatorCache[item.name].GetComponent<RectTransform>().anchoredPosition = new Vector2(screenPos.x + 15, screenPos.y + 25);
        }
    }

    public void UpdateProgessIndicator(string name, float amount) {
        healthIndicatorCache.TryGetValue(name, out ProgressIndicator healthbar);
        if (healthbar)
        {
            healthbar.UpdateBar(amount);
        }       
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
            if(nextActionIndicatorIndex < actionIndicatorCount) {
                actionIndicatorCache[t.name] = actionIndicators[nextActionIndicatorIndex++];

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
