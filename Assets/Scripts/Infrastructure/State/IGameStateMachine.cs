using Infrastructure.Service;
using Infrastructure.StaticData.Players;

namespace Infrastructure.State
{
    public interface IGameStateMachine : IService
    {
        public void Enter<TState>() where TState : class, IState;
        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
        public void Enter<TState, TPayload>(TPayload payload, HeroStaticData payload1) where TState : class, IPayloadedState1<TPayload, HeroStaticData>;
    }
}