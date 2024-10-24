using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DrawerFeatures : CoreFeatures
{

    [Header("Drawer Atributes")]
    public float Speed = 1.0f;
    public float MaximumDistance = .5f;
    public FeatureDirection Direction;
    public bool Open;
    public XRSimpleInteractable Interactable;

    void Start()
    {
        Interactable?.selectEntered.AddListener((s) =>
        {
            if (!Open)
            {
                OpenDrawer();
            }
        });

    }
    void OpenDrawer()
    {
        Open = true;
        PlayOnStart();
        StartCoroutine(ProcessMotion());
    }
    private IEnumerator ProcessMotion()
    {
        while (Open)
        {
            if (Direction == FeatureDirection.Forward
                && transform.localPosition.z <= MaximumDistance)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * Speed);
            }
            else if (Direction == FeatureDirection.Backward
                && transform.localPosition.z >= MaximumDistance)
            {
                transform.Translate(Vector3.back * Time.deltaTime * Speed);
            }
            else
            {
                Open = false;
            }
            
            yield return null;
        }
    }
}
