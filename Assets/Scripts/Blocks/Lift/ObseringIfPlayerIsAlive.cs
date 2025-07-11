using UnityEngine;

namespace AcidCube
{
    public class ObseringIfPlayerAlive : MonoBehaviour
    {
        [SerializeField] private BuildPathForLift liftScript;

        private void OnEnable()
        {
            GameOverMenu.OnGameOverTriggered += ResetLift;
        }

        private void ResetLift()
        {
            liftScript.StopMovement();

            liftScript.transform.Translate(liftScript.pathPoints[0]);
            liftScript.currentPointIndex = 0;

            if (liftScript.originalPowerMode == true)
            {
                liftScript.GetPower();
            }
        }
    }
}
