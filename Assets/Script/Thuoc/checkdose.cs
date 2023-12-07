using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;


public class checkdose : MonoBehaviour
{
    [SerializeField] public Button _checkdoseButton;
    [SerializeField] public Button _resetdose;
    [SerializeField] public TMP_InputField LieuLuongInput;
    [SerializeField] public TMP_InputField LieuDungInput;
    [SerializeField] public TMP_InputField ngaysudung;
    [SerializeField] public TextMeshProUGUI lieumininput;
    [SerializeField] public TextMeshProUGUI lieumaxinput;
    [SerializeField] public TextMeshProUGUI thongbao;
    [SerializeField] public TMP_Dropdown _Tansuat;
    float _Soluong;
    float _Lieuchidinh;

    private void Start()
    {
        _checkdoseButton.onClick.AddListener(CheckDose);
        _resetdose.onClick.AddListener(resetdose);
    }
    public void CheckDose()
    {
        int currentIndex = _Tansuat.value;
        float.TryParse(LieuLuongInput.text, out _Soluong);
        float.TryParse(LieuDungInput.text, out _Lieuchidinh);
        float _dosemin ;
        float.TryParse(lieumininput.text, out _dosemin);
        float _dosmax;
        float.TryParse(lieumaxinput.text, out _dosmax);
        if (_Soluong * _Lieuchidinh > _dosmax || _Soluong * _Lieuchidinh < _dosemin)
        {
            thongbao.text = "Thông báo : Số lượng và liều chỉ định không hợp lý";
        }
        else
        {

            StartCoroutine(Connect());
            SceneManager.LoadScene("MenuCTHSBN");
        }
        IEnumerator Connect()
        {
            // Tạo dữ liệu gửi đi
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            formData.Add(new MultipartFormDataSection("_ID_Donthuoc", PlayerPrefs.GetString("_ID_Donthuoc")));
            formData.Add(new MultipartFormDataSection("_ID_thuoc", PlayerPrefs.GetString("_ID_thuoc")));
            formData.Add(new MultipartFormDataSection("_Soluong", _Soluong.ToString()));
            formData.Add(new MultipartFormDataSection("_Lieuchidinh", _Lieuchidinh.ToString()));
            formData.Add(new MultipartFormDataSection("_thoigian", ngaysudung.text));
            formData.Add(new MultipartFormDataSection("_tansuat", _Tansuat.options[currentIndex].text));
            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/Them_Donthuoc.php", formData))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    string responseData = www.downloadHandler.text;
                    Debug.Log(responseData);
                    ProcessData_them(responseData);
                }
                else
                {
                    // Yêu cầu thất bại, hiển thị lỗi
                    Debug.Log("Error: " + www.error);
                }
            }

        }
    }
    public void ProcessData_them(string jsonString)
    {
        var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        string message = data["message"];
        Debug.Log(message);
        if (message == "Data updated successfully")
        {
            thongbao.text = "Cập nhật liều dùng thành công!";
        }
    }
    public void resetdose()
    {
        LieuLuongInput.text = null;
        LieuDungInput.text = null;
    }
}
