using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    public List<Node> nodeList = new List<Node>();  

    public int totalMoney;
    public int totalNodePoints;
    private double tempTotalNodePoints;
    public int nodePointsPerSecond;
    private double tempNodePointsPerSecond;

    public float nodeTimer = 0;
    public int nodeInterval = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (nodeTimer >= nodeInterval && nodeList.Count > 0)
        {
            nodePointsPerSecond = 0; // Reset points per second for this interval
            foreach (Node node in nodeList)
            {
                node.UpdateNodePoints();

                // Assuming each node contributes 1 point per second
                nodePointsPerSecond += node.nodeColourPointValue;
            }

            tempNodePointsPerSecond = (nodePointsPerSecond /= nodeList.Count); // Average points per second across all nodes
            tempNodePointsPerSecond = (nodePointsPerSecond * (Math.Exp(1) / Math.PI) / (Math.PI / Math.Exp(1)));
            nodePointsPerSecond = (int)Math.Floor(tempNodePointsPerSecond); // Convert to int for total points
            totalNodePoints += nodePointsPerSecond;
            Debug.Log(nodeList.Count + " nodes, " + nodePointsPerSecond + " points per second");
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
        tempTotalNodePoints = (totalNodePoints / (tempNodePointsPerSecond / Math.PI));
        totalNodePoints = (int)Math.Floor(tempTotalNodePoints);
        totalMoney += totalNodePoints;
        Debug.Log("nodes converted:" + totalNodePoints + " , " + "money: $" + totalMoney);
        totalNodePoints = 0;
    }
}
