using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultCompany.Singleton;
using System;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [Header("Backround Music")]
    
    [SerializeField]
    private AudioClip[] Tracks;
    private AudioSource AudioSource;

    [Header("Audio Events")]
    public Action onCurrentTrackEnd;

    public void Awake()
    {
        AudioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShuffleOnTrackEnd());
        ShuffleAndPlay();
    }
    public IEnumerator ShuffleOnTrackEnd()
    {
        while (true)
        {
            yield return new WaitUntil(() => !AudioSource.isPlaying);
            ShuffleAndPlay();
            onCurrentTrackEnd?.Invoke();
        }
    }
    private void ShuffleAndPlay(GameState gameState = GameState.Playing)
    {
        if (Tracks.Length > 0)
        {
            UnityEngine.Random.InitState(DateTime.Now.Millisecond);
            AudioSource.clip = Tracks[UnityEngine.Random.Range(0 , Tracks.Length -1)];
            AudioSource.Play();
        }
    }
}
