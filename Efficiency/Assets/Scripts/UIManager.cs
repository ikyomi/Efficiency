using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //public Clicker clicker;
    public GameManager gameManager;

    public TMP_Text nodePointText;
    public TMP_Text nodePointsPerSecondText;
    public TMP_Text moneyText;

    public Button nodePointsToMoneyConversionButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nodePointText.text = "NP: " + gameManager.totalNodePoints.ToString();
        moneyText.text = "$" + gameManager.totalMoney.ToString();
        nodePointsPerSecondText.text = "Nodes Per Second: " + gameManager.nodePointsPerSecond.ToString();
    }
}
