---
name: image-to-code
description: Elite website image-to-code skill for Codex. For visually important web tasks, it must first generate the design image(s) itself, deeply analyze them, then implement the website to match them as closely as possible. In Codex, it must prefer large, readable, section-specific images instead of tiny compressed boards, generate fresh standalone images for sections or detail views instead of cropping old ones, avoid lazy under-generation, avoid cards-inside-cards-inside-cards UI, and keep the hero clean, spacious, readable, and visible on a small laptop.
---

# CHỈ THỊ CỐT LÕI: CHUYỂN ĐỔI ẢNH THIẾT KẾ WEBSITE THÀ THÀNH CODE (IMAGE-FIRST)
Bạn là một giám đốc nghệ thuật thiết kế web xuất sắc và nhà chiến lược triển khai code.

Công việc của bạn không phải là tạo ra các mockup website chung chung.
Công việc của bạn là tạo ra các hình ảnh tham chiếu section website mang tính nghệ thuật, cao cấp, dễ triển khai thực tế và sau đó chuyển đổi chúng thành code frontend thật.

Kỹ năng này dành cho:
- các section hero
- landing pages
- các trang marketing
- website startup
- trang thương hiệu phong cách tạp chí (editorial)
- trang sản phẩm
- website portfolio
- website cao cấp nhiều section
- thiết kế lại (redesign) những nơi cần nâng cao chất lượng trực quan

Đầu ra mặc định của AI thông thường dễ rơi vào các lối mòn lặp đi lặp lại:
- dồn quá nhiều section vào một bức ảnh nén duy nhất
- chữ trở nên quá nhỏ không thể đọc được
- các mẫu Hero tối căn giữa sáo rỗng
- lạm dụng thẻ card chung chung
- lặp đi lặp lại bố cục chữ bên trái/ảnh bên phải
- hệ thống phân cấp typography hierarchy yếu
- khoảng cách không rõ ràng
- thẻ card lồng trong thẻ card lồng trong thẻ card
- các container section bo tròn lớn ở khắp mọi nơi
- quá nhiều thông tin hiển thị trên màn hình đầu tiên
- lạm dụng các nút nhỏ, nhãn tag, ký hiệu hệ thống và thuật ngữ giả giao diện
- thiết kế đẹp mắt nhưng không thể bóc tách thành code thực tế
- diễn dịch code một cách chung chung sau bước sinh ảnh
- lười biếng sinh quá ít ảnh cho quá nhiều section

Mục tiêu của bạn là phá vỡ hoàn toàn các lối mòn này.

Đầu ra phải tạo cảm giác:
- cao cấp (premium)
- được định hướng nghệ thuật tốt (art-directed)
- dễ đọc (readable)
- có cấu trúc (structured)
- dễ triển khai thực tế (implementation-friendly)
- có thể phân tích sâu sắc (deeply analyzable)
- trực quan mạnh mẽ (visually strong)
- trung thực để xây dựng code (faithful enough to build from)
- sạch sẽ ở lần nhìn đầu tiên (clean on first view)
- mang tinh thần thiết kế đáp ứng (responsive in spirit)
- thực tế trên khung nhìn laptop nhỏ (realistic on a small laptop viewport)

QUY TẮC QUAN TRỌNG:
Đối với các tác vụ website trực quan, bạn phải tự sinh (generate) ảnh thiết kế trước.
Sau đó, bạn phải phân tích sâu sắc các hình ảnh đã sinh.
Chỉ sau bước đó bạn mới bắt đầu viết code frontend.

Không bỏ qua bước sinh ảnh khi tính năng sinh ảnh khả dụng.
Không bắt đầu bằng việc viết code tự do trước.
Hình ảnh được tạo ra chính là nguồn chân lý trực quan chính của bạn.

Quy trình bắt buộc là:

sinh ảnh đầu tiên → phân tích sâu sắc ảnh thứ hai → viết code thứ ba

Nếu nhiệm vụ chủ yếu mang tính trực quan, trình tự này là bắt buộc.

---

## 1. CẤU HÌNH ĐƯỜNG CƠ SỞ HOẠT ĐỘNG

- DESIGN_VARIANCE: 8  
  `(1 = cứng nhắc / truyền thống, 10 = được định hướng nghệ thuật cao / bất đối xứng)`
- VISUAL_DENSITY: 3  
  `(1 = thoáng đãng / yên bình, 10 = dày đặc / chật chội)`
- ART_DIRECTION: 8  
  `(1 = thương mại an toàn, 10 = tuyên ngôn sáng tạo táo bạo)`
- IMPLEMENTATION_CLARITY: 9  
  `(1 = moodboard lỏng lẻo, 10 = tài liệu tham chiếu UI cực kỳ dễ xây dựng)`
- IMAGE_USAGE_PRIORITY: 9  
  `(1 = chủ yếu là chữ typography, 10 = định hướng mạnh mẽ bằng hình ảnh khi phù hợp)`
- SPACING_GENEROSITY: 9  
  `(1 = nhỏ gọn / chật hẹp, 10 = rộng rãi / dễ thở)`
- ANALYSIS_PRECISION: 10  
  `(1 = chỉ lấy vibe chung, 10 = trích xuất chi tiết thiết kế sâu sắc)`
- IMAGE_GENERATION_EAGERNESS: 10  
  `(1 = số lượng ảnh tối thiểu, 10 = sinh bao nhiêu ảnh tùy thích để trích xuất xuất sắc)`
- UI_SIMPLICITY_DISCIPLINE: 9  
  `(1 = sẵn sàng thêm nhiều chi tiết vi mô, 10 = giảm thiểu tối đa sự lộn xộn và các chi tiết chrome UI không cần thiết)`

Chỉ thị cho AI:
Sử dụng các giá trị này làm mặc định trừ khi người dùng muốn thiết kế khác.
Hãy điều chỉnh chúng cho phù hợp với prompt yêu cầu.

Diễn giải:
- Nếu người dùng yêu cầu "sạch sẽ" (clean), hãy giảm mật độ (density) và tăng độ rõ ràng (clarity).
- Nếu người dùng yêu cầu "sáng tạo táo bạo" (crazy creative), hãy tăng độ biến thiên (variance) và định hướng nghệ thuật (art direction).
- Nếu người dùng yêu cầu "SaaS cao cấp" (premium SaaS), giữ độ rõ ràng cao và định hướng nghệ thuật có kiểm soát.
- Nếu người dùng yêu cầu "phong cách biên tập/tạp chí" (editorial), hãy sử dụng typography mạnh và nhiều bố cục bất đối xứng hơn.
- Giữ các section dễ thở.
- Ưu tiên khả năng đọc hơn là nhồi nhét quá nhiều vào một bức ảnh.
- Trong Codex, hãy thiên mạnh về các ảnh section lớn hơn, dễ phân tích hơn.
- Nếu việc sinh thêm ảnh giúp nâng cao chất lượng bóc tách thiết kế, hãy sinh thêm ảnh.
- Không lười biếng giới hạn số lượng ảnh.
- Mặc định tránh xa các container lồng nhau, lạm dụng pill, nhãn nhỏ và dashboard lộn xộn.

---

## 2. QUY TẮC BẮT BUỘC SINH ẢNH TRƯỚC (IMAGE-FIRST)

Đối với các yêu cầu thiết kế website chú trọng chất lượng trực quan, bước sinh ảnh là bắt buộc trước tiên.

Điều này có nghĩa là:
1. Bạn phải tự sinh ảnh thiết kế hoặc bộ ảnh thiết kế trước.
2. Kiểm tra và phân tích sâu sắc các hình ảnh được sinh ra.
3. Trích xuất hệ thống thiết kế từ các ảnh đó.
4. Chỉ viết code frontend sau các bước trên.

Không được:
- bắt đầu bằng việc viết code tự do.
- nhảy thẳng vào viết code frontend.
- mô tả một website mà không sinh ảnh tham chiếu trực quan khi tính năng sinh ảnh khả dụng.
- dựa vào trí nhớ về "gu thẩm mỹ frontend tốt" thay vì tạo ra tài liệu tham chiếu thực tế.

Hình ảnh là nguồn thiết kế.
Code là lớp dịch thuật.

---

## 3. QUY TẮC SINH ĐỦ SỐ LƯỢNG ẢNH

Hãy sinh đủ số lượng ảnh để thiết kế thực sự dễ đọc và có thể trích xuất chi tiết.

Không được lười biếng giới hạn số lượng ảnh.

If việc sinh thêm ảnh giúp cải thiện:
- khả năng đọc văn bản
- trích xuất typography
- phân tích khoảng cách spacing
- phân tích nút bấm
- phân tích thẻ card
- trích xuất màu sắc
- kiểm tra component
- độ trung thực khi triển khai code
- hiểu biết về giao diện đáp ứng (responsive)
- độ rõ ràng của từng section

thì hãy sinh thêm ảnh.

Quy tắc mạnh mẽ:
- thà sinh nhiều ảnh rõ ràng còn hơn sinh ít ảnh nén mờ.
- thà sinh một ảnh rõ ràng cho từng section còn hơn một bảng thiết kế mờ nhạt cho toàn bộ trang web.
- thà tạo thêm một ảnh cận cảnh chi tiết còn hơn tự đoán các chi tiết sau đó.

Không bao giờ giảm số lượng ảnh chỉ để cho tiện nếu việc đó làm giảm chất lượng đầu ra.

---

## 4. QUY TẮC ẢNH SECTION CỦA CODEX

Trong môi trường Codex, không nén quá nhiều section website vào một bức ảnh duy nhất khiến chữ, khoảng cách, nút bấm hoặc chi tiết bố cục trở nên quá nhỏ không thể phân tích chính xác.

Trong Codex, hãy ưu tiên các ảnh lớn riêng biệt cho từng section.

Quy tắc mặc định trong Codex:
- Yêu cầu 1 section → sinh 1 ảnh
- Yêu cầu 2 section → sinh 2 ảnh
- Yêu cầu 3 section → sinh 3 ảnh
- Yêu cầu 4 section → sinh 4 ảnh
- Yêu cầu 5 section → sinh 5 ảnh
- Yêu cầu 6 section → sinh 6 ảnh
- Yêu cầu 7 section → sinh 7 ảnh
- Yêu cầu 8 section → sinh 8 ảnh
- Yêu cầu 9 section → sinh 9 ảnh
- Yêu cầu 10 section → sinh 10 ảnh
- và tiếp tục như vậy khi phù hợp.

Quy trình này được ưu tiên vì:
- chữ luôn dễ đọc
- typography có thể phân tích được
- khoảng cách luôn hiển thị rõ
- các chi tiết nút bấm hiển thị rõ
- tỷ lệ bố cục hiển thị rõ
- chất lượng trích xuất thiết kế tốt hơn nhiều
- việc viết code frontend trung thực hơn

Không mặc định tạo ra:
- một bức ảnh ghép nhiều cột lộn xộn
- một bảng thiết kế dài nén chặt với chữ tí hon không đọc được
- một bức ảnh chứa quá nhiều section làm giảm chất lượng trích xuất thiết kế

Nếu cần thiết, hãy sinh nhiều ảnh hơn thay vì thu nhỏ mọi thứ lại.

Ngoài Codex, kỹ năng này vẫn có thể cho phép các bố cục nhiều section nhỏ gọn hơn khi phù hợp.
Trong Codex, hãy ưu tiên độ rõ nét của section và độ chính xác của việc trích xuất thiết kế.

---

## 5. QUY TẮC KHÔNG CẮT GHÉP ẢNH CŨ (NO CROP)

Khi một section cần một bức ảnh chuyên biệt hoặc một góc nhìn cận cảnh chi tiết, không chỉ đơn thuần là cắt, thu phóng hoặc trích xuất nó từ một bức ảnh lớn đã sinh trước đó.

Không được:
- cắt phần hero ra khỏi một bảng thiết kế toàn trang.
- cắt khu vực bảng giá ra khỏi một bố cục lớn hơn.
- cắt các thẻ card nhỏ ra khỏi một ảnh nhiều section.
- dựa vào các nét cắt thô từ ảnh cũ.
- sử dụng các mảnh ảnh cắt làm nguồn chính để viết code nếu chúng làm méo mó khoảng cách, tỷ lệ hoặc typography.

Thay vào đó:
- hãy sinh một bức ảnh mới tinh cho section đó.
- hãy sinh một bức ảnh cận cảnh mới tinh cho section đó.
- duy trì cùng một ngôn ngữ thiết kế, bảng màu, phong cách typography và họ component.
- làm cho bức ảnh mới được tối ưu hóa tốt nhất cho khả năng đọc và trích xuất chi tiết.

Lý do:
các ảnh cắt ghép thường phá hủy:
- độ chính xác của khoảng cách
- tỷ lệ kích thước chữ
- sự sạch sẽ của lề
- tỷ lệ bố cục
- độ rõ nét của nút bấm
- sự cân bằng của section
- độ trung thực của code frontend cuối cùng

Việc sinh ảnh mới cho từng section được ưu tiên hơn rất nhiều so với cắt ghép ảnh cũ.

---

## 6. QUY TẮC TÁI SINH ẢNH MỚI TINH (FRESH RE-GENERATION)

Nếu một section hoặc chi tiết thiết kế không đủ rõ ràng, hãy sinh lại nó dưới dạng một hình ảnh độc lập mới tinh.

Việc tái sinh ảnh này nên:
- bảo tồn cùng một ngôn ngữ trực quan của thiết kế tổng thể ban đầu
- giữ cùng bảng màu
- giữ cùng phong cách typography
- giữ cùng kiểu nút bấm
- giữ cùng logic bo góc
- giữ cùng cách xử lý hình ảnh
- giữ cùng thế giới thương hiệu tổng thể

Nhưng nó cũng cần:
- làm cho chữ to hơn và dễ đọc hơn
- làm cho khoảng cách hiển thị rõ ràng hơn
- làm cho nút bấm dễ kiểm tra hơn
- làm cho cấu trúc component dễ phân tích hơn
- làm cho tỷ lệ bố cục rõ ràng hơn
- làm cho section sạch sẽ hơn nếu lần render trước quá rối rắm

Đây không phải là một thiết kế khác.
Đây là một bản render sạch hơn, dễ phân tích hơn của cùng một hệ thống thiết kế cho section đó.

---

## 7. QUY TẮC ẢNH CHI TIẾT / TRÍCH XUẤT PHỤ (TÙY CHỌN)

Nếu ảnh section vẫn chưa hiển thị các chi tiết cần thiết đủ rõ ràng, hãy sinh thêm một ảnh chi tiết bổ sung cho chính section đó.

Ví dụ về các ảnh thứ cấp hữu ích:
- một bản render hero cận cảnh để đọc headline, subheadline, CTA và typography.
- một ảnh chi tiết cho các thẻ bảng giá.
- một bản render cận cảnh cho các đánh giá (testimonials).
- một bản render cận cảnh cho thanh điều hướng / navbar.
- một bản render cận cảnh cho các thẻ tính năng hoặc panel UI.
- một bản render cận cảnh cho chân trang hoặc section CTA.
- một biến thể tinh chỉnh của ảnh đã sinh đầu tiên giúp section dễ trích xuất chi tiết hơn.
- một ảnh sinh lại sạch hơn của cùng section đó với chữ lớn hơn để dễ trích xuất.
- một ảnh chủ yếu tập trung vào typography và spacing thay vì toàn bộ bố cục.

Các ảnh bổ sung này tồn tại để cải thiện chất lượng phân tích và trích xuất thiết kế.

Hãy sử dụng chúng khi cần thiết cho:
- chữ dễ đọc
- các trạng thái nút rõ ràng hơn
- phân tích khoảng cách chặt chẽ hơn
- kiểm tra thẻ card và component
- trích xuất màu sắc rõ ràng hơn
- quan sát typography tốt hơn
- viết code chính xác hơn

Đừng ngần ngại tạo ảnh thứ hai hoặc thứ ba tập trung vào trích xuất thiết kế cho một section nếu ảnh đầu tiên quá rộng.

---

## 8. TIÊU CHUẨN PHÂN TÍCH SẠCH SẼ

Hãy phân tích một cách sạch sẽ và có hệ thống.

Không phân tích hời hợt chỉ dựa trên vibe chung.
Không nhảy quá nhanh từ hình ảnh sang viết code.

Đối với mỗi ảnh section được sinh ra, hãy kiểm tra sạch sẽ:
- section đó là gì
- thứ tự ưu tiên trực quan là gì
- chữ nào có thể đọc được
- các mối quan hệ typography nào hiển thị rõ
- các mối quan hệ khoảng cách (spacing) nào hiển thị rõ
- các nút bấm và bộ điều khiển nào hiển thị rõ
- logic thẻ card hoặc khối hộp nào hiển thị rõ
- màu sắc nào chủ đạo
- nhịp điệu cấu trúc nào hiển thị rõ
- chi tiết nào vẫn chưa rõ ràng

Nếu có điều gì chưa rõ, hãy sinh một bức ảnh khác trước khi viết code.

Quá trình phân tích cần tạo cảm giác:
- điềm tĩnh
- có cấu trúc
- chính xác
- trung thực
- am hiểu thiết kế
- am hiểu việc triển khai code

---

## 9. YÊU CẦU PHÂN TÍCH SÂU SẮC ẢNH THIẾT KẾ

Trước khi viết bất kỳ code nào, hãy phân tích sâu sắc các hình ảnh đã sinh.

Không chỉ nhìn lướt qua chúng.
Treat them like a design specification.

Kiểm tra và trích xuất cẩn thận:
- chữ hiển thị chính xác ở những nơi đọc được
- nội dung tiêu đề chính (hero headline)
- nội dung tiêu đề phụ (subheadline)
- nhãn nút CTA
- tiêu đề các section
- đặc tính typography
- mối quan hệ tỷ lệ kích thước chữ (type scale)
- font mood
- số lượng dòng chữ
- hành vi ngắt dòng chữ
- logic căn lề
- khoảng cách các section
- khoảng cách nội bộ (internal spacing)
- padding và gutters
- kích thước và nhịp điệu của thẻ card
- logic bo góc (border radius)
- cách dùng nét vẽ / đường phân chia
- hình dáng nút bấm
- phân cấp nút bấm
- padding của nút
- style hover ngầm định nếu được gợi ý trực quan
- bảng màu sắc
- các màu nhấn
- cách xử lý nền background
- cách xử lý hình ảnh
- cách xử lý icon
- đổ bóng / logic chiều sâu
- logic lưới grid
- cấu trúc bố cục
- thứ tự sắp xếp section
- mật độ hiển thị của section
- nhịp điệu trực quan
- các họa tiết lặp lại định nghĩa ngôn ngữ thiết kế

Mục tiêu của bạn là hiểu chính xác tại sao website được sinh ra trông lại mạnh mẽ và đẹp mắt.

Chỉ sau bước phân tích sâu sắc này bạn mới bắt đầu viết code frontend.

---

## 10. QUY TRÌNH CODEX WEBSITE ƯU TIÊN ẢNH TRƯỚC (IMAGE-FIRST)

Khi kỹ năng này được sử dụng trong Codex hoặc bất kỳ môi trường nào hỗ trợ cả sinh ảnh và viết code, hãy mặc định áp dụng quy trình ưu tiên ảnh trước cho các tác vụ thiết kế website.

Trình tự thực thi được ưu tiên:
1. suy luận số lượng section
2. sinh các ảnh tham chiếu section trước tiên
3. sinh thêm các ảnh chi tiết/trích xuất ở những nơi cần thiết
4. nếu cần, sinh lại các section chưa rõ ràng dưới dạng các ảnh độc lập mới tinh
5. kiểm tra sâu sắc toàn bộ các hình ảnh đã sinh
6. trích xuất chữ, typography, khoảng cách, màu sắc, bố cục, nút bấm và logic component
7. viết code website để khớp với thiết kế đã sinh một cách sát nhất có thể
8. chỉ tự sáng tạo các chi tiết còn thiếu khi các bức ảnh để lại những điểm mơ hồ không thể làm rõ

Đối với các tác vụ frontend quan trọng về mặt trực quan, không bắt đầu bằng việc tự thiết kế tự do bằng code.
Hãy bắt đầu bằng việc tạo ra các hình ảnh tham chiếu trực quan trước tiên khi tính năng sinh ảnh khả dụng.

Hình ảnh là nguồn định hướng nghệ thuật chính.
Code là lớp triển khai thực tế.

---

## 11. KHI NÀO KÍCH HOẠT QUY TRÌNH ƯU TIÊN ẢNH TRƯỚC

Nếu tính năng sinh ảnh khả dụng, hãy ưu tiên sinh ảnh tham chiếu trước đối với các yêu cầu tập trung vào chất lượng trực quan của frontend.

Kích hoạt quy trình ưu tiên ảnh trước khi người dùng yêu cầu:
- một section hero đẹp mắt
- một landing page cao cấp
- một website sáng tạo
- một thiết kế lại (redesign)
- một website hiện đại hơn
- một giao diện có tính thẩm mỹ cao hơn
- một trang marketing bóng bẩy
- một trang portfolio
- một trang startup nơi gu thẩm mỹ trực quan cực kỳ quan trọng
- một ý tưởng website nhiều section
- bất cứ thứ gì được mô tả chủ yếu bằng các thuật ngữ trực quan, thẩm mỹ

Viết code trực tiếp trước chỉ được chấp nhận khi:
- tác vụ chủ yếu mang tính kỹ thuật logic
- người dùng muốn sửa một lỗi bug cụ thể
- người dùng đã cung cấp một hệ thống thiết kế chính xác
- tác vụ chủ yếu mang tính cấu trúc hơn là thẩm mỹ trực quan

---

## 12. CƠ CHẾ BIẾN THIÊN KẾT HỢP

Để tránh các đầu ra trông giống như AI rập khuôn, hãy chọn một sự kết hợp phong cách mạnh mẽ và thực hiện nó một cách nhất quán.

Không trộn lẫn mọi thứ thành một sự hỗn loạn.
Hãy chọn một định hướng trực quan đồng nhất và thực thi nó rõ ràng.

### Phong cách Chủ đề (Theme Paradigm)
Chọn 1:
1. Pristine Light Mode (Chế độ sáng tinh khiết)
2. Deep Dark Mode (Chế độ tối sâu thẳm)
3. Bold Studio Solid (Màu trơn studio táo bạo)
4. Quiet Premium Neutral (Màu trung tính cao cấp tĩnh lặng)

### Đặc tính Nền (Background Character)
Chọn 1:
1. lưới kỹ thuật mờ tinh tế / dotted field
2. nền màu trơn với gradient môi trường mềm mại tạo chiều sâu
3. ảnh phủ tràn màn hình cinematic (full-bleed)
4. chất liệu texture bề mặt xúc giác

### Đặc tính Typography
Chọn 1:
1. grotesk sạch sẽ
2. grotesk tinh tế
3. display biểu cảm mạnh
4. typography tuyên ngôn nén chặt
5. kết hợp serif phong cách tạp chí + sans
6. phân cấp Swiss rational

### Kiến trúc Hero
Chọn 1:
1. tối giản căn giữa phong cách cinematic
2. hero bất đối xứng chia đôi
3. bố cục ảnh polaroid bay tán xạ
4. tiêu đề khổng lồ tích hợp ảnh lồng trong chữ (inline typography)
5. bố cục offset phong cách biên tập
6. hero thiên về ảnh lớn với chữ được tiết chế tối đa

### Hệ thống Section
Chọn 1:
1. nhịp điệu bento mô-đun
2. các khối biên tập xen kẽ
3. kể chuyện xếp chồng phong cách poster
4. nhịp điệu dẫn dắt bởi thư viện ảnh
5. kỷ luật lưới Swiss grid
6. luồng marketing cao cấp bất đối xứng

### Bộ Component Đặc trưng
Chọn chính xác 4 component độc đáo:
- lưới masonry vuông so le chéo
- chồng thẻ card cascade 3D
- bố cục accordion trượt ngang
- lưới bento không khoảng trống tinh khiết
- dải chữ chạy vô tận (marquee strip)
- vòng cung ảnh polaroid xoay
- các đường kẻ nhịp điệu dọc
- bố cục biên tập lệch lưới (off-grid)
- chồng panel UI sản phẩm
- bức tường trích dẫn testimonial chia đôi
- các khung ảnh cắt xếp lớp

### Ngôn ngữ Gợi ý Chuyển động (Motion-Implied)
Chọn chính xác 2:
- hiệu ứng hé lộ chữ scrub
- hiệu ứng ghim kể chuyện theo tiến trình cuộn
- hiệu ứng xuất hiện so le từ dưới lên (float-up)
- hiệu ứng trượt ảnh parallax
- hiệu ứng mở rộng accordion mượt mà
- hiệu ứng mờ dần cinematic (fade-through)

Đây không phải là hướng dẫn viết code.
Đây là các tín hiệu định hướng trực quan mà thiết kế của bạn cần gợi ý ra.

---

## 13. QUY TẮC THAM CHIẾU WEBSITE

Mọi ảnh section website được sinh ra phải truyền tải rõ ràng:
- bố cục (layout)
- hệ thống phân cấp (hierarchy)
- khoảng cách (spacing)
- kích thước chữ (typography scale)
- độ ưu tiên của CTA
- định dạng component
- cách xử lý hình ảnh
- hệ thống thiết kế tổng thể

Một lập trình viên hoặc mô hình viết code phải có thể nhìn vào các ảnh đó và hiểu rõ cách xây dựng website.

Không tạo ra các tác phẩm nghệ thuật trừu tượng mơ hồ khi yêu cầu là về frontend.
Mặc định tạo ra các bản thiết kế section thực tế.

---

## 14. QUY TẮC TỐI GIẢN HERO

Phần hero phải mang tính cinematic, rõ ràng và có tính toán.

### Các quy tắc Hero Tuyệt đối:
- hero phải tạo cảm giác như một cảnh mở đầu mạnh mẽ
- giữ bố cục hero cực kỳ sạch sẽ
- không làm chật chội màn hình đầu tiên (above-the-fold)
- tiêu đề chính phải ngắn gọn và mạnh mẽ
- tiêu đề chính của hero lý tưởng nhất là nằm trong khoảng 1–3 dòng
- không để tiêu đề hero dài bị ngắt thành quá nhiều dòng
- nếu tiêu đề bắt đầu quá dài, hãy lược bớt từ ngữ thay vì ép nó hiển thị trên nhiều dòng
- giữ văn bản mô tả ngắn gọn
- ưu tiên khoảng trống (negative space) và độ tương phản
- tránh nhồi nhét hero với các nút badge, số liệu giả, logo nhỏ và các chi tiết vô nghĩa
- tránh các nhãn phụ, thẻ điều khiển, ký hiệu hệ thống hoặc văn bản trang trí không giúp ích thực tế cho hero
- giữ màn hình đầu tiên dễ đọc trên laptop nhỏ mà không bị cảm giác quá đầy

### Quy tắc Sạch sẽ của Hero:
Hero cần tạo cảm giác yên bình, cao cấp và dễ đọc lập tức.

Nên làm:
- sử dụng một điểm nhấn tập trung mạnh mẽ duy nhất
- giữ hệ thống phân cấp trực quan rõ ràng
- để hero được hít thở
- giữ hệ thống trực quan chặt chẽ và được kiểm soát
- làm cho màn hình đầu tiên trông tinh tế và có chủ đích
- tiết chế lượng nội dung hiển thị để hero vẫn thanh lịch trên laptop nhỏ

Không nên:
- làm lộn xộn hero
- tạo ra nhiều điểm nhấn trực quan cạnh tranh nhau
- nhồi nhét hero với các thẻ card hoặc chi tiết vụn vặt
- làm hero trở nên ồn ào hoặc rối mắt
- thêm các nhãn không cần thiết kiểu “00 orchestration layer” hoặc các văn bản trang trí nếu nó không mang lại giá trị thật

### Quy tắc Tiêu đề (Headline):
Ưu tiên cao:
- 1 dòng nếu có thể
- 2 dòng rất tốt
- 3 dòng là tối đa trong các trường hợp thông thường

Cần tránh:
- tiêu đề hero từ 4 dòng trở lên
- nội dung mô tả dài dòng như một đoạn văn trong hero
- độ tương phản yếu giữa tiêu đề chính và tiêu đề phụ

---

## 15. QUY TẮC HIỂN THỊ ĐÁP ỨNG MÀN HÌNH ĐẦU TIÊN

Khung nhìn đầu tiên của website phải sạch sẽ và có thể sử dụng được trên một chiếc laptop nhỏ.

Điều này có nghĩa là:
- không quá tải vùng màn hình đầu tiên (above the fold)
- không ép quá nhiều khối nội dung vào viewport hero
- không dựa vào các tấm panel lớn lồng nhau chiếm dụng không gian mà không tăng thêm độ rõ ràng
- làm cho section đầu tiên tạo cảm giác được sắp xếp có chủ đích, không bị nhồi nhét quá mức

Phần hero và vùng xem đầu tiên cần:
- hiển thị thông điệp chính rõ ràng
- hiển thị nút CTA chính rõ ràng
- hiển thị hình ảnh/asset chính rõ ràng
- tránh việc cố gắng trưng bày toàn bộ sản phẩm trong một góc nhìn chật chội đầu tiên

Một chiếc laptop nhỏ vẫn cần nhìn thấy rõ:
- một tiêu đề rõ ràng
- văn bản mô tả dễ đọc
- khoảng cách sạch sẽ
- một nút CTA hiển thị rõ
- một điểm nhấn trực quan cân bằng, đáng tin cậy

---

## 16. QUY TẮC CHỐNG LẠM DỤNG HỘP LỒNG NHAU (ANTI-NESTED-BOX)

Không mặc định thiết kế theo kiểu bố cục hộp trong hộp trong hộp.

Cần tránh:
- các container section bo tròn khổng lồ bao bọc mọi thứ
- thẻ card lồng trong thẻ card lớn hơn lồng trong thẻ card ngoài cùng
- xếp chồng các ngăn kiểu dashboard không có lý do
- giao diện UI dạng hộp lồng nhau tạo cảm giác bố cục bị giam cầm
- các section chỉ là một panel viền lớn chứa thêm các panel viền khác chứa thêm các panel viền khác nữa

Chỉ sử dụng hộp khung viền khi chúng có mục đích rõ ràng.

Ưu tiên:
- bố cục mở (open layouts)
- khoảng trắng thoáng đãng hơn
- ít container hơn nhưng chất lượng hơn
- phân cấp phẳng hơn ở những nơi phù hợp
- căn lề và khoảng cách trực tiếp thay vì bao bọc quá mức
- một khung viền chính thay vì quá nhiều khung xếp lớp

Một section không nên tạo cảm giác như một nhà tù của các container.
Nó phải tạo cảm giác được thiết kế thoáng, cởi mở và có chủ đích.

---

## 17. QUY TẮC GIẢM THIỂU SỰ LỘN XỘN CỦA MICRO-UI

Không làm rối thiết kế bằng các chi tiết UI phụ tí hon không giúp ích thực tế cho độ rõ ràng của trang.

Cần tránh:
- các nút pill không cần thiết
- các ký hiệu giả lập hệ thống
- các nhãn điều khiển giả
- các nhãn tag trang trí dạng code
- các hàng metadata nhỏ vô nghĩa
- các chip màu trang trí
- các badge nhỏ ở khắp mọi nơi
- các thuật ngữ giả dashboard
- các nhãn được thiết kế quá mức làm xao nhãng bố cục chính

Ví dụ về các điều cần tránh trừ khi thực sự cần thiết:
- “00 orchestration layer”
- các chip trạng thái kỹ thuật tí hon
- các chỉ thị runtime trang trí
- các văn bản enterprise giả quá cụ thể
- các nhãn điều khiển/vận hành chỉ tồn tại để trông có vẻ phức tạp

Ưu tiên:
- tiêu đề sạch sẽ hơn
- ít nhãn mác hơn
- phân cấp thực tế
- khoảng cách rõ ràng hơn
- văn bản mô tả đơn giản hơn
- typography mạnh mẽ thay vì các chi tiết trang trí lộn xộn

---

## 18. QUY TẮC SINH ẢNH THEO SECTION

Trong Codex, hãy coi mỗi section là một đơn vị phân tích độc lập.

Nếu người dùng yêu cầu:
- chỉ 1 hero → sinh 1 ảnh hero
- 4 section → sinh 4 ảnh section tương ứng
- 8 section → sinh 8 ảnh section tương ứng
- 12 section → sinh 12 ảnh section tương ứng khi phù hợp.

Ưu tiên chung:
- một section = một bức ảnh chính
- một section phức tạp = một ảnh chính + một hoặc nhiều ảnh chi tiết tùy chọn
- một section chưa rõ ràng = sinh lại nó dưới dạng ảnh độc lập mới tinh sạch sẽ

Quy tắc sinh ảnh theo section này tồn tại để ngăn chặn:
- chữ tí hon không đọc được
- các nút bấm quá nhỏ
- khoảng cách không rõ ràng
- chất lượng trích xuất thiết kế yếu
- dịch dịch code bị mất chi tiết

---

## 19. QUY TẮC HỆ THỐNG ẢNH CỦA WEBSITE

Khi tạo thiết kế website, không chỉ nghĩ về toàn bộ trang web mà còn phải nghĩ về hệ thống hình ảnh nội bộ được sử dụng bên trong chính website đó.

Điều này có thể bao gồm:
- media của phần hero
- các hình ảnh trong section
- ảnh cắt phong cách tạp chí
- hình ảnh sản phẩm
- ảnh chụp có khung viền
- các thẻ card ảnh xếp lớp
- các khối dạng gallery
- các panel hình ảnh hỗ trợ trực quan

Nếu website được hưởng lợi từ nhiều hình ảnh, hãy đưa vào nhiều khoảnh khắc hình ảnh khác nhau trên trang.

Quy tắc:
- việc sử dụng hình ảnh phải tạo cảm giác có chủ đích
- số lượng hình ảnh phải khớp với độ phức tạp của trang
- không chỉ dựa vào một ảnh hero duy nhất nếu nhiều section khác cần hỗ trợ trực quan
- giữ việc dùng ảnh cân bằng và sạch sẽ
- tất cả hình ảnh sử dụng vẫn phải tạo cảm giác thuộc về một thế giới thiết kế đồng nhất

---

## 20. QUY TẮC KHUNG MEDIA CỐ ĐỊNH

Các hình ảnh bên trong website thông thường nên nằm trong các khung viền được kiểm soát, rõ ràng và dễ viết code.

Ưu tiên:
- các khối media có tỷ lệ khung hình cố định (fixed-aspect)
- các vùng ảnh được đóng khung rõ ràng
- các mô-đun media có thể lặp lại
- logic bo góc đồng nhất
- tỷ lệ trực quan ổn định giữa các section tương tự

Ví dụ:
- ảnh hero nằm trong một khung lớn được giới hạn rõ ràng
- ảnh phong cách tạp chí sử dụng tỷ lệ dọc hoặc ngang có thể lặp lại
- ảnh thẻ card với tỷ lệ đồng nhất
- các khối gallery với tỷ lệ khung hình được kiểm soát
- ảnh sản phẩm được đặt trong các container có chủ đích và ổn định

Cần tránh:
- kích thước ảnh ngẫu nhiên không có hệ thống
- tỷ lệ không đồng nhất giữa các mô-đun tương tự
- co giãn lộn xộn
- sự hỗn loạn của việc ghép ảnh (collage) trừ khi được yêu cầu cụ thể

Mục tiêu là:
- hình ảnh trực quan mạnh mẽ
- nằm trong một hệ thống mà mô hình frontend có thể xây dựng lại một cách thực tế

---

## 21. QUY TẮC TRÍCH XUẤT VĂN BẢN (TEXT EXTRACTION)

Khi văn bản có thể đọc được trong ảnh section đã sinh, hãy trích xuất và sử dụng nó.

Đặc biệt kiểm tra và trích xuất:
- tiêu đề chính hero
- tiêu đề phụ hero
- nhãn nút CTA
- tiêu đề section
- nhãn bảng giá
- tên tính năng
- tên và vai trò trong testimonial nếu hiển thị rõ
- các nhãn trên navbar
- các nhãn ở chân trang (footer) nếu có liên quan

Nếu chữ quá nhỏ không thể trích xuất đáng tin cậy:
- hãy sinh một ảnh chi tiết cận cảnh hơn
- hoặc sinh một phiên bản thứ hai rõ ràng hơn của section đó

Không bỏ qua việc trích xuất văn bản.
Văn bản hiển thị là một phần của hệ thống thiết kế và phải ảnh hưởng đến việc viết code.

---

## 22. QUY TẮC TRÍCH XUẤT TYPOGRAPHY

Không chỉ nhận xét rằng typography "trông đẹp".
Hãy phân tích nó một cách chuẩn xác.

Trích xuất và quan sát:
- mối quan hệ kích thước (size)
- mối quan hệ độ dày nét (weight)
- số lượng dòng chữ
- cảm giác chiều cao dòng (line height)
- cảm giác khoảng cách chữ (tracking)
- hành vi font serif so với sans-serif
- sự tương phản giữa display và body text
- nhịp điệu tiêu đề section
- tỷ lệ chữ của nút CTA
- thiết kế đang sử dụng kiểu chữ tĩnh lặng hay táo bạo

Áp dụng các phát hiện này trong quá trình viết code.
Không làm phẳng typography thành một hệ thống phân cấp code chung chung, đơn điệu.

---

## 23. QUY TẮC TRÍCH XUẤT KHOẢNG CÁCH (SPACING EXTRACTION)

Phân tích khoảng cách một cách có chủ đích.

Kiểm tra:
- khoảng cách giữa tiêu đề chính và tiêu đề phụ
- khoảng cách giữa chữ và các nút bấm
- khoảng cách giữa các thẻ card
- khoảng cách lề trên và dưới của section
- lề hai bên (side gutters)
- padding của thẻ card
- khoảng cách từ ảnh đến chữ
- khoảng cách của navbar
- khoảng cách của khối CTA
- nhịp điệu spacing tổng thể trên toàn trang

Mục tiêu không phải là đo chính xác từng pixel bằng OCR.
Mục tiêu là hiểu và tái hiện logic khoảng cách một cách trung thực.

Không thu hẹp code thành các khoảng cách chật chội mặc định nếu thiết kế được sinh ra thoáng đãng và rộng rãi hơn.

---

## 24. QUY TẮC TRÍCH XUẤT NÚT / COMPONENT

Nút bấm và các component phải được phân tích kỹ, không được đoán mò.

Kiểm tra:
- kích thước nút
- hình dáng nút
- bo góc nút
- hành vi tô màu nền so với viền rỗng (fill vs outline)
- cách dùng icon
- trạng thái hover ngầm định
- phân cấp nút chính so với nút phụ
- cấu trúc thẻ card
- cách dùng badge
- đường kẻ phân chia (dividers)
- đổ bóng (shadows)
- đường viền (borders)
- logic viên thuốc (pills)
- style ô nhập liệu nếu có

Nếu chi tiết nút hoặc thẻ card quá nhỏ, hãy sinh ảnh cận cảnh hơn.

---

## 25. QUY TẮC TRÍCH XUẤT MÀU SẮC

Chủ động phân tích và trích xuất màu sắc từ các hình ảnh được tạo ra.

Kiểm tra:
- màu nền background
- màu các panel
- các màu nhấn
- màu nền nút bấm
- phân cấp màu chữ
- logic màu viền
- tone màu đổ bóng
- bộ lọc màu / độ tương phản của ảnh
- sự tiết chế hay đậm đà của gradient

Website được triển khai code phải bảo tồn logic màu sắc ban đầu một cách sát nhất có thể.

Không thay thế bảng màu được thiết kế chu đáo bằng các màu sắc web mặc định chung chung.

---

## 26. KỶ LUẬT SAO CHÉP TỪ THIẾT KẾ SANG CODE (COPY-ORIENTED)

Sau khi sinh ảnh và phân tích tài liệu tham chiếu, hãy triển khai code website theo cách hướng tới sao chép trung thực.

Điều này có nghĩa là:
- bám sát tài liệu tham chiếu hình ảnh
- bảo tồn logic bố cục
- bảo tồn nhịp điệu khoảng cách
- bảo tồn thứ tự sắp xếp section
- bảo tồn sự cân bằng giữa chữ và ảnh
- bảo tồn phong cách typography
- bảo tồn style component
- bảo tồn sự sạch sẽ trực quan tổng thể

Không đi lệch sang một hướng thiết kế khác trong quá trình viết code.
Không tự ý "cải tiến" thiết kế bằng cách thay thế nó bằng một bố cục code mặc định chung chung.

Mục tiêu không phải là:
- lấy cảm hứng từ hình ảnh

Mục tiêu là:
- trung thực về mặt trực quan với hình ảnh, chuyển dịch chuẩn xác thành code frontend thật

---

## 27. QUY TẮC CHỐNG LỆCH THIẾT KẾ (ANTI-DRIFT)

Một lỗi phổ biến là lệch thiết kế (design drift):
hình ảnh đã sinh trông rất mạnh mẽ, nhưng code kết quả lại trở nên chung chung, rập khuôn.

Hãy tránh tuyệt đối điều này.

Trong quá trình viết code:
- không đơn giản hóa thành các template mặc định.
- không thay thế các section độc đáo bằng các hàng cột chung chung.
- không nén khoảng cách thoáng rộng thành bố cục chật chội.
- không thay thế typography mạnh mẽ bằng hệ thống phân cấp chữ thông thường.
- không lược bỏ bản sắc trực quan của trang cho tiện.
- không gộp logic section thành các mẫu lặp đi lặp lại không có trong ảnh gốc.
- không đưa lại các cấu trúc hộp lồng nhau phức tạp đã bị lược bỏ trong quá trình phân tích.

Code kết quả cuối cùng phải tạo cảm giác như chính là website trong ảnh tham chiếu.

---

## 28. GIẢI QUYẾT CÁC CHI TIẾT CÒN THIẾU

Khi viết code từ hình ảnh, một số chi tiết có thể vẫn chưa rõ ràng.

Hãy giải quyết sự mơ hồ theo thứ tự ưu tiên sau:
1. bảo tồn ngôn ngữ thiết kế hiển thị rõ
2. bảo tồn logic bố cục và khoảng cách
3. bảo tồn họ component dùng chung
4. bảo tồn tone màu và độ tinh tế
5. sinh thêm ảnh chi tiết cận cảnh nếu cần
6. sinh lại section dưới dạng ảnh độc lập mới tinh nếu cần
7. chỉ sau đó mới chọn phương án viết code trung thực và dễ triển khai nhất

Không vội vàng lấp đầy các khoảng mơ hồ bằng các thiết kế mặc định chung chung.

---

## 29. CÁC QUY TẮC CHỐNG RẬP KHUÔN (ANTI-AI-SLOP)

Tránh nghiêm ngặt các mẫu thiết kế này trừ khi được yêu cầu cụ thể.

### Bố cục rập khuôn (Layout slop)
- một bức ảnh ghép khổng lồ không đọc được chữ
- các section căn giữa lặp đi lặp lại vô tận
- các hàng thẻ card giống hệt nhau lặp lại hết section này đến section khác
- các khối chữ bên trái/ảnh bên phải rập khuôn
- sự phức tạp giả tạo thiếu phân cấp thông tin
- khoảng trống trang trí không có mục đích
- thẻ card lồng trong thẻ card lồng trong thẻ card
- các section wrapper lớn bo tròn bao bọc mọi thứ
- khung viền dashboard quá phức tạp không cần thiết

### Trực quan rập khuôn (Visual slop)
- dải màu gradient tím/xanh mặc định của AI
- quá nhiều viền phát sáng nhẹ
- các hình blob lơ lửng ở khắp nơi
- hiệu ứng glassmorphic xếp chồng không có lý do
- các chi tiết tương lai ngẫu nhiên không có cấu trúc
- noise được render quá mức che lấp bố cục sạch

### Typography rập khuôn (Typography slop)
- tiêu đề lớn + mô tả nhỏ yếu ớt
- quá nhiều kiểu font chữ chọi nhau
- ngắt dòng chữ cẩu thả
- lười biếng viết hoa toàn bộ (all-caps) ở mọi nơi
- thủ thuật chữ gradient trên tiêu đề lớn

### Nội dung sáo rỗng (Content slop)
Tránh các văn phong tiếp thị sáo rỗng của AI như:
- unleash (giải phóng)
- elevate (nâng tầm)
- revolutionize (cách mạng hóa)
- next-gen (thế hệ mới)
- seamless (liền mạch)
- transformative platform (nền tảng chuyển đổi)

Tránh tên thương hiệu giả chung chung:
- Acme
- Nexus
- Flowbit
- Quantumly
- NovaCore

Tránh sự phức tạp giả tạo:
- nhãn điều khiển enterprise giả
- ký hiệu hệ thống trang trí
- chữ trạng thái vi mô rác
- các thuật ngữ giả lập operator / runtime / orchestration trừ khi thực sự cốt lõi của thương hiệu

### Mật độ hiển thị rập khuôn (Density slop)
- các section nhồi nhét quá nhiều
- quá tải thẻ card
- khoảng cách quá nhỏ giữa các section chính
- các bức tường nội dung gây kiệt quệ trực quan

---

## 30. KỶ LUẬT ƯU TIÊN TYPOGRAPHY

Typography là vật liệu thiết kế chính.

Luôn đảm bảo:
- tương phản kích thước chữ rõ ràng
- thứ tự đọc tự nhiên, rõ ràng
- các khoảnh khắc display mạnh mẽ
- văn bản mô tả dễ đọc
- copy ngắn gọn
- tiêu đề section củng cố cấu trúc trang

Đối với định hướng tạp chí/biên tập:
- hãy để typography định hình bố cục trang

Đối với định hướng kỹ thuật/sản phẩm:
- hãy để typography truyền tải sự tin cậy và chính xác

---

## 31. QUY TẮC NHỊP ĐIỆU SECTION

Một website cao cấp không tạo cảm giác như một khối thiết kế được lặp lại mãi mãi.

Hãy thay đổi nhịp điệu của các section dọc theo trang bằng cách thay đổi:
- mật độ hiển thị
- tỷ lệ ảnh so với chữ
- căn lề
- kích thước tỷ lệ
- khoảng trắng
- cách gom nhóm thẻ card
- cường độ màu nền background
- nhịp độ trực quan

Nhưng:
- giữ trang web đồng nhất
- kiểm soát khoảng cách spacing
- tránh các thay đổi đột ngột vô lý
- giữ mỗi section đủ sạch để phân tích tốt

---

## 32. KỶ LUẬT MẬT ĐỘ & KHOẢNG CÁCH (SPACING)

Không làm website quá chật chội.

Trang web cần được hít thở.

Quy tắc:
- sử dụng khoảng cách section đồng đều
- kiểm soát khoảng cách giữa các section chính
- cho phép khoảng trắng tạo ra sự tĩnh lặng
- tránh tình trạng section này quá chật trong khi section tiếp theo lại quá trống
- các section nhỏ vẫn cần đủ không gian xung quanh
- ưu tiên khoảng cách rộng rãi dễ phân tích hơn các bố cục nén chặt
- không lấp đầy mọi vùng trống bằng các UI phụ
- hãy để sự tối giản thực hiện một phần công việc thiết kế

Một website cao cấp cần tạo cảm giác:
- mở
- được sắp xếp chu đáo
- cân bằng
- tự tin
- dễ thở

Không:
- chật chội
- ồn ào
- không đều
- quá đầy
- kiệt quệ trực quan

---

## 33. CÁC BỘ SECTION MẶC ĐỊNH

### Bộ 4 section
1. Hero
2. Features (Tính năng)
3. Social proof / testimonial (Đánh giá khách hàng)
4. CTA (Nút hành động)

### Bộ 8 section
1. Hero
2. Trust bar (Logo đối tác)
3. Features
4. Product showcase (Trưng bày sản phẩm)
5. Benefits / use cases (Lợi ích / Trường hợp sử dụng)
6. Testimonials
7. Pricing (Bảng giá)
8. CTA

### Bộ 12 section
1. Hero
2. Trust bar
3. Feature grid (Lưới tính năng)
4. Product preview (Xem trước sản phẩm)
5. Problem / solution (Vấn đề / Giải pháp)
6. Benefits
7. Workflow (Quy trình làm việc)
8. Metrics / proof / integration (Số liệu chứng minh / Tích hợp)
9. Testimonials
10. Pricing
11. FAQ (Câu hỏi thường gặp)
12. CTA + footer (Chân trang)

Trong Codex, các phần này thông thường nên được tách thành các ảnh độc lập cho từng section, không nén chung thành một trang dài.

---

## 34. QUY TẮC ĐỒNG NHẤT NHIỀU ẢNH

Đối với các website sử dụng nhiều hình ảnh, hãy bắt buộc:
- cùng một thế giới thương hiệu
- cùng logic kích thước chữ (type scale)
- cùng kỷ luật khoảng cách spacing
- cùng định dạng style CTA
- cùng phong cách icon
- cùng cách xử lý hình ảnh
- cùng ngôn ngữ sắc độ
- cùng họ component dùng chung

Ảnh số 2, 3, hay 8 không được phép lệch hướng sang một website khác.

---

## 35. KIỂM TRA ĐỘ RÕ RÀNG (CLARITY CHECK)

Trước khi hoàn thiện, hãy tự kiểm tra nội bộ:

1. Thiết kế đã được sinh ảnh trước tiên chưa?
2. Tất cả ảnh đã sinh đã được phân tích sâu sắc chưa?
3. Chữ trong ảnh có đủ rõ để đọc chưa?
4. Nếu chưa, các ảnh chi tiết cận cảnh đã được sinh chưa?
5. Số lượng ảnh đã sinh có đủ chưa, hay bị giới hạn do lười biếng?
6. Các section chưa rõ ràng đã được sinh lại độc lập thay vì cắt ghép ảnh cũ chưa?
7. Hệ thống phân cấp trực quan có rõ ràng không?
8. Phần hero có đủ sạch sẽ không?
9. Typography đã được phân tích chuẩn xác chưa?
10. Mối quan hệ khoảng cách đã được hiểu rõ chưa?
11. Nút bấm và component đã được trích xuất kỹ chưa?
12. Màu sắc đã được phân tích chuẩn xác chưa?
13. Thiết kế có tính độc đáo trực quan không?
14. Thiết kế có sạch bóng các dấu hiệu AI rập khuôn không?
15. Lập trình viên có thể viết code trung thực từ tài liệu này không?
16. Nếu có nhiều ảnh, chúng có tạo cảm giác thuộc về một trang web duy nhất không?
17. Codex đã tránh việc nén quá nhiều section vào một ảnh nhỏ chưa?
18. Quá trình phân tích có sạch sẽ, có cấu trúc và cụ thể không?
19. Các hộp lồng nhau không cần thiết đã được lược bỏ chưa?
20. Khung nhìn đầu tiên có sạch và dễ đọc trên laptop nhỏ không?
21. Các chi tiết rác như pill, nhãn phụ, thuật ngữ kỹ thuật giả đã được giảm thiểu chưa?

Nếu chưa đạt, hãy tinh chỉnh lại trước khi xuất đầu ra.

---

## 36. HÀNG VI PHẢN HỒI

Khi người dùng yêu cầu thiết kế website trong quy trình image-to-code:
1. suy luận loại trang web
2. suy luận số lượng section
3. nếu tính năng sinh ảnh khả dụng và chất lượng trực quan là cốt lõi, hãy sinh ảnh thiết kế trước tiên
4. trong Codex, ưu tiên một ảnh lớn cho từng section
5. sinh thêm các ảnh chi tiết/trích xuất nếu chữ hoặc component quá nhỏ
6. sinh thêm ảnh bất cứ khi nào việc đó giúp cải thiện khả năng đọc hoặc chất lượng trích xuất thiết kế
7. không lười biếng giới hạn số lượng ảnh
8. không cắt ghép ảnh cũ để trích xuất section
9. sinh lại các section dưới dạng ảnh độc lập mới tinh khi cần thiết
10. chọn một sự kết hợp trực quan mạnh mẽ
11. chọn 4 component đặc trưng
12. chọn 2 gợi ý chuyển động (motion-implied)
13. thực thi sự sạch sẽ của hero và số lượng dòng chữ hero ngắn
14. giảm thiểu tối đa các chip màu, nhãn tag và sự lộn xộn vi mô của UI
15. tránh thẻ card lồng nhau và các wrapper hộp bao bọc section quá lớn
16. giữ màn hình đầu tiên dễ đọc và cân bằng trên laptop nhỏ
17. thực thi việc sử dụng hình ảnh mạnh mẽ khi phù hợp
18. giữ khoảng cách spacing rộng rãi, đồng đều và dễ phân tích
19. phân tích sâu sắc và sạch sẽ toàn bộ các hình ảnh đã sinh
20. trích xuất chữ, typography, khoảng cách, nút bấm, màu sắc, component và logic bố cục
21. viết code website để khớp với các ảnh tham chiếu một cách sát nhất có thể
22. chỉ tạo các file code cuối cùng sau khi hoàn thành bước phân tích đầy đủ

Không hỏi các câu hỏi làm rõ không cần thiết nếu có thể tự diễn giải một cách mạnh mẽ.
Không bắt đầu bằng viết code tự do khi vấn đề trực quan rõ ràng nên được giải quyết bằng việc sinh ảnh trước tiên.
Trong Codex, không nén nhiều section vào một ảnh không đọc được chữ.
Không cắt ghép ảnh cũ khi việc sinh một ảnh mới chuyên biệt cho section đó giúp bảo tồn khoảng cách, bố cục và khả năng đọc tốt hơn.

---

## 37. VÍ DỤ DIỄN GIẢI

### Ví dụ 1
User:
“make me one hero section for an AI startup”

Diễn giải:
- sinh 1 ảnh hero
- nếu cần, sinh thêm 1 ảnh chi tiết cận cảnh để đọc chữ/nút bấm
- không cắt ghép từ một bảng thiết kế lớn hơn
- nếu cần rõ hơn, sinh lại hero dưới dạng một ảnh độc lập sạch sẽ mới tinh
- giữ hero yên bình và dễ đọc
- tránh các nhãn tiện ích giả và các thẻ card lồng nhau
- phân tích headline, subheadline, CTA, spacing, màu sắc, hero media
- sau đó viết code triển khai hero

### Ví dụ 2
User:
“design me an 8-section landing page”

Diễn giải:
- sinh 8 ảnh section riêng biệt trong Codex
- mỗi ảnh cho một section
- sinh thêm các ảnh chi tiết cận cảnh khi cần thiết
- phân tích sâu sắc toàn bộ 8 section
- trích xuất chữ, typography, khoảng cách, nút bấm, màu sắc, thẻ card, cấu trúc
- nếu một section chưa rõ, sinh lại section đó mới tinh sạch sẽ thay vì cắt ghép ảnh cũ
- giữ các section thoáng đãng và không lạm dụng đóng hộp
- sau đó viết code triển khai toàn bộ trang web từ các tài liệu tham chiếu đó

### Ví dụ 3
User:
“make a premium creative agency website with 4 sections”

Diễn giải:
- sinh 4 ảnh section riêng biệt trong Codex
- giữ hero cực kỳ sạch sẽ
- đảm bảo chữ đọc được rõ ràng
- phân tích sâu sắc từng section
- không dùng các nét cắt thô từ ảnh cũ
- sinh lại các ảnh section sạch hơn nếu cần
- tránh lạm dụng chữ nhỏ và lạm dụng container
- sau đó viết code triển khai trang web từ 4 tài liệu tham chiếu đó

---

## 38. MỤC TIÊU CUỐI CÙNG

Tạo ra các hình ảnh tham chiếu website tạo cảm giác:
- cao cấp (premium)
- được định hướng nghệ thuật tốt (art-directed)
- rõ ràng (clear)
- có cấu trúc (structured)
- dễ đọc (readable)
- dễ phân tích (analyzable)
- dễ nhớ (memorable)
- chống rập khuôn (anti-generic)
- dễ viết code triển khai (implementation-friendly)

Đối với các tác vụ website trực quan, kỹ năng này bắt buộc phải tự sinh ảnh thiết kế trước, sau đó phân tích sâu sắc và sạch sẽ các ảnh đã sinh, dùng chúng làm nguồn trực quan chính, sau đó viết code frontend khớp sát với chúng.

Trong Codex, nếu người dùng muốn nhiều section, hãy ưu tiên các ảnh section lớn riêng biệt thay vì nén chung vào một bảng thiết kế nhỏ, để chữ, khoảng cách, typography, nút bấm và màu sắc có thể được trích xuất chuẩn xác.

Nếu một section cần rõ ràng hơn, hãy sinh thêm một ảnh chi tiết tập trung vào trích xuất cho section đó.

Nếu việc sinh thêm ảnh nâng cao chất lượng trực quan, hãy sinh thêm ảnh.
Không lười biếng giới hạn số lượng ảnh.

Không cắt ghép các ảnh đã sinh trước đó khi việc sinh một ảnh mới chuyên biệt cho section đó giúp bảo tồn khoảng cách, bố cục và khả năng đọc tốt hơn.
Hãy sinh ảnh mới tinh thay thế.

Tránh thẻ card lồng nhau.
Tránh các hộp bao bọc section quá lớn.
Tránh lạm dụng nhãn mác rác và thuật ngữ giả kỹ thuật.
Giữ phần hero đặc biệt sạch sẽ, thoáng đãng, được tiết chế và dễ đọc trên laptop nhỏ.

Kết quả phải:
- mạnh mẽ dưới dạng các ảnh section
- mạnh mẽ dưới dạng một hệ thống thiết kế
- mạnh mẽ dưới sự phân tích sâu sắc
- và mạnh mẽ dưới dạng code frontend được triển khai thực tế

Sản phẩm cuối cùng phải trông giống như một ý tưởng website hàng đầu được chuyển dịch một cách trung thực thành code thật, chứ không phải một bảng thiết kế tí hon không đọc được chữ hay một bản viết code mặc định chung chung.
