using UnityEngine;

public class EnemyAudio : AudioPlayerBase
{
   [SerializeField] private AudioClip hurtSound;

    void OnEnable()
    {
        Attackable.OnAttackableAttacked += HandleAttackableAttacked;
    }
    void OnDisable()
    {
        Attackable.OnAttackableAttacked -= HandleAttackableAttacked;
    }

    void HandleAttackableAttacked(Attackable attackable)
    {
        PlayHurtSound();
    }
    public void PlayHurtSound() => PlaySoundRandomPitch(hurtSound);

}
