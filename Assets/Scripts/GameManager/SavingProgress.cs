using UnityEngine;

namespace AcidCube
{
    public class SavingProgress : MonoBehaviour
    {
        public GameObject pointOfRestart;

        public void SavePlayerRevivingPoint(Vector3 pointRebirth)
        {
            pointOfRestart.transform.position = pointRebirth;
        }
    }
}
