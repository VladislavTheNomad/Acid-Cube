using UnityEngine;

namespace AcidCube
{
    public class PlayAudioOnInteraction : MonoBehaviour
    {
        private AudioSource audioSource;
        [Header("onEnter [0], onExit [1], onAction [2]")]
        [SerializeField] private AudioClip[] audioClips; // onEnter [0], onExit [1], onAction [2]
        

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        // USE AUDIO ON ACTION

        public void PlayonAction()
        {
            if (audioClips.Length > 2)
            {
                audioSource.clip = audioClips[2];
                audioSource.Play();
            }

        }

        // USE AUDIO ON ENTER / EXIT

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.GetComponent<PlayerController>()) return;

            audioSource.clip = audioClips[0];
            audioSource.Play();
        }

        private void OnCollisionExit(Collision collision)
        {
            if (!collision.gameObject.GetComponent<PlayerController>()) return;

            if (audioClips.Length > 0)
            {
                audioSource.clip = audioClips[1];
                audioSource.Play();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            audioSource.clip = audioClips[0];
            audioSource.Play();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.GetComponent<PlayerController>()) return;

            if (audioClips.Length > 0)
            {
                audioSource.clip = audioClips[1];
                audioSource.Play();
            }
        }

    }
}
