using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    float _speed = 5;

    [SerializeField]
    float CHASE_TIME = 5;

    Rigidbody2D _rigid;
    Animator _anim;
    SpriteRenderer _spriteRenderer;

    [SerializeField]
    List<Transform> _transformList;

    Transform _player;
    GameObject _sightScope;

    public enum EnemyState
    {
        Patrol,
        Attack,
        Idle,
    }

    [SerializeField]
    EnemyState _state = EnemyState.Patrol;
    public virtual EnemyState State
    {
        get { return _state; }
        set
        {
            _state = value;
            switch (_state)
            {
                case EnemyState.Patrol:
                    _anim.Play("Run");
                    break;
                case EnemyState.Attack:
                    _anim.Play("Run");
                    break;
                case EnemyState.Idle:
                    _anim.Play("Idle");
                    break;
            }
        }
    }

    void Start()
    {
        transform.position = _transformList[index].position;
        _player = Managers.Object.Player.transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();

        _attackTimer = CHASE_TIME;
        _sightScope = Util.FindChild(gameObject, "SightScopeContainer", true);
    }

    void FixedUpdate()
    {
        switch (_state)
        {
            case EnemyState.Patrol:
                UpdatePatrol();
                break;
            case EnemyState.Attack:
                UpdateAttack();
                break;
            case EnemyState.Idle:
                UpdateIdle();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(ChangePlayerState());
            Vibration.Vibrate((long)50);
            Managers.Sound.Play(Define.Sound.Effect, "Effects/CatCry", volume: 0.4f);
            State = EnemyState.Idle;
        }
    }

    void UpdatePatrol()
    {
        SetCircularSector();
        Patrol();
    }

    float _attackTimer;
    void UpdateAttack()
    {
        if (_player.gameObject.layer == 28)
        {
            State = EnemyState.Idle;
            _attackTimer = CHASE_TIME;
        }

        _attackTimer -= Time.deltaTime;

        if (_attackTimer < 0)
        {
            State = EnemyState.Idle;
            _attackTimer = CHASE_TIME;
        }

        Vector3 dir = _player.position - transform.position;
        lookDir = dir.normalized;
        _rigid.MovePosition(transform.position + lookDir * _speed * Time.deltaTime);
    }

    float _timer = 0;
    void UpdateIdle()
    {
        _timer += Time.deltaTime;
        if (_timer > 2)
        {
            _timer = 0;

            // Respawn
            transform.position = _transformList[0].position;
            index = 0;
            destPos = Vector3.zero;
            lookDir = Vector3.zero;

            State = EnemyState.Patrol;
        } 
    }
    
    void Patrol()
    {
        if (transform.position == _transformList[index].position)
            setDestination();
        else
        {
            Vector3 dir = destPos - transform.position;
            if (dir.magnitude < 0.1f)
            {
                transform.position = _transformList[index].position;
            }
            _rigid.MovePosition(transform.position + lookDir * _speed * Time.deltaTime);
        }
    }

    [SerializeField]
    float angleRange = 60f;
    [SerializeField]
    float radius = 4f;
    bool isCollide = false;
    bool IsCollide
    {
        get { return isCollide; }
        set
        {
            isCollide = value;
            if (value)
            {
                Managers.Sound.Play(Define.Sound.Effect, "Effects/Detected", volume: 0.4f);
            }
        }
    }

    void SetCircularSector()
    {
        if (_player.gameObject.layer == 28)
            return;

        Vector3 dir = _player.transform.position - transform.position;

        // target과 나 사이의 거리가 radius 보다 작다면
        if (dir.magnitude < radius)
        {
            // 내가 타겟을 바라보는 벡터와 내 정면 벡터를 내적
            float dot = Vector3.Dot(dir.normalized, lookDir);

            // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
            float theta = Mathf.Acos(dot);

            // angleRange와 비교하기 위해 degree로 변환
            float degree = Mathf.Rad2Deg * theta;

            // 시야각 판별
            if (degree <= angleRange / 2f && Managers.Object.Player.gameObject.layer == 29)
            {
                IsCollide = true;
                State = EnemyState.Attack;
            }
                
            else
                IsCollide = false;
        }
        else
            IsCollide = false;
    }

    int index = 0;
    Vector3 destPos;
    Vector3 lookDir;
    void setDestination()
    {
        // Last index point -> First index point
        if(index == _transformList.Count-1)
        {
            lookDir = (_transformList[0].position - _transformList[index].position).normalized;
            destPos = _transformList[0].position;
            index = 0;
        }
        else
        {
            index++;
            destPos = _transformList[index].position;
            lookDir = (_transformList[index].position - _transformList[index-1].position).normalized;
        }

        float Dot = Vector3.Dot(lookDir, Vector3.down);
        float Angle = Mathf.Acos(Dot) * Mathf.Rad2Deg;
        if (lookDir.x > 0)
        {
            _sightScope.transform.localRotation = Quaternion.Euler(0,0,Angle);
        }
        else
        {
            _sightScope.transform.localRotation = Quaternion.Euler(0, 0, -Angle);
        }

    }

    void LateUpdate()
    {
        _spriteRenderer.flipX = (lookDir.x < 0);
    }

    IEnumerator ChangePlayerState()
    {
        Managers.Object.Player.Stat.Hp--;
        Managers.Object.Player.gameObject.layer = 27;
        Managers.Object.Player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        yield return new WaitForSeconds(2f);
        Managers.Object.Player.gameObject.layer = 29;
        Managers.Object.Player.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1f);
    }

    #region OnDrawGizmos
    //Color _red = new Color(1f, 0f, 0f, 0.2f);
    //Color _blue = new Color(0f, 0f, 1f, 0.2f);
    //void OnDrawGizmos()
    //{
        //Handles.color = isCollide ? _red : _blue;
        //Handles.DrawSolidArc(transform.position, new Vector3(0, 0, 1), lookDir, angleRange / 2, radius);
        //Handles.DrawSolidArc(transform.position, new Vector3(0, 0, 1), lookDir, -angleRange / 2, radius);
    //}
    #endregion
}
