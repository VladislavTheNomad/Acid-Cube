using System.Collections;
using UnityEditor;
using UnityEngine;

namespace AcidCube
{
    public class StartTheAnimation : MonoBehaviour
    {
        private Animator anim;

        [SerializeField] private float animationDelay;

        private void Awake()
        {
            anim = gameObject.GetComponent<Animator>();

            StartCoroutine(PauseAnim());
        }

        private IEnumerator PauseAnim()
        {
            yield return new WaitForSeconds(animationDelay);
            anim.SetBool("hasStart", true);
        }
    }
}
