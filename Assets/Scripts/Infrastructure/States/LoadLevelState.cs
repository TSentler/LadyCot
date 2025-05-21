using Infrastructure.Factory;
using Interaction;
using Logic;
using Services.Input;
using Services.Interaction;
using UnityEngine;

namespace Infrastructure.States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly IGameFactory _gameFactory;
    private readonly IInputService _inputService;
    private readonly IInteractionChainService _interactionChainService;

    public LoadLevelState(GameStateMachine gameStateMachine, 
      SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
      IGameFactory gameFactory, IInputService inputService, 
      IInteractionChainService interactionChainService)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingCurtain = loadingCurtain;
      _gameFactory = gameFactory;
      _inputService = inputService;
      _interactionChainService = interactionChainService;
    }

    public void Enter(string sceneName)
    {
      _loadingCurtain.Show();
      //_gameFactory.Cleanup();
      _sceneLoader.Load(sceneName, OnLoaded);
    }

    public void Exit() =>
      _loadingCurtain.Hide();

    private void OnLoaded()
    {
      InitGameWorld();
      
      _stateMachine.Enter<GameLoopState>();
    }

    private void InitGameWorld()
    {
      _gameFactory.CreateMouse();
      _gameFactory.CreateUI();
      _gameFactory.CreateHero();
      InitRoom();
      _gameFactory.CreateContextMenuBuilder();
      _gameFactory.CreateDialog();
      InitInteractionPoints();
    }

    private void InitRoom()
    {
      RoomBackground roomBackground = Object.FindObjectOfType<RoomBackground>();
      roomBackground.Construct(_inputService, _interactionChainService);
    }

    private void InitInteractionPoints()
    {
      _gameFactory.InteractionPanelView.Hide();
      Object.FindObjectOfType<InteractionPointsGroup>()
        .Construct(_inputService, 
          _gameFactory.InteractionMouseInfo, 
          _gameFactory.InteractionPanelView,
          _gameFactory.ContextMenuBuilder, _gameFactory.DialogViewProvider, _interactionChainService);
    }
  }
}