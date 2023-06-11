using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureMap : Item
{
    PlayerController player;
    GameObject indicatorCanvas;
    GameObject indicatorPref;
    private float defaultAngle;

    public bool isIndicate = false;

    void Start()
    {
        player = Managers.Object.Player;
        indicatorPref = Managers.Resource.Instantiate("Indicator");

        indicatorCanvas = GameObject.Find("UI_IndicatorCanvas");
        indicatorPref.transform.SetParent(indicatorCanvas.transform);

        Vector2 dir = new Vector2(Screen.width, Screen.height);
        defaultAngle = Vector2.Angle(new Vector2(0, 1), dir);
    }

    void Update()
    {
        SetIndicator();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Managers.Sound.Play(Define.Sound.Effect, "Effects/GetItem", volume: 0.4f);

            Managers.Object.Player.Stat.Map++;
            Managers.Resource.Destroy(indicatorPref);
            Managers.Resource.Destroy(gameObject);

            if (player.TryGetComponent<GrayCat>(out GrayCat cat))
            {
                cat.SetNewMap();
            }
        }
    }

    public void SetIndicator()
    {
        if (!isIndicate)
        {
            indicatorPref.SetActive(false);
            return;
        }

        if (!IsOffScreen())
            return;

        float angle = Vector2.Angle(new Vector2(0, 1), transform.position - player.transform.position);
        int sign = player.transform.position.x > transform.position.x ? -1 : 1;
        angle *= sign;

        Vector3 target = Camera.main.WorldToViewportPoint(transform.position);

        float x = target.x - 0.5f;
        float y = target.y - 0.5f;

        RectTransform indicatorRect = indicatorPref.GetComponent<RectTransform>();
        indicatorRect.rotation = Quaternion.Euler(0, 0, -angle);

        if (-defaultAngle <= angle && angle <= defaultAngle)
        {
            Debug.Log("up");
            //anchor minY, maxY 0.96

            float anchorMinMaxY = 0.96f;

            float anchorMinMaxX = x * (anchorMinMaxY - 0.5f) / y + 0.5f;

            if (anchorMinMaxX >= 0.94f) anchorMinMaxX = 0.94f;
            else if (anchorMinMaxX <= 0.06f) anchorMinMaxX = 0.06f;

            indicatorRect.anchorMin = new Vector2(anchorMinMaxX, anchorMinMaxY);
            indicatorRect.anchorMax = new Vector2(anchorMinMaxX, anchorMinMaxY);
        }
        else if (defaultAngle <= angle && angle <= 180 - defaultAngle)
        {
            Debug.Log("right");
            //anchor minX, maxX 0.94

            float anchorMinMaxX = 0.94f;

            float anchorMinMaxY = y * (anchorMinMaxX - 0.5f) / x + 0.5f;

            if (anchorMinMaxY >= 0.96f) anchorMinMaxY = 0.96f;
            else if (anchorMinMaxY <= 0.04f) anchorMinMaxY = 0.04f;

            indicatorRect.anchorMin = new Vector2(anchorMinMaxX, anchorMinMaxY);
            indicatorRect.anchorMax = new Vector2(anchorMinMaxX, anchorMinMaxY);
        }
        else if (-180 + defaultAngle <= angle && angle <= -defaultAngle)
        {
            Debug.Log("left");
            //anchor minX, maxX 0.06

            float anchorMinMaxX = 0.06f;

            float anchorMinMaxY = (y * (anchorMinMaxX - 0.5f) / x) + 0.5f;

            if (anchorMinMaxY >= 0.96f) anchorMinMaxY = 0.96f;
            else if (anchorMinMaxY <= 0.04f) anchorMinMaxY = 0.04f;

            indicatorRect.anchorMin = new Vector2(anchorMinMaxX, anchorMinMaxY);
            indicatorRect.anchorMax = new Vector2(anchorMinMaxX, anchorMinMaxY);
        }
        else if (-180 <= angle && angle <= -180 + defaultAngle || 180 - defaultAngle <= angle && angle <= 180)
        {
            Debug.Log("down");
            //anchor minY, maxY 0.04

            float anchorMinMaxY = 0.04f;

            float anchorMinMaxX = x * (anchorMinMaxY - 0.5f) / y + 0.5f;

            if (anchorMinMaxX >= 0.94f) anchorMinMaxX = 0.94f;
            else if (anchorMinMaxX <= 0.06f) anchorMinMaxX = 0.06f;

            indicatorRect.anchorMin = new Vector2(anchorMinMaxX, anchorMinMaxY);
            indicatorRect.anchorMax = new Vector2(anchorMinMaxX, anchorMinMaxY);
        }

        indicatorRect.anchoredPosition = new Vector3(0, 0, 0);
    }

    bool IsOffScreen()
    {
        Vector2 vec = Camera.main.WorldToViewportPoint(transform.position);
        if (vec.x >= 0 && vec.x <= 1 && vec.y >= 0 && vec.y <= 1)
        {
            indicatorPref.SetActive(false);
            return false;
        }
        else
        {
            indicatorPref.SetActive(true);
            return true;
        }
    }
}
