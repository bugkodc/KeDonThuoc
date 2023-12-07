using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class D : MonoBehaviour
{
    public string _ID_BN;
    [SerializeField] public GameObject _DT_BN;
    [SerializeField] private GameObject _scrollViewContent;
    [SerializeField] public GameObject _DT_ADD;
    void Start()
    {
        _ID_BN = PlayerPrefs.GetString("ID_BenhNhan");
        CallConnect();
    }
    public void CallConnect()
    {
        StartCoroutine(Connect());
    }
    IEnumerator Connect()
    {
        // Tạo dữ liệu gửi đi
        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
        formData.Add(new MultipartFormDataSection("idHoSoBenhNhan", _ID_BN)); // Thay thế giá trị_ID_BenhNhan bằng giá trị thực tế bạn muốn gửi
        // Gửi yêu cầu POST đến file PHP
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity-CNPM/DT_HSBN.php", formData))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseData = www.downloadHandler.text;
               
          
                    ProcessData_DT_BN(responseData);
                
            }
            else
            {
                // Yêu cầu thất bại, hiển thị lỗi
                Debug.Log("Error: " + www.error);
            }
        }
    }
    private void ProcessData_DT_BN(string jsonData)
    {
        // Giải mã dữ liệu JSON thành một danh sách các mảng đối tượng CTHoSoBenhNhan
        List<DTBN> dataList = JsonConvert.DeserializeObject<List<DTBN>>(jsonData);
        // In ra lượng dữ liệu đã nhận
        // Instantiate _Hosobenhnhan trong Content GameObject của ScrollView
        foreach (DTBN item in dataList)
        { 
         _DT_BN.transform.Find("Text(TMP)").GetComponent<TextMeshProUGUI>().text = item._idDonThuoc;
            if (_DT_BN.transform.Find("Text(TMP)").GetComponent<TextMeshProUGUI>().text == null)
            {
            }
            else
            {
                Instantiate(_DT_BN, _scrollViewContent.transform);
            }
        }
        Instantiate(_DT_ADD, _scrollViewContent.transform);

    }
    public void HSBN()
    {
        SceneManager.LoadScene("MenuHSBN");
    }
}
[System.Serializable]
public class DTBN
{
    [JsonProperty("idDonThuoc")]
    public string _idDonThuoc;
}