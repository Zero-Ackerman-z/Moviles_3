using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{
    public PlayerDataSO[] navesDisponibles;  // Array de naves para elegir
    public Image naveSeleccionadaImagen;   // Imagen UI donde se muestra la nave seleccionada
    public Text nombreNaveText;            // Texto para mostrar el nombre de la nave seleccionada
    public PlayerDataSO playerLifeSO;      // Scriptable Object de vida

    private PlayerDataSO naveSeleccionada;

    void Start()
    {
        SeleccionarNave(0);
    }

    public void SeleccionarNave(int index)
    {
        naveSeleccionada = navesDisponibles[index];
        naveSeleccionadaImagen.sprite = naveSeleccionada.naveSprite;
        nombreNaveText.text = naveSeleccionada.naveName;
        playerLifeSO = naveSeleccionada;
        PlayerPrefs.SetString("NaveSeleccionada", naveSeleccionada.naveName);
    }
}
