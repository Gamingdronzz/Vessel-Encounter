using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VesselEncounter
{
    public abstract class AbstractEvent
    {
        public int DispatchCount { get; private set; }

        public abstract bool HasDuplicateParameterTypes();

        protected void OnDispatch()
        {
            DispatchCount++;
        }
    }

    public class MyEvent : AbstractEvent
    {
        private System.Action onTrigger;

        public MyEvent()
        {
        }

        public void Dispatch()
        {
            if (onTrigger != null)
            {
                onTrigger();
            }
            OnDispatch();
        }

        public void AddListener(System.Action handler)
        {
            onTrigger += handler;
        }

        public void RemoveListener(System.Action handler)
        {
            onTrigger -= handler;
        }

        public override bool HasDuplicateParameterTypes()
        {
            return false;
        }
    }

    /// <typeparam name="T">Type of the first parameter.</typeparam>
    public class MyEvent<T> : AbstractEvent
    {
        private System.Action<T> onTrigger;

        public MyEvent()
        {
        }

        public void Dispatch(T value)
        {
            if (onTrigger != null)
            {
                onTrigger(value);
            }
            OnDispatch();
        }

        public void AddListener(System.Action<T> handler)
        {
            onTrigger += handler;
        }

        public void RemoveListener(System.Action<T> handler)
        {
            onTrigger -= handler;
        }

        public override bool HasDuplicateParameterTypes()
        {
            return false;
        }
    }

    /// <typeparam name="T">Type of the first parameter.</typeparam>
    /// <typeparam name="U">Type of the second parameter.</typeparam>
    public class MyEvent<T, U> : AbstractEvent
    {
        private System.Action<T, U> onTrigger;

        public MyEvent()
        {
        }

        public void Dispatch(T value, U value2)
        {
            if (onTrigger != null)
            {
                onTrigger(value, value2);
            }
            OnDispatch();
        }

        public void AddListener(System.Action<T, U> handler)
        {
            onTrigger += handler;
        }

        public void RemoveListener(System.Action<T, U> handler)
        {
            onTrigger -= handler;
        }

        public override bool HasDuplicateParameterTypes()
        {
            return typeof(T) == typeof(U);
        }
    }

    /// <summary>
    /// Can be dispatched and listened to, with up to three parameters of given types.
    /// </summary>
    /// <typeparam name="T">Type of the first parameter.</typeparam>
    /// <typeparam name="U">Type of the second parameter.</typeparam>
    /// <typeparam name="I">Type of the third parameter.</typeparam>
    public class MyEvent<T, U, I> : AbstractEvent
    {
        private System.Action<T, U, I> onTrigger;

        public MyEvent()
        {
        }

        public void Dispatch(T value, U value2, I value3)
        {
            if (onTrigger != null)
            {
                onTrigger(value, value2, value3);
            }
            OnDispatch();
        }

        public void AddListener(System.Action<T, U, I> handler)
        {
            onTrigger += handler;
        }

        public void RemoveListener(System.Action<T, U, I> handler)
        {
            onTrigger -= handler;
        }

        public override bool HasDuplicateParameterTypes()
        {
            return typeof(T) == typeof(U) ||
                   typeof(T) == typeof(I) ||
                   typeof(U) == typeof(I);
        }
    }
    //public class MyEvent
    //{
    //    public delegate void eventAction(params object[] obj);
    //    public delegate void intAction(int value);
    //    public delegate void stringAction(string value);
    //    public delegate void voidAction();

    //    public event eventAction EventAction;
    //    public event intAction EventActionInt;
    //    public event stringAction EventActionString;
    //    public event voidAction EventActionVoid;

    //    public MyEvent()
    //    {
    //    }

    //    public void Dispatch(params object[] obj)
    //    {
    //        if (EventAction != null)
    //            EventAction(obj);
    //        else
    //            XDebug.Log("Event is Null\nPlease Initialize event first", XDebug.Mask.MyEvent, XDebug.Color.Red);
    //    }

    //    public void Dispatch(int value)
    //    {
    //        if (EventActionInt != null)
    //            EventActionInt(value);
    //        else
    //            XDebug.Log("Event is Null\nPlease Initialize event first", XDebug.Mask.MyEvent, XDebug.Color.Red);
    //    }

    //    public void Dispatch(string value)
    //    {
    //        if (EventActionString != null)
    //            EventActionString(value);
    //        else
    //            XDebug.Log("Event is Null\nPlease Initialize event first", XDebug.Mask.MyEvent, XDebug.Color.Red);
    //    }

    //    public void Dispatch()
    //    {
    //        if (EventActionVoid != null)
    //            EventActionVoid();
    //        else
    //            XDebug.Log("Event is Null\nPlease Initialize event first", XDebug.Mask.MyEvent, XDebug.Color.Red);
    //    }

    //}
}