using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] protected int _level = 1;
    [SerializeField] protected int _exp;
    [SerializeField] protected int _hp = 3;
    [SerializeField] protected int _maxHp = 3;
    [SerializeField] protected int _gold;
    [SerializeField] protected int _wood;
    [SerializeField] protected int _rock;
    [SerializeField] protected int _cotton;
    [SerializeField] protected int _stage = 1;
    [SerializeField] protected float _moveSpeed = 5;
    [SerializeField] protected float _sightRange;

    public int Level
    {
        get { return _level; }
        set
        {
            _level = value;

        }
    }
    public int Exp 
    { 
        get { return _exp; } 
        set
        {
            _exp = value;

            int level = Level;
            while(true)
            {
                LevelExpData levelExpData;
                if (Managers.Data.LevelExps.TryGetValue(level + 1, out levelExpData) == false)
                    break;
                if (_exp < levelExpData.TotalExp)
                {
                    (Managers.UI.SceneUI as UI_GameScene).SetExpBar(_exp, levelExpData.TotalExp);
                    break;
                }
                    
                level++;
                _exp = 0;
            }

            if (level != Level)
                Level = level;
        } 
    }
    public int Hp
    {
        get { return _hp; } 
        set 
        {
            if (_hp < 0)
                _hp = 0;

            _hp = value;

            if (_hp > _maxHp)
                _hp = _maxHp;

            (Managers.UI.SceneUI as UI_GameScene).SetHeartUI(_hp);
        } 
    }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Gold { get { return _gold; } set { _gold = value; } }
    public int Wood { get { return _wood; } set { _wood = value; } }
    public int Rock { get { return _rock; } set { _rock = value; } }
    public int Cotton { get { return _cotton; } set { _cotton = value; } }
    public int Stage { get { return _stage; } set { _stage = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }
    public float SightRange { get { return _sightRange; } set { _sightRange = value; } }

    void Start()
    {
        // Init
    }
}
