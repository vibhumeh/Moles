using System;
using System.Collections;
using UnityEngine;
using Vuforia;

public class MoleBehavior : MonoBehaviour
{
    private Vector3 downPosition;
    private Vector3 upPosition;
    private bool isActive = false;
    public bool hit=false;
    public Action OnHit; // Event for when mole is hit
    public Action OnMissed; // Event for when mole is missed

    private void Start()
    {
        downPosition = transform.localPosition;
        upPosition = new Vector3(downPosition.x, 0.07f, downPosition.z);
    }

    public void PopUp(float moveSpeed, float activeDuration)
    {
        if (isActive) return;
        StartCoroutine(PopUpRoutine(moveSpeed, activeDuration));
    }

    private IEnumerator PopUpRoutine(float moveSpeed, float activeDuration)
    {   Debug.Log("coming out");
        isActive = true;
        hit=false;
        // Move up
        float time = 0;
        while (time < 1)
        {
            transform.localPosition = Vector3.Lerp(downPosition, upPosition, time);
            time += Time.deltaTime * moveSpeed;
            yield return null;
        }
        transform.localPosition = upPosition;

        // Wait for active duration
        Debug.Log(activeDuration);
        yield return new WaitForSeconds(10000000);

        // Miss logic: If mole wasn't hit in time
        //if (isActive && OnMissed != null)
        

        // Move down
        time = 0;
        while (time < 1)
        {
            transform.localPosition = Vector3.Lerp(upPosition, downPosition, time);
            time += Time.deltaTime * moveSpeed;
            yield return null;
        }
        transform.localPosition = downPosition;
        if (!hit)
        {
            Debug.Log("MISSED BABY");
            OnMissed.Invoke();
        }
        isActive = false;
    }

    private void OnMouseDown()
    {
        if (isActive && OnHit != null)
        {
            Debug.Log("HIT BABY!");
            OnHit.Invoke(); // Trigger hit event
            hit=true;
            isActive = false; // Prevent multiple hits
        }
    }
}

 