using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyRechargeManager : MonoBehaviour
{
    const int JELLY_MAX_COUNT = 5;
    const int RECHARGE_INTERVAL = 5; // sec
    Coroutine rechargeTimerCoroutine = null;
    int remainedTime = 0;

    private void Update()
    {
        // Debug.Log($"Jelly : {Managers.Game.Jelly}");
        // Debug.Log($"LastQuitTime : {DateTime.FromBinary(Convert.ToInt64(Managers.Game.LastQuitTime))}");
    }

    void Start()
    {
        Init();
    }

    void Init()
    {
        
    }

    public void SaveLastQuitTime()
    {
        Managers.Game.LastQuitTime = DateTime.Now.ToBinary();
        Managers.Game.SaveGame();
    }

    public void SetRechargeScheduler(Action onFinish = null)
    {
        DateTime lastQuitTime = DateTime.FromBinary(Convert.ToInt64(Managers.Game.LastQuitTime));
        int timeDifferenceInSec = (int)((DateTime.Now - lastQuitTime).TotalSeconds);
        int jellyToAdd = timeDifferenceInSec / RECHARGE_INTERVAL;
        int remainedTime = timeDifferenceInSec % RECHARGE_INTERVAL;
        if (Managers.Game.Jelly < JELLY_MAX_COUNT)
        {
            Managers.Game.Jelly += jellyToAdd;
            if (Managers.Game.Jelly > JELLY_MAX_COUNT)
                Managers.Game.Jelly = JELLY_MAX_COUNT;
            else
                rechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(remainedTime, onFinish));
        }
    }

    IEnumerator DoRechargeTimer(int inputTime, Action onFinish = null)
    {
        if (inputTime <= 0)
        {
            remainedTime = RECHARGE_INTERVAL;
        }
        else
        {
            remainedTime = inputTime;
        }

        while (remainedTime > 0)
        {
            remainedTime -= 1;
            yield return new WaitForSeconds(1f);
        }

        if (Managers.Game.Jelly < JELLY_MAX_COUNT)
        {
            // Managers.Game.Jelly = JELLY_MAX_COUNT;
            Managers.Game.Jelly++;
            if (Managers.Game.Jelly < JELLY_MAX_COUNT)
                rechargeTimerCoroutine = StartCoroutine(DoRechargeTimer(RECHARGE_INTERVAL, onFinish));
        }
        else
        {
            remainedTime = 0;
            rechargeTimerCoroutine = null;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            SetRechargeScheduler();
        }
    }

    void OnApplicationQuit()
    {
        SaveLastQuitTime();
    }
}
