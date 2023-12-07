<?php
function xoa_thuoc() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $idDonThuoc = $_POST["idDonThuoc"];
        $_ID_thuoc = $_POST["_ID_thuoc"];
        
        // Câu truy vấn DELETE FROM
        $query = "DELETE FROM chitietdonthuoc 
        WHERE chitietdonthuoc.idDonThuoc = ? AND chitietdonthuoc.idThuoc = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("ss", $idDonThuoc, $_ID_thuoc);
        $stmt->execute();
        
        $data = array('message' => 'Data deleted successfully');
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

xoa_thuoc();
?>