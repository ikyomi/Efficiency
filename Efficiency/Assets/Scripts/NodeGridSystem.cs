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
    public GameObject conveyorPrefab;

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
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Right-Click for Clicker Mechanic
        if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
        {
            grid.GetXY(mousePos, out int x, out int y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log($"Raycast hit: {hitObject.name}");

                // Check if we hit a node or its child
                if (hitObject.CompareTag("Node") || (hitObject.transform.parent != null && hitObject.transform.parent.CompareTag("Node")))
                {
                    Debug.Log("Node clicked! Points awarded.");
                    gameManager.totalNodePoints++;
                }
            }
            else
            {
                Debug.Log("Raycast missed everything!");
            }
        }

        // Left-Click to Place Nodes
        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXY(mousePos, out int x, out int y);

            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, y), dir);

            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList)
            {
                if (!CanBuild(gridPosition.x, gridPosition.y))
                {
                    canBuild = false;
                    break;
                }
            }

            if (canBuild)
            {
                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, y) + new Vector3(rotationOffset.x, rotationOffset.y, 0) * grid.GetCellSize();

                GameObject builtNode = Instantiate(nodePrefab, placedObjectWorldPosition, Quaternion.Euler(0, 0, placedObjectTypeSO.GetRotationAngle(dir)));
                Debug.Log($"Building node at position ({x}, {y})");

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.SetGridObject(gridPosition.x, gridPosition.y, builtNode);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Instantiate(conveyorPrefab, mousePos, Quaternion.Euler(0, 0, placedObjectTypeSO.GetRotationAngle(dir)));
                }

            }
            else
            {
                Debug.Log("Can't build here");
            }
        }
        else
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
            Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, y) + new Vector3(rotationOffset.x, rotationOffset.y, 0) * grid.GetCellSize();
            Instantiate(conveyorPrefab, mousePos, Quaternion.Euler(0, 0, placedObjectTypeSO.GetRotationAngle(dir)));
        }

        // Rotate Placement Object
        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
            //Debug.Log($"Rotation changed to: {dir}");
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
