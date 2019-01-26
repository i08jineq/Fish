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
    public Pawn playerPawn;


    public static IEnumerator Init()
    {
        if(instance == null)
        {
            instance = new Singleton();
        }
        instance.gameEvent = new GameEvent();
        yield break;
    }

    public Singleton()
    {
        mainCanvas = GameObject.FindObjectOfType<Canvas>();
        mainCamera = GameObject.FindObjectOfType<Camera>();

        GameObject cameraRootObject = new GameObject("Camera Root");
        cameraRootObject.transform.SetPositionAndRotation(mainCamera.transform.position, mainCamera.transform.rotation);

        cameraRoot = cameraRootObject.transform;
        mainCamera.transform.SetParent(cameraRoot);
       
    }
}
