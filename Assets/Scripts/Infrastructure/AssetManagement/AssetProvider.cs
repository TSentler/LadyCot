using UnityEngine;

namespace Infrastructure.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject Instantiate(string path, Vector3 at)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, parent);
    }
  }
}