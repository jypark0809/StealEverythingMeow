using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    // singleton
    static Managers s_instance = null;
    static Managers Instance { get { Init(); return s_instance; } }

    AdsManager _ads = new AdsManager();
    DataManager _data = new DataManager();
    GameManagerEx _game = new GameManagerEx();
    IAPManager _iap = new IAPManager();
    ObjectManager _object = new ObjectManager();
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static AdsManager Ads { get { return Instance?._ads; } }
    public static DataManager Data { get { return Instance?._data; } }
    public static GameManagerEx Game { get { return Instance?._game; } }
    public static IAPManager IAP { get { return Instance?._iap; } }
    public static ObjectManager Object { get { return Instance?._object; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static SceneManagerEx Scene { get { return Instance?._scene; } }
    public static SoundManager Sound { get { return Instance?._sound; } }
    public static UIManager UI { get { return Instance?._ui; } }


    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._ads.Init();
            s_instance._iap.Init();
            s_instance._data.Init();
            s_instance._pool.Init();
            s_instance._resource.Init();
            s_instance._scene.Init();
            s_instance._sound.Init();

            Application.targetFrameRate = 60;
        }
    }

    public static void Clear()
    {
        Sound.Clear();
        Scene.Clear();
        UI.Clear();
        Pool.Clear();
    }
}