using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour
{
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

    public bool CanBuild()
    {
        return nodePrefab == null;
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

            //nodePrefab = grid.GetGridObject(x, y);
            if (CanBuild())
            {
                GameObject builtNode = Instantiate(nodePrefab, grid.GetWorldPosition(x, y), Quaternion.identity);
                SetPrefab(builtNode);
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
