# Website Du Lịch Biển Ba Động

> Đồ án website quảng bá du lịch Biển Ba Động, tỉnh Trà Vinh. Cho phép quản trị viên quản lý thể loại và bài viết, người dùng xem tin tức du lịch.

## Mục lục

- [Giới thiệu](#giới-thiệu)
- [Cấu trúc thư mục](#cấu-trúc-thư-mục)
- [Yêu cầu hệ thống](#yêu-cầu-hệ-thống)
- [Hướng dẫn cài đặt](#hướng-dẫn-cài-đặt)
- [Cấu hình Web.config cho máy khác](#cấu-hình-webconfig-cho-máy-khác)
- [Các chức năng hiện có](#các-chức-năng-hiện-có)
- [Tài khoản đăng nhập](#tài-khoản-đăng-nhập)
- [Các link quan trọng](#các-link-quan-trọng)

---

## Giới thiệu

| Thông tin | Chi tiết đồ án |
|---|---|
| Tên đồ án | Website Du Lịch Biển Ba Động - Trà Vinh |
| Công nghệ | ASP.NET MVC 5, Entity Framework 6, SQL Server |
| Target Framework | .NET Framework 4.7.2 |
| Database | `webbienbadong` |

Website giới thiệu các điểm du lịch, ẩm thực, dịch vụ tại Biển Ba Động (huyện Duyên Hải, tỉnh Trà Vinh). Hệ thống hỗ trợ phân quyền người dùng: quản trị viên quản lý nội dung, người dùng xem tin tức.

---

## Cấu trúc thư mục

```
D:\cvan\cvan\
├── bienbadongweb.sql          # Script SQL tạo database & dữ liệu mẫu
├── README.md
└── Travel/
    ├── Quangbadulich.sln      # Solution file
    └── WebTravelMVC/          # Project chính
        ├── Quangbadulich.csproj
        ├── Web.config                    # File cấu hình (chứa connection string)
        ├── bin/
        │   └── WebTravelMVC.dll.config    # Bản sao Web.config (cần đồng bộ)
        ├── Models/
        │   ├── EFTravel.cs       # DbContext - kết nối database
        │   ├── BaiViet.cs        # Model Bài viết
        │   ├── DanhMuc.cs        # Model Danh mục
        │   ├── TheLoai.cs         # Model Thể loại
        │   ├── TaiKhoan.cs        # Model Tài khoản
        │   └── Quyen.cs          # Model Quyền
        ├── Areas/Common/
        │   ├── Controllers/
        │   │   ├── HomeController.cs          # Trang chủ
        │   │   ├── DangNhapController.cs      # Đăng nhập / Đăng xuất
        │   │   ├── TheLoaiController.cs       # Quản lý thể loại
        │   │   ├── BaiVietController.cs       # Quản lý bài viết
        │   │   ├── ThongBaoController.cs      # Thông báo thành công/thất bại
        │   │   └── PartialviewController.cs    # Partial view
        │   └── Views/
        │       ├── Home/Index.cshtml
        │       ├── DangNhap/DangNhap.cshtml
        │       ├── TheLoai/
        │       │   ├── ListTheLoai.cshtml
        │       │   ├── ThemMoiTheLoai.cshtml
        │       │   └── CapNhatTheLoai.cshtml
        │       ├── BaiViet/
        │       │   ├── BaiVietAll.cshtml
        │       │   ├── ThemMoiBaiViet.cshtml
        │       │   ├── CapNhatBaiViet.cshtml
        │       │   ├── DanhSachBaiViet.cshtml
        │       │   └── XemChiTiet.cshtml
        │       ├── ThongBao/
        │       │   ├── ThanhCong.cshtml
        │       │   └── ThatBai.cshtml
        │       └── Shared/_LayoutCommon.cshtml  # Layout chính
        └── Assets/
            ├── css/
            ├── js/
            ├── images/
            └── Login/                       # Trang đăng nhập riêng
```

---

## Yêu cầu hệ thống

- **Visual Studio 2019 / 2022**
- **SQL Server** (SQL Server Express hoặc bản đầy đủ)
- **.NET Framework 4.7.2** (SDK tương ứng)
- **SSMS** (SQL Server Management Studio) - để chạy script SQL

---

## Hướng dẫn cài đặt

### Bước 1: Mở Solution

Mở file `Quangbadulich.sln` bằng Visual Studio:

```
D:\cvan\cvan\Travel\Quangbadulich.sln
```

### Bước 2: Tạo Database trong SSMS

1. Mở **SQL Server Management Studio (SSMS)**
2. Kết nối đến server của bạn (VD: `local\SQLEXPRESS`)
3. Mở file `bienbadongweb.sql`
4. **Sửa đường dẫn file** trong câu lệnh `CREATE DATABASE` (nếu cần):

```sql
-- Dòng 7-8 - Sửa đường dẫn phù hợp với máy của bạn
( NAME = N'webbienbadong', FILENAME = N'C:\Program Files\...\webbienbadong.mdf' ...)
```

5. **Chọn toàn bộ** nội dung file SQL (`Ctrl+A`) → nhấn **Execute** (F5)
6. Xác nhận database `webbienbadong` đã được tạo trong **Object Explorer**

### Bước 3: Cấu hình Connection String

Kiểm tra và sửa file `Web.config` và `bin\WebTravelMVC.dll.config`:

```xml
<connectionStrings>
  <add name="EFTravel"
       connectionString="data source=<TÊN_SERVER_CỦA_BẠN>;initial catalog=webbienbadong;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

- Thay `<TÊN_SERVER_CỦA_BẠN>` bằng tên server SQL của bạn (VD: `local\SQLEXPRESS`, `localhost`, `DESKTOP-XXX\SQLEXPRESS`, ...)

### Bước 4: Build và Chạy

1. Trong Visual Studio: `Build` → `Build Solution` (`Ctrl+Shift+B`)
2. Nhấn `F5` (Start Debugging) hoặc `Ctrl+F5` (Start Without Debugging)

> Website sẽ mở tại: `http://localhost:<PORT>/`

---

## Cấu hình Web.config cho máy khác

### Cách tìm tên SQL Server của bạn

Trong **SSMS**, khi kết nối, ô **Server name** hiển thị tên server. Thường có dạng:

| Dạng | Ví dụ |
|---|---|
| LocalSQL | `localhost` |
| Named Instance | `local\SQLEXPRESS`, `DESKTOP-PC\SQLEXPRESS` |
| Default Instance | `DESKTOP-PC` |

### Cấu hình chi tiết

Sửa **cả 2 file** sau:

**File 1:** `WebTravelMVC\Web.config`
```
D:\cvan\cvan\Travel\WebTravelMVC\Web.config
```

**File 2:** `WebTravelMVC\bin\WebTravelMVC.dll.config`
```
D:\cvan\cvan\Travel\WebTravelMVC\bin\WebTravelMVC.dll.config
```

Nội dung cần sửa:

```xml
<!-- Thay local\SQLEXPRESS bằng tên server của bạn -->
<add name="EFTravel"
     connectionString="data source=local\SQLEXPRESS;initial catalog=webbienbadong;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework"
     providerName="System.Data.SqlClient" />
```

### Giải thích các tham số

| Tham số | Ý nghĩa |
|---|---|
| `data source` | Tên server SQL (/server\instance) |
| `initial catalog` | Tên database (`webbienbadong`) |
| `integrated security=True` | Dùng Windows Authentication (không cần username/password) |
| `MultipleActiveResultSets=True` | Cho phép đọc nhiều result set cùng lúc (cần cho Entity Framework) |
| `App=EntityFramework` | Tên ứng dụng trong connection string |

### Nếu dùng SQL Authentication

```xml
<add name="EFTravel"
     connectionString="data source=local\SQLEXPRESS;initial catalog=webbienbadong;User ID=sa;Password=<your_password>;MultipleActiveResultSets=True;App=EntityFramework"
     providerName="System.Data.SqlClient" />
```

---

## Các chức năng hiện có

### Chức năng công khai (ai cũng xem được)

| STT | Chức năng | Mô tả |
|-----|-----------|-------|
| 1 | Xem trang chủ | Banner giới thiệu, bài viết nổi bật, bài viết mới |
| 2 | Xem bài viết chi tiết | Nội dung bài viết đầy đủ với hình ảnh |
| 3 | Xem bài viết theo thể loại | Lọc bài viết theo danh mục/thể loại |
| 4 | Đăng nhập | Dành cho quản trị viên |

### Chức năng quản trị (cần đăng nhập)

| STT | Chức năng | Mô tả | Link |
|-----|-----------|-------|------|
| 1 | Quản lý Thể Loại | Thêm, cập nhật, xóa thể loại | [Danh sách](#các-link-quan-trọng) |
| 2 | Quản lý Bài Viết | Thêm, cập nhật, xóa bài viết | [Danh sách](#các-link-quan-trọng) |
| 3 | Ẩn/Hiện bài viết | Toggle trạng thái hiển thị | Trong trang Quản lý Bài Viết |
| 4 | Đăng xuất | Thoát khỏi tài khoản quản trị | Menu Quản Trị |

### Cấu trúc phân quyền

```
Quyen (Bảng Quyen)
├── MaQuyen = 1: Ban Quan Tri  → Đăng nhập quản trị, truy cập menu Quản Trị
└── MaQuyen = 2: Nguoi Dung   → Chưa phát triển (hiện thông báo "Chưa phát triển cho khách hàng")
```

### Sơ đồ quan hệ Database

```
DanhMuc (1) ──── (N) TheLoai (1) ──── (N) BaiViet
                                    (N)
TaiKhoan ───────────────────────────────┘
  (1)
  │
Quyen
```

---

## Tài khoản đăng nhập

### Tài khoản Quản Trị (Admin)

| Tài khoản | Mật khẩu | Họ tên | Quyền |
|-----------|----------|--------|-------|
| `Admin` | `abc123` | Ban Quan Tri | Ban Quan Tri |

### Tài khoản Người Dùng (Customer)

| Tài khoản | Mật khẩu | Họ tên | Quyền |
|-----------|----------|--------|-------|
| `Customer` | `abc123` | Khach Hang | Nguoi Dung |

> **Lưu ý:** Tài khoản `Customer` hiện chưa được phát triển tính năng dành cho khách hàng. Khi đăng nhập bằng `Customer`, hệ thống trả về thông báo **"Chưa phát triển cho khách hàng!"** và quay lại trang đăng nhập. Chức năng dành cho khách hàng chưa được xây dựng.

---

## Các link quan trọng

### Link người dùng (User)

| Mục | Link |
|-----|------|
| Trang chủ | `http://localhost:<PORT>/Common/Home/Index` |
| Đăng nhập | `http://localhost:<PORT>/Common/DangNhap/DangNhap` |

### Link quản trị (Admin) - Cần đăng nhập trước

| Mục | Link |
|-----|------|
| Danh sách Thể Loại | `http://localhost:<PORT>/Common/TheLoai/ListTheLoai` |
| Thêm Thể Loại | `http://localhost:<PORT>/Common/TheLoai/ThemMoiTheLoai` |
| Cập nhật Thể Loại | `http://localhost:<PORT>/Common/TheLoai/CapNhatTheLoai?iMaTheLoai=<id>` |
| Danh sách Bài Viết | `http://localhost:<PORT>/Common/BaiViet/BaiVietAll` |
| Thêm Bài Viết | `http://localhost:<PORT>/Common/BaiViet/ThemMoiBaiViet` |
| Cập nhật Bài Viết | `http://localhost:<PORT>/Common/BaiViet/CapNhatBaiViet?iMaBaiViet=<id>` |
| Xem Chi tiết Bài Viết | `http://localhost:<PORT>/Common/BaiViet/XemChiTiet?iMaBaiViet=<id>` |
| Xem Bài Viết theo Thể Loại | `http://localhost:<PORT>/Common/BaiViet/DanhSachBaiViet?iMaTheLoai=<id>` |
| Đăng xuất | `http://localhost:<PORT>/Common/DangNhap/DangXuatAdmin` |

### Cách truy cập trang quản trị

1. Mở trang **Đăng nhập**: `/Common/DangNhap/DangNhap`
2. Nhập **Tài khoản**: `Admin`
3. Nhập **Mật khẩu**: `abc123`
4. Nhấn **Đăng Nhập**
5. Menu **Quản Trị** sẽ xuất hiện trên thanh header → Di chuột vào để mở submenu

> **Lưu ý:** Menu "Quản Trị" dùng CSS hover - cần **di chuột vào** chứ không phải click (trên desktop). Trên thiết bị cảm ứng, có thể cần bấm giữ hoặc chỉnh sửa CSS.

---

## Xử lý lỗi thường gặp

### Lỗi kết nối SQL Server

```
System.Data.SqlClient.SqlException: A network-related or instance-specific error 
occurred while establishing a connection to SQL Server...
```

**Nguyên nhân:** Tên server trong `Web.config` không khớp với server SQL thực tế.

**Cách sửa:** Xem phần [Cấu hình Web.config cho máy khác](#cấu-hình-webconfig-cho-máy-khác) bên trên.

### Lỗi "Server not found" trong SSMS

- Kiểm tra **SQL Server Browser** service có đang chạy không.
- Thử kết nối bằng tên server mặc định: `localhost` hoặc `.` hoặc `(local)`.
- Nếu dùng Named Instance: `localhost\SQLEXPRESS`

### Lỗi build fails

- Chạy `NuGet Package Restore`: `Tools` → `NuGet Package Manager` → `Restore NuGet Packages`
- Kiểm tra .NET Framework 4.7.2 đã được cài đặt.
