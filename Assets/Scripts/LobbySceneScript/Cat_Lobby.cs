using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Lobby : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;

    public float _Speed;
    private bool IsMove;

    int curWalk = 0;
    int WalkCount = 16;



    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        StartCoroutine(Move());
    }

    private void Update()
    {
        if(!IsMove)
        {
            StartCoroutine(Move());
        }
    }
    IEnumerator Move(float x = 0, float y = 0)
    {
        IsMove = true;
        float InputX = x;
        float InputY = y;
        if (x != 0 || y != 0)
        {
            while (curWalk < WalkCount)
            {
                anim.SetBool("walk", true);
                anim.SetFloat("dirX", InputX);
                anim.SetFloat("dirY", InputY);
                transform.Translate(new Vector2(InputX, InputY) * Time.deltaTime * _Speed);
                curWalk++;
                yield return new WaitForSeconds(0.01f);
            }
            anim.SetBool("walk", false);
            yield return new WaitForSeconds(2f);
        }
        else
        {
            anim.SetBool("walk", false);
            anim.SetFloat("dirX", InputX);
            anim.SetFloat("dirY", InputY);
            yield return new WaitForSeconds(3f);

        }
        curWalk = 0;
        WalkCount = Random.Range(16, 33);
        int a = Random.Range(-1, 2);
        int b = Random.Range(-1, 2);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Move(a, b));
    }
}

