using Infrastructure.AssetManagement;
using UnityEngine;

namespace Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    private GameBootstrapper _bootstrapperPrefab;

    private void Awake()
    {
      var bootstrapper = FindObjectOfType<GameBootstrapper>();
      
      if(bootstrapper != null) return;

      _bootstrapperPrefab = Resources.Load<GameBootstrapper>(AssetPath.GameBootstrapper);
      Instantiate(_bootstrapperPrefab);
      Destroy(this);
    }
  }
}