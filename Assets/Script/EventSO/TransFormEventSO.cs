using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[CreateAssetMenu(menuName = "Event/TransformEventSO")]

public class TransFormEventSO : ScriptableObject
{
    public UnityAction<Transform> OnTransformEventRaised;
    public void RaisedTransFormEvent(Transform transform)
    {
        OnTransformEventRaised?.Invoke(transform);
    }

}
