using Infrastructure.AssetManagement;
using Infrastructure.States;
using Logic;
using UnityEngine;

namespace Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    private LoadingCurtain _curtainPrefab;
    private Game _game;

    private void Awake()
    {
      _curtainPrefab = Resources.Load<LoadingCurtain>(AssetPath.LoadingCurtain);
      _game = new Game(this, Instantiate(_curtainPrefab));
      _game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}