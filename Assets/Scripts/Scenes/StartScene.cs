using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScene : MonoBehaviour
{
    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private Image Tab;

    public bool canOpen = false;

    public void SetCanOpen()
    {
        if (Ondo)
            canOpen = true;
    }

    private bool Fade;
    private bool Ondo = true;

    private void Start()
    {
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync("CatHouseScene");
        async.allowSceneActivation = false;
        float timer = 0.0f;

        while (!async.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if (async.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, async.progress, timer);
                if (progressBar.fillAmount >= async.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if (progressBar.fillAmount == 1.0f)
                {
                    async.allowSceneActivation = true;
                    break;
                    /*
                    if (canOpen)
                    {
                        async.allowSceneActivation = true;
                        break;
                    }
                    */
                }

            }
        }
    }


    float start = 1f;
    float end = 0f;
    IEnumerator Alpa()
    {
        Fade = true;
        Color color = Tab.GetComponent<Image>().color;
        color.a = Mathf.Lerp(1f, 0.2f, 3f);
        yield return null;
        /*
        while (color.a > 0f)
        {
            color.a = Mathf.Lerp(start, end, 0.5f);
            Tab.color = color;
            yield return new WaitForSeconds(0.4f);
            color.a = Mathf.Lerp(end, start, 0.5f);
            Tab.color = color;
        }
        */
    }
}

