using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StateMachine<T> : Machine<T> where T : Enum
{
    public StateMachine(MonoBehaviour owner, T startingState) : base(owner)
    {
        _stateQueue.Enqueue(startingState);
    }

    public T CurrentState { get; private set; }
    private Queue<T> _stateQueue = new();
    private bool started = false;
    private bool isChangingState = false;

    public void Start()
    {
        if (started) Debug.LogError(Owner.name + "'s state machine is started multiple time.");
        else if (isChangingState) Debug.LogError(Owner.name + "'s state machine is started while changing state.");
        else Owner.StartCoroutine(StartMachine_CO());
    }

    public void Stop()
    {
        if (!started) Debug.LogError(Owner.name + "'s state machine is ended before started.");
        else if (isChangingState) Debug.LogError(Owner.name + "'s state machine is ended while changing state.");
        else
        {
            Owner.StopCoroutine(StartMachine_CO());
            Owner.StartCoroutine(EndMachine_CO());
        }
    }

    private IEnumerator StartMachine_CO()
    {
        yield return null;
        started = true;

        var startingState = _stateQueue.Dequeue();
        Debug.Log(Owner.name + " is started with the state: " + startingState);
        yield return Owner.StartCoroutine(RunQueueOfType(startingState, enterWorkUnits));

        while (true)
        {
            if (_stateQueue.Count > 0)
            {
                var newState = _stateQueue.Dequeue();
                yield return Owner.StartCoroutine(ChangeState_CO(newState));
                CurrentState = newState;
            }
            else yield return null;
        }
    }

    private IEnumerator EndMachine_CO()
    {
        yield return null;
        started = false;

        Debug.Log(Owner.name + " is ended with the state: " + CurrentState);
        yield return Owner.StartCoroutine(RunQueueOfType(CurrentState, exitWorkUnits));
    }

    public void ChangeState(T newState)
    {
        Debug.Log(Owner.name + " enters state: " + newState);
        if (!started) Debug.LogError("State machine changed state before starting.");
        // if (isChangingState)
        //     Debug.LogError("State machine changed state when changing to another state. Tried to change from " +
        //                    CurrentState + " to " + newState + ".");

        _stateQueue.Enqueue(newState);
    }

    private IEnumerator ChangeState_CO(T newState)
    {
        isChangingState = true;
        Debug.Log(Owner.name + " executing OnExits of " + CurrentState);
        yield return Owner.StartCoroutine(RunQueueOfType(CurrentState, exitWorkUnits));
        Debug.Log(Owner.name + " executing OnEntrys of " + newState);
        yield return Owner.StartCoroutine(RunQueueOfType(newState, enterWorkUnits));
        isChangingState = false;

        CurrentState = newState;
    }

    public StateMachine<T> Configure(MonoBehaviour owner, T configureState)
    {
        SettingOwner = owner;
        SettingState = configureState;
        return this;
    }

    public StateMachine<T> OnEntry(Action entryWork)
    {
        QueueAction(SettingOwner, SettingState, entryWork, null);
        return this;
    }

    public StateMachine<T> OnEntry(Func<IEnumerator> entryWork)
    {
        QueueCoroutine(SettingOwner, SettingState, entryWork, null);
        return this;
    }

    public StateMachine<T> OnEntry(Tween entryWork)
    {
        QueueTween(SettingOwner, SettingState, entryWork, null);
        return this;
    }

    public StateMachine<T> OnExit(Action exitWork)
    {
        QueueAction(SettingOwner, SettingState, null, exitWork);
        return this;
    }

    public StateMachine<T> OnExit(Func<IEnumerator> exitWork)
    {
        QueueCoroutine(SettingOwner, SettingState, null, exitWork);
        return this;
    }

    public StateMachine<T> OnExit(Tween exitWork)
    {
        QueueTween(SettingOwner, SettingState, null, exitWork);
        return this;
    }
}