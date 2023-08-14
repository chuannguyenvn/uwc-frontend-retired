using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForAll : CustomYieldInstruction
{
    // Only wait until all coroutines have finished
    public override bool keepWaiting => _done.NotDone();

    private readonly Done _done;

    public WaitForAll(MonoBehaviour monoBehaviour, params IEnumerator[] coroutines)
    {
        _done = new Done(coroutines.Length);

        // Start all wrapped coroutines
        foreach (var coroutine in coroutines)
        {
            monoBehaviour.StartCoroutine(WaitForCoroutine(monoBehaviour, coroutine, _done));
        }
    }

    // Keeps track of number of coroutines still running
    private class Done
    {
        private int n;

        public Done(int n)
        {
            this.n = n;
        }

        public void CoroutineDone()
        {
            n--;
        }

        public bool NotDone()
        {
            return n != 0;
        }
    }

    // Coroutine wrapper to track when a coroutine has finished
    private IEnumerator WaitForCoroutine(MonoBehaviour monoBehaviour, IEnumerator coroutine, Done done)
    {
        yield return monoBehaviour.StartCoroutine(coroutine);
        done.CoroutineDone();
    }
}