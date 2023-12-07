<?php
function ADDNEW() {
    if ($_SERVER['REQUEST_METHOD'] === 'GET') {
        require 'Connect_Php.php';
        $query = "SELECT MAX(idDonThuoc) as sumDT
        FROM donthuoc";
        $result = mysqli_query($conn, $query); 
        $data = array();
        while ($row = mysqli_fetch_assoc($result)) {
            $data[] = $row;
        }
        $sumDT = $data[0]['sumDT'] + 1;
        echo $sumDT;
    }
    elseif ($_SERVER['REQUEST_METHOD'] === 'POST') {
        require 'Connect_Php.php';

        // Lấy dữ liệu từ POST request
        $receivedData = $_POST['data'];

        // Xử lý dữ liệu nhận được ở đây

        // Ví dụ: Gửi phản hồi JSON
        $response = array('message' => 'Data received successfully');
        echo json_encode($response);
    } 
}
ADDNEW();
?>