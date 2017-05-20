using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour
{
    void Start()
    {
        MainMenuHolder.SetActive(true);
        OptionsMenuHolder.SetActive(false);
        TitleMenu.text = "Menu Options";
    }
    void Awake()
    {
        MainMenuHolder.SetActive(true);
        OptionsMenuHolder.SetActive(false);
        TitleMenu.text = "Menu Main";
    }
    public Text TitleMenu;
    public GameObject MainMenuHolder;
    public GameObject OptionsMenuHolder;
    public Toggle[] resoluccionToggles;
    public Toggle Audio;
    public Slider[] volumeSliders;
    public int[] screenWidths;
    private int activeScreenResolutionIndex;
    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        MainMenuHolder.SetActive(true);
        OptionsMenuHolder.SetActive(false);
        TitleMenu.text = "Menu Options";
    }
    public void OptionsMenu()
    {
        TitleMenu.text = "Main Menu";
        MainMenuHolder.SetActive(false);
        OptionsMenuHolder.SetActive(true);
    }
    public void SetScreenResolucion(int i)
    {
        AudioManager.Instance.playSoundEfect("CheckBoxMenuSound");
        if (resoluccionToggles[i].isOn)
        {
            activeScreenResolutionIndex = i;

            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / CalcAspect()), false);
        }
    }
    public void SetMasterVolume(float num)
    {
        AudioManager.Instance.playSoundEfect("SliderMenuSound");
        AudioManager.Instance.SetVolume(num, AudioManager.AudioChannel.Master);
        AudioManager.Instance.SetVolume(num, AudioManager.AudioChannel.Music);
        AudioManager.Instance.SetVolume(num, AudioManager.AudioChannel.Sfx);
        foreach ( Slider s in volumeSliders)
        {
            s.value = num;
        }
    }
    public void SetMusicVolume(float num)
    {
        AudioManager.Instance.playSoundEfect("SliderMenuSound");
       // AudioManager.Instance.PlaySound("SliderMenuSound", transform.position);
        AudioManager.Instance.SetVolume(num,AudioManager.AudioChannel.Music);
    }
    public void SetSFXVolume(float num)
    {
        AudioManager.Instance.playSoundEfect("SliderMenuSound");
      //  AudioManager.Instance.PlaySound("SliderMenuSound", transform.position);
        AudioManager.Instance.SetVolume(num, AudioManager.AudioChannel.Sfx);
    }
    public void SetFullScreen(bool isFullScreen)
    {
        AudioManager.Instance.playSoundEfect("CheckBoxMenuSound");
        for (int i = 0; i < resoluccionToggles.Length; i++)
        {
            resoluccionToggles[i].interactable= !isFullScreen;
        }
        if(isFullScreen)
        {
            Resolution[] allResolucions = Screen.resolutions;
            Resolution maxResolution = allResolucions[allResolucions.Length - 1];
            Screen.SetResolution(maxResolution.height, maxResolution.width, true);

        }
        else
        {
            SetScreenResolucion(activeScreenResolutionIndex);
        }
    }
    public void SetAudioOnOff(bool audio)
    {
        if (audio)
        {
            foreach (Slider s in volumeSliders)
            {
                s.value = 0.5f;
            }
            Audio.GetComponentInChildren<Text>().text = "Audio On";
            SetMasterVolume(0.5f);
        }
        else
        {
            SetMasterVolume(0.0f);
            foreach (Slider s in volumeSliders)
            {
                s.value = 0.0f;
            }
            Audio.GetComponentInChildren<Text>().text = "Audio Off";
        }
    }
    private float CalcAspect()
    {
        float r = Screen.width / Screen.height;
        string _r = r.ToString("F2");
        string ratio = _r.Substring(0, 4);
        return r;
      
    }
}
