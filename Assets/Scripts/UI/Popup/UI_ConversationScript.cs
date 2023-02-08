using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ConversationScript : UI_Popup
{
    public Sprite[] _cutScenes;
    List<Script> _scripts;
    int _scriptIndex = 0;

    bool _isType;
    public int _charPerSecend;
    float _interval;
    int _index;
    string _curScriptLine;

    struct Script
    {
        public string name;
        public bool hasName;
        public int cutSceneIndex;
        public string script;

        public Script(string name, bool hasName, int cutSceneIndex, string script)
        {
            this.name = name;
            this.hasName = hasName;
            this.cutSceneIndex = cutSceneIndex;
            this.script = script;
        }
    }

    enum Texts
    {
        ScriptText,
        NameText
    }

    enum Images
    {
        Blocker,
        CutScene,
        ScriptPanel,
        NamePanel,
        CursorImage
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Managers.Sound.Play(Define.Sound.Bgm, "BGM/StoryBgm_Sad", volume: 0.1f);

        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        _scripts = InitScripts();
        _interval = 1.0f / _charPerSecend;

        GetImage((int)Images.Blocker).gameObject.BindEvent(OnScreenClicked);
        GetImage((int)Images.CutScene).gameObject.BindEvent(OnScreenClicked);
        GetImage((int)Images.ScriptPanel).gameObject.BindEvent(OnScreenClicked);

        GetImage((int)Images.CutScene).sprite = _cutScenes[_scriptIndex];
        GetText((int)Texts.NameText).text = _scripts[_scriptIndex].name;

        SetLine(_scripts[_scriptIndex].script);
    }

    void OnScreenClicked(PointerEventData data)
    {
        Managers.Sound.Play(Define.Sound.Effect, "Effects/UI_Click");

        if (_isType == false)
            _scriptIndex++;

        if (_scriptIndex < _scripts.Count)
        {
            GetText((int)Texts.NameText).text = _scripts[_scriptIndex].name;
            SetLine(_scripts[_scriptIndex].script);
        }
        else
        {
            // Read All Scripts
            Managers.Sound.Play(Define.Sound.Bgm, "BGM/BGM_Home", volume: 0.1f);
            ClosePopupUI();
            Managers.UI.ShowPopupUI<UI_FindHelp>();
        }
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
            GetImage((int)Images.CutScene).sprite = _cutScenes[_scripts[_scriptIndex].cutSceneIndex];
            GetImage((int)Images.NamePanel).gameObject.SetActive(_scripts[_scriptIndex].hasName);

            if (_scriptIndex == 3)
                Managers.Sound.Play(Define.Sound.Bgm, "BGM/StoryBgm_Happy", volume: 0.1f);

            _curScriptLine = script;
            StartCoroutine(StartTyping());
        }
    }

    IEnumerator StartTyping()
    {
        GetText((int)Texts.ScriptText).text = "";
        _index = 0;
        GetImage((int)Images.CursorImage).gameObject.SetActive(false);
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
        GetImage((int)Images.CursorImage).gameObject.SetActive(true);
        _isType = false;
    }

    List<Script> InitScripts()
    {
        List<Script> scripts = new List<Script>();
        scripts.Add(new Script("전 주인", true, 0, "아기 고양이일 때는 예뻤는데\n갈수록 못생겨지네..\n슬슬 새로운 애를 데려와 볼까?"));
        scripts.Add(new Script("", false, 1, "어느 추운 겨울날,\n\n차도에 버려진 하양이는 \n떠나간 차만 하염없이 바라보는데..."));
        scripts.Add(new Script("하양이", true, 2, "춥고.. 배고파..\n나 이제 어떻게 살지...?"));
        scripts.Add(new Script("", false, 3, "인적이 드문 뒷산 인근,\n\n텅 빈 집을 발견한 하양이"));
        scripts.Add(new Script("하양이", true, 3, "이 집도 나처럼 버려진 집인가..?\n여기서 제 2의 묘생을 시작하는 거야!"));
        scripts.Add(new Script("", false, 4, "낯설고 허름한 새로운 집,\n\n떠돌이 고양이들이\n하나둘씩 모이게 되었어요."));
        scripts.Add(new Script("하양이", true, 4, "너희들도 나와 같은 처지인 거야?\n나랑 함께 할래?"));
        scripts.Add(new Script("하양이", true, 5, "두고 보자... 다 훔치고\n부숴버릴 테다...\n시작은 이 집이야!"));
        scripts.Add(new Script("", false, 5, "고양이들은 먹고 살기 위해,\n\n버려진 지난 날의 복수를 위해,\n\n인간들의 집을 털기로 합니다."));
        scripts.Add(new Script("", false, 5, "오늘부터 공범이야, 우리\n\n<다훔칠고양>"));

        return scripts;
    }    
}
