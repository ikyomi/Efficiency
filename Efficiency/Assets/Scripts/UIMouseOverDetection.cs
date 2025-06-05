using UnityEngine;

public class UIMouseOverDetection : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        NodeGridSystem nodeGridSystem = FindObjectOfType<NodeGridSystem>();
        if (nodeGridSystem != null)
        {
            nodeGridSystem.overUi = true;  // Set overUI to true
        }
        else
        {
            Debug.LogWarning("NodeGridSystem instance not found.");
        }
    }

    private void OnMouseExit()
    {
        NodeGridSystem nodeGridSystem = FindObjectOfType<NodeGridSystem>();
        if (nodeGridSystem != null)
        {
            nodeGridSystem.overUi = false;  // Set overUI to false
        }
        else
        {
            Debug.LogWarning("NodeGridSystem instance not found.");
        }
    }
}
