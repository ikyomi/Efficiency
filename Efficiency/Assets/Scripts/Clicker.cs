using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour
{
    public PlacedObjectTypeSO placedObjectTypeSO;
    public UIManager uiManager;
    public GameManager gameManager;
    public Grid<int> grid;

    public GameObject nodePrefab;

    public int x;
    public int y;

    private Vector2 startPoint;
    private Vector2 endPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       grid = new Grid<int>(40, 20, 10f, new Vector2(-200, -100), (Grid<int> g, int x, int y) => 0);
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

    public bool CanBuild(int x, int y)
    {

        int nodePrefab = grid.GetGridObject(x, y);
        return nodePrefab == 0;
    }

    public override string ToString()
    {
        return x + ", " + y + "\n" + nodePrefab;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) //clicker mechanic for nodes
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            Debug.Log(mousePos);

            if (hit.collider != null && hit.collider.CompareTag("Node")) // Check tag directly
            {
                Debug.Log("node clicked");
                gameManager.totalNodePoints++;

            }
        }

        if (Input.GetMouseButtonDown(0)) //test for node creation
        {
            grid.GetXY(mousePos, out int x, out int y);

            //Debug.Log("x: " + x + ",y: " + y);

            List<Vector2Int> gridPositionList = placedObjectTypeSO.GetGridPositionList(new Vector2Int(x, y), PlacedObjectTypeSO.Dir.Down);
            
            if (CanBuild(x,  y))
            {
                GameObject builtNode = Instantiate(nodePrefab, grid.GetWorldPosition(x, y), Quaternion.identity);

                foreach (Vector2Int gridPosition in gridPositionList)
                {
                    grid.SetGridObject(gridPosition.x, gridPosition.y, 1); 
                }
                SetPrefab(builtNode);
                grid.SetGridObject(x, y, 1);

            }
            else
            {
                Debug.Log("Can't build here");
            }
            

        }
    }

    private void FixedUpdate()
    {
        //Vector2.Lerp(startPoint, endPoint, Time.deltaTime);

    }

    public Vector2 mousePos
    {
        get
        { 
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
       
    }
}
