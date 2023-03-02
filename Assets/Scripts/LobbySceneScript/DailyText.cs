using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyText : MonoBehaviour
{
    TextMeshPro _GoldText;
    Color _alpha;

    public void SetInfo(Vector2 pos, int gold)
    {
        _GoldText = GetComponent<TextMeshPro>();
        transform.position = new Vector3(pos.x, pos.y, -4);
        _GoldText.text = $"+{gold}";
        _alpha = new Color(1, 1, 1, 1);
        StartCoroutine(FloatGoldText());
    }

    void Start()
    {
        _alpha = _GoldText.color;
    }

    IEnumerator FloatGoldText()
    {
        float timer = 1;
        while (timer > 0)
        {
            transform.Translate(new Vector2(0, 2f * Time.deltaTime));

            _alpha.a = Mathf.Lerp(_alpha.a, 0, Time.deltaTime * 3f);
            _GoldText.color = _alpha;

            timer -= Time.deltaTime;
            yield return null;
        }

        Managers.Resource.Destroy(gameObject);
    }
}
