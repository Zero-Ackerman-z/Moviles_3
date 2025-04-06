using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public ScoreSO scoreData;  

    public PlayerDataSO playerData;  
    void Update()
    {
        scoreData.AumentarPuntos((int)(playerData.ScoringSpeed * Time.deltaTime));
    }
}


