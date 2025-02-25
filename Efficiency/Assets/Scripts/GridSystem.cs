using UnityEngine;
using CodeMonkey.Utils;

public class GridSystem : MonoBehaviour
{
    private Grid<int> grid;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        grid = new Grid<int> (40, 20, 10f, new Vector3(-200, -100), (Grid<int> g, int x, int y) => 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
