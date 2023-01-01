using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyControll : MonoBehaviour
{
    public float _speed;
    string[] move = { "UP", "DOWN", "RIGHT", "LEFT" };
    private void Start()
    {
        StartCoroutine(MoveCat("UP"));
    }

    private void Update()
    {
    }

    IEnumerator MoveCat(string _dir)
    {
        switch (_dir)
        {
            case "UP":
                transform.Translate(Vector3.up*_speed);
                break;
            case "DOWN":
                transform.Translate(Vector3.down * _speed); 
                break;
            case "RIGHT":
                transform.Translate(Vector3.right * _speed); 
                break;
            case "LEFT":
                transform.Translate(Vector3.left * _speed); 
                break;
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(MoveCat(move[Random.Range(0,3)]));
    }

}