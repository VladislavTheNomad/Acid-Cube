using UnityEngine;
using AcidCube;

public class AutoRotatingBlock : MonoBehaviour
{
    [SerializeField] public bool isRight = true;
    [SerializeField] private float speedOfRotating = 1f;

    void FixedUpdate()
    {
        if (isRight)
        {
            transform.Rotate(new Vector3(0f, 0f, speedOfRotating));
        }
        else
        {
            transform.Rotate(new Vector3(0f, 0f, -speedOfRotating));
        }
    }
}
