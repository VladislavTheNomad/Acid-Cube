using System.Collections;
using UnityEditor;
using UnityEngine;

namespace AcidCube
{
    public class StartTheAnimation : MonoBehaviour
    {

        [SerializeField] private float delayBeforeStart;

        private Animator anim;

        private void Awake()
        {
            anim = gameObject.GetComponent<Animator>();

            StartCoroutine(PauseAnim());
        }

        private IEnumerator PauseAnim()
        {
            yield return new WaitForSeconds(delayBeforeStart);
            anim.SetBool("hasStart", true);
        }
    }
}
