using UnityEngine;
using AcidCube;

public class AutoRotatingPartOfBladeTrap : MonoBehaviour
{
    [SerializeField] private MovingOfBladeTrap movingScript;
    [SerializeField] private GameObject movingPartOfBladeTrap;

    [SerializeField] private float speedOfRotating = 1f;

    void FixedUpdate()
    {
        if (movingScript.currentDirection == Direction.Right)
        {
            movingPartOfBladeTrap.transform.Rotate(new Vector3(0f, 0f, speedOfRotating));
        }
        else
        {
            movingPartOfBladeTrap.transform.Rotate(new Vector3(0f, 0f, -speedOfRotating));
        }
    }
}
