---
name: redesign-existing-projects
description: Nâng cấp các website và ứng dụng hiện có lên chất lượng cao cấp. Kiểm tra thiết kế hiện tại, phát hiện các phong cách AI rập khuôn và áp dụng các tiêu chuẩn thiết kế cao cấp mà không làm ảnh hưởng đến tính năng của ứng dụng. Hỗ trợ mọi CSS framework hoặc CSS thuần.
---

# Kỹ năng Thiết kế lại (Redesign Skill)

## Quy trình Thực hiện

Khi áp dụng vào một dự án hiện có, hãy tuân theo trình tự sau:

1. **Scan (Quét)** — Đọc mã nguồn. Xác định framework, phương thức định nghĩa style (Tailwind, CSS thuần, styled-components, v.v.) và các mẫu thiết kế hiện tại.
2. **Diagnose (Chẩn đoán)** — Thực hiện kiểm tra theo danh mục bên dưới. Liệt kê mọi phong cách rập khuôn, điểm yếu và các trạng thái còn thiếu mà bạn tìm thấy.
3. **Fix (Sửa chữa)** — Áp dụng các nâng cấp có mục tiêu cụ thể dựa trên stack công nghệ hiện có. Không viết lại từ đầu. Hãy cải thiện những gì đang có sẵn.

## Kiểm tra Thiết kế (Design Audit)

### Typography (Font chữ & Định dạng chữ)

Kiểm tra các vấn đề sau và khắc phục chúng:

- **Sử dụng font chữ mặc định của trình duyệt hoặc font Inter ở khắp nơi.** Thay thế bằng một font chữ có cá tính hơn. Lựa chọn tốt: `Geist`, `Outfit`, `Cabinet Grotesk`, Satoshi. Đối với các dự án mang tính biên tập/sáng tạo, hãy kết hợp tiêu đề font serif với nội dung font sans-serif.
- **Tiêu đề thiếu điểm nhấn.** Tăng kích thước cho display text, thu hẹp letter-spacing (khoảng cách chữ), giảm line-height (chiều cao dòng). Tiêu đề phải tạo cảm giác đầm và có tính toán.
- **Văn bản mô tả (body text) quá rộng.** Giới hạn chiều rộng đoạn văn tối đa khoảng 65 ký tự trên một dòng. Tăng line-height để cải thiện khả năng đọc.
- **Chỉ sử dụng độ dày Regular (400) và Bold (700).** Bổ sung thêm Medium (500) và SemiBold (600) để tạo hệ thống phân cấp trực quan tinh tế hơn.
- **Hiển thị số bằng font chữ tỷ lệ (proportional).** Sử dụng font monospace hoặc kích hoạt tính năng số dạng bảng (`font-variant-numeric: tabular-nums`) cho các giao diện nhiều dữ liệu số.
- **Thiếu điều chỉnh khoảng cách chữ (letter-spacing).** Sử dụng negative tracking (khoảng cách hẹp) cho các tiêu đề lớn, positive tracking (khoảng cách rộng) cho chữ in hoa nhỏ hoặc các nhãn tag.
- **Sử dụng tiêu đề phụ viết hoa toàn bộ (all-caps) ở mọi nơi.** Hãy thử thay thế bằng chữ thường in nghiêng, viết hoa chữ cái đầu câu, hoặc dùng small-caps.
- **Từ mồ côi (Orphaned words).** Một từ duy nhất đứng cô độc ở dòng cuối cùng của đoạn văn. Khắc phục bằng cách dùng `text-wrap: balance` hoặc `text-wrap: pretty`.

### Màu sắc và Bề mặt (Color & Surfaces)

- **Sử dụng nền màu đen tuyệt đối `#000000`.** Thay thế bằng màu đen dịu (off-black), xám than tối, hoặc đen có ánh màu (`#0a0a0a`, `#121212`, hoặc navy tối).
- **Màu nhấn (accent) quá sặc sỡ (oversaturated).** Giữ độ bão hòa màu (saturation) dưới 80%. Giảm độ bão hòa màu nhấn để chúng hòa hợp với các màu trung tính thay vì quá chói mắt.
- **Sử dụng nhiều hơn một màu nhấn.** Chỉ chọn một màu nhấn duy nhất. Loại bỏ các màu khác. Sự đồng nhất quan trọng hơn sự đa dạng.
- **Pha trộn các tone màu xám ấm và xám lạnh.** Chỉ trung thành với một họ màu xám duy nhất trên trang. Pha màu xám với một ánh màu thống nhất (chỉ ấm hoặc chỉ lạnh, không trộn cả hai).
- **Thẩm mỹ "AI gradient" tím/xanh.** Đây là dấu vân tay thiết kế AI phổ biến nhất. Hãy thay thế bằng các nền trung tính và một màu nhấn duy nhất được cân nhắc kỹ.
- **Đổ bóng `box-shadow` mặc định.** Pha màu đổ bóng khớp với sắc độ màu nền. Sử dụng bóng có màu sắc (ví dụ: đổ bóng xanh dương tối trên nền xanh dương) thay vì dùng màu đen thuần túy với opacity thấp.
- **Thiết kế phẳng hoàn toàn, thiếu chất liệu (texture).** Thêm nhiễu hạt nhẹ, chất liệu giấy mờ hoặc các micro-pattern vào nền. Các vector phẳng hoàn toàn tạo cảm giác vô trùng và thiếu sức sống.
- **Gradient chuyển màu quá đều.** Phá vỡ sự đồng đều bằng cách sử dụng radial gradient, lớp phủ nhiễu hạt, hoặc mesh gradient thay vì các dải màu tuyến tính 45 độ thông thường.
- **Hướng ánh sáng không nhất quán.** Kiểm tra toàn bộ đổ bóng để đảm bảo chúng mô phỏng một nguồn sáng duy nhất và đồng nhất trên toàn giao diện.
- **Các section tối xuất hiện ngẫu nhiên trong trang sáng (hoặc ngược lại).** Một section nền tối đột ngột cắt quang một trang web nền sáng trông giống như một lỗi copy-paste. Hãy cam kết thiết kế một giao diện sáng/tối đồng nhất hoặc duy trì tone nền nhất quán. Nếu cần tạo tương phản, hãy dùng một tone màu tối hơn một chút cùng bảng màu — không nhảy đột ngột sang `#111` ở giữa một trang màu kem.
- **Các section phẳng lì, trống trải không có chiều sâu trực quan.** Các section chỉ có chữ trên nền phẳng tạo cảm giác chưa hoàn thiện. Thêm hình ảnh nền chất lượng cao (làm mờ, phủ màu hoặc dùng mask), pattern tinh tế, hoặc gradient môi trường. Sử dụng nguồn ảnh placeholder uy tín như `https://picsum.photos/seed/{name}/1920/1080` khi chưa có asset thật. Thử nghiệm đặt ảnh nền phía sau các phần hero, khối tính năng hoặc CTA — ngay cả một bức ảnh mờ phủ toàn trang với opacity thấp cũng tăng tính hiện diện trực quan.

### Bố cục (Layout)

- **Mọi thứ đều căn giữa và đối xứng.** Phá vỡ sự đối xứng bằng các lề lệch (offset margins), trộn lẫn tỷ lệ khung hình ảnh, hoặc căn lề trái tiêu đề phía trên nội dung căn giữa.
- **Hàng tính năng gồm 3 thẻ card bằng nhau.** Đây là bố cục AI rập khuôn nhất. Thay thế bằng lưới zic-zac 2 cột, lưới bất đối xứng, thanh cuộn ngang, hoặc bố cục xếp gạch (masonry).
- **Sử dụng `height: 100vh` cho các section đầy màn hình.** Thay thế bằng `min-height: 100dvh` để ngăn chặn lỗi giật bố cục trên các trình duyệt di động (lỗi viewport của Safari trên iOS).
- **Sử dụng flexbox tính toán phần trăm phức tạp.** Thay thế bằng CSS Grid để có cấu trúc nhiều cột ổn định và dễ quản lý hơn.
- **Không giới hạn chiều rộng container.** Thêm giới hạn chiều rộng container (khoảng 1200-1440px) kết hợp lề tự động (`margin: auto`) để nội dung không bị kéo dãn tràn viền trên màn hình siêu rộng.
- **Các thẻ card bị ép cùng chiều cao bằng flexbox.** Hãy cho phép chiều cao biến thiên hoặc sử dụng bố cục xếp gạch (masonry) khi nội dung có độ dài ngắn khác nhau.
- **Bo góc (border-radius) giống hệt nhau trên mọi phần tử.** Hãy làm đa dạng bán kính bo góc: bo góc nhỏ hơn cho các phần tử bên trong, bo góc mềm mại/lớn hơn cho các container bao ngoài.
- **Không có các phần tử xếp đè hoặc tạo chiều sâu.** Các phần tử nằm phẳng cạnh nhau. Sử dụng margin âm để tạo các lớp xếp chồng và chiều sâu trực quan.
- **Padding dọc đối xứng hoàn hảo.** Padding trên và dưới luôn bằng nhau. Hãy điều chỉnh theo cảm nhận trực quan — padding dưới thường cần lớn hơn một chút để cân bằng.
- **Dashboard luôn có sidebar bên trái.** Hãy thử dùng thanh điều hướng phía trên, menu lệnh nổi (command menu), hoặc bảng điều khiển có thể thu gọn.
- **Thiếu khoảng trắng.** Hãy tăng gấp đôi khoảng cách. Hãy để thiết kế hít thở. Bố cục dày đặc chỉ phù hợp cho dashboard dữ liệu chuyên sâu, không phù hợp cho trang marketing.
- **Nút bấm không được căn lề dưới trong nhóm thẻ card.** Khi các thẻ card có độ dài nội dung khác nhau, các nút bấm CTA sẽ bị lệch dòng. Hãy ghim các nút bấm vào đáy của từng thẻ card để chúng tạo thành một đường ngang thẳng hàng đẹp mắt.
- **Danh sách tính năng bắt đầu ở các vị trí dọc khác nhau.** Trong bảng giá hoặc thẻ so sánh, danh sách tính năng nên bắt đầu ở cùng một vị trí Y trên tất cả các cột. Sử dụng khoảng cách đồng nhất phía trên danh sách hoặc các khối tiêu đề/giá có chiều cao cố định.
- **Nhịp điệu dọc không nhất quán ở các phần tử đặt cạnh nhau.** Khi đặt các thẻ card, các cột hoặc các bảng cạnh nhau, hãy căn chỉnh các phần tử chung (tiêu đề, mô tả, giá cả, nút bấm) thẳng hàng trên toàn bộ các mục. Các baseline lệch nhau làm bố cục trông như bị lỗi.
- **Căn chỉnh toán học tạo cảm giác lệch về mặt trực quan.** Căn giữa bằng toán học không phải lúc nào cũng tạo cảm giác căn giữa cho mắt người đọc. Các icon đặt cạnh chữ, nút play đặt trong hình tròn, hoặc chữ trong nút bấm thường cần điều chỉnh dịch chuyển 1-2px để tạo cảm giác cân bằng.

### Tương tác và Trạng thái (Interactivity & States)

- **Không có trạng thái hover trên nút bấm.** Thêm hiệu ứng đổi màu nền nhẹ, phóng to/thu nhỏ nhẹ, hoặc dịch chuyển vị trí khi hover.
- **Không phản hồi khi nhấn (active/pressed state).** Thêm hiệu ứng `scale(0.98)` hoặc `translateY(1px)` nhẹ khi nhấn để mô phỏng lực click vật lý.
- **Thay đổi trạng thái lập tức không có thời gian chuyển tiếp (transition duration).** Thêm transition mượt mà (200-300ms) cho tất cả các phần tử tương tác.
- **Thiếu vòng viền tập trung (focus ring).** Đảm bảo có chỉ thị focus rõ ràng cho việc điều hướng bằng bàn phím. Đây là yêu cầu bắt buộc về khả năng tiếp cận (accessibility), không phải tùy chọn.
- **Không có trạng thái tải dữ liệu (loading states).** Thay thế vòng xoay spinner mặc định nhàm chán bằng các skeleton loader mô phỏng hình dáng bố cục thực tế.
- **Không có trạng thái trống (empty states).** Một dashboard trống không có gì là một cơ hội bị bỏ lỡ. Hãy thiết kế một chế độ xem hướng dẫn "bắt đầu sử dụng" được sắp xếp chu đáo.
- **Không có trạng thái lỗi.** Thêm thông báo lỗi nội dòng (inline error) rõ ràng cho các form. Không sử dụng `window.alert()`.
- **Liên kết chết.** Các nút bấm trỏ đến liên kết `#`. Hãy trỏ chúng đến đích thực tế hoặc vô hiệu hóa chúng về mặt trực quan.
- **Không có chỉ thị trang hiện tại trên nav.** Định nghĩa style cho liên kết điều hướng đang kích hoạt khác biệt đi để người dùng biết họ đang ở đâu.
- **Cuộn trang bị giật.** Nhấp chuột vào anchor nhảy trang lập tức. Hãy thêm `scroll-behavior: smooth`.
- **Hoạt ảnh sử dụng các thuộc tính `top`, `left`, `width`, `height`.** Chuyển sang sử dụng `transform` and `opacity` để hoạt ảnh mượt mà và được tăng tốc phần cứng bằng GPU.

### Nội dung (Content)

- **Các tên gọi chung chung như "John Doe" hay "Jane Smith".** Sử dụng các tên gọi đa dạng, thực tế hơn.
- **Số liệu làm tròn giả tạo như `99.99%`, `50%`, `$100.00`.** Sử dụng dữ liệu thực tế, tự nhiên: `47.2%`, `$99.00`, `+1 (312) 847-1928`.
- **Tên công ty giả lập mặc định như "Acme Corp", "Nexus", "SmartFlow".** Sáng tạo các tên thương hiệu tin cậy, phù hợp với ngữ cảnh.
- **Cụm từ copywriting sáo rỗng của AI.** Tuyệt đối không dùng các từ "Elevate", "Seamless", "Unleash", "Next-Gen", "Game-changer", "Delve", "Tapestry", hoặc "In the world of...". Hãy viết ngôn từ rõ ràng, cụ thể và tự nhiên.
- **Dấu chấm than trong thông báo thành công.** Hãy loại bỏ chúng. Thể hiện sự tự tin, không cần la lớn.
- **Thông báo lỗi kiểu "Oops!" (Rất tiếc!).** Hãy đi thẳng vào vấn đề: "Lỗi kết nối. Vui lòng thử lại."
- **Sử dụng câu bị động.** Hãy dùng câu chủ động: "Chúng tôi không thể lưu các thay đổi của bạn" thay vì "Lỗi đã xảy ra khi lưu."
- **Mọi bài viết blog có ngày đăng giống hệt nhau.** Hãy ngẫu nhiên hóa ngày tháng để trông giống như một trang web đang hoạt động thật.
- **Sử dụng cùng một ảnh avatar cho nhiều tài khoản.** Sử dụng các asset avatar duy nhất cho từng người.
- **Văn bản Lorem Ipsum.** Tuyệt đối không sử dụng chữ latin placeholder. Hãy viết nội dung nháp thực tế.
- **Viết Hoa Toàn Bộ Từ Trong Mọi Tiêu Đề.** Hãy sử dụng cách viết thường thông thường, chỉ viết hoa chữ cái đầu tiên của tiêu đề.

### Các Mẫu Component (Component Patterns)

- **Kiểu thẻ card mặc định (border + shadow + nền trắng).** Hãy bỏ viền, hoặc chỉ dùng màu nền, hoặc chỉ dùng khoảng cách spacing. Thẻ card chỉ nên tồn tại khi độ nổi (elevation) thể hiện sự phân cấp thông tin.
- **Luôn là cặp nút: một nút đầy màu + một nút ghost.** Hãy thêm các liên kết văn bản hoặc kiểu nút cấp 3 để giảm bớt tiếng ồn trực quan.
- **Huy hiệu "New" và "Beta" dạng viên thuốc.** Hãy thử dùng huy hiệu vuông, dạng tag, hoặc nhãn văn bản thuần túy.
- **Các section FAQ dạng accordion.** Thử dùng danh sách chia cột bên cạnh nhau, ô tìm kiếm trợ giúp, hoặc hiển thị thông tin tăng tiến (progressive disclosure).
- **Carousel đánh giá 3 thẻ card đi kèm các chấm tròn.** Thay thế bằng một bức tường xếp gạch, các bài viết mạng xã hội nhúng thực tế, hoặc một trích dẫn xoay vòng duy nhất.
- **Bảng giá gồm 3 cột tháp.** Nổi bật gói khuyên dùng bằng màu sắc và nhấn mạnh trực quan, không chỉ đơn thuần là kéo cao thẻ card đó lên.
- **Sử dụng modal cho mọi hành động.** Hãy dùng chỉnh sửa nội dòng, bảng trượt từ cạnh (slide-over panels), hoặc các section mở rộng thay vì dùng popup cho các tác vụ đơn giản.
- **Chỉ sử dụng avatar tròn.** Hãy thử dùng squircles (bo góc mềm) hoặc hình vuông bo góc để giao diện bớt rập khuôn hơn.
- **Nút chuyển đổi sáng/tối luôn là sun/moon.** Hãy dùng dropdown danh sách, tự động phát hiện tùy chọn hệ thống, hoặc tích hợp nó gọn gàng vào phần cài đặt.
- **Chân trang quá nhiều cột link.** Hãy đơn giản hóa. Tập trung vào các luồng điều hướng chính và các liên kết pháp lý bắt buộc.

### Iconography (Hệ thống Icon)

- **Chỉ sử dụng duy nhất Lucide hoặc Feather icons.** Đây là những lựa chọn icon mặc định của AI. Hãy dùng Phosphor, Heroicons, hoặc một bộ icon tùy chỉnh để tạo sự khác biệt.
- **Sử dụng tên lửa cho "Launch", khiên cho "Security".** Thay thế các biểu tượng sáo rỗng bằng các biểu tượng tinh tế hơn (tia chớp, dấu vân tay, tia sáng, két sắt).
- **Độ dày nét vẽ của các icon không đồng nhất.** Kiểm tra lại toàn bộ icon và quy chuẩn về một độ dày nét vẽ thống nhất.
- **Thiếu favicon.** Luôn thiết kế một favicon đồng bộ thương hiệu.
- **Ảnh stock "đội ngũ đa dạng" giả tạo.** Hãy dùng ảnh chụp thực tế của đội ngũ, ảnh chụp tự nhiên, hoặc phong cách minh họa đồng nhất thay vì dùng ảnh stock gượng gạo.

### Chất lượng Code

- **Div soup (Lạm dụng thẻ div).** Sử dụng HTML ngữ nghĩa: `<nav>`, `<main>`, `<article>`, `<aside>`, `<section>`.
- **Trộn lẫn inline styles với CSS classes.** Di chuyển toàn bộ định nghĩa style vào hệ thống quản lý style chung của dự án.
- **Khai báo chiều rộng bằng pixel cứng.** Sử dụng các đơn vị tương đối (`%`, `rem`, `em`, `max-width`) cho các bố cục linh hoạt.
- **Thiếu thuộc tính `alt` trên thẻ ảnh.** Mô tả nội dung hình ảnh cho các trình đọc màn hình. Không bao giờ để `alt=""` hoặc `alt="image"` trên các hình ảnh có ý nghĩa.
- **Sử dụng các giá trị z-index tùy tiện như `9999`.** Thiết lập một hệ thống thang đo z-index rõ ràng trong các biến/theme.
- **Code chết bị comment.** Loại bỏ toàn bộ các dòng code debug trước khi bàn giao.
- **Khai báo import ảo.** Kiểm tra xem mọi package import có thực sự tồn tại trong `package.json` hoặc các dependency của dự án hay không.
- **Thiếu thẻ meta.** Bổ sung đầy đủ thẻ `<title>`, `description`, `og:image`, và các thẻ meta chia sẻ mạng xã hội.

### Những phần thường bị bỏ quên (Thiên kiến của AI)

- **Không có các liên kết pháp lý.** Thêm liên kết chính sách bảo mật và điều khoản dịch vụ ở chân trang.
- **Không có nút quay lại.** Người dùng bị kẹt trong các luồng thao tác. Mỗi trang con cần có cách để quay lại.
- **Không có trang 404 tùy chỉnh.** Thiết kế một trang báo lỗi "không tìm thấy trang" thân thiện và đồng bộ thương hiệu.
- **Không có validation cho form.** Thêm validation phía client cho email, các trường bắt buộc và định dạng đầu vào.
- **Không có liên kết "skip to content" (chuyển nhanh đến nội dung).** Đây là tính năng thiết yếu cho người dùng sử dụng bàn phím. Hãy thêm một skip-link ẩn.
- **Không có thông báo cookie.** Nếu khu vực pháp lý yêu cầu, hãy thêm một banner xin ý kiến đồng thuận về cookie.

## Các kỹ thuật Nâng cấp

Khi nâng cấp một dự án, hãy áp dụng các kỹ thuật có sức tác động trực quan lớn sau đây để thay thế các mẫu rập khuôn:

### Nâng cấp Typography
- **Hoạt ảnh variable font.** Thay đổi độ dày hoặc chiều rộng font chữ theo hành vi cuộn trang hoặc hover để chữ có cảm giác sống động.
- **Hiệu ứng Outlined-to-fill.** Chữ bắt đầu dưới dạng viền rỗng (stroke outline) và tự động tô đầy màu khi cuộn trang tới hoặc tương tác.
- **Hé lộ chữ qua mặt nạ (Text mask).** Typography cực lớn đóng vai trò như một cửa sổ hiển thị video hoặc hoạt ảnh chuyển động phía sau nó.

### Nâng cấp Bố cục
- **Phá vỡ lưới / Bất đối xứng.** Các phần tử cố tình bỏ qua cấu trúc cột — xếp đè lên nhau, tràn khỏi viền màn hình, hoặc lệch dòng với độ ngẫu nhiên được tính toán.
- **Tối đa hóa khoảng trắng.** Sử dụng không gian âm mạnh mẽ để tập trung hoàn toàn ánh nhìn vào một phần tử duy nhất.
- **Xếp chồng thẻ parallax.** Các section dính và xếp chồng lên nhau khi cuộn trang.
- **Cuộn chia màn hình (Split-screen scroll).** Hai nửa màn hình trượt theo hai hướng ngược nhau khi cuộn.

### Nâng cấp Hoạt ảnh
- **Cuộn mượt mà có quán tính (Smooth scroll).** Tách biệt hành vi cuộn khỏi mặc định của trình duyệt để tạo cảm giác cinematic, đầm hơn.
- **Xuất hiện so le (Staggered entry).** Các phần tử xuất hiện lần lượt với độ trễ ngắn, kết hợp dịch chuyển trục Y và mờ dần. Không bao giờ hiển thị tất cả cùng lúc.
- **Vật lý lò xo (Spring physics).** Thay thế easing tuyến tính bằng chuyển động dạng lò xo để tạo cảm giác tự nhiên, có trọng lượng cho các tương tác.
- **Hé lộ theo hành vi cuộn (Scroll-driven).** Nội dung xuất hiện thông qua các mặt nạ mở rộng, hiệu ứng quét (wipes), hoặc vẽ đường SVG chạy theo tiến trình cuộn trang.

### Nâng cấp Bề mặt
- **Kính mờ đích thực (Glassmorphism).** Vượt ra ngoài thuộc tính `backdrop-filter: blur`. Thêm một viền trong 1px và đổ bóng trong tinh tế để mô phỏng khúc xạ ánh sáng ở cạnh kính.
- **Đường viền rọi sáng (Spotlight borders).** Đường viền của thẻ card tự động phát sáng chạy theo vị trí con trỏ chuột.
- **Lớp phủ nhiễu hạt (Grain/Noise).** Một lớp phủ cố định, không nhận sự kiện chuột với texture hạt nhẹ để phá vỡ sự phẳng lì đơn điệu kỹ thuật số.
- **Đổ bóng có ánh màu.** Đổ bóng mang sắc độ của màu nền thay vì dùng màu đen mặc định.

## Thứ tự Ưu tiên Khắc phục

Áp dụng các thay đổi theo thứ tự sau để đạt hiệu quả trực quan cao nhất với rủi ro thấp nhất:

1. **Thay thế Font chữ** — nâng cấp lớn và nhanh nhất, ít rủi ro nhất.
2. **Làm sạch bảng màu** — loại bỏ các màu chọi nhau hoặc quá lòe loẹt.
3. **Thêm trạng thái hover và active** — giúp giao diện sống động và phản hồi.
4. **Sắp xếp lại bố cục và khoảng cách** — lưới grid chuẩn, max-width, padding thống nhất.
5. **Thay thế các component rập khuôn** — đổi các thiết kế sáo rỗng lấy giải pháp hiện đại hơn.
6. **Thêm các trạng thái loading, trống và lỗi** — giúp ứng dụng tạo cảm giác hoàn thiện.
7. **Tinh chỉnh tỷ lệ typography và khoảng cách chữ** — bước hoàn thiện cao cấp cuối cùng.

## Quy tắc

- Làm việc dựa trên công nghệ hiện có của dự án. Không tự ý đổi framework hoặc thư viện styling.
- Không làm ảnh hưởng đến các tính năng hiện có. Kiểm tra lại sau mỗi thay đổi.
- Trước khi import bất kỳ thư viện mới nào, hãy kiểm tra file dependency của dự án trước.
- Nếu dự án sử dụng Tailwind, hãy xác định phiên bản (v3 so với v4) trước khi sửa đổi cấu hình.
- Nếu dự án không sử dụng framework, hãy dùng CSS thuần.
- Giữ các thay đổi ở mức tập trung và dễ review. Thực hiện các cải tiến nhỏ, có mục tiêu thay vì viết lại các mảng code lớn.
