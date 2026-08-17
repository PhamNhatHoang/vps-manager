---
name: minimalist-ui
description: Giao diện phong cách báo chí (editorial) tối giản và tinh tế. Bảng màu đơn sắc ấm áp, độ tương phản typography cực cao, lưới bento phẳng, và các điểm nhấn pastel dịu nhẹ. Không sử dụng gradient sặc sỡ, không dùng đổ bóng nặng.
---

# Giao Thức: Kiến Trúc Sư Giao Diện Tối Giản Cao Cấp (Minimalist & Editorial UI)

## 1. Tổng Quan Giao Thức

*   **Tên gọi:** Phong cách Tối giản Tiện ích Cao cấp & Giao diện Biên tập (Premium Utilitarian Minimalism & Editorial UI).
*   **Mô tả:** Chỉ thị kỹ thuật frontend nâng cao để tạo ra các giao diện web tinh tế, siêu tối giản, mang phong cách "tài liệu" tương tự như các nền tảng quản lý công việc hàng đầu thế giới. Giao thức này bắt buộc sử dụng bảng màu đơn sắc ấm áp có độ tương phản cao, hệ thống phân cấp typography tùy chỉnh, khoảng cách macro-whitespace rộng rãi, bố cục lưới bento phẳng và kiến trúc component tối giản có điểm nhấn là các màu pastel dịu nhẹ. Giao thức này loại bỏ hoàn toàn các xu hướng thiết kế SaaS rập khuôn của AI.

---

## 2. Các Giới Hạn Tuyệt Đối (Các Thành Phần Bị CẤM)

AI Agent bắt buộc phải tránh xa các thiết lập mặc định rập khuôn sau:
*   **CẤM SỬ DỤNG** các font chữ: "Inter", "Roboto" hoặc "Open Sans".
*   **CẤM SỬ DỤNG** các thư viện icon nét mỏng thông thường như "Lucide", "Feather" hoặc "Heroicons" tiêu chuẩn.
*   **CẤM SỬ DỤNG** hiệu ứng đổ bóng nặng mặc định của Tailwind (ví dụ: `shadow-md`, `shadow-lg`, `shadow-xl`). Đổ bóng phải gần như bằng không hoặc được tùy biến cực kỳ khuếch tán với độ mờ siêu thấp (dưới 0.05).
*   **CẤM SỬ DỤNG** nền màu sắc chủ đạo cho các khối lớn (ví dụ: cấm dùng nền Hero màu xanh lá, xanh dương hoặc đỏ rực rỡ).
*   **CẤM SỬ DỤNG** hiệu ứng chuyển màu (gradients), màu neon rực rỡ hoặc hiệu ứng kính 3D (ngoại trừ hiệu ứng mờ nhẹ của navbar).
*   **CẤM SỬ DỤNG** dạng bo tròn viên thuốc (`rounded-full`) cho các container lớn, card hoặc nút bấm chính.
*   **CẤM SỬ DỤNG** biểu tượng cảm xúc (emojis) ở bất kỳ đâu trong mã nguồn, văn bản hoặc alt text. Thay thế bằng các icon tiêu chuẩn hoặc hình vẽ SVG tối giản.
*   **CẤM SỬ DỤNG** các tên giả định chung chung như "John Doe", "Acme Corp" hoặc "Lorem Ipsum". Luôn sử dụng dữ liệu thực tế và đúng ngữ cảnh.
*   **CẤM SỬ DỤNG** các cụm từ sáo rỗng thường thấy của AI: "Elevate", "Seamless", "Unleash", "Next-Gen", "Game-changer", "Delve". Hãy viết ngôn từ rõ ràng, thực tế và cụ thể.

---

## 3. Kiến Trúc Typography (Font Chữ)

Giao diện phải dựa trên sự tương phản chữ cực cao và các font chữ cao cấp để tạo ra cảm giác biên tập:
*   **Font Sans-Serif chính (Body, UI, Buttons):** Sử dụng các font chữ hình học sạch sẽ hoặc font hệ thống. Lựa chọn: `font-family: 'SF Pro Display', 'Geist Sans', 'Helvetica Neue', 'Switzer', sans-serif`.
*   **Font Display Serif (Hero Headings & Quotes):** Lựa chọn: `font-family: 'Lyon Text', 'Newsreader', 'Playfair Display', 'Instrument Serif', serif`. Áp dụng khoảng cách chữ hẹp (`letter-spacing: -0.02em` đến `-0.04em`) và chiều cao dòng hẹp (`leading-[1.1]`).
*   **Font Monospace (Mã nguồn, phím bấm, metadata):** Lựa chọn: `font-family: 'Geist Mono', 'SF Mono', 'JetBrains Mono', monospace`.
*   **Màu chữ:** Chữ hiển thị không bao giờ dùng màu đen tuyệt đối (`#000000`). Bắt buộc dùng màu đen than / off-black (`#111111` hoặc `#2F3437`) với chiều cao dòng rộng rãi `1.6` để dễ đọc. Chữ phụ dùng màu xám dịu (`#787774`).

---

## 4. Bảng Màu (Đơn Sắc Ấm + Điểm Nhấn Pastel)

Màu sắc là tài nguyên khan hiếm, chỉ được sử dụng cho mục đích biểu đạt ngữ nghĩa hoặc làm điểm nhấn cực kỳ tiết chế:
*   **Nền trang (Canvas / Background):** Màu trắng tinh khiết `#FFFFFF` hoặc màu xương ấm / off-white `#F7F6F3` / `#FBFBFA`.
*   **Nền thẻ (Cards):** `#FFFFFF` hoặc xám kem nhạt `#F9F9F8`.
*   **Đường viền / Vạch chia cấu trúc:** Xám siêu nhạt `#EAEAEA` hoặc `rgba(0,0,0,0.06)`.
*   **Màu điểm nhấn (Accent):** Chỉ sử dụng các màu pastel dịu nhẹ, giảm bão hòa cho các thẻ tag, nền code inline hoặc nền icon:
    *   *Đỏ nhạt (Pale Red):* Nền `#FDEBEC` (Màu chữ: `#9F2F2D`)
    *   *Xanh dương nhạt (Pale Blue):* Nền `#E1F3FE` (Màu chữ: `#1F6C9F`)
    *   *Xanh lá nhạt (Pale Green):* Nền `#EDF3EC` (Màu chữ: `#346538`)
    *   *Vàng nhạt (Pale Yellow):* Nền `#FBF3DB` (Màu chữ: `#956400`)

---

## 5. Đặc Tả Thành Phần (Components)

*   **Lưới Bento Box Grid:**
    *   Sử dụng CSS Grid bất đối xứng.
    *   Các thẻ card phải có viền chính xác `border: 1px solid #EAEAEA`.
    *   Bo tròn góc sắc nét: tối đa `8px` hoặc `12px`.
    *   Padding bên trong rộng rãi (ví dụ: `24px` đến `40px`).
*   **Nút Kêu Gọi Hành Động (CTA Button):**
    *   Nền đặc màu tối `#111111`, chữ trắng `#FFFFFF`.
    *   Bo tròn góc nhẹ (`4px` đến `6px`). Không dùng đổ bóng.
    *   Trạng thái hover chuyển màu nhẹ sang `#333333` hoặc hiệu ứng co nhẹ `scale(0.98)`.
*   **Thẻ trạng thái (Status Badges / Tags):**
    *   Dạng bo tròn viên thuốc (`rounded-full`), cỡ chữ siêu nhỏ (`text-xs`), viết hoa với khoảng cách chữ rộng (`tracking-wider`).
    *   Nền bắt buộc dùng màu Pastel dịu nhẹ ở mục 4.
*   **Khối FAQs (Accordions):**
    *   Lược bỏ toàn bộ khung bao bọc bên ngoài. Ngăn cách các câu hỏi chỉ bằng một đường viền dưới `border-bottom: 1px solid #EAEAEA`.
    *   Sử dụng biểu tượng `+` và `-` sắc nét, tối giản cho trạng thái đóng mở.
*   **Phím tắt hệ thống (Keystroke Micro-UI):**
    *   Hiển thị các phím tắt bằng thẻ `<kbd>` với CSS: `border: 1px solid #EAEAEA`, `border-radius: 4px`, `background: #F7F6F3`, sử dụng font Monospace.
*   **Mô phỏng cửa sổ macOS (Faux-OS Window):**
    *   Khi mô phỏng giao diện phần mềm, bao bọc trong một container tối giản với thanh bar màu trắng ở trên chứa 3 hình tròn nhỏ màu xám nhạt (mô phỏng nút tắt/bật cửa sổ của macOS).

---

## 6. Định Hướng Hình Ảnh & Biểu Tượng

*   **Biểu tượng:** Sử dụng họ "Phosphor Icons (nét Bold hoặc Fill)" hoặc "Radix UI Icons" để tạo cảm giác kỹ thuật, nét dày dặn. Giữ nguyên độ dày nét vẽ (stroke width) thống nhất trên toàn trang.
*   **Hình vẽ minh họa:** Định dạng đơn sắc, nét vẽ tay phác thảo mảnh trên nền trắng, kết hợp một khối hình học đơn màu màu pastel nhạt nằm lệch phía sau.
*   **Hình ảnh:** Sử dụng ảnh chất lượng cao, giảm độ bão hòa màu và phủ tone màu ấm. Áp dụng hiệu ứng nhiễu hạt nhẹ để ảnh chìm vào bảng màu đơn sắc của trang. Không dùng ảnh stock rực rỡ màu sắc.
*   **Chiều sâu của Nền:** Nền trang không được để phẳng và trống rỗng. Hãy áp dụng các ảnh nền mờ độ đục cực thấp, các quầng sáng mờ ảo (`radial-gradient` tone ấm với độ đục `opacity: 0.03`) hoặc lưới dòng kẻ hình học tối giản để tạo độ sâu.

---

## 7. Chuyển Động & Hoạt Ảnh Nhẹ Nhàng (Micro-Animations)

Chuyển động phải mang lại cảm giác ẩn hiện tinh tế — luôn hiện diện nhưng không bao giờ gây phân tâm. Mục tiêu là sự tinh tế tĩnh lặng, không phải sự phô diễn.
*   **Xuất hiện khi cuộn (Scroll Entry):** Các phần tử chuyển động trượt nhẹ lên và hiện dần. Sử dụng `translateY(12px)` + `opacity: 0` chạy trong `600ms` với easing `cubic-bezier(0.16, 1, 0.3, 1)`. Sử dụng `IntersectionObserver`, cấm dùng sự kiện cuộn của window.
*   **Hover & Active:** Các thẻ card nhấc nhẹ lên bằng cách chuyển đổi shadow mờ từ `0` sang `0 2px 8px rgba(0,0,0,0.04)` trong `200ms`. Nút bấm thu nhỏ nhẹ `scale(0.98)` khi `:active`.
*   **Xuất hiện so le (Staggered Reveals):** Các phần tử lưới hoặc danh sách xuất hiện trễ nhau theo thứ tự (`animation-delay: calc(var(--index) * 80ms)`). Không để mọi thứ xuất hiện cùng một lúc.

---

## 8. Giao Thức Thực Thi

Khi bắt tay vào viết mã nguồn giao diện:
1.  Thiết lập khoảng cách macro-whitespace trước. Sử dụng padding dọc cực lớn giữa các section (ví dụ: `py-24` hoặc `py-32`).
2.  Giới hạn chiều rộng nội dung chữ chính ở mức `max-w-4xl` hoặc `max-w-5xl`.
3.  Áp dụng ngay lập tức hệ thống phân cấp font chữ và màu sắc đơn sắc đã cấu hình.
4.  Đảm bảo mọi thẻ card, vạch chia và viền tuân thủ nghiêm ngặt quy tắc nét viền `1px solid #EAEAEA`.
5.  Thêm hoạt ảnh xuất hiện khi cuộn (scroll-entry) cho tất cả các khối nội dung lớn.
6.  Đảm bảo nền trang có chiều sâu thông qua ảnh nền mờ, quầng sáng hoặc lưới dòng kẻ — cấm để nền phẳng trơn hoàn toàn.
