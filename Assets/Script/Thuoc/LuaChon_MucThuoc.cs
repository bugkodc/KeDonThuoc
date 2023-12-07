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

public class LuaChon_MucThuoc : MonoBehaviour
{
    [SerializeField]  public TMP_Dropdown _MucThuoc;
    [SerializeField] public TMP_Dropdown _Thuoc;
    public string selectedOption_Mucthuoc;
    public string selectedOption_thuoc;
    [SerializeField] public GameObject _Input;
    void Start()
    {
        _MucThuoc.ClearOptions();
        _MucThuoc.onValueChanged.AddListener(OnDropdownValueChanged);
        _Thuoc.ClearOptions();
        _Thuoc.onValueChanged.AddListener(OnDropdownValueChanged_Thuoc);
        CallConnect();
    }
    public void HSBN()
    {
        SceneManager.LoadScene("MenuHSBN");
    }
    public void CallConnect()
    {
        StartCoroutine(Connect());
    }
    #region connect
    IEnumerator Connect()
    {
        string url = "http://localhost/Unity-CNPM/Luachon_MucThuoc.php";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseData = www.downloadHandler.text;
            Debug.Log(responseData);
            // Xử lý dữ liệu JSON được nhận về
            ProcessData_Mucthuoc(responseData);
        }
        else
        {
            Debug.Log("Lỗi kết nối: " + www.error);
        }
    }
    #endregion
    #region Connect_lieuluong
    IEnumerator Connect_lieuluong()
    {
        string url = "http://localhost/Unity-CNPM/Luachon_MucThuoc.php";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseData = www.downloadHandler.text;
            Debug.Log(responseData);
            // Xử lý dữ liệu JSON được nhận về
            ProcessData_lieuluong(responseData);
        }
        else
        {
            Debug.Log("Lỗi kết nối: " + www.error);
        }
    }
    #endregion
    #region Connect_Tenthuoc
    IEnumerator Connect_Tenthuoc()
    {
        // Tạo dữ liệu gửi đi
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("tenLoaiThuoc", selectedOption_Mucthuoc)); // Thay thế giá trị_ID_BenhNhan bằng giá trị thực tế bạn muốn gửi
        // Gửi yêu cầu POST đến file PHP
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/Luachon_Thuoc.php", formData))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseData = www.downloadHandler.text;
                Debug.Log(responseData);
                ProcessData_thuoc(responseData);
            }
            else
            {
                // Yêu cầu thất bại, hiển thị lỗi
                Debug.Log("Error: " + www.error);
            }
        }
    }
    #endregion
    #region Connect_MinMax
    IEnumerator Connect_MinMax()
    {
        // Tạo dữ liệu gửi đi
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("tenThuoc", selectedOption_thuoc)); // Thay thế giá trị_ID_BenhNhan bằng giá trị thực tế bạn muốn gửi
        // Gửi yêu cầu POST đến file PHP
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/Data_Dose.php", formData))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseData = www.downloadHandler.text;
                Debug.Log(responseData);
                ProcessData_lieuluong(responseData);
            }
            else
            {
                // Yêu cầu thất bại, hiển thị lỗi
                Debug.Log("Error: " + www.error);
            }
        }
    }
    #endregion
    #region ProcessData
    private void ProcessData_Mucthuoc(string jsonData)
    {
        // Giải mã dữ liệu JSON thành một danh sách các mảng đối tượng CTHoSoBenhNhan
        List<List<LuaChonMucThuoc>> dataList = JsonConvert.DeserializeObject<List<List<LuaChonMucThuoc>>>(jsonData);
        List<LuaChonMucThuoc> innerList = dataList[0];
        foreach (LuaChonMucThuoc item in innerList)
        {
            _MucThuoc.options.Add(new TMP_Dropdown.OptionData(item._Loaithuoc));
        }
    }
    private void ProcessData_thuoc(string jsonData)
    {
        // Giải mã dữ liệu JSON thành một danh sách các mảng đối tượng CTHoSoBenhNhan
        List<LuaChonThuoc> dataList = JsonConvert.DeserializeObject<List<LuaChonThuoc>>(jsonData);
        foreach (LuaChonThuoc item in dataList)
        {
            _Thuoc.options.Add(new TMP_Dropdown.OptionData(item._Tenthuoc));
        }
    }
    private void ProcessData_lieuluong(string jsonData)
    {
        // Giải mã dữ liệu JSON thành một danh sách các mảng đối tượng CTHoSoBenhNhan
        List<Lieuluong> dataList = JsonConvert.DeserializeObject<List<Lieuluong>>(jsonData);
        foreach (Lieuluong item in dataList)
        {
            PlayerPrefs.SetString("_ID_thuoc", item._idThuoc);
            _Input.transform.Find("lieumininput").GetComponent<TextMeshProUGUI>().text = item._Lieumin;
            _Input.transform.Find("lieumaxinput").GetComponent<TextMeshProUGUI>().text = item._lieumax;
        }
    }
    #endregion
    #region OnDropdownValueChanged
    public void OnDropdownValueChanged(int index)
    {
        selectedOption_Mucthuoc = _MucThuoc.options[index].text;
        StartCoroutine(Connect_Tenthuoc());
    }
    public void OnDropdownValueChanged_Thuoc(int index)
    {
        selectedOption_thuoc = _Thuoc.options[index].text;
        PlayerPrefs.SetString("selectedOption_thuoc", selectedOption_thuoc);
        StartCoroutine(Connect_MinMax());
    }
    #endregion
}
[System.Serializable]
    public class LuaChonMucThuoc
    {
        [JsonProperty("tenLoaiThuoc")]
        public string _Loaithuoc;
    }
public class LuaChonThuoc
{
    [JsonProperty("tenThuoc")]
    public string _Tenthuoc;
}
public class Lieuluong
{
    [JsonProperty("idThuoc")]
    public string _idThuoc;
    [JsonProperty("lieuDungmin")]
    public string _Lieumin;
    [JsonProperty("lieuDungmax")]
    public string _lieumax;
}