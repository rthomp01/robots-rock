using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BaseController : MonoBehaviour
{
    public FiniteStateMachine StateMachine { get; private set; }

    protected virtual void Awake()
    {
        StateMachine = new FiniteStateMachine();
    }
}
