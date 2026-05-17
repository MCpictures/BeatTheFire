using UnityEngine;

public class MusicAudio : AudioPlayerBase
{
    [SerializeField] private AudioClip gameplayMusicClip;
    [SerializeField] private AudioClip mainMenuMusicClip;

    // Audio For starting Scene
    [SerializeField] private AudioClip startingMusicClip;

    public GameObject PlayGameplayMusic() => PlayMusic(gameplayMusicClip, 0.1f);
    public GameObject PlayMainMenuMusic() => PlayMusic(mainMenuMusicClip, 0.2f);

    public GameObject PlayStartingMusic() => PlayMusic(mainMenuMusicClip, 0.2f);

}
