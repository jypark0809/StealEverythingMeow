using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class Cat_Lobby : MonoBehaviour
{
    public Vector2Int bottomLeft, topRight;
    private Vector2Int startPos, targetPos;
    public List<Node> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;


    private int _index;
    private string _indexstr;

[SerializeField]
    public enum Catname
    {
        Black,
        Gray,
        Tabby,
        Calico,
        White
    }

    public Catname cat;

    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;

    Rigidbody2D rigid;
    Animator anim;

    public float _Speed;

    int index = 0;

    private bool ReFind = true;
    private bool IsEmotion = false;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (FinalNodeList.Count == 0 && ReFind)
        {
            CancelInvoke();
            ReFind = false;
            targetPos = new Vector2Int(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y));
            PathFinding(this.transform, targetPos);
            StartCoroutine(boolFind());
            InvokeRepeating("doEmotion", 3f, Random.Range(10f,15f));
        }
        if (FinalNodeList.Count != 0 && !IsEmotion)
        {
            MovePath();  
        }

    }

    IEnumerator boolFind()
    {
        yield return new WaitForSeconds(10f);
        ReFind = true;
    }
    private void MovePath()
    {
        int InputX = FinalNodeList[index].x;
        int InputY = FinalNodeList[index].y;
        Vector2Int targetNode = new Vector2Int(InputX, InputY);
        transform.position = Vector2.MoveTowards(transform.position, targetNode, _Speed * Time.deltaTime);
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
                foreach (Collider2D col in Physics2D.OverlapCircleAll(new Vector2(i + bottomLeft.x, j + bottomLeft.y), 0.4f))
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
    private void doEmotion()
    {
        if (IsEmotion)
            return;
        IsEmotion = true;
        _index = Random.Range(0, Managers.Game.SaveData.Emotion.Count);
        _indexstr = Managers.Game.SaveData.Emotion[_index];
        anim.SetBool(_indexstr, true);
        StartCoroutine(IsEmoe(_indexstr, Managers.Game.SaveData.EmotionTime[_index]));
    }

    IEnumerator IsEmoe(string _str ,float _Time)
    {
        yield return new WaitForSeconds(_Time);
        IsEmotion = false;
        anim.SetBool(_str,false);
    }
    public void Love(string _food)
    {
        Debug.Log("�������� �ö����ϴ�");
        //����̺� ���� �з�
        //�̺�Ʈ �߰� (������)
        Managers.UI.ClosePopupUI();
    }


    private void OnMouseDown()
    {
        if (IsEmotion)
        {
            Managers.Sound.Play(Define.Sound.Effect, "Effects/CatTouch", 0.3f);
            anim.SetBool(_indexstr, false );
            IsEmotion = false;
        }
    }

}

