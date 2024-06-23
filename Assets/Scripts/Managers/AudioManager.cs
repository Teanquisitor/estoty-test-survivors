using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using DependencyInjection;

public class AudioManager : MonoBehaviour, IDependencyProvider
{
    [Provide] private AudioManager ProvideAudioManager() => this;
    [SerializeField] private List<SoundSO> sounds;
    private AudioSource sfxSource;
    private AudioSource bgmSource;

    private const string DEFAULT_BGM = "BGM";

    void Awake()
    {
        sfxSource = gameObject.AddComponent<AudioSource>();
        bgmSource = gameObject.AddComponent<AudioSource>();

        PlayBGM(DEFAULT_BGM);
    }

    public void Play(string name)
    {
        if (!TryGetSound(name, out SoundSO sound))
            return;

        PlaySound(sfxSource, sound);
    }

    public void PlayBGM(string name)
    {
        if (!TryGetSound(name, out SoundSO sound))
            return;

        bgmSource.loop = true;
        PlaySound(bgmSource, sound);
    }

    public void StopBGM() => bgmSource.Stop();

    private bool TryGetSound(string name, out SoundSO sound)
    {
        sound = sounds.FirstOrDefault(s => s.soundName == name);
        if (sound == null)
            return false;
        
        return true;
    }

    private void PlaySound(AudioSource source, SoundSO sound)
    {
        source.clip = sound.clip;
        source.volume = sound.volume;
        source.pitch = sound.pitch;
        source.Play();
    }

}