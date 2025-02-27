using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour
{
    public UIManager uiManager;
    public Grid<int> grid;

    public int totalNodePoints;

    public GameObject nodePrefab;

    private Vector2 startPoint;
    private Vector2 endPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       grid = new Grid<int>(40, 20, 10f, new Vector2(-200, -100), (Grid<int> g, int x, int y) => 0);
    }

    public Vector2 mousePos
    {
        get
        { 
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
       
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) //clicker mechanic for nodes
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Node")) // Check tag directly
            {
                Debug.Log("node clicked");
                totalNodePoints++;

            }
        }
    }

    private void FixedUpdate()
    {
        //Vector2.Lerp(startPoint, endPoint, Time.deltaTime);

        if (Input.anyKeyDown) //test for node creation
        {
            grid.GetXY(mousePos, out int x, out int y);
            Instantiate(nodePrefab, grid.GetWorldPosition(x, y), Quaternion.identity);
        }
    }
}
