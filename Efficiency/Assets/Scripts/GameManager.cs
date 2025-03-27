using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;

    public int totalMoney;
    public int totalNodePoints;
    public int nodePointsPerSecond;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        totalNodePoints += (int)(nodePointsPerSecond * Time.deltaTime); //inactive

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
