using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMaster : MonoBehaviour
{
    //PlayerPrefs keys
    public const string KEY_BGM_VAlUE = "KEY_BGM_VAlUE";
    public const string KEY_UIFX_VAlUE = "KEY_UIFX_VAlUE";

    public static SoundMaster instance;

    public const float MIN = 0.001f;
    public const float MAX = 1f;

    public static bool _muteBGM
    {
        get
        {
            return instance.muteBGM;
        }

        set
        {
            instance.muteBGM = value;
        }
    }
    public static float _volumeBGM
    {
        get
        {
            return instance.volumeBGM;
        }

        set
        {
            instance.volumeBGM = value;
        }
    }
    public static float _volumeUIFX
    {
        get
        {
            return instance.volumeUIFX;
        }

        set
        {
            instance.volumeUIFX = value;
        }
    }

    public bool muteBGM = false;

    [Range(MIN, MAX)]
    public float volumeBGM = 0.8f;

    [Range(MIN, MAX)]
    public float volumeUIFX = 1f;

    public AudioMixer mixer;

    public static string BGM = "Music"; // музыка
    public static string UI = "UI"; // UI
    public static string FX = "Effects"; //

    public static string M = "Master"; // все

    [Header("Music")]

    public AudioSource BGMMusic;

    [Header("UI")]

    public AudioSource buttonSound; //нажатие на кнопку

    [Header("FX")]

    public AudioSource cellSound; //

    public void OnEnable()
    {
        instance = this;
    }

    void Start()
    {
        _volumeBGM = PlayerPrefs.GetFloat(KEY_BGM_VAlUE, 1f);
        _volumeUIFX = PlayerPrefs.GetFloat(KEY_BGM_VAlUE, 0.8f);
    }

    //преобразовывает логарифмическое значение в линейное
    public static void SetLinearVolume(string soundName, float volume)
    {
        volume = Mathf.Clamp(volume, MIN, MAX);
        instance.mixer.SetFloat(soundName, Mathf.Log(volume) * 20);
    }

    public static void BGMFade(bool down = false)
    {
        instance.StartCoroutine(Fade(down, 0.6f)); //0.6 длинна анимации затемнения экрана при загрузке
    }

    static IEnumerator Fade(bool reverse, float time)
    {
        float start = MIN;
        float end = MAX;

        if (reverse)
        {
            start = MAX;
            end = MIN;
        }

        float speed = 1 / time;

        for (float _t = 0; _t < 1f; _t += Time.deltaTime * speed)
        {
            float value = Mathf.Lerp(start, end, _t);

            SetLinearVolume(BGM, value);

            yield return null;
        }

        SetLinearVolume(BGM, end);

    }

    public static void StopBGM()
    {
        instance.BGMMusic.Pause();
    }

    public static void StartBGM()
    {
        instance.BGMMusic.Play();
    }

    //для кнопки
    public void PlayBTNSound()
    {
        buttonSound.Play();
    }

    //для кнопки
    public void MuteToggle()
    {
        _muteBGM = !_muteBGM;
        SetMute(_muteBGM);
    }

    public static void SetVolumeBGM(float value)
    {
        _volumeBGM = value;
        SetLinearVolume(BGM, _volumeBGM);

        PlayerPrefs.SetFloat(KEY_BGM_VAlUE, _volumeBGM);
    }

    public static void SetVolumeUIFX(float value)
    {
        _volumeUIFX = value;
        SetLinearVolume(UI, _volumeUIFX);
        SetLinearVolume(FX, _volumeUIFX);

        PlayerPrefs.SetFloat(KEY_UIFX_VAlUE, _volumeUIFX);
    }

    public static float GetVolumeBGM()
    {
        return _volumeBGM;
    }

    public static float GetVolumeUIFX()
    {
        return _volumeUIFX;
    }

    public void SetMute(bool m)
    {
        if (m)
        {
            SetLinearVolume(BGM, MIN);
        }
        else
        {
            SetLinearVolume(BGM, MAX);
        }
    }

}
