using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonscontrol : MonoBehaviour
{
    //������� ���������� ������

    [SerializeField]private GameObject VolumeMenu;
    [SerializeField]private GameObject StartMenu;
    [SerializeField]private GameObject sysMenu;
    [SerializeField]private GameObject GameMenu;
    [SerializeField]private GameObject GameCord;

    public void VolumeClick() // ����� ���� � ����������
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
    public void StartClick() //������� �� ���������� ������ �� ������� ����
    {
        StartMenu.SetActive(false);
        GameMenu.SetActive(true);
    }

    public void AgainClick() //������ ������ ������
    {
        GameContr.Instance.RefreshLevel();
        sysMenu.SetActive(false);
    }

    public void CloseVolumeClick()
    {
        VolumeMenu.SetActive(false);
    }
}
