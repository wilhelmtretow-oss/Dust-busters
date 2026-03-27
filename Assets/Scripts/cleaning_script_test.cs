using UnityEngine;

public class DirtCleanable : MonoBehaviour
{
    public string playerTag = "Player";
    public SpriteRenderer spriteRenderer;

    [Header("Cleaning Settings")]
    public float cleaningWidth = 1.0f;
    public int samples = 7;
    public int radius = 4;
    public float cleanThreshold = 0.1f;
    public float backOffset = 0.05f;

    private Texture2D dirtTexture;
    private bool isDestroyed = false;
    private int width, height;

    void Start()
    {
        Texture2D source = spriteRenderer.sprite.texture;
        dirtTexture = new Texture2D(
            source.width,
            source.height,
            TextureFormat.RGBA32,
            false
        );
        dirtTexture.SetPixels(source.GetPixels());
        dirtTexture.Apply();
        width = dirtTexture.width;
        height = dirtTexture.height;
        spriteRenderer.sprite = Sprite.Create(
            dirtTexture,
            new Rect(0, 0, width, height),
            new Vector2(0.5f, 0.5f),
            spriteRenderer.sprite.pixelsPerUnit
        );
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && !isDestroyed)
        {
            BoxCollider2D box = collision.GetComponent<BoxCollider2D>();
            if (box != null)
            {
                Vector3 bottomLeft = box.bounds.min;
                Vector3 bottomRight = new Vector3(box.bounds.max.x, box.bounds.min.y, box.bounds.min.z);
                bool cleanedThisFrame = false;

                for (int i = 0; i < samples; i++)
                {
                    float t = (float)i / (samples - 1);
                    Vector3 worldPoint = Vector3.Lerp(bottomLeft, bottomRight, t);
                    worldPoint -= collision.transform.up * backOffset;
                    if (Clean(worldPoint))
                        cleanedThisFrame = true;
                }

                if (cleanedThisFrame)
                {
                    dirtTexture.Apply();
                    CheckIfCleaned();
                }
            }
        }
    }

    //this is where the cleaning happens,
    //it checks a circular area around the given world position and makes pixels transparent if they are within the radius and not already clear.
    //It returns true if any pixels were changed, which signals that we need to apply the texture changes and check if the object is fully cleaned.
    bool Clean(Vector2 worldPos)
    {
        bool changed = false;
        Vector2 localPos = transform.InverseTransformPoint(worldPos);
        int centerX = Mathf.RoundToInt((localPos.x + 0.5f) * width);
        int centerY = Mathf.RoundToInt((localPos.y + 0.5f) * height);
        int rSquared = radius * radius;
        int xStart = Mathf.Max(centerX - radius, 0);
        int xEnd = Mathf.Min(centerX + radius, width - 1);
        int yStart = Mathf.Max(centerY - radius, 0);
        int yEnd = Mathf.Min(centerY + radius, height - 1);

        for (int x = xStart; x <= xEnd; x++)
        {
            for (int y = yStart; y <= yEnd; y++)
            {
                int dx = x - centerX;
                int dy = y - centerY;
                if (dx * dx + dy * dy <= rSquared)
                {
                    Color c = dirtTexture.GetPixel(x, y);
                    if (c.a > 0)
                    {
                        dirtTexture.SetPixel(x, y, Color.clear);
                        changed = true;
                    }
                }
            }
        }
        return changed;
    }

    void CheckIfCleaned()
    {
        if (isDestroyed) return;
        Color[] pixels = dirtTexture.GetPixels();
        int dirtyPixels = 0;

        foreach (Color c in pixels)
        {
            if (c.a > 0.1f)
                dirtyPixels++;
        }

        float dirtyPercent = (float)dirtyPixels / pixels.Length;
        if (dirtyPercent < cleanThreshold)
        {
            isDestroyed = true;
            if (CleaningManager.Instance != null)
                CleaningManager.Instance.AddCleanedObject();
            Destroy(gameObject);
        }
    }
}