using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    /*private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private GridXZ<PathNode> grid;
    private List<PathNode> openList;
    private List<PathNode> closedList;

    public Pathfinding(int width, int height)
    {
       grid = new GridXZ<PathNode>(width, height, 2f, Vector3.zero, (GridXZ<PathNode> g, int x, int z) => new PathNode(grid,x,z));
    }

    private List<PathNode> FindPath(int startX, int startZ, int endX, int endZ)
    {
        PathNode startNode = grid.GetGridObject(startX, startZ);

        openList = new List<PathNode> { startNode };
        closedList = new List<PathNode>();

        for(int x = 0; x < grid.GetWidth(); x++)
        {
            for(int z = 0; z < grid.GetHeight(); z++)
            {
                PathNode pathNode = grid.GetGridObject(x, z);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        startNode.gCost = 0;
        //startNode.hCost = 
    }

    private int CalculateDistanceCost(PNode a, PNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
    }*/
}
