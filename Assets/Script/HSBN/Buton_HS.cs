using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public delegate void ButtonPressedDelegate(string idBenhNhan);
    private string _ID_BenhNhan;
    public static NewBehaviourScript instance;

    public void Awake()
    {
        instance = this;
    }
    public void SetIDBenhNhan(string idBenhNhan)
    {
        _ID_BenhNhan = idBenhNhan;
    }
    public void ButtonPressed()
    {
        PlayerPrefs.SetString("ID_BenhNhan", _ID_BenhNhan);  
        SceneManager.LoadScene("Donthuoc");
    }
}
