using UnityEngine;

namespace Services.Input
{
  public class InputService : IInputService
  {
    public Vector2 MousePosition => GetWorldPoint();

    private Vector2 GetWorldPoint() => 
      Camera.main.ScreenToWorldPoint(UnityEngine.Input.mousePosition);
  }
}