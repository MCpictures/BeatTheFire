using UnityEngine;

public class EnemyAudio : AudioPlayerBase
{
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private float minHurtSoundPitch = 0.95f;
    [SerializeField] private float maxHurtSoundPitch = 1.2f;
    private Attackable _attackable;

    void Awake()
    {
        _attackable = GetComponent<Attackable>();
    }

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
        if (attackable == _attackable)
        {
            PlayHurtSound();
        }
    }
    public void PlayHurtSound() => PlaySoundRandomPitch(hurtSound, 1f, minHurtSoundPitch, maxHurtSoundPitch);

}
