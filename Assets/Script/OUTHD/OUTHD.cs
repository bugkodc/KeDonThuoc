using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public class OUTHD : MonoBehaviour
{
    public string _ID_BN;
    public string _ID_DT;
    [SerializeField] public GameObject _Input;
    [SerializeField] private GameObject _scrollViewContent;
    [SerializeField] public GameObject _Thuoc;
    // Start is called before the first frame update
    void Start()
    {
        _ID_BN = PlayerPrefs.GetString("ID_BenhNhan");
        _ID_DT = PlayerPrefs.GetString("_ID_Donthuoc");
        _Input.transform.Find("MDT_input").GetComponent<TextMeshProUGUI>().text = _ID_DT;
        _Input.transform.Find("dando").GetComponent<TextMeshProUGUI>().text = "Uống thuốc sau khi ăn";
        CallConnect();
    }
    public void CallConnect()
    {
        StartCoroutine(Connect());
    }
    public void HSBN()
    {
        SceneManager.LoadScene("MenuHSBN");
    }
    IEnumerator Connect()
    {
        // Tạo dữ liệu gửi đi
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("ID_BenhNhan", _ID_BN));
        formData.Add(new MultipartFormDataSection("_ID_DT", _ID_DT));// Thay thế giá trị_ID_BenhNhan bằng giá trị thực tế bạn muốn gửi
        // Gửi yêu cầu POST đến file PHP
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/OUTBN.php", formData))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseData = www.downloadHandler.text;
                Debug.Log(responseData);
                ProcessData_HoSoBenhNhan(responseData);
            }
            else
            {
                // Yêu cầu thất bại, hiển thị lỗi
                Debug.Log("Error: " + www.error);
            }
        }
    }
    private void ProcessData_HoSoBenhNhan(string jsonData)
    {
        // Giải mã dữ liệu JSON thành một danh sách các mảng đối tượng CTHoSoBenhNhan
        List<showbn> dataList = JsonConvert.DeserializeObject<List<showbn>>(jsonData);
        // In ra lượng dữ liệu đã nhận
        Debug.Log("Số lượng dữ liệu nhận được: " + dataList.Count);
        // Instantiate _Hosobenhnhan trong Content GameObject của ScrollView
        foreach (showbn item in dataList)
        {
            _Input.transform.Find("tenbn_input").GetComponent<TextMeshProUGUI>().text = item._tenBenhNhan;
            _Input.transform.Find("tuoi").GetComponent<TextMeshProUGUI>().text = item._tuoi;
            _Input.transform.Find("gioi").GetComponent<TextMeshProUGUI>().text = item._gioiTinh;
            _Input.transform.Find("diachi").GetComponent<TextMeshProUGUI>().text = item._diaChi;
            _Input.transform.Find("sdt").GetComponent<TextMeshProUGUI>().text = item._soDienThoai;
            _Input.transform.Find("benhly").GetComponent<TextMeshProUGUI>().text = item._benhly;
            _Input.transform.Find("bacsi").GetComponent<TextMeshProUGUI>().text = item._tenBacsi;
        }
        ResetScrollViewContent();
        StartCoroutine(Connect_Thuoc());

    }
    IEnumerator Connect_Thuoc()
    {
        // Tạo dữ liệu gửi đi
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("_ID_DT", _ID_DT));// Thay thế giá trị_ID_BenhNhan bằng giá trị thực tế bạn muốn gửi
        // Gửi yêu cầu POST đến file PHP
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/OUTThuoc.php", formData))
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
    private void ProcessData_thuoc(string jsonData)
    {
        List<showThuoc> thuocList = JsonConvert.DeserializeObject<List<showThuoc>>(jsonData);

        // Lấy danh sách đối tượng CTHoSoBenhNhan từ danh sách mảng

        foreach (showThuoc item in thuocList)
        {
            _Thuoc.transform.Find("TenThuocInput").GetComponent<TextMeshProUGUI>().text = item._Ten_Thuoc;
            _Thuoc.transform.Find("soluong").GetComponent<TextMeshProUGUI>().text = item._Lieu_Dung;
            _Thuoc.transform.Find("Tasuat").GetComponent<TextMeshProUGUI>().text = item._Tansuatdung;
            _Thuoc.transform.Find("day").GetComponent<TextMeshProUGUI>().text = item._TimeDung;
            Instantiate(_Thuoc, _scrollViewContent.transform);
        }

    }
    private void ResetScrollViewContent()
    {
        Transform content = _scrollViewContent.transform;
        foreach (Transform child in content)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
public class showbn
{
    [JsonProperty("tenBenhNhan")]
    public string _tenBenhNhan;
    [JsonProperty("tuoi")]
    public string _tuoi;
    [JsonProperty("gioiTinh")]
    public string _gioiTinh;
    [JsonProperty("diaChi")]
    public string _diaChi;
    [JsonProperty("soDienThoai")]
    public string _soDienThoai;
    [JsonProperty("benhLy")]
    public string _benhly;
    [JsonProperty("tenBacsi")]
    public string _tenBacsi;
}
public class showThuoc
{
    [JsonProperty("tenThuoc")]
    public string _Ten_Thuoc;
    [JsonProperty("lieuluong")]
    public string _Lieu_Dung;
    [JsonProperty("tansuatdung")]
    public string _Tansuatdung;
    [JsonProperty("thoigiansudung")]
    public string _TimeDung;
}
