using System.Collections;
using UnityEngine;

public class KnockbackOnCharacter : MonoBehaviour
{

    [SerializeField] private float knockbackLength = 0.5f;

    CharacterMovement characterMovement;
    Rigidbody2D rb;

   
    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void DoKnockback(float knockbackStrength)
    {
        StartCoroutine(DisablePlayerMovement(knockbackLength));
        rb.linearVelocity = new Vector2(-characterMovement.FacingDirection * knockbackStrength, rb.linearVelocity.y);//-characterMovement.IsLookingRight *
    }

    IEnumerator DisablePlayerMovement(float time)
    {
        characterMovement.canMove = false;
        yield return new WaitForSeconds(time);
        characterMovement.canMove = true;
    }

}
