using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Map.PathFinding
{
    public static class Astar
    {
        public static Block PathFinding(this Board board, Block start, Block dest)
        {
            if (board.Exists(start) && board.Exists(dest))
            {
                board.CheckClear();

                List<Block> waittingBlocks = new List<Block>();
                List<Block> finishedBlocks = new List<Block>();

                Block current = start;

                while (current != null)
                {
                    var aroundBlocks = board.GetAroundBlocks(current);

                    for (int i = 0; i < aroundBlocks.Count; i++)
                    {
                        var block = aroundBlocks[i];
                        if (!waittingBlocks.Equals(block) && !block.check)
                            waittingBlocks.Add(block);
                    }

                    current.check = true;

                    if (waittingBlocks.Remove(current))
                        finishedBlocks.Add(current);

                    if (aroundBlocks.Count == 0)
                        return null;
                    else
                    {
                        aroundBlocks = aroundBlocks.FindAll(block => !block.check);
                    }

                    //다음블록 계산
                    CalcRating(aroundBlocks, start, current, dest);

                    current = GetNextBlock(aroundBlocks, current);
                    if (current == null)
                    {
                        current = GetPriorityBlock(waittingBlocks);

                        if (current == null)
                        {
                            Block exceptionBlock = null;
                            for (int i = 0; i < finishedBlocks.Count; i++)
                            {
                                if (exceptionBlock == null || exceptionBlock.H > finishedBlocks[i].H)
                                    exceptionBlock = finishedBlocks[i];
                            }

                            current = exceptionBlock;
                            break;
                        }
                    }
                    else if (current.Equals(dest))
                    {
                        break;
                    }
                }
            }
            return null;
        }
        private static Block GetPriorityBlock(List<Block> waittingBlocks)
        {
            Block block = null;
            var enumerator = waittingBlocks.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (block == null || block.F < current.F)
                {
                    block = current;
                }
            }
            return block;
        }

        private static Block GetNextBlock(List<Block> arounds, Block current)
        {
            Block minValueBlock = null;
            for (int i = 0; i < arounds.Count; i++)
            {
                Block next = arounds[i];
                if (!next.check)
                {
                    if (minValueBlock == null)
                        minValueBlock = next;
                    else if (minValueBlock.H > next.H)
                        minValueBlock = next;
                }
            }
            return minValueBlock;
        }
        private static void CalcRating(List<Block> arounds, Block start, Block current, Block dest)
        {
            if (arounds != null)
            {
                for (int i = 0; i < arounds.Count; i++)
                {
                    var block = arounds[i];
                    bool isDiagonalBlock = Mathf.Abs(block.x - current.x) == 1 && Mathf.Abs(block.y - current.y) == 1;
                    int priceFromDest = (Mathf.Abs(dest.x - block.x) + Mathf.Abs(dest.y - block.y)) * 10;
                    if (block.prev == null)
                        block.prev = current;
                    block.SetPrice(current.G + (isDiagonalBlock ? 14 : 10), priceFromDest);
                }
            }
        }

    }
}
