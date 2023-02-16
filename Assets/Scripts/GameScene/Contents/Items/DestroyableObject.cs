using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour
{
    [SerializeField]
    int id;
    DestroyableObjectData _object;

    void Start()
    {
        Managers.Data.DestroyableObjects.TryGetValue(id, out _object);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StartCoroutine(ShowDestroyableObjectUI());
        }
    }

    IEnumerator ShowDestroyableObjectUI()
    {
        UI_DestroyableObjectPopup ui = Managers.UI.ShowPopupUI<UI_DestroyableObjectPopup>();
        ui.SetInfo(_object);

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
        Managers.Object.ShowGoldText(transform.position, _object.Object_Gold);
        Managers.Game.SaveData.Dia += _object.Object_Diamond;
    }
}
