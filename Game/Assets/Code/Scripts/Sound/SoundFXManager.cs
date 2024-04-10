﻿using Misc;
using UnityEngine;
using Utilities;

namespace Sound
{
    public class SoundFXManager : Singleton<SoundFXManager>
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