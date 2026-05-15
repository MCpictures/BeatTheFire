using UnityEngine;

public class CharacterAudio : AudioPlayerBase
{
    [SerializeField] private AudioClip stepSound;

    public void PlayStepSound() => PlaySoundRandomPitch(stepSound);
}
