using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Map
{
    public class Block
    {
        public int x, y;

        public bool wall;

        public int F => G + H;

        //���������� �Ÿ�
        public int G { get; private set; }=0;
        //���������� �Ÿ�
        public int H { get; private set; } = 0;

        //��ȿ�� �Ǵ�
        public bool check = false;

        //������
        public Block prev = null;
        //������
        public Block next = null;

        public Block(int x,int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetPrice(int g, int h)
        {
            this.G = g;
            this.H = h;
        }

        public void Clear()
        {
            check = false;
            G = 0;
            H = 0;
            prev = null;
            next = null;
        }

    }
}
