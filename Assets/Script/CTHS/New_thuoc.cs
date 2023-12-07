using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class New_thuoc : MonoBehaviour
{
    [SerializeField] public Button _NewThuoc;
    [SerializeField] public GameObject CTHS;
    [SerializeField] public GameObject _Thuoc;
    [SerializeField] public GameObject _Input;
    CTHS _CTHS;
    void Start()
    {
        _NewThuoc.onClick.AddListener(SET);
        _NewThuoc.onClick.AddListener(input_data);
    }
    public void SET()
    {
        CTHS.SetActive(false);
        _Thuoc.SetActive(true);
    }
    public void input_data()
    {
        _Input.transform.Find("MDTinput").GetComponent<TextMeshProUGUI>().text = _CTHS._Input.transform.Find("MDTinput").GetComponent<TextMeshProUGUI>().text;
        _Input.transform.Find("HotenInput").GetComponent<TextMeshProUGUI>().text = _CTHS._Input.transform.Find("hoteninput").GetComponent<TextMeshProUGUI>().text;
        _Input.transform.Find("ageinput").GetComponent<TextMeshProUGUI>().text = _CTHS._Input.transform.Find("ageInput").GetComponent<TextMeshProUGUI>().text;
        _Input.transform.Find("gioiinput").GetComponent<TextMeshProUGUI>().text = _CTHS._Input.transform.Find("Sexinput").GetComponent<TextMeshProUGUI>().text;
        _Input.transform.Find("CDinput").GetComponent<TextMeshProUGUI>().text = _CTHS._Input.transform.Find("CDinput").GetComponent<TextMeshProUGUI>().text;

    }
}
