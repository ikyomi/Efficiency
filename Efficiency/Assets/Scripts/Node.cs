using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph.Legacy;
using UnityEngine;

public class Node : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    GameObject nodeColour1;
    GameObject nodeColour2;
    GameObject nodeColour3;

    byte redOne;
    byte redTwo;
    byte redThree;

    byte greenOne;
    byte greenTwo;
    byte greenThree;

    byte blueOne;
    byte blueTwo;
    byte blueThree;

    byte mixedRed;
    byte mixedGreen;
    byte mixedBlue;

    byte finalRed;
    byte finalGreen;
    byte finalBlue;
    byte finalAlpha = 255;

    Color32 finalColour;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    Color32 LerpColor32(Color32 start, Color32 end, float t)
    {
        mixedRed = (byte)Mathf.Lerp(start.r, end.r, t);
        mixedGreen = (byte)Mathf.Lerp(start.g, end.g, t);
        mixedBlue = (byte)Mathf.Lerp(start.b, end.b, t);

        return finalColour = new Color32(mixedRed, mixedGreen, mixedBlue, finalAlpha);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] nodeColours = { nodeColour1, nodeColour2, nodeColour3 };

        for (int i = 0; i < nodeColours.Length; i++)
        {
            GameObject nodeColour = nodeColours[i];

            if (nodeColour != null)
            {
                SpriteRenderer spriteRenderer = nodeColour.GetComponent<SpriteRenderer>();

                if (spriteRenderer != null)
                {
                    Color32 colour = spriteRenderer.color;

                    switch (i)
                    {
                        case 0:
                            redOne = colour.r;
                            greenOne = colour.g;
                            blueOne = colour.b;
                            Debug.Log($"Red: {redOne}, Green: {greenOne}, Blue: {blueOne}");
                            break;
                        case 1:
                            redTwo = colour.r;
                            greenTwo = colour.g;
                            blueTwo = colour.b;
                            Debug.Log($"Red: {redTwo}, Green: {greenTwo}, Blue: {blueTwo}");
                            break;
                        case 2:
                            redThree = colour.r;
                            greenThree = colour.g;
                            blueThree = colour.b;
                            Debug.Log($"Red: {redThree}, Green: {greenThree}, Blue: {blueThree}");
                            break;
                    }
                }
                else
                {
                    Debug.Log($"{nodeColour.name} does not have a SpriteRenderer.");
                }
            }
        }







        if (redTwo == 0 && redThree == 0)
        {
            finalRed = redOne;
        }
        else
        if (redThree == 0)
        {
            //LerpColor32();
            finalRed = (byte)((redOne + redTwo) / 2);
        }
        else
        { 
            finalRed = (byte)((redOne + redTwo + redThree) / 3);
        }

        if (greenTwo == 0)
        {
            finalGreen = greenOne;
        }
        else
        if (greenThree == 0)
        {
            finalGreen = (byte)((greenOne + greenTwo) / 2);
        }
        else
        {
            finalGreen = (byte)((greenOne + greenTwo + greenThree) / 3);
        }

        if (blueTwo == 0)
        {
            finalBlue = blueOne;
        }
        else
        if (blueThree == 0)
        {
            finalBlue = (byte)((blueOne + blueTwo) / 2);
        }
        else
        {
            finalBlue = (byte)((blueOne + blueTwo + blueThree) / 3);
        }

        finalColour = new Color32(finalRed, finalGreen, finalBlue, finalAlpha);
        Debug.Log($"Final Red: {finalRed}, Final Green: {finalGreen}, Final Blue: {finalBlue}");
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
            Debug.Log("End Conveyor");
            //set the colour of the end point to the colour of the node
            spriteRenderer.color = finalColour;
                //collision.gameObject.transform.parent.GetComponent<SpriteRenderer>().color;
        }
    }
}
