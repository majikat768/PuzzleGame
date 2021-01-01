using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    RectTransform rectTransform;
    void Start()
    {
        rectTransform = this.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(Screen.width, Screen.height) / 2 - rectTransform.sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
