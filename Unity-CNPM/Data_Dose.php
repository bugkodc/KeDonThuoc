<?php
function DATA_DOSE() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $ID_BenhNhan = $_POST["tenThuoc"];
        $query = "SELECT thuoc.idThuoc,thuoc.lieuDungmin, thuoc.lieuDungmax
        FROM thuoc
        WHERE thuoc.tenThuoc = ?";
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
DATA_DOSE();
?>