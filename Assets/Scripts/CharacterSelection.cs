using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Assets.Scripts;
using System;
public class CharacterSelection : MonoBehaviour
{
    public PlayerDataSO[] navesDisponibles; 
    public Image naveSeleccionadaImagen;   
    public TextMeshProUGUI nombreNaveText;           
    public PlayerDataSO playerLifeSO;
    //public GameObject panelSelector;
    //public GameManager gameManager;

    private PlayerDataSO naveSeleccionada;
    private int currentIndex = 0;
    private float tapTime;
    private const float doubleTapDelay = 0.3f;
    private bool isWaitingForSecondTap = false;
    private bool seleccionConfirmada = false;

    private void OnEnable()
    {
        EventManager.OnSceneChanged += () => SeleccionarNave(currentIndex);
    }

    private void OnDisable()
    {
        EventManager.OnSceneChanged -= () => SeleccionarNave(currentIndex);
    }
    void Start()
    {
        if (navesDisponibles != null && navesDisponibles.Length > 0)
        {
            SeleccionarNave(currentIndex);
        }
        else
        {
            Debug.LogError("No hay naves disponibles asignadas");
        }
    }
    void Update()
    {
        if (seleccionConfirmada) return; 

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
    private void ConfirmarSeleccion()
    {
        seleccionConfirmada = true;
        playerLifeSO = naveSeleccionada;
        PlayerPrefs.SetString("NaveSeleccionada", naveSeleccionada.naveName);

        SceneGlobalManager.Instance.CargarEscenaGameConResults(naveSeleccionada);
        //PanelSelector.SetActive(false);
        //gameManager.IniciarJuego(naveSeleccionada);
    }

}
