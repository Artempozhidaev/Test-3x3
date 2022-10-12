using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Audio : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text_VolumeValue;
    [SerializeField] private Slider SliderGo;
    private AudioSource m_AudioSource;
    float m_Volume = 0.5f;
    private void Start()
    {
        m_Volume =  PlayerPrefs.GetFloat("Volume");
        SliderGo.value = m_Volume;
        m_AudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        m_AudioSource.volume = m_Volume;
        text_VolumeValue.text = (m_Volume * 100).ToString("0") + "%";
    }

    public void SetVolume(float Vol)
    {
        m_Volume = Vol;
        PlayerPrefs.SetFloat("Volume", Vol);
    }
}
