using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//마우스 클릭이 됬다면
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))//레이케스트(마우스 클릭위치,레이케스트가 맞은곳,무한대기)
            {
                this.transform.position = hit.point;//플레이어의 위치를 레이케스트가 맞은곳으로 이동  

            }
        }
    }
}
