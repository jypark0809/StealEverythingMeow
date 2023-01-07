using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    Item[] _items;

    [SerializeField]
    float spawnTime = 60;
    float timer;
    public bool isActive = false;

    void Start()
    {
        timer = spawnTime;
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
                timer = spawnTime;
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
