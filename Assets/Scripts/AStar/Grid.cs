using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Grid : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] LayerMask obstacleMask;
    [SerializeField] Vector2 gridWorldSize;
    [SerializeField] float nodeRadius;

    [Header("Test")]
    public Transform agent;

    private Node[,] grid;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    public List<Node> path;


    private void Start()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
                    Vector3.forward * (y * nodeDiameter + nodeRadius);

                bool walkable = !Physics.CheckSphere(worldPoint, nodeRadius, obstacleMask);

                grid[x, y] = new Node(walkable, worldPoint, new Vector2Int(x, y));
            }
        }

    }

    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }


    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridPosition.x + x;
                int checkY = node.gridPosition.y + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            Node agentNode = NodeFromWorldPoint(agent.position);

            foreach (Node node in grid)
            {
                Handles.color = (node.walkable) ? Color.green : Color.red;

                if (agentNode == node)
                {
                    Handles.color = Color.magenta;
                }


                if (path != null)
                {
                    if (path.Contains(node))
                    {
                        Handles.color = Color.black;
                    }
                }

                Handles.DrawWireDisc(node.worldPosition, Vector3.up, nodeRadius);
            }
        }
    }
}
