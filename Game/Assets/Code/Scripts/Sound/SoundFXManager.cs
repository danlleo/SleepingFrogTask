using Misc;
using UnityEngine;
using Utilities;

namespace Sound
{
    /// <summary>
    /// I know that a lot of people don't prefer using singletons, and I can agree with them.
    /// If project is getting bigger, it's gonna get harder to maintain well organized code,
    /// and personally I think it will ruin maintenance.
    /// Personally I would do most of the stuff event-driven. Hovewer in this case I decided to choose singleton,
    /// simply because of it's simplicity. 
    /// </summary>
    public sealed class SoundFXManager : Singleton<SoundFXManager>
    {
        [Header("External references")]
        [SerializeField] private AudioSource _soundFX2DPrefab;

        public void PlaySoundFX2DClip(AudioClip audioClip, float volume = 1f)
        {
            AudioSource audioSource = Instantiate(_soundFX2DPrefab, Vector3.zero, Quaternion.identity);
            audioSource.clip = audioClip;
            audioSource.volume = volume;
            audioSource.Play();

            float clipLength = audioSource.clip.length;

            Destroy(audioSource.gameObject, clipLength);
        }

        public void PlayRandomSoundFX2DClip(AudioClip[] audioClips, float volume = 1f)
        {
            AudioSource audioSource = Instantiate(_soundFX2DPrefab, Vector3.zero, Quaternion.identity);
            audioSource.clip = audioClips.GetRandomItem();
            audioSource.volume = volume;
            audioSource.Play();

            float clipLength = audioSource.clip.length;

            Destroy(audioSource.gameObject, clipLength);
        }
    }
}