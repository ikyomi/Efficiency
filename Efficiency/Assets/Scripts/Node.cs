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

    public List<Color> inputColourList = new List<Color>();
    public List<GameObject> inputNodeList = new List<GameObject>();

    public int nodeColourPointValue = 0;

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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("StartConveyor"))
        {
            Debug.Log("Start Conveyor Exit");
            //spriteRenderer.color = Color.white; // Reset the node's color when exiting a start conveyor

            //if the start point is not the same as the node, remove it from the input list
            if (collision.gameObject != transform.parent.gameObject)
            {
                Color inputColor = collision.transform.parent.GetComponent<SpriteRenderer>().color;
                if (inputColourList.Contains(inputColor))
                {
                    inputColourList.Remove(inputColor);
                }
            }
        }

        if (collision.gameObject.CompareTag("EndConveyor"))
        {
            Debug.Log(collision.gameObject.tag);
            Color inputColor = collision.transform.parent.GetComponent<SpriteRenderer>().color;
            if (inputColourList.Contains(inputColor))
            {
                inputColourList.Remove(inputColor);
            }
        }
    }
}
