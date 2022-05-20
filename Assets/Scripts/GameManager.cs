using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool played = false;

    [Header("Settings")]
    public static bool fullscreen = true;
    [Tooltip("0-1920x1080, 1-1280x720, 2-960x540")]
    public static int resolution = 0;
    public static float sfxVolume = 1.0f, bgmVolume = 1.0f;

    [Header("Setups")]
    public Canvas worldCanvas;
    public Animator canvasAnimator;
    public AudioSource bgm;
    public string[] levelHint;
    [SerializeField]
    private AudioClip levelClear;
    [SerializeField]
    private AudioSource sfxSource;

    [Header("UI Setups")]
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider sfxSlider;
    private bool UIUpdateing = false;

    [Header("In-Game Variables")]
    [SerializeField]
    private bool[] sceneBools;
    [Tooltip("0-Running, 1-Conversation, 2-Paused, 3-Retrying, 4-Winning, 5-AfterWinning")]
    public int state = 0;
    private float winningTimer = 5.0f;

    private void Awake()
    {
        played = true;

        instance = this;
        bgm.volume = bgmVolume;
        UIUpdateing = true;
        bgmSlider.value = bgmVolume;
        sfxSlider.value = sfxVolume;
        UIUpdateing = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if(state == 4) {
            Time.timeScale = 0;
            winningTimer -= Time.unscaledDeltaTime;
            if(winningTimer <= -0.02)
            {
                AfterWinning();
                state = 5;
                Time.timeScale = 1;
            }
        }
    }

    public void SetSceneBool(int index, bool value)
    {
        if(index >= 0 && index < sceneBools.Length)
        {
            sceneBools[index] = value;
        }
    }

    public bool GetSceneBool(int index)
    {
        if (index >= 0 && index < sceneBools.Length)
        {
            return sceneBools[index];
        }
        else
        {
            return false;
        }
    }

    public void Retry()
    {
        Time.timeScale = 1;
        canvasAnimator.Play("Retry", 1);
        state = 3;
    }

    public void Quit()
    {
        Time.timeScale = 1;
        canvasAnimator.Play("GameMenu", 1);
        state = 3;
    }

    public void Pause()
    {
        if(state == 0)
        {
            Time.timeScale = 0;
            state = 2;
            pauseMenu.SetActive(true);
        }
        else if(state == 2)
        {
            Time.timeScale = 1;
            state = 0;
            pauseMenu.SetActive(false);
        }
    }

    public void UpdateVolume()
    {
        if (UIUpdateing) return;
        bgmVolume = bgmSlider.value;
        sfxVolume = sfxSlider.value;
        PlayerPrefs.SetFloat("BGM", bgmVolume);
        PlayerPrefs.SetFloat("SFX", sfxVolume);
        bgm.volume = bgmVolume;
    }

    public void LevelClear()
    {
        int prog = SceneController.loadedLevelIndex + 1;
        if(prog > PlayerPrefs.GetInt("Progress", 0))
        {
            PlayerPrefs.SetInt("Progress", prog);
        }
        Debug.Log(PlayerPrefs.GetInt("Progress", 0) + " Level");
        bgm.Stop();
        bgm.PlayOneShot(levelClear);
        state = 4;
        winningTimer = 4.5f;
    }

    private void AfterWinning()
    {
        canvasAnimator.Play("GameMenu", 1);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.volume = sfxVolume;
        sfxSource.PlayOneShot(clip);
    }

}
