using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows;
using static System.Net.WebRequestMethods;

public class update_all : MonoBehaviour
{
    public static bool _Add_MDT;
    string url;
    public string _ID_Donthuoc;
    [SerializeField] private Transform content;
    public void updatdata()
    {
        _ID_Donthuoc = PlayerPrefs.GetString("_ID_Donthuoc");
        Connect_update();
    }
    IEnumerator Connect_update()
    {
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("_ID_Donthuoc", _ID_Donthuoc));
        formData.Add(new MultipartFormDataSection("_ID_Donthuoc", _ID_Donthuoc));
        if (update_all._Add_MDT == true)
        {
            url = " http://localhost/Unity-CNPM/Update_dose.php";

        }
        else
        {
            url = " http://localhost/Unity-CNPM/Update_dose.php";
        }
        using (UnityWebRequest www = UnityWebRequest.Post(url, formData))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseData = www.downloadHandler.text;
                Debug.Log(responseData);
                ProcessData_Update(responseData);
            }
            else
            {
                // Yêu cầu thất bại, hiển thị lỗi
                Debug.Log("Error: " + www.error);
            }
        }
    }
    public void ProcessData_Update(string jsonString)
    {
        var data = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonString);
        string message = data["message"];
        Debug.Log(message);
        if (message == "Data updated successfully")
        {
        }
    }
}
