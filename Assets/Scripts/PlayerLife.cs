using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;
using System;
using System.Collections;
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
        Debug.Log("Daño recibido. Vida actual: " + CurrentLife);
        CurrentLife -= damage;
        AudioManager.Instance.PlaySFX("Damage");
        if (CurrentLife <= 0)
        {
            CurrentLife = 0;
            OnGameOver?.Invoke();
            AudioManager.Instance.PlaySFX("CriticalDamage");
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