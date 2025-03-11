using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pressing W moves the camera up
        if (Input.GetKey(KeyCode.W))
        {
            mainCamera.transform.position += new Vector3(0, 1, 0);
        }

        //pressing S moves the camera down
        if (Input.GetKey(KeyCode.S))
        {
            mainCamera.transform.position += new Vector3(0, -1, 0);
        }

        //pressing A moves the camera left
        if (Input.GetKey(KeyCode.A))
        {
            mainCamera.transform.position += new Vector3(-1, 0, 0);
        }

        //pressing D moves the camera right
        if (Input.GetKey(KeyCode.D))
        {
            mainCamera.transform.position += new Vector3(1, 0, 0);
        }
    }
}
