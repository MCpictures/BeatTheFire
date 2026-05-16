using UnityEngine;

public class MusicAudio : AudioPlayerBase
{
    [SerializeField] private AudioClip gameplayMusicClip;
    [SerializeField] private AudioClip mainMenuMusicClip;

    public GameObject PlayGameplayMusic() => PlayMusic(gameplayMusicClip, 0.1f);
    public GameObject PlayMainMenuMusic() => PlayMusic(mainMenuMusicClip, 0.2f);

}
