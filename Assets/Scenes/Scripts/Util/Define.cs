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
        Move,
        Attack,
        Die,
    }

    public enum Layer
    {
        //Monster = 8,
        //Ground = 9,
        //Block = 10,
    }

    public enum Scene
    {
        Unknown,
        Game,
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
    }
}
