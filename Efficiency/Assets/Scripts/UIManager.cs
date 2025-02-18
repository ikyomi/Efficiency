using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Clicker clicker;

    public TMP_Text nodePointCounter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nodePointCounter.text = clicker.totalNodePoints.ToString();
    }
}
