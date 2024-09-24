using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FeatureUsage
{ 
    Once,
    Toggle
}

public class CoreFeatures : MonoBehaviour
{
    //Proporties are declared like variables but formatted differently and can be accessed outside of the script
    //you can create and public varible to access them from another script
    //proporties are incapsulated and are formatted as fields
    //proporties have two way to acsess them
    //get accessor is reading a value
    //set accessor is writing a value

    public bool AudioSFXSourceCreated {  get; set; }

    
    //Audio Plays on door open
    [field: SerializeField] public AudioClip AudioClipOnStart { get; set; }

    //Audio plays on door close
    [field: SerializeField] public AudioClip AudioClipOnClose { get; set; }

    private AudioSource audioSource;

    public FeatureUsage featureUsage = FeatureUsage.Once;

    protected virtual void Awake()
    { 
        MakeSFXAudioScource();
    }

    private void MakeSFXAudioScource()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
}
