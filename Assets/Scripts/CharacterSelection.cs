using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterSelection : MonoBehaviour
{
    public PlayerDataSO[] navesDisponibles; 
    public Image naveSeleccionadaImagen;   
    public TextMeshProUGUI nombreNaveText;           
    public PlayerDataSO playerLifeSO;
    public GameObject panelSelector;
    public GameManager gameManager;

    private PlayerDataSO naveSeleccionada;
    private int currentIndex = 0;
    private float tapTime;
    private const float doubleTapDelay = 0.3f;
    private bool isWaitingForSecondTap = false;
    void Start()
    {
        SeleccionarNave(currentIndex);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isWaitingForSecondTap && (Time.time - tapTime) <= doubleTapDelay)
            {
                isWaitingForSecondTap = false;
                ConfirmarSeleccion();
            }
            else
            {
                isWaitingForSecondTap = true;
                tapTime = Time.time;
            }
        }

        if (isWaitingForSecondTap && (Time.time - tapTime) > doubleTapDelay)
        {
            isWaitingForSecondTap = false;
            CambiarNave();
        }
    }
    public void SeleccionarNave(int index)
    {
        naveSeleccionada = navesDisponibles[index];
        naveSeleccionadaImagen.sprite = naveSeleccionada.naveSprite;
        nombreNaveText.text = naveSeleccionada.naveName;
        playerLifeSO = naveSeleccionada;
        PlayerPrefs.SetString("NaveSeleccionada", naveSeleccionada.naveName);
    }
    void CambiarNave()
    {
        currentIndex = (currentIndex + 1) % navesDisponibles.Length;
        SeleccionarNave(currentIndex);
    }
    void ConfirmarSeleccion()
    {
        playerLifeSO = naveSeleccionada;
        panelSelector.SetActive(false);
        gameManager.IniciarJuego(naveSeleccionada);
    }
}
