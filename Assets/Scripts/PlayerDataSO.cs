using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerDataSO : ScriptableObject
{
    public string naveName;
    public int MaxLife;
    public float MovementSpeed; // Speed in X axis
    public float ScoringSpeed; // How fast score is screasing
    public float Cadencia;
    public Sprite naveSprite;
    
}