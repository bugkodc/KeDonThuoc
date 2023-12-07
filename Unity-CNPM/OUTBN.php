<?php
function OUTDT() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $ID_BenhNhan = $_POST["ID_BenhNhan"];
        $ID_donthuoc = $_POST["_ID_DT"];
        $query = "SELECT hosobenhnhan.tenBenhNhan,
         hosobenhnhan.tuoi,hosobenhnhan.gioiTinh,hosobenhnhan.diaChi,hosobenhnhan.soDienThoai,
         hosobenhnhan.benhLy ,hosobacsi.tenBacsi
        FROM hosobenhnhan
        JOIN donthuoc ON donthuoc.idHoSoBenhNhan = hosobenhnhan.idHoSoBenhNhan
        JOIN hosobacsi ON donthuoc.idHoSoBacsi  = hosobacsi.idHoSoBacsi
        WHERE hosobenhnhan.idHoSoBenhNhan = ? AND donthuoc.idDonThuoc = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("ss", $ID_BenhNhan,$ID_donthuoc);
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
OUTDT();
?>