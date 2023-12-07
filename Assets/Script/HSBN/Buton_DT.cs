using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buton_DT : MonoBehaviour
{
    public TextMeshProUGUI _IDDT;
    private void Start()
    {
        PlayerPrefs.SetString("_ID_Donthuoc", _IDDT.text);
    }
    public void CHTS_DT()
    {
        SceneManager.LoadScene("MenuCTHSBN");
    }
}
