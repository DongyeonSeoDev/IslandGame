using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image fadeInPanel = null;
    private WaitForSeconds startGameDelay;

    [SerializeField] private float delayTime = 0f;
    [SerializeField] private float fadeInTime = 0f;

    private void Start()
    {
        startGameDelay = new WaitForSeconds(delayTime);

        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float currentTime = 0f;
        Color color = Color.black;

        yield return startGameDelay;

        while (true)
        {
            currentTime += Time.deltaTime;

            color.a = Mathf.Lerp(1, 0, currentTime / fadeInTime);
            fadeInPanel.color = color;

            if (currentTime >= fadeInTime)
            {
                break;
            }

            yield return null;
        }
    }
}
