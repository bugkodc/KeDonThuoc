<?php
function Dataset() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $ID_BenhNhan = $_POST["HoTen_BN"];
        $query = "SELECT donthuoc._ID_DonThuoc, hosobenhnhan.CanNang, hosobenhnhan.ChieuCao, hosobenhnhan.ChuanDoan, thuoc.TenThuoc, thuoc.LieuDung, thuoc.Donvi 
        FROM hosobenhnhan
        LEFT JOIN donthuoc ON donthuoc._ID_BenhNhan = hosobenhnhan._ID_BenhNhan
        LEFT JOIN chitietdonthuoc ON chitietdonthuoc._ID_DonThuoc = donthuoc._ID_DonThuoc
        LEFT JOIN thuoc ON thuoc._ID_Thuoc = chitietdonthuoc._ID_Thuoc
        WHERE hosobenhnhan.HoTen_BN = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("s", $ID_BenhNhan);
        $stmt->execute();
        $result = $stmt->get_result();
        $data = array();
        while ($row = $result->fetch_assoc()) {
            $data[] = $row;
        }
        echo json_encode($data);
    }
    elseif ($_SERVER['REQUEST_METHOD'] === 'GET') {
        $receivedData = $_GET['data'];
        // Xử lý dữ liệu nhận được ở đây
        // Ví dụ: Gửi phản hồi JSON
        $response = array('message' => 'Data received successfully');
        echo json_encode($response);
    }
}
Dataset();
?>