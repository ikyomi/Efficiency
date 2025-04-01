using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Legacy;
using UnityEngine;

public class Node : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] public GameManager gameManager;

    public Color targetColour;

    private bool isNodePlaced = false;
    public static event Action OnNodePlaced;

    public List<Color> inputColourList = new List<Color>();
    public List<GameObject> inputNodeList = new List<GameObject>();

    [SerializeField] private float lerpSpeed = 1f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }


    void Update()
    {
        //change the colour of the node to the target colour
        targetColour = AverageColours();
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColour, Time.deltaTime * lerpSpeed);
    }

    // This method will be called when a node is placed
    public void PlaceNode()
    {
        if (!isNodePlaced)
        {
            isNodePlaced = true;
            // Call the event to signal node placement
            OnNodePlaced?.Invoke();
        }
    }

    private void OnEnable()
    {
        // Subscribe to the event
        OnNodePlaced += UpdateNodePoints;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event to avoid memory leaks
        OnNodePlaced -= UpdateNodePoints;
    }

    private void UpdateNodePoints()
    {
        Debug.Log("UpdateNodePoints called");
        // Check if the inputColourList is empty
        if (inputColourList.Count == 0)
        {
            Debug.LogWarning("No colors in inputColourList to calculate node points.");
            return; // Exit early if the list is empty
        }

        // Calculate the average color value (from your previous code)
        int finalRed = 0;
        int finalGreen = 0;
        int finalBlue = 0;

        foreach (Color colour in inputColourList)
        {
            finalRed += (int)(colour.r * 255);
            finalGreen += (int)(colour.g * 255);
            finalBlue += (int)(colour.b * 255);
        }

        int averageColorValue = (finalRed + finalGreen + finalBlue) / (inputColourList.Count * 3);

        // Add to the nodePointsPerSecond when a node is placed
        gameManager.nodePointsPerSecond += averageColorValue;

        Debug.Log("Node Points per Second Updated: " + gameManager.nodePointsPerSecond);
    }

    private Color AverageColours()
    {
        //if there are no input colours, return the current colour
        if (inputColourList.Count == 0)
        {
            return spriteRenderer.color;
        }
        //create a new colour to store the average colour
        Color averageColour = new Color(0, 0, 0, 0);
        //add all the input colours together
        foreach (Color colour in inputColourList)
        {
            averageColour += colour;
        }
        //divide the total by the number of colours to get the average
        averageColour /= inputColourList.Count;

        return averageColour;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the collision is on a start point
        if (collision.gameObject.CompareTag("StartConveyor"))
        {
            Debug.Log("Start Conveyor");

            //set the colour of the start point to the colour of the node
            collision.gameObject.transform.parent.GetComponent<SpriteRenderer>().color = spriteRenderer.color;
        }

        if (collision.gameObject.CompareTag("EndConveyor"))
        {
            Debug.Log(collision.gameObject.tag);

            Color inputColor = collision.transform.parent.GetComponent<SpriteRenderer>().color;

            if(!inputColourList.Contains(inputColor))
            {
                inputColourList.Add(inputColor);
            }
        }
    }
}
