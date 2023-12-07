<?php
function Drop_Thuoc() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $ID_BenhNhan = $_POST["tenLoaiThuoc"];
        $query = "SELECT thuoc.tenThuoc 
        FROM loaithuoc
        LEFT JOIN thuoc ON thuoc.idLoaiThuoc = loaithuoc.idLoaiThuoc
        WHERE loaithuoc.tenLoaiThuoc = ?";
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
Drop_Thuoc();
?>