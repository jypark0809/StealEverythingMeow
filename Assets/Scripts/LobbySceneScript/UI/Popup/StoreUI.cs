using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftName;
    public GameObject go_prefab;
    public GameObject go_previwPrefab;
}


public class StoreUI : UI_Popup
{
    public List<TabButton> tabButtons;
    public List<GameObject> contentsPanels;

    [SerializeField]
    private Craft[] somsom;

    private GameObject go_Previw;

    public void ClickTab(int id)
    {
        for(int i = 0; i <contentsPanels.Count; i++)
        {
            if(i == id)
            {
                contentsPanels[i].SetActive(true);
                tabButtons[i].Selected();
            }
            else
            {
                contentsPanels[i].SetActive(false);
                tabButtons[i].DeSeleted();
            }
        }
    }

    public void BuySOM()
    {
        go_Previw = Instantiate(somsom[0].go_previwPrefab, new Vector3(0.5f, 0.5f, 0), Quaternion.identity);
        go_Previw.transform.parent = FindObjectOfType<Grid>().transform;
        Destroy(this.gameObject);
    }
}
