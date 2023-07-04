using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource music;

    public void MusicEnabled(bool value)
    {
        music.enabled = value;
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
    }
}
