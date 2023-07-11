using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class InitMachine<T> : Machine<T> where T : Enum
{
    private bool isRunning;

    public InitMachine(MonoBehaviour owner) : base(owner)
    {
    }

    public bool Completed { get; private set; }

    public void StartWorkQueue()
    {
        if (isRunning) Debug.LogError(Owner.name + "'s initMachine is started multiple time.");
        if (Completed) Debug.LogError(Owner.name + "'s initMachine is started before reverted.");
        Owner.StartCoroutine(RunEnterWorkQueue_CO());
    }

    public void RevertWorkQueue()
    {
        if (isRunning) Debug.LogError(Owner.name + "'s initMachine is reverted when running.");
        if (!Completed) Debug.LogError(Owner.name + "'s initMachine is reverted before completed.");
        Owner.StartCoroutine(RunExitWorkQueue_CO());
    }

    protected override IEnumerator RunEnterWorkQueue_CO()
    {
        isRunning = true;
        yield return base.RunEnterWorkQueue_CO();
        isRunning = false;
        Completed = true;
    }

    protected override IEnumerator RunExitWorkQueue_CO()
    {
        isRunning = true;
        yield return base.RunExitWorkQueue_CO();
        isRunning = false;
        Completed = false;
    }

    public InitMachine<T> Configure(MonoBehaviour owner, T configureState)
    {
        SettingOwner = owner;
        SettingState = configureState;
        return this;
    }

    public InitMachine<T> Queue(Action work)
    {
        QueueAction(SettingOwner, SettingState, work, null);
        return this;
    }

    public InitMachine<T> Queue(Func<IEnumerator> work)
    {
        QueueCoroutine(SettingOwner, SettingState, work, null);
        return this;
    }

    public InitMachine<T> Queue(Tween work)
    {
        QueueTween(SettingOwner, SettingState, work, null);
        return this;
    }
}