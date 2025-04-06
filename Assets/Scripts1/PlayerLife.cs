using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
public class PlayerLife : MonoBehaviour
{
    public PlayerDataSO playerData;

    public int CurrentLife;
    public TMP_Text lifeText;
    private void Start()
    {
        CurrentLife = playerData.MaxLife;
        UpdateLifeText();
    }

    public void GetDamage(int damage)
    {
        CurrentLife -= damage;
        if (CurrentLife <= 0)
        {
            CurrentLife = 0;
            Debug.Log("¡Juego Terminado!");
        }
    }
    private void UpdateLifeText()
    {
        lifeText.text = "Vida: " + CurrentLife.ToString(); 
    }
}