-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th12 07, 2023 lúc 02:59 PM
-- Phiên bản máy phục vụ: 10.4.28-MariaDB
-- Phiên bản PHP: 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `donthuoc`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `chitietdonthuoc`
--

CREATE TABLE `chitietdonthuoc` (
  `idDonThuoc` int(11) NOT NULL,
  `idThuoc` int(11) NOT NULL,
  `thoigiansudung` int(11) NOT NULL,
  `lieuluong` varchar(50) NOT NULL,
  `Lieuchidinh` decimal(10,0) NOT NULL,
  `danDoBacSy` varchar(50) DEFAULT NULL,
  `ngayXuatDon` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `chitietdonthuoc`
--

INSERT INTO `chitietdonthuoc` (`idDonThuoc`, `idThuoc`, `thoigiansudung`, `lieuluong`, `Lieuchidinh`, `danDoBacSy`, `ngayXuatDon`) VALUES
(1, 1, 7, '4', 2, 'Uống sau bữa ăn', '2023-09-12'),
(1, 6, 7, '6', 2, NULL, '2023-12-07'),
(2, 2, 7, '6', 3, 'Uống sau bữa ăn', '2023-09-13'),
(3, 3, 7, '2', 3, 'Uống sau bữa ăn', '2023-09-14'),
(4, 4, 7, '2', 2, 'Uống sau bữa ăn', '2023-09-15'),
(7, 7, 7, '2', 2, 'Uống sau bữa ăn', '2023-09-18'),
(8, 8, 7, '2', 4, 'Uống sau bữa ăn', '2023-09-19'),
(9, 9, 7, '2', 4, 'Uống sau bữa ăn', '2023-09-20'),
(10, 1, 7, '2', 5, 'Uống sau bữa ăn', '2023-09-21'),
(11, 6, 7, '5', 2, NULL, '2023-12-07'),
(12, 7, 7, '5', 4, NULL, '2023-12-07'),
(14, 6, 7, '5', 3, NULL, '2023-12-07');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `donthuoc`
--

CREATE TABLE `donthuoc` (
  `idDonThuoc` int(11) NOT NULL,
  `idHoSoBenhNhan` int(11) NOT NULL,
  `idHoSoBacsi` int(11) NOT NULL,
  `ngayKeDon` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `donthuoc`
--

INSERT INTO `donthuoc` (`idDonThuoc`, `idHoSoBenhNhan`, `idHoSoBacsi`, `ngayKeDon`) VALUES
(1, 1, 1, '2023-09-12'),
(2, 2, 2, '2023-09-13'),
(3, 3, 3, '2023-09-14'),
(4, 4, 4, '2023-09-15'),
(7, 7, 3, '2023-09-18'),
(8, 8, 4, '2023-09-19'),
(9, 9, 1, '2023-09-20'),
(10, 10, 2, '2023-09-21'),
(11, 1, 2, '2023-12-07'),
(12, 2, 2, '2023-12-07'),
(13, 5, 2, '2023-12-07'),
(14, 1, 2, '2023-12-07');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `hosobacsi`
--

CREATE TABLE `hosobacsi` (
  `idHoSoBacsi` int(11) NOT NULL,
  `tenBacsi` varchar(50) NOT NULL,
  `tuoi` int(11) NOT NULL,
  `gioiTinh` varchar(10) NOT NULL,
  `diaChi` varchar(50) NOT NULL,
  `soDienThoai` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `hosobacsi`
--

INSERT INTO `hosobacsi` (`idHoSoBacsi`, `tenBacsi`, `tuoi`, `gioiTinh`, `diaChi`, `soDienThoai`) VALUES
(1, 'Bác sĩ A', 45, 'nam', 'hà nội', '0123456789'),
(2, 'Bác sĩ B', 46, 'nữ', 'đà nẵng', '0123456780'),
(3, 'Bác sĩ C', 47, 'nữ', 'hà nội', '0123456781'),
(4, 'Bác sĩ D', 48, 'nam', 'HCM', '0123456782');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `hosobenhnhan`
--

CREATE TABLE `hosobenhnhan` (
  `idHoSoBenhNhan` int(11) NOT NULL,
  `tenBenhNhan` varchar(50) NOT NULL,
  `tuoi` int(11) NOT NULL,
  `gioiTinh` varchar(10) NOT NULL,
  `diaChi` varchar(50) NOT NULL,
  `soDienThoai` varchar(20) NOT NULL,
  `benhLy` varchar(50) NOT NULL,
  `ngayKham` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `hosobenhnhan`
--

INSERT INTO `hosobenhnhan` (`idHoSoBenhNhan`, `tenBenhNhan`, `tuoi`, `gioiTinh`, `diaChi`, `soDienThoai`, `benhLy`, `ngayKham`) VALUES
(1, 'Nguyen Van A', 35, 'nam', 'ha noi', '0123456789', 'cam cum', '2023-09-12'),
(2, 'Nguyen Van B', 16, 'nu', 'đa nang', '0123456780', 'sot', '2023-09-13'),
(3, 'Nguyen Van C', 27, 'nam', 'ha noi', '0123456781', 'ho', '2023-09-14'),
(4, 'Nguyen Van D', 48, 'nu', 'quang nam', '0123456782', 'da day', '2023-09-15'),
(5, 'Nguyen Van E', 39, 'nam', 'hue', '0123456783', 'sot', '2023-09-16'),
(6, 'Nguyen Van F', 42, 'nu', 'ha noi', '0123456784', 'cam cum', '2023-09-17'),
(7, 'Nguyen Van G', 41, 'nam', 'quang ngai', '0123456785', 'da day', '2023-09-18'),
(8, 'Nguyen Van H', 44, 'nu', 'ha noi', '0123456786', 'Cam Cam', '2023-09-19'),
(9, 'Nguyen Van I', 17, 'nam', 'đa nang', '0123456787', 'Cam Cam', '2023-09-20'),
(10, 'Nguyen Van J', 20, 'nu', 'ha noi', '0123456788', 'Cam Cam', '2023-09-21');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `loaithuoc`
--

CREATE TABLE `loaithuoc` (
  `idLoaiThuoc` int(11) NOT NULL,
  `tenLoaiThuoc` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `loaithuoc`
--

INSERT INTO `loaithuoc` (`idLoaiThuoc`, `tenLoaiThuoc`) VALUES
(1, 'Thuoc khang sinh'),
(2, 'Thuoc giam dau'),
(3, 'Thuoc ha sot'),
(4, 'Thuoc chong di ung'),
(5, 'Thuoc cam'),
(6, 'Thuoc da day');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `thuoc`
--

CREATE TABLE `thuoc` (
  `idThuoc` int(11) NOT NULL,
  `idLoaiThuoc` int(11) NOT NULL,
  `tenThuoc` varchar(50) NOT NULL,
  `lieuDungmin` float NOT NULL,
  `lieuDungmax` float NOT NULL,
  `donVi` varchar(10) NOT NULL,
  `tansuatdung` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Đang đổ dữ liệu cho bảng `thuoc`
--

INSERT INTO `thuoc` (`idThuoc`, `idLoaiThuoc`, `tenThuoc`, `lieuDungmin`, `lieuDungmax`, `donVi`, `tansuatdung`) VALUES
(1, 1, 'Thuoc khang sinh A', 5, 20, 'viên', '1 viên/ lần'),
(2, 2, 'Thuoc giam dau B', 15, 30, 'viên', '1 viên/ lần'),
(3, 3, 'Thuoc ha sot C', 10, 20, 'viên', '1 viên/lần'),
(4, 4, 'Thuoc chong di ung D', 10, 20, 'viên', '1 viên/lần'),
(5, 1, 'Thuoc khang sinh E', 10, 20, 'viên', '1 viên/lần'),
(6, 2, 'Thuoc giam dau F', 10, 20, 'viên', '1 viên/ lần'),
(7, 3, 'Thuoc ha sot G', 15, 30, 'viên', '1 viên/lần'),
(8, 4, 'Thuoc chong di ung H', 5, 20, 'viên', '1 viên/ lần'),
(9, 1, 'Thuoc khang sinh I', 4, 10, 'viên', '1 viên/lần');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `chitietdonthuoc`
--
ALTER TABLE `chitietdonthuoc`
  ADD PRIMARY KEY (`idDonThuoc`,`idThuoc`),
  ADD KEY `FK_chiTietDonThuoc_thuoc` (`idThuoc`);

--
-- Chỉ mục cho bảng `donthuoc`
--
ALTER TABLE `donthuoc`
  ADD PRIMARY KEY (`idDonThuoc`),
  ADD KEY `FK_BN` (`idHoSoBenhNhan`),
  ADD KEY `FK_BS` (`idHoSoBacsi`);

--
-- Chỉ mục cho bảng `hosobacsi`
--
ALTER TABLE `hosobacsi`
  ADD PRIMARY KEY (`idHoSoBacsi`);

--
-- Chỉ mục cho bảng `hosobenhnhan`
--
ALTER TABLE `hosobenhnhan`
  ADD PRIMARY KEY (`idHoSoBenhNhan`);

--
-- Chỉ mục cho bảng `loaithuoc`
--
ALTER TABLE `loaithuoc`
  ADD PRIMARY KEY (`idLoaiThuoc`);

--
-- Chỉ mục cho bảng `thuoc`
--
ALTER TABLE `thuoc`
  ADD PRIMARY KEY (`idThuoc`),
  ADD KEY `FK_Thuoc` (`idLoaiThuoc`);

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `chitietdonthuoc`
--
ALTER TABLE `chitietdonthuoc`
  ADD CONSTRAINT `FK_chiTietDonThuoc_donThuoc` FOREIGN KEY (`idDonThuoc`) REFERENCES `donthuoc` (`idDonThuoc`),
  ADD CONSTRAINT `FK_chiTietDonThuoc_thuoc` FOREIGN KEY (`idThuoc`) REFERENCES `thuoc` (`idThuoc`);

--
-- Các ràng buộc cho bảng `donthuoc`
--
ALTER TABLE `donthuoc`
  ADD CONSTRAINT `FK_BN` FOREIGN KEY (`idHoSoBenhNhan`) REFERENCES `hosobenhnhan` (`idHoSoBenhNhan`),
  ADD CONSTRAINT `FK_BS` FOREIGN KEY (`idHoSoBacsi`) REFERENCES `hosobacsi` (`idHoSoBacsi`);

--
-- Các ràng buộc cho bảng `thuoc`
--
ALTER TABLE `thuoc`
  ADD CONSTRAINT `FK_Thuoc` FOREIGN KEY (`idLoaiThuoc`) REFERENCES `loaithuoc` (`idLoaiThuoc`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
