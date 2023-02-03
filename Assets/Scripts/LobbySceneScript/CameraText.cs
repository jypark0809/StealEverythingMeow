using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var ray = Camera.main.ScreenPointToRay(pos);
        RaycastHit hit;
        Debug.DrawRay(pos, transform.forward * 1000, Color.blue);
        if (Physics.Raycast(pos, transform.forward, out hit, 100f , LayerMask.GetMask("Cat")))
        {
            Debug.Log("½ÇÇà");
        }
    }
}
