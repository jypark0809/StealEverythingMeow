using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;

public class CameraMove : MonoBehaviour
{
    int pointerID;

    float height;
    float width;

    [SerializeField]
    Vector2[] mapsize;
    [SerializeField]
    Vector2[] center;
    [SerializeField]
    Vector2[] RoomPos;


    public int Index;
    public bool IsMove;

    Camera thecamera;
    PixelPerfectCamera pix;

    public float Movespeed;
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
    private void Update()
    {

    }
    private void FixedUpdate()
    {
        if (IsMove)
            transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * 3f);
        else
            Moving();

        LimitCameraArea();
    }
    Vector2 clickPoint;
    private void Moving()
    {
        if (!IsPointerOverUIObject(Input.mousePosition))
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
    public void Exam(GameObject go)
    {
        StartCoroutine(TargetMove(go));
    }
    public IEnumerator TargetMove(GameObject _go)
    {
        Vector3 curPos = _go.transform.position;
        if (Managers.Game.SaveData.SpaceLevel > 2 && Managers.Game.SaveData.SpaceLevel < 10)
        {
            targetPos = RoomPos[Managers.Game.SaveData.SpaceLevel - 2];
        }
        else if (Managers.Game.SaveData.SpaceLevel == 2 || Managers.Game.SaveData.SpaceLevel == 10)
        {
            targetPos = _go.transform.position; // 거실가구 보여주는법 해결하기
        }
        _go.transform.position = new Vector3(0, -50f, 0);
        IsMove = true;
        yield return new WaitForSeconds(2f);
        _go.transform.position = curPos;
        IsMove = false;
        pix.enabled = true;
        Managers.Sound.Play(Define.Sound.Effect, "Effects/OpenFurniture", volume: 0.4f);

        if(Managers.Game.SaveData.SpaceLevel == 10 && Managers.Game.SaveData.FList.Count == 43)
        {
            yield return new WaitForSeconds(1f);
            Managers.UI.ShowPopupUI<UI_Ending>();
            Managers.Resource.Instantiate("UI/UI_Effect");
        }

    }
    IEnumerator lerpCorotuine(float b = 20, float Time1 =3f)
    {
        pix.enabled = false;
        float elasedTime = 0.0f;
        float start = Camera.main.orthographicSize;
        while(elasedTime < Time1)
        {
            elasedTime += (Time.deltaTime);
            Camera.main.orthographicSize = Mathf.Lerp(start, b, elasedTime / Time1);

            yield return null;
        }
        yield return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center[Index], mapsize[Index] * 2);
    }

    public bool IsPointerOverUIObject(Vector2 touchPos)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = touchPos;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
