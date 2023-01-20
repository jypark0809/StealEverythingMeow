using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_NeedTreasureMapPopup : UI_Popup
{
    float time;

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
        while (GetImage((int)Images.Panel).color.a < 0.7f)
        {
            time += Time.deltaTime;
            imageAlpha.a = Mathf.Lerp(0, 0.7f, time);
            textAlpha.a = Mathf.Lerp(0, 0.7f, time);
            GetImage((int)Images.Panel).color = imageAlpha;
            GetText((int)Texts.NoticeText).color = textAlpha;
            yield return null;
        }
        time = 0;

        yield return new WaitForSeconds(3f);

        while (GetImage((int)Images.Panel).color.a > 0f)
        {
            time += Time.deltaTime;
            imageAlpha.a = Mathf.Lerp(0.7f, 0, time);
            textAlpha.a = Mathf.Lerp(0.7f, 0, time);
            GetImage((int)Images.Panel).color = imageAlpha;
            GetText((int)Texts.NoticeText).color = textAlpha;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        Managers.UI.ClosePopupUI();
    }
}
