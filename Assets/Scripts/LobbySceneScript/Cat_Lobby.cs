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

    public GameManager thegame;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (thegame.FinalNodeList.Count != 0 && IsMove == false)
        {
            StartCoroutine(Move());
        }
    }
    IEnumerator Move(int x = 0, int y = 0)
    {
        IsMove = true;
        for (int i = 0; i < thegame.FinalNodeList.Count; i++)
        {
            int InputX = thegame.FinalNodeList[i].x;
            int InputY = thegame.FinalNodeList[i].y;
            Debug.Log(InputX + " " + InputY);
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
            yield return new WaitForSeconds(1f);
        }
    }
}

