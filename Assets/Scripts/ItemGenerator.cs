using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    Item[] _items;
    float timer = 10;
    public bool isActive = false;

    void Start()
    {
        _items = GetComponentsInChildren<Item>();
    }

    void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                RespawnCommonItem();
                timer = 10;
            }
        }
    }

    void RespawnCommonItem()
    {
        foreach(Item item in _items)
        {
            if (item.gameObject.activeSelf == false)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}
