using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInputHandler : MonoBehaviour
{
    [Header("Duración necesaria para long press")]
    public float longPressThreshold = 1.2f;

    private float pressTime;
    private bool isPressing = false;
    private void OnEnable()
    {
        EventManager.OnSceneChanged += OnTap;
    }

    private void OnDisable()
    {
        EventManager.OnSceneChanged -= OnTap;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    isPressing = true;
                    pressTime = Time.time;
                    break;

                case TouchPhase.Ended:
                    if (isPressing)
                    {
                        float heldDuration = Time.time - pressTime;

                        if (heldDuration >= longPressThreshold)
                        {
                            OnLongPress();
                        }
                        else
                        {
                            OnTap();
                        }

                        isPressing = false;
                    }
                    break;

                case TouchPhase.Canceled:
                    isPressing = false;
                    break;
            }
        }
    }

    void OnTap()
    {
        Debug.Log("Tap detectado - ir a la siguiente escena");

        SceneGlobalManager.Instance.LoadSceneAdditiveAsync("CharacterSelect");
    }

    void OnLongPress()
    {
        Debug.Log("Long Press detectado - salir del juego");
        Application.Quit();

    }

}
