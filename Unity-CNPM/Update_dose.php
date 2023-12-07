<?php
function update_dose() {
    if ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';
        $idDonThuoc = $_POST["_ID_Donthuoc"];
        $_Ten_Thuoc = $_POST["_Ten_Thuoc"]; 
        $_Soluong = $_POST["_Soluong"];
        $_Lieuchidinh = $_POST["_Lieuchidinh"];
        $_thoigian = $_POST["_thoigian"];
        $_tansuat = $_POST["_tansuat"];
        $query = "UPDATE chitietdonthuoc
        LEFT JOIN thuoc ON thuoc.idThuoc = chitietdonthuoc.idThuoc
        SET chitietdonthuoc.lieuluong = ?, chitietdonthuoc.Lieuchidinh = ?,
        chitietdonthuoc.thoigiansudung = ?, thuoc.tansuatdung = ?
        WHERE chitietdonthuoc.idDonThuoc = ? AND thuoc.tenThuoc = ?";
        $stmt = $conn->prepare($query);
        $stmt->bind_param("ssssss", $_Soluong, $_Lieuchidinh, $_thoigian, $_tansuat, $idDonThuoc, $_Ten_Thuoc);
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
update_dose();
?>