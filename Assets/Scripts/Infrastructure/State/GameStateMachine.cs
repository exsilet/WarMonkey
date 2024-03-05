using System;
using System.Collections.Generic;
using Infrastructure.Factory;
using Infrastructure.LevelLogic;
using Infrastructure.Service;
using Infrastructure.Service.PersistentProgress;
using Infrastructure.Service.SaveLoad;
using Infrastructure.Service.StaticData;
using Infrastructure.StaticData.Players;

namespace Infrastructure.State
{
    public class GameStateMachine : IGameStateMachine
    {
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadMenuState)] = new LoadMenuState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>()),
                
                [typeof(TransitionState)] = new TransitionState(this, sceneLoader, loadingCurtain),
                
                [typeof(TransitionReloadState)] = new TransitionReloadState(this, sceneLoader, loadingCurtain,
                    services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>()),

                [typeof(LoadProgressState)] = new LoadProgressState(this, 
                    services.Single<IPersistentProgressService>(),services.Single<ISaveLoadService>()),

                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain, services.Single<IGameFactory>(),
                    services.Single<IPersistentProgressService>(), services.Single<IStaticDataService>()),

                [typeof(GameLoopState)] = new GameLoopState(this),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }
        
        public void Enter<TState, TPayload>(TPayload payload, HeroStaticData payload1) where TState : class, IPayloadedState1<TPayload, HeroStaticData>
        {
            TState state = ChangeState<TState>();
            state.EnterThreeParameters(payload, payload1);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();

            TState state = GetState<TState>();
            _activeState = state;

            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState =>
            _states[typeof(TState)] as TState;
    }
}