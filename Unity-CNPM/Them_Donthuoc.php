<?php
function them_donthuoc() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $idDonThuoc = $_POST["_ID_Donthuoc"];
        $_ID_thuoc = $_POST["_ID_thuoc"];
        $lieuluong = $_POST["_Soluong"];
        $_Lieuchidinh = $_POST["_Lieuchidinh"];
        $thoigiansudung = $_POST["_thoigian"];
        $_tansuat = $_POST["_tansuat"];
        $ngayXuatDon = date('Y-m-d');
        $query = "INSERT INTO chitietdonthuoc (idDonThuoc, idThuoc, thoigiansudung, lieuluong, Lieuchidinh, ngayXuatDon)
        VALUES (?, ?, ?, ?, ?, ?)";
        
        $stmt = $conn->prepare($query);
        $stmt->bind_param("ssssss", $idDonThuoc, $_ID_thuoc, $thoigiansudung, $lieuluong, $_Lieuchidinh, $ngayXuatDon);
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

them_donthuoc();
?>