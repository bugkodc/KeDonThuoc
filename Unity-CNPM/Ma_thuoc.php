<?php
function view() {
    require 'Connect_Php.php';

    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        $tenthuoc = $_POST["tenthuoc"];
        $query = "SELECT thuoc.idThuoc
        FROM thuoc
        WHERE thuoc.tenThuoc = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("s", $tenthuoc);
        $stmt->execute();
        $result = $stmt->get_result();
        $data = array();
        while ($row = $result->fetch_assoc()) {
            $data[] = $row;
        }
        echo json_encode($data);
    } elseif ($_SERVER['REQUEST_METHOD'] === 'GET') {
        $receivedData = $_GET['data'];
        // Xử lý dữ liệu nhận được ở đây
        // Ví dụ: Gửi phản hồi JSON
        $response = array('message' => 'Data received successfully');
        echo json_encode($response);
    }
}

view();
?>