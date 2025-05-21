using Dialogs;
using Hero;
using Infrastructure.AssetManagement;
using Interaction;
using Services.Interaction;
using UnityEngine;
using UnityEngine.AI;

namespace Infrastructure.Factory
{
  public class GameFactory : IGameFactory
  {
    private readonly IAssetProvider _assets;

    private InteractionMouseInfoView _mouseInfo;
    private InteractionPanelView _interactionPanelView;
    private ContextMenuBuilder _contextMenuBuilder;
    private HeroBehaviour _heroBehaviour;
    private IInteractionChainService _interactionChainService;
    private DialogViewProvider _dialogViewProvider;

    public GameFactory(IAssetProvider assets, IInteractionChainService interactionChainService)
    {
      _interactionChainService = interactionChainService;
      _assets = assets;
    }

    public InteractionMouseInfoView InteractionMouseInfo => _mouseInfo;
    public InteractionPanelView InteractionPanelView => _interactionPanelView;
    public ContextMenuBuilder ContextMenuBuilder => _contextMenuBuilder;
    public HeroBehaviour HeroBehaviour => _heroBehaviour;
    public DialogViewProvider DialogViewProvider => _dialogViewProvider;

    public void CreateMouse()
    {
      GameObject canvasMouseIcon = _assets.Instantiate(AssetPath.CanvasMouseIcon);
      //Canvas canvas = canvasMouseIcon.GetComponent<Canvas>();
      //canvas.worldCamera = Camera.main;
      _mouseInfo = canvasMouseIcon.GetComponentInChildren<InteractionMouseInfoView>();
      _mouseInfo.ResetText();
    }

    public void CreateUI()
    {
      CreateCanvasInteraction();
    }

    public void CreateHero()
    {
      _heroBehaviour = Object.FindObjectOfType<HeroBehaviour>();
      NavMeshAgent agent = _heroBehaviour.GetComponent<NavMeshAgent>();
      _heroBehaviour.Construct(agent, _interactionChainService);
    }

    public void CreateContextMenuBuilder()
    {
      _contextMenuBuilder = new ContextMenuBuilder(_assets, _interactionChainService, _interactionPanelView);
    }

    public void CreateDialog()
    {
      GameObject dialog = _assets.Instantiate(AssetPath.CanvasDialog);
      _dialogViewProvider = dialog.GetComponent<DialogViewProvider>();
      _dialogViewProvider.Initialize();
    }

    private void CreateCanvasInteraction()
    {
      GameObject canvasInteraction = _assets.Instantiate(AssetPath.CanvasInteraction);
      Canvas canvas = canvasInteraction.GetComponent<Canvas>();
      canvas.worldCamera = Camera.main;
      _interactionPanelView = canvasInteraction.GetComponentInChildren<InteractionPanelView>();
    }

    private GameObject InstantiateRegistered(string prefabPath, Vector3 at)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath, at: at);
      return gameObject;
    }

    private GameObject InstantiateRegistered(string prefabPath)
    {
      GameObject gameObject = _assets.Instantiate(path: prefabPath);
      return gameObject;
    }
  }
}