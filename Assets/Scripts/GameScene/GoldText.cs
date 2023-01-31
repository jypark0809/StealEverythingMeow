using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldText : MonoBehaviour
{
    TextMeshPro _GoldText;
    Coroutine _coroutine = null;
    Color _alpha;

    public void SetInfo(Vector2 pos, int gold)
    {
        _GoldText = GetComponent<TextMeshPro>();
        transform.position = pos;
        _GoldText.text = $"+{gold}G";
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
        while(timer > 0)
        {
            transform.Translate(new Vector2(0, 2f * Time.deltaTime));

            _alpha.a = Mathf.Lerp(_alpha.a, 0, Time.deltaTime * 2f);
            _GoldText.color = _alpha;

            timer -= Time.deltaTime;
            yield return null;
        }

        Managers.Resource.Destroy(gameObject);
    }
}
