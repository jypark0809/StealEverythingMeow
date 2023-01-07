using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SomSomPreivew : MonoBehaviour
{
    private bool IsActive = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private RaycastHit hitInfo;
    private LayerMask layerMask;
    [SerializeField]
    private Vector3 mousPos;

    void Update()
    {
        if(!IsActive)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit ,LayerMask.GetMask("Cat")))
            { 
                Vector3 _location = new Vector3(hit.point.x, hit.point.y, 0) ;
                this.transform.position = _location;

                if(Input.GetMouseButtonUp(0))
                {
                    GameObject go = Managers.Resource.Instantiate("Somsom", Managers.Object.CatHouse.transform);
                    go.transform.position = this.transform.position;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
