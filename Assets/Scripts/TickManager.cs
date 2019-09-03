using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TickManager : MonoBehaviour
{
    // the speed that the node moves at
    public static float tickSpeed;
    public delegate void Tick();
    public static Tick OnTick;

    public Text speedLabel;

    float minSpeed = .5f;
    float maxSpeed = 8;
    float startSpeed = 2;

    private void Start()
    {
        StartCoroutine(StartTickRoutine());
        tickSpeed = startSpeed;
        speedLabel.text = tickSpeed + "";
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

    public void SpeedUp()
    {
        tickSpeed *= 2;
        if(tickSpeed >= maxSpeed)
        {
            tickSpeed = maxSpeed;
        }
        speedLabel.text = tickSpeed + "";
    }

    public void SlowDown()
    {
        tickSpeed /= 2;
        if (tickSpeed <= minSpeed)
        {
            tickSpeed = minSpeed;
        }
        speedLabel.text = tickSpeed + "";
    }
}
