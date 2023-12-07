<?php
function view() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $ID_donthuoc = $_POST["_ID_DT"];
        $query = "SELECT thuoc.idThuoc,thuoc.tenThuoc,chitietdonthuoc.lieuluong, thuoc.tansuatdung ,chitietdonthuoc.thoigiansudung 
        FROM donthuoc
        JOIN chitietdonthuoc ON chitietdonthuoc.idDonThuoc = donthuoc.idDonThuoc
        JOIN thuoc ON thuoc.idThuoc = chitietdonthuoc.idThuoc
        WHERE  donthuoc.idDonThuoc = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("s", $ID_donthuoc);
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
view();
?>