using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonscontrol : MonoBehaviour
{
    //Объекты управления сценой

    [SerializeField]private GameObject VolumeMenu;
    [SerializeField]private GameObject StartMenu;
    [SerializeField]private GameObject sysMenu;
    [SerializeField]private GameObject GameMenu;
    [SerializeField]private GameObject GameCord;

    public void VolumeClick() // Показ окна с громкостью
    {
        if (VolumeMenu.activeInHierarchy)
        {
            VolumeMenu.SetActive(false);
        }
        else
        {
            VolumeMenu.SetActive(true);
        }
    }
    public void StartClick() //Переход со стартового экрана на игровое поле
    {
        StartMenu.SetActive(false);
        GameMenu.SetActive(true);
    }

    public void AgainClick() //Запуск уровня заново
    {
        GameContr.Instance.RefreshLevel();
        sysMenu.SetActive(false);
    }

    public void CloseVolumeClick()
    {
        VolumeMenu.SetActive(false);
    }
}
