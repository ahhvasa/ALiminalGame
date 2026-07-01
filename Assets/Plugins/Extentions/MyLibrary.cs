using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MyLibrary
{
    namespace StateMachine
    {
        public class StateMachine<T> where T : IState
        {
            public StateMachine(params T[] allStates)
            {
                this.allStates = allStates.ToList();
                Start();
            }
            public void Start()
            {
                currentId = 0;
                if (allStates.Count == 0) { return; }
                allStates[currentId].OnEnter();
            }
            public List<T> allStates;
            public int currentId;
            public void EnterState(int id)
            {
                allStates[currentId].OnExit();
                currentId = id;
                allStates[currentId].OnEnter();
            }
            public void EnterState(T state)
            {
                if (allStates.Contains(state) == false)
                {
                    allStates.Add(state);
                }

                allStates[currentId].OnExit();
                currentId = allStates.IndexOf(state);
                allStates[currentId].OnEnter();
            }

            public T Current
            {
                get { return allStates[currentId]; }
            }
        }
        public interface IState
        {
            void OnEnter();
            void OnExit();
        }

        public interface IState_UseFrames : IState
        {
            void FixedUpdate();
            void Update();
        }

        namespace StateMachineWithParamenetrs
        {
            public class StateMachine_P<T> where T : IState_P
            {
                public StateMachine_P(T[] allStates1, object[] firstStateEnterParameters)
                {
                    allStates = allStates1;
                    currentId = 0;
                    allStates[currentId].OnEnter(firstStateEnterParameters);
                }
                public T[] allStates;
                public int currentId;
                public void EnterState(int id, object[] curentState_Exit_Parameters, object[] nextState_Enter_Parameters)
                {
                    allStates[currentId].OnExit(curentState_Exit_Parameters);
                    currentId = id;
                    allStates[currentId].OnEnter(nextState_Enter_Parameters);
                }
            }
            public interface IState_P
            {
                void OnEnter(object[] parameters);
                void OnExit(object[] parameters);
            }
            public interface IState_UseFrames_P : IState_P
            {
                void OnFixedFrame(object[] parameters);
                void OnFrame(object[] parameters);
            }
        }
    }
    public static class MyMath
    {
        public static bool Aproximatly(float a, float b, float range)
        {
            return Mathf.Abs(a - b) <= range;
        }
    }

}

public struct TransformInfo
{
    public TransformInfo(Vector3 position1, Quaternion rotation1)
    {
        position = position1;
        rotation = rotation1;
    }
    public Vector3 position;
    public Quaternion rotation;
}