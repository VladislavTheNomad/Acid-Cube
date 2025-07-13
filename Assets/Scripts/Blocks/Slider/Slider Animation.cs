using System.Collections;
using UnityEngine;

namespace AcidCube
{
    public class SliderAnimation : MonoBehaviour
    {
        [SerializeField] private bool isMoving;
        [SerializeField] private float waitngTimeBetweenAnimations;
        [SerializeField] private float delayBeforeStart;

        private Animator _animator;
        private Coroutine currentCoroutine;


        private void Awake()
        {
            _animator = GetComponent<Animator>();

            if(isMoving)
            {
                currentCoroutine = StartCoroutine(MovingAnim());
            }
        }

        private IEnumerator MovingAnim()
        {
            yield return new WaitForSeconds(delayBeforeStart);
            _animator.SetTrigger("Start");

            while(isMoving)
            {
                yield return new WaitForSeconds(waitngTimeBetweenAnimations);
                _animator.SetTrigger("GoOpen");
                yield return new WaitForSeconds(waitngTimeBetweenAnimations);
                _animator.SetTrigger("GoClose");
            }
            currentCoroutine = null;
        }
    }
}

    
    
