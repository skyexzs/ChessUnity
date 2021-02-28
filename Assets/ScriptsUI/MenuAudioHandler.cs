using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MenuAudioHandler : MonoBehaviour
{
    public List<AudioClip> audioClips;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioSource(int _index)
    {
        audioSource.PlayOneShot(audioClips[_index]);
    }
}
