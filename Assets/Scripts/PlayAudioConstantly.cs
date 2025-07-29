using AcidCube;
using UnityEngine;

public class PlayAudioConstantly : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        audioSource.Play();
    }

}
