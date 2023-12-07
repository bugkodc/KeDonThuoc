<?php
function VIEW_dose() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $_ID_Donthuoc = $_POST["_ID_Donthuoc"];
        $_Ten_Thuoc = $_POST["_Ten_Thuoc"];
        $query = "SELECT loaithuoc.tenLoaiThuoc,thuoc.tenThuoc,chitietdonthuoc.lieuluong,chitietdonthuoc.Lieuchidinh,
        chitietdonthuoc.thoigiansudung,thuoc.lieuDungmin,thuoc.lieuDungmax
        FROM chitietdonthuoc
        LEFT JOIN thuoc ON thuoc.idThuoc = chitietdonthuoc.idThuoc
        LEFT JOIN loaithuoc ON thuoc.idLoaiThuoc = loaithuoc.idLoaiThuoc
        WHERE chitietdonthuoc.idDonThuoc = ? AND thuoc.tenThuoc = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("ss", $_ID_Donthuoc, $_Ten_Thuoc);
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
VIEW_dose();
?>