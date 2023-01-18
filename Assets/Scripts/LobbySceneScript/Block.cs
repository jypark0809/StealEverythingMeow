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

        //시작점과의 거리
        public int G { get; private set; }=0;
        //도착점과의 거리
        public int H { get; private set; } = 0;

        //유효성 판단
        public bool check = false;

        //이전블럭
        public Block prev = null;
        //다음블럭
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
