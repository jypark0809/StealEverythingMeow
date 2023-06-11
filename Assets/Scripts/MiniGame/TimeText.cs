using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeText : MonoBehaviour
{
    TextMeshPro _timeText;
    SpriteRenderer _sp;
    Color _alpha;

    public void SetInfo(Vector2 pos)
    {
        _timeText = GetComponent<TextMeshPro>();
        _sp = GetComponentInChildren<SpriteRenderer>();
        transform.position = pos;
        _alpha = new Color(1, 1, 1, 1);
        StartCoroutine(FloatTimeText());
    }

    void Start()
    {
        _alpha = _timeText.color;
    }

    IEnumerator FloatTimeText()
    {
        float timer = 1;
        while (timer > 0)
        {
            transform.Translate(new Vector2(0, 2f * Time.deltaTime));

            _alpha.a = Mathf.Lerp(_alpha.a, 0, Time.deltaTime * 2f);
            _timeText.color = _alpha;
            _sp.color = _alpha;

            timer -= Time.deltaTime;
            yield return null;
        }

        Managers.Resource.Destroy(gameObject);
    }
}
