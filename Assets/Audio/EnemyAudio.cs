using UnityEngine;

public class EnemyAudio : AudioPlayerBase
{
    [SerializeField] private AudioClip hurtSound;
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
    public void PlayHurtSound() => PlaySoundRandomPitch(hurtSound);

}
