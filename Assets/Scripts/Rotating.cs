using UnityEngine;

public class Rotating : MonoBehaviour
{

    void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 1f);
    }
}
