using UnityEngine;

[CreateAssetMenu(fileName = "New Sound", menuName = "SO/Sound")]
public class SoundSO : ScriptableObject
{
    public string soundName;
    public AudioClip clip;
    public float volume = 1.0f;
    public float pitch = 1.0f;
}