using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.EventSystems;
public class CameraTest : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed;
    //[SerializeField] private float cameraZoomSpeed = 0.1f;

    [SerializeField]
    Vector2 mapsize;
    [SerializeField]
    Vector2 center;

    private Vector3 beginMousePos = Vector3.zero;
    private Vector3 preMousePos = Vector3.zero;
    private Vector3 beginCamPos = Vector3.zero;


    public bool IsMove;



    Camera thecamera;
    PixelPerfectCamera pix;

    Vector2 prePos;

    private float dragspeed = 2f;
    public Vector3 targetPos;
    public int _zoom;

    float height;
    float width;

    private bool dragmove;
    private void Awake()
    {
        thecamera = GetComponent<Camera>();
        pix = GetComponent<PixelPerfectCamera>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }
    public void Update()
    {
        if (IsMove)
        {
            transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * dragspeed);
        }
    }
    private void FixedUpdate()
    {
        if(!IsMove)
        {
            CameraMove();
        }
        LimitCameraArea();
    }

    void CameraMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                beginMousePos = Input.mousePosition;
                beginCamPos = transform.position;
            }
            else
                return;
        }
        else if (Input.GetMouseButton(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                preMousePos = -(Input.mousePosition - beginMousePos) * cameraMoveSpeed;
                Vector3 newCamPos = beginCamPos + preMousePos;
                transform.position = newCamPos;
            }
        }
    }
    void LimitCameraArea()
    {
        float Lx = mapsize.x - width;
        float clampX = Mathf.Clamp(transform.position.x, -Lx + center.x, Lx + center.x);

        float Ly = mapsize.y - height;
        float clampY = Mathf.Clamp(transform.position.y, -Ly + center.y, Ly + center.y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, mapsize * 2);
    }
}
