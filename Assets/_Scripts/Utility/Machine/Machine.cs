using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

/// <summary>
///     MUST be constructed in Awake(), so that any class using Start() can subscribe to it.
/// </summary>
public abstract class Machine<T> where T : Enum
{
    protected readonly Dictionary<T, List<WorkUnit>> enterWorkUnits;
    protected readonly Dictionary<T, List<WorkUnit>> exitWorkUnits;

    protected readonly MonoBehaviour Owner;

    protected MonoBehaviour SettingOwner;
    protected T SettingState;

    public Machine(MonoBehaviour owner)
    {
        enterWorkUnits = new Dictionary<T, List<WorkUnit>>();
        exitWorkUnits = new Dictionary<T, List<WorkUnit>>();

        foreach (T value in Enum.GetValues(typeof(T)))
        {
            enterWorkUnits[value] = new List<WorkUnit>();
            exitWorkUnits[value] = new List<WorkUnit>();
        }

        Owner = owner;
    }

    private void Queue(T workType, WorkUnit enterWorkUnit, WorkUnit exitWorkUnit)
    {
        if (enterWorkUnit != null) enterWorkUnits[workType].Add(enterWorkUnit);
        if (exitWorkUnit != null) exitWorkUnits[workType].Add(exitWorkUnit);
    }

    protected void QueueAction(MonoBehaviour owner, T workType, Action enterWork, Action exitWork)
    {
        Queue(workType,
            enterWork != null ? new SynchronousWorkUnit(owner, workType, enterWork) : null,
            exitWork != null ? new SynchronousWorkUnit(owner, workType, exitWork) : null);
    }

    protected void QueueCoroutine(MonoBehaviour owner, T workType, Func<IEnumerator> enterWork, Func<IEnumerator> exitWork)
    {
        Queue(workType,
            enterWork != null ? new AsynchronousWorkUnit(owner, workType, enterWork) : null,
            exitWork != null ? new AsynchronousWorkUnit(owner, workType, exitWork) : null);
    }

    protected void QueueTween(MonoBehaviour owner, T workType, Tween enterWork, Tween exitWork)
    {
        enterWork.Pause();
        exitWork.Pause();

        Queue(workType,
            enterWork != null ? new TweenWorkUnit(owner, workType, enterWork) : null,
            exitWork != null ? new TweenWorkUnit(owner, workType, exitWork) : null);
    }

    public void RemoveAllWorksOf(MonoBehaviour owner)
    {
        foreach (var list in enterWorkUnits.Values) list.RemoveAll(workUnit => workUnit.Owner == owner);

        foreach (var list in exitWorkUnits.Values) list.RemoveAll(workUnit => workUnit.Owner == owner);
    }

    protected virtual IEnumerator RunEnterWorkQueue_CO()
    {
        yield return null;

        foreach (T value in Enum.GetValues(typeof(T)))
        {
            Debug.Log(Owner.name + "'s machine running enter works of state: " + value);
            yield return Owner.StartCoroutine(RunQueueOfType(value, enterWorkUnits));
        }
    }

    protected virtual IEnumerator RunExitWorkQueue_CO()
    {
        yield return null;

        foreach (T value in Enum.GetValues(typeof(T)))
        {
            Debug.Log(Owner.name + "'s machine running exit works of state: " + value);
            yield return Owner.StartCoroutine(RunQueueOfType(value, exitWorkUnits));
        }
    }

    protected virtual IEnumerator RunWorkQueue_CO()
    {
        yield return null;

        foreach (T value in Enum.GetValues(typeof(T)))
        {
            Debug.Log(Owner.name + "'s machine running all works of state: " + value);
            yield return Owner.StartCoroutine(RunQueueOfType(value, enterWorkUnits));
            yield return Owner.StartCoroutine(RunQueueOfType(value, exitWorkUnits));
        }
    }

    protected IEnumerator RunQueueOfType(T workType, Dictionary<T, List<WorkUnit>> workQueue)
    {
        List<WorkUnit> discardingWorkUnits = new();

        List<Coroutine> coroutines = new();
        var mainSequence = DOTween.Sequence().Pause();
        foreach (var workUnit in workQueue[workType])
            // BUG: Terrible coding strategy?
            try
            {
                switch (workUnit)
                {
                    case SynchronousWorkUnit synchronousWorkUnit:
                        synchronousWorkUnit.Work.Invoke();
                        break;
                    case AsynchronousWorkUnit asynchronousWorkUnit:
                        var coroutine = Owner.StartCoroutine(asynchronousWorkUnit.Work.Invoke());
                        coroutines.Add(coroutine);
                        break;
                    case TweenWorkUnit tweenWorkUnit:
                        mainSequence.Insert(0f, tweenWorkUnit.Work);
                        break;
                }
            }
            catch (MissingReferenceException _)
            {
                discardingWorkUnits.Add(workUnit);
                Debug.LogWarning(Owner.name + "'s machine has caught a MissingReferenceException.");
            }

        // Bug: Is this really parallel?
        foreach (var coroutine in coroutines) yield return coroutine;

        yield return mainSequence.Play().AsyncWaitForCompletion();

        foreach (var discardingWorkUnit in discardingWorkUnits) workQueue[workType].Remove(discardingWorkUnit);
    }

    protected abstract class WorkUnit
    {
        public readonly MonoBehaviour Owner;

        public WorkUnit(MonoBehaviour owner, T workType)
        {
            Owner = owner;
            WorkType = workType;
        }

        public T WorkType { get; }
    }

    protected class SynchronousWorkUnit : WorkUnit
    {
        public SynchronousWorkUnit(MonoBehaviour owner, T workType, Action work) : base(owner, workType)
        {
            Work = work;
        }

        public Action Work { get; }
    }

    protected class AsynchronousWorkUnit : WorkUnit
    {
        public AsynchronousWorkUnit(MonoBehaviour owner, T workType, Func<IEnumerator> work) : base(owner, workType)
        {
            Work = work;
        }

        public Func<IEnumerator> Work { get; }
    }

    protected class TweenWorkUnit : WorkUnit
    {
        public TweenWorkUnit(MonoBehaviour owner, T workType, Tween work) : base(owner, workType)
        {
            Work = work;
        }

        public Tween Work { get; }
    }
}

public interface IMachineUser
{
    /// <summary>
    ///     Add all works in the body and call the method in Start().
    /// </summary>
    public void QueueWork();

    /// <summary>
    ///     Call the method in OnDestroy().
    /// </summary>
    public void DequeueWork();
}