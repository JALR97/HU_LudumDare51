using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    
    [SerializeField] private AudioSource _audioSource;

    public void Play(AudioClip audioC) {
        _audioSource.PlayOneShot(audioC);
    }
    
}
