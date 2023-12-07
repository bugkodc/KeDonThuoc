using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
public class HS : MonoBehaviour
{
    [SerializeField] private GameObject _Hosobenhnhan;
    [SerializeField] private GameObject _scrollViewContent;
    private void Start()
    {
        PlayerPrefs.SetString("_ID_Donthuoc", null) ;
        PlayerPrefs.SetString("_ID_thuoc", null);
        PlayerPrefs.SetString("selectedOption_thuoc", null);
        PlayerPrefs.SetString("ID_BenhNhan", null);
        PlayerPrefs.SetString("_TenThuoc", null);
        CallConnect();
    }
    public void CallConnect()
    {
        StartCoroutine(Connect());
    }
    IEnumerator Connect()
    {
        string url = "http://localhost/Unity-CNPM/HoSoBenhNhan.php";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseData = www.downloadHandler.text;
            Debug.Log(responseData);
            // Xử lý dữ liệu JSON được nhận về
            ProcessData_HoSoBenhNhan(responseData);
        }
        else
        {
            Debug.Log("Lỗi kết nối: " + www.error);
        }
    }
    private void ProcessData_HoSoBenhNhan(string jsonData)
    {
        // Giải mã dữ liệu JSON thành một danh sách các mảng đối tượng CTHoSoBenhNhan
        List<List<HoSoBenhNhan>> dataList = JsonConvert.DeserializeObject<List<List<HoSoBenhNhan>>>(jsonData);

        // Lấy danh sách đối tượng CTHoSoBenhNhan từ danh sách mảng
        List<HoSoBenhNhan> hoSoBenhNhanList = dataList[0];

        // In ra lượng dữ liệu đã nhận
        Debug.Log("Số lượng dữ liệu nhận được: " + hoSoBenhNhanList.Count);

        // Instantiate _Hosobenhnhan trong Content GameObject của ScrollView
        foreach (HoSoBenhNhan item in hoSoBenhNhanList)
        {
            // Tìm các GameObject con trong hosobenhnhan bằng tên và gán giá trị vào TextMeshProUGUI
            _Hosobenhnhan.transform.Find("_ID_BenhNhan").GetComponent<TextMeshProUGUI>().text = item._ID_BenhNhan;
            _Hosobenhnhan.transform.Find("HoTenBN").GetComponent<TextMeshProUGUI>().text = item._HoTen_BN;
            _Hosobenhnhan.transform.Find("BHYT").GetComponent<TextMeshProUGUI>().text = item._BenhLy;
            _Hosobenhnhan.transform.Find("NgayKham").GetComponent<TextMeshProUGUI>().text = item._NgayKham;
            if (item._DonThuoc != null) _Hosobenhnhan.transform.Find("DonThuoc").GetComponent<TextMeshProUGUI>().text = "Đã có";
            else _Hosobenhnhan.transform.Find("DonThuoc").GetComponent<TextMeshProUGUI>().text = "Chưa có";
            Instantiate(_Hosobenhnhan, _scrollViewContent.transform);
            NewBehaviourScript.instance.SetIDBenhNhan(_Hosobenhnhan.transform.Find("_ID_BenhNhan").GetComponent<TextMeshProUGUI>().text);
        }
    }
    public void HSBN()
    {
        SceneManager.LoadScene("MenuHSBN");
    }
}

[System.Serializable]
public class HoSoBenhNhan
{
    [JsonProperty("idHoSoBenhNhan")]
    public string _ID_BenhNhan;
    [JsonProperty("tenBenhNhan")]
    public string _HoTen_BN;
    [JsonProperty("benhLy")]
    public string _BenhLy;
    [JsonProperty("NgayKham")]
    public string _NgayKham;
    [JsonProperty("_ID_DT")]
    public string _DonThuoc;
}