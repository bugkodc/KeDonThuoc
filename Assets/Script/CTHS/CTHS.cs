using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class CTHS : MonoBehaviour
{
    public string _ID_BN;
    public string _ID_DT;
    [SerializeField] public GameObject _Input;
    [SerializeField] public GameObject _Thuoc;
    [SerializeField] private GameObject _scrollViewContent;
    [SerializeField] private Button _NewThuoc;
    private void Start()
    {
        _ID_BN = PlayerPrefs.GetString("ID_BenhNhan");
        _ID_DT = PlayerPrefs.GetString("_ID_Donthuoc");
        CallConnect();
        _NewThuoc.onClick.AddListener(Newthuoc);
    }
    public void Newthuoc()
    {
        SceneManager.LoadScene("Newthuoc");
    }
    public void IN()
    {
        SceneManager.LoadScene("OUTDT");
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
        formData.Add(new MultipartFormDataSection("ID_BenhNhan", _ID_BN));
        formData.Add(new MultipartFormDataSection("_ID_DT", _ID_DT));// Thay thế giá trị_ID_BenhNhan bằng giá trị thực tế bạn muốn gửi
        // Gửi yêu cầu POST đến file PHP
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/CTHS.php", formData))
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
    IEnumerator Connect_Thuoc()
    {
        // Tạo dữ liệu gửi đi
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("_ID_DT", _ID_DT));// Thay thế giá trị_ID_BenhNhan bằng giá trị thực tế bạn muốn gửi
        // Gửi yêu cầu POST đến file PHP
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/Showthuoc.php", formData))
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
    private void ProcessData_HoSoBenhNhan(string jsonData)
    {
        // Giải mã dữ liệu JSON thành một danh sách các mảng đối tượng CTHoSoBenhNhan
        List<CTHoSoBenhNhan> dataList = JsonConvert.DeserializeObject<List<CTHoSoBenhNhan>>(jsonData);
        // In ra lượng dữ liệu đã nhận
        Debug.Log("Số lượng dữ liệu nhận được: " + dataList.Count);
        // Instantiate _Hosobenhnhan trong Content GameObject của ScrollView
        foreach (CTHoSoBenhNhan item in dataList)
        {
            _Input.transform.Find("ID_BNinput").GetComponent<TextMeshProUGUI>().text = item._ID_CT_BenhNhan;
            _Input.transform.Find("MDTinput").GetComponent<TextMeshProUGUI>().text = _ID_DT;
            _Input.transform.Find("hoteninput").GetComponent<TextMeshProUGUI>().text = item._HoTen_CT_BN;
            _Input.transform.Find("ageInput").GetComponent<TextMeshProUGUI>().text = item._tuoi;
            _Input.transform.Find("Sexinput").GetComponent<TextMeshProUGUI>().text = item._gioi;
            _Input.transform.Find("CDinput").GetComponent<TextMeshProUGUI>().text = item._benhly;
        }
        ResetScrollViewContent();
        StartCoroutine(Connect_Thuoc());

    }
    private void ProcessData_thuoc(string jsonData)
    {
        List<Thuoc> thuocList = JsonConvert.DeserializeObject<List<Thuoc>>(jsonData);

        // Lấy danh sách đối tượng CTHoSoBenhNhan từ danh sách mảng

        foreach (Thuoc item in thuocList)
        { 
                _Thuoc.transform.Find("ID_thuoc_input").GetComponent<TextMeshProUGUI>().text = item._idThuoc;
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
[System.Serializable]
public class CTHoSoBenhNhan
{
    [JsonProperty("idHoSoBenhNhan")]
    public string _ID_CT_BenhNhan;
    [JsonProperty("tenBenhNhan")]
    public string _HoTen_CT_BN;
    [JsonProperty("tuoi")]
    public string _tuoi;
    [JsonProperty("gioiTinh")]
    public string _gioi;
    [JsonProperty("benhLy")]
    public string _benhly; 
}

public class Thuoc
{
    [JsonProperty("idThuoc")]
    public string _idThuoc;
    [JsonProperty("tenThuoc")]
    public string _Ten_Thuoc;
    [JsonProperty("lieuluong")]
    public string _Lieu_Dung;
    [JsonProperty("tansuatdung")]
    public string _Tansuatdung;
    [JsonProperty("thoigiansudung")]
    public string _TimeDung;
}
