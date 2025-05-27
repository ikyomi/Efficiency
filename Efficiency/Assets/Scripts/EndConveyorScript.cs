using UnityEngine;

public class EndConveyorScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("End Conveyor hit: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("StartConveyor") && collision.gameObject != transform.parent)
        {
/*            Debug.Log("Start Conveyor within end conveyor, we hit: " + collision.gameObject.name);

            Color currentColor = transform.parent.GetComponent<SpriteRenderer>().color;
            Debug.Log("Current color: " + currentColor);

            Color collisionColor = collision.transform.parent.GetComponent<SpriteRenderer>().color;
            Debug.Log("Collision color: " + collisionColor);*/

            //set the colour of the end point to the colour of the node
            collision.transform.parent.GetComponent<SpriteRenderer>().color = transform.parent.GetComponent<SpriteRenderer>().color;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("End Conveyor exit: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("StartConveyor") && collision.gameObject != transform.parent)
        {
            SpriteRenderer spriteRenderer = transform.parent.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.white; // Reset the color to white when exiting the conveyor
        }
    }
}
