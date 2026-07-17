# CloudVPSViet Manager (VPSManager)

Ứng dụng Windows Desktop quản trị Cloud VPS siêu nhẹ, tối ưu hóa đặc biệt cho VPS cấu hình thấp, được biên dịch bằng **Native AOT** trên nền tảng **.NET 10** và **Avalonia UI 12**.

---

## 🚀 Đặc điểm nổi bật
* **Native AOT (Ahead-Of-Time):** Biên dịch trực tiếp ra mã máy native, không cần cài đặt .NET Runtime trên VPS.
* **Siêu nhẹ & Tiết kiệm RAM:** Khởi động tức thì, tiêu hao rất ít RAM và tài nguyên CPU.
* **Scale / DPI Safe:** Fix triệt để lỗi giao diện co giãn, vỡ khung khi kết nối qua RDP với các tỷ lệ DPI khác nhau (100%, 125%, 150%, 175%).
* **Quản trị An toàn:** Đổi cổng RDP, đổi mật khẩu và đổi username trực tiếp qua các API hệ thống an toàn (P/Invoke), không lộ thông tin ra log hoặc process arguments.
* **Auto Clear RAM:** Giải phóng bộ nhớ vật lý định kỳ nhẹ nhàng qua API `EmptyWorkingSet`.
* **Cảnh báo Telegram:** Tự động giám sát nhật ký Security Event Log (Event ID 4624/4625) và gửi thông báo đăng nhập VPS về Telegram.

---

## 🛠️ Hướng dẫn cài đặt & Cấu hình

Ứng dụng tự động tạo thư mục dữ liệu tại `%AppData%\VPSManager` khi khởi chạy lần đầu.

### 1. Cấu hình Telegram & Hệ thống (`appsettings.json`)
File cấu hình được lưu tại: `%AppData%\VPSManager\appsettings.json`.

Các trường nhạy cảm như `TelegramBotToken` và `TelegramChatId` sẽ được ứng dụng **tự động mã hóa bằng Windows DPAPI** (chỉ tài khoản chạy ứng dụng trên máy đó mới có quyền giải mã), đảm bảo an toàn tuyệt đối.

Cấu trúc file `appsettings.json` mẫu:
```json
{
  "TelegramEnabled": true,
  "TelegramBotToken": "BOT_TOKEN_CỦA_BẠN",
  "TelegramChatId": "CHAT_ID_NHẬN_TIN_NHẮN",
  "ClearRamEnabled": false,
  "ClearRamIntervalMinutes": 15,
  "LastProcessedEventRecordId": 0
}
```

### 2. Cấu hình Danh sách Tool tải từ GitHub (`tools.json`)
File danh sách công cụ tải về nằm ở: `%AppData%\VPSManager\tools.json`.

Bạn có thể chỉnh sửa file này để thêm hoặc bớt các công cụ tải xuống:
```json
[
  {
    "name": "AutoDeTuPro",
    "description": "Bộ công cụ tự động úp đệ tử sơ sinh chuyên nghiệp và tối ưu tài nguyên.",
    "url": "https://github.com/cloudvpsviet/AutoDeTuPro/releases/latest/download/AutoDeTuPro.rar",
    "outputFolderName": "AutoDeTuPro"
  }
]
```
* **url:** Phải sử dụng giao thức HTTPS. Hỗ trợ giải nén tự động định dạng `.zip` (qua thư viện chuẩn) và `.rar` (qua thư viện `GrindCore.SharpCompress` tương thích Native AOT).
* **outputFolderName:** Thư mục giải nén sẽ được tạo trên Desktop với tên này.

---

## 🏗️ Hướng dẫn Build từ Source Code

### Yêu cầu hệ thống
* Cài đặt **.NET 10 SDK** (bản 10.0.301 hoặc mới hơn).
* Cài đặt **Visual Studio 2022** kèm theo gói *Desktop development with C++* (cần thiết cho trình liên kết Native AOT Linker trên Windows).

### Lệnh Build & Publish

#### 1. Biên dịch thông thường (Debug/Release để chạy thử):
```bash
# Restore các package
dotnet restore

# Build dự án
dotnet build src/VPSManager/VPSManager.csproj -c Release
```

#### 2. Biên dịch Native AOT (Tạo file .exe chạy độc lập không phụ thuộc Runtime):
```bash
dotnet publish src/VPSManager/VPSManager.csproj -c Release -r win-x64 /p:PublishAot=true /p:SelfContained=true
```

Sau khi chạy lệnh trên thành công, file chạy duy nhất `VPSManager.exe` sẽ được tạo ra tại thư mục:
`src\VPSManager\bin\Release\net10.0\win-x64\publish\`

---

## 🔒 Yêu cầu Quyền hạn
* Ứng dụng cần được chạy dưới quyền **Administrator** (Run as Administrator) để có thể thực thi các tác vụ hệ thống:
  * Đọc registry cổng RDP và thay đổi cấu hình Windows Firewall.
  * Thay đổi tên Username / Password của tài khoản Windows Local.
  * Truy cập đọc Security Event Log để quét lịch sử đăng nhập.
  * Dọn dẹp RAM của các tiến trình hệ thống khác.
* Nếu chưa có quyền Admin, ứng dụng sẽ hiển thị một banner cảnh báo màu vàng ở trên cùng kèm theo nút bấm giúp bạn nhanh chóng khởi động lại ứng dụng với quyền Administrator.
