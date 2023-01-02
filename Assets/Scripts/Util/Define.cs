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
        Drag,
        PointerDown,
        PointerUp,
    }

    public const int MOTION_COUNT = 16;
}
