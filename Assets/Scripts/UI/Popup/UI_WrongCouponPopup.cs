using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_WrongCouponPopup : UI_Popup
{
    float time;
    const float speed = 3;

    enum Images
    {
        Panel,
    }

    enum Texts
    {
        NoticeText,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));
        Bind<TextMeshProUGUI>(typeof(Texts));

        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        Color imageAlpha = GetImage((int)Images.Panel).color;
        Color textAlpha = GetText((int)Texts.NoticeText).color;
        while (GetImage((int)Images.Panel).color.a < 1f)
        {
            time += Time.deltaTime * speed;
            imageAlpha.a = Mathf.Lerp(0, 1f, time);
            textAlpha.a = Mathf.Lerp(0, 1f, time);
            GetImage((int)Images.Panel).color = imageAlpha;
            GetText((int)Texts.NoticeText).color = textAlpha;
            yield return null;
        }
        time = 0;

        yield return new WaitForSeconds(1f);

        while (GetImage((int)Images.Panel).color.a > 0f)
        {
            time += Time.deltaTime * speed;
            imageAlpha.a = Mathf.Lerp(1f, 0, time);
            textAlpha.a = Mathf.Lerp(1f, 0, time);
            GetImage((int)Images.Panel).color = imageAlpha;
            GetText((int)Texts.NoticeText).color = textAlpha;
            yield return null;
        }

        Managers.UI.ClosePopupUI();
    }
}
