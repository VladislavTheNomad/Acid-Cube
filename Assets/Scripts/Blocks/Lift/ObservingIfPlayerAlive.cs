using UnityEngine;

namespace AcidCube
{
    public class ObservingIfPlayerAlive : MonoBehaviour
    {
        [SerializeField] private BuildPathForLift liftScript;
        [SerializeField] private LiftMoving liftMovingScript;

        private void OnEnable()
        {
            GameOverMenu.OnGameOverTriggered += ResetLift;
        }

        private void ResetLift()
        {
            liftMovingScript.StopMovement();

            liftMovingScript.rb.MovePosition(liftScript.pathPoints[0]);
            liftScript.currentPointIndex = 0;

            if (liftMovingScript.originalPowerMode == true)
            {
                liftMovingScript.GetPower();
            }
        }
    }
}
