using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreSO", menuName = "Game/Score")]
public class ScoreSO : ScriptableObject
{
    public int puntuacion;

    public void AumentarPuntos(int puntos)
    {
        puntuacion += puntos;
    }

    public void ResetearPuntaje()
    {
        puntuacion = 0;
    }
}
