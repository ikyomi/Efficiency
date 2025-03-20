using UnityEngine;

public class Conveyor : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("StartConveyor"))
        {
            Debug.Log("StartConveyor");
            //set the colour of the end point to the colour of the node
            spriteRenderer.color = collision.gameObject.GetComponent<SpriteRenderer>().color;
        }
    }
}
