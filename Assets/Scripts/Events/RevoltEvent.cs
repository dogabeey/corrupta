//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;
//using UnityEngine.EventSystems;

//public class RevoltEvent : RandomEvent
//{
//    public RevoltEvent(string eventCaption, string eventDescription, int duration) : base(eventCaption, eventDescription, duration)
//    {

//    }

//    public RevoltEvent() : base()
//    {

//    }

//    public override double CalculateProbability()
//    {
//        return Math.Tanh((double)Country.Instance.GetStability()) / 4;
//    }

//    public override bool Effect()
//    {
//        if (Duration > 0)
//        {
//            Country.Instance.baseCulture--;
//            Duration--;
//            return true;
//        }
//        return false;
//    }
//}
