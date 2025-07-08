using AcidCube;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<PlayerController>()) return;
        GameOverMenu.instance.OpenGameOverMenu();
    }
}