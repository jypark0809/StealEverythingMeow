using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : BaseScene
{
    public static string nextScene;

    [SerializeField]
    private Image progressBar;


    private AsyncOperation async;

    protected override void Init()
    {
        base.Init();
        SceneType = Define.SceneType.LoadingScene;

        //StartCoroutine(Load());
    }


    public static void LoadScene(string sceneName)
    {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return null;
        async = SceneManager.LoadSceneAsync(nextScene);
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
                    yield return new WaitForSeconds(2f);
                    async.allowSceneActivation = true;
                    break;
                }
            }
        }
    }
    public override void Clear()
    {
    }
}
