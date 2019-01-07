using SSTFormat.v4;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumSound : MonoBehaviour
{
    public AudioClip[] DrumClips;

    private Dictionary<ChipType, DrumSoundInfo> mChipTypeToDrumSound = null;

    private void Start()
    {
        mChipTypeToDrumSound = new Dictionary<ChipType, DrumSoundInfo>();

        var soundRoot = transform;
        foreach (var clip in DrumClips)
        {
            var clipName = clip.name;
            var chipType = ChipType.Unknown;
            if (!System.Enum.TryParse(clipName, out chipType))
            {
                Debug.LogError("The chip sound not matching chip type: " + clipName);
                continue;
            }
            var go = new GameObject(clipName);
            var drumChild = go.transform;
            drumChild.parent = soundRoot;

            var drumSoundInfo = new DrumSoundInfo(go, clip);
            mChipTypeToDrumSound[chipType] = drumSoundInfo;
        }
    }

    public void PlaySound(ChipType chipType, bool muteOther = false, MuteGroupType muteGroup = MuteGroupType.Unknown, float volume = 1f)
    {
        if (!mChipTypeToDrumSound.ContainsKey(chipType))
            return;

        if (muteOther && muteGroup != MuteGroupType.Unknown)
        {
            foreach (var soundInfo in mChipTypeToDrumSound.Values)
            {
                if (soundInfo.MuteGroup == muteGroup)
                    soundInfo.Stop();
            }
        }

        var drumSoundInfo = mChipTypeToDrumSound[chipType];
        drumSoundInfo.PlaySound(muteGroup, volume);
    }

    private class DrumSoundInfo
    {
        private int mNextSoundId = 0;
        private AudioClip mAudioClip;
        private AudioSource[] mAudioSources;

        public int MultiSound { get; private set; }
        public MuteGroupType MuteGroup { get; private set; }

        public DrumSoundInfo(GameObject go, AudioClip audioClip, int num = 4)
        {
            mAudioClip = audioClip;
            MultiSound = num;
            mAudioSources = new AudioSource[MultiSound];
            for (var i = 0; i < MultiSound; i++)
                mAudioSources[i] = go.AddComponent<AudioSource>();
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
