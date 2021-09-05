using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> musicTracks;

    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = musicTracks[Random.Range(0, musicTracks.Count)];

        audioSource.Play();
    }
}
