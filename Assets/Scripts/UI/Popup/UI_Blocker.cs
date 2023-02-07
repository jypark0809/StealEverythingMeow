using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Blocker : UI_Popup
{
    float time;

    enum Images
    {
        Blocker,
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));

        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut()
    {
        Color imageAlpha = GetImage((int)Images.Blocker).color;
        while (GetImage((int)Images.Blocker).color.a < 1f)
        {
            time += Time.deltaTime;
            imageAlpha.a = Mathf.Lerp(0, 1f, time);
            GetImage((int)Images.Blocker).color = imageAlpha;
            yield return null;
        }
        time = 0;

        (Managers.Scene.CurrentScene as GameScene).Clear();
        (Managers.Scene.CurrentScene as GameScene).GoToNextStage();

        yield return new WaitForSeconds(2f);

        while (GetImage((int)Images.Blocker).color.a > 0f)
        {
            time += Time.deltaTime;
            imageAlpha.a = Mathf.Lerp(1f, 0, time);
            GetImage((int)Images.Blocker).color = imageAlpha;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        Managers.UI.ClosePopupUI();
    }
}
