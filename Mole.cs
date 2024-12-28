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
    private IEnumerator Move(int direction, float moveSpeed)
{
    Vector3 startPosition = transform.localPosition; // Current position
    Vector3 targetPosition = direction == 1 ? upPosition : downPosition; // Determine direction
    float time = 0;
    float duration = 1f / moveSpeed; // Duration of the movement

    while (time < duration)
    {
        transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
        time += Time.deltaTime;
        yield return null;
    }
    transform.localPosition = targetPosition; // Ensure exact final position
}

    

    private IEnumerator PopUpRoutine(float moveSpeed, float activeDuration)

    {   
         Debug.Log("coming out");
        isActive = true;
        hit=false;
        yield return StartCoroutine(Move(1, moveSpeed)); 
        transform.localPosition = upPosition;

        // Wait for active duration
        float elapsedTime = 0f;
        while (elapsedTime < activeDuration && !hit)
        {
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }
        yield return StartCoroutine(Move(0, moveSpeed));
        // Check if the mole was not hit within the active duration

        // Miss logic: If mole wasn't hit in time
        //if (isActive && OnMissed != null)
        

        // Move down
       
        transform.localPosition = downPosition;

        // if (!hit)
        // {
        //     Debug.Log("MISSED BABY");
        //     OnMissed.Invoke();
        // }
        yield return new WaitForSeconds(4);
        isActive = false;
    }
    private IEnumerator wait_boss(){
         yield return new WaitForSeconds(4);
         isActive=false;
    }
    private void OnMouseDown()
    {
        if (isActive && OnHit != null)
        {
            Debug.Log("HIT BABY!");
            OnHit.Invoke(); // Trigger hit event
            hit=true;

           wait_boss();

        }
    }
}
