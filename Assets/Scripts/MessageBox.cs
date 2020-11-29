using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageBox : MonoBehaviour
{
    CanvasGroup group;

    void Start()
    {
        group = GetComponent<CanvasGroup>();
    }

    public void SetText(string text)
    {
        this.GetComponentInChildren<TextMeshProUGUI>().SetText(text);
    }

    public IEnumerator ShowText()
    {
        while(group.alpha < 1f)
        {
            group.alpha += Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime/2f);
        }
        group.alpha = 1f;
        yield return new WaitForSeconds(2f);

        while(group.alpha > 0f)
        {
            group.alpha -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }

    void Update()
    {
        
    }
}
