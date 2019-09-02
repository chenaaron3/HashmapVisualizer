using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    // the speed that the node moves at
    public static float tickSpeed = 4;
    public delegate void Tick();
    public static Tick OnTick;

    private void Start()
    {
        StartCoroutine(StartTickRoutine());
    }

    // starts ticking a frame later
    IEnumerator StartTickRoutine()
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(TickRoutine());
    }

    IEnumerator TickRoutine()
    {
        // everything revolves around the tick event
        OnTick?.Invoke();
        yield return new WaitForSeconds(1 / tickSpeed);
        // loops the tick
        StartCoroutine(TickRoutine());
    }
}
