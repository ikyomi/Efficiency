using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeGridSystem : MonoBehaviour
{
    [SerializeField] private List<PlacedObjectTypeSO> placedObjectTypeSOList;
    private PlacedObjectTypeSO placedObjectTypeSO;
    public UIManager uiManager;
    public GameManager gameManager;
    public Node node;
    public Grid<GameObject> grid;

    public GameObject nodePrefab;
    public GameObject conveyorPrefab;

    public int x;
    public int y;

    public bool overUi = false;

    private PlacedObjectTypeSO.Dir dir = PlacedObjectTypeSO.Dir.Down;

    private Vector2 startPoint;
    private Vector2 endPoint;

    private void Awake()
    {
        placedObjectTypeSO = placedObjectTypeSOList[0];  // Default to the first object type
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create grid with GameObjects
        grid = new Grid<GameObject>(40, 20, 10f, new Vector2(-200, -100), (Grid<GameObject> g, int x, int y) => null);  // Initialize with null (empty)
    }

    public void SetPrefab(Node node)
    {
        this.node = node;
        grid.TriggerGridObjectChanged(x, y);
    }

    public void ClearPrefab()
    {
        node = null;
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
        if (Input.GetMouseButtonDown(3) && !EventSystem.current.IsPointerOverGameObject())
        {
            grid.GetXY(mousePos, out int x, out int y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            if (hit.collider != null)
            {
                GameObject hitObject = hit.collider.gameObject;
                //Debug.Log($"Raycast hit: {hitObject.name}");

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

            if (canBuild && overUi == false)
            {
               /* if (placedObjectTypeSO == null)
                {
                    Debug.LogError("placedObjectTypeSO is NULL before Node.Create is called.");
                    return;
                }
                else
                {
                    Debug.Log($"Placed Object Type: {placedObjectTypeSO.nameString}");
                }*/

                Vector2Int rotationOffset = placedObjectTypeSO.GetRotationOffset(dir);
                Vector3 placedObjectWorldPosition = grid.GetWorldPosition(x, y) + new Vector3(rotationOffset.x, rotationOffset.y, 0) * grid.GetCellSize();
/*
                Debug.Log($"Placing object at: {placedObjectWorldPosition}");
                Debug.Log($"Dir: { dir}");
                Debug.Log($"Placed Object Type: {placedObjectTypeSO.nameString}");*/
                
                PlacedObject node = PlacedObject.Create(placedObjectWorldPosition, new Vector2Int(x, y), dir, placedObjectTypeSO); // Create the node at the specified position

                /*if (node == null)
                {
                    Debug.LogError("Node is null after Node.Create!");
                    return;
                }*/

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.SetGridObject(gridPosition.x, gridPosition.y, node.gameObject);
                }

            }
            else
            {
                Debug.Log("Can't build here");
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            grid.GetXY(mousePos, out int x, out int y);  // Get correct grid position from mouse
            GameObject gameObject = grid.GetGridObject(x, y); //fix this... is null

            // Check if the gameObject is null
            if (gameObject == null)
            {
                Debug.LogWarning($"GameObject at grid position ({x}, {y}) is null.");
                return; // Exit early if gameObject is null
            }
            PlacedObject node = gameObject.GetComponentInChildren<PlacedObject>();
            if (node != null)
            {
                node.DestroySelf();

                List<Vector2Int> gridPositionList = node.GetGridPositionList();
                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.SetGridObject(gridPosition.x, gridPosition.y, null);
                }
            }
        }

        // Rotate Placement Object
        if (Input.GetKeyDown(KeyCode.R))
        {
            dir = PlacedObjectTypeSO.GetNextDir(dir);
            Debug.Log($"Rotation changed to: {dir}");
        }

        /*if (Input.GetKeyDown(KeyCode.Alpha1)) { placedObjectTypeSO = placedObjectTypeSOList[0]; }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { placedObjectTypeSO = placedObjectTypeSOList[1]; }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { placedObjectTypeSO = placedObjectTypeSOList[2]; }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { placedObjectTypeSO = placedObjectTypeSOList[3]; }*/

    }

    public void SetPlacedObjectType(int index)
    {
        if (index >= 0 && index < placedObjectTypeSOList.Count)
        {
            placedObjectTypeSO = placedObjectTypeSOList[index];
        }
        else
        {
            Debug.LogWarning("Index out of range: " + index);
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
