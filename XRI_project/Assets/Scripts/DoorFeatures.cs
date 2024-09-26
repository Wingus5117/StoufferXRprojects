using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorFeatures : CoreFeatures
{
    [Header("Door Confing")]
    public Transform doorPivot;
    public float maximumPivotAngle = 90f;
    public bool reversePivotDirection = false;
    public float DoorSpeed = 2f;
    public bool DoorOpen = false;
    public bool makeKinematicOnOpen = false;

    [Header("Interactions Config")]
    public XRSocketInteractor SocketInteractor;
    public XRSimpleInteractable SimpleInteractable;

    private void Start()
    {
        SocketInteractor?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
        });
    }

    private void OpenDoor()
    { 
    
    }
   
}
