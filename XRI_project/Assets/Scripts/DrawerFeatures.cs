using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerFeatures : CoreFeatures
{

    [Header("Drawer Atributes")]
    public Transform transform;
    public float Speed = 1.0f;
    public float MaximumDistance = .5f;
    public FeatureDirection Direction;
    public bool Open;
    public XRSimpleInteractable Interactable;
    private Vector3 InitialPosition;
    private float DrawerMinLimit;
    private float DrawerMaxLimit;

    void Start()
    {
        InitialPosition = transform.localPosition;

        if (Direction == FeatureDirection.Forward)
        {
            DrawerMinLimit = InitialPosition.z;
            DrawerMaxLimit = InitialPosition.z + MaximumDistance;
        }
        else
        {
            DrawerMinLimit = InitialPosition.z - MaximumDistance;
            DrawerMaxLimit = InitialPosition.z;
        }
        
        Interactable?.selectEntered.AddListener((s) =>
        {
            if (!Open)
            {
                OpenDrawer();
            }
            else
            {
                CloseDrawer();
            }
        });
    }
    void OpenDrawer()
    {
        Open = true;
        PlayOnStart();
        StopAllCoroutines();
        StartCoroutine(ProcessMotion());
    }
    void CloseDrawer()
    { 
        Open = false;
        PlayOnStart();
        StopAllCoroutines();
        StartCoroutine(ProcessMotion());
    }
    private IEnumerator ProcessMotion()
    {
        Vector3 TargetPosition = Open ? new Vector3(transform.localPosition.x, transform.localPosition.y, DrawerMaxLimit) : InitialPosition;
        Debug.Log(TargetPosition);
        while (transform.localPosition != TargetPosition)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPosition, Time.deltaTime * Speed);
            
            //float clampedZ = Mathf.Clamp(transform.localPosition.z, DrawerMinLimit, DrawerMaxLimit);

            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, clampedZ);
            
            yield return null;
        }
        
    }
}
