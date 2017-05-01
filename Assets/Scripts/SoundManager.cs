using UnityEngine;
using UnityEngine.Audio;

using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    AudioSource mCurrentMusicSource = null;
    List<AudioSource> mSounds = new List<AudioSource>();
    public bool MuteSound = false;

    public AudioMixerGroup soundAudioMixerGroup;

    const float ABSOLUMTE_MUSIC_VOLUME = 1.0f;
    const float ABSOLUMTE_SOUND_VOLUME = 0.8f;

    public float MusicVolume = 1.0f;
    public float SoundVolume = 1.0f;
    public int MAX_SAME_SOUND = 5;

    string mCurrentMusicName;

    private static SoundManager mInstance;
    public static SoundManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                if(GameObject.Find("SoundManager") == null)
                {
                    GameObject go = new GameObject("SoundManager");
                    mInstance = go.AddComponent<SoundManager>();
                }

                mInstance = GameObject.Find("SoundManager").GetComponent<SoundManager>();
            }

            return mInstance;
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    #region public
    public void PlaySound(string soundName, bool bPuasable)
    {
        if (string.IsNullOrEmpty(soundName))
        {
            Debug.Log("SoundName is null or empty.");
            return;
        }

        int sameSoundCount = 0;
        for (int index = 0; index < mSounds.Count; index++)
        {
            if (mSounds[index].name == soundName)
            {
                sameSoundCount++;
            }
        }

        if (sameSoundCount > MAX_SAME_SOUND)
        {
            return;
        }

        StartCoroutine(PlaySoundAfterLoading(soundName, bPuasable));
    }

    public void PlayMusic(string musicName, bool bPusable)
    {
        if (string.IsNullOrEmpty(musicName))
        {
            Debug.Log("MusicName is null or empty.");
            return;
        }


        mCurrentMusicName = musicName;

        StartCoroutine(PlayMusicAfterLoading(musicName, bPusable));
    }
    public void StopMusic()
    {
        mCurrentMusicName = "";
        if (mCurrentMusicSource == null)
        {
            return;
        }

        mCurrentMusicSource.Stop();
        Destroy(mCurrentMusicSource.gameObject);
        mCurrentMusicSource = null;
    }
    #endregion

    #region private
    IEnumerator PlaySoundAfterLoading(string soundName, bool bPuasable)
    {
        ResourceRequest request = LoadClipAsync(soundName);
        while (!request.isDone)
        {
            yield return null;
        }

        AudioClip soundClip = (AudioClip)request.asset;
        if (!soundClip)
        {
            Debug.Log("sound not loaded:" + soundName);
        }

        GameObject sound = new GameObject("sound:" + soundName);
        AudioSource source = sound.AddComponent<AudioSource>();

        sound.transform.parent = transform;

        source.mute = MuteSound;
        source.playOnAwake = true;
        source.volume = SoundVolume * ABSOLUMTE_SOUND_VOLUME;
        source.priority = 128;
        source.ignoreListenerPause = bPuasable;
        source.clip = soundClip;
        source.outputAudioMixerGroup = soundAudioMixerGroup;
        source.Play();

        mSounds.Add(source);
    }

    IEnumerator PlayMusicAfterLoading(string musicName, bool bPuasable)
    {
        ResourceRequest request = LoadClipAsync(musicName);
        while (!request.isDone)
        {
            yield return null;
        }

        AudioClip musicClip = (AudioClip)request.asset;
        if (!musicClip)
        {
            Debug.Log("sound not loaded:" + musicName);
        }

        StopMusic();

        mCurrentMusicName = musicName;

        GameObject music = new GameObject("sound:" + musicName);
        AudioSource musicSource = music.AddComponent<AudioSource>();

        music.transform.parent = transform;

        musicSource.loop = true;
        musicSource.priority = 0;
        musicSource.playOnAwake = false;
        musicSource.ignoreListenerPause = true;
        musicSource.clip = musicClip;
        musicSource.Play();

        mCurrentMusicSource = musicSource;
    }

    ResourceRequest LoadClipAsync(string name)
    {
        string path = "Sound/" + name;
        return Resources.LoadAsync<AudioClip>(path);
    }

    void Update()
    {
        var soundsToDelete = mSounds.FindAll(sound => !sound.isPlaying);

        foreach (AudioSource sound in soundsToDelete)
        {
            mSounds.Remove(sound);
            Destroy(sound.gameObject);
        }
    }
    #endregion
}
