using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Astar
{
    /// <summary>
    /// TODO: Implement this function so that it returns a list of Vector2Int positions which describes a path from the startPos to the endPos
    /// Note that you will probably need to add some helper functions
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="endPos"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    //
    public readonly static List<Vector2Int> neighbours = new List<Vector2Int>
    {
        new Vector2Int(0,1), new Vector2Int(0,-1), new Vector2Int(1,0), new Vector2Int(-1,0), new Vector2Int(1,1), new Vector2Int(-1,1), new Vector2Int(-1,-1), new Vector2Int(1,-1)
    };

    public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
        List<Vector2Int> openNodes = new List<Vector2Int>();
        List<Vector2Int> closedNodes = new List<Vector2Int>();
        openNodes.Add(startPos);

        while(openNodes.Count() > 0)
        {
            Vector2Int currentPosition = openNodes[0];

            if(currentPosition == endPos)
            {
                break;
            }




        }

        return null;
    }

    public List<Vector2Int> FindNeighbours(Vector2Int currentPos, Cell[,] grid)
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        foreach (Vector2Int directions in neighbours)
        {
            Vector2Int tempPos = currentPos + directions;
            //if(grid contains tempPos)
            //{
            positions.Add(tempPos);
            //}
        }

        return positions;
    }


    /// <summary>
    /// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
    /// </summary>
    public class Node
    {
        public Vector2Int position; //Position on the grid
        public Node parent; //Parent Node of this node

        public float FScore
        { //GScore + HScore
            get { return GScore + HScore; }
        }
        public float GScore; //Current Travelled Distance
        public float HScore; //Distance estimated based on Heuristic

        public Node() { }
        public Node(Vector2Int position, Node parent, int GScore, int HScore)
        {
            this.position = position;
            this.parent = parent;
            this.GScore = GScore;
            this.HScore = HScore;
        }
    }
}
