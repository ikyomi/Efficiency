using UnityEngine;
using CodeMonkey.Utils;
using System.Collections.Generic;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private PathfindingDebugStepVisual pathfindingDebugStepVisual;
    [SerializeField] private PathfindingVisual pathfindingVisual;

    private Grid<int> grid;
    private Pathfinding pathfinding;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //grid = new Grid<int> (40, 20, 10f, new Vector3(-200, -100), (Grid<int> g, int x, int y) => 0);
        pathfinding = new Pathfinding(40, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = UtilsClass.GetMouseWorldPosition();
            pathfinding.GetGrid().GetXY(mouseWorldPosition, out int x, out int y);
            List<PathNode> path = pathfinding.FindPath(0, 0, x, y);
            if (path != null)
            {
                for (int i = 0; i < path.Count - 1; i++)
                {
                    Debug.DrawLine(new Vector3(path[i].x, path[i].y) * 10f + Vector3.one * 5f, new Vector3(path[i + 1].x, path[i + 1].y) * 10f + Vector3.one * 5f, Color.green, 5f);
                }
            }
        }
    }
}
