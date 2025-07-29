using UnityEngine;

namespace AcidCube
{
    public class BladeTrapAudio : MonoBehaviour
    {

        private AudioSource audioSource;
        [SerializeField] private AudioClip[] audioClips; // Idle [0], Off [1]

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void TurnOnSound()
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }

        public void TurnOffSound()
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }

    }
}
