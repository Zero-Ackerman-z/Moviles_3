using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System;
public class PlayerLife : MonoBehaviour
{
    public PlayerDataSO playerData;

    public int CurrentLife;
    public TMP_Text lifeText;
    public event Action OnGameOver;
    public void AsignarDatos(PlayerDataSO data)
    {
        playerData = data;
        CurrentLife = playerData.MaxLife;
        UpdateLifeText();
    }
    public void GetDamage(int damage)
    {
        CurrentLife -= damage;
        if (CurrentLife <= 0)
        {
            CurrentLife = 0;
            OnGameOver?.Invoke();
            Debug.Log("¡Juego Terminado!");
        }
        UpdateLifeText();

    }
    private void UpdateLifeText()
    {
        if(lifeText != null)
        lifeText.text = "Vida: " + CurrentLife; 
    }
}