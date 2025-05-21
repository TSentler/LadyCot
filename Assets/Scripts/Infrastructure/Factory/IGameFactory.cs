using Dialogs;
using Hero;
using Interaction;
using Services;

namespace Infrastructure.Factory
{
  public interface IGameFactory : IService
  {
    InteractionMouseInfoView InteractionMouseInfo { get; }
    InteractionPanelView InteractionPanelView { get; }
    ContextMenuBuilder ContextMenuBuilder { get; }
    HeroBehaviour HeroBehaviour { get; }
    DialogViewProvider DialogViewProvider { get; }

    void CreateMouse();
    void CreateUI();
    void CreateHero();
    void CreateContextMenuBuilder();
    void CreateDialog();
  }
}