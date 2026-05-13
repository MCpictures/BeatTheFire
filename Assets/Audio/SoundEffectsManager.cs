using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// the methods in this class should just be used by AudioPlayerBase, where
// then other audio children of AudioPlayerBase (e.g. MusicAudio or EnemyAudio) will
// use methods from AudioPlayerBase
public class SoundEffectsManager : MonoBehaviour
{
    [SerializeField] private AudioSource soundEffectsObject;
    [SerializeField] private AudioSource musicObject;
    [SerializeField] public MusicAudio musicAudio;

    public static SoundEffectsManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        musicAudio.PlayGameplayMusic();
    }



    public GameObject PlaySoundEffectsClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // spawning in sound gameObject
        AudioSource audioSource = Instantiate(
        soundEffectsObject,
        spawnTransform.position,
        Quaternion.identity,
        spawnTransform // <-- parent
    );

        // assign the audio clip
        audioSource.clip = audioClip;

        // assign volume
        audioSource.volume = volume;

        // play sound
        audioSource.Play();

        // get length of sound effect clip
        float clipLength = audioSource.clip.length;

        // destroy the clip after it is done playing
        Destroy(audioSource.gameObject, clipLength);

        return audioSource.gameObject; // return the spawned object
    }

    // method overload which includes pitch shifting
    public GameObject PlaySoundEffectsClip(AudioClip audioClip, Transform spawnTransform, float volume, float pitch)
    {
        // spawning in sound GameObject
        AudioSource audioSource = Instantiate(
            soundEffectsObject,
            spawnTransform.position,
            Quaternion.identity,
            spawnTransform // <-- parent
        );

        // assign settings
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.pitch = pitch;

        // play sound
        audioSource.Play();

        // destroy after it’s done
        Destroy(audioSource.gameObject, audioSource.clip.length);

        return audioSource.gameObject;
    }

    // plays a sound effect at a random pitch between a given pitch range (optional pitch parameters)
    public GameObject PlaySoundEffectsClipRandomPitch(AudioClip audioClip, Transform spawnTransform, float volume, float minPitch, float maxPitch)
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        return PlaySoundEffectsClip(audioClip, spawnTransform, volume, randomPitch);
    }

    public GameObject PlayRandomSoundEffectsClip(List<AudioClip> audioClips, Transform spawnTransform, float volume, float minPitch, float maxPitch)
    {
        AudioClip randomAudioClip = audioClips[Random.Range(0, audioClips.Count)];
        return PlaySoundEffectsClipRandomPitch(randomAudioClip, spawnTransform, volume, minPitch, maxPitch);
    }

    public GameObject PlayLoopingSoundClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // spawning in sound gameObject
        AudioSource audioSource = Instantiate(
            soundEffectsObject,
            spawnTransform.position,
            Quaternion.identity,
            spawnTransform // <-- parent
        );

        // assign the audio clip
        audioSource.clip = audioClip;

        // assign volume
        audioSource.volume = volume;

        audioSource.loop = true;

        // play sound
        audioSource.Play();

        return audioSource.gameObject; // return the spawned object
    }

    public GameObject PlayMusicClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        // spawning in sound gameObject
        AudioSource audioSource = Instantiate(
            musicObject,
            spawnTransform.position,
            Quaternion.identity,
            spawnTransform // <-- parent
        );

        // assign the audio clip
        audioSource.clip = audioClip;

        // assign volume
        audioSource.volume = volume;

        audioSource.loop = true;

        // play sound
        audioSource.Play();

        return audioSource.gameObject; // return the spawned object
    }
}



