using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ButtonHandler : MonoBehaviour
{
    [Header("Play Audio when hovered")]
    AudioSource audioSource;
    public AudioClip audioHover;
    public AudioClip audioClick;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void PlayAudioHovered()
    {
        audioSource.PlayOneShot(audioHover);
    }

    public void PlayAudioClicked()
    {
        audioSource.PlayOneShot(audioClick);
    }
}
