using System.Collections;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    SpriteRenderer mySpriteRenderer;
    [SerializeField] Color conveyorColor = Color.white;
    public Color ConveyorColor
    {
        get { return conveyorColor; }
        set
        {
            Debug.Log($"Going from {conveyorColor} to {value}"); // Log the color change
            conveyorColor = value;
            mySpriteRenderer.color = conveyorColor; // Update the color of the conveyor
        }
    }

    IEnumerator UpdateOutputConveyorColour()
    {
        yield return new WaitForSeconds(0.1f); // Wait for a short duration before updating the color
        //Update output conveyor
        if (outputConveyor != null)
        {
            /*while(outputConveyor.ConveyorColor != ConveyorColor)
            {
                outputConveyor.ConveyorColor = Color.Lerp(outputConveyor.ConveyorColor, ConveyorColor, Time.deltaTime); // Set the color of the output conveyor to match the conveyor's color
                yield return null; // Wait for the next frame
            }*/

            outputConveyor.ConveyorColor = ConveyorColor; // Set the color of the output conveyor to match the conveyor's color

        }
    }

    [Header("End Points")]
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject endPoint;

    [Header("Input")]
    [SerializeField] Conveyor inputConveyor;

    [Header("Output")]
    [SerializeField] Conveyor outputConveyor;


    private void Awake()
    {
        // Get the SpriteRenderer component from the parent GameObject
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        if (mySpriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on parent GameObject.");
        }
    }

    private void Update()
    {
        if(inputConveyor != null)
        {
            mySpriteRenderer.color = Color.Lerp(mySpriteRenderer.color, inputConveyor.mySpriteRenderer.color, Time.deltaTime); // Set the color of the conveyor to match the input conveyor's color
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("End Conveyor hit: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("StartConveyor") && collision.gameObject != startPoint)
        {
            //set the colour of the end point to the colour of the node
            //Assign that sprite renderer to outputSpriteRenderer
            if(collision.transform.parent.TryGetComponent<Conveyor>(out Conveyor tempConveyor))
            {
                outputConveyor = tempConveyor;
                outputConveyor.ConveyorColor = mySpriteRenderer.color; // Set the color of the output conveyor to match the conveyor's color

                Debug.Log("Set outputSpriteRenderer color to: " + mySpriteRenderer.color);
            }
            else
            {
                Debug.LogError("SpriteRenderer not found on the conveyor GameObject.");
            }            
        }

        // Check if the collided object is an end conveyor
        if (collision.gameObject.CompareTag("EndConveyor") && collision.gameObject != endPoint)
        {
            //set the colour of the end point to the colour of the node
            //Assign that sprite renderer to outputSpriteRenderer
            if (collision.transform.parent.TryGetComponent<Conveyor>(out Conveyor tempConveyor))
            {
                inputConveyor = tempConveyor;

                ConveyorColor = inputConveyor.ConveyorColor; // Set the color of the conveyor to match the input conveyor's color
            }
            else
            {
                Debug.LogError("SpriteRenderer not found on the conveyor GameObject.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If the conveyor is exited, reset the sprite renderers
        /*if (collision.gameObject.CompareTag("StartConveyor") && collision.gameObject != startPoint)
        {
            //set the colour of the end point to the colour of the node
            //Assign that sprite renderer to outputSpriteRenderer
            if (collision.transform.parent.TryGetComponent<SpriteRenderer>(out SpriteRenderer tempSpriteRenderer))
            {
                outputSpriteRenderer = tempSpriteRenderer;

                outputSpriteRenderer.color = mySpriteRenderer.color; // Set the color of the output sprite renderer to match the conveyor's color
                Debug.Log("Set outputSpriteRenderer color to: " + mySpriteRenderer.color);
            }
            else
            {
                Debug.LogError("SpriteRenderer not found on the conveyor GameObject.");
            }
        }*/

        // Check if the collided object is an end conveyor
        if (collision.gameObject.CompareTag("EndConveyor") && collision.gameObject != endPoint)
        {
            ConveyorColor = Color.white; // Reset the color of the conveyor when exiting
        }
    }
}
