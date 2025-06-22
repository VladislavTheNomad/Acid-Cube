using UnityEngine;

public class AutoRotatingBlock : MonoBehaviour
{
    [SerializeField] private bool isRight = true;
       
    void FixedUpdate()
    {
        if (isRight)
        {
            transform.Rotate(new Vector3(0f, 0f, 1f));
        }
        else
        {
            transform.Rotate(new Vector3(0f, 0f, -1f));
        }
    }
}
