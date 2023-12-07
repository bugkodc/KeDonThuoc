<?php
function HoSoBenhNhan() {
    if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    require 'Connect_Php.php';
    $query = "SELECT DISTINCT hosobenhnhan.idHoSoBenhNhan, hosobenhnhan.tenBenhNhan, hosobenhnhan.benhLy, hosobenhnhan.NgayKham , donthuoc.idHoSoBenhNhan as _ID_DT
    FROM hosobenhnhan
    LEFT JOIN donthuoc ON hosobenhnhan.idHoSoBenhNhan = donthuoc.idHoSoBenhNhan";
    $result = mysqli_query($conn, $query); 
    $data = array();
    while ($row = mysqli_fetch_assoc($result)) {
    $data[] = $row;
    }
    echo json_encode(array($data));
    }
   elseif ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $receivedData = $_POST['data'];

    // Xử lý dữ liệu nhận được ở đây

    // Ví dụ: Gửi phản hồi JSON
    $response = array('message' => 'Data received successfully');
    echo json_encode($response);
    } 
}
HoSoBenhNhan();
?>