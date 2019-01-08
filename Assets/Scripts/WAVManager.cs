﻿using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WAVManager : MonoBehaviour
{
    Dictionary<int, WaveInfo> mWavInfoList = new Dictionary<int, WaveInfo>();

    public void Clear()
    {
        foreach (var waveInfo in mWavInfoList)
            Destroy(waveInfo.Value.GameObject);
        mWavInfoList.Clear();
    }

    public void Sinup(int wavId, WWW clipWWW, bool loop)
    {
        if (!string.IsNullOrEmpty(clipWWW.error))
        {
            Debug.LogError("Fail to load wave: " + clipWWW.url);
            return;
        }

        var audioClip = clipWWW.GetAudioClipCompressed(false, AudioType.MPEG);
        if (!audioClip)
        {
            Debug.LogWarning("The data is not a audio type: " + clipWWW.url);
            return;
        }

        var clipName = Path.GetFileNameWithoutExtension(clipWWW.url);// audioClip.name;
        var go = new GameObject(clipName);
        var drumChild = go.transform;
        drumChild.parent = transform;

        var waveInfo = new WaveInfo(go, audioClip, 1);
        mWavInfoList[wavId] = waveInfo;
    }

    public void PlaySound(int wavId, ChipType chipType, bool muteOther, MuteGroupType muteGroup, float volume = 1.0f, float delayTime = 0.0f)
    {
        if (!mWavInfoList.ContainsKey(wavId))
            return;

        if (muteOther && muteGroup != MuteGroupType.Unknown)
        {
            foreach (var soundInfo in mWavInfoList.Values)
            {
                if (soundInfo.MuteGroup == muteGroup)
                    soundInfo.Stop();
            }
        }

        var waveInfo = mWavInfoList[wavId];
        waveInfo.PlaySound(muteGroup, volume);
    }


    private class WaveInfo
    {
        private int mNextSoundId = 0;
        private AudioClip mAudioClip;
        private AudioSource[] mAudioSources;

        public GameObject GameObject { get; private set; }
        public int MultiSound { get; private set; }
        public MuteGroupType MuteGroup { get; private set; }

        public WaveInfo(GameObject go, AudioClip audioClip, int num = 4)
        {
            GameObject = go;
            mAudioClip = audioClip;
            MultiSound = num;
            mAudioSources = new AudioSource[MultiSound];
            for (var i = 0; i < MultiSound; i++)
            {
                var audioSource = go.AddComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.clip = audioClip;
            }   
        }

        public void PlaySound(MuteGroupType muteGroup, float volume)
        {
            MuteGroup = muteGroup;

            var audioSource = mAudioSources[mNextSoundId];
            audioSource.PlayOneShot(mAudioClip, volume);

            mNextSoundId = (mNextSoundId + 1) % MultiSound;
        }

        public void Stop()
        {
            for (var i = 0; i < MultiSound; i++)
            {
                var audioSource = mAudioSources[i];
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }
        }
    }
}
