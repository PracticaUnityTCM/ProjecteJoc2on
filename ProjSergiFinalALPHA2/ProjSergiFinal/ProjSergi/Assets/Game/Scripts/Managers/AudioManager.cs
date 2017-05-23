using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public enum AudioChannel { Master, Sfx, Music };
    public float masterVolumePercent= 0.5f;
    public float sfxVolumePercent= 0.5f;
    public float musicVolumePercent = 0.5f;

    AudioSource sfx2DSource, sfxLoopASource;//sfxMenuAudioSource;
    AudioSource[] musicSources;
    int activeMusicSourceIndex;
    private AudioMixerGroup MusicMixer;
    private AudioMixerGroup SFXMixer;
    private AudioMixerGroup MenuSoundMixer;
    private AudioMixer MasterMixer;
     
    private static AudioManager _instance = null;
    private AudioSource AudioSourceLoop;
     
    Transform audioListener;
    Transform playerT;
    SoundLibrary library;

    public static AudioManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
               
                MasterMixer = Resources.Load("MasterMixer") as AudioMixer;
                MasterMixer.updateMode = AudioMixerUpdateMode.Normal;
              
                AudioMixerGroup[] nn= MasterMixer.FindMatchingGroups("MusicMixer");
                AudioMixerGroup[] n1n = MasterMixer.FindMatchingGroups("SfxSounds");
           //     AudioMixerGroup[] n12n = MasterMixer.FindMatchingGroups("MenuMixer");
                MusicMixer = nn[0];

                SFXMixer = n1n[0];
                Debug.Log(n1n + "mixer");
              //  MenuSoundMixer = n12n[0];
               
                AudioListener[] au = FindObjectsOfType<AudioListener>();
         
                _instance = this;
                
                musicSources = new AudioSource[2];
                AudioSourceLoop = new AudioSource();
                for (int i = 0; i < 2; i++)
                {
                    GameObject newMusicSource = new GameObject("Music source " + (i + 1));
                    musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                    newMusicSource.transform.parent = transform;
                    musicSources[i].loop=true;
                    musicSources[i].outputAudioMixerGroup = MusicMixer;
                    musicSources[i].volume = 0.5f;
                }
                GameObject newSfx2Dsource = new GameObject("sfx source");
                sfx2DSource = newSfx2Dsource.AddComponent<AudioSource>();
                sfx2DSource.outputAudioMixerGroup = SFXMixer;
                newSfx2Dsource.transform.parent = transform;
                sfx2DSource.volume = 0.5f;

              
                sfx2DSource.playOnAwake = false;
                //GameObject  newSfxLoopSource = new GameObject("sfx SourceLoop");
                //sfxLoopASource = newSfxLoopSource.AddComponent<AudioSource>();
                //newSfxLoopSource.transform.parent = transform;
                //sfxLoopASource.volume = 1f;
                //sfxLoopASource.loop = true;
                //sfxLoopASource.playOnAwake = false;
                //sfxLoopASource.outputAudioMixerGroup = SFXMixer;
                //GameObject newSfxMenuSource = new GameObject("sfx SourceMenu");
                //sfxMenuAudioSource = newSfxMenuSource.AddComponent<AudioSource>();
                //newSfxMenuSource.transform.parent = transform;
                //sfxMenuAudioSource.volume = 1f;
                //sfxMenuAudioSource.loop= false;
                //sfxMenuAudioSource.playOnAwake = false;
                //sfxMenuAudioSource.outputAudioMixerGroup = MenuSoundMixer;

                library = GetComponent<SoundLibrary>();
                audioListener = FindObjectOfType<AudioListener>().transform;
                if (FindObjectOfType<ShipController>() != null)
                {
                    playerT = FindObjectOfType<ShipController>().transform;
                }
             
                masterVolumePercent = PlayerPrefs.GetFloat("master vol", 1);
                sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", 1);
                Debug.Log(sfxVolumePercent + "s");
                musicVolumePercent = PlayerPrefs.GetFloat("music vol", 1);
                MusicMixer.audioMixer.SetFloat("Attenuation",musicVolumePercent);
                SFXMixer.audioMixer.SetFloat("Attenuation", sfxVolumePercent);
                MasterMixer.SetFloat("Attenuation", masterVolumePercent);
            }
        }
    }
    void OnLevelWasLoaded(int index)
    {
        if (playerT == null)
        {
            if (FindObjectOfType<ShipController>() != null)
            {
                playerT = FindObjectOfType<ShipController>().transform;
            }
        }
    }
    void Update()
    {
        if (playerT != null)
        {
            audioListener.position = playerT.position;
        }
    }
    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                MasterMixer.SetFloat("Attenuation", volumePercent);
                break;
            case AudioChannel.Sfx:
                SFXMixer.audioMixer.SetFloat("Attenuation", volumePercent);
                sfxVolumePercent = volumePercent;
               
                break;
            case AudioChannel.Music:
                MusicMixer.audioMixer.SetFloat("Attenuation", volumePercent);
               musicVolumePercent = volumePercent;
                break;
        }
     
        musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        musicSources[1].volume = musicVolumePercent * masterVolumePercent;
        Debug.Log(sfxVolumePercent + "ssf");
        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
        PlayerPrefs.Save();
    }
    //public void PlaySound(AudioClip clip, Vector3 pos)
    //{
    //    if (clip != null)
    //    {
    //        AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
    //    }
    //}
    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();
        StartCoroutine(AnimateMusicCrossfade(fadeDuration));
    }
    public void playSoundEfect(string name)
    {
      //  AudioSource.PlayClipAtPoint(library.GetClipFromName(name), new Vector3(0, 0, 0));
        sfx2DSource.PlayOneShot(library.GetClipFromName(name), sfxVolumePercent*masterVolumePercent);
    }
    //public void PlaySound(string soundName, Vector3 pos)
    //{
    //    PlaySound(library.GetClipFromName(soundName), pos);
    //}
    //public void CannonShotAudioStart()
    //{ 
    //   sfx2DSource.PlayOneShot(library.GetClipFromName("CannonShot"), masterVolumePercent* sfxVolumePercent);
    //}
    
    //public void CannonTouchWater()
    //{
    //    sfx2DSource.PlayOneShot(library.GetClipFromName("SplashWater"), masterVolumePercent * sfxVolumePercent);
    //}
    bool startPress = false;
    public void FireThrowerAudioStart(Vector3 pos)
    {
        if (!startPress)
        {
           
            sfx2DSource.PlayOneShot(library.GetClipFromName("StartFireThrower"), masterVolumePercent * sfxVolumePercent);
            startPress = true;
           // StartCoroutine(WaitFor(6f));
        }
       
    }
    
    public void FireThrowerAudioCenter()
    {   
        sfxLoopASource.clip = library.GetClipFromName("CenterFireThrower");
        sfxLoopASource.Play();
    }
    public void FireThrowerAudioEnd()
    {
       
        startPress = false;
        StartCoroutine(WaitFor(2f));
        sfxLoopASource.Stop();
        sfx2DSource.PlayOneShot(library.GetClipFromName("EndFireThrower"), masterVolumePercent * sfxVolumePercent);
    }
    IEnumerator WaitFor(float num)
    {
        yield return new WaitForSeconds(num);
    }
    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, musicVolumePercent * masterVolumePercent, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0, percent);
            yield return null;
        }
    }
}


