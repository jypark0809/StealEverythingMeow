using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EndingScene : MonoBehaviour
{
    public GameObject Panels;
    public GameObject Texts;
    public GameObject Buttons;
        

    private void Start()
    {
        StartCoroutine(Onstart());
    }

    IEnumerator Onstart()
    {
        yield return new WaitForSeconds(20f);
        Panels.SetActive(false);
        Texts.SetActive(true);
        Buttons.SetActive(true);
    }
    public void ReturnGame()
    {
        LoadingScene.LoadScene("CatHouseScene", false);
    }
}
