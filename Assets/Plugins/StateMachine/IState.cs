using System;

namespace IngameStateMachine
{
    public interface IState : IDisposable
    {
        void SetStateMachine(StateMachine stateMachine);

        void OnEnter();
        void OnExit();
    }
}