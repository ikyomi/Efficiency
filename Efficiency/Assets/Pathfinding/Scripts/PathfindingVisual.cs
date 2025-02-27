/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class PathfindingVisual : MonoBehaviour {

    private Grid<PathNode> InitializeGrid;
    private Mesh mesh;
    private bool updateMesh;

    private void Awake() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    public void SetGrid(Grid<PathNode> InitializeGrid) {
        this.InitializeGrid = InitializeGrid;
        UpdateVisual();

        InitializeGrid.OnGridObjectChanged += Grid_OnGridValueChanged;
    }

    private void Grid_OnGridValueChanged(object sender, Grid<PathNode>.OnGridObjectChangedEventArgs e) {
        updateMesh = true;
    }

    private void LateUpdate() {
        if (updateMesh) {
            updateMesh = false;
            UpdateVisual();
        }
    }

    private void UpdateVisual() {
        MeshUtils.CreateEmptyMeshArrays(InitializeGrid.GetWidth() * InitializeGrid.GetHeight(), out Vector3[] vertices, out Vector2[] uv, out int[] triangles);

        for (int x = 0; x < InitializeGrid.GetWidth(); x++) {
            for (int y = 0; y < InitializeGrid.GetHeight(); y++) {
                int index = x * InitializeGrid.GetHeight() + y;
                Vector3 quadSize = new Vector3(1, 1) * InitializeGrid.GetCellSize();

                PathNode pathNode = InitializeGrid.GetGridObject(x, y);

                if (pathNode.isWalkable) {
                    quadSize = Vector3.zero;
                }

                MeshUtils.AddToMeshArrays(vertices, uv, triangles, index, InitializeGrid.GetWorldPosition(x, y) + quadSize * .5f, 0f, quadSize, Vector2.zero, Vector2.zero);
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

}*/

