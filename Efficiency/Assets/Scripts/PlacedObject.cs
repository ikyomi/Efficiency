using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, PlacedObjectTypeSO.Dir dir, PlacedObjectTypeSO placedObjectTypeSO)
    {
/*        if (placedObjectTypeSO == null)
        {
            Debug.LogError("Node.Create called with null placedObjectTypeSO!");
            return null;
        }

        if (placedObjectTypeSO.prefab == null)
        {
            Debug.LogError("Node.Create: placedObjectTypeSO.prefab is null!");
            return null;
        }
        Debug.Log("Node.Create called.");
        Debug.Log($"placedObjectTypeSO: {placedObjectTypeSO}");
        Debug.Log($"placedObjectTypeSO.prefab: {placedObjectTypeSO.prefab}");
        Debug.Log($"dir: {dir}");*/
        GameObject nodePrefab = Instantiate(placedObjectTypeSO.prefab, worldPosition, Quaternion.Euler(0, 0, placedObjectTypeSO.GetRotationAngle(dir)));

        PlacedObject node = nodePrefab.GetComponentInChildren<PlacedObject>();

        node.placedObjectTypeSO = placedObjectTypeSO;
        node.origin = origin;
        node.dir = dir;

        GameManager gm = FindObjectOfType<GameManager>();
        if (nodePrefab.CompareTag("Node"))
        {
            gm.nodeList.Add(nodePrefab.GetComponentInChildren<Node>());
        }

        return node;
    }

    private PlacedObjectTypeSO placedObjectTypeSO;
    private Vector2Int origin;
    private PlacedObjectTypeSO.Dir dir;

    public void DestroySelf() { Destroy(gameObject); }

    public List<Vector2Int> GetGridPositionList()
    {
        return placedObjectTypeSO.GetGridPositionList(origin, dir);
    }

}
