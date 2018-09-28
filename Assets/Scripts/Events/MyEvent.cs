using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter
{
    public class MyEvent
    {
        public delegate void eventAction(params object[] obj);
        public delegate void intAction(int value);
        public delegate void stringAction(string value);
        public delegate void voidAction();

        public event eventAction EventAction;
        public event intAction EventActionInt;
        public event stringAction EventActionString;
        public event voidAction EventActionVoid;

        public MyEvent()
        {
        }

        public void Dispatch(params object[] obj)
        {
            if (EventAction != null)
                EventAction(obj);
            else
                XDebug.Log("Event is Null\nPlease Initialize event first", XDebug.Mask.MyEvent, XDebug.Color.Red);
        }

        public void Dispatch(int value)
        {
            if (EventActionInt != null)
                EventActionInt(value);
            else
                XDebug.Log("Event is Null\nPlease Initialize event first", XDebug.Mask.MyEvent, XDebug.Color.Red);
        }

        public void Dispatch(string value)
        {
            if (EventActionString != null)
                EventActionString(value);
            else
                XDebug.Log("Event is Null\nPlease Initialize event first", XDebug.Mask.MyEvent, XDebug.Color.Red);
        }

        public void Dispatch()
        {
            if (EventActionVoid != null)
                EventActionVoid();
            else
                XDebug.Log("Event is Null\nPlease Initialize event first", XDebug.Mask.MyEvent, XDebug.Color.Red);
        }

    }
}