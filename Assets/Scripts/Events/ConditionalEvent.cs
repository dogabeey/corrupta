using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEvent
{
    public delegate bool Con();
    public delegate void Effect();

    public class ConditionalEvent : MonoBehaviour
    {
        public List<Con> dels;
        public Effect effect;

        void ExecuteEvent()
        {
            foreach (Con d in dels)
            {
                if (!d())
                {
                    return;
                }
            }

            effect();
        }
    }
}
