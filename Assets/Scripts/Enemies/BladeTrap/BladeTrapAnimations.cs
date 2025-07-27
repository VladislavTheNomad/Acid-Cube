using UnityEngine;

namespace AcidCube
{
    public class BladeTrapAnimations : MonoBehaviour
    {
        [SerializeField] private MovingOfBladeTrap movingScript;
        [SerializeField] private BladeTrapPathConstructor constructorScript;

        [SerializeField] private GameObject sparklesToRight;
        [SerializeField] private GameObject sparklesToLeft;

        public void AnimationChanger(Direction direction)
        {
            if (constructorScript.isRightSideTrap || constructorScript.isLeftSideTrap)
            {
                if (movingScript.currentDirection == Direction.Left)
                {
                    sparklesToRight.SetActive(true);
                    sparklesToLeft.SetActive(false);
                }
                else
                {
                    sparklesToRight.SetActive(false);
                    sparklesToLeft.SetActive(true);
                }
            }
            else
            {
                if (movingScript.currentDirection == Direction.Right)
                {
                    sparklesToRight.SetActive(true);
                    sparklesToLeft.SetActive(false);
                }
                else
                {
                    sparklesToRight.SetActive(false);
                    sparklesToLeft.SetActive(true);
                }
            }
        }
    }
}
