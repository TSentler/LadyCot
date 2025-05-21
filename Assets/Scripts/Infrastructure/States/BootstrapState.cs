using System;
using Infrastructure.AssetManagement;
using Infrastructure.Factory;
using Services;
using Services.Input;
using Services.Interaction;
using Services.Randomizer;

namespace Infrastructure.States
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private const string DemoLevel = "Demo";
    
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly AllServices _services;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _services = services;

      RegisterServices();
    }

    public void Enter() =>
      _sceneLoader.Load(Initial, onLoaded: EnterLoadLevel);

    public void Exit()
    {
    }

    private void RegisterServices()
    {
      InputService inputService = new InputService();
      _services.RegisterSingle<IInputService>(inputService);
      _services.RegisterSingle<IRandomService>(new RandomService());
      _services.RegisterSingle<IAssetProvider>(new AssetProvider());
      _services.RegisterSingle<IInteractionStatus>(new InteractionStatus());
      _services.RegisterSingle<IInteractionChainService>(new InteractionChainService(
        _services.Single<IInteractionStatus>()));
      _services.RegisterSingle<IGameFactory>(new GameFactory(
        _services.Single<IAssetProvider>(), _services.Single<IInteractionChainService>()));

      _services.Single<IInteractionChainService>().Initialise(_services.Single<IGameFactory>());
    }

    private void EnterLoadLevel()
    {
      _stateMachine.Enter<LoadLevelState, string>(DemoLevel);
    }

  }
}