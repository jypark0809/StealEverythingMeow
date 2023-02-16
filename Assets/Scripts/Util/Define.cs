using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define : MonoBehaviour
{
    public enum WorldObject
    {
        Unknown,
        Player,
    }

    public enum State
    {
        Idle,
        Walk,
        Die,
        Other,
    }

    public enum Layer
    {
        //Monster = 8,
        //Ground = 9,
        //Block = 10,
    }

    public enum SceneType
    {
        Unknown,
        GameScene,
        CatHouseScene,
        StartScene,
        LoadingScene,
    }

    public enum Sound
    {
        Bgm,
        Effect,
        MaxCount,
    }

    public enum UIEvent
    {
        Click,
        PointerDown,
        PointerUp,
        BeginDrag,
        Drag,
        EndDrag
    }

    public enum ShopPurchaseType
    {
        Gold,
        Diamond,
        Cash,
        Ads,
    }

    public enum SnackType
    {
        CatnipCandy,
        Churu,
        Mackerel,
        Jerky,
        Tuna,
        Salmon
    }

    public enum CatType
    {
        White,
        Black,
        Calico,
        Tabby,
        Gray,
        MaxCount
    }
    public enum CatEmotion
    {
        Blink,
        Dig,
        Ennui,
        Fly,
        Lick,
        Paw,
        Relax,
        Scratch, 
        Sleep1, 
        Sleep2,
        Sleep3,
        Sniff,
        Stretch,
        Sway, 
        Tail, 
        Attack
    }


    public const int MOTION_COUNT = 16;
}
