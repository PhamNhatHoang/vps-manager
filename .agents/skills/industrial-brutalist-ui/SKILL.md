---
name: industrial-brutalist-ui
description: Raw mechanical interfaces fusing Swiss typographic print with military terminal aesthetics. Rigid grids, extreme type scale contrast, utilitarian color, analog degradation effects. For data-heavy dashboards, portfolios, or editorial sites that need to feel like declassified blueprints.
---

# Kỹ năng: Thẩm mỹ Brutalism Công nghiệp & Giao diện Đo lường Chiến thuật (Tactical Telemetry UI)

## 1. Thông tin Chung
**Tên:** Kỹ thuật Giao diện Brutalism Công nghiệp & Đo lường Chiến thuật
**Mô tả:** Trình độ nâng cao trong việc xây dựng các giao diện web tổng hợp giữa thiết kế in ấn Thụy Sĩ giữa thế kỷ 20, sách hướng dẫn sản xuất công nghiệp và giao diện terminal hàng không vũ trụ/quân sự cổ điển. Lĩnh vực này yêu cầu làm chủ hoàn toàn hệ thống lưới mô-đun cứng nhắc, tương phản tỷ lệ typography cực lớn, bảng màu thực dụng tối giản và giả lập lập trình hiệu ứng analog cũ kỹ (halftone, CRT scanline, bitmap dither). Mục tiêu là xây dựng các không gian số thể hiện tính năng thô ráp, sự chính xác cơ khí và mật độ dữ liệu cao, chủ động loại bỏ các mẫu UI tiêu dùng thông thường.

## 2. Các Kiểu Mẫu Trực Quan
Hệ thống thiết kế hoạt động bằng cách hợp nhất hai mô hình trực quan riêng biệt nhưng cực kỳ tương thích. **Chọn MỘT kiểu mẫu cho mỗi dự án và nhất quán thực thi nó. Không thay đổi tùy tiện hoặc trộn lẫn cả hai chế độ trong cùng một giao diện.**

### 2.1 Swiss Industrial Print (Thiết kế In ấn Công nghiệp Thụy Sĩ)
Lấy cảm hứng từ hệ thống nhận diện thương hiệu doanh nghiệp và các bản vẽ máy móc hạng nặng những năm 1960.
*   **Đặc điểm:** Chế độ sáng (light mode) có độ tương phản cao (nền giấy báo/giấy ngà thô). Sử dụng typography sans-serif monolithic dày và nặng. Lưới cấu trúc hiển thị rõ ràng thông qua các đường kẻ phân chia sắc nét. Sử dụng khoảng không gian âm bất đối xứng một cách táo bạo, được nhấn mạnh bởi các chữ số hoặc ký tự khổng lồ tràn viền màn hình. Sử dụng màu đỏ nguyên bản làm màu cảnh báo/màu nhấn chính.

### 2.2 Tactical Telemetry & CRT Terminal (Đo lường Chiến thuật & Terminal CRT)
Lấy cảm hứng từ các cơ sở dữ liệu quân sự bảo mật, hệ thống máy chủ mainframe cũ và màn hình hiển thị HUD hàng không vũ trụ.
*   **Đặc điểm:** Chỉ sử dụng chế độ tối (dark mode). Trình bày dữ liệu dạng bảng mật độ cao. Typography monospace chiếm ưu thế tuyệt đối. Tích hợp các chi tiết kỹ thuật đóng khung (ngoặc vuông ASCII, hồng tâm). Giả lập các hạn chế của phần cứng (phát sáng phosphor, scanline, độ sâu bit thấp).

## 3. Kiến trúc Typography
Typography là cấu trúc chính và cũng là chi tiết trang trí cốt lõi. Hình ảnh chỉ đóng vai trò thứ cấp. Hệ thống đòi hỏi sự biến thiên cực lớn về kích thước, độ dày nét và khoảng cách chữ.

### 3.1 Macro-Typography (Tiêu đề Cấu trúc lớn)
*   **Phân loại:** Neo-Grotesque / Heavy Sans-Serif.
*   **Font chữ tối ưu cho Web:** Neue Haas Grotesk (Black), Inter (Extra Bold/Black), Archivo Black, Roboto Flex (Heavy), Monument Extended.
*   **Thông số thực thi:**
    *   **Kích thước:** Triển khai ở quy mô khổng lồ sử dụng fluid typography (ví dụ: `clamp(4rem, 10vw, 15rem)`).
    *   **Tracking (Khoảng cách chữ):** Cực kỳ hẹp, thường là giá trị âm (`-0.03em` đến `-0.06em`), buộc các ký tự liên kết chặt chẽ tạo thành các khối kiến trúc vững chắc.
    *   **Leading (Chiều cao dòng):** Nén cực chặt (`0.85` đến `0.95`).
    *   **Casing (Viết hoa):** Chỉ sử dụng chữ in hoa để tạo tác động cấu trúc mạnh mẽ.

### 3.2 Micro-Typography (Dữ liệu số liệu & Đo lường)
*   **Phân loại:** Monospace / Technical Sans.
*   **Font chữ tối ưu cho Web:** JetBrains Mono, IBM Plex Mono, Space Mono, VT323, Courier Prime.
*   **Thông số thực thi:**
    *   **Kích thước:** Cố định và nhỏ (`10px` đến `14px` / `0.7rem` đến `0.875rem`).
    *   **Tracking:** Rộng rãi (`0.05em` đến `0.1em`) để giả lập khoảng cách của máy đánh chữ cơ khí hoặc ma trận hiển thị của terminal.
    *   **Leading:** Từ trung bình đến hẹp (`1.2` đến `1.4`).
    *   **Casing (Viết hoa):** Chỉ viết in hoa. Sử dụng cho tất cả metadata, điều hướng, ID đơn vị và tọa độ.

### 3.3 Textural Contrast (Tương phản Chất liệu - Sự phá cách Nghệ thuật)
*   **Phân loại:** High-Contrast Serif (Font Serif tương phản cao).
*   **Font chữ tối ưu cho Web:** Playfair Display, EB Garamond, Times New Roman.
*   **Thông số thực thi:** Sử dụng cực kỳ hạn chế và tiết chế. Phải trải qua quá trình xử lý hậu kỳ mạnh (halftone filters, dither 1-bit) để phá vỡ sự hoàn hảo của vector và tạo ra sự đối lập về chất liệu trước font sans-serif sạch sẽ.

## 4. Hệ màu sắc
Kiến trúc màu sắc mang tính thực dụng cao. Các hiệu ứng chuyển màu (gradient), đổ bóng mềm và hiệu ứng mờ kính hiện đại bị cấm hoàn toàn. Màu sắc mô phỏng các vật liệu in ấn vật lý hoặc màn hình phát xạ sơ khai.

**CỰC KỲ QUAN TRỌNG: Chỉ chọn DUY NHẤT một bảng màu nền cho mỗi dự án và sử dụng nhất quán. Không pha trộn nền sáng và nền tối trong cùng một giao diện.**

### Nếu là Swiss Industrial Print (Sáng):
*   **Nền (Background):** `#F4F4F0` hoặc `#EAE8E3` (Màu ngà, giấy tài liệu chưa tẩy trắng).
*   **Tiền cảnh (Foreground - Chữ/Viền):** `#050505` đến `#111111` (Màu mực carbon).
*   **Màu nhấn (Accent):** `#E61919` hoặc `#FF2A2A` (Màu đỏ hàng không/Đỏ cảnh báo nguy hiểm). Đây là màu nhấn DUY NHẤT. Được dùng cho các nét gạch ngang chữ, đường kẻ phân chia cấu trúc dày, hoặc nổi bật dữ liệu quan trọng.

### Nếu là Tactical Telemetry (Tối):
*   **Nền (Background):** `#0A0A0A` hoặc `#121212` (Màu màn hình CRT tắt. Tránh dùng màu đen tuyệt đối `#000000`).
*   **Tiền cảnh (Foreground - Chữ/Viền):** `#EAEAEA` (Màu phosphor trắng). Đây là màu chữ chính.
*   **Màu nhấn (Accent):** `#E61919` hoặc `#FF2A2A` (Màu đỏ hàng không/Đỏ cảnh báo nguy hiểm). Cùng mã màu đỏ, áp dụng cùng quy tắc.
*   **Màu xanh lá Terminal (`#4AF626`):** Tùy chọn. CHỈ sử dụng cho một phần tử UI cụ thể và duy nhất (ví dụ: một chỉ số trạng thái hoặc một thông số dữ liệu đọc) — không bao giờ dùng làm màu chữ chung cho toàn bộ giao diện. Nếu không phục vụ mục đích chức năng rõ ràng, hãy lược bỏ hoàn toàn.

## 5. Bố cục và Thiết lập Không gian
Bố cục phải tạo cảm giác được tính toán kỹ thuật một cách khoa học. Nó từ bỏ khoảng cách padding thông thường của web để chuyển sang hiển thị phân khu rõ ràng.

*   **Lưới Blueprint Grid:** Tuân thủ nghiêm ngặt cấu trúc CSS Grid. Các phần tử không tự do trôi nổi; chúng được định vị chính xác vào các track và điểm giao của lưới grid.
*   **Phân khu rõ ràng:** Sử dụng rộng rãi các viền kẻ đặc (`1px` hoặc `2px solid`) để phân chia các vùng thông tin riêng biệt. Đường kẻ ngang (`<hr>`) thường kéo dài toàn bộ chiều rộng container để phân tách các đơn vị vận hành.
*   **Mật độ lưỡng cực (Bimodal Density):** Bố cục dao động giữa mật độ dữ liệu cực kỳ dày đặc (các cụm metadata monospace được xếp chặt chẽ cạnh nhau) và không gian âm lớn được tính toán kỹ để đóng khung các typography lớn.
*   **Hình học:** Không sử dụng thuộc tính bo góc `border-radius`. Tất cả các góc phải vuông chính xác 90 độ để thể hiện tính cứng cáp của cơ khí.

## 6. Các Component UI và Ký hiệu
Các thành phần giao diện web thông thường được thay thế bằng các phần tử đồ họa công nghiệp, thực dụng.

*   **Trang trí Cú pháp:** Sử dụng các ký tự ASCII để đóng khung các điểm dữ liệu.
    *   *Đóng khung:* `[ HỆ THỐNG GIAO HÀNG ]`, `< RE-IND >`
    *   *Định hướng:* `>>>`, `///`, `\\\\`
*   **Ký hiệu Công nghiệp:** Tích hợp nổi bật các ký hiệu đăng ký thương hiệu (`®`), bản quyền (`©`), và nhãn hiệu (`™`) hoạt động như các phần tử hình học cấu trúc hơn là văn bản pháp lý thông thường.
*   **Asset Kỹ thuật:** Tích hợp các dấu hồng tâm (`+`) tại các điểm giao của lưới grid, các vệt nét dọc lặp lại (mã vạch), các dải cảnh báo ngang dày và các chuỗi dữ liệu ngẫu nhiên (ví dụ: `REV 2.6`, `UNIT / D-01`) để giả lập tiến trình máy móc đang hoạt động.

## 7. Các Hiệu ứng Chất liệu và Hậu xử lý
Để ngăn thiết kế trông quá phẳng kỹ thuật số, lập trình hiệu ứng analog cũ kỹ được đưa vào frontend thông qua CSS và các bộ lọc SVG.

*   **Halftone và Dither 1-Bit:** Biến đổi các hình ảnh sắc độ liên tục hoặc chữ serif lớn thành các chấm pattern. Đạt được thông qua xử lý trước hoặc sử dụng các lớp phủ CSS `mix-blend-mode: multiply` kết hợp với pattern chấm tròn radial của SVG.
*   **CRT Scanlines (Đường quét CRT):** Đối với các giao diện terminal, áp dụng `repeating-linear-gradient` vào nền để giả lập các đường quét tia điện tử ngang của màn hình CRT (ví dụ: `repeating-linear-gradient(0deg, transparent, transparent 2px, rgba(0,0,0,0.1) 2px, rgba(0,0,0,0.1) 4px)`).
*   **Mechanical Noise (Nhiễu hạt cơ khí):** Một bộ lọc nhiễu hạt SVG mờ nhẹ, tĩnh lặng trên toàn bộ root của DOM để tạo vân chất liệu giấy/nhiễu đồng nhất cho cả chế độ sáng và tối.

## 8. Chỉ thị Kỹ thuật Web
1.  **Grid Determinism:** Sử dụng `display: grid; gap: 1px;` kết hợp màu nền tương phản giữa cha và con để tạo ra các đường phân chia mảnh như dao cạo hoàn hảo về mặt toán học mà không cần khai báo border phức tạp.
2.  **Độ cứng cáp Ngữ nghĩa:** Xây dựng DOM sử dụng chính xác các thẻ HTML ngữ nghĩa (`<data>`, `<samp>`, `<kbd>`, `<output>`, `<dl>`) để phản ánh đúng bản chất kỹ thuật của đo lường chiến thuật.
3.  **Thuộc tính Clamping Typography:** Áp dụng hàm `clamp()` của CSS cho các typography tiêu đề lớn để đảm bảo kích thước chữ co giãn mạnh mẽ mà vẫn bảo toàn tính toàn vẹn cấu trúc trên các khung nhìn khác nhau.
