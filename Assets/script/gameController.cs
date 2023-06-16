using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : PNGMonoBehaviour
{
    private static GameController instance;
    public static GameController Instance { get => instance; }

    [SerializeField] protected Camera mainCamera;
    public Camera MainCamera { get => mainCamera; }

    protected override void Awake()
    {
        base.Awake();
        if (GameController.instance != null) Debug.LogError("Only one  GameCtrl allow to awake");
        GameController.instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadMainCamera();
    }

    protected virtual void LoadMainCamera()
    {
        if (this.mainCamera != null) return;
        this.mainCamera = GameController.FindAnyObjectByType<Camera>();
        Debug.Log(transform.name + " :Load MainCamera", gameObject);

    }
}
