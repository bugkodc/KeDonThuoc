using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class View_delete_Thuoc : MonoBehaviour
{
    private string _ID_thuoc;
    private string _tenthuoc;
    [SerializeField] public TextMeshProUGUI _input_IDthuoc;
    [SerializeField] public TextMeshProUGUI _input_tenthuoc;

    public void SetID(string IDthuoc , string tenthuoc)
    {
        _ID_thuoc = IDthuoc;
        _tenthuoc = tenthuoc;
    }

    public void ViewThuoc()
    {
        SetID(_input_IDthuoc.text, _input_tenthuoc.text);
        PlayerPrefs.SetString("_TenThuoc", _tenthuoc);
        SceneManager.LoadScene("Viewthuoc");
    }

    public void DeleteThuoc()
    {
        SetID(_input_IDthuoc.text, _input_tenthuoc.text);
        StartCoroutine(Connect());
        Destroy(gameObject);
    }
    IEnumerator Connect()
    {
        // Tạo dữ liệu gửi đi
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("idDonThuoc", PlayerPrefs.GetString("_ID_Donthuoc")));
        formData.Add(new MultipartFormDataSection("_ID_thuoc", _ID_thuoc));
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/Xoa_thuoc.php", formData))
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
    [System.Serializable]
    public class IDThuoc
{
    [JsonProperty("idThuoc")]
    public string _idThuoc;
}