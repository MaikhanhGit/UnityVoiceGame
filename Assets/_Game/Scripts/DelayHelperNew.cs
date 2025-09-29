using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class DelayHelperNew
{
    public static Coroutine DelayAction(this MonoBehaviour monobehavior, 
        Action action, float delayDuration)
    {
        return monobehavior.StartCoroutine(DelayActionRoutine(action, delayDuration));
    }

    private static IEnumerator DelayActionRoutine(Action action, float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);
        action();
    }
}
