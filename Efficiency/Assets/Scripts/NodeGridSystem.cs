using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeGridSystem : MonoBehaviour
{
    public PlacedObjectTypeSO placedObjectTypeSO;
    public UIManager uiManager;
    public GameManager gameManager;
    public Grid<GameObject> grid;

    public GameObject nodePrefab;

    public int x;
    public int y;

    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;

    private Vector2 startPoint;
    private Vector2 endPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create grid with GameObjects
        grid = new Grid<GameObject>(40, 20, 10f, new Vector2(-200, -100), (Grid<GameObject> g, int x, int y) => null);  // Initialize with null (empty)
    }

    public void SetPrefab(GameObject nodePrefab)
    {
        this.nodePrefab = nodePrefab;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void ClearPrefab()
    {
        nodePrefab = null;
        grid.TriggerGridObjectChanged(x, y);
    }

    // Check if we can build at the specified position (grid coordinates)
    public bool CanBuild(int x, int y)
    {
        GameObject existingNode = grid.GetGridObject(x, y);  // Get the existing node at this position
        Debug.Log($"Checking build at ({x}, {y}): {(existingNode == null ? "Can Build" : "Cannot Build")}");
        return existingNode == null;  // We can build if there's no existing node (i.e., the position is empty)
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) // Clicker mechanic for nodes
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Node"))
            {
                Debug.Log("Node clicked");
                gameManager.totalNodePoints++;
            }
        }

        if (Input.GetMouseButtonDown(0)) // Test for node creation
        {
            grid.GetXY(mousePos, out int x, out int y);

            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, y), dir);

            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                // Correct check using CanBuild with grid coordinates
                if (!CanBuild(gridPosition.x, gridPosition.y))
                {
                    canBuild = false;
                    break;
                }
            }

            if (canBuild) // If we can build
            {
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);

                if (dir == PlacedObjectTypeSO.Dir.Left)
                {
                    rotationOffset.x += 2;
                    rotationOffset.y -= 2; 
                }

                if (dir == PlacedObjectTypeSO.Dir.Right)
                {
                    rotationOffset.x -= 2;
                    rotationOffset.y += 2;  
                }

                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, y) + new Vector3(rotationOffset.x, rotationOffset.y, 0) * grid.GetCellSize();


                GameObject builtNode = Instantiate(nodePrefab, placedObjectWorldPosition, Quaternion.Euler(0, 0, placedObjectTypeSO.GetRotationAngle(dir)));
                Debug.Log($"Building node at position ({x}, {y})");

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.SetGridObject(gridPosition.x, gridPosition.y, builtNode);  // Place the new node in the grid
                }
            }
            else
            {
                Debug.Log("Can't build here");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
            Debug.Log(dir);
        }
    }

    public Vector2 mousePos
    {
        get
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
