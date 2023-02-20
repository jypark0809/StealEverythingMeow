using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.EventSystems;
public class CameraMove : MonoBehaviour
{
    int pointerID;

    float height;
    float width;

    [SerializeField]
    Vector2[] mapsize;
    [SerializeField]
    Vector2[] center;

    public int Index;
    public bool IsMove;

    Camera thecamera;
    PixelPerfectCamera pix;

    public float Movespeed = 2f;
    public Vector3 targetPos;

    private void Awake()
    {
        thecamera = GetComponent<Camera>();
        pix = GetComponent<PixelPerfectCamera>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;

#if UNITY_EDITOR
        pointerID = -1; //PC나 유니티 상에서는 -1
#elif UNITY_ANDROID
        pointerID = 0;  // 휴대폰이나 이외에서 터치 상에서는 0 
#endif
    }

    private void Start()
    {
        Index = Managers.Game.SaveData.SoomLevel - 1;
    }
    public void Update()
    {
        if (IsMove)
            transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * Movespeed);
        /*
        if (Input.GetMouseButton(0))
        {
            pix.enabled = false;
            ZoomIn();
        }
        else
        {
            pix.enabled = true;
        }
        */

    }
    private void FixedUpdate()
    {
        Moving();
        LimitCameraArea();
    }
    Vector2 clickPoint;
    private void Moving()
    {
        if (!EventSystem.current.IsPointerOverGameObject(pointerID))
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickPoint = Input.mousePosition;
                Vector2 pos = Camera.main.ScreenToWorldPoint(clickPoint);
                if (Physics2D.Raycast(pos, transform.forward, LayerMask.GetMask("Soom")))
                    return;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(clickPoint);
                if (Physics2D.Raycast(pos, transform.forward, LayerMask.GetMask("Soom")))
                {
                    return;
                }
                Vector3 position = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);
                Vector3 move = -position.normalized * (Time.deltaTime) * Movespeed;
                transform.Translate(move);
                clickPoint = Input.mousePosition;
                transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }
            else if (Input.GetMouseButtonUp(0))
                return;
        }
    }

    private void LimitCameraArea()
    {
        float Lx = mapsize[Index].x - width;
        float clampX = Mathf.Clamp(transform.position.x, -Lx + center[Index].x, Lx + center[Index].x);

        float Ly = mapsize[Index].y - height;
        float clampY = Mathf.Clamp(transform.position.y, -Ly + center[Index].y, Ly + center[Index].y);

        transform.position = new Vector3(clampX, clampY, -10f);
    }

    private void ZoomIn()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize += scrollWheel * Time.deltaTime * scrollWheel;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center[Index], mapsize[Index] * 2);
    }
}
