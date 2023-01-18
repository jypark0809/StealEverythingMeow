using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    public Vector3 target;
    public float _speed;
    public void Update()
    {
        transform.position =  Vector3.Lerp(Camera.main.transform.position, target, Time.deltaTime* _speed);
    }
 
}
