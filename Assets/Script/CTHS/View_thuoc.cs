using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class View_thuoc : MonoBehaviour
{
    string _ID_Donthuoc;
    string _Ten_Thuoc;
    [SerializeField] public GameObject _Input;
    [SerializeField] public TextMeshProUGUI thongbao;
    [SerializeField] public TMP_Dropdown _Tansuat;
    float _Soluong;
    float _Lieuchidinh;
    float _thoigian;
    float _dosemin;
    float _dosmax;
    private void Start()
    {
        _ID_Donthuoc = PlayerPrefs.GetString("_ID_Donthuoc");
        _Ten_Thuoc = PlayerPrefs.GetString("_TenThuoc");
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
    IEnumerator Connect()
    {
        // Tạo dữ liệu gửi đi
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("_ID_Donthuoc", _ID_Donthuoc));
        formData.Add(new MultipartFormDataSection("_Ten_Thuoc", _Ten_Thuoc));// Thay thế giá trị_ID_BenhNhan bằng giá trị thực tế bạn muốn gửi
        // Gửi yêu cầu POST đến file PHP
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/ViewDose.php", formData))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseData = www.downloadHandler.text;
                Debug.Log(responseData);
                ProcessData_Viewdose(responseData);
            }
            else
            {
                // Yêu cầu thất bại, hiển thị lỗi
                Debug.Log("Error: " + www.error);
            }
        }
    }
    IEnumerator Connect_update()
    {
        int currentIndex = _Tansuat.value;
        // Tạo dữ liệu gửi đi
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("_ID_Donthuoc", _ID_Donthuoc));
        formData.Add(new MultipartFormDataSection("_Ten_Thuoc", _Ten_Thuoc));
        formData.Add(new MultipartFormDataSection("_Soluong", _Input.transform.Find("LieuLuongInput").GetComponent<TMP_InputField>().text));
        formData.Add(new MultipartFormDataSection("_Lieuchidinh", _Input.transform.Find("LieuDungInput").GetComponent<TMP_InputField>().text));
        formData.Add(new MultipartFormDataSection("_thoigian", _Input.transform.Find("ngaydunginput").GetComponent<TMP_InputField>().text));
        formData.Add(new MultipartFormDataSection("_tansuat", _Tansuat.options[currentIndex].text));
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/Update_dose.php", formData))
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
            thongbao.text = "Cập nhật liều dùng thành công!";
        }
    }
    private void ProcessData_Viewdose(string jsonData)
    {
        // Giải mã dữ liệu JSON thành một danh sách các mảng đối tượng CTHoSoBenhNhan
        List<ViewDOSE> dataList = JsonConvert.DeserializeObject<List<ViewDOSE>>(jsonData);
        // In ra lượng dữ liệu đã nhận
        Debug.Log("Số lượng dữ liệu nhận được: " + dataList.Count);
        // Instantiate _Hosobenhnhan trong Content GameObject của ScrollView
        foreach (ViewDOSE item in dataList)
        {
            _Input.transform.Find("loaithuocinput").GetComponent<TextMeshProUGUI>().text = item._tenLoaiThuoc;
            _Input.transform.Find("TenThuocinput").GetComponent<TextMeshProUGUI>().text = item._tenThuoc;
            _Input.transform.Find("LieuLuongInput").GetComponent<TMP_InputField>().text = item._lieuluong;
            _Input.transform.Find("LieuDungInput").GetComponent<TMP_InputField>().text = item._Lieuchidinh;
            _Input.transform.Find("ngaydunginput").GetComponent<TMP_InputField>().text = item._thoigiansudung;
            _Input.transform.Find("lieumininput").GetComponent<TextMeshProUGUI>().text = item._lieuDungmin;
            _Input.transform.Find("lieumaxinput").GetComponent<TextMeshProUGUI>().text = item._lieuDungmax;

        }

    }
    public void CheckDose()
    {
        int currentIndex = _Tansuat.value;
        float.TryParse(_Input.transform.Find("LieuLuongInput").GetComponent<TMP_InputField>().text, out _Soluong);
        float.TryParse(_Input.transform.Find("LieuDungInput").GetComponent<TMP_InputField>().text, out _Lieuchidinh);
        float.TryParse(_Input.transform.Find("ngaydunginput").GetComponent<TMP_InputField>().text , out _thoigian);
        float.TryParse (_Input.transform.Find("lieumininput").GetComponent<TextMeshProUGUI>().text, out _dosemin);
        float.TryParse(_Input.transform.Find("lieumaxinput").GetComponent<TextMeshProUGUI>().text, out _dosmax);
        if (_Soluong * _Lieuchidinh > _dosmax || _Soluong * _Lieuchidinh < _dosemin)
        {
            thongbao.text = "Thông báo : Số lượng và liều chỉ định không hợp lý";
        }
        else
        {
            Debug.Log("upload");
            updata_lieuluong();
            SceneManager.LoadScene("MenuCTHSBN");
        }
    }
        public void updata_lieuluong()
    {
        StartCoroutine(Connect_update());
    }
}
public class ViewDOSE
{
    [JsonProperty("tenLoaiThuoc")]
    public string _tenLoaiThuoc;
    [JsonProperty("tenThuoc")]
    public string _tenThuoc;
    [JsonProperty("lieuluong")]
    public string _lieuluong;
    [JsonProperty("Lieuchidinh")]
    public string _Lieuchidinh;
    [JsonProperty("thoigiansudung")]
    public string _thoigiansudung;
    [JsonProperty("lieuDungmin")]
    public string _lieuDungmin;
    [JsonProperty("lieuDungmax")]
    public string _lieuDungmax;
}