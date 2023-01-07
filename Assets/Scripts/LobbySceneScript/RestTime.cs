using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class RestTime : MonoBehaviour
{
    public float resttime = 20f;
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = Mathf.Floor(resttime).ToString();
        resttime -= Time.deltaTime;

        if (resttime <= 0)
            Destroy(this.gameObject);

    }
}
