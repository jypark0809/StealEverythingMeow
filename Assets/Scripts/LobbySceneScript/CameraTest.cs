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
        if (Input.GetMouseButtonDown(0))//���콺 Ŭ���� ��ٸ�
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity))//�����ɽ�Ʈ(���콺 Ŭ����ġ,�����ɽ�Ʈ�� ������,���Ѵ��)
            {
                this.transform.position = hit.point;//�÷��̾��� ��ġ�� �����ɽ�Ʈ�� ���������� �̵�  

            }
        }
    }
}
