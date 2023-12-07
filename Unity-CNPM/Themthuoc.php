<?php
function them_dose() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $idDonThuoc = $_POST["_ID_Donthuoc"];
        $ID_BenhNhan = $_POST["ID_BenhNhan"];
        $Bacsi = "2";
        $ngayXuatDon = date('Y-m-d');
        $query = "INSERT INTO donthuoc (idDonThuoc, idHoSoBenhNhan, idHoSoBacsi , ngayKeDon)
        VALUES (?, ?, ?, ?)";
        
        $stmt = $conn->prepare($query);
        $stmt->bind_param("ssss", $idDonThuoc, $ID_BenhNhan, $Bacsi, $ngayXuatDon);
        $stmt->execute();
        
        $data = array('message' => 'Data updated successfully');
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

them_dose();
?>