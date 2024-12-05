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

    Dictionary<Vector2Int, Node> openNodes;
    Dictionary<Vector2Int, Node> closedNodes;
    List<Vector2Int> path;

    public List<Vector2Int> FindPathToTarget(Vector2Int startPos, Vector2Int endPos, Cell[,] grid)
    {
        path = new List<Vector2Int>();

        Cell currentCell = grid[startPos.x, startPos.y];
        Node currentNode = new Node(currentCell.gridPosition, null, 0, GetDistance(startPos, endPos));

        openNodes = new Dictionary<Vector2Int, Node>() { { currentNode.position, currentNode } };
        closedNodes = new Dictionary<Vector2Int, Node>();

        while (openNodes.Count > 0)
        {
            openNodes.Remove(currentNode.position);
            closedNodes.Add(currentNode.position, currentNode);

            if (currentNode.position == endPos)
                return GetPath(currentNode, startPos, endPos);

            FindNeighbours(currentNode, endPos, grid);
            currentNode = FindLowestScore();
        }
        return path;
    }

    public List<Vector2Int> GetPath(Node currentNode, Vector2Int startPos, Vector2Int endPos)
    {
        Node reversePath = currentNode;
        while (reversePath.position != startPos)
        {
            path.Add(reversePath.position);
            reversePath = reversePath.parent;
        }
        path.Reverse();

        return path;
    }

    public void FindNeighbours(Node currentNode, Vector2Int endPos, Cell[,] grid)
    {
        List<Cell> neighbours = new List<Cell>();

        foreach (Cell cell in grid[currentNode.position.x, currentNode.position.y].GetNeighbours(grid))
        {
            // filter cells out of list that have wall on x/y value

            neighbours.Add(cell);

            if (cell.gridPosition.x == currentNode.position.x +1 && grid[currentNode.position.x, currentNode.position.y].HasWall(Wall.RIGHT))
            {
                neighbours.Remove(cell);

            }
            if (cell.gridPosition.x == currentNode.position.x -1 && grid[currentNode.position.x, currentNode.position.y].HasWall(Wall.LEFT))
            {
                neighbours.Remove(cell);

            }
            if (cell.gridPosition.y == currentNode.position.y +1 && grid[currentNode.position.x, currentNode.position.y].HasWall(Wall.UP))
            {
                neighbours.Remove(cell);

            }
            if (cell.gridPosition.y == currentNode.position.y -1 && grid[currentNode.position.x, currentNode.position.y].HasWall(Wall.DOWN))
            {
                neighbours.Remove(cell);

            }           
        }

        foreach (Cell cell in neighbours)
        {
            if (openNodes.ContainsKey(cell.gridPosition) || closedNodes.ContainsKey(cell.gridPosition))
                continue;
            Node newNode = new Node(cell.gridPosition, currentNode, currentNode.GScore + GetDistance(cell.gridPosition, currentNode.position), GetDistance(cell.gridPosition, endPos));
            openNodes.Add(cell.gridPosition, newNode);
        }

    }

    public Node FindLowestScore()
    {
        Vector2Int lowestScore = openNodes.Keys.First();
        int currentValue = int.MaxValue;
        foreach (KeyValuePair<Vector2Int, Node> node in openNodes)
        {
            if (node.Value.FScore < currentValue)
            {
                currentValue = node.Value.FScore;
                lowestScore = node.Key;
            }
        }
        return openNodes[lowestScore];
    }

    public static int GetDistance(Vector2Int begin, Vector2Int end)
    {
        return Mathf.Abs((begin.x - end.x) + (begin.y - end.y));
    }


    /// <summary>
    /// This is the Node class you can use this class to store calculated FScores for the cells of the grid, you can leave this as it is
    /// </summary>
    public class Node
    {
        public Vector2Int position; //Position on the grid
        public Node parent; //Parent Node of this node

        public int FScore
        { //GScore + HScore
            get { return GScore + HScore; }
        }
        public int GScore; //Current Travelled Distance
        public int HScore; //Distance estimated based on Heuristic

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
