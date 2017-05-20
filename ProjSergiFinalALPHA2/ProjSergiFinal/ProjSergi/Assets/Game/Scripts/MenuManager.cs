using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//public class MenuManager : MonoBehaviour
//{
//    public GameObject MainMenuHolder;
//    public GameObject OptionsMenuHolder;
//    public Toggle[] resoluccionToggles;
//    public Slider[] volumeSliders;
//    public int[] screenWidths;
//    private int activeScreenResolutionIndex;
//    public void Play()
//    {
//        SceneManager.LoadScene("");
//    }

//    public void Quit()
//    {
//        Application.Quit();
//    }
//    public void MainMenu()
//    {
//        MainMenuHolder.SetActive(true);
//        OptionsMenuHolder.SetActive(false);
//    }
//    public void OptionsMenu()
//    {

//        MainMenuHolder.SetActive(false);
//        OptionsMenuHolder.SetActive(true);
//    }
//    public void SetScreenResolucion(int i)
//    {
//        if (resoluccionToggles[i].isOn)
//        {
//            activeScreenResolutionIndex = i;

//            Screen.SetResolution(screenWidths[i], (int)(screenWidths[i] / CalcAspect()), false);
//        }
//    }
//    public void SetMasterVolume(float num)
//    {
        
//    }
//    public void SetMusicVolume(float num)
//    {

//    }
//    public void SetSFXVolume(float num)
//    {

//    }
//    public void SetFullScreen(bool isFullScreen)
//    {
       
//        for(int i = 0; i < resoluccionToggles.Length; i++)
//        {
//            resoluccionToggles[i].interactable= !isFullScreen;
//        }
//        if(isFullScreen)
//        {
//            Resolution[] allResolucions = Screen.resolutions;
//            Resolution maxResolution = allResolucions[allResolucions.Length - 1];
//            Screen.SetResolution(maxResolution.height, maxResolution.width, true);

//        }
//        else
//        {
//            SetScreenResolucion(activeScreenResolutionIndex);
//        }
//    }
//    private float CalcAspect()
//    {
//        float r = Screen.width / Screen.height;
//        string _r = r.ToString("F2");
//        string ratio = _r.Substring(0, 4);
//        return r;
      
//    }
//}
