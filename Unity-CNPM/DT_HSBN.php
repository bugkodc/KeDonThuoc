<?php
function DT_HSBN() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $ID_BenhNhan = $_POST["idHoSoBenhNhan"];
        $query = "SELECT donthuoc.idDonThuoc 
        FROM hosobenhnhan
        LEFT JOIN donthuoc ON hosobenhnhan.idHoSoBenhNhan = donthuoc.idHoSoBenhNhan
        WHERE hosobenhnhan.idHoSoBenhNhan = ?";
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
DT_HSBN();
?>