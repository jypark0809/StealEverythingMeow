using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Lobby : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;

    public float _Speed;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        StartCoroutine(Move());
    }

    private void Update()
    {

    }

    IEnumerator Move(float x = 0, float y = 0)
    {
        float InputX = x; //Input.GetAxisRaw("Horizontal");
        float InputY = y;//Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(InputX, InputY) * Time.deltaTime * _Speed);
        if (InputX != 0 || InputY != 0)
            anim.SetBool("walk", true);
        else
            anim.SetBool("walk", false);

        anim.SetFloat("dirX", InputX);
        anim.SetFloat("dirY", InputY);
        yield return new WaitForSeconds(1f);


        int a = Random.Range(-2, 2);
        int b = Random.Range(-2, 2);
        StartCoroutine(Move(a, b));
    }
}
