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
            PlayOnStart();
        });

        SimpleInteractable?.selectEntered.AddListener((s) =>
        {
            OpenDoor();
        });
    }

    public void OpenDoor()
    {
        if (!DoorOpen)
        { 
            PlayOnStart();
            DoorOpen = true;
            StartCoroutine(ProcessMotion());
        }
    }

    private IEnumerator ProcessMotion()
    {
        while (DoorOpen)
        {
            var angle = doorPivot.localEulerAngles.y < 100 ? doorPivot.localEulerAngles.y : doorPivot.localEulerAngles.y -360;
            angle = reversePivotDirection? Mathf.Abs(angle) : angle;
            if (angle <= maximumPivotAngle)
            {
                doorPivot?.Rotate(Vector3.up, DoorSpeed * Time.deltaTime * (reversePivotDirection ? -1 : 1));
            }
            
            else
            {
                DoorOpen = false;
                Rigidbody rigidbody = GetComponent<Rigidbody>();
                if (rigidbody != null && makeKinematicOnOpen)
                {
                    rigidbody.isKinematic = true;
                }  
            }

            yield return null;

        }
    }
}
