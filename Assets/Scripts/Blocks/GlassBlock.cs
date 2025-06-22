using UnityEngine;

public class GlassBlock : MonoBehaviour
{
    [SerializeField] private bool isCracked = false;

    private Material mat;

    private void Awake()
    {
        if (!isCracked)
        {
            mat = GetComponent<Renderer>().material;
            mat.SetFloat("_BumpScale", 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > 8 || (collision.relativeVelocity.magnitude > 2 && collision.gameObject.CompareTag("Rock")))
        {
            if (isCracked)
            {
                Destroy(gameObject);
            }
            else
            {
                isCracked = true;
                mat = GetComponent<Renderer>().material;
                mat.SetFloat("_BumpScale", 1f);
            }
        }
    }
}
