using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StartScene : BaseScene
{
    [SerializeField]
    private Image progressBar;


    private AsyncOperation async;
    private bool canOpen = false;

    public void SetCanOpen()
    {
        canOpen = true;
    }
    protected override void Init()
    {
        base.Init();
        SceneType = Define.SceneType.StartScene;

        //StartCoroutine(Load());
    }

    private void Start()
    {
        StartCoroutine(Load());
    }
    IEnumerator Load()
    {
        yield return null;
        async = SceneManager.LoadSceneAsync("CatHouseScene");
        async.allowSceneActivation = false;
        float timer = 0.0f;

        while (!async.isDone)
        {
            yield return null;
            timer += Time.deltaTime;
            if(async.progress<0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, async.progress, timer);
                if(progressBar.fillAmount >= async.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                if(progressBar.fillAmount == 1.0f)
                { 
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

