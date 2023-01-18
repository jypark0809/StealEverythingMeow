using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Map
{
    public class Board
    {
        public Block[,] blocks;

        //¸Ê ¼ÂÆÃ
        public Board(int width, int height)
        {
            blocks = new Block[width, height];
            for (int i = 0; i < blocks.GetLength(0); i++)
            {
                for (int j = 0; j < blocks.GetLength(1); j++)
                {
                    blocks[i, j] = new Block(i, j);
                }
            }
        }

        public void SetBlock(int x,int y, bool wall)
        {
            blocks[x, y].wall = wall;
        }
        public void CheckClear()
        {
            foreach(Block block in blocks)
            {
                block.Clear();
            }
        }

        public bool Exists(Block block)
        {
            return Exists(block.x, block.y);
        }

        public bool Exists(int x, int y)
        {
            foreach (Block block in blocks)
            {
                if (block.x == x && block.y == y)
                    return true;
            }
            return false;
        }


        public List<Block> GetAroundBlocks(Block target)
        {
            List<Block> arounds = new List<Block>();
            if(Exists(target.x -1 , target.y-1))
            {
                Block block = blocks[target.x - 1, target.y - 1];
                arounds.Add(block);
            }
            if (Exists(target.x, target.y - 1))
            {
                Block block = blocks[target.x, target.y - 1];
                arounds.Add(block);
            }
            if (Exists(target.x + 1, target.y - 1))
            {
                Block block = blocks[target.x + 1, target.y - 1];
                arounds.Add(block);
            }
            if (Exists(target.x - 1, target.y))
            {
                Block block = blocks[target.x - 1, target.y];
                arounds.Add(block);
            }
            if (Exists(target.x , target.y))
            {
                Block block = blocks[target.x, target.y];
                arounds.Add(block);
            }
            if (Exists(target.x + 1, target.y))
            {
                Block block = blocks[target.x + 1, target.y];
                arounds.Add(block);
            }
            if (Exists(target.x -1, target.y + 1))
            {
                Block block = blocks[target.x - 1, target.y + 1];
                arounds.Add(block);
            }
            if (Exists(target.x, target.y +1))
            {
                Block block = blocks[target.x , target.y + 1];
                arounds.Add(block);
            }
            if (Exists(target.x + 1, target.y + 1))
            {
                Block block = blocks[target.x + 1, target.y + 1];
                arounds.Add(block);
            }

            for(int i = arounds.Count -1; i>=0;i--)
            {
                Block block = arounds[i];
                bool isDiagonalBlock = Mathf.Abs(block.x - target.y) == 1 && Mathf.Abs(block.y - target.y) == 1;
                if(isDiagonalBlock)
                {
                    Block blockX = arounds.Find(b => b.x == block.x && b.y == target.y);
                    if (blockX.wall)
                        arounds.Remove(block);

                    Block blockY = arounds.Find(b => b.x == target.x && b.y == block.y) ;
                    if (blockY.wall)
                        arounds.Remove(block);
                }
            }
            arounds.RemoveAll(b => b.wall);
            return arounds;
        }
    }
    
}

