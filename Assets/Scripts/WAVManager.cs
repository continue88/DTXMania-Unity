﻿using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WAVManager : MonoBehaviour
{
    public AudioSource AudioSource;

    Dictionary<int, WaveInfo> mWavInfoList = new Dictionary<int, WaveInfo>();
    Dictionary<string, AudioClip> mWaveCacheList = new Dictionary<string, AudioClip>();

    void Start()
    {
        if (!AudioSource) AudioSource = GetComponent<AudioSource>();
        if (!AudioSource) AudioSource = gameObject.AddComponent<AudioSource>();
    }

    public void Clear()
    {
        foreach (var waveInfo in mWavInfoList)
            Destroy(waveInfo.Value.GameObject);
        mWavInfoList.Clear();
    }

    public void PlaySound(string soundFilePath, bool loop = false)
    {
        Debug.Log("PlaySound:" + soundFilePath);

        AudioClip audioClip;
        if (mWaveCacheList.TryGetValue(soundFilePath, out audioClip))
        {
            if (audioClip)
            {
                if (AudioSource.isPlaying) AudioSource.Stop();
                AudioSource.loop = loop;
                AudioSource.PlayOneShot(audioClip);
            }
        }
        else
        {
            StartCoroutine(DelayLoadAudio(soundFilePath, loop));
        }
    }

    public void Stop()
    {
        AudioSource.Stop();
    }

    IEnumerator DelayLoadAudio(string filePath, bool loop)
    {
        // android file url.
        var loadUrl = filePath;
        if (loadUrl.StartsWith("/")) loadUrl = "file://";

        using (var clipWWW = new WWW(loadUrl))
        {
            yield return clipWWW;
            var audioClip = GetAudioClipFromWWW(clipWWW);
            mWaveCacheList[filePath] = audioClip; // if it is null, still set.
            if (audioClip) PlaySound(filePath, loop);
        }
    }

    AudioClip GetAudioClipFromWWW(WWW clipWWW)
    {
        if (!string.IsNullOrEmpty(clipWWW.error))
        {
            Debug.LogError("Fail to load wave: " + clipWWW.url + "\nError:" + clipWWW.error);
            return null;
        }

        var ext = Path.GetExtension(clipWWW.url).ToLower();

        // load xa audio file from data.
        if (ext == ".xa") return DecodeXaAudioData(
            Path.GetFileNameWithoutExtension(clipWWW.url), 
            clipWWW.bytes);

        var audioType = ext == ".wav" ? AudioType.WAV :
            ext == ".mp3" ? AudioType.MPEG :
            ext == ".ogg" ? AudioType.OGGVORBIS :
            AudioType.UNKNOWN;
        var audioClip = clipWWW.GetAudioClipCompressed(false, audioType);
        if (!audioClip)
        {
            Debug.LogWarning("The data is not a audio type: " + clipWWW.url);
            return null;
        }
        return audioClip;
    }

    AudioClip DecodeXaAudioData(string name, byte[] data)
    {
        var docoder = new bjxa.Decoder();
        var pcmData = docoder.Decode(data);
        var clip = AudioClip.Create(name,
            docoder.Samples,
            docoder.Channels,
            docoder.SampleRate,
            false);
        clip.SetData(pcmData, 0);
        return clip;
    }

    public void Sinup(int wavId, WWW clipWWW, bool loop)
    {
        var audioClip = GetAudioClipFromWWW(clipWWW);
        if (!audioClip)
            return;

        var clipName = Path.GetFileNameWithoutExtension(clipWWW.url);
        var go = new GameObject(clipName);
        var drumChild = go.transform;
        drumChild.parent = transform;

        var waveInfo = new WaveInfo(go, audioClip, 1);
        mWavInfoList[wavId] = waveInfo;
    }

    public void PlaySound(int wavId, ChipType chipType, bool muteOther, MuteGroupType muteGroup, float volume = 1.0f, float delayTime = 0.0f)
    {
        if (!mWavInfoList.ContainsKey(wavId))
        {
            // fallback to drum clip sound.
            var prop = UserManager.Instance.LoggedOnUser.DrumChipProperty[chipType];
            MainScript.Instance.DrumSound.PlaySound(
                chipType,
                prop.MuteBeforeUtter,
                prop.MuteGroupType,
                volume);
            return;
        }

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
                mAudioSources[i] = audioSource;
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
