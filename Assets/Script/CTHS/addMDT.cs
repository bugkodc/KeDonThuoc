using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class addMDT : MonoBehaviour
{
    [SerializeField] public Button _ADD_NEW;


    void Start()
    {
        _ADD_NEW.onClick.AddListener(CallConnect);
    }
    public void CallConnect()
    {
        StartCoroutine(Connect());
    }
    IEnumerator Connect()
    {
        string url = "http://localhost/Unity-CNPM/ADD_DT.php";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseData = www.downloadHandler.text;
            Debug.Log(responseData);
            // Xử lý dữ liệu JSON được nhận về
            PlayerPrefs.SetString("_ID_Donthuoc", responseData);
            StartCoroutine(Connect_adt());
            SceneManager.LoadScene("MenuCTHSBN");
        }
        else
        {
            Debug.Log("Lỗi kết nối: " + www.error);
        }
    }
    IEnumerator Connect_adt()
    {
        // Tạo dữ liệu gửi đi
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("_ID_Donthuoc", PlayerPrefs.GetString("_ID_Donthuoc")));
        formData.Add(new MultipartFormDataSection("ID_BenhNhan", PlayerPrefs.GetString("ID_BenhNhan")));
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/Themthuoc.php", formData))
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
    public void ProcessData_them(string jsonString)
    {
        var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        string message = data["message"];
        Debug.Log(message);
        if (message == "Data updated successfully")
        {
        }
    }

}