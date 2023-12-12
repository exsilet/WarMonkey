namespace Infrastructure.State
{
    public interface IState: IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload sceneName);
    }
    
    public interface IPayloadedState1<TPayload, TPayload1> : IExitableState
    {
        void EnterThreeParameters(TPayload payload, TPayload1 payload1);     
    }    
  
    public interface IExitableState
    {
        void Exit();
    }
}