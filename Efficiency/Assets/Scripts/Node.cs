using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] public GameManager gameManager;
    public int nodeColourPointValue = 0;

    public Color targetColour;

    private bool isNodePlaced = false;


    [Header("Connected Conveyors")]
    public List<SpriteRenderer> inputConveyors = new List<SpriteRenderer>();
    public List<SpriteRenderer> outputConveyors = new List<SpriteRenderer>();

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
        targetColour = GetInputColour();
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColour, Time.deltaTime * lerpSpeed);

        //Set output colour
        foreach(SpriteRenderer outputConveyor in outputConveyors)
        {
            if (outputConveyor != null)
            {
                outputConveyor.color = spriteRenderer.color;
            }
        }
    }

    public void UpdateNodePoints()
    {
        Debug.Log("UpdateNodePoints called");
        // Check if the inputColourList is empty
        /*if (inputColourList.Count == 0)
        {
            Debug.LogWarning("No colors in inputColourList to calculate node points.");
            return; // Exit early if the list is empty
        }*/

        // Calculate the average color value (from your previous code)
        int finalRed = 0;
        int finalGreen = 0;
        int finalBlue = 0;

        finalRed += (int)(spriteRenderer.color.r * 255);
        finalGreen += (int)(spriteRenderer.color.g * 255);
        finalBlue += (int)(spriteRenderer.color.b * 255);

        if (finalRed == 0 && finalGreen == 0 && finalBlue == 0 || finalRed == 255 && finalGreen == 255 && finalBlue == 255)
        {
            nodeColourPointValue = 0;
        }
        else
        {
            nodeColourPointValue = (finalRed + finalGreen + finalBlue) / 3;  //(inputColourList.Count * 3);
        }
    }

    private Color GetInputColour()
    {
        //if there are no input colours, return the current colour
        if (inputConveyors.Count == 0)
        {
            return spriteRenderer.color;
        }
        //create a new colour to store the average colour
        Color averageColour = new Color(0, 0, 0, 0);
        //add all the input colours together

        foreach (SpriteRenderer sprite in inputConveyors)
        {
            averageColour += sprite.color;
        }
        //divide the total by the number of colours to get the average
        averageColour /= inputConveyors.Count;

        return averageColour;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if the collision is a conveyor start point
        if (collision.gameObject.CompareTag("StartConveyor"))
        {
            //add collision game object to list if it isnt in there already
            if (!outputConveyors.Contains(collision.transform.parent.GetComponent<SpriteRenderer>()))
            {
                outputConveyors.Add(collision.transform.parent.GetComponent<SpriteRenderer>());
            }
        }

        if (collision.gameObject.CompareTag("EndConveyor"))
        {
            //if the end point is not the same as the node, add it to the input list
            if(!inputConveyors.Contains(collision.transform.parent.GetComponent<SpriteRenderer>()))
            {
                inputConveyors.Add(collision.transform.parent.GetComponent<SpriteRenderer>());
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StartConveyor"))
        {
            //remove output conveyor from the list if it exists
            if (outputConveyors.Contains(collision.transform.parent.GetComponent<SpriteRenderer>()))
            {
                outputConveyors.Remove(collision.transform.parent.GetComponent<SpriteRenderer>());
            }
        }

        if (collision.gameObject.CompareTag("EndConveyor"))
        {
            //remove input conveyor from the list if it exists
            if (inputConveyors.Contains(collision.transform.parent.GetComponent<SpriteRenderer>()))
            {
                inputConveyors.Remove(collision.transform.parent.GetComponent<SpriteRenderer>());
            }
        }
    }
}
