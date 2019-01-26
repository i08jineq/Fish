using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton
{
    public static Singleton instance;
    public Canvas mainCanvas;

    public Transform cameraRoot;
    public Camera mainCamera;
    public GameEvent gameEvent;

    public static void Init()
    {
        if(instance == null)
        {
            instance = new Singleton();
        }
    }

    public Singleton()
    {
        mainCanvas = GameObject.FindObjectOfType<Canvas>();
        mainCamera = GameObject.FindObjectOfType<Camera>();

        GameObject cameraRootObject = new GameObject("Camera Root");
        cameraRootObject.transform.SetPositionAndRotation(mainCamera.transform.position, mainCamera.transform.rotation);

        cameraRoot = cameraRootObject.transform;
        mainCamera.transform.SetParent(cameraRoot);

        gameEvent = new GameEvent();
    }
}
