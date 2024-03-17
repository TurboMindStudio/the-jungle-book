using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;
   public AudioSource audioSource;
    public AudioSource trainRun;
    public AudioClip trainStop;
    
    public void Awake()
    {
        instance = this;
    }
}
