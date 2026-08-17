---
name: stitch-design-taste
description: Kỹ năng Hệ thống Thiết kế Ngữ nghĩa cho Google Stitch. Tạo ra các file DESIGN.md thân thiện với Agent giúp áp dụng các tiêu chuẩn UI cao cấp, chống rập khuôn — typography nghiêm ngặt, màu sắc cân chỉnh, bố cục bất đối xứng, chuyển động micro-motion liên tục và tối ưu hiệu năng phần cứng.
---

# Stitch Design Taste — Kỹ năng Hệ thống Thiết kế Ngữ nghĩa (Semantic Design System)

## Tổng quan
Kỹ năng này tạo ra các file `DESIGN.md` được tối ưu hóa cho việc sinh giao diện màn hình tự động của Google Stitch. Nó chuyển đổi các chỉ thị kỹ thuật frontend chống rập khuôn (anti-slop) đã được kiểm chứng thực tế thành ngôn ngữ thiết kế ngữ nghĩa nguyên bản của Stitch — các quy tắc mô tả bằng ngôn ngữ tự nhiên đi kèm với các giá trị chính xác mà AI agent của Stitch có thể thông dịch để tạo ra các giao diện cao cấp, độc đáo.

File `DESIGN.md` được tạo ra đóng vai trò như **nguồn chân lý duy nhất (single source of truth)** để định hướng Stitch tạo ra các màn hình mới đồng bộ với một ngôn ngữ thiết kế có tính thẩm mỹ cao. Stitch thông dịch thiết kế thông qua các **"Mô tả Trực quan"** đi kèm với các giá trị màu sắc, thông số typography và hành vi của component cụ thể.

## Yêu cầu trước khi bắt đầu
- Quyền truy cập vào Google Stitch thông qua [labs.google.com/stitch](https://labs.google.com/stitch)
- Tùy chọn: Stitch MCP Server để tích hợp trực tiếp qua Cursor, Antigravity hoặc Gemini CLI

## Mục tiêu
Tạo ra một file `DESIGN.md` mã hóa các nội dung sau:
1. **Không khí trực quan (Visual atmosphere)** — tâm trạng, mật độ hiển thị và triết lý thiết kế.
2. **Cân chỉnh màu sắc (Color calibration)** — các màu trung tính, màu nhấn, các mã màu hex và các mẫu màu bị cấm.
3. **Kiến trúc typographic (Typographic architecture)** — họ font chữ, hệ thống phân cấp tỷ lệ và các thiên kiến typography cần tránh.
4. **Hành vi component (Component behaviors)** — nút bấm, thẻ card, ô nhập liệu đi kèm các trạng thái tương tác.
5. **Nguyên tắc bố cục (Layout principles)** — hệ thống lưới, triết lý khoảng cách, chiến lược hiển thị đáp ứng (responsive).
6. **Triết lý chuyển động (Motion philosophy)** — thông số kỹ thuật của công cụ animation, vật lý lò xo, tương tác micro-interaction liên tục.
7. **Các mẫu rập khuôn cần tránh (Anti-patterns)** — danh sách cụ thể các lối mòn thiết kế AI bị cấm.

## Hướng dẫn Phân tích & Tổng hợp

### 1. Định nghĩa Không khí trực quan (Atmosphere)
Đánh giá mục đích của dự án mục tiêu. Sử dụng các tính từ gợi cảm xúc từ lăng kính thẩm mỹ cao cấp:
- **Mật độ (Density):** "Thoáng đãng như triển lãm nghệ thuật" (1–3) → "Cân bằng ứng dụng hàng ngày" (4–7) → "Dày đặc như bảng điều khiển buồng lái" (8–10)
- **Biến thiên (Variance):** "Đối xứng dễ đoán" (1–3) → "Bất đối xứng lệch dòng" (4–7) → "Hỗn loạn đầy tính nghệ thuật" (8–10)
- **Chuyển động (Motion):** "Tĩnh lặng tiết chế" (1–3) → "Chuyển động CSS mượt mà" (4–7) → "Biên đạo chuyển động cinematic" (8–10)

Giá trị mặc định: Biến thiên 8, Chuyển động 6, Mật độ 4. Điều chỉnh linh hoạt theo vibe mô tả của người dùng.

### 2. Thiết lập Bảng màu
Với mỗi màu sắc, cung cấp: **Tên mô tả** + **Mã Hex** + **Vai trò chức năng**.

**Các ràng buộc bắt buộc:**
- Tối đa 1 màu nhấn. Độ bão hòa màu (saturation) dưới 80%.
- Thẩm mỹ "AI Purple/Blue Neon" (Phát sáng neon tím/xanh) bị CẤM NGHIÊM NGẶT — không có hiệu ứng phát sáng tím ở nút bấm, không dùng gradient neon sặc sỡ.
- Sử dụng màu nền trung tính tuyệt đối (Zinc/Slate) kết hợp một màu nhấn duy nhất có độ tương phản cao.
- Trung thành với duy nhất một bảng màu cho toàn bộ đầu ra — không pha trộn xám ấm và xám lạnh.
- Không bao giờ dùng màu đen tuyệt đối (`#000000`) — hãy dùng đen dịu (Off-Black), Zinc-950, hoặc xám than.

### 3. Thiết lập Quy tắc Typography
- **Display/Tiêu đề lớn:** Khoảng cách chữ hẹp (track-tight), kích thước được kiểm soát. Không la lớn. Phân cấp thông tin qua độ dày nét chữ và màu sắc, không chỉ đơn thuần là dùng size siêu lớn.
- **Body/Văn bản thường:** Chiều cao dòng (leading) thoải mái, tối đa 65 ký tự trên một dòng.
- **Lựa chọn Font chữ:** Font `Inter` bị CẤM trong các ngữ cảnh cao cấp/sáng tạo. Bắt buộc dùng các font có cá tính riêng: `Geist`, `Outfit`, `Cabinet Grotesk`, hoặc `Satoshi`.
- **Cấm Serif mặc định:** Các font Serif thông thường (`Times New Roman`, `Georgia`, `Garamond`, `Palatino`) bị CẤM. Nếu dự án yêu cầu font serif cho mục đích biên tập/sáng tạo, chỉ sử dụng các font serif hiện đại, độc đáo: `Fraunces`, `Gambarino`, `Editorial New`, hoặc `Instrument Serif`. Font Serif bị cấm hoàn toàn trong các dashboard hoặc UI phần mềm.
- **Ràng buộc đối với Dashboard:** Chỉ sử dụng các cặp font Sans-Serif (`Geist` + `Geist Mono` hoặc `Satoshi` + `JetBrains Mono`).
- **Ghi đè cho Mật độ cao:** Khi mật độ hiển thị vượt quá 7, toàn bộ số liệu phải sử dụng font Monospace.

### 4. Định nghĩa Section Hero
Hero là phần tạo ấn tượng đầu tiên và phải cực kỳ sáng tạo, nổi bật, không được rập khuôn:
- **Ảnh lồng trong chữ (Inline Image Typography):** Nhúng trực tiếp các ảnh nhỏ, phù hợp ngữ cảnh vào giữa các từ hoặc chữ cái trong tiêu đề chính. Các ảnh này nằm nội dòng ở cùng chiều cao chữ, bo tròn, đóng vai trò như các dấu chấm câu trực quan. Đây là kỹ thuật sáng tạo đặc trưng.
- **Không xếp đè:** Chữ không được đè lên ảnh hoặc đè lên chữ khác. Mỗi phần tử phải chiếm một vùng không gian sạch sẽ riêng biệt.
- **Không dùng chữ rác/dẫn hướng:** Các cụm từ như "Cuộn để khám phá", "Vuốt xuống", các icon mũi tên cuộn, chevron nhảy nhót bị CẤM. Nội dung trang phải tự nhiên thu hút người dùng cuộn xuống.
- **Cấu trúc Bất đối xứng:** Bố cục Hero căn giữa bị CẤM khi độ biến thiên (variance) vượt quá 4.
- **Hạn chế CTA:** Tối đa một nút CTA chính. Không dùng các link phụ kiểu "Tìm hiểu thêm".

### 5. Định nghĩa Style cho Component
Với mỗi loại component, mô tả hình dáng, màu sắc, độ sâu đổ bóng và hành vi tương tác:
- **Nút bấm (Buttons):** Phản hồi lực nhấn vật lý (tactile push feedback) ở trạng thái hoạt động. Không có hiệu ứng phát sáng neon bên ngoài. Không dùng con trỏ chuột tùy chỉnh.
- **Thẻ card:** CHỈ dùng khi độ nổi (elevation) thể hiện sự phân cấp thông tin. Pha màu đổ bóng khớp với tone màu nền. Đối với bố cục mật độ cao, thay thế thẻ card bằng các đường kẻ phân chia border-top hoặc khoảng trắng.
- **Ô nhập liệu (Inputs/Forms):** Nhãn (label) đặt trên ô nhập, văn bản hướng dẫn (helper text) là tùy chọn, thông báo lỗi đặt dưới ô nhập. Khoảng cách spacing tiêu chuẩn.
- **Trạng thái tải (Loading States):** Sử dụng skeleton loader khớp với kích thước của bố cục — không dùng các vòng xoay spinner tròn mặc định.
- **Trạng thái trống (Empty States):** Thiết kế giao diện trực quan hướng dẫn cách tạo dữ liệu.
- **Trạng thái lỗi (Error States):** Báo lỗi nội dòng rõ ràng.

### 6. Định nghĩa Nguyên tắc Bố cục
- Không xếp chồng đè các phần tử lên nhau — mỗi phần tử chiếm một vùng không gian rõ ràng. Không sử dụng định vị absolute để xếp chồng nội dung.
- Bố cục Hero căn giữa bị CẤM khi độ biến thiên vượt quá 4 — bắt buộc dùng bố cục chia đôi màn hình (Split Screen), căn lề trái, hoặc sử dụng khoảng trắng bất đối xứng.
- Hàng tính năng gồm "3 thẻ card bằng nhau nằm ngang" bị CẤM — hãy dùng zic-zac 2 cột, lưới bất đối xứng, hoặc thanh cuộn ngang.
- Ưu tiên CSS Grid hơn là tính toán flexbox — không dùng các thủ thuật phần trăm bằng hàm `calc()`.
- Giới hạn chiều rộng bố cục bằng container (ví dụ: 1400px căn giữa).
- Các section đầy màn hình phải dùng `min-h-[100dvh]` — không bao giờ dùng `h-screen` (gây giật màn hình nghiêm trọng trên iOS Safari).

### 7. Định nghĩa Quy tắc hiển thị đáp ứng (Responsive)
Mọi thiết kế phải hoạt động hoàn hảo trên mọi khung nhìn:
- **Thu gọn về một cột trên Mobile (< 768px):** Mọi bố cục nhiều cột phải tự động chuyển thành một cột xếp chồng. Không có ngoại lệ.
- **Không có thanh cuộn ngang:** Tràn nội dung tạo thanh cuộn ngang trên mobile là một lỗi nghiêm trọng.
- **Co giãn Typography:** Tiêu đề lớn co giãn qua hàm `clamp()`. Văn bản thường tối thiểu `1rem`/`14px`.
- **Vùng chạm (Touch Targets):** Mọi phần tử tương tác phải có vùng chạm tối thiểu `44px`.
- **Hành vi của Hình ảnh:** Các ảnh lồng trong chữ (inline typography images) sẽ tự động xếp chồng xuống phía dưới tiêu đề chính trên thiết bị di động.
- **Thanh điều hướng (Navigation):** Nav ngang trên desktop tự động thu gọn thành menu di động sạch sẽ.
- **Khoảng cách:** Khoảng cách dọc giữa các section giảm theo tỷ lệ tương ứng (`clamp(3rem, 8vw, 6rem)`).

### 8. Mã hóa Triết lý Chuyển động
- **Vật lý lò xo mặc định:** `stiffness: 100, damping: 20` — tạo cảm giác đầm, cao cấp. Không dùng easing tuyến tính (linear).
- **Tương tác Micro-Interaction liên tục:** Mọi active component nên có một trạng thái lặp vô tận (Nhấp nháy nhẹ, Đánh chữ, Lơ lửng, Lấp lánh).
- **Biên đạo so le (Staggered):** Không bao giờ hiển thị danh sách đồng loạt cùng lúc — hãy sử dụng hiệu ứng trễ so le thác nước (cascade delay).
- **Hiệu năng:** Chỉ tạo hoạt ảnh thông qua `transform` và `opacity`. Không tạo hoạt ảnh cho `top`, `left`, `width`, `height`. Bộ lọc hạt/noise chỉ áp dụng trên các phần tử giả cố định (fixed pseudo-elements).

### 9. Danh sách Mẫu rập khuôn bị cấm (AI Tells)
Khai báo rõ ràng các quy tắc "KHÔNG BAO GIỜ LÀM" trong file DESIGN.md:
- Không dùng emoji ở bất cứ đâu.
- Không dùng font `Inter`.
- Không dùng font serif mặc định (`Times New Roman`, `Georgia`, `Garamond`) — chỉ dùng serif hiện đại độc đáo nếu thực sự cần thiết.
- Không dùng màu đen tuyệt đối (`#000000`).
- Không dùng đổ bóng phát sáng neon bên ngoài.
- Không dùng màu nhấn quá chói (oversaturated).
- Không dùng chữ gradient quá mức trên các tiêu đề lớn.
- Không dùng con trỏ chuột tùy chỉnh.
- Không xếp chồng đè các phần tử lên nhau — luôn giữ khoảng cách rõ ràng.
- Không dùng bố cục 3 thẻ card bằng nhau nằm ngang.
- Không dùng tên gọi mặc định chung chung ("John Doe", "Acme", "Nexus").
- Không dùng số liệu làm tròn giả tạo (`99.99%`, `50%`).
- Không dùng văn văn phong tiếp thị sáo rỗng của AI ("Elevate", "Seamless", "Unleash", "Next-Gen").
- Không dùng chữ dẫn hướng rác: "Cuộn để khám phá", "Vuốt xuống", icon mũi tên, chevron nhảy nhót.
- Không dùng link Unsplash bị hỏng — hãy dùng `picsum.photos` hoặc avatar dạng SVG.
- Không dùng bố cục Hero căn giữa (đối với các dự án có độ biến thiên cao).

## Định dạng Đầu ra (Cấu trúc file DESIGN.md)

```markdown
# Hệ thống Thiết kế: [Tên Dự án]

## 1. Chủ đề Trực quan & Không khí
(Mô tả gợi cảm xúc về tâm trạng, mật độ hiển thị, độ biến thiên và mức độ hoạt ảnh.
Ví dụ: "Một giao diện thoáng đãng như phòng triển lãm tranh với các bố cục bất đối xứng đầy tự tin
và biên đạo chuyển động sử dụng vật lý lò xo mượt mà. Không khí mang tính kỹ thuật tinh tế nhưng ấm áp —
giống như một studio kiến trúc ngập tràn ánh sáng.")

## 2. Bảng màu & Vai trò
- **Canvas White** (#F9FAFB) — Màu nền chính của giao diện
- **Pure Surface** (#FFFFFF) — Màu nền của thẻ card và container
- **Charcoal Ink** (#18181B) — Màu chữ chính, sắc độ tối Zinc-950
- **Muted Steel** (#71717A) — Màu chữ phụ, văn bản mô tả, metadata
- **Whisper Border** (rgba(226,232,240,0.5)) — Viền thẻ card, các đường kẻ phân chia cấu trúc 1px
- **[Tên Màu Nhấn]** (#XXXXXX) — Màu nhấn duy nhất cho các nút CTA, trạng thái kích hoạt, focus ring
(Tối đa 1 màu nhấn. Độ bão hòa màu < 80%. Không dùng màu tím/neon.)

## 3. Quy tắc Typography
- **Display (Tiêu đề):** [Tên Font] — Khoảng cách chữ hẹp, thang đo được kiểm soát, phân cấp bằng độ dày nét.
- **Body (Văn bản thường):** [Tên Font] — Chiều cao dòng thoải mái, chiều rộng tối đa 65ch, màu chữ phụ trung tính.
- **Mono (Monospace):** [Tên Font] — Dùng cho code, metadata, nhãn thời gian, số liệu hiển thị mật độ cao.
- **Bị cấm:** Font Inter, các font hệ thống mặc định trong các ngữ cảnh cao cấp. Cấm font serif trong dashboard.

## 4. Style của Component
* **Nút bấm:** Phẳng, không phát sáng viền ngoài. Dịch chuyển -1px khi click để phản hồi lực nhấn. Màu nền nhấn cho nút chính, dạng ghost/outline cho nút phụ.
* **Thẻ card:** Bo góc lớn (2.5rem). Đổ bóng mờ nhẹ phân tán rộng. Chỉ dùng khi độ nổi phục vụ phân cấp thông tin. Đối với mật độ hiển thị cao: thay thế bằng các đường kẻ phân chia border-top.
* **Ô nhập liệu:** Nhãn đặt phía trên, báo lỗi phía dưới. Focus ring sử dụng màu nhấn. Không dùng nhãn lơ lửng.
* **Trạng thái tải (Loaders):** Skeletal shimmer khớp chính xác với kích thước bố cục. Không dùng vòng xoay spinner.
* **Trạng thái trống (Empty States):** Giao diện minh họa sinh động — không chỉ hiển thị chữ "Không có dữ liệu".

## 5. Nguyên tắc Bố cục
(Cấu trúc responsive ưu tiên grid. Bố cục Hero bất đối xứng lệch dòng.
Thu gọn nghiêm ngặt về một cột duy nhất dưới 768px. Giới hạn chiều rộng container.
Không dùng flexbox tính toán phần trăm. Padding dọc rộng rãi.)

## 6. Chuyển động & Tương tác
(Vật lý lò xo cho tất cả phần tử tương tác. Xuất hiện trễ so le.
Tương tác micro-loop liên tục trên các component của dashboard đang hoạt động. Chỉ tạo hoạt ảnh bằng transform và opacity. Tách biệt Client Components cho các chuyển động nặng.)

## 7. Các mẫu rập khuôn bị cấm (Anti-Patterns)
(Danh sách cụ thể các mẫu bị cấm: không dùng emoji, không dùng font Inter, không dùng màu đen tuyệt đối,
không đổ bóng phát sáng neon, không dùng lưới 3 cột bằng nhau, không viết văn sáo rỗng AI,
không dùng tên giả mặc định, không dùng link ảnh bị hỏng.)
```

## Thực hành Tốt nhất
- **Hãy mang tính mô tả:** "Deep Charcoal Ink (#18181B)" — không chỉ viết ngắn gọn "màu chữ tối".
- **Hãy mang tính chức năng:** Giải thích rõ vai trò sử dụng của từng phần tử.
- **Hãy nhất quán:** Sử dụng thuật ngữ đồng bộ xuyên suốt tài liệu.
- **Hãy chính xác:** Đưa ra các mã hex, giá trị rem, giá trị pixel chính xác trong dấu ngoặc đơn.
- **Hãy có chính kiến:** Đây không phải một template trung tính — nó áp dụng và bắt buộc một thẩm mỹ cụ thể, cao cấp.

## Mẹo để Thành công
1. Bắt đầu từ không khí trực quan — hiểu rõ vibe chung của dự án trước khi đi sâu vào chi tiết các token.
2. Tìm kiếm quy luật chung — xác định khoảng cách, kích thước và định dạng style đồng bộ.
3. Tư duy ngữ nghĩa — đặt tên màu sắc theo mục đích sử dụng, không đặt theo hình thức xuất hiện.
4. Cân nhắc phân cấp thông tin — ghi chép rõ cách trọng số trực quan truyền tải tầm quan trọng của phần tử.
5. Mã hóa các điều cấm — danh sách anti-pattern cũng quan trọng như các quy tắc thiết kế.

## Các lỗi phổ biến cần tránh
- Sử dụng thuật ngữ kỹ thuật khô khan mà không giải thích ("rounded-xl" thay vì "các góc bo tròn mềm mại").
- Bỏ quên mã hex hoặc chỉ dùng tên mô tả chung chung.
- Quên ghi chú vai trò chức năng của các phần tử thiết kế.
- Mô tả không khí trực quan quá mơ hồ.
- Bỏ qua danh sách anti-pattern — đây chính là yếu tố cốt lõi giúp thiết kế của bạn trở nên cao cấp.
- Thiết kế theo các lối "an toàn" mặc định thay vì áp dụng thẩm mỹ tinh tế được định hướng sẵn.
