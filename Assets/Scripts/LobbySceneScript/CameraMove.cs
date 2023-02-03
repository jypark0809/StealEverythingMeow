using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.EventSystems;
public class CameraMove : MonoBehaviour
{
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

    private float Movespeed = 2f;
    public Vector3 targetPos;

    private void Awake()
    {
        thecamera = GetComponent<Camera>();
        pix = GetComponent<PixelPerfectCamera>();

        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
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
    private void ZoomIn()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize += scrollWheel * Time.deltaTime * scrollWheel;
    }

    private void FixedUpdate()
    {
        Moving();
        LimitCameraArea();
    }
    Vector2 clickPoint;
    private void Moving()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0))
                clickPoint = Input.mousePosition;

            if (Input.GetMouseButton(0))
            {
                Vector3 position
                    = Camera.main.ScreenToViewportPoint((Vector2)Input.mousePosition - clickPoint);

                Vector3 move = -position * (Time.deltaTime * 30f);

                transform.Translate(move);
                transform.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
            }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center[Index], mapsize[Index] * 2);
    }
}
