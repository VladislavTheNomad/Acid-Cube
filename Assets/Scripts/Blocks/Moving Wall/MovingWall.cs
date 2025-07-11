using System.Collections;
using UnityEngine;
using AcidCube;

public class MovingWall : MonoBehaviour
{

    // internal
    private bool startOpening = true;
    private bool startClosing = false;

    private float startPositionY;
    private float endPositionY;
    private float currentY;

    //settings
    [SerializeField] private float timeForWallMoving = 2f;
    [SerializeField] private float movePositionY;

    private void Awake()
    {
        startPositionY = transform.localPosition.y;
        endPositionY = startPositionY + movePositionY;
    }

    private void FixedUpdate()
    {
        if(startOpening)
        {
            startOpening = false;
            StartCoroutine(WallMoving());
        }
        else if (startClosing)
        {
            startClosing = false;
            StartCoroutine(WallClosing());
        }
    }

    private IEnumerator WallMoving()
    {
        float elapsedTimeForWall = 0f;
        float nowPositionY = transform.localPosition.y; // get local Y 
        while (elapsedTimeForWall < timeForWallMoving)
        {
            elapsedTimeForWall += Time.deltaTime;
            currentY = Mathf.Lerp(nowPositionY, endPositionY, elapsedTimeForWall / timeForWallMoving);
            transform.localPosition = new Vector3(transform.localPosition.x, currentY, transform.localPosition.z);
            yield return null;
        }
        startClosing = true;
    }

    IEnumerator WallClosing()
    {
        float elapsedTimeForSpike = 0f;
        float nowPositionY = transform.localPosition.y; // get local Y 
        while (elapsedTimeForSpike < timeForWallMoving)
        {
            elapsedTimeForSpike += Time.deltaTime;
            currentY = Mathf.Lerp(nowPositionY, startPositionY, elapsedTimeForSpike / timeForWallMoving);
            transform.localPosition = new Vector3(transform.localPosition.x, currentY, transform.localPosition.z);
            yield return null;
        }
        startOpening = true;
    }
}
