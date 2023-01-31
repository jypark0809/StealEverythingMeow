using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    Item[] _items;
    BoxCollider2D _boxCollider;

    [SerializeField]
    int respawnCount = 1;

    [SerializeField]
    float spawnTime = 60;
    float timer;
    public bool isActive = false;

    void Start()
    {
        timer = spawnTime;
        SpawnCunstructionItem();
        _items = GetComponentsInChildren<Item>();
    }

    void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                if (respawnCount > 0)
                {
                    RespawnCommonItem();
                    timer = spawnTime;
                    respawnCount--;
                }
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

    void SpawnCunstructionItem()
    {
        int rand = Random.Range(0, 8);
        switch(rand)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                Managers.Resource.Instantiate("Item/ConstructionItem/Wood", transform).transform.position = transform.position;
                break;
            case 4:
            case 5:
            case 6:
                Managers.Resource.Instantiate("Item/ConstructionItem/Rock", transform).transform.position = transform.position;
                break;
            case 7:
                Managers.Resource.Instantiate("Item/ConstructionItem/Cotton", transform).transform.position = transform.position;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Managers.Object.Player.gameObject.layer = 27;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Managers.Object.Player.gameObject.layer = 29;
    }
}
