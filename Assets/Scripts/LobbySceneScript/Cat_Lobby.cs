using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Node(bool _isWall, int _x, int _y) { isWall = _isWall; x = _x; y = _y; }

    public bool isWall;
    public Node ParentNode;

    // G : 시작으로부터 이동했던 거리, H : |가로|+|세로| 장애물 무시하여 목표까지의 거리, F : G + H
    public int x, y, G, H;
    public int F { get { return G + H; } }
}
public class Cat_Lobby : MonoBehaviour
{
    public Vector2Int bottomLeft, topRight, startPos, targetPos;
    public List<Node> FinalNodeList;
    public bool allowDiagonal, dontCrossCorner;

    int sizeX, sizeY;
    Node[,] NodeArray;
    Node StartNode, TargetNode, CurNode;
    List<Node> OpenList, ClosedList;

    Rigidbody2D rigid;
    Animator anim;

    public float _Speed;
    private bool IsMove;

    int curWalk = 0;
    int WalkCount = 16;

    int index = 0;

    private bool ReFind = true;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (FinalNodeList.Count == 0 && ReFind)
        {
            ReFind = false;
            PathFinding(this.transform, new Vector2Int(Random.Range(bottomLeft.x, topRight.x), Random.Range(bottomLeft.y, topRight.y)));
            StartCoroutine(boolFind());
        }
        if (FinalNodeList.Count != 0)
        {
            MovePath();
        }

    }
    IEnumerator boolFind()
    {
        yield return new WaitForSeconds(1f);
        ReFind = true;
    }
    public void MovePath()
    {
        int InputX = FinalNodeList[index].x;
        int InputY = FinalNodeList[index].y;
        Vector2 targetPos = new Vector2(InputX, InputY);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, _Speed * Time.deltaTime);
        if ((transform.position.x == targetPos.x && transform.position.y == targetPos.y))
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
            IsMove = false;
        }

    }
    public void PathFinding(Transform Catpos, Vector2Int targetPos)
    {
        // NodeArray의 크기 정해주고, isWall, x, y 대입
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
        // 시작과 끝 노드, 열린리스트와 닫힌리스트, 마지막리스트 초기화
        StartNode = NodeArray[(int)Catpos.position.x - bottomLeft.x, (int)Catpos.position.y - bottomLeft.y];
        TargetNode = NodeArray[(int)targetPos.x - bottomLeft.x, (int)targetPos.y - bottomLeft.y];

        OpenList = new List<Node>() { StartNode };
        ClosedList = new List<Node>();
        FinalNodeList = new List<Node>();
        while (OpenList.Count > 0)
        {
            // 열린리스트 중 가장 F가 작고 F가 같다면 H가 작은 걸 현재노드로 하고 열린리스트에서 닫힌리스트로 옮기기
            CurNode = OpenList[0];
            for (int i = 1; i < OpenList.Count; i++)
                if (OpenList[i].F <= CurNode.F && OpenList[i].H < CurNode.H) CurNode = OpenList[i];

            OpenList.Remove(CurNode);
            ClosedList.Add(CurNode);


            // 마지막
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

                //for (int i = 0; i < FinalNodeList.Count; i++) print(i + "번째는 " + FinalNodeList[i].x + ", " + FinalNodeList[i].y);
                return;
            }
            // ↗↖↙↘
            if (allowDiagonal)
            {
                OpenListAdd(CurNode.x + 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y + 1);
                OpenListAdd(CurNode.x - 1, CurNode.y - 1);
                OpenListAdd(CurNode.x + 1, CurNode.y - 1);
            }
            // ↑ → ↓ ←
            OpenListAdd(CurNode.x, CurNode.y + 1);
            OpenListAdd(CurNode.x + 1, CurNode.y);
            OpenListAdd(CurNode.x, CurNode.y - 1);
            OpenListAdd(CurNode.x - 1, CurNode.y);
        }
    }
    void OpenListAdd(int checkX, int checkY)
    {
        // 상하좌우 범위를 벗어나지 않고, 벽이 아니면서, 닫힌리스트에 없다면
        if (checkX >= bottomLeft.x && checkX < topRight.x + 1 && checkY >= bottomLeft.y && checkY < topRight.y + 1 && !NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y].isWall && !ClosedList.Contains(NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y]))
        {
            // 대각선 허용시, 벽 사이로 통과 안됨
            if (allowDiagonal) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall && NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;

            // 코너를 가로질러 가지 않을시, 이동 중에 수직수평 장애물이 있으면 안됨
            if (dontCrossCorner) if (NodeArray[CurNode.x - bottomLeft.x, checkY - bottomLeft.y].isWall || NodeArray[checkX - bottomLeft.x, CurNode.y - bottomLeft.y].isWall) return;


            // 이웃노드에 넣고, 직선은 10, 대각선은 14비용
            Node NeighborNode = NodeArray[checkX - bottomLeft.x, checkY - bottomLeft.y];
            int MoveCost = CurNode.G + (CurNode.x - checkX == 0 || CurNode.y - checkY == 0 ? 10 : 14);


            // 이동비용이 이웃노드G보다 작거나 또는 열린리스트에 이웃노드가 없다면 G, H, ParentNode를 설정 후 열린리스트에 추가
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
        int _index = Random.Range(0, 15);
        switch (_index)
        {
            case 0:
                anim.Play("B_Other_Attack");
                break;
            case 1:
                anim.Play("W_Other_Blink");
                break;
            case 2:
                anim.Play("W_Other_Dig");
                break;
            case 3:
                anim.Play("W_Other_Ennui");
                break;
            case 4:
                anim.Play("W_Other_Lick");
                break;
            case 5:
                anim.Play("W_Other_Paw");
                break;
            case 6:
                anim.Play("W_Other_Relux");
                break;
            case 7:
                anim.Play("W_Other_Scratch");
                break;
            case 8:
                anim.Play("W_Other_Sleep1");
                break;
            case 9:
                anim.Play("W_Other_Sleep2");
                break;
            case 10:
                anim.Play("W_Other_Sleep3");
                break;
            case 11:
                anim.Play("W_Other_Sniff");
                break;
            case 12:
                anim.Play("W_Other_Stretch");
                break;
            case 13:
                anim.Play("W_Other_Sway");
                break;
            case 14:
                anim.Play("W_Other_Tail");
                break;
        }
    }
}

