<?php
function DonThuoc() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $ID_BenhNhan = $_POST["ID_BenhNhan"];
        $ID_donthuoc = isset($_POST["_ID_DT"]) ? $_POST["_ID_DT"] : null;
        if($ID_donthuoc == null){
            $query = "SELECT hosobenhnhan.idHoSoBenhNhan, hosobenhnhan.tenBenhNhan,
         hosobenhnhan.tuoi,hosobenhnhan.gioiTinh, hosobenhnhan.benhLy,
        FROM hosobenhnhan
        WHERE hosobenhnhan.idHoSoBenhNhan = ?";
        }
        else{
        $query = "SELECT hosobenhnhan.idHoSoBenhNhan, hosobenhnhan.tenBenhNhan,
         hosobenhnhan.tuoi,hosobenhnhan.gioiTinh, hosobenhnhan.benhLy, thuoc.tenThuoc,
          chitietdonthuoc.lieuluong, thuoc.tansuatdung ,chitietdonthuoc.thoigiansudung 
        FROM hosobenhnhan
        LEFT JOIN donthuoc ON donthuoc.idHoSoBenhNhan = hosobenhnhan.idHoSoBenhNhan
        LEFT JOIN chitietdonthuoc ON chitietdonthuoc.idDonThuoc = donthuoc.idDonThuoc
        LEFT JOIN thuoc ON thuoc.idThuoc = chitietdonthuoc.idThuoc
        WHERE hosobenhnhan.idHoSoBenhNhan = ? AND donthuoc.idDonThuoc = ?";}
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
DonThuoc();
?>