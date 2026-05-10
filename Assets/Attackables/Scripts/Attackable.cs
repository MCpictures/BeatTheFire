using UnityEngine;

public class Attackable : MonoBehaviour
{
    [Header("Knockback")]
    [SerializeField] bool hasKnockback;
    [SerializeField] float knockbackStrength;
    [SerializeField] float knockbackAngleInDeg;

    [Header("Destruction")]
    [SerializeField] bool shattersOnDestroyed;
    [SerializeField] int pieces = 4;
    [SerializeField] float explosionForce = 4f;
    [SerializeField] float timeForDespawn = 4f;
    [SerializeField] string brokenPieceLayerName = "BrokenPiece";

    [Header("Health")]
    [SerializeField] bool takesDamage;
    [SerializeField] float health;
    [SerializeField] float damageOnHit;
    [SerializeField] bool takesDamageOnTick;
    [SerializeField] float damagePerTick;
    [SerializeField] float timePerTick;
    [SerializeField] int numberOfTicksBeforeTicksStop;

    [Header("Base classes")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer fireSpriteRenderer;
    [SerializeField] BoxCollider2D attackableCollider;

    float timePassedSinceLastDamage;
    int numberOfTicks;
    bool shouldTickForDamage;

    void Update()
    {
        if (takesDamageOnTick && shouldTickForDamage)
        {
            timePassedSinceLastDamage += Time.deltaTime;
            if (timePassedSinceLastDamage >= timePerTick)
            {
                numberOfTicks++;
                timePassedSinceLastDamage = 0;
                health -= damagePerTick;
                if (health <= 0)
                {
                    Break();
                }
                else if(numberOfTicksBeforeTicksStop > 0 && numberOfTicks >= numberOfTicksBeforeTicksStop)
                {
                    StopTickDamage();
                }
            }
        }
    }

    public void Attacked(bool isCharacterLookingRight)
    {
        if(hasKnockback) ApplyKnockback(isCharacterLookingRight);
        if (takesDamage) ApplyDamage();
        if (shattersOnDestroyed && !takesDamage) Break();
    }

    void ApplyKnockback(bool isCharacterLookingRight)
    {
        rigidBody.linearVelocity = Vector2.zero;

        Vector2 direction = isCharacterLookingRight ? Vector2.right : Vector2.left;
        direction = Quaternion.Euler(0, 0, isCharacterLookingRight ? knockbackAngleInDeg : -knockbackAngleInDeg) * direction;

        rigidBody.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);
    }

    void ApplyDamage()
    {
        health -= damageOnHit;
        if(health <= 0)
        {
            Break();
        }
        else if(takesDamageOnTick)
        {
            shouldTickForDamage = true;
            fireSpriteRenderer.enabled = true;
        }
    }

    void StopTickDamage()
    {
        timePassedSinceLastDamage = 0;
        numberOfTicks = 0;
        shouldTickForDamage = false;
    }

    void Break()
    {
        Sprite sprite = spriteRenderer.sprite;
        Texture2D sourceTexture = sprite.texture;

        Rect spriteRect = sprite.textureRect;

        int pieceWidth = Mathf.FloorToInt(spriteRect.width / pieces);
        int pieceHeight = Mathf.FloorToInt(spriteRect.height);

        for (int i = 0; i < pieces; i++)
        {
            int x = Mathf.FloorToInt(spriteRect.x) + i * pieceWidth;
            int y = Mathf.FloorToInt(spriteRect.y);

            int width = i == pieces - 1
                ? Mathf.FloorToInt(spriteRect.width) - pieceWidth * i
                : pieceWidth;

            Texture2D pieceTexture = new Texture2D(width, pieceHeight);
            Color[] pixels = sourceTexture.GetPixels(x, y, width, pieceHeight);

            pieceTexture.SetPixels(pixels);
            pieceTexture.Apply();

            Sprite pieceSprite = Sprite.Create(
                pieceTexture,
                new Rect(0, 0, width, pieceHeight),
                new Vector2(0.5f, 0.5f),
                sprite.pixelsPerUnit
            );

            GameObject piece = new GameObject("Sprite Piece");
            piece.transform.position = transform.position + new Vector3(
                (i - pieces / 2f) * (width / sprite.pixelsPerUnit) * spriteRenderer.transform.localScale.x,
                0f,
                0f
            );
            piece.transform.localScale = spriteRenderer.transform.localScale;
            piece.layer = LayerMask.NameToLayer(brokenPieceLayerName);

            SpriteRenderer pieceRenderer = piece.AddComponent<SpriteRenderer>();
            pieceRenderer.sprite = pieceSprite;
            pieceRenderer.sortingLayerID = spriteRenderer.sortingLayerID;
            pieceRenderer.sortingOrder = spriteRenderer.sortingOrder;

            Rigidbody2D rb = piece.AddComponent<Rigidbody2D>();
            BoxCollider2D col = piece.AddComponent<BoxCollider2D>();

            Vector2 forceDirection = new Vector2(
                Random.Range(-1f, 1f),
                Random.Range(0.5f, 1.5f)
            ).normalized;

            rb.AddForce(forceDirection * explosionForce, ForceMode2D.Impulse);
            rb.AddTorque(Random.Range(-200f, 200f));

            Destroy(piece, timeForDespawn);
        }
        Destroy(gameObject);
    }
}
