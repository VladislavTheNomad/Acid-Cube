using System.Collections;
using System.Timers;
using UnityEngine;

namespace AcidCube
{
    public class PressBlockMoving : MonoBehaviour
    {

        [SerializeField] private bool isMoving;
        [SerializeField] private float waitngTimeBetweenAnimations;
        [SerializeField] private bool isUpToDownMovement = true;
        //[SerializeField]
        //[SerializeField]

        private Animator anim;
        private Coroutine currentCoroutine;

        private void Awake()
        {
            anim = GetComponent<Animator>();

            anim.SetBool("isUpToDownMovement", true);


            if (isMoving)
            {
                anim.SetBool("hasPower", true);
                currentCoroutine = StartCoroutine(Moving());
            }
        }

        private IEnumerator Moving()
        {
            yield return new WaitForSeconds(waitngTimeBetweenAnimations);
            anim.SetTrigger("GoUp");
            yield return new WaitForSeconds(waitngTimeBetweenAnimations);
            anim.SetTrigger("GoDown");
            currentCoroutine = StartCoroutine(Moving());
        }
    }
}
