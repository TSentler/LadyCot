using SkiaSharp.Unity;
using UnityEngine;

public class LottieTest : MonoBehaviour
{
    [SerializeField] private string path = "json/";
    [SerializeField] private SkottiePlayerV2 skottiePlayer;
    [SerializeField] private string state;
    
    void Start()
    {
        TextAsset json = Resources.Load<TextAsset>(path);
        skottiePlayer.LoadAnimation(json.text);
        if (string.IsNullOrEmpty(state) == false)
            skottiePlayer.SetState(state);
        skottiePlayer.PlayAnimation();
    }
}
