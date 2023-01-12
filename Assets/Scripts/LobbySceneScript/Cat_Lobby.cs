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

    int index = 0;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (thegame.FinalNodeList.Count != 0)
        {
            MovePath();
        }

    }
    public void MovePath()
    {
        int InputX = thegame.FinalNodeList[index].x; //- (int)transform.position.x;
        int InputY = thegame.FinalNodeList[index].y;// - (int)transform.position.y;
        Vector2 targetPos = new Vector2(InputX, InputY);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, _Speed * Time.deltaTime);
        if((transform.position.x == targetPos.x && transform.position.y == targetPos.y))
        {
            index++;
        }
        else
        {
            float dirX = thegame.FinalNodeList[index].x - transform.position.x;
            float dirY = thegame.FinalNodeList[index].y - transform.position.y;
            anim.SetBool("walk", true);
            anim.SetFloat("dirX", dirX);
            anim.SetFloat("dirY", dirY);
        }

        if (index == thegame.FinalNodeList.Count)
        {
            index = 0;
            thegame.FinalNodeList.Clear();
            anim.SetBool("walk", false);
            anim.SetFloat("dirX", 0);
            anim.SetFloat("dirY", -1f);
            IsMove = false;
        }    
           
    }
    IEnumerator Move(int x = 0, int y = 0)
    {
        IsMove = true;
        for (int i = 0; i < thegame.FinalNodeList.Count; i++)
        {
            int InputX = thegame.FinalNodeList[i].x; //- (int)transform.position.x;
            int InputY = thegame.FinalNodeList[i].y;// - (int)transform.position.y;
            Vector2 targetPos = new Vector2(InputX, InputY);
            while (transform.position.x != targetPos.x && transform.position.y != targetPos.y)
            {
                //transform.position = Vector2.MoveTowards(this.transform.position,new Vector2( InputX,InputY), _Speed);
                //transform.Translate(new Vector2(InputX, InputY) * Time.deltaTime * _Speed);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(2f); 
            curWalk = 0;
        }
        yield return new WaitForSeconds(1f);
    }
}
    /*
    public void Moasd()
    {
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
    */

