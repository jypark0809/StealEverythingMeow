using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField]
    int id;

    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerPrefs.SetInt("DestoryableObject", id);
            StartCoroutine(UITest());
        }
    }

    IEnumerator UITest()
    {
        UI_DestroyableObjectPopup ui = Managers.UI.ShowPopupUI<UI_DestroyableObjectPopup>();
        while(ui != null)
        {
            yield return new WaitForSeconds(0.1f);
        }

        if (id == 2305)
        {
            Time.timeScale = 0;
            Managers.UI.ShowPopupUI<UI_StageClear>();
        }

        Managers.Resource.Destroy(gameObject);
    }
}
