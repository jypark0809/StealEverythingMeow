using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_HappinessStory : UI_Popup
{
    private string[] CatName = { "White", "Black", "Calico", "Tabby", "Gray" };

    int Index;
    int count = 0;

    int _index;
    bool _isType;

    string _curScriptLine;
    float _interval;
    public int _charPerSecend = 15;
    enum GameObjects
    {
        ScriptPanel

    }

    enum Buttons
    {

    }
    enum Texts
    {
        ScriptText
    }

    enum Images
    {
        HappyImages,
        GiftBox,
        OpenLetter,
        CloseLetter,
        CatImages,
    }
    void Start()
    {
        Init();
    }
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        _interval = 1.0f / _charPerSecend;

        GetImage((int)Images.GiftBox).gameObject.SetActive(false);
        GetObject((int)GameObjects.ScriptPanel).BindEvent(NextIndex);
        GetObject((int)GameObjects.ScriptPanel).SetActive(false);

        GetImage((int)Images.CatImages).sprite = Resources.Load<Sprite>(("Sprites/Nyan/" + CatName[Index] + "/" + CatName[Index] + "_Walk1"));
        GetImage((int)Images.CatImages).gameObject.SetActive(false);

        GetImage((int)Images.OpenLetter).gameObject.SetActive(false);
        GetImage((int)Images.CloseLetter).gameObject.BindEvent(Startevent);



        Managers.Sound.Play(Define.Sound.Bgm, "BGM/BGM_HappyStory", volume: 0.4f);

    }

    private void Startevent(PointerEventData data)
    {
        GetImage((int)Images.CloseLetter).gameObject.SetActive(false);
        GetImage((int)Images.OpenLetter).gameObject.SetActive(true);
        GetImage((int)Images.CatImages).gameObject.SetActive(true);
        GetObject((int)GameObjects.ScriptPanel).SetActive(true);
        SetLine(Managers.Data.CatBooks[1401 + Index].End_Story1);

    }
    private void NextIndex(PointerEventData data)
    {
        if (!_isType)
        {
            count++;
            switch (count)
            {
                case 1:
                    GetImage((int)Images.CatImages).gameObject.SetActive(false);
                    GetImage((int)Images.OpenLetter).gameObject.SetActive(false);
                    _curScriptLine = Managers.Data.CatBooks[1401 + Index].End_Story2;
                    SetLine(Managers.Data.CatBooks[1401 + Index].End_Story2);
                    GetImage((int)Images.HappyImages).sprite = Resources.Load<Sprite>(("Sprites/HappyStory/하양이/" + Managers.Data.CatBooks[1401 + Index].End_Story2_Img));
                    break;
                case 2:
                    _curScriptLine = Managers.Data.CatBooks[1401 + Index].End_Story3;
                    SetLine(Managers.Data.CatBooks[1401 + Index].End_Story3);
                    GetImage((int)Images.HappyImages).sprite = Resources.Load<Sprite>(("Sprites/HappyStory/하양이/" + Managers.Data.CatBooks[1401 + Index].End_Story3_Img));
                    break;
                case 3:
                    _curScriptLine = Managers.Data.CatBooks[1401 + Index].End_Story4;
                    SetLine(Managers.Data.CatBooks[1401 + Index].End_Story4);
                    GetImage((int)Images.HappyImages).sprite = Resources.Load<Sprite>(("Sprites/HappyStory/하양이/" + Managers.Data.CatBooks[1401 + Index].End_Story4_Img));
                    break;
                case 4:
                    _curScriptLine = Managers.Data.CatBooks[1401 + Index].End_Story5;
                    SetLine(Managers.Data.CatBooks[1401 + Index].End_Story5);
                    break;
                case 5:
                    if (!Managers.Game.SaveData.IsViewStory[Index])
                    {
                        Managers.Sound.Play(Define.Sound.Effect, "Effects/GiftSound", volume: 0.4f);
                        GetImage((int)Images.GiftBox).gameObject.SetActive(true);
                        GetImage((int)Images.GiftBox).gameObject.BindEvent(OpenGift);
                    }
                    else
                    {
                        Managers.UI.ClosePopupUI();
                        Managers.Sound.Play(Define.Sound.Bgm, "BGM/BGM_Home", volume: 0.4f);
                    }
                    break;
            }
        }
        else
        {
            SetLine(_curScriptLine);
        }
    }

    private void OpenGift(PointerEventData data)
    {
        Managers.UI.ShowPopupUI<UI_HappinessEndRwd>().Setinfo(Managers.Data.CatBooks[1401 + Index].End_Reward1, Managers.Data.CatBooks[1401 + Index].End_Reward2, Index);
    }
    void SetLine(string script)
    {
        if (_isType)
        {
            StopCoroutine(Typing());
            GetText((int)Texts.ScriptText).text = _curScriptLine;
            EndTyping();
        }
        else
        {
            _curScriptLine = script;
            StartCoroutine(StartTyping());
        }
    }
    IEnumerator StartTyping()
    {
        GetText((int)Texts.ScriptText).text = "";
        _index = 0;
        _isType = true;

        yield return new WaitForSeconds(_interval);
        StartCoroutine(Typing());
    }
    IEnumerator Typing()
    {
        if (GetText((int)Texts.ScriptText).text == _curScriptLine)
        {
            EndTyping();
            yield break;
        }

        GetText((int)Texts.ScriptText).text += _curScriptLine[_index];

        //Sound
        if (_curScriptLine[_index] != ' ' || _curScriptLine[_index] != '.')
            Managers.Sound.Play(Define.Sound.Effect, "Effects/Typing");

        _index++;

        yield return new WaitForSeconds(_interval);
        StartCoroutine(Typing());
    }
    void EndTyping()
    {
        _isType = false;
    }
    public void SetInfo(int _index)
    {
        Index = _index;
    }
}
