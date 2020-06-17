-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Waktu pembuatan: 29 Apr 2020 pada 13.38
-- Versi server: 10.4.6-MariaDB
-- Versi PHP: 7.3.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_hitgaji`
--

-- --------------------------------------------------------

--
-- Struktur dari tabel `tbulan`
--

CREATE TABLE `tbulan` (
  `id` varchar(11) NOT NULL,
  `nama_bulan` varchar(35) NOT NULL,
  `Total_masuk` varchar(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tbulan`
--

INSERT INTO `tbulan` (`id`, `nama_bulan`, `Total_masuk`) VALUES
('1', 'Januari', '19'),
('10', 'Oktober', ''),
('2', 'Februari', '19'),
('3', 'Maret', '15'),
('4', 'April', '19'),
('5', 'Mei', '21'),
('6', 'Juni', ''),
('7', 'Juli', ''),
('8', 'Agustus', ''),
('9', 'September', '');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tgolongan`
--

CREATE TABLE `tgolongan` (
  `Golongan` int(11) NOT NULL,
  `Tunjangan_keluarga` varchar(25) NOT NULL,
  `Tunjangan_asuransi` varchar(25) NOT NULL,
  `Tunjangan_transport` varchar(25) NOT NULL,
  `Pot_simpanan_wajib` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tgolongan`
--

INSERT INTO `tgolongan` (`Golongan`, `Tunjangan_keluarga`, `Tunjangan_asuransi`, `Tunjangan_transport`, `Pot_simpanan_wajib`) VALUES
(1, '1000000', '500000', '50000', '50000'),
(2, '1500000', '750000', '50000', '50000'),
(3, '2000000', '1000000', '50000', '50000'),
(4, '2500000', '1250000', '50000', '50000');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tjabatan`
--

CREATE TABLE `tjabatan` (
  `Kode_jabatan` varchar(10) NOT NULL,
  `Nama_jabatan` varchar(50) NOT NULL,
  `guru` varchar(25) NOT NULL,
  `walikelas` varchar(25) NOT NULL DEFAULT '0',
  `Gaji_pokok` varchar(30) NOT NULL,
  `Gaji_jam_ngajar` varchar(25) NOT NULL,
  `Tunjangan_jabatan` varchar(25) NOT NULL,
  `Tunjangan_guru` varchar(25) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tjabatan`
--

INSERT INTO `tjabatan` (`Kode_jabatan`, `Nama_jabatan`, `guru`, `walikelas`, `Gaji_pokok`, `Gaji_jam_ngajar`, `Tunjangan_jabatan`, `Tunjangan_guru`) VALUES
('ADM', 'Administrasi', '0', '', '9000000', '20000', '7500000', '0'),
('GRU', 'Guru', '1', '0', '4500000', '20000', '500000', '750000'),
('MKT', 'Marketing', '0', '', '10000000', '20000', '5000000', '0'),
('MNG', 'Manager', '0', '', '25000000', '20000', '12500000', '0'),
('SPV', 'Supervisor', '0', '', '20000000', '20000', '10000000', '0'),
('STF', 'Staff', '0', '', '7500000', '20000', '5000000', '0');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tkaryawan`
--

CREATE TABLE `tkaryawan` (
  `NPK` varchar(15) NOT NULL,
  `Nama` varchar(30) NOT NULL,
  `Alamat` text NOT NULL,
  `No_telp` varchar(20) NOT NULL,
  `Jenis_kelamin` varchar(15) NOT NULL,
  `Status` varchar(20) NOT NULL,
  `Jumlah_anak` varchar(10) NOT NULL,
  `Email` varchar(30) NOT NULL,
  `Kode_golongan` varchar(10) NOT NULL,
  `Kode_jabatan` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tkaryawan`
--

INSERT INTO `tkaryawan` (`NPK`, `Nama`, `Alamat`, `No_telp`, `Jenis_kelamin`, `Status`, `Jumlah_anak`, `Email`, `Kode_golongan`, `Kode_jabatan`) VALUES
('0110000', 'Rendi Putra Pradana', 'Tambun, Elok 2', '088861681234', 'Laki-Laki', 'Lajang', '0', 'rendiputrapradana@gmail.com', '4', 'GRU'),
('0112040', 'Yogga Fadhillah', 'Bekasi, Tambun', '085562142', 'Laki-Laki', 'Menikah', '1', 'yoggafdlh@gmail.com', '1', 'MNG'),
('0113101', 'Paijo', 'Jonggol', '081345410515', 'Laki-Laki', 'Menikah', '5', 'paijo@gmail.com', '2', 'ADM'),
('02321300', 'Sukirman', 'Jonggol, Bogor', '0883451014', 'Laki-Laki', 'Lajang', '0', 'kirman@gmail.com', '1', 'ADM'),
('1213123', 'Abi ', 'Cibubur', '088881221', 'Laki-Laki', 'Lajang', '0', 'Abi@a.com', '1', 'ADM'),
('123', 'rendi', 'test', '0888', 'Laki-Laki', 'Lajang', '1', 'wqe@jnn.com', '1', 'MKT');

-- --------------------------------------------------------

--
-- Struktur dari tabel `tlogin`
--

CREATE TABLE `tlogin` (
  `id_user` int(11) NOT NULL,
  `username` varchar(20) NOT NULL,
  `password` varchar(20) NOT NULL,
  `gambar` varchar(1000) NOT NULL,
  `level` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tlogin`
--

INSERT INTO `tlogin` (`id_user`, `username`, `password`, `gambar`, `level`) VALUES
(1, 'rendi', 'rendi', 'D:\\GALERI\\Dokumen 9.8\\DSC_0344.JPG', 1),
(2, 'putra', 'putra', 'D:\\GALERI\\Dokumen 9.8\\DSC_0344.JPG', 2),
(3, 'jawa', 'jawa', 'D:GALERIDokumen 9.8DSC_0344.JPG', 2);

-- --------------------------------------------------------

--
-- Struktur dari tabel `tpenggajian`
--

CREATE TABLE `tpenggajian` (
  `id_gaji` varchar(20) NOT NULL,
  `NPK` varchar(11) NOT NULL,
  `tanggal` varchar(20) NOT NULL,
  `periode` varchar(255) DEFAULT NULL,
  `nama` varchar(50) NOT NULL,
  `jabatan` varchar(20) NOT NULL,
  `gaji_pokok` varchar(12) NOT NULL,
  `Gaji_jam` varchar(25) NOT NULL,
  `Tunjangan_transport` varchar(25) NOT NULL,
  `Tunjangan_jabatan` varchar(12) NOT NULL,
  `Tunjangan_guru` varchar(25) NOT NULL,
  `tunjangan_keluarga` varchar(122) NOT NULL,
  `Tunjangan_asuransi` varchar(25) NOT NULL,
  `pot_PPN` varchar(25) NOT NULL,
  `pot_dansos` varchar(25) NOT NULL,
  `pot_infaq` varchar(25) NOT NULL,
  `pot_simpanan_wajib` varchar(25) NOT NULL,
  `pendapatan` varchar(50) NOT NULL,
  `Potongan` varchar(50) NOT NULL,
  `gaji_bersih` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data untuk tabel `tpenggajian`
--

INSERT INTO `tpenggajian` (`id_gaji`, `NPK`, `tanggal`, `periode`, `nama`, `jabatan`, `gaji_pokok`, `Gaji_jam`, `Tunjangan_transport`, `Tunjangan_jabatan`, `Tunjangan_guru`, `tunjangan_keluarga`, `Tunjangan_asuransi`, `pot_PPN`, `pot_dansos`, `pot_infaq`, `pot_simpanan_wajib`, `pendapatan`, `Potongan`, `gaji_bersih`) VALUES
('0000000002', '0110000', '18 November 2019', 'Agustus 2019', 'Rendi Putra Pradana', 'Guru', '4500000', '500000', '50000', '0', '750000', '2500000', '0', '655000', '0', '0', '', '3800000', '655000', '5395000'),
('0000000003', '0112040', '17 November 2019', 'November 2019', 'Yogga Fadhillah', 'Manager', '25000000', '0', '50000', '0', '0', '1000000', '0', '2505000', '0', '0', '50000', '25050000', '2555000', '22495000'),
('0000000004', '0112040', '17 November 2019', 'September 2019', 'Yogga Fadhillah', 'Manager', '25000000', '0', '50000', '0', '0', '1000000', '0', '2505000', '0', '0', '50000', '25050000', '2555000', '22495000'),
('0000000005', '0110000', '17 November 2019', 'November 2019', 'Rendi Putra Pradana', 'Guru', '4500000', '0', '50000', '0', '750000', '2500000', '0', '605000', '0', '0', '50000', '6050000', '655000', '5395000'),
('0000000006', '123', '19 November 2019', 'November 2019', 'rendi', 'Marketing', '10000000', '0', '50000', '0', '0', '1000000', '0', '1005000', '0', '0', '50000', '10050000', '1055000', '8995000'),
('0000000007', '123', '19 November 2019', 'September 2019', 'rendi', 'Marketing', '10000000', '0', '50000', '0', '0', '1000000', '0', '1005000', '0', '0', '50000', '10050000', '1055000', '8995000'),
('0000000008', '1213123', '23 Januari 2020', 'Januari 2020', 'Abi ', 'Administrasi', '9000000', '200000', '50000', '0', '0', '1000000', '0', '925000', '0', '0', '50000', '9250000', '975000', '8275000');

--
-- Indexes for dumped tables
--

--
-- Indeks untuk tabel `tbulan`
--
ALTER TABLE `tbulan`
  ADD PRIMARY KEY (`id`);

--
-- Indeks untuk tabel `tgolongan`
--
ALTER TABLE `tgolongan`
  ADD PRIMARY KEY (`Golongan`);

--
-- Indeks untuk tabel `tjabatan`
--
ALTER TABLE `tjabatan`
  ADD PRIMARY KEY (`Kode_jabatan`);

--
-- Indeks untuk tabel `tkaryawan`
--
ALTER TABLE `tkaryawan`
  ADD PRIMARY KEY (`NPK`);

--
-- Indeks untuk tabel `tlogin`
--
ALTER TABLE `tlogin`
  ADD PRIMARY KEY (`id_user`);

--
-- Indeks untuk tabel `tpenggajian`
--
ALTER TABLE `tpenggajian`
  ADD PRIMARY KEY (`id_gaji`) USING BTREE;

--
-- AUTO_INCREMENT untuk tabel yang dibuang
--

--
-- AUTO_INCREMENT untuk tabel `tlogin`
--
ALTER TABLE `tlogin`
  MODIFY `id_user` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
