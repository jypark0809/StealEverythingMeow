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

    private float Movespeed = 2f;
    public Vector3 targetPos;

    float height;
    float width;

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
            transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * Movespeed);


    }
    private Vector3 vecl;
    private void FixedUpdate()
    {
        if(!IsMove)
            CameraMove();
        LimitCameraArea();
    }

    private Vector2 nowPos, prePos;
    private Vector2 movePosDiff;
    private Vector2 GetTouchDragValue()
    {
        movePosDiff = Vector3.zero;

        if (Input.touchCount == 1)
        {
            Touch Touch = Input.GetTouch(0);
            if (Touch.phase == TouchPhase.Began)
            {
                prePos = Touch.position - Touch.deltaPosition;
            }
            else if (Touch.phase == TouchPhase.Moved)
            {
                nowPos = Touch.position - Touch.deltaPosition;
                movePosDiff = (Vector2)(prePos - nowPos) * Time.deltaTime * cameraMoveSpeed;
                vecl = -movePosDiff.normalized;
                prePos = Touch.position - Touch.deltaPosition;
                transform.position = Vector3.SmoothDamp(this.transform.position, ((Vector3)(GetTouchDragValue()) + this.transform.position), ref vecl, cameraMoveSpeed);
            }
        }
        return movePosDiff;
    }
    void CameraMove()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {

            if (Input.GetMouseButtonDown(0))
            {
                beginMousePos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 position = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - (Vector2)beginMousePos);
                Vector3 Move = -position * (Time.deltaTime * cameraMoveSpeed);
                vecl = Move.normalized;
                transform.Translate(this.transform.position + Move);
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
