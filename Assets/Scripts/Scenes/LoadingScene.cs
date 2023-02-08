using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public static string nextScene;
    public static bool GoGame;


    [SerializeField]
    private Image progressBar;
    [SerializeField]
    private Image ToGame;
    [SerializeField]
    private Image ToCatHouse;


    public static void LoadScene(string sceneName, bool ToGame = false)
    {
        nextScene = sceneName;
        GoGame = ToGame;
        SceneManager.LoadScene("LoadingScene");
    }

    private void Start()
    {
        if(GoGame)
        {
            ToGame.gameObject.SetActive(true);
            ToCatHouse.gameObject.SetActive(false);
        }
        else
        {
            ToGame.gameObject.SetActive(false);
            ToCatHouse.gameObject.SetActive(true);
        }
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        yield return null;
        AsyncOperation async = SceneManager.LoadSceneAsync(nextScene);
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
                    yield return new WaitForSeconds(1f);
                    async.allowSceneActivation = true;
                    break;
                }
            }
        }
    }
}
