using UnityEngine;

public class MusicAudio : AudioPlayerBase
{
    [SerializeField] private AudioClip gameplayMusicClip;

    public GameObject PlayGameplayMusic() => PlayMusic(gameplayMusicClip, 0.5f);

}
