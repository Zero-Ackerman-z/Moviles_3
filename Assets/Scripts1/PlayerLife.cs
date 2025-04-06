using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public PlayerDataSO playerData;

    public int CurrentLife;

    private void Start()
    {
        CurrentLife = playerData.MaxLife;
    }

    public void GetDamage(int damage)
    {
        CurrentLife -= damage;
    }
}