using UnityEngine;
using UnityEngine.EventSystems;

public class Clicker : MonoBehaviour
{
    public UIManager uiManager;

    public int totalNodePoints;

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
}