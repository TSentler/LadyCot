using System;
using System.IO;
using u.movin;
using UnityEngine;

public class MovinAsset
{
    private readonly Movin movin;
    private readonly MovinLayer movinLayer;
    private readonly BodymovinAssets assetData;
    private readonly string directory;

    private GameObject gameObject;
    
    public MovinAsset(MovinLayer movinLayer, BodymovinAssets assetData)
    {
        movin = movinLayer.movin;
        this.movinLayer = movinLayer;
        this.assetData = assetData;
        string assetName = Path.GetFileNameWithoutExtension(assetData.p);
        directory = Path.Combine(movin.directory, assetData.u + assetName).Replace('\\','/');

    }
    
    public Transform transform => 
        gameObject.transform;

    public void Initialize()
    {
        gameObject = new GameObject(assetData.id);
        transform.SetParent(movinLayer.transform, false);
        transform.localScale *= movin.imgScale;
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        Sprite sprite = null;
        if (assetData.e == 0)
        {
            sprite = Resources.Load<Sprite>(directory);
        }

        if (assetData.e == 1)
        {
            string pureBase64 = GetPureBase64(assetData.p);
            sprite = ConvertBase64ToSprite(pureBase64, assetData.w, assetData.h);
        }
        spriteRenderer.sprite = sprite;
    }
    
    public Sprite ConvertBase64ToSprite(string base64String, int width, int height)
    {
        Sprite sprite = null;
        try
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(imageBytes); // Автоматически определяет размер
            
            sprite = Sprite.Create(
                texture, 
                new Rect(0, 0, texture.width, texture.height), 
                new Vector2(0.5f, 0.5f)
            );
            
        }
        catch (Exception e)
        {
            Debug.LogError("Ошибка конвертации Base64 в Sprite: " + e.Message);
        }

        return sprite;
    }
    
    string GetPureBase64(string base64WithHeader)
    {
        const string marker = "base64,";
        int markerIndex = base64WithHeader.IndexOf(marker);
    
        if (markerIndex >= 0)
        {
            return base64WithHeader.Substring(markerIndex + marker.Length);
        }
    
        // Если заголовка нет, возвращаем как есть (возможно, это уже чистая base64)
        return base64WithHeader;
    }
}