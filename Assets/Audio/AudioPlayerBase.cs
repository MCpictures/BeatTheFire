using System.Collections.Generic;
using UnityEngine;


// abstract class for playing sound effects
// This is the class that utilizes the methods from SoundEffectsManager
public abstract class AudioPlayerBase : MonoBehaviour
{
    protected GameObject PlaySound(AudioClip clip, float volume = 1f) // optional volume parameter, defaults to full volume
    {
        if (clip == null)
        {
            Debug.LogWarning($"No audio clip assigned in {name}");
            return null;
        }
        return SoundEffectsManager.Instance.PlaySoundEffectsClip(clip, transform, volume);
    }

    // Random pitch version
    protected GameObject PlaySoundRandomPitch(AudioClip clip, float volume = 1f, float minPitch = 0.95f, float maxPitch = 1.2f)
    {
        if (clip == null)
        {
            Debug.LogWarning($"No audio clip assigned in {name}");
            return null;
        }
        return SoundEffectsManager.Instance.PlaySoundEffectsClipRandomPitch(clip, transform, volume, minPitch, maxPitch);
    }

    protected GameObject PlayRandomSound(List<AudioClip> clips, float volume = 1f, float minPitch = 0.95f, float maxPitch = 1.2f)
    {
        if (clips == null)
        {
            Debug.LogWarning($"No audio clips assigned in {name}");
            return null;
        }
        return SoundEffectsManager.Instance.PlayRandomSoundEffectsClip(clips, transform, volume, minPitch, maxPitch);
    }

    protected GameObject PlayLoopingSound(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning($"No audio clip assigned in {name}");
            return null;
        }
        return SoundEffectsManager.Instance.PlayLoopingSoundClip(clip, transform, volume);
    }

    protected GameObject PlayMusic(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning($"No audio clip assigned in {name}");
            return null;
        }
        return SoundEffectsManager.Instance.PlayMusicClip(clip, transform, volume);
    }

}