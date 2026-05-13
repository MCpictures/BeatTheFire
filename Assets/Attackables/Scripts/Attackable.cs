using System;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField] float damageOnDirectHit;
    [SerializeField] bool takesDamageOnTick;
    [SerializeField] float damagePerTick;
    [SerializeField] float timePerTick;
    [SerializeField] int numberOfTicksBeforeTicksStop;

    [Header("Score")]
    [SerializeField] bool doesScore;
    [SerializeField] int scoreAmount;

    [Header("Base classes")]
    [SerializeField] Rigidbody2D rigidBody;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer fireSpriteRenderer;
    [SerializeField] BoxCollider2D attackableCollider;
    [SerializeField] ScoreManager scoreManager;

    float timePassedSinceLastDamage;
    int numberOfDamageTicks;
    bool shouldTickForDamage;
    bool isKnockedBack;

    public static event Action<Attackable> OnAttackableAttacked; // so rooms know when an attackable is attacked by the player

    void Update()
    {
        CalculateHealthTick();
        CalculateKnockbackStop();
    }

    private void CalculateHealthTick()
    {
        if (takesDamageOnTick && shouldTickForDamage)
        {
            timePassedSinceLastDamage += Time.deltaTime;
            if (timePassedSinceLastDamage >= timePerTick)
            {
                numberOfDamageTicks++;
                timePassedSinceLastDamage = 0;
                health -= damagePerTick;
                if (health <= 0)
                {
                    HandleDestroy();
                }
                else if (numberOfTicksBeforeTicksStop > 0 && numberOfDamageTicks >= numberOfTicksBeforeTicksStop)
                {
                    StopTickDamage();
                }
            }
        }
    }

    private void CalculateKnockbackStop()
    {
        if (Mathf.Abs(rigidBody.linearVelocity.x) == 0f) isKnockedBack = false;
    }

    public void Attacked(Transform attackerTransform)
    {
        OnAttackableAttacked?.Invoke(this);
        if (hasKnockback) ApplyKnockback(attackerTransform);
        if (takesDamage)
        {
            ApplyDamage();
        }
        else
        {
            HandleDestroy();
        }
    }

    void ApplyKnockback(Transform attackerTransform)
    {
        isKnockedBack = true;
        rigidBody.linearVelocity = Vector2.zero; // Reset velocity so only kockback is applied

        // Calculate direction
        Vector2 direction = transform.position - attackerTransform.position;
        direction.y = 0f;
        direction.Normalize();

        // Apply rotation to direction
        float angle = direction.x > 0 ? knockbackAngleInDeg : -knockbackAngleInDeg;
        direction = Quaternion.Euler(0, 0, angle) * direction;

        rigidBody.AddForce(direction.normalized * knockbackStrength, ForceMode2D.Impulse);
    }

    void ApplyDamage()
    {
        // Apply direct hit and check if object has health left
        health -= damageOnDirectHit;
        if (health <= 0)
        {
            HandleDestroy();
        }
        else if (takesDamageOnTick) // Activate damage tick
        {
            shouldTickForDamage = true;
            fireSpriteRenderer.enabled = true;
        }
    }

    void StopTickDamage()
    {
        timePassedSinceLastDamage = 0;
        numberOfDamageTicks = 0;
        shouldTickForDamage = false;
    }

    void HandleDestroy()
    {
        if (shattersOnDestroyed) Break();
        if (doesScore) HandleScore();
        Destroy(gameObject);
    }

    void HandleScore()
    {
        scoreManager.Scored(scoreAmount);
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
                UnityEngine.Random.Range(-1f, 1f),
                UnityEngine.Random.Range(0.5f, 1.5f)
            ).normalized;

            rb.AddForce(forceDirection * explosionForce, ForceMode2D.Impulse);
            rb.AddTorque(UnityEngine.Random.Range(-200f, 200f));

            Destroy(piece, timeForDespawn);
        }
    }

    public bool IsKnockedBack { get { return isKnockedBack; } }
}
