using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    // G : �������κ��� �̵��ߴ� �Ÿ�, H : |����|+|����| ��ֹ� �����Ͽ� ��ǥ������ �Ÿ�, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }

}
public class Cat_LobbyMove : MonoBehaviour
{
    int pointerID;
    
    private int _indexEmotion;
    private string _curEmotion;

    private string[] BasicEmotion = { "Blink", "Ennui" ,"Sleep3", "Sniff", };

    private bool IsEmotion = false;
    private bool IsSpecialEmotion= false;


    public Vector2Int bottomLeft, topRight;
    private Vector2Int startPos, targetPos;
    public List<Node> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;
    int sizeX, sizeY;

    public Node[,] NodeArray;

    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;
    private bool ReFind = false;

  
    Rigidbody2D rigid;
    Animator anim;

    public float _Speed;
    int index = 0;


    private void Awake()
    {
#if UNITY_EDITOR
        pointerID = -1; //PC�� ����Ƽ �󿡼��� -1
#elif UNITY_ANDROID
        pointerID = 0;  // �޴����̳� �̿ܿ��� ��ġ �󿡼��� 0 
#endif
    }
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Invoke("DoBasicEmotion", 2f);
        StartCoroutine(IsReFind());
    }

    private void Update()
    {
        if (FinalNodeList.Count == 0 && ReFind)
        {
            ReFind = false;
            targetPos = new Vector2Int(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
            PathFinding(this.transform, targetPos);
            StartCoroutine(IsReFind());
            DoBasicEmotion();
        }
        if (FinalNodeList.Count != 0 && !IsEmotion && !IsSpecialEmotion)
        {
            CancelInvoke();
            MovePath();
        }
    }
    IEnumerator IsReFind()
    {
        yield return new WaitForSeconds(10f);
        ReFind = true;
    }
    private void MovePath()
    {
        int InputX = FinalNodeList[index].x;
        int InputY = FinalNodeList[index].y;
        Vector3Int targetNode = new Vector3Int(InputX, InputY,1);
        transform.position = Vector3.MoveTowards(transform.position, targetNode, _Speed * Time.deltaTime);
        if ((transform.position.x == targetNode.x && transform.position.y == targetNode.y))
        {
            index++;
        }
        else
        {
            float dirX = FinalNodeList[index].x - transform.position.x;
            float dirY = FinalNodeList[index].y - transform.position.y;
            anim.SetBool("walk", true);
            anim.SetFloat("dirX", dirX);
            anim.SetFloat("dirY", dirY);
        }
        if (index == FinalNodeList.Count)
        {
            index = 0;
            FinalNodeList.Clear();
            anim.SetBool("walk", false);
            anim.SetFloat("dirX", 0);
            anim.SetFloat("dirY", -1f);
            Managers.Sound.Play(Define.Sound.Effect, "Effects/CatIdle");
        }
    }
    void PathFinding(Transform Catpos, Vector2Int targetPos)
    {
        // NodeArray�� ũ�� �����ְ�, isWall, x, y ����
        sizeX = topRight.x - bottomLeft.x + 1;
        sizeY = topRight.y - bottomLeft.y + 1;
        NodeArray = new Node[sizeX, sizeY];

        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                bool isWall = false;
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.45f))
                    if (col.gameObject.layer == LayerMask.NameToLayer("Wall")) isWall = true;

                NodeArray[i, j] = new Node(isWall, i + bottomLeft.x, j + bottomLeft.y);
            }
        }
        // ���۰� �� ���, ��������Ʈ�� ��������Ʈ, ����������Ʈ �ʱ�ȭ
        StartNode = NodeArray[(int)Catpos.position.x - bottomLeft.x, (int)Catpos.position.y - bottomLeft.y];
        TargetNode = NodeArray[(int)targetPos.x - bottomLeft.x, (int)targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();
        while (OpenList.Count > 0)
        {
            // ��������Ʈ �� ���� F�� �۰� F�� ���ٸ� H�� ���� �� ������� �ϰ� ��������Ʈ���� ��������Ʈ�� �ű��
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // ������
            if (CurNode == TargetNode)
            {
                Node TargetCurNode = TargetNode;
                while (TargetCurNode != StartNode)
                {
                    FinalNodeList.Add(TargetCurNode);
                    TargetCurNode = TargetCurNode.ParentNode;
                }
                FinalNodeList.Add(StartNode);
                FinalNodeList.Reverse();

                //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "��°�� " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                return;
            }
            // �֢آע�
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }
            // �� �� �� ��
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }
    void OpenListAdd(int checkX, int checkY)
    {
        // �����¿� ������ ����� �ʰ�, ���� �ƴϸ鼭, ��������Ʈ�� ���ٸ�
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // �밢�� ����, �� ���̷� ��� �ȵ�
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // �ڳʸ� �������� ���� ������, �̵� �߿� �������� ��ֹ��� ������ �ȵ�
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // �̿���忡 �ְ�, ������ 10, �밢���� 14���
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // �̵������ �̿����G���� �۰ų� �Ǵ� ��������Ʈ�� �̿���尡 ���ٸ� G, H, ParentNode�� ���� �� ��������Ʈ�� �߰�
            if (MoveCost < NeighborNode.G || !OpenList.Contains(NeighborNode))
            {
                NeighborNode.G = MoveCost;
                NeighborNode.H = (Mathf.Abs(NeighborNode.x - TargetNode.x) + Mathf.Abs(NeighborNode.y - TargetNode.y)) * 10;
                NeighborNode.ParentNode = CurNode;

                OpenList.Add(NeighborNode);
            }
        }
    }

    private void OnMouseDown()
    {
        if (!IsPointerOverUIObject(Input.mousePosition))
        {
            SpecialEmotion();
        }
    }
    private void DoBasicEmotion()
    {
        if (IsEmotion)
            return;
        if (IsSpecialEmotion)
            return;
        IsEmotion = true;
        _indexEmotion = Random.Range(0, 4);
        _curEmotion = BasicEmotion[_indexEmotion];
        anim.SetBool(_curEmotion, true);
        StartCoroutine(CanBasicEmotion(_curEmotion, Random.Range(5f, 15f)));
    }

    public void EatEmotion()
    {
        IsSpecialEmotion = true;
        anim.SetBool(_curEmotion, false);
        anim.SetBool("walk", false);
        anim.SetBool("Sniff", true);

        StartCoroutine(CanSpcialEmotion("Sniff", 1f));
    }
    private void SpecialEmotion()
    {
        if(Managers.Game.SaveData.EmotionList.Count ==0)
        {
            Managers.UI.ShowPopupUI<UI_NoEmotion>();
            return;
        }

        if (IsSpecialEmotion)
            return;
        IsSpecialEmotion = true;
        anim.SetBool(_curEmotion, false);
        anim.SetBool("walk", false);
        Managers.Sound.Play(Define.Sound.Effect, "Effects/CatTouch", 0.3f);
        _indexEmotion = Random.Range(0, Managers.Game.SaveData.EmotionList.Count);
        _curEmotion = Managers.Game.SaveData.EmotionList[_indexEmotion];
        anim.SetBool(_curEmotion, true);

        StartCoroutine(CanSpcialEmotion(_curEmotion, Managers.Data.ExpressBooks[1501 + _indexEmotion].Express_Time));
    }

    IEnumerator CanBasicEmotion(string _str, float _Time)
    {
        yield return new WaitForSeconds(_Time);
        anim.SetBool(_str, false);
        IsEmotion = false;
    }
    IEnumerator CanSpcialEmotion(string _str, float _Time)
    {
        yield return new WaitForSeconds(_Time);
        anim.SetBool(_str, false);
        yield return new WaitForSeconds(2f);
        IsSpecialEmotion = false;
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


