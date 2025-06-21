using UnityEngine;

public class GlassBlock : MonoBehaviour
{
    [SerializeField] private bool isCracked = false;

    private Material mat;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > 10 || (collision.relativeVelocity.magnitude > 5 && collision.gameObject.CompareTag("Rock")))
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
