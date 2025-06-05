using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public TMP_Text nodePointText;
    public TMP_Text nodePointsPerSecondText;
    public TMP_Text moneyText;

    public Scrollbar scrollbar;

    public GameObject scrollbarContent;

    public Button nodePointsToMoneyConversionButton;
    public Button newNodeButton;
    public Button newNodeColourPrefab;

    private float lastSpawnTime = -1f;
    private float cooldown = 1f;

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

        newNodeButton.onClick.AddListener(AddNewNodeButton);
    }

    public void AddNewNodeButton()
    {
        if (Time.time - lastSpawnTime < cooldown) return;

        lastSpawnTime = Time.time;
        GameObject newButton = Instantiate(newNodeColourPrefab.gameObject, transform);
        newButton.transform.SetParent(scrollbarContent.transform, false);
    }
}
