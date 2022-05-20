using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuActions : MonoBehaviour
{
    public Animator canvasAnimator;

    [SerializeField]
    private Toggle fullscreenToggle;
    [SerializeField]
    private Slider volume, bgmVolume;
    [SerializeField]
    private Toggle res1, res2, res3;
    [SerializeField]
    private AudioSource menuMusic;
    [SerializeField]
    private GameObject warningPanel;
    [SerializeField]
    private LevelSelecting levels;

    private bool inited = false;

    void Awake()
    {
        GameManager.resolution = PlayerPrefs.GetInt("Resolution", 0);
        GameManager.fullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 0 ? false : true;
        GameManager.bgmVolume = PlayerPrefs.GetFloat("BGM", 1.0f);
        GameManager.sfxVolume = PlayerPrefs.GetFloat("SFX", 1.0f);

        inited = false;
        menuMusic.volume = GameManager.bgmVolume;
        bgmVolume.value = GameManager.bgmVolume;
        volume.value = GameManager.sfxVolume;
        fullscreenToggle.isOn = GameManager.fullscreen;
        Screen.fullScreen = GameManager.fullscreen;
        switch (GameManager.resolution)
        {
            default:
                Screen.SetResolution(1920, 1080, GameManager.fullscreen);
                res1.isOn = true;
                res2.isOn = false;
                res3.isOn = false;
                break;
            case 1:
                Screen.SetResolution(1280, 720, GameManager.fullscreen);
                res2.isOn = true;
                res1.isOn = false;
                res3.isOn = false;
                break;
            case 2:
                Screen.SetResolution(960, 540, GameManager.fullscreen);
                res3.isOn = true;
                res1.isOn = false;
                res2.isOn = false;
                break;
        }
        inited = true;

        if (GameManager.played && PlayerPrefs.GetInt("Progress", 0) > 0)
        {
            Levels();
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void Settings()
    {
        canvasAnimator.Play("MenuSettings", 0);
        Debug.Log(GameManager.bgmVolume + "-" + GameManager.sfxVolume + ", " + GameManager.fullscreen + ", " + GameManager.resolution);
    }

    public void GameStart()
    {
        Debug.Log(PlayerPrefs.GetInt("Progress", 0));
        if (PlayerPrefs.GetInt("Progress", 0) == 0)
        {
            //Go to tutorial
            EnterTutorial();
        }
        else
        {
            //Go to level selection
            Levels();
        }
    }

    public void Levels()
    {
        levels.Init();
        canvasAnimator.Play("MenuLevels", 0);
    }

    public void CloseSettings()
    {
        canvasAnimator.Play("MenuCloseSettings", 0);
    }

    public void CloseLevels()
    {
        canvasAnimator.Play("MenuLevelsClose", 0);
    }

    public void OnFullscreenUpdate()
    {
        GameManager.fullscreen = fullscreenToggle.isOn;
        PlayerPrefs.SetInt("Fullscreen", GameManager.fullscreen ? 1 : 0);
        Screen.fullScreen = GameManager.fullscreen;

        //Debug.Log(GameManager.fullscreen);
    }

    public void OnMusicVolumeUpdate()
    {
        GameManager.bgmVolume = bgmVolume.value;
        PlayerPrefs.SetFloat("BGM", GameManager.bgmVolume);
        menuMusic.volume = GameManager.bgmVolume;

        //Debug.Log(GameManager.bgmVolume);
    }

    public void OnVolumeUpdate()
    {
        GameManager.sfxVolume = volume.value;
        PlayerPrefs.SetFloat("SFX", GameManager.sfxVolume);

        //Debug.Log(GameManager.sfxVolume);
    }

    public void OnResolutionUpdate()
    {
        //Debug.Log(inited+"INI");
        if (!inited) return;
        inited = false;

        if (res1.isOn)
        {
            GameManager.resolution = 0;
        }
        else if (res2.isOn)
        {
            GameManager.resolution = 1;
        }
        else
        {
            GameManager.resolution = 2;
        }
        PlayerPrefs.SetInt("Resolution", GameManager.resolution);

        switch (GameManager.resolution)
        {
            default:
                Screen.SetResolution(1920, 1080, GameManager.fullscreen);
                res1.isOn = true;
                res2.isOn = false;
                res3.isOn = false;
                break;
            case 1:
                Screen.SetResolution(1280, 720, GameManager.fullscreen);
                res2.isOn = true;
                res1.isOn = false;
                res3.isOn = false;
                break;
            case 2:
                Screen.SetResolution(960, 540, GameManager.fullscreen);
                res3.isOn = true;
                res1.isOn = false;
                res2.isOn = false;
                break;
        }
        inited = true;

        //Debug.Log(GameManager.resolution);
    }

    public void ClearGameProgress()
    {
        PlayerPrefs.SetInt("Progress", 0);
        warningPanel.SetActive(false);
        Debug.Log(PlayerPrefs.GetInt("Progress", 0));
    }

    public void CancelClearGameProgress()
    {
        warningPanel.SetActive(false);
    }

    public void AttemptToClearGameProgress()
    {
        warningPanel.SetActive(true);
    }

    public void EnterTutorial()
    {
        SceneController.loadedLevelIndex = 0;
        canvasAnimator.Play("GameLevel", 1);
    }

    public void EnterLevel(int level)
    {
        SceneController.loadedLevelIndex = level;
        canvasAnimator.Play("GameLevel", 1);
    }

}
