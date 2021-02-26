﻿using System;
using UnityEngine;

namespace MUtils.StateMachine
{
    public abstract class State : MonoBehaviour
    {
        [NonSerialized] public StateMachine StateMachine;
        public abstract void StateEnter();
        public abstract void StateUpdate();
        public abstract void StateFixedUpdate();
        public abstract void StateExit();
    }
}
