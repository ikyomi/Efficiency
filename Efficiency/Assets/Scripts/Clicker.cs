using UnityEngine;
using UnityEngine.EventSystems;
using CodeMonkey.Utils;

public class Clicker : MonoBehaviour
{
    public UIManager uiManager;

    public int totalNodePoints;

    public GameObject nodePrefab;

    private Vector2 startPoint;
    private Vector2 endPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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
        Vector2.Lerp(startPoint, endPoint, Time.deltaTime);

        if (Input.anyKeyDown)
        {
            Instantiate(nodePrefab, new Vector2(0.5f,0.5f), Quaternion.identity);
        }

        UtilsClass.CreateWorldText();
    }
}