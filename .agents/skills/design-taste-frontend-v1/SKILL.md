---
name: design-taste-frontend-v1
description: The original v1 taste-skill, preserved for projects depending on its exact behavior. The current default is `design-taste-frontend` (v2 experimental), which is a substantial rewrite. Use this v1 install name only if you need exact backward compatibility.
---

# Kỹ năng Frontend Định hướng Cao (High-Agency Frontend Skill)

## 1. CẤU HÌNH ĐƯỜNG CƠ SỞ HOẠT ĐỘNG (ACTIVE BASELINE CONFIGURATION)
* DESIGN_VARIANCE: 8 (1=Đối xứng hoàn hảo, 10=Hỗn loạn đầy tính nghệ thuật)
* MOTION_INTENSITY: 6 (1=Tĩnh/Không chuyển động, 10=Chuyển động cinematic/Vật lý ma thuật)
* VISUAL_DENSITY: 4 (1=Phòng triển lãm/Thoáng đãng, 10=Buồng lái phi công/Dày đặc dữ liệu)

**Chỉ thị cho AI:** Đường cơ sở mặc định cho tất cả các lần tạo là các giá trị này (8, 6, 4). Không yêu cầu người dùng chỉnh sửa file này. Mặt khác, LUÔN LUÔN lắng nghe người dùng: tự động điều chỉnh các giá trị này dựa trên những gì họ yêu cầu rõ ràng trong chat prompt. Sử dụng các giá trị cơ sở (hoặc các giá trị được người dùng ghi đè) làm biến toàn cục để điều khiển logic cụ thể trong các Phần từ 3 đến 7.

## 2. KIẾN TRÚC MẶC ĐỊNH & QUY ƯỚC
Trừ khi người dùng yêu cầu rõ ràng một stack công nghệ khác, hãy tuân thủ nghiêm ngặt các ràng buộc cấu trúc này để duy trì tính đồng nhất:

* **XÁC THỰC DEPENDENCY [BẮT BUỘC]:** Trước khi import BẤT KỲ thư viện bên thứ ba nào (ví dụ: `framer-motion`, `lucide-react`, `zustand`), bạn BẮT BUỘC phải kiểm tra `package.json`. Nếu thư viện chưa được cài đặt, bạn phải đưa ra lệnh cài đặt (ví dụ: `npm install package-name`) trước khi cung cấp code. **Không bao giờ** tự giả định một thư viện đã tồn tại.
* **Framework & Tính tương tác:** React hoặc Next.js. Mặc định sử dụng Server Components (`RSC`).
    * **AN TOÀN RSC:** Global state CHỈ hoạt động trong Client Components. Trong Next.js, hãy bao bọc các provider trong một component `"use client"`.
    * **TÁCH BIỆT TƯƠNG TÁC:** Nếu các Phần 4 hoặc 7 (Chuyển động/Liquid Glass) hoạt động, component UI tương tác cụ thể đó BẮT BUỘC phải được tách riêng thành một component lá cô lập với khai báo `'use client'` ở ngay đầu file. Server Components chỉ được dùng để render bố cục tĩnh.
* **Quản lý trạng thái (State):** Sử dụng `useState`/`useReducer` local cho UI độc lập. Chỉ dùng global state khi thực sự cần thiết để tránh prop-drilling sâu.
* **Quy tắc định nghĩa style:** Sử dụng Tailwind CSS (v3/v4) cho 90% việc định nghĩa style.
    * **KHÓA PHIÊN BẢN TAILWIND:** Kiểm tra `package.json` trước. Không sử dụng cú pháp v4 trong dự án v3.
    * **BẢO VỆ CẤU HÌNH V4:** Đối với v4, KHÔNG sử dụng plugin `tailwindcss` trong `postcss.config.js`. Hãy sử dụng `@tailwindcss/postcss` hoặc plugin Vite.
* **CHÍNH SÁCH CHỐNG EMOJI [CỰC KỲ QUAN TRỌ]:** KHÔNG BAO GIỜ sử dụng emoji trong code, markup, nội dung văn bản hoặc alt text của ảnh. Thay thế bằng các icon chất lượng cao (Radix, Phosphor) hoặc SVG thuần túy. Emoji bị CẤM hoàn toàn.
* **Hiển thị đáp ứng & Spacing:**
  * Đồng bộ các breakpoint chuẩn (`sm`, `md`, `lg`, `xl`).
  * Giới hạn chiều rộng trang bằng `max-w-[1400px] mx-auto` hoặc `max-w-7xl`.
  * **Độ ổn định của Viewport [CỰC KỲ QUAN TRỌ]:** KHÔNG BAO GIỜ sử dụng `h-screen` cho các phần Hero đầy màn hình. LUÔN LUÔN dùng `min-h-[100dvh]` để ngăn chặn lỗi giật bố cục nghiêm trọng trên trình duyệt di động (iOS Safari).
  * **Lưới Grid thay vì Flexbox thủ công:** KHÔNG BAO GIỜ sử dụng flexbox tính toán phần trăm phức tạp (`w-[calc(33%-1rem)]`). LUÔN LUÔN sử dụng CSS Grid (`grid grid-cols-1 md:grid-cols-3 gap-6`) cho các cấu trúc cột.
* **Icons:** Bạn BẮT BUỘC phải sử dụng chính xác path import từ `@phosphor-icons/react` hoặc `@radix-ui/react-icons` (kiểm tra phiên bản được cài đặt). Đồng bộ `strokeWidth` trên toàn hệ thống (ví dụ: chỉ sử dụng `1.5` hoặc `2.0`).

## 3. CHỈ THỊ KỸ THUẬT THIẾT KẾ (Chống Thiên Kiến AI)
Các mô hình LLM có thiên kiến thống kê hướng về các mẫu UI sáo rỗng. Hãy chủ động xây dựng giao diện cao cấp bằng các quy tắc sau:

**Quy tắc 1: Typography có tính toán**
* **Display/Tiêu đề lớn:** Mặc định sử dụng `text-4xl md:text-6xl tracking-tighter leading-none`.
    * **CHỐNG RẬP KHUÔN:** Hạn chế dùng font `Inter` cho phong cách "Premium" hoặc "Creative". Hãy bắt buộc dùng các font có tính cá tính riêng như `Geist`, `Outfit`, `Cabinet Grotesk`, hoặc `Satoshi`.
    * **QUY TẮC UI KỸ THUẬT:** Font Serif bị CẤM hoàn toàn trong các giao diện Dashboard/Phần mềm. Trong các ngữ cảnh này, chỉ sử dụng các cặp Sans-Serif cao cấp (`Geist` + `Geist Mono` hoặc `Satoshi` + `JetBrains Mono`).
* **Body/Văn bản thường:** Mặc định sử dụng `text-base text-gray-600 leading-relaxed max-w-[65ch]`.

**Quy tắc 2: Cân chỉnh Màu sắc**
* **Ràng buộc:** Tối đa 1 màu nhấn. Độ bão hòa màu (saturation) < 80%.
* **CẤM MÀU TÍM NEON:** Thẩm mỹ tím/xanh dạng AI bị CẤM NGHIÊM NGẶT. Không có đổ bóng nút màu tím, không có gradient neon. Sử dụng các màu nền trung tính tuyệt đối (Zinc/Slate) đi kèm một màu nhấn duy nhất có tương phản cao (ví dụ: Emerald, Electric Blue, hoặc Deep Rose).
* **ĐỒNG NHẤT MÀU SẮC:** Chỉ dùng một bảng màu cho toàn bộ đầu ra. Không thay đổi tùy tiện giữa xám ấm và xám lạnh trong cùng một dự án.

**Quy tắc 3: Đa dạng hóa Bố cục**
* **CHỐNG CĂN GIỮA:** Các phần Hero/H1 căn giữa bị CẤM hoàn toàn khi `LAYOUT_VARIANCE > 4`. Hãy dùng bố cục chia đôi màn hình "Split Screen" (50/50), "Chữ lề trái / ảnh lề phải", hoặc sử dụng khoảng trắng bất đối xứng.

**Quy tắc 4: Chất liệu, Đổ bóng, và "Tránh lạm dụng Thẻ card"**
* **TỐI GIẢN DASHBOARD:** Khi `VISUAL_DENSITY > 7`, các thẻ card chứa nội dung chung chung bị CẤM hoàn toàn. Hãy phân nhóm logic bằng `border-t`, `divide-y`, hoặc chỉ sử dụng khoảng trắng. Dữ liệu số liệu nên có không gian thoáng đãng mà không cần đóng khung trừ khi thực sự cần độ nổi (z-index) cho mục đích chức năng.
* **Thực thi:** Chỉ dùng thẻ card khi độ nổi thể hiện sự phân cấp thông tin. Khi dùng bóng đổ, hãy pha màu bóng trùng với sắc độ của nền.

**Quy tắc 5: Trạng thái UI Tương tác**
* **Thực thi Bắt buộc:** AI thường chỉ sinh ra các trạng thái tĩnh thành công. Bạn BẮT BUỘC phải viết đầy đủ các chu kỳ tương tác:
  * **Loading:** Skeleton loader khớp kích thước bố cục (tránh vòng quay spinner tròn mặc định).
  * **Empty States:** Trạng thái trống được thiết kế đẹp mắt chỉ dẫn cách tạo dữ liệu.
  * **Error States:** Báo lỗi nội dòng rõ ràng (ví dụ: trong form).
  * **Phản hồi lực nhấn (Tactile):** Khi `:active`, dùng hiệu ứng `-translate-y-[1px]` hoặc `scale-[0.98]` nhẹ để mô phỏng lực nhấn nút vật lý.

**Quy tắc 6: Mẫu dữ liệu & Form**
* **Form:** Nhãn (label) BẮT BUỘC phải nằm trên ô nhập. Helper text is optional nhưng nên có trong markup. Text báo lỗi nằm dưới ô nhập. Sử dụng khoảng cách `gap-2` tiêu chuẩn cho các khối input.

## 4. CHỦ ĐỘNG SÁNG TẠO (Chống Rập Khuôn AI)
Để chủ động loại bỏ các thiết kế AI chung chung, hãy áp dụng các khái niệm lập trình cao cấp sau đây làm đường cơ sở:
* **"Liquid Glass" Refraction (Khúc xạ kính mờ):** Khi cần dùng hiệu ứng glassmorphism, hãy vượt qua thuộc tính `backdrop-blur` thông thường. Thêm viền trong 1px (`border-white/10`) và đổ bóng trong tinh tế (`shadow-[inset_0_1px_0_rgba(255,255,255,0.1)]`) để mô phỏng khúc xạ ánh sáng ở viền kính vật lý.
* **Vật lý Nam châm vi mô (Nếu MOTION_INTENSITY > 5):** Triển khai các nút bấm tự động hút nhẹ về phía con trỏ chuột. **CỰC KỲ QUAN TRỌNG:** KHÔNG bao giờ dùng `useState` của React cho hiệu ứng hover nam châm hoặc các hoạt ảnh lặp liên tục. Chỉ sử dụng `useMotionValue` và `useTransform` của Framer Motion nằm ngoài vòng đời render React để tránh làm giảm hiệu năng trên thiết bị di động.
* **Tương tác Micro-Interaction liên tục:** Khi `MOTION_INTENSITY > 5`, nhúng các hoạt ảnh micro-animation lặp vô tận (Pulse, Typewriter, Float, Shimmer, Carousel) vào các component tiêu chuẩn (avatar, trạng thái chấm tròn, background). Áp dụng Spring Physics cao cấp (`type: "spring", stiffness: 100, damping: 20`) cho tất cả các phần tử tương tác—không dùng easing tuyến tính (linear).
* **Chuyển đổi Bố cục:** Luôn sử dụng thuộc tính `layout` và `layoutId` của Framer Motion để sắp xếp lại, thay đổi kích thước và chuyển đổi mượt mà các phần tử dùng chung qua các thay đổi trạng thái.
* **Biên đạo so le (Staggered):** Không mount đồng loạt danh sách hoặc lưới grid. Sử dụng `staggerChildren` (Framer) hoặc CSS cascade (`animation-delay: calc(var(--index) * 100ms)`) để tạo hiệu ứng xuất hiện thác nước sequential. **CỰC KỲ QUAN TRỌNG:** Đối với `staggerChildren`, phần tử Cha (`variants`) và các phần tử Con BẮT BUỘC phải nằm trong cùng một cây Client Component. Nếu dữ liệu được fetch không đồng bộ, hãy truyền dữ liệu dưới dạng prop vào một wrapper Parent Motion chung.

## 5. HÀNG RÀO HIỆU NĂNG
* **Chi phí DOM:** Chỉ áp dụng bộ lọc hạt/noise trên các phần tử giả cố định, không nhận sự kiện chuột (ví dụ: `fixed inset-0 z-50 pointer-events-none`) và KHÔNG bao giờ gắn vào container cuộn để tránh GPU phải vẽ lại liên tục làm giảm hiệu năng mobile.
* **Tăng tốc phần cứng:** Không bao giờ tạo hoạt ảnh cho `top`, `left`, `width`, hoặc `height`. Chỉ tạo hoạt ảnh qua `transform` và `opacity`.
* **Tiết chế Z-Index:** KHÔNG lạm dụng các giá trị `z-50` hoặc `z-10` tùy tiện. Chỉ dùng z-index cho các tầng hệ thống rõ ràng (Nav dính, Modal, Lớp phủ).

## 6. THÔNG SỐ KỸ THUẬT (Các nút vặn cấu hình)

### DESIGN_VARIANCE (Độ biến thiên bố cục - Cấp 1-10)
* **1-3 (Dễ đoán):** Flexbox `justify-center`, lưới đối xứng 12 cột nghiêm ngặt, padding bằng nhau.
* **4-7 (Lệch dòng):** Sử dụng lề âm `margin-top: -2rem` để tạo xếp chồng đè, thay đổi tỷ lệ ảnh (ví dụ: 4:3 đặt cạnh 16:9), tiêu đề căn trái phía trên dữ liệu căn giữa.
* **8-10 (Bất đối xứng):** Bố cục dạng xếp gạch (masonry), CSS Grid sử dụng các đơn vị phân số (ví dụ: `grid-template-columns: 2fr 1fr 1fr`), khoảng trắng trống lớn (`padding-left: 20vw`).
* **GHI ĐÈ MOBILE:** Đối với các cấp từ 4-10, mọi bố cục bất đối xứng trên `md:` BẮT BUỘC phải tự động thu gọn về dạng một cột duy nhất (`w-full`, `px-4`, `py-8`) trên các khung nhìn `< 768px` để tránh xuất hiện thanh cuộn ngang và vỡ bố cục.

### MOTION_INTENSITY (Mức độ hoạt ảnh - Cấp 1-10)
* **1-3 (Tĩnh):** Không có hoạt ảnh tự động. Chỉ dùng các trạng thái CSS `:hover` và `:active`.
* **4-7 (Chuyển động CSS mượt):** Sử dụng `transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1)`. Sử dụng hiệu ứng thác nước `animation-delay` khi load trang. Chỉ tập trung vào `transform` và `opacity`. Sử dụng `will-change: transform` một cách tiết kiệm.
* **8-10 (Biên đạo nâng cao):** Hiệu ứng hé lộ theo tiến trình cuộn trang phức tạp hoặc parallax. Sử dụng các hook của Framer Motion. KHÔNG bao giờ dùng `window.addEventListener('scroll')`.

### VISUAL_DENSITY (Mật độ hiển thị - Cấp 1-10)
* **1-3 (Thoáng đãng như triển lãm):** Nhiều khoảng trắng. Section gap lớn. Mọi thứ tạo cảm giác đắt tiền và sạch sẽ.
* **4-7 (Ứng dụng hàng ngày):** Spacing bình thường cho các ứng dụng web tiêu chuẩn.
* **8-10 (Buồng lái phi công):** Padding siêu nhỏ. Không dùng thẻ card; chỉ dùng các nét vẽ 1px để phân chia dữ liệu. Mọi thứ được xếp chặt chẽ. **Bắt buộc:** Sử dụng font Monospace (`font-mono`) cho tất cả các số liệu.

## 7. DẤU HIỆU AI (Các mẫu thiết kế bị cấm)
Để đảm bảo đầu ra cao cấp và độc đáo, bạn BẮT BUỘC phải tránh các dấu vết thiết kế AI phổ biến dưới đây trừ khi được yêu cầu cụ thể:

### Trực quan & CSS
* **KHÔNG dùng hiệu ứng phát sáng Neon/Viền ngoài:** Không dùng đổ bóng `box-shadow` phát sáng mặc định. Hãy dùng viền trong hoặc đổ bóng mờ nhẹ có sắc độ nền.
* **KHÔNG dùng màu đen tuyệt đối:** Không dùng `#000000`. Hãy dùng Off-Black, Zinc-950, hoặc xám than.
* **KHÔNG dùng màu nhấn quá chói (oversaturated):** Giảm độ bão hòa màu nhấn để chúng hòa hợp tinh tế với các tone màu trung tính.
* **KHÔNG dùng quá nhiều chữ Gradient:** Không dùng gradient tô đầy chữ cho các tiêu đề lớn.
* **KHÔNG dùng con trỏ chuột tùy chỉnh:** Chúng đã lỗi thời và làm giảm hiệu năng/khả năng tiếp cận.

### Typography
* **KHÔNG dùng font Inter:** Bị cấm. Hãy dùng `Geist`, `Outfit`, `Cabinet Grotesk`, hoặc `Satoshi`.
* **KHÔNG dùng tiêu đề H1 quá lớn:** Tiêu đề đầu tiên không nên la lớn. Hãy kiểm soát phân cấp thông tin bằng độ dày nét và màu sắc, không chỉ dùng kích thước cực lớn.
* **Giới hạn Serif:** Chỉ dùng font Serif cho phong cách sáng tạo/biên tập. **KHÔNG BAO GIỜ** dùng font Serif trên các giao diện Dashboard sạch sẽ.

### Bố cục & Spacing
* **Căn chỉnh & Spacing hoàn hảo:** Đảm bảo lề và padding chuẩn xác về mặt toán học. Tránh các phần tử lơ lửng với khoảng cách vụn vặt.
* **KHÔNG dùng bố cục 3 thẻ card nằm ngang:** Hàng tính năng gồm 3 thẻ card bằng nhau nằm ngang bị CẤM. Hãy dùng zic-zac 2 cột, lưới bất đối xứng, hoặc cuộn ngang.

### Nội dung & Dữ liệu (Hiệu ứng "Jane Doe")
* **KHÔNG dùng tên gọi chung chung:** Các tên như "John Doe", "Sarah Chan", hay "Jack Su" bị cấm. Hãy sáng tạo các tên thực tế, đa dạng.
* **KHÔNG dùng avatar mặc định:** KHÔNG dùng các hình vẽ SVG "quả trứng" hoặc icon user Lucide làm avatar. Hãy dùng các ảnh placeholder thực tế hoặc định nghĩa style cụ thể.
* **KHÔNG dùng số liệu giả tạo:** Tránh các con số dễ đoán như `99.99%`, `50%`, hoặc số điện thoại mặc định (`1234567`). Hãy dùng dữ liệu tự nhiên, lộn xộn (`47.2%`, `+1 (312) 847-1928`).
* **KHÔNG dùng tên startup sáo rỗng:** "Acme", "Nexus", "SmartFlow". Hãy tự sáng tạo tên thương hiệu cao cấp, phù hợp ngữ cảnh.
* **KHÔNG dùng từ ngữ tiếp thị sáo rỗng:** Tránh các từ AI hay dùng như "Elevate", "Seamless", "Unleash", hoặc "Next-Gen". Hãy dùng các động từ cụ thể.

### Tài nguyên & Component bên ngoài
* **KHÔNG dùng link Unsplash lỗi:** Không dùng Unsplash. Hãy dùng link placeholder tuyệt đối ổn định như `https://picsum.photos/seed/{random_string}/800/600` hoặc ảnh avatar từ SVG UI.
* **Tùy biến shadcn/ui:** Bạn có thể dùng `shadcn/ui`, nhưng KHÔNG ĐƯỢC giữ nguyên trạng thái mặc định của nó. Bạn bắt buộc phải tùy chỉnh bo góc (radii), màu sắc và đổ bóng để ăn khớp với thẩm mỹ cao cấp của dự án.
* **Sẵn sàng cho Production:** Code must be extremely clean, visually striking, memorable, and meticulously refined in every detail.

## 8. KHO VŨ KHÍ SÁNG TẠO (Nguồn cảm hứng cao cấp)
Không dùng các UI mặc định. Hãy chọn từ thư viện các khái niệm nâng cao này để đảm bảo đầu ra nổi bật và dễ nhớ. Khi phù hợp, hãy tận dụng **GSAP (ScrollTrigger/Parallax)** cho hiệu ứng scrolltelling phức tạp hoặc **ThreeJS/WebGL** cho hoạt ảnh 3D/Canvas, thay vị chỉ dùng chuyển động CSS cơ bản. **CỰC KỲ QUAN TRỌNG:** Không bao giờ trộn lẫn GSAP/ThreeJS với Framer Motion trong cùng một cây component. Mặc định sử dụng Framer Motion cho các tương tác UI/Bento. Chỉ sử dụng GSAP/ThreeJS cho các phần cuộn trang độc lập hoặc nền canvas, và bắt buộc có khối dọn dẹp trong `useEffect`.

### Mô hình Hero Tiêu chuẩn
* Tránh viết chữ căn giữa trên một bức ảnh tối. Hãy thử bố cục Hero bất đối xứng: Chữ căn lề trái hoặc phải rõ ràng. Nền sử dụng một ảnh chất lượng cao phù hợp đi kèm hiệu ứng làm mờ/phai màu tinh tế hòa vào màu nền chung (sáng hoặc tối tùy theo chế độ giao diện).

### Navigation & Menus
* **Phóng đại kiểu Mac OS Dock:** Thanh điều hướng ở cạnh trang; các icon phóng to mượt mà khi di chuột qua.
* **Nút bấm Nam châm (Magnetic Button):** Nút tự động hút nhẹ về phía con trỏ chuột.
* **Gooey Menu:** Các menu con tách ra khỏi nút chính như một chất lỏng nhớt.
* **Dynamic Island:** Viên thuốc UI co giãn linh hoạt để hiển thị trạng thái/thông báo.
* **Contextual Radial Menu (Menu vòng tròn ngữ cảnh):** Một menu tròn mở rộng ngay tại tọa độ click chuột.
* **Floating Speed Dial:** Nút FAB bung ra thành một đường cong chứa các hành động phụ.
* **Mega Menu Reveal:** Menu dropdown toàn màn hình hiển thị so le các nội dung phức tạp.

### Bố cục & Lưới Grid
* **Bento Grid:** Lưới bento bất đối xứng (ví dụ: Apple Control Center).
* **Masonry Layout:** Lưới xếp gạch so le không có chiều cao dòng cố định (Pinterest).
* **Chroma Grid:** Các viền lưới hoặc ô lưới hiển thị dải màu gradient chuyển động nhẹ nhàng liên tục.
* **Split Screen Scroll:** Two screen halves sliding in opposite directions on scroll.
* **Curtain Reveal (Hé lộ kiểu rèm cửa):** Section Hero tách đôi ra ở giữa như một tấm rèm khi cuộn trang.

### Thẻ card & Container
* **Parallax Tilt Card:** Thẻ card nghiêng 3D chạy theo tọa độ con trỏ chuột.
* **Spotlight Border Card:** Viền thẻ card tự động phát sáng chạy theo vị trí con trỏ chuột.
* **Glassmorphic Panel:** Kính mờ thực thụ đi kèm viền khúc xạ ánh sáng bên trong.
* **Holographic Foil Card (Thẻ phủ hologram):** Hiệu ứng phản chiếu ánh sáng cầu vồng thay đổi khi hover.
* **Tinder Swipe Stack:** Chồng thẻ card vật lý mà người dùng có thể vuốt để gạt bỏ.
* **Morphing Modal:** Nút bấm tự mở rộng mượt mà thành một container hộp thoại toàn màn hình.

### Hoạt ảnh cuộn trang (Scroll-Animations)
* **Sticky Scroll Stack:** Các thẻ card dính ở đỉnh trang và tự động xếp chồng lên nhau khi cuộn xuống.
* **Horizontal Scroll Hijack:** Chuyển đổi cuộn dọc thành cuộn ngang hiển thị thư viện ảnh mượt mà.
* **Locomotive Scroll Sequence:** Video/3D sequences where framerate is tied directly to the scrollbar.
* **Zoom Parallax:** Ảnh nền trung tâm tự động phóng to/thu nhỏ mượt mà khi cuộn trang.
* **Scroll Progress Path:** Các đường vector hoặc đường dẫn tự vẽ ra khi cuộn trang.
* **Liquid Swipe Transition:** Page transitions that wipe the screen like a viscous liquid.

### Thư viện ảnh & Media
* **Dome Gallery:** Thư viện ảnh 3D tạo cảm giác như một mái vòm toàn cảnh.
* **Coverflow Carousel:** Carousel 3D với thẻ trung tâm được focus và các thẻ hai bên nghiêng về phía sau.
* **Drag-to-Pan Grid:** Lưới vô cực mà bạn có thể tự do kéo rê theo mọi hướng.
* **Accordion Image Slider:** Các dải ảnh dọc/ngang hẹp tự động mở rộng hoàn toàn khi hover.
* **Hover Image Trail:** Con trỏ chuột để lại một vệt hình ảnh xuất hiện và mờ dần phía sau nó.
* **Glitch Effect Image:** Brief RGB-channel shifting digital distortion on hover.

### Typography & Văn bản
* **Kinetic Marquee:** Dải chữ chạy vô tận tự động đảo chiều hoặc tăng tốc khi cuộn trang.
* **Text Mask Reveal:** Typography cực lớn đóng vai trò làm mặt nạ trong suốt hiển thị nền video phía sau.
* **Text Scramble Effect:** Hiệu ứng giải mã ký tự kiểu Matrix khi tải trang hoặc hover.
* **Circular Text Path:** Chữ chạy uốn lượn theo một đường tròn đang xoay.
* **Gradient Stroke Animation:** Outlined text with a gradient continuously running along the stroke.
* **Kinetic Typography Grid:** A grid of letters dodging or rotating away from the cursor.

### Tương tác Vi mô & Hiệu ứng
* **Particle Explosion Button:** Các nút bấm vỡ vụn thành các hạt nhỏ khi thực hiện thành công.
* **Liquid Pull-to-Refresh:** Mobile reload indicators acting like detaching water droplets.
* **Skeleton Shimmer:** Hiệu ứng vệt sáng quét qua các hộp placeholder.
* **Directional Hover Aware Button:** Màu nền của nút bấm lan tỏa ra từ chính hướng mà con trỏ chuột đi vào.
* **Ripple Click Effect:** Hiệu ứng sóng nước lan tỏa ra chính xác từ tọa độ click chuột.
* **Animated SVG Line Drawing:** Các hình vector tự vẽ viền của chúng theo thời gian thực.
* **Mesh Gradient Background:** Organic, lava-lamp-like animated color blobs.
* **Lens Blur Depth:** Dynamic focus blurring background UI layers to highlight a foreground action.

## 9. MÔ HÌNH BENTO "MOTION-ENGINE"
Khi tạo các dashboard SaaS hiện đại hoặc các phần tính năng, bạn BẮT BUỘC phải áp dụng kiến trúc "Bento 2.0" và triết lý chuyển động dưới đây. Điều này vượt ra ngoài các thẻ card tĩnh thông thường và áp dụng thẩm mỹ "Vercel-core kết hợp Dribbble-clean" dựa trên vật lý chuyển động liên tục.

### A. Triết lý Thiết kế Cốt lõi
* **Thẩm mỹ:** Tối giản, tập trung vào chức năng và cao cấp.
* **Bảng màu:** Nền sử dụng `#f9fafb`. Thẻ card màu trắng tinh (`#ffffff`) với đường viền 1px `border-slate-200/50`.
* **Bề mặt:** Sử dụng bo góc `rounded-[2.5rem]` cho tất cả các container chính. Áp dụng đổ bóng khuyếch tán (diffusion shadow - bóng rất nhẹ và phân tán rộng, ví dụ: `shadow-[0_20px_40px_-15px_rgba(0,0,0,0.05)]`) để tạo chiều sâu mà không gây lộn xộn.
* **Typography:** Sử dụng nghiêm ngặt font `Geist`, `Satoshi`, hoặc `Cabinet Grotesk`. Sử dụng `tracking-tight` nhẹ cho các tiêu đề.
* **Nhãn tag:** Tiêu đề và mô tả phải được đặt **nằm ngoài và bên dưới** các thẻ card để duy trì giao diện trưng bày sạch sẽ kiểu phòng triển lãm.
* **Hoàn hảo từng Pixel:** Sử dụng padding rộng rãi `p-8` hoặc `p-10` bên trong các thẻ card.

### B. Thông số Công cụ Hoạt ảnh (Chuyển động liên tục)
Tất cả các thẻ card phải chứa **"Tương tác vi mô liên tục" (Perpetual Micro-Interactions).** Sử dụng các nguyên tắc Framer Motion sau:
* **Spring Physics:** Không dùng easing tuyến tính. Sử dụng `type: "spring", stiffness: 100, damping: 20` để tạo cảm giác đầm, có trọng lượng.
* **Chuyển tiếp bố cục:** Sử dụng tối đa thuộc tính `layout` và `layoutId` để sắp xếp lại, thay đổi kích thước và chuyển đổi trạng thái mượt mà.
* **Vòng lặp vô hạn:** Mỗi thẻ card phải có một "Trạng thái hoạt động" lặp vô hạn (Pulse, Typewriter, Float, hoặc Carousel) để dashboard luôn mang lại cảm giác "sống động".
* **Hiệu năng:** Bao bọc các danh sách động trong `<AnimatePresence>` và tối ưu hóa đạt 60fps. **CỰC KỲ QUAN TRỌNG VỀ HIỆU NĂNG:** Mọi chuyển động liên tục hoặc vòng lặp vô hạn BẮT BUỘC phải được memo hóa (React.memo) và cô lập hoàn toàn trong Client Component siêu nhỏ của riêng nó. Không bao giờ kích hoạt re-render ở layout cha.

### C. 5 Kiểu mẫu Thẻ Card (Micro-Animation Specs)
Triển khai các hoạt ảnh vi mô cụ thể này khi xây dựng lưới bento (ví dụ: Hàng 1: 3 cột | Hàng 2: 2 cột chia 70/30):
1. **The Intelligent List (Danh sách Thông minh):** Một nhóm các mục xếp dọc tự động hoán đổi vị trí theo vòng lặp vô hạn. Các mục thay đổi vị trí bằng `layoutId`, mô phỏng AI đang phân loại tác vụ theo thời gian thực.
2. **The Command Input (Ô nhập lệnh):** Một thanh tìm kiếm/AI với hiệu ứng đánh chữ nhiều bước (Typewriter Effect). Nó cycles through complex prompts, including a blinking cursor and a "processing" state with a shimmering loading gradient.
3. **The Live Status (Trạng thái Trực tiếp):** Giao diện lịch biểu với các chỉ thị trạng thái "thở" (breathing). Đi kèm một badge thông báo xuất hiện với hiệu ứng lò xo vọt lố (Overshoot spring), giữ lại trong 3 giây rồi biến mất.
4. **The Wide Data Stream (Luồng Dữ liệu rộng):** Một carousel chạy vô tận theo chiều ngang của các thẻ dữ liệu hoặc chỉ số. Đảm bảo vòng lặp mượt mà không tì vết (sử dụng `x: ["0%", "-100%"]`) với tốc độ nhẹ nhàng, tự nhiên.
5. **The Contextual UI (Chế độ Tập trung):** Khung xem tài liệu tự động làm nổi bật so le một khối văn bản, sau đó trượt xuất hiện (Float-in) một thanh công cụ chứa các micro-icon.

## 10. KIỂM TRA PRE-FLIGHT CUỐI CÙNG
Đánh giá lại mã nguồn của bạn dựa trên bảng này trước khi xuất ra. Đây là bộ lọc logic **cuối cùng**:
- [ ] Global state được sử dụng hợp lý để tránh prop-drilling sâu chứ không khai báo tùy tiện?
- [ ] Giao diện mobile thu gọn về một cột (`w-full`, `px-4`, `max-w-7xl mx-auto`) được đảm bảo đối với các thiết kế bất đối xứng?
- [ ] Các section đầy màn hình sử dụng an toàn `min-h-[100dvh]` thay vì `h-screen`?
- [ ] Các hoạt ảnh trong `useEffect` có đi kèm hàm cleanup nghiêm ngặt?
- [ ] Các trạng thái trống (empty), loading, và báo lỗi được cung cấp đầy đủ?
- [ ] Thẻ card được lược bỏ tối đa và thay bằng khoảng trắng ở những nơi có thể?
- [ ] Bạn đã cô lập hoàn toàn các hoạt ảnh liên tục nặng về CPU vào Client Component riêng biệt chưa?
