using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;

    public int totalMoney;
    public int totalNodePoints;
    public int nodePointsPerSecond;

    public float nodeTimer = 0;
    public int nodeInterval = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (nodeTimer >= nodeInterval)
        {
            totalNodePoints += nodePointsPerSecond;
            nodeTimer = 0;
        }
        else
        {
            nodeTimer += Time.deltaTime;
        }

        if (totalNodePoints > 0)
        {
            uiManager.nodePointsToMoneyConversionButton.onClick.AddListener(Conversion);
        }
        else
        {
            //poor text
        }
    }

    public void Conversion()
    {
        totalMoney += totalNodePoints;
        Debug.Log("nodes converted:" + totalNodePoints + " , " + "money: $" + totalMoney);
        totalNodePoints = 0;
    }
}
