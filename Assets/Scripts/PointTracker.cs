using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointTracker : MonoBehaviour
{
    [SerializeField] private int totalPoints = 0;
    [SerializeField] private TMP_Text pointsOnUI;

    

    public void AddPoints(int points) { totalPoints += points; UpdatePointsUI(); }

    public void UpdatePointsUI() { pointsOnUI.text = totalPoints.ToString(); }

    public int GetTotalPoints() { return totalPoints; }

    public void ResetPoints() { totalPoints = 0; UpdatePointsUI(); }


}
