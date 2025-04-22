using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SplashText : MonoBehaviour
{
    public TextMeshProUGUI nameTMP;
    public string TargetText;
    public float TextDelayTime;

    public Slider LoadBar;
    public float LoadDelay;

    private void Start()
    {
        StartCoroutine(TextDelay(TextDelayTime));
    }
    public IEnumerator TextDelay(float T)
    {
        for (int i = 0; i < TargetText.Length; i++)
        {
            nameTMP.text += TargetText[i];

            if (i == 2)
            {
                yield return new WaitForSeconds(TextDelayTime * 3);
            }
            else
            {
                yield return new WaitForSeconds(TextDelayTime);
            }
        }
        yield return new WaitForSeconds(TextDelayTime);
        StartCoroutine(LoadingBar(LoadDelay));
    }
    public IEnumerator LoadingBar(float T)
    {
        while (LoadBar.value < 1)
        {
            if (LoadBar.value > 0 && LoadBar.value < 0.5f)
            {
                LoadBar.value += 0.5f;
                yield return new WaitForSeconds(T);
            }
            else
            {
                LoadBar.value += 0.25f;
                yield return new WaitForSeconds(T);
            }
        }
    }
}