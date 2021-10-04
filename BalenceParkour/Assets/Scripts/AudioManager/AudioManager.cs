using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] audioClips;
    public AudioSource clipPlayer;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void ClipPlay(int index)
    {
        clipPlayer.loop = false;
        clipPlayer.clip = audioClips[index];
        clipPlayer.Play();
    }
}
