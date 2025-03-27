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

    public int finalRed;
    public int finalGreen;
    public int finalBlue;

    public Color targetColour;

    public Color32 finalColour;

    public List<Color> inputColourList = new List<Color>();
    public List<GameObject> inputNodeList = new List<GameObject>();

    [SerializeField] private float lerpSpeed = 1f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }


    void Update()
    {
        //change the colour of the node to the target colour
        targetColour = AverageColours();
        spriteRenderer.color = Color.Lerp(spriteRenderer.color, targetColour, Time.deltaTime * lerpSpeed);

        finalColour = spriteRenderer.color;

        finalRed = finalColour.r;
        finalGreen = finalColour.g;
        finalBlue = finalColour.b;

        gameManager.nodePointsPerSecond = (finalRed + finalGreen + finalBlue) / 6;
        gameManager.nodePointsPerSecond += gameManager.nodePointsPerSecond;
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

        if (collision.gameObject.CompareTag("EndConveyor"));
        {
            Debug.Log(collision.gameObject.name);

            Color inputColor = collision.transform.parent.GetComponent<SpriteRenderer>().color;

            if(!inputColourList.Contains(inputColor))
            {
                inputColourList.Add(inputColor);
            }
        }
    }
}
