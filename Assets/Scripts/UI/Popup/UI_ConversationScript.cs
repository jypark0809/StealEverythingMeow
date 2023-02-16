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
        scripts.Add(new Script("�� ����", true, 0, "�Ʊ� ������� ���� �����µ�\n������ ����������..\n���� ���ο� �ָ� ������ ����?"));
        scripts.Add(new Script("", false, 1, "��� �߿� �ܿﳯ,\n\n������ ������ �Ͼ��̴� \n������ ���� �Ͽ����� �ٶ󺸴µ�..."));
        scripts.Add(new Script("�Ͼ���", true, 2, "���.. �����..\n�� ���� ��� ����...?"));
        scripts.Add(new Script("", false, 3, "������ �幮 �޻� �α�,\n\n�� �� ���� �߰��� �Ͼ���"));
        scripts.Add(new Script("�Ͼ���", true, 3, "�� ���� ��ó�� ������ ���ΰ�..?\n���⼭ �� 2�� ������ �����ϴ� �ž�!"));
        scripts.Add(new Script("", false, 4, "������ �㸧�� ���ο� ��,\n\n������ ����̵���\n�ϳ��Ѿ� ���̰� �Ǿ����."));
        scripts.Add(new Script("�Ͼ���", true, 4, "����鵵 ���� ���� ó���� �ž�?\n���� �Բ� �ҷ�?"));
        scripts.Add(new Script("�Ͼ���", true, 5, "�ΰ� ����... �� ��ġ��\n�ν����� �״�...\n������ �� ���̾�!"));
        scripts.Add(new Script("", false, 5, "����̵��� �԰� ��� ����,\n\n������ ���� ���� ������ ����,\n\n�ΰ����� ���� �б�� �մϴ�."));
        scripts.Add(new Script("", false, 5, "���ú��� �����̾�, �츮\n\n<����ĥ���>"));

        return scripts;
    }    
}
