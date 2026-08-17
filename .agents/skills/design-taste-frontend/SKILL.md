---
name: design-taste-frontend
description: Kỹ năng Frontend chống rập khuôn (anti-slop) cho landing page, portfolio và redesign. Hướng dẫn Agent đọc hiểu yêu cầu (brief), tự suy luận ngôn ngữ thiết kế phù hợp và viết mã giao diện chất lượng cao, không bị rập khuôn bởi các thiên kiến mặc định của AI. Áp dụng hệ thống thiết kế thực tế khi cần, ưu tiên kiểm tra (audit-first) khi redesign, và chạy bảng tự kiểm tra (pre-flight check) nghiêm ngặt.
---

# tasteskill: Kỹ Năng Thiết Kế Frontend Chống Rập Khuôn (Anti-Slop)

> Dành riêng cho trang landing page, portfolios, và thiết kế lại (redesigns). Không áp dụng cho dashboard thô, bảng dữ liệu hoặc UI sản phẩm nhiều bước.
> Mọi quy tắc dưới đây là **mang tính ngữ cảnh**. Không có quy tắc nào tự động kích hoạt. Trước tiên hãy đọc hiểu yêu cầu (brief), sau đó chỉ áp dụng những gì phù hợp.

---

## 0. ĐỌC HIỂU YÊU CẦU (Brief Inference - Đọc vị bối cảnh trước khi làm bất cứ điều gì)

Trước khi chạm vào code hoặc tinh chỉnh các nút vặn thẩm mỹ, **hãy suy luận xem người dùng thực sự muốn gì**. Hầu hết các sản phẩm thiết kế của AI đều tệ vì mô hình nhảy ngay vào một giao diện mặc định thay vì đọc hiểu ngữ cảnh của dự án.

### 0.A Đọc các tín hiệu này trước
1. **Loại trang** - landing (SaaS / tiêu dùng / agency / sự kiện), portfolio (nhà phát triển / nhà thiết kế / studio sáng tạo), redesign (giữ nguyên cấu trúc vs cải tổ toàn diện), editorial / blog.
2. **Từ khóa cảm xúc (vibe)** mà người dùng sử dụng - "minimalist" (tối giản), "calm" (tĩnh lặng), "Linear-style" (phong cách Linear), "Awwwards", "brutalist", "premium consumer" (tiêu dùng cao cấp), "Apple-y", "playful" (vui tươi), "serious B2B" (doanh nghiệp nghiêm túc), "editorial" (biên tập/tạp chí), "agency-y", "glassy" (hiệu ứng kính), "dark tech" (công nghệ tối).
3. **Tín hiệu tham chiếu** - các URL họ liên kết, ảnh chụp màn hình họ dán, sản phẩm họ đặt tên, thương hiệu họ đang cạnh tranh.
4. **Khán giả** - ban mua sắm B2B doanh nghiệp vs người tiêu dùng chú trọng thẩm mỹ vs nhà tuyển dụng quét portfolio. Đối tượng khán giả sẽ quyết định thẩm mỹ, không phải sở thích cá nhân của bạn.
5. **Tài sản thương hiệu hiện có** - logo, màu sắc, font chữ, hình ảnh. Đối với redesign, đây là nguyên liệu bắt đầu bắt buộc, không phải tùy chọn (xem Phần 11).
6. **Giới hạn ngầm** - khán giả ưu tiên khả năng tiếp cận (accessibility-first), khu vực công, ngành nghề bị kiểm soát ngặt nghèo, thương mại điện tử cần tạo lòng tin, sản phẩm cho trẻ em. Những giới hạn này sẽ **GHI ĐÈ** mọi sở thích thẩm mỹ khác.

### 0.B Trả về một dòng "Nhận Định Thiết Kế" (Design Read) trước khi sinh code
Trước khi viết bất kỳ dòng code nào, hãy tuyên bố trong một dòng duy nhất: **"Reading this as: \<page kind> for \<audience>, with a \<vibe> language, leaning toward \<design system or aesthetic family>."** (Đọc vị bối cảnh: \<loại trang> dành cho \<đối tượng>, với ngôn ngữ thiết kế \<vibe>, hướng tới \<hệ thống thiết kế hoặc phong cách thẩm mỹ>.)

Ví dụ:
- *"Reading this as: B2B SaaS landing for technical buyers, with a Linear-style minimalist language, leaning toward Tailwind utilities + Geist + restrained motion."*
- *"Reading this as: solo designer portfolio for hiring managers, with an editorial / kinetic-type language, leaning toward native CSS + scroll-driven animation + custom typography."*
- *"Reading this as: redesign of a public-sector service site, with a trust-first language, leaning toward GOV.UK Frontend or USWDS."*

### 0.C Nếu yêu cầu mơ hồ, hãy hỏi đúng một câu, không đoán mò
Hỏi chính xác **một** câu hỏi làm rõ - không bao giờ hỏi một trùm nhiều câu hỏi - và chỉ hỏi khi nhận định thiết kế thực sự bị phân nhánh. Ví dụ: *"Should this feel closer to Linear-clean or Awwwards-experimental?"* (Trải nghiệm này nên thiên về phong cách Linear sạch sẽ hay thử nghiệm dạng Awwwards?)

Nếu bạn có thể tự tin suy luận từ ngữ cảnh, **không được hỏi**. Chỉ cần tuyên bố dòng Nhận định thiết kế (Design Read) và tiến hành.

### 0.D Kỷ luật chống mặc định (Anti-Default Discipline)
Tuyệt đối không sử dụng các lối thiết kế mặc định rập khuôn của AI: các dải màu gradient tím AI, tiêu đề hero căn giữa trên nền lưới tối, ba thẻ tính năng giống hệt nhau, hiệu ứng kính mờ (glassmorphism) tràn lan, hoạt ảnh lặp lại vô tận ở mọi nơi, và cặp font Inter + slate-900. Đây là những thiết lập mặc định của LLM. Hãy chủ động vượt qua chúng dựa trên nhận định thiết kế.

---

## 1. BA NÚT VẶN CẤU HÌNH (Core Configuration)

Sau khi có nhận định thiết kế, hãy thiết lập ba nút vặn cấu hình. Mọi quyết định về bố cục (layout), chuyển động (motion) và mật độ (density) hiển thị sau đó đều bị chi phối bởi các tham số này.

* **`DESIGN_VARIANCE: 8`** - 1 = Đối xứng hoàn hảo, 10 = Bất đối xứng đầy tính nghệ thuật (Artsy Chaos)
* **`MOTION_INTENSITY: 6`** - 1 = Tĩnh hoàn toàn, 10 = Chuyển động điện ảnh / Vật lý phức tạp
* **`VISUAL_DENSITY: 4`** - 1 = Airy / Thoáng đãng như triển lãm nghệ thuật, 10 = Bảng điều khiển dày đặc thông tin

**Baseline (Đường cơ sở mặc định):** `8 / 6 / 4`. Sử dụng các giá trị này trừ khi nhận định thiết kế ghi đè chúng. Không yêu cầu người dùng chỉnh sửa file này - các ghi đè sẽ diễn ra thông qua hội thoại.

### 1.A Suy luận nút vặn (design read → dial values)
| Tín hiệu bối cảnh | VARIANCE | MOTION | DENSITY |
|---|---|---|---|
| "minimalist / clean / calm / editorial / Linear-style" | 5-6 | 3-4 | 2-3 |
| "premium consumer / Apple-y / luxury / brand" | 7-8 | 5-7 | 3-4 |
| "playful / wild / Dribbble / Awwwards / experimental / agency" | 9-10 | 8-10 | 3-4 |
| "landing page / portfolio / marketing site (mặc định)" | 7-9 | 6-8 | 3-5 |
| "trust-first / public-sector / regulated / accessibility-critical" | 3-4 | 2-3 | 4-5 |
| "redesign - preserve" (giữ nguyên cấu trúc) | khớp trang cũ | +1 | khớp trang cũ |
| "redesign - overhaul" (thay đổi toàn diện) | +2 | +2 | khớp trang cũ |

### 1.B Thiết lập sẵn theo Use-Case (Use-Case Presets)
| Trường hợp sử dụng | VARIANCE | MOTION | DENSITY |
|---|---|---|---|
| Landing (SaaS, đại chúng) | 7 | 6 | 4 |
| Landing (Agency / sáng tạo) | 9 | 8 | 3 |
| Landing (Tiêu dùng cao cấp) | 7 | 6 | 3 |
| Portfolio (Nhà thiết kế / studio) | 8 | 7 | 3 |
| Portfolio (Nhà phát triển) | 6 | 5 | 4 |
| Biên tập (Editorial) / Blog | 6 | 4 | 3 |
| Dịch vụ công (Public-sector service) | 3 | 2 | 5 |
| Redesign - preserve (giữ nguyên cấu trúc) | khớp | khớp+1 | khớp |
| Redesign - overhaul (cải tổ toàn diện) | +2 | +2 | khớp |

### 1.C Cách các nút vặn điều khiển đầu ra
Sử dụng các giá trị này (hoặc các giá trị được ghi đè) làm biến toàn cục. Các tham chiếu chéo xuyên suốt tài liệu này sử dụng chính xác các tên biến này - không tự chế ra các bí danh khác như `LAYOUT_VARIANCE` hay `ANIM_LEVEL`.

---

## 2. BẢN ĐỒ HỆ THỐNG THIẾT KẾ (Brief → Design System Map)

Khi đã có nhận định thiết kế (Phần 0) và các nút vặn cấu hình (Phần 1), hãy chọn nền tảng phù hợp. Không tự chế CSS cho những thành phần đã có gói thư viện chính thức. Không coi một xu hướng thẩm mỹ là một hệ thống thiết kế chính thức.

### 2.A Khi nào cần sử dụng hệ thống thiết kế thực sự (sử dụng các package chính thức)
| Yêu cầu bối cảnh... | Sử dụng hệ thống | Lý do |
|---|---|---|
| Microsoft / enterprise SaaS / dashboards | `@fluentui/react-components` hoặc `@fluentui/web-components` | Fluent UI chính thức, bộ token Microsoft, khả năng tiếp cận hoàn thiện |
| Giao diện kiểu Google, sản phẩm hơi hướng Material | `@material/web` + Material 3 tokens | Chính thức, hỗ trợ Material Theming |
| Carbon B2B của IBM / enterprise analytics | `@carbon/react` + `@carbon/styles` | Carbon chính thức, các pattern mật độ dữ liệu cao |
| Giao diện ứng dụng Shopify | `polaris.js` web components / Polaris React | Bắt buộc đối với UI quản trị Shopify |
| Sản phẩm kiểu Atlassian / Jira | `@atlaskit/*` + `@atlaskit/tokens` | Atlassian DS chính thức |
| Trang devtool / cộng đồng kiểu GitHub | `@primer/css` hoặc `@primer/react-brand` | Primer chính thức; bản Brand dành cho marketing |
| Dịch vụ công của Anh (UK) | `govuk-frontend` | Được yêu cầu về mặt luật pháp / quy chuẩn |
| Dịch vụ công của Mỹ / yêu cầu độ tin cậy cao | `uswds` | Tương tự |
| Trang local-business nhanh / MVP của agency | Bootstrap 5.3 | Đơn giản, nhanh, ổn định |
| Nền tảng React hiện đại chú trọng a11y | `@radix-ui/themes` | Các component nguyên bản + theme được tinh chế |
| SaaS hiện đại nơi bạn sở hữu code của component | shadcn/ui (`npx shadcn@latest add ...`) | Sở hữu mã nguồn component, dễ tùy biến; cấm giữ nguyên trạng thái mặc định không chỉnh sửa |
| SaaS hiện đại / AI marketing dựa trên Tailwind | Tailwind v4 utilities + `dark:` variant | Mặc định cho các dự án độc lập + đội ngũ nhỏ |

**Quy tắc trung thực:** nếu bối cảnh yêu cầu một trong các hệ thống trên, hãy cài đặt và sử dụng package **chính thức**. Không tự code lại CSS của chúng bằng tay. Không import token của hệ thống rồi ghi đè 90% chúng.

**Mỗi dự án chỉ dùng một hệ thống.** Không trộn lẫn Fluent React với Carbon trong cùng một cây component. Không import component của shadcn/ui vào một ứng dụng Material 3.

### 2.B Khi yêu cầu là một phong cách thẩm mỹ, không phải một hệ thống thiết kế
Đối với các hướng đi này, **không có package chính thức duy nhất**. Hãy xây dựng bằng CSS thuần + Tailwind + một thư viện component được bảo trì tốt. Hãy trung thực ghi rõ trong comment code phần nào là lấy cảm hứng vs phần nào là tài liệu chính thức.

| Thẩm mỹ | Triển khai trung thực |
|---|---|
| Kính mờ (Glassmorphism / "frosted glass") | `backdrop-filter`, viền nhiều lớp, lớp phủ highlight. Cung cấp fallback màu đặc cho `prefers-reduced-transparency`. |
| Bento (Lưới dạng ô của Apple) | CSS Grid với kích thước các ô hỗn hợp. Không thư viện đơn lẻ nào sở hữu cái này. |
| Brutalism | CSS thuần, font chữ monospace, đường viền thô. Không dùng thư viện. |
| Tạp chí / Biên tập (Editorial) | Font chữ Serif, lưới bất đối xứng, khoảng trắng rộng rãi. Không dùng thư viện. |
| Dark tech / hacker | Font Mono + màu nhấn neon, các họa tiết terminal. Không dùng thư viện. |
| Gradient cực quang / dạng lưới (Aurora / mesh) | SVG hoặc các radial gradient xếp lớp. Không dùng thư viện. |
| Kinetic typography (Chữ động) | CSS animation thuần, scroll-driven animations, GSAP cho các hiệu ứng cuộn. Không dùng thư viện. |
| **Apple Liquid Glass** | Apple chỉ tài liệu hóa phong cách này cho các nền tảng của Apple. **Không có file `liquid-glass.css` chính thức.** Các triển khai trên web là bản xấp xỉ dùng `backdrop-filter` + viền xếp lớp + highlight. Ghi nhãn rõ ràng là bản xấp xỉ (approximation) trong comment. |

---

## 3. KIẾN TRÚC MẶC ĐỊNH & QUY ƯỚC

Trừ khi nhận định thiết kế chọn một hệ thống thiết kế thực tế (Phần 2.A), đây là các thiết lập mặc định:

### 3.A Stack công nghệ
* **Framework:** React hoặc Next.js. Mặc định sử dụng Server Components (RSC).
  * **AN TOÀN RSC:** Global state CHỈ hoạt động trong Client Components. Trong Next.js, hãy bao bọc các provider trong một component `"use client"`.
  * **CÔ LẬP TƯƠNG TÁC:** Bất kỳ component nào dùng Motion, trình lắng nghe sự kiện cuộn (scroll), hoặc vật lý con trỏ (pointer physics) BẮT BUỘC phải là một component lá cô lập có dòng `'use client'` ở ngay đầu file. Server Components chỉ dùng để render bố cục tĩnh.
* **Styling:** **Tailwind v4** (mặc định). Chỉ dùng Tailwind v3 khi dự án hiện tại yêu cầu.
  * Với v4: KHÔNG sử dụng plugin `tailwindcss` trong `postcss.config.js`. Hãy sử dụng `@tailwindcss/postcss` hoặc plugin Vite.
* **Animation:** **Motion** (tên gọi mới của Framer Motion). Nhập từ thư viện `motion/react` (`import { motion } from "motion/react"`). Gói `framer-motion` vẫn hoạt động như một bí danh cũ - hãy ưu tiên dùng `motion/react` trong code mới.
* **Font chữ:** Luôn sử dụng `next/font` (Next.js) hoặc tự host bằng `@font-face` + `font-display: swap`. Không bao giờ liên kết Google Fonts qua thẻ `<link>` ở môi trường production.

### 3.B Quản lý trạng thái (State)
* Sử dụng `useState` / `useReducer` local cho UI độc lập.
* Chỉ dùng global state khi thực sự cần tránh prop-drilling quá sâu - Zustand, Jotai, hoặc React context.
* **KHÔNG BAO GIỜ** sử dụng `useState` để theo dõi các giá trị biến đổi liên tục do thao tác của người dùng (vị trí chuột, tiến trình cuộn trang, vật lý con trỏ, hover nam châm). Bắt buộc sử dụng các biến chuyển động của Motion: `useMotionValue` / `useTransform` / `useScroll`. `useState` sẽ kích hoạt re-render cây React trên mỗi thay đổi và gây lag nghiêm trọng trên thiết bị di động.

### 3.C Thư viện Icon
* **Thư viện được phép (ưu tiên từ trên xuống):** `@phosphor-icons/react`, `hugeicons-react`, `@radix-ui/react-icons`, `@tabler/icons-react`.
* **Không khuyến khích:** `lucide-react`. Chỉ chấp nhận khi người dùng yêu cầu rõ ràng hoặc dự án đã phụ thuộc sẵn vào nó.
* **KHÔNG BAO GIỜ tự viết mã SVG cho các icon.** Nếu thiếu ký hiệu, hãy cài thêm thư viện thứ hai hoặc lắp ghép từ các hình học cơ bản - không tự vẽ các đường dẫn icon (path) từ đầu.
* **Mỗi dự án chỉ dùng một họ icon.** Không trộn lẫn icon Phosphor với Lucide trong cùng một cây component.
* **Đồng bộ `strokeWidth` trên toàn trang** (ví dụ: luôn dùng `1.5` hoặc `2.0`).

### 3.D Quy tắc Emoji
Mặc định không khuyến khích sử dụng emoji trong code, markup và văn bản hiển thị. Hãy thay thế bằng các icon từ thư viện. **Ngoại lệ:** chỉ cho phép dùng emoji khi người dùng yêu cầu rõ ràng một trải nghiệm vui tươi, dạng chat hoặc mạng xã hội di động - và ngay cả khi đó, hãy dùng cực kỳ tiết chế và có tính toán.

### 3.E Spacing & Khả năng đáp ứng bố cục (Responsiveness)
* Đồng bộ các breakpoint chuẩn (`sm 640`, `md 768`, `lg 1024`, `xl 1280`, `2xl 1536`).
* Giới hạn chiều rộng trang bằng `max-w-[1400px] mx-auto` hoặc `max-w-7xl`.
* **Độ ổn định của Viewport:** KHÔNG BAO GIỜ sử dụng `h-screen` cho các phần Hero đầy màn hình. LUÔN LUÔN dùng `min-h-[100dvh]` để ngăn chặn lỗi giật bố cục nghiêm trọng trên trình duyệt di động (do thanh địa chỉ của iOS Safari co giãn).
* **CSS Grid thay vì Flexbox thủ công:** KHÔNG BAO GIỜ sử dụng flexbox tính toán phần trăm phức tạp (`w-[calc(33%-1rem)]`). LUÔN LUÔN sử dụng CSS Grid (`grid grid-cols-1 md:grid-cols-3 gap-6`).

### 3.F Xác thực thư viện phụ thuộc (Bắt buộc)
Trước khi import BẤT KỲ thư viện bên thứ ba nào, bạn bắt buộc phải kiểm tra `package.json`. Nếu thư viện chưa được cài đặt, bạn phải hiển thị lệnh cài đặt trước. **Không bao giờ** tự giả định một thư viện đã tồn tại.

---

## 4. CHỈ THỊ KỸ THUẬT THIẾT KẾ (Chống Thiên Kiến AI)

Các mô hình LLM thường có thiên kiến tạo ra các thiết kế sáo rỗng. Hãy chủ động ghi đè các lỗi mặc định này. Mỗi quy tắc dưới đây đều có đường dẫn ghi đè dựa trên ngữ cảnh.

### 4.1 Typography (Hệ thống chữ)
* **Display / Tiêu đề chính:** Mặc định sử dụng `text-4xl md:text-6xl tracking-tighter leading-none`.
* **Body / Văn bản thường:** Mặc định sử dụng `text-base text-gray-600 leading-relaxed max-w-[65ch]`.
* **Lựa chọn font Sans:**
  * **Không khuyến khích dùng làm mặc định:** `Inter`. Hãy ưu tiên chọn `Geist`, `Outfit`, `Cabinet Grotesk`, `Satoshi`, hoặc một font serif phù hợp thương hiệu trước.
  * **Ngoại lệ:** Font Inter được chấp nhận khi người dùng yêu cầu rõ ràng một giao diện trung tính, tiêu chuẩn hoặc phong cách Linear sạch sẽ, hoặc khi bối cảnh là trang dịch vụ công / trang web ưu tiên tuyệt đối khả năng tiếp cận (a11y).
* **Các cặp font khuyên dùng:** `Geist` + `Geist Mono`, `Satoshi` + `JetBrains Mono`, `Cabinet Grotesk` + `Inter Tight`, `GT America` + `IBM Plex Mono`.

* **KỶ LUẬT FONT SERIF (CỰC KỲ KHÔNG KHUYẾN KHÍCH DÙNG LÀM MẶC ĐỊNH):**
  * Font Serif **cực kỳ không được khuyến khích dùng làm font chữ mặc định cho bất kỳ dự án nào.** Lý do "nó mang lại cảm giác sáng tạo / cao cấp / biên tập" KHÔNG phải là lý do chính đáng để chọn font serif. Thiên kiến mặc định của AI cho rằng "cứ yêu cầu sáng tạo = dùng font serif" là dấu hiệu nhận biết AI phổ biến nhất trong các bài kiểm tra thực tế.
  * **Font Serif chỉ được chấp nhận khi MỘT trong các điều kiện sau đây đúng một cách rõ ràng:**
    - Yêu cầu của thương hiệu nêu đích danh một font serif cụ thể, HOẶC
    - Phong cách thẩm mỹ thực sự mang tính biên tập / xa xỉ / xuất bản / tư liệu cổ / thủ công / vintage VÀ bạn có thể lập luận rõ ràng tại sao font serif cụ thể này lại khớp với thương hiệu đó.
  * Đối với tất cả trường hợp còn lại (creative agency, design studio, thương hiệu hiện đại, hàng tiêu dùng cao cấp, portfolio, lifestyle), **hãy mặc định sử dụng font sans-serif hiển thị** (Geist Display, ABC Diatype, Söhne Breit, Cabinet Grotesk Display, Migra Sans, GT Walsheim, Inter Display, PP Neue Montreal). Các font sans display không hề "nhạt nhẽo" — chúng là lựa chọn mặc định tương tự như màu đen trong giới thời trang.
  * **QUY TẮC NHẤN MẠNH (Related):** Khi bạn muốn nhấn mạnh một từ bên trong một headline lớn (ví dụ kiểu chữ động "and `spatial` design"), hãy sử dụng **chữ nghiêng (italic) hoặc chữ đậm (bold) của CHÍNH font chữ đó**. KHÔNG ĐƯỢC chèn một từ font serif ngẫu nhiên vào giữa tiêu đề font sans (hoặc ngược lại) chỉ để tạo điểm nhấn trực quan giả tạo. Việc pha trộn các họ font chữ trong cùng một từ nhấn mạnh trông rất thiếu chuyên nghiệp. Nhấn mạnh bằng chữ nghiêng/đậm của cùng một họ font chữ mới là hướng đi đúng đắn.
  * **Các font serif bị CẤM TUYỆT ĐỐI dùng làm mặc định:** `Fraunces` và `Instrument_Serif` (hai display serif mà AI thích dùng nhất).
  * **Nếu việc dùng font serif là hợp lý** (hiếm gặp, theo các quy tắc trên), hãy luân phiên chọn từ pool sau, KHÔNG dùng lại cùng một font serif cho các dự án liên tục: PP Editorial New, GT Sectra Display, Cardinal Grotesque, Reckless Neue, Tiempos Headline, Recoleta, Cormorant Garamond, Playfair Display, EB Garamond, IvyPresto, Migra, Editorial Old, Saol Display, Söhne Breit Kursiv, Domaine Display, Canela, Schnyder, Tobias, NB Architekt, ITC Galliard.

* **KHOẢNG CÁCH CHỮ IN NGHIÊNG CÓ ĐUÔI (Descender Clearance - Bắt buộc):** Khi dùng chữ in nghiêng (italic) cho tiêu đề lớn hiển thị và từ đó chứa các chữ cái có đuôi hướng xuống (`y g j p q`), các thuộc tính `leading-[1]` hoặc `leading-none` sẽ làm mất nét của phần đuôi chữ. Bạn bắt buộc phải dùng `leading-[1.1]` tối thiểu và thêm `pb-1` hoặc `mb-1` dự phòng trên thẻ bao ngoài. Hãy kiểm tra kỹ tất cả chữ in nghiêng trong tiêu đề hiển thị trước khi bàn giao.
### 4.2 Cân chỉnh Màu sắc (Color Calibration)
* Sử dụng tối đa 1 màu nhấn (accent color). Giữ độ bão hòa màu (saturation) dưới 80% theo mặc định.
* **QUY TẮC MÀU TÍM NEON (THE LILA RULE):** Thẩm mỹ tím/xanh phát sáng dạng AI bị CẤM NGHIÊM NGẶT làm mặc định. Không có hiệu ứng phát sáng nút màu tím tự động, không dùng dải màu neon ngẫu nhiên. Hãy sử dụng các màu nền trung tính tuyệt đối (Zinc / Slate / Stone) đi kèm một màu nhấn duy nhất có tương phản cao (Emerald, Electric Blue, Deep Rose, Burnt Orange, v.v.).
  * **Ngoại lệ:** Nếu thương hiệu hoặc yêu cầu chỉ rõ màu tím / violet / hoa cà, hãy áp dụng nó. Nhưng thực hiện với tính toán kỹ: bảng màu nhất quán, màu trung tính hài hòa, dải màu được tiết chế. Không dùng gradient màu tím AI rẻ tiền.
* **Mỗi dự án chỉ dùng một bảng màu.** Không thay đổi tùy tiện giữa xám ấm (warm grays) và xám lạnh (cool grays) trong cùng một trang web.
* **KHOÁ NHẤT QUÁN MÀU SẮC (Color Consistency Lock - Bắt buộc):** Một khi đã chọn màu nhấn cho trang, màu đó phải được dùng thống nhất trên **TOÀN BỘ** trang. Một trang tông xám ấm với màu nhấn lục bảo (Emerald) thì không được đột ngột xuất hiện nút bấm màu xanh dương ở phần 7. Một trang tông hồng nhấn (Rose) thì không được xuất hiện thẻ trạng thái màu xanh teal ở chân trang (footer). Hãy chọn một màu nhấn, khóa nó lại và tự kiểm tra (audit) từng component trước khi bàn giao.

* **CẤM BẢNG MÀU THỦ CÔNG/TIÊU DÙNG CAO CẤP MẶC ĐỊNH (Premium-Consumer Palette Ban - Bắt buộc):**
  * Đối với các yêu cầu tiêu dùng cao cấp (nồi niêu xoong chảo, chăm sóc sức khỏe, thủ công, xa xỉ, DTC đồ gia dụng, v.v.), lựa chọn mặc định sáo rỗng của AI là **kem/ngà ấm + đồng/đất sét/đỏ oxblood/vàng ochre + chữ tối màu espresso/mực ấm**. Các họ màu hex cụ thể bị CẤM dùng làm mặc định cho nền và màu nhấn:
    - Nền: `#f5f1ea`, `#f7f5f1`, `#fbf8f1`, `#efeae0`, `#ece6db`, `#faf7f1`, `#e8dfcb` (tất cả các tông "warm paper / cream / chalk / bone")
    - Màu nhấn: `#b08947`, `#b6553a`, `#9a2436`, `#9c6e2a`, `#bc7c3a`, `#7d5621` (tất cả các tông "brass / clay / oxblood / ochre")
    - Chữ: `#1a1714`, `#1a1814`, `#1b1814` (tất cả các tông "espresso / warm near-black")
  * Bảng màu này bị CẤM dùng làm mặc định cho hàng tiêu dùng cao cấp. Mọi trang web tiêu dùng cao cấp do AI tạo ra đều dùng chung công thức này, khiến thương hiệu bị lu mờ.
  * **Các giải pháp thay thế mặc định (Luân phiên chọn, không dùng trùng nhau):**
    - **Cold Luxury (Xa xỉ lạnh):** xám bạc + chrome + khói mờ (như Tesla, Apple Watch Hermes không kèm da)
    - **Forest (Rừng sâu):** xanh lá sâu + xương ấm + màu nhấn hổ phách (như Filson, Patagonia cao cấp)
    - **Black and Tan (Đen và Rám nắng):** màu off-black thực thụ + màu da bò/rám nắng ấm, tạo tương phản sắc nét, không dùng kem
    - **Cobalt + Cream (Cobalt + Kem):** xanh cobalt đậm tương phản trên một màu nền trung tính duy nhất, không dùng màu đồng
    - **Terracotta + Slate (Đất nung + Đá):** màu gạch nung ấm đặt trên xám đá lạnh, không dùng màu đồng
    - **Olive + Brick + Paper (Olive + Gạch + Giấy):** màu olive dịu kết hợp màu nhấn đỏ gạch
    - **Pure monochrome + single saturated pop (Đơn sắc + một màu nhấn cực mạnh):** màu off-white + off-black + một màu nhấn rực rỡ duy nhất (electric blue, emerald, hot pink, v.v.)
  * **Quy tắc luân chuyển bảng màu:** nếu dự án tiêu dùng cao cấp trước đó bạn dùng họ màu kem+đồng, dự án này BẮT BUỘC phải dùng một họ màu khác. Không ship cùng một bảng màu thủ công ấm áp hai lần liên tiếp.
  * **Ngoại lệ:** Bảng màu kem+đồng+espresso chỉ được chấp nhận khi yêu cầu thương hiệu nêu đích danh các màu đó, hoặc thương hiệu thực sự mang tính cổ điển / thủ công / mỹ nghệ VÀ bạn lập luận được tại sao bảng màu cụ thể này lại khớp với thương hiệu đó. Việc tự động chọn nó chỉ vì "đây là trang bán nồi niêu" là bị cấm.

### 4.3 Đa dạng hóa Bố cục (Layout Diversification)
* **CHỐNG THIÊN KIẾN CĂN GIỮA (Anti-Center Bias):** Bố cục Hero / H1 căn giữa bị tránh khi `DESIGN_VARIANCE > 4`. Hãy dùng bố cục chia đôi màn hình (Split Screen 50/50), "chữ căn trái / ảnh/asset căn phải", khoảng trắng bất đối xứng, hoặc các cấu trúc cuộn ghim (scroll-pinned).
* **Ngoại lệ:** Căn giữa Hero được chấp nhận cho các trang biên tập / tuyên ngôn (manifesto) / thông báo ra mắt nơi thông điệp chữ chính là thiết kế.

### 4.4 Chất liệu, Đổ bóng, Thẻ card (Materiality, Shadows, Cards)
* Chỉ dùng thẻ card khi độ nổi (elevation) thực sự thể hiện sự phân cấp thông tin. Ngược lại, hãy gom nhóm bằng `border-t`, `divide-y`, hoặc khoảng trắng (negative space).
* Khi dùng đổ bóng shadow, hãy pha sắc độ bóng trùng với màu nền. Không dùng bóng đổ đen tuyệt đối trên nền sáng.
* Với `VISUAL_DENSITY > 7`: CẤM dùng các container thẻ card chung chung. Các số liệu dữ liệu cần được thở trong bố cục phẳng, thoáng.
* **KHÓA NHẤT QUÁN HÌNH KHỐI (Shape Consistency Lock - Bắt buộc):** Chọn MỘT thang đo bo góc (corner-radius) cho toàn bộ trang và tuân thủ nó. Các lựa chọn: bo góc sắc nhọn (radius 0), bo góc mềm mại (radius 12-16px), bo góc tròn viên thuốc (full radius cho các phần tử tương tác). Hệ thống hỗn hợp chỉ được phép khi có quy tắc rõ ràng (ví dụ: "nút bấm bo tròn viên thuốc, thẻ card bo góc 16px, ô nhập liệu bo góc 8px") và quy tắc đó phải được áp dụng ở mọi nơi. Việc dùng nút bấm tròn xoe trong một bố cục toàn thẻ card góc vuông, hoặc dùng thẻ card vuông chằn chặn trên trang toàn nút bấm bo tròn viên thuốc là thiết kế bị lỗi.

### 4.5 Các trạng thái tương tác của UI (Interactive UI States)
AI thường mặc định chỉ sinh ra "trạng thái tĩnh thành công". Bạn bắt buộc phải triển khai đầy đủ chu trình:
* **Tải dữ liệu (Loading):** Dùng skeleton loader khớp chính xác với hình dáng của bố cục đích. Tránh dùng vòng quay spinner tròn mặc định.
* **Trạng thái Trống (Empty States):** Được sắp xếp đẹp mắt; chỉ dẫn rõ cách tạo dữ liệu.
* **Trạng thái Lỗi (Error States):** Báo lỗi nội dòng rõ ràng (đối với form), hoặc theo ngữ cảnh (chỉ dùng toast cho thông báo tạm thời).
* **Phản hồi lực nhấn (Tactile Feedback):** Khi `:active`, dùng hiệu ứng `-translate-y-[1px]` hoặc `scale-[0.98]` để mô phỏng lực nhấn nút vật lý.
* **KIỂM TRA TƯƠNG PHẢN NÚT BẤM (Button Contrast Check - Bắt buộc, a11y):** Trước khi bàn giao bất kỳ nút bấm nào, hãy xác minh chữ trên nút bấm hiển thị rõ ràng trên nền nút. Nút trắng + chữ trắng, nút CTA `bg-white` với chữ `text-white`, nút trong suốt đặt trên nền trang mà không có viền → tất cả đều bị CẤM. Tự kiểm tra mọi CTA: tỷ lệ tương phản đạt tiêu chuẩn WCAG AA tối thiểu (4.5:1 cho văn bản thường, 3:1 cho chữ lớn 18px+). Quy tắc tương tự áp dụng cho nút bấm dạng ghost button đặt trên ảnh nền (hãy dùng một lớp phủ mờ, lớp scrim hoặc viền stroke).
* **CẤM CTA BẤT ĐỐI XỨNG CO DÒNG (CTA Button Wrap Ban - Bắt buộc):** Chữ trên nút CTA **bắt buộc phải nằm trên 1 dòng duy nhất trên desktop**. Nếu một nhãn như "XEM CÁC DỰ ÁN ĐÃ CHỌN" bị xuống dòng thành 2 hoặc 3 dòng, nút bấm đó bị lỗi. Khắc phục bằng cách: HOẶC rút ngắn nhãn (tối đa 3 từ cho CTA chính, lý tưởng nhất là 1-2 từ) HOẶC mở rộng chiều rộng của nút bấm (không giới hạn `max-width` của nút bấm một cách khiên cưỡng). Chữ nút bấm bị xuống dòng trên desktop là lỗi tự kiểm tra trước bay (Pre-Flight Fail).
* **KHÔNG TRÙNG LẶP Ý ĐỒ CTA (No Duplicate CTA Intent - Bắt buộc):** Hai nút CTA có cùng một mục đích hành động trên một trang là lỗi Pre-Flight Fail. Ví dụ về cùng một ý đồ: "Liên hệ ngay" + "Gửi lời nhắn" + "Trò chuyện" + "Bắt đầu dự án" + "Kết nối" = đều là ý đồ "liên hệ" → chọn DUY NHẤT một nhãn và dùng thống nhất ở mọi vị trí (thanh điều hướng, phần hero, chân trang). Tương tự cho "Dùng thử miễn phí" + "Bắt đầu ngay" + "Đăng ký miễn phí" (cùng ý đồ đăng ký tài khoản) và "Xem sản phẩm" + "Khám phá dự án" + "Duyệt danh mục" (cùng ý đồ xem portfolio). Mỗi ý đồ hành động chỉ dùng một nhãn chữ duy nhất.
* **KIỂM TRA TƯƠNG PHẢN FORM (Form Contrast Check - Bắt buộc, a11y):** Các trường nhập liệu, chữ gợi ý (placeholder), viền focus, chữ hướng dẫn và chữ báo lỗi đều phải đạt độ tương phản tiêu chuẩn WCAG AA trên nền của section đó. Chữ placeholder mờ nhạt trên ô nhập màu xám nhạt, trường nhập liệu màu trắng đặt trên nền section màu trắng, nhãn của form có độ tương phản dưới 4.5:1 → tất cả đều bị CẤM. Hãy tự kiểm tra mọi form trước khi bàn giao.

### 4.6 Các mẫu Form & Dữ liệu (Data & Form Patterns)
* Nhãn (label) đặt TRÊN ô nhập. Văn bản hướng dẫn (helper text) là tùy chọn nhưng phải có trong markup. Chữ báo lỗi đặt DƯỚI ô nhập. Khoảng cách tiêu chuẩn `gap-2` cho các khối ô nhập.
* Không bao giờ dùng placeholder thay thế cho nhãn.

### 4.7 Kỷ luật Bố cục (Layout Discipline - Các quy tắc cứng. Vi phạm bất kỳ điều nào là bàn giao sản phẩm lỗi)

* **Hero BẮT BUỘC phải nằm trọn trong khung nhìn đầu tiên (initial viewport).** Headline tối đa 2 dòng trên desktop, văn bản mô tả (subtext) tối đa **20 từ** VÀ tối đa 3-4 dòng, các nút CTA phải nhìn thấy được mà không cần cuộn trang. Nếu nội dung quá dài: hãy giảm kích thước font chữ HOẶC cắt bớt chữ. Nếu bạn không thể mô tả giá trị cốt lõi của sản phẩm trong vòng 20 từ mô tả, lỗi nằm ở khâu định hình giá trị, không phải quy tắc quá chặt chẽ. Không bao giờ để phần hero tràn màn hình khiến người dùng phải cuộn trang mới tìm thấy nút CTA chính.
* **Kỷ luật tỷ lệ font chữ phần Hero.** Hãy lập kế hoạch kích thước chữ và kích thước hình ảnh *đồng thời*. Nếu asset hình ảnh của hero lớn và tiêu đề dài hơn 6 từ, không được bắt đầu với cỡ chữ khổng lồ `text-7xl/text-8xl`. Khung kích thước hợp lý mặc định: `text-4xl md:text-5xl lg:text-6xl` cho hầu hết các hero; chỉ dùng `text-6xl md:text-7xl` khi tiêu đề ngắn gọn chỉ 3-5 từ. Tiêu đề hero dài tới 4 dòng luôn là lỗi thiết kế kích cỡ chữ, không phải lỗi độ dài copy.
* **GIỚI HẠN PADDING TRÊN CỦA HERO (Hero Top Padding Cap - Bắt buộc):** Padding phía trên của phần Hero tối đa là `pt-24` (≈6rem) trên desktop. Lớn hơn mức đó sẽ khiến nội dung hero bị đẩy xuống nửa dưới màn hình và trông giống như một lỗi bố cục, không phải khoảng trắng có chủ đích. Nếu hero cần thêm không gian thở, hãy tăng cỡ chữ hoặc kích thước asset, đừng tăng padding trên.
* **KỶ LUẬT XẾP CHỒNG HERO (Hero Stack Discipline - Tối đa 4 thành phần chữ).** Hero là một khoảnh khắc tập trung duy nhất, không phải danh sách tính năng. Các thành phần chữ được phép xuất hiện, tối đa 4 thành phần:
  1. Eyebrow (nhãn in hoa nhỏ phía trên) HOẶC dải thương hiệu (brand strip) HOẶC không dùng cả hai - chọn không dùng hoặc dùng một cái.
  2. Headline (tiêu đề chính, tối đa 2 dòng, xem ở trên).
  3. Subtext (mô tả ngắn, tối đa 20 từ, tối đa 4 dòng).
  4. Các nút CTA (1 nút chính + tối đa 1 nút phụ).
  - **BỊ CẤM trong phần Hero:** dòng slogan nhỏ dưới nút CTA ("Hoạt động tốt với GitHub, GitLab..."), dải logo đối tác ("Được tin dùng bởi..."), mức giá mồi ("Miễn phí cho cá nhân, $10 cho đội ngũ"), danh sách gạch đầu dòng tính năng, hàng avatar người dùng (social proof). Tất cả các thành phần này bắt buộc phải được chuyển xuống các section riêng biệt đặt ngay phía dưới phần hero.
  - Nếu bạn đã dùng eyebrow VÀ một câu tagline dưới nút CTA trong cùng một hero, hãy bỏ tagline. Nếu dùng dải logo VÀ tagline, hãy bỏ tagline. Tối đa chỉ có một thành phần chữ phụ nhỏ trong hero.
* **Dải logo đối tác "Used by / Trusted by" phải đặt DƯỚI phần hero, không được để bên trong hero.** Hero chỉ dành cho tuyên ngôn giá trị và nút hành động chính. Dải logo đối tác là một section riêng đặt ngay phía dưới. Không nhét logo đối tác chung hàng flexbox với nội dung chữ của hero.
* **Thanh điều hướng (Navigation) BẮT BUỘC phải nằm trên một dòng duy nhất trên desktop.** Nếu các liên kết không đủ chỗ hiển thị ở breakpoint `lg` (1024px), hãy rút ngắn chữ nhãn, lược bớt các liên kết phụ hoặc chuyển sang menu hamburger. Thanh nav xuống dòng thành 2 dòng trên desktop là lỗi thiết kế.
* **Giới hạn chiều cao thanh điều hướng: Tối đa 80px trên desktop, mặc định là 64-72px.** Không dùng các thanh nav khổng lồ kiểu "agency" chiếm tới 15% diện tích màn hình.
* **Bento Grid phải có nhịp điệu, tránh lặp lại một màu tẻ nhạt.** Không xếp chồng liên tiếp 6 hàng có chung bố cục "ảnh bên trái / chữ bên phải". Hãy thay đổi cấu trúc: xen kẽ các hàng tính năng full-width, kích thước ô bất đối xứng, hoặc các vạch chia dọc.
* **QUY TẮC SỐ Ô LƯỚI BENTO (Bento Cell Count Rule - Bắt buộc):** Một lưới bento grid có CHÍNH XÁC số lượng ô bằng đúng số lượng nội dung bạn có. Có 3 nội dung → lưới 3 ô (chia 1+2, hoặc 2+1, hoặc bộ ba bất đối xứng). Có 5 nội dung → lưới 5 ô (chia 2+3, 3+2, 1 ô lớn+4 ô nhỏ, v.v.). Nếu lưới bento xuất hiện ô trống ở giữa hoặc ở cuối, bạn đã lập kế hoạch sai. Hãy tái cấu trúc lưới bento; không chèn một ô trống giả lập vào.
* **Cấm lặp lại bố cục các Section (Section-Layout-Repetition Ban).** Một khi bạn đã sử dụng một họ bố cục cho một section (ví dụ: 3 thẻ card hình ảnh, câu trích dẫn full-width, bố cục chia đôi chữ/ảnh), họ bố cục đó chỉ được xuất hiện tối đa MỘT LẦN trên trang. Phần "Các dự án nổi bật" không được có bố cục giống hệt phần "Chúng tôi làm gì". Một trang landing page có 8 section phải sử dụng tối thiểu 4 họ bố cục khác nhau.
* **GIỚI HẠN LẶP LẠI ZIGZAG (Zigzag Alternation Cap - Bắt buộc).** Việc xếp liên tiếp các section theo kiểu zigzag "ảnh trái + chữ phải" rồi "chữ trái + ảnh phải" là thiết kế rất nhàm chán. Tối đa chỉ được xếp 2 section liên tiếp theo mô hình chia đôi ảnh+chữ này. Section thứ 3 liên tiếp sử dụng mô hình này là lỗi Pre-Flight Fail. Hãy phá vỡ chu kỳ bằng một section full-width, section xếp dọc, bento grid, chữ chạy marquee, hoặc một họ bố cục khác.
* **TIẾT CHẾ SỬ DỤNG EYEBROW (Eyebrow Restraint - Bắt buộc, quy tắc bị vi phạm nhiều nhất trong các test AI).** "Eyebrow" là nhãn chữ in hoa nhỏ với khoảng cách chữ rộng nằm ngay phía trên tiêu đề section (ví dụ: `BỐN PHIÊN BẢN`, `CÁC DỰ ÁN ĐÃ CHỌN`, `PHẦN CỨNG`, `Quản lý tác vụ dạng Git-native`). Dấu hiệu CSS đặc trưng: `text-[11px] uppercase tracking-[0.18em]`, `font-mono text-[10.5px] uppercase tracking-[0.22em]`. Mọi trang web do AI xây dựng đều tự động chèn eyebrow lên TRÊN MỌI tiêu đề section, tạo ra một nhịp điệu rập khuôn, nhàm chán. Quy tắc cứng:
  - **Tối đa 1 eyebrow cho mỗi 3 section.** Hero tính là 1. Do đó, một trang có 9 section chỉ được phép sử dụng tối đa 3 eyebrow trên toàn bộ trang.
  - Nếu section A đã dùng eyebrow, 2 section tiếp theo không được phép dùng.
  - **Kiểm tra Pre-Flight mang tính cơ học:** đếm số lượng class `uppercase tracking` (hoặc các nhãn in hoa nhỏ tương tự nằm trên headline) của tất cả các component section. Nếu số lượng > ceil(tổng số section / 3), kết quả kiểm tra thất bại.
  - **Giải pháp thay thế:** lược bỏ eyebrow hoàn toàn. Tiêu đề chính là đủ. Vị trí của section trên trang đã tự phân loại chức năng của nó; không cần nhãn phụ.
* **CẤM CHIA ĐÔI TIÊU ĐỀ SECTION (Split-Header Ban - Bắt buộc):** Mô hình tiêu đề section dạng "tiêu đề lớn bên trái + đoạn văn giải thích nhỏ bên phải" (cột trái col-span-7/8, cột phải col-span-4/5 chứa đoạn văn giải thích) bị **CẤM dùng làm mặc định**. Mỗi section chỉ nên truyền tải MỘT thông điệp tập trung. Nếu bạn thực sự cần cả tiêu đề và văn bản giải thích, hãy xếp chồng chúng theo chiều dọc (tiêu đề ở trên, văn bản ở dưới, max-width 65ch). Chỉ sử dụng mô hình split-header khi cột bên phải chứa một thành phần trực quan hoặc tương tác thực sự, không phải chỉ chứa chữ điền vào chỗ trống.
* **Đa dạng hóa nền Bento (Bento Background Diversity - Bắt buộc):** Bento grid và các section lưới tính năng không được là 6 thẻ card trắng trên nền trắng chỉ chứa chữ. Tối thiểu phải có 2-3 ô trong lưới có sự đa dạng trực quan rõ rệt: chứa ảnh thật, gradient phù hợp thương hiệu (không dùng tím AI), pattern, hoặc màu nền được phủ nhẹ. Một lưới bento trắng-trên-trắng chỉ chứa chữ trông giống như một bản template AI mặc định nghèo nàn, cho dù các phần khác của trang có tốt đến đâu.
* **Khai báo thu gọn mobile rõ ràng cho từng section.** Với mọi bố cục nhiều cột, bạn bắt buộc phải khai báo rõ ràng cơ chế co giãn cho màn hình `< 768px` trong chính component đó. Không tự giả định "Tailwind sẽ tự xử lý ổn thỏa".
### 4.8 Chiến lược về Hình ảnh & Tài nguyên Trực quan (Image & Visual Asset Strategy)

Landing pages và portfolios là các **sản phẩm trực quan**. Các trang chỉ có chữ đi kèm với các thẻ div giả lập ảnh chụp màn hình là sản phẩm cẩu thả.

**Thứ tự ưu tiên đối với tài nguyên trực quan:**
1. **Dùng công cụ sinh ảnh AI trước tiên.** Nếu có BẤT KỲ công cụ sinh ảnh AI nào khả dụng trong môi trường (như `generate_image`, công cụ ảnh MCP, trình sinh ảnh tích hợp của IDE, công cụ ảnh của OpenAI, v.v.), bạn BẮT BUỘC phải sử dụng nó để tạo ra các asset dành riêng cho từng section: ảnh chụp hero, ảnh sản phẩm, ảnh texture nền, ảnh không khí trực quan. Hãy sinh ảnh với tỷ lệ khung hình (aspect ratio) phù hợp với section. Không bỏ qua bước này chỉ vì bạn thấy tự viết CSS sẽ nhanh hơn.
2. **Sử dụng ảnh web thực tế ở vị trí thứ hai.** Khi không có công cụ sinh ảnh AI, hãy dùng các nguồn ảnh chụp thực tế. Các nguồn mặc định được chấp nhận:
   * `https://picsum.photos/seed/{descriptive-seed}/{w}/{h}` cho ảnh placeholder chụp thực tế (seed nên mô tả rõ section đó, ví dụ: `marrow-cookware-kitchen`).
   * Các URL ảnh gốc hoặc URL thương hiệu khi yêu cầu bối cảnh cung cấp.
   * Các nguồn ảnh mở (Unsplash qua link trực tiếp, Pexels) nếu được cho phép rõ ràng.
3. **Giải pháp cuối cùng: báo cho người dùng.** Nếu cả hai phương án trên đều không khả thi, tuyệt đối KHÔNG tự vẽ các hình trang trí SVG cẩu thả hay dựng các div giả lập screenshot sản phẩm. Thay vào đó, hãy để lại các thẻ placeholder được ghi nhãn rõ ràng (`<!-- TODO: hero product photo, 1600x1200 -->`) và ghi ở cuối phản hồi: *"Trang này cần hình ảnh thực tế tại các vị trí: [danh sách vị trí]. Vui lòng cung cấp hoặc tự tạo."*

**Ngay cả các trang tối giản cũng cần ảnh thực tế.** Một trang web chỉ có chữ không phải là tối giản, nó là một trang web chưa hoàn thiện. Ngay cả một trang web phong cách Linear đậm chất biên tập cũng cần tối thiểu 2-3 ảnh thực tế (ảnh hero, một ảnh sản phẩm/lifestyle, một ảnh hỗ trợ). Hãy sinh ảnh đen trắng tối giản nếu bối cảnh yêu cầu sự tiết chế; không bỏ qua hình ảnh chỉ vì bạn đặt nút vặn dial ở mức thấp.

**Sử dụng logo công ty thực tế cho phần social proof.** Khi bối cảnh yêu cầu một dải logo đối tác/khách hàng ("Trusted by / Used by / Customers"), tuyệt đối KHÔNG dùng các chữ wordmark dạng text thông thường (`<span>Acme Co</span>` xếp hàng ngang). Hãy dùng logo dạng SVG thực tế:
* **Nguồn: Simple Icons** (`https://cdn.simpleicons.org/{slug}/ffffff` để đổi màu bất kỳ, hoặc package npm `simple-icons`). Hỗ trợ hầu hết các thương hiệu nổi tiếng.
* **Giải pháp thay thế: devicon** cho các logo công nghệ (`@svgr/cli` hoặc dùng CDN).
* **Nếu tên thương hiệu là tự chế? Hãy tự chế cả logo dạng SVG.** Thiết kế một ký hiệu monogram đơn giản (một chữ cái trong hình tròn, chữ lồng 2 ký tự, hoặc hình học trừu tượng) render dưới dạng `<svg>` nội dòng ăn khớp với style của trang. Việc dùng text thuần cho các tên thương hiệu tự chế trông rất rẻ tiền.
* **Luôn luôn** đảm bảo các logo hiển thị tốt trên cả chế độ sáng và tối (trắng trên nền tối, đen trên nền sáng, hoặc dùng biến màu sắc đồng bộ với theme).
* **QUY TẮC CHỈ LOGO (Logo-only - Bắt buộc):** dải logo chỉ chứa logo và không có gì khác. KHÔNG in tên ngành/phân khúc dưới từng logo (không ghi `Vercel` + `hosting` phía dưới, không ghi `Stripe` + `payments`, không ghi `Cloudflare` + `infra`). Bản thân logo đã tự thể hiện sự uy tín, các dòng nhãn phụ không mang lại giá trị gì cho người dùng. Tùy chọn: dùng tên thương hiệu làm alt-text cho trình đọc màn hình, hoặc liên kết đến trang của thương hiệu đó. Đó là tất cả.

**Các hình minh họa tự code (Hand-rolled illustrations):**
* Sử dụng icon dạng SVG từ thư viện: tốt (xem Phần 3.C).
* Tự code các hình minh họa SVG trang trí (hình vẽ tùy chỉnh, logo, ký hiệu): **cực kỳ không khuyến khích**, không bao giờ dùng làm mặc định. Chỉ chấp nhận khi:
  - Yêu cầu ghi rõ ("hãy vẽ cho tôi một logo SVG").
  - Đó là một hình ký hiệu hình học đơn giản (hình vuông, hình tròn, hoặc một chữ wordmark dạng display).
  - Bạn tự tin vào chất lượng đồ họa đầu ra.

**Cấm tuyệt đối các div giả lập screenshot sản phẩm.** Một bản "preview sản phẩm tự dựng" bằng các hình chữ nhật `<div>`, danh sách tác vụ giả, terminal giả, dashboard giả lập từ div là một dấu hiệu nhận diện AI. Nếu cần hiển thị sản phẩm:
* Sử dụng một URL ảnh chụp màn hình thật nếu có.
* Sinh một ảnh screenshot bằng công cụ sinh ảnh AI.
* Dùng một component preview thật (một phiên bản UI chức năng siêu nhỏ hoạt động ngay trong trang).
* Hoặc bỏ qua phần preview đó và thay bằng ảnh chụp nghệ thuật/biên tập.

**Hero bắt buộc phải có visual thật.** Một khối chữ + một hình cầu gradient mờ không phải là hero - đó chỉ là một placeholder chưa hoàn thiện.

### 4.9 Mật độ Nội dung (Content Density)

Trang landing page sống nhờ **ấn tượng đầu tiên**, không phải nhờ việc đọc hết toàn bộ chữ. Hãy cắt giảm nội dung một cách tàn nhẫn.

* **Cấu trúc nội dung mặc định của từng section:** tiêu đề ngắn (≤ 8 từ) + đoạn văn mô tả ngắn (≤ 25 từ) + một asset trực quan HOẶC một nút CTA. Bất kỳ nội dung nào nhiều hơn mức này phải được giải thích rõ lý do.
* **Không làm các section đổ dữ liệu (data-dump).** Một bảng danh sách 20 dòng bài viết, 30 dòng giải thưởng, một bảng giá khổng lồ trên trang marketing → sai bố cục. Giải pháp:
  - Chỉ hiển thị 3-5 mục nổi bật nhất + liên kết "Xem toàn bộ danh sách".
  - Sử dụng marquee / carousel để hiển thị bề rộng.
  - Chuyển sang một trang riêng hoàn toàn nếu dữ liệu đó chính là sản phẩm.
* **Danh sách dài cần một component UI khác, không phải một danh sách dài hơn.** Mặc định dùng thẻ `<ul>` với các dấu chấm đầu dòng hoặc hàng phân cách `divide-y` là lựa chọn lười biếng. Nếu bạn có nhiều hơn 5 mục, hãy dùng các giải pháp thay thế sau:
  - Chia đôi cột (split) với các mục được phân nhóm logic.
  - Lưới card grid với hình ảnh + nhãn chữ cho mỗi mục.
  - Sử dụng Tabs / Accordions nếu các mục có thể phân loại.
  - Dải cuộn snap ngang (horizontal scroll-snap pills).
  - Carousel cho các danh sách rộng (đánh giá khách hàng, logo, tính năng).
  - Chạy marquee cho các mục phụ không cần sự chú ý chi tiết của người dùng.
  Một bảng thông số kỹ thuật gồm 10 dòng với các đường kẻ mảnh bên dưới là giải pháp tồi tệ nhất. Hãy phân nhóm 10 dòng đó thành 2-3 cụm với tiêu đề và đường kẻ phân cách thưa, hoặc chuyển sang bố cục mỗi thông số là một thẻ card.
* **Đặc tả bảng thông số kỹ thuật (mô hình nồi Marrow-cookware).** Một bảng thông số kỹ thuật dài với đường viền `border-b` ở mỗi dòng là mặc định của AI cho các bối cảnh đồ gia dụng / phần cứng / trang phục / đồ thủ công. Bị CẤM. Các giải pháp thay thế cụ thể:
  - **Lưới card 2 cột:** mỗi thông số nằm trong một thẻ card riêng với tên thông số, giá trị (số lớn hiển thị), và một dòng mô tả "lý do nó quan trọng". Sắp xếp 2 cột trên desktop, 1 cột trên mobile.
  - **Dải cuộn snap ngang:** mỗi thông số là một viên thuốc, người dùng có thể lướt qua.
  - **Phân nhóm thành cụm:** gom 10 thông số thành 3 cụm logic (ví dụ: "Chất liệu", "Đặc tính nấu", "Bảo hành"), mỗi cụm có một đường kẻ phân cách mềm và tiêu đề cụm.
  - **Nổi bật vs Collapsed:** hiển thị 3-4 thông số chính dưới dạng các ô lưới lớn, các thông số còn lại ẩn dưới một mục rút gọn "Xem toàn bộ thông số kỹ thuật".

* **TỰ KIỂM TRA CHỮ (Copy Self-Audit - Bắt buộc trước khi bàn giao):** Trước khi tuyên bố hoàn thành tác vụ, hãy đọc lại từng chuỗi chữ hiển thị trên trang (tiêu đề, tiêu đề phụ, eyebrow, nhãn nút bấm, văn bản mô tả, chú thích, alt text, chữ chân trang, thông báo lỗi). Hãy đánh dấu bất kỳ chuỗi chữ nào:
  - **Sai ngữ pháp / lủng củng** ("free on its past", "two plans but one is honest", "to put it on the table" ngoài ngữ cảnh).
  - **Không rõ đối tượng hướng tới** ("we plan to stay that way" mà không có ngữ cảnh phía trước).
  - **Nghe giống như AI tự nghĩ ra** (chơi chữ gượng ép, ẩn dụ không khớp, các cụm từ "elegant nothing" sáo rỗng).
  - **Nghe giống như một LLM đang cố tỏ ra sâu sắc** (sự khiêm tốn performative, nhãn hiệu thủ công giả tạo, micro-meta thơ ca giả).
  Hãy viết lại mọi chuỗi chữ bị đánh dấu. Nếu không chắc chắn một câu có hợp lý hay không, hãy thay thế nó bằng một câu chức năng đơn giản, rõ nghĩa. Câu từ "sáng tạo" do AI tự chế còn tệ hơn một câu từ bình thường nhưng rõ nghĩa.
* **Cảnh báo các con số chính xác giả lập.** Các con số như `92%`, `4.1×`, `48k`, `5.8 mm`, `13.4 lb` chỉ được chấp nhận nếu:
  - Đến từ dữ liệu thật (tài liệu yêu cầu, brand guidelines, chỉ số công khai).
  - Được ghi nhãn rõ ràng là dữ liệu giả lập (`<!-- mock -->`, "ví dụ", "dữ liệu mẫu").
  - Ngược lại, việc tự chế ra các con số chính xác để tạo thẩm mỹ kỹ thuật bị CẤM. Đừng tự chế ra độ chính xác kỹ thuật mà thương hiệu không công bố.
* **Nhất quán văn phong trên trang.** Không trộn lẫn ngôn ngữ kỹ thuật monospace ("47 tasks · 0.6 ctx-switches/day") với văn phong biên tập và ngôn ngữ marketing đại chúng trong cùng một bố cục trừ khi giọng điệu thương hiệu yêu cầu rõ ràng.

### 4.10 Các câu trích dẫn & Đánh giá (Quotes & Testimonials)

* **Tối đa 3 dòng** cho nội dung trích dẫn. Không bao giờ viết dài tới 6 dòng. Nếu trích dẫn gốc quá dài → hãy cắt ngắn. Một câu trích dẫn trên trang marketing là một lát cắt, không phải toàn bộ bài review.
* Với các cỡ chữ siêu nhỏ (ví dụ: các đánh giá nhỏ ở chân trang), số dòng có thể co giãn nhẹ. Tinh thần chung: "đọc hiểu ngay trong một cái nhìn".
* **Không dùng ký hiệu gạch ngang em-dash (`—`) làm điểm nhấn trang trí** bên trong câu trích dẫn (các khoảng nghỉ dài, gạch đầu dòng). Xem Phần 9.G - em-dash bị cấm hoàn toàn.
* Định dạng nguồn trích dẫn: Tên + Chức danh + (Tùy chọn) Công ty. Không bao giờ chỉ ghi mỗi tên ("- Sarah").
* Dấu ngoặc kép: sử dụng dấu ngoặc kép typographic chuẩn ( “ ” ) hoặc không dùng dấu ngoặc nào. Không dùng dấu ngoặc thẳng ASCII ( " ).

### 4.11 Khóa Theme của Trang (Light / Dark Mode Consistency)

Toàn bộ trang web chỉ sử dụng MỘT theme duy nhất. Các section không đảo ngược theme của nhau.

* Nếu trang web là dark mode, TẤT CẢ các section đều phải là dark mode. Không kẹp một section màu kem/giấy ấm vào giữa hai section nền tối (hoặc ngược lại). Người dùng không được phép có cảm giác họ đang đi lạc sang một website khác khi cuộn trang.
* Ngoại lệ: Nếu bối cảnh yêu cầu rõ ràng một thiết bị "Color Block Story" hoặc "Theme Switch on Scroll" (đổi theme khi cuộn) VÀ đó là một ý đồ thiết kế có tính toán (một lần chuyển đổi theme lớn với hiệu ứng chuyển cảnh mạnh mẽ, không phải đổi màu liên tục ngẫu nhiên), điều đó được phép xuất hiện 1 lần trên trang.
* Hành vi mặc định: chọn chế độ sáng (light), tối (dark), hoặc tự động theo hệ thống (`prefers-color-scheme`) ở cấp độ trang và khóa nó lại. Việc thay đổi sắc độ nền nhẹ trong cùng một họ theme là tốt (`bg-zinc-950` đặt cạnh `bg-zinc-900`); việc đột ngột nhảy sang `bg-amber-50` ở giữa một trang `bg-zinc-950` là lỗi thiết kế.
* Khi sử dụng hệ thống thiết kế có sẵn tính năng theme (Radix Themes, shadcn/ui với `<Theme>`), hãy thiết lập theme một lần duy nhất ở file `layout.tsx` hoặc root của trang. Không cho phép các section riêng lẻ ghi đè.

---

## 5. HOẠT ẢNH NÂNG CAO (GSAP & Motion - Chủ động theo ngữ cảnh)

Đây là các công cụ hỗ trợ, không phải thiết lập mặc định. Chỉ sử dụng khi nhận định thiết kế yêu cầu. **Không tự động kích hoạt bất kỳ hiệu ứng nào.**

* **Liquid Glass / Glassmorphism:** Phù hợp cho hàng tiêu dùng cao cấp, các thương hiệu adjacent với Apple, xa xỉ, hoặc các phần phủ đè lên media. Không phù hợp cho dashboard, dịch vụ công, hoặc "B2B nhàm chán". Khi sử dụng, hãy vượt qua thuộc tính `backdrop-blur` thông thường: thêm viền trong 1px (`border-white/10`) và đổ bóng trong tinh tế (`shadow-[inset_0_1px_0_rgba(255,255,255,0.1)]`) để mô phỏng khúc xạ ánh sáng ở cạnh kính vật lý. Cung cấp fallback màu đặc cho `prefers-reduced-transparency`.
* **Vật lý Nam châm vi mô (Magnetic Micro-physics):** Sử dụng khi `MOTION_INTENSITY > 5` VÀ nhận định thiết kế là cao cấp / vui tươi / agency. Triển khai ĐỘC QUYỀN bằng các biến `useMotionValue` / `useTransform` của Motion nằm ngoài vòng đời render React. Không bao giờ dùng `useState`. Xem Phần 3.B.
* **Tương tác vi mô liên tục (Perpetual Micro-Interactions)** (Pulse, Typewriter, Float, Shimmer, Carousel): Sử dụng khi `MOTION_INTENSITY > 5` VÀ section đó thực sự có lợi từ chuyển động (các chỉ báo trạng thái, live feeds, trải nghiệm AI). **Không phải thẻ card nào cũng cần một hoạt ảnh chạy vô hạn.** Nếu section mang tính thông tin tĩnh, hãy giữ nó tĩnh hoàn toàn. Áp dụng Spring Physics (`type: "spring", stiffness: 100, damping: 20`) - không dùng easing tuyến tính.
* **"Chuyển động tuyên bố, chuyển động hiển thị."** Nếu `MOTION_INTENSITY > 4`, trang web bắt buộc phải chuyển động thực tế: hoạt ảnh xuất hiện ở hero, hiệu ứng scroll-reveal ở các section chính, hover physics ở các nút bấm CTA. Một trang web tĩnh hoàn toàn tuyên bố `MOTION_INTENSITY: 7` là lỗi thiết kế. Ngược lại, nếu bạn không thể triển khai chuyển động ổn định trong phạm vi công việc, hãy hạ dial xuống 3 và ship một trang tĩnh sạch sẽ. Không bao giờ dựng dở dang hoạt ảnh gây lỗi (ScrollTrigger bị cắt nửa chừng, chuyển cảnh giật cục, thiếu hàm cleanup).
* **HOẠT ẢNH BẮT BUỘC PHẢI CÓ LÝ DO (Motivated Motion - Bắt buộc):** Trước khi thêm bất kỳ animation nào, hãy tự hỏi: "hoạt ảnh này truyền tải điều gì?". Các lý do hợp lệ: phân cấp (thu hút sự chú ý vào đúng vị trí), kể chuyện (hiển thị nội dung theo trình tự logic khớp với mạch câu chuyện), phản hồi (xác nhận hành động của người dùng), chuyển đổi trạng thái (hiển thị sự thay đổi). Lý do không hợp lệ: "nhìn cho đẹp/cho ngầu". Dùng GSAP ở khắp mọi nơi chỉ vì GSAP khả dụng là nghiệp dư. Mỗi ScrollTrigger, mỗi marquee, mỗi section được ghim đều cần một lý do. Nếu bạn không thể giải thích lý do đó trong một câu, hãy lược bỏ hoạt ảnh.
* **MARQUEE TỐI ĐA MỘT LẦN TRÊN TRANG (Marquee Max-One-Per-Page - Bắt buộc):** Các dải chữ cuộn ngang chạy vô tận ("logo chạy liên tục", "tuyên ngôn cuộn ngang", "dải chữ động") chỉ được xuất hiện tối đa MỘT LẦN trên trang. Dùng từ 2 marquee trở lên trên cùng một trang trông giống như một sản phẩm lười biếng, chắp vá. Hãy chọn duy nhất một section nơi marquee thực sự phục vụ nội dung; các section khác phải dùng bố cục khác.
* **Mẫu GSAP Sticky-Stack (khi sử dụng hiệu ứng cuộn xếp chồng).** Một hiệu ứng "cuộn xếp chồng thẻ card" phải là một sticky-stack THỰC SỰ, không phải một danh sách hiển thị sequential thông thường. Xem cấu trúc code chuẩn Phần 5.A bên dưới. Lỗi thường gặp: hoạt ảnh kích hoạt khi cuộn được một nửa thay vì ghim ở đỉnh viewport. Sửa bằng cách: `start: "top top"` không dùng `start: "top center"` hoặc `"top 80%"`.
* **Mẫu GSAP Horizontal-Pan (khi sử dụng cuộn ngang).** Xem cấu trúc code chuẩn Phần 5.B bên dưới. Lỗi thường gặp: hoạt ảnh bắt đầu trước khi section được ghim, khiến người dùng nhìn thấy một nửa slide. Sửa bằng cách: `start: "top top"`, ghim wrapper, chạy scrub trên track nội dung bên trong.

### 5.A Sticky-Stack - Cấu trúc code chuẩn (Canonical Skeleton)

```tsx
"use client";
import { useRef, useEffect } from "react";
import { gsap } from "gsap";
import { ScrollTrigger } from "gsap/ScrollTrigger";
import { useReducedMotion } from "motion/react";

gsap.registerPlugin(ScrollTrigger);

export function StickyStack({ cards }: { cards: React.ReactNode[] }) {
  const ref = useRef<HTMLDivElement>(null);
  const reduce = useReducedMotion();

  useEffect(() => {
    if (reduce || !ref.current) return;
    const ctx = gsap.context(() => {
      const cardEls = gsap.utils.toArray<HTMLElement>(".stack-card");
      cardEls.forEach((card, i) => {
        if (i === cardEls.length - 1) return;
        ScrollTrigger.create({
          trigger: card,
          start: "top top",                              // ghim tại đỉnh viewport
          endTrigger: cardEls[cardEls.length - 1],
          end: "top top",
          pin: true,
          pinSpacing: false,
        });
        gsap.to(card, {
          scale: 0.92,
          opacity: 0.55,
          ease: "none",
          scrollTrigger: {
            trigger: cardEls[i + 1],
            start: "top bottom",
            end: "top top",
            scrub: true,
          },
        });
      });
    }, ref);
    return () => ctx.revert();
  }, [reduce]);

  return (
    <div ref={ref} className="relative">
      {cards.map((card, i) => (
        <div
          key={i}
          className="stack-card sticky top-0 min-h-[100dvh] flex items-center justify-center"
        >
          {card}
        </div>
      ))}
    </div>
  );
}
```

Các điểm cốt lõi: `start: "top top"`, `pin: true`, tất cả các thẻ card ngoại trừ thẻ cuối cùng đều được ghim, hiệu ứng biến đổi scale/opacity được điều khiển bởi trigger cuộn của thẻ KẾ TIẾP (thẻ trước sẽ nhỏ lại khi thẻ tiếp theo đè lên).

### 5.B Horizontal-Pan - Cấu trúc code chuẩn (Canonical Skeleton)

```tsx
"use client";
import { useRef, useEffect } from "react";
import { gsap } from "gsap";
import { ScrollTrigger } from "gsap/ScrollTrigger";
import { useReducedMotion } from "motion/react";

gsap.registerPlugin(ScrollTrigger);

export function HorizontalPan({ children }: { children: React.ReactNode }) {
  const wrap = useRef<HTMLDivElement>(null);
  const track = useRef<HTMLDivElement>(null);
  const reduce = useReducedMotion();

  useEffect(() => {
    if (reduce || !wrap.current || !track.current) return;
    const ctx = gsap.context(() => {
      const distance = track.current!.scrollWidth - window.innerWidth;
      gsap.to(track.current, {
        x: -distance,
        ease: "none",
        scrollTrigger: {
          trigger: wrap.current,
          start: "top top",                              // ghim bắt đầu khi đỉnh section chạm đỉnh viewport
          end: () => `+=${distance}`,                    // độ dài cuộn = chiều rộng track trừ đi viewport
          pin: true,
          scrub: 1,
          invalidateOnRefresh: true,
        },
      });
    }, wrap);
    return () => ctx.revert();
  }, [reduce]);

  return (
    <section ref={wrap} className="relative overflow-hidden">
      <div ref={track} className="flex h-[100dvh] items-center">
        {children}
      </div>
    </section>
  );
}
```

Các điểm cốt lõi: `start: "top top"`, `pin: true`, `end: "+=${distance}"` (chiều dài cuộn bằng đúng khoảng trượt ngang cần thiết), `scrub: 1`. Wrapper được ghim, track bên trong trượt ngang khi người dùng cuộn dọc.

### 5.C Scroll-Reveal Stagger - Cấu trúc code chuẩn (Giải pháp thay thế gọn nhẹ hơn)

Đối với các hiệu ứng đơn giản "các mục xuất hiện lần lượt khi cuộn vào khung nhìn" (không cần ghim màn hình), hãy ưu tiên dùng thuộc tính `whileInView` của Motion thay vì GSAP - nhẹ hơn và không cần ScrollTrigger:

```tsx
"use client";
import { motion, useReducedMotion } from "motion/react";

export function RevealStagger({ items }: { items: string[] }) {
  const reduce = useReducedMotion();
  return (
    <ul className="grid gap-6">
      {items.map((item, i) => (
        <motion.li
          key={item}
          initial={reduce ? false : { opacity: 0, y: 24 }}
          whileInView={{ opacity: 1, y: 0 }}
          viewport={{ once: true, amount: 0.3 }}
          transition={{
            duration: 0.6,
            delay: i * 0.06,
            ease: [0.16, 1, 0.3, 1],
          }}
        >
          {item}
        </motion.li>
      ))}
    </ul>
  );
}
```

Sử dụng giải pháp này cho: danh sách tính năng, lưới đánh giá khách hàng, logo đối tác, bất kỳ thứ gì chỉ cần "xuất hiện khi cuộn". Hãy dành GSAP cho các tác vụ cuộn ghim (pin) và trượt ngang thực thụ.

### 5.D Các mẫu hoạt ảnh bị CẤM

* **CẤM NGHIÊM NGẶT `window.addEventListener("scroll", ...)`**. Thao tác này chạy trên mỗi khung hình cuộn, dễ gây giật và không có cơ chế batching. Hãy sử dụng `useScroll()` của Motion, `ScrollTrigger` của GSAP, `IntersectionObserver`, hoặc CSS `scroll-driven animations` (`animation-timeline: view()`).
* **Cấm tự tính toán tiến trình cuộn bằng `window.scrollY`** lưu trong React state. Cùng lý do ở trên: gây re-render liên tục trên mỗi khung hình.
* **Cấm chạy vòng lặp `requestAnimationFrame` trực tiếp thay đổi state của React.** Thay thế bằng các motion values (`useMotionValue` + `useTransform`).
* **Chuyển đổi bố cục (Layout Transitions):** Dùng các thuộc tính `layout` và `layoutId` của Motion cho các thay đổi trạng thái trực quan (sắp xếp lại danh sách, mở rộng modal, chia sẻ phần tử chung giữa các route). Không bao bọc nội dung tĩnh trong thuộc tính `layout` "cho an toàn" - nó gây lãng phí tài nguyên tính toán.
* **Biên đạo xuất hiện so le (Staggered):** Sử dụng `staggerChildren` (Motion) hoặc CSS cascade (`animation-delay: calc(var(--index) * 100ms)`) cho các chu kỳ xuất hiện có thứ tự. Đối với `staggerChildren`, component Cha (`variants`) và các Con BẮT BUỘC phải nằm trong cùng một cây Client Component.

---

## 6. HÀNG RÀO HIỆU NĂNG & KHẢ NĂNG TIẾP CẬN (A11Y)

### 6.A Tăng tốc phần cứng (Hardware Acceleration)
* CHỈ tạo hoạt ảnh cho `transform` và `opacity`. Tuyệt đối không tạo hoạt ảnh cho `top`, `left`, `width`, `height`.
* Sử dụng `will-change: transform` một cách tiết kiệm - chỉ áp dụng trên các phần tử sẽ thực sự chuyển động.

### 6.B Chế độ giảm chuyển động (Reduced Motion - Bắt buộc)
* **Mọi hoạt ảnh cuộn/chuyển động vượt mức `MOTION_INTENSITY > 3` BẮT BUỘC phải tuân thủ thiết lập `prefers-reduced-motion`.** Đây là quy tắc không thương lượng.
* Trong Motion: bao bọc bằng hook `useReducedMotion()` và hạ cấp về trạng thái tĩnh (static).
* Trong CSS: giới hạn hoạt ảnh bên trong `@media (prefers-reduced-motion: no-preference)` hoặc cung cấp khối ghi đè dưới `@media (prefers-reduced-motion: reduce)` để tắt hoạt ảnh.
* Các vòng lặp vô hạn, hiệu ứng parallax, cuộn ngang, và vật lý hover nam châm BẮT BUỘC phải chuyển về trạng thái tĩnh / hiển thị ngay lập tức dưới chế độ giảm chuyển động.

### 6.C Chế độ tối (Dark Mode - Bắt buộc đối với mọi trang hướng đến người dùng)
* Thiết kế cho **cả hai chế độ ngay từ đầu**. Không bao giờ bàn giao bản chỉ-sáng hoặc chỉ-tối trừ khi có chỉ thị cụ thể của người dùng.
* Sử dụng biến thể `dark:` của Tailwind HOẶC sử dụng biến CSS (CSS variables) cho các token màu sắc. Chọn một chiến lược duy nhất cho mỗi dự án.
* **Không chỉ định màu sắc tối cụ thể ở đây.** Bối cảnh yêu cầu sẽ quyết định. Đảm bảo giữ nguyên phân cấp trực quan, nhận diện thương hiệu và tương phản WCAG AA (AAA cho nội dung chính) trên cả hai chế độ.
* Tôn trọng `prefers-color-scheme: dark`. Mặc định theo tùy chọn của hệ thống trừ khi thương hiệu yêu cầu bắt buộc một chế độ.

### 6.D Mục tiêu Core Web Vitals
* **LCP** < 2.5s. Hình ảnh hero phải dùng `next/image` với thuộc tính `priority` hoặc được preload.
* **INP** < 200ms. Chuyển các tác vụ nặng ra ngoài main thread.
* **CLS** < 0.1. Dành sẵn không gian hiển thị cho hình ảnh, font chữ, các khung nhúng.
* Chạy Lighthouse kiểm tra trước khi tuyên bố hoàn thành trang.

### 6.E Tài nguyên DOM (DOM Cost)
* Chỉ áp dụng bộ lọc nhiễu hạt/noise trên các phần tử giả cố định, không nhận sự kiện chuột (`fixed inset-0 z-[60] pointer-events-none`). TUYỆT ĐỐI không gắn vào container cuộn - GPU re-paint liên tục sẽ hủy hoại FPS trên di động.
* Chú ý dung lượng package. Motion không hề nhỏ. Three.js rất nặng. Hãy áp dụng lazy-load cho bất kỳ thành phần nào nằm ngoài màn hình đầu tiên (below-the-fold).

### 6.F Tiết chế Z-Index
KHÔNG BAO GIỜ lạm dụng các giá trị `z-50` hay `z-10` tùy tiện. Chỉ sử dụng z-index cho các tầng hệ thống rõ ràng (nav dính, modal, lớp phủ, hạt noise). Hãy ghi nhận lại thang z-index trong một file hằng số của dự án.

---

## 7. ĐỊNH NGHĨA CHI TIẾT NÚT VẶN (Tài liệu tham chiếu kỹ thuật)

### DESIGN_VARIANCE (Độ biến thiên bố cục - Cấp 1-10)
* **1-3 (Dễ đoán):** Lưới CSS Grid đối xứng (12 cột, các đơn vị `fr` bằng nhau), padding bằng nhau, căn giữa.
* **4-7 (Lệch dòng):** Sử dụng `margin-top: -2rem` tạo xếp chồng đè, thay đổi tỷ lệ khung hình ảnh (ví dụ: 4:3 đặt cạnh 16:9), tiêu đề căn trái trên nội dung căn giữa.
* **8-10 (Bất đối xứng):** Bố cục dạng xếp gạch (masonry), CSS Grid sử dụng các đơn vị phân số (`grid-template-columns: 2fr 1fr 1fr`), các vùng khoảng trống khổng lồ (`padding-left: 20vw`).
* **GHI ĐÈ MOBILE:** Đối với các cấp từ 4-10, các bố cục bất đối xứng trên `md:` BẮT BUỘC phải thu gọn về dạng một cột duy nhất (`w-full`, `px-4`, `py-8`) trên các khung nhìn di động `< 768px`.

### MOTION_INTENSITY (Mức độ hoạt ảnh - Cấp 1-10)
* **1-3 (Tĩnh):** Không có hoạt ảnh tự động. Chỉ dùng các trạng thái CSS `:hover` và `:active`. Chế độ giảm chuyển động (`prefers-reduced-motion`) hoạt động như mặc định.
* **4-7 (CSS mượt mà):** Sử dụng `transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1)`. Sử dụng hiệu ứng thác nước `animation-delay` khi load trang. Chỉ tập trung vào `transform` và `opacity`.
* **8-10 (Biên đạo nâng cao):** Hiệu ứng hé lộ theo tiến trình cuộn trang phức tạp, parallax, hoặc scroll-driven animation (CSS `animation-timeline` hoặc GSAP ScrollTrigger). Sử dụng các hook của Motion. **CẤM TUYỆT ĐỐI dùng `window.addEventListener('scroll')`** - đây là lệnh cấm cứng, không phải khuyên dùng. Xem Phần 5.D các giải pháp thay thế được phép.

### VISUAL_DENSITY (Mật độ hiển thị - Cấp 1-10)
* **1-3 (Thoáng đãng như triển lãm):** Nhiều khoảng trắng. Section gap khổng lồ (`py-32` đến `py-48`). Đắt tiền, sạch sẽ.
* **4-7 (Ứng dụng hàng ngày):** Spacing tiêu chuẩn của các ứng dụng web thông thường (`py-16` đến `py-24`).
* **8-10 (Buồng lái phi công):** Padding siêu nhỏ. Không dùng thẻ card; chỉ dùng các đường viền 1px để ngăn tách dữ liệu. Bắt buộc: sử dụng font `font-mono` cho tất cả các con số hiển thị.
## 8. GIAO THỨC CHẾ ĐỘ TỐI (DARK MODE PROTOCOL)

Hỗ trợ giao diện kép (dual-mode) theo mặc định. Không bao giờ tự giả định trang web chỉ dùng chế độ sáng, trừ khi yêu cầu (brief) là thiết kế dạng biên tập mô phỏng in ấn.

### 8.A Chiến lược Token (chọn một chiến lược và tuân thủ nhất quán)
* **Biến thể `dark:` của Tailwind** (mặc định cho các dự án utility-first): mọi tiện ích màu sắc phải được ghép cặp với biến thể tối của nó (ví dụ: `bg-white dark:bg-zinc-950`, `text-gray-900 dark:text-gray-100`).
* **Biến CSS (CSS variables)** (dành cho shadcn/ui, Radix Themes, hoặc các thư viện component có sẵn tính năng theme): định nghĩa các token ngữ nghĩa (`--surface`, `--surface-elevated`, `--text-primary`, `--accent`) và hoán đổi giá trị của chúng dưới lớp selector `[data-theme="dark"]` hoặc `@media (prefers-color-scheme: dark)`.

### 8.B Không chỉ định trước các màu sắc cụ thể ở đây
Yêu cầu bối cảnh và thương hiệu sẽ quyết định màu sắc. Kỹ năng này chỉ bắt buộc:
* **Tương phản** - Tối thiểu đạt chuẩn WCAG AA cho văn bản thông thường, hướng tới chuẩn AAA cho tiêu đề chính (hero copy).
* **Đồng bộ phân cấp (Hierarchy parity)** - Phân cấp trực quan hoạt động tốt ở chế độ sáng thì cũng phải hoạt động tốt ở chế độ tối. Nếu một nút CTA nổi bật ở chế độ sáng, nó cũng phải nổi bật ở chế độ tối.
* **Nhất quán thương hiệu (Brand fidelity)** - Màu thương hiệu chính phải luôn nhận diện được. Không được làm bạc màu hoặc xỉn màu thương hiệu khi đưa vào chế độ tối.
* **Không dùng màu đen tuyệt đối `#000000` và trắng tuyệt đối `#ffffff`** - hãy dùng màu off-black (như zinc-950, xám ấm gần đen) và off-white. Màu tuyệt đối sẽ triệt tiêu chiều sâu trực quan.

### 8.C Chế độ Mặc định
Tôn trọng cấu hình hệ thống `prefers-color-scheme` trừ khi thương hiệu yêu cầu bắt buộc một chế độ. Thêm một nút gạt chuyển đổi thủ công nếu việc thiếu một trong hai chế độ làm mất đi biểu trưng quan trọng của thương hiệu.

### 8.D Kiểm thử trên cả hai chế độ trước khi hoàn thành
Hãy mở trang web ở cả chế độ sáng và tối trong suốt quá trình phát triển. Không bàn giao bất kỳ trang nào nếu bạn chỉ mới nhìn thấy nó ở một chế độ duy nhất.

---

## 9. DẤU VẾT AI (CÁC MẪU BỊ CẤM - AI TELLS)

Hãy chủ động tránh các dấu hiệu thiết kế đặc trưng dưới đây trừ khi yêu cầu (brief) nêu đích danh chúng.

### 9.A Trực quan & CSS
* **KHÔNG dùng hiệu ứng phát sáng neon / viền ngoài (outer glows)** theo mặc định. Hãy dùng viền trong tinh tế (inner borders) hoặc bóng đổ nhẹ có tông màu (tinted shadows).
* **KHÔNG dùng màu đen tuyệt đối (`#000000`).** Hãy dùng off-black, zinc-950, hoặc màu than (charcoal).
* **KHÔNG dùng các màu nhấn quá bão hòa (oversaturated).** Hãy giảm bớt độ bão hòa để màu nhấn hòa quyện mượt mà với các tông màu trung tính.
* **KHÔNG dùng chữ gradient quá mức** cho các tiêu đề lớn.
* **KHÔNG tự chế con trỏ chuột tùy chỉnh (custom mouse cursors).** Lỗi thời, cản trở khả năng tiếp cận (a11y) và gây suy giảm hiệu năng.

### 9.B Typography (Hệ thống chữ)
* **TRÁNH dùng Inter làm mặc định.** Xem Phần 4.1 để biết các giải pháp thay thế.
* **KHÔNG dùng các tiêu đề H1 quá khổ** chỉ để cố gây ấn tượng. Hãy kiểm soát phân cấp trực quan bằng độ đậm nhạt (weight) + màu sắc thay vì tăng cỡ chữ thô bạo.
* **Giới hạn font Serif:** Chỉ dùng Serif cho thiết kế biên tập / xa xỉ / xuất bản. Không dùng cho dashboard.

### 9.C Bố cục & Spacing (Khoảng cách)
* **Tránh khoảng cách hoàn hảo một cách máy móc.** Không để các phần tử trôi lơ lửng với các khoảng trống vụng về.
* **KHÔNG xếp 3 thẻ tính năng bằng nhau nằm ngang.** Bố cục hàng ngang gồm 3 thẻ card giống hệt nhau giới thiệu tính năng bị CẤM. Hãy dùng zigzag 2 cột, lưới bất đối xứng, ghim cuộn (scroll-pinned) hoặc dải cuộn ngang thay thế.

### 9.D Nội dung & Dữ liệu (Hiệu ứng "Jane Doe")
* **KHÔNG dùng tên giả lập chung chung.** "John Doe", "Sarah Chan", "Jack Su" &rarr; hãy dùng tên sáng tạo, thực tế và phù hợp với khu vực địa lý của ngữ cảnh.
* **KHÔNG dùng avatar giả lập mặc định.** Tránh dùng các vòng tròn vẽ ký hiệu "quả trứng" SVG hoặc icon người dùng của Lucide &rarr; hãy dùng ảnh placeholder thực tế hoặc xử lý style đặc thù.
* **KHÔNG dùng các con số đẹp một cách giả tạo.** Tránh dùng `99.99%`, `50%`, `1234567`. Hãy dùng dữ liệu tự nhiên, có chút lộn xộn (`47.2%`, `+1 (312) 847-1928`).
* **KHÔNG dùng tên thương hiệu giả lập kiểu startup sáo rỗng.** "Acme", "Nexus", "SmartFlow", "Cloudly" &rarr; hãy tự nghĩ ra các tên gọi thực tế, cao cấp và ăn khớp với bối cảnh dự án.
* **KHÔNG dùng các động từ sáo rỗng điền vào chỗ trống.** "Elevate" (Nâng tầm), "Seamless" (Liền mạch), "Unleash" (Giải phóng), "Next-Gen" (Thế hệ mới), "Revolutionize" (Cách mạng hóa) &rarr; chỉ dùng các động từ cụ thể, có nghĩa.

### 9.E Tài nguyên & Component bên ngoài
* **KHÔNG tự vẽ icon SVG bằng tay.** Hãy sử dụng các thư viện được phép: Phosphor / HugeIcons / Radix / Tabler. Chỉ dùng Lucide khi có yêu cầu rõ ràng.
* **Cực kỳ hạn chế vẽ hình minh họa SVG trang trí thủ công** (xem Phần 4.8).
* **KHÔNG dùng div để giả lập ảnh chụp màn hình.** Tuyệt đối không dùng các hình chữ nhật `<div>` xếp lớp để mô phỏng dashboard sản phẩm. Hãy dùng ảnh thật, ảnh sinh từ AI, hoặc bỏ qua phần preview đó.
* **KHÔNG để link Unsplash bị hỏng.** Hãy dùng `https://picsum.photos/seed/{descriptive-string}/{w}/{h}`, ảnh sinh từ AI, hoặc các tài nguyên ảnh thực tế có sẵn.
* **Tùy biến shadcn/ui:** Được phép sử dụng, nhưng KHÔNG bao giờ giữ nguyên trạng thái mặc định của thư viện. Hãy tinh chỉnh bo góc (radii), màu sắc, bóng đổ và typography để khớp với ngôn ngữ thiết kế riêng của dự án.
* **Độ hoàn thiện chuẩn Production:** Code trực quan sạch sẽ, dễ nhớ, được chau chuốt tỉ mỉ.

### 9.F Dấu hiệu lỗi khi kiểm thử (Bị cấm hoàn toàn)
Các mẫu dưới đây được đúc rút từ các lỗi thực tế của LLM khi cố gắng tạo giao diện "có vẻ được thiết kế". Hãy coi đây là các lệnh cấm cứng trừ khi yêu cầu nêu đích danh:

**Phần Hero & Đầu trang**
* **KHÔNG dùng nhãn phiên bản trong hero.** Các nhãn kiểu `V0.6`, `v2.0`, `BETA`, `INVITE-ONLY PREVIEW`, `EARLY ACCESS`, `ALPHA` bị cấm dùng làm eyebrow mặc định. Chỉ chấp nhận khi bối cảnh thực sự là một buổi ra mắt sản phẩm hoặc trạng thái thử nghiệm thực tế.
* **KHÔNG dùng các dòng chữ kiểu "Brand · No. 01".** Ví dụ như `Marrow · No. 01 · The 6-quart` kiểu dòng chữ siêu nhỏ trang trí. Hãy bỏ qua chúng.

**Đánh số section & Nhãn phụ**
* **KHÔNG đánh số thứ tự cho tiêu đề section.** Các kiểu `00 / INDEX`, `001 · Capabilities`, `002 · Featured commission`, `06 · how it works`, `05 · The honest table` bị cấm. Eyebrow phải mô tả chủ đề bằng ngôn ngữ tự nhiên, không đánh số.
* **KHÔNG dùng nhãn kiểu `01 / 4` để đánh số trang trí trên ảnh hoặc ô bento.** Nếu người dùng có thể tự đếm, họ không cần nhãn đó.
* **KHÔNG dùng nhãn điều hướng kiểu `Scroll · 001 Capabilities`.** Chỉ cần một mũi tên đơn giản hoặc từ "Cuộn" là đủ; không thêm tiền tố số section.
* **KHÔNG dùng dải nhãn khoảng thời gian kiểu "Index of Work, 2018 - 2026" làm eyebrow.** Chỉ cần nói rõ section đó chứa nội dung gì.

**Dấu phân cách & Dấu chấm tròn**
* **Tiết chế dấu chấm giữa (`·`).** Tối đa 1 dấu chấm trên mỗi dòng text metadata. KHÔNG dùng nó làm dấu phân cách mặc định cho mọi thứ (kiểu `foo · bar · baz · qux · quux`). Nếu cần phân tách các phần tử, hãy ưu tiên dùng xuống dòng, đường hairline mỏng hoặc chia cột.
* **KHÔNG chèn các chấm tròn trạng thái có màu (status dots) ở mọi nơi.** Đặt chấm màu trước nhãn điều hướng, trước mỗi dòng danh sách, hoặc trước badge để trang trí &rarr; bị cấm. Chỉ dùng khi thể hiện trạng thái logic thực tế (như server đang chạy, trạng thái rảnh/bận) và dùng cực kỳ hạn chế.

**Dấu gạch ngang Em-dash & Typography trang trí**
* **CẤM HOÀN TOÀN DẤU GẠCH NGANG EM-DASH (`—`) TRONG MỌI TRƯỜNG HỢP.** Xem Phần 9.G dưới đây để biết quy tắc cấm cứng không thương lượng. Ký tự em-dash bị cấm xuất hiện trong tiêu đề, eyebrow, badge, body text, câu trích dẫn, tên nguồn trích dẫn, alt text và nút bấm. Hãy thay bằng dấu gạch ngang thường (`-`).
* **KHÔNG dùng thẻ `<br>` kèm chữ in nghiêng ngẫu nhiên** trong tiêu đề như một "chiêu trò thiết kế" mặc định (ví dụ: `cho ba mươi<br>*năm.*`). Headline phải đọc tự nhiên trước, chỉ sáng tạo khi yêu cầu thực sự đòi hỏi.
* **KHÔNG dùng chữ xoay dọc** (ví dụ: "INDEX OF WORK, 2018 - 2026" xoay 90 độ). Đây là khuôn mẫu sáo rỗng của các portfolio cũ. Chỉ dùng khi dự án thực sự là agency thử nghiệm sáng tạo / Awwwards VÀ nó đóng vai trò bố cục thực tế.
* **KHÔNG vẽ các lưới tọa độ / đường kẻ crosshair để trang trí.** Các đường kẻ dọc ngang vẽ tùy tiện để làm trang web "có vẻ được thiết kế" &rarr; bị cấm. Chỉ vẽ khi chúng thực sự phân chia nội dung rõ ràng.

**Preview sản phẩm giả lập**
* **KHÔNG dựng dashboard/terminal giả bằng div trong phần hero.** Đây là dấu vết AI số 1. Hãy dùng ảnh screenshot thật, ảnh sinh từ AI, một component demo thực tế hoạt động được, hoặc không dùng gì cả.
* **KHÔNG ghi các nhãn phiên bản giả** (như `v0.6.2-rc.1`, `last sync 4s ago · main`) bên trong các ảnh preview tự dựng. Không mang lại giá trị gì và lộ rõ vết AI.

**Văn phong copy kiểu AI**
* **KHÔNG dùng các tiêu đề kiểu "Quietly in use at" / "Quietly trusted by"** (Âm thầm được tin dùng bởi). Hãy dùng ngôn ngữ tự nhiên: "Được tin dùng bởi", "Khách hàng của chúng tôi", hoặc bỏ qua tiêu đề nếu phần logo đã đủ tự thể hiện.
* **KHÔNG dùng các nhãn mang tính "thơ ca/thủ công giả tạo"** cho các section quote, blog hoặc sidebar (kiểu "From the field", "Field notes", "Currently on the bench", "On our desks", "Loose plates"). Hãy dùng nhãn chức năng rõ ràng ("Đánh giá", "Bài viết mới nhất", "Dự án đang làm") hoặc bỏ qua nhãn.
* **KHÔNG tự tiện viết các câu meta mang tính Meta-humble** kiểu AI (ví dụ: *"Chúng tôi tôn trọng những người làm việc bằng tiếng Pháp"*). Nghe rất gượng ép và đậm chất AI.
* **KHÔNG đưa dải thông tin thời tiết / địa điểm** (như `LIS 14:23 · 18°C`) vào header/footer trừ khi yêu cầu là một studio phân tán toàn cầu hoặc thương hiệu gắn liền với địa danh đó.
* **KHÔNG nhét các câu meta nhỏ dưới eyebrow.** Những câu kiểu: *"Mỗi tính năng dưới đây đều hoạt động thực tế, không phải lời hứa lộ trình. Danh sách được giữ ngắn gọn có chủ đích."* đặt dưới tiêu đề section là rác thông tin. Chỉ cần Eyebrow + Headline + Body là đủ.
* **KHÔNG dùng nhãn tiến trình chung chung.** "Stage 1 / Stage 2 / Stage 3", "Step 1 / Step 2", "Phase 01 / Phase 02". Bị cấm. Bản thân nội dung bước chính là nhãn. Nếu cần thể hiện sự tiến triển, hãy dùng trực tiếp động từ-danh từ hành động ("Cài đặt", "Cấu hình", "Vận hành") thay vì ghi "Bước 1: Cài đặt".

**Badge, nhãn và nhãn phiên bản**
* **KHÔNG đè badge/nhãn/tag lên trên hình ảnh.** Tránh dùng các tag kiểu `Brand · 02`, `PLATE · BRAND`, `Field notes - journal` đè lên ảnh. Hãy để ảnh tự kể chuyện, hoặc viết chú thích trực tiếp phía dưới ảnh (nằm ngoài ảnh).
* **KHÔNG dùng chú thích ghi công ảnh để trang trí.** Các chuỗi kiểu `Field study no. 12 · Ines Caetano`, `Plate 03 · House archive` dưới các ảnh picsum/stock trông rất giả tạo. Chỉ ghi công khi có nhiếp ảnh gia thật sự. Ngược lại, hãy dùng chú thích mô tả chức năng ngắn gọn ("Nồi 6-quart, màu Sage.").
* **KHÔNG ghi phiên bản build ở chân trang marketing.** Các chuỗi kiểu `v1.4.2`, `Build 0048` chỉ dành cho CLI hoặc tài liệu kỹ thuật, không dành cho landing page. Bị cấm ở footer của trang marketing/portfolio.
* **KHÔNG dùng bộ đếm số lượt giữ chỗ giả** (như "Còn 412 trên 800 chỗ") trừ khi đó thực sự là danh sách chờ giới hạn có dữ liệu thời gian thực.

**Dải chữ trang trí**
* **KHÔNG đặt dải chữ chạy trang trí ở chân phần hero.** Bố cục kiểu `BRAND. MOTION. SPATIAL.`, `DESIGN · BUILD · SHIP` chạy ngang chân hero là lối thiết kế sáo rỗng. Bị cấm mặc định. Chỉ cho phép khi dải đó chứa các link điều hướng thực sự (sticky bottom nav) hoặc thông tin trạng thái thực tế (thanh thông báo cookie).
* **KHÔNG để đoạn văn giải thích trôi nổi góc trên bên phải tiêu đề section.** Section có tiêu đề lớn căn trái, còn ở góc trên bên phải section lại xuất hiện một đoạn văn nhỏ giải thích lơ lửng không căn chỉnh với cái gì &rarr; đây là lỗi AI. Hãy đặt đoạn văn đó ngay dưới tiêu đề, hoặc thiết kế bố cục 2 cột cân đối (trái: tiêu đề, phải: văn bản mô tả).

**Danh sách, đường kẻ và chấm điểm**
* **KHÔNG dùng đồng thời cả `border-t` và `border-b` trên mỗi dòng của danh sách dài.** Hãy chọn một phương án (đường kẻ ngăn giữa các dòng HOẶC đường kẻ bao đầu nhóm) và dùng tiết chế. Một bảng thông số kỹ thuật 10 dòng với 10 đường hairline ngang là cách dựng bố cục lười biếng nhất &rarr; xem Phần 4.9 để biết các giải pháp thay thế.
* **KHÔNG vẽ thanh tiến trình / thanh điểm số (progress bars) để so sánh tính năng.** Nếu cần so sánh, hãy dùng số + icon nhỏ, hoặc thanh inline siêu mảnh KHÔNG có đường track nền. Các thanh màu xám to đùng với vạch tô đậm một phần là rác giao diện trên trang landing page.

**Địa điểm, thời gian, gợi ý cuộn**
* **Dải địa điểm / thời gian / thời tiết bị cấm trong 99% trường hợp.** Chỉ dùng khi bối cảnh thực sự yêu cầu (như studio toàn cầu, thương hiệu du lịch, địa điểm vật lý). Một địa chỉ liên hệ nhỏ ở footer là đủ; dải thông tin khí hậu trang trí thì không.
* **Cấm dùng gợi ý cuộn trang.** Những nhãn kiểu `Scroll`, `↓ scroll`, `Cuộn để khám phá`, icon chuột cuộn chuyển động &rarr; bị cấm. Người dùng nhìn thấy hero sẽ tự biết cuộn trang. Không cần dán nhãn cho đáy màn hình.
* **CẤM TUYỆT ĐỐI các chấm tròn trang trí mặc định.** Chỉ dùng tối đa một chấm tròn cho mỗi section nếu nó biểu thị trạng thái logic thực tế.

### 9.G CẤM DẤU GẠCH EM-DASH (`—`) - (Dấu vết AI bị vi phạm nhiều nhất)

**Ký tự em-dash (`—`) bị CẤM HOÀN TOÀN.** Đây là thói quen viết văn sáo rỗng nhất của các mô hình ngôn ngữ lớn và là dấu hiệu nhận diện AI số 1 trên môi trường production. Không có ngoại lệ "dùng ít", không có ngoại lệ "chỉ dùng trong văn bản thường", không có ngoại lệ "dùng trong quote". Hoàn toàn bằng không.

* **Cấm trong tiêu đề.** Thay bằng dấu chấm hoặc dấu phẩy.
* **Cấm trong eyebrow / nhãn / badge / chữ trên nút bấm / alt text / chú thích ảnh / link nav.** Thay bằng xuống dòng, chia cột hoặc đường hairline.
* **Cấm trong body text.** Hãy viết lại câu: tách thành hai câu bằng dấu chấm, dùng dấu phẩy, dùng dấu ngoặc đơn, hoặc dấu hai chấm.
* **Cấm trong tên nguồn trích dẫn.** Thay bằng dấu gạch ngang thường có khoảng cách (` - `) hoặc xuống dòng + giảm chữ.
* **Cấm dùng ký tự en-dash (`–`) làm dấu phân cách.** Khoảng năm (`2018-2026`) dùng dấu gạch nối thường (`-`). Khoảng số lượng (`€40-80k`) dùng dấu gạch nối thường (`-`).

Các ký tự gạch ngang duy nhất được phép xuất hiện trên trang là:
* Dấu gạch nối thường `-` (dành cho từ ghép, khoảng số, hoặc vạch chia trong markdown).
* Dấu trừ trong toán học (`-5°C`).

Nếu sản phẩm của bạn chứa dù chỉ một ký tự `—` hoặc `–` hiển thị cho người dùng, kết quả kiểm tra Pre-Flight Check sẽ tự động thất bại và bắt buộc phải viết lại. Quy tắc này là tuyệt đối.

---

## 10. TỪ VỰNG THAM CHIẾU (REFERENCE VOCABULARY)

Đây là bộ từ vựng thiết kế mà Agent cần hiểu rõ để giao tiếp, lên ý tưởng và áp dụng khi nhận định thiết kế yêu cầu. **Mã nguồn thực thi và component mẫu sẽ nằm trong Thư viện Khối (Phần 12) được cập nhật dần.**

### Các kiểu Hero (Hero Paradigms)
* **Asymmetric Split Hero** - Tiêu đề một bên, hình ảnh/asset bên còn lại, khoảng trắng rộng rãi.
* **Editorial Manifesto Hero** - Chữ lớn hiển thị, không dùng ảnh, trông như một tấm poster chữ.
* **Video / Media Mask Hero** - Chữ lớn được đục lỗ làm mặt nạ (mask) hiển thị video nền phía sau.
* **Kinetic-Type Hero** - Sử dụng chữ động nghệ thuật làm nhân tố trực quan chính.
* **Curtain-Reveal Hero** - Các phần của hero tách ra như rèm cửa khi người dùng cuộn trang.
* **Scroll-Pinned Hero** - Phần hero được ghim cố định, nội dung phía dưới cuộn đè lên hoặc cuộn phía sau.

### Thanh điều hướng & Menu (Navigation & Menus)
* **Mac OS Dock Magnification** - Thanh điều hướng sát cạnh trang, các icon phóng to mượt mà khi hover chuột qua.
* **Magnetic Button** - Nút tương tác tự hút lệch về phía con trỏ chuột khi đến gần.
* **Gooey Menu** - Các nút menu phụ tách ra khỏi nút chính như chất lỏng co giãn.
* **Dynamic Island** - Khung bong bóng viên thuốc tự co giãn/biến hình để hiển thị trạng thái/thông báo.
* **Contextual Radial Menu** - Menu dạng vòng tròn mở rộng ngay tại vị trí click chuột.
* **Floating Speed Dial** - Nút hành động nổi bật bung ra các nút phụ theo đường cong vật lý.
* **Mega Menu Reveal** - Menu thả xuống toàn màn hình, các mục xuất hiện dạng stagger-fade.

### Bố cục & Lưới (Layout & Grids)
* **Bento Grid** - Nhóm các ô lưới bất đối xứng (giống giao diện Apple Control Center).
* **Masonry Layout** - Lưới xếp gạch so le, chiều cao các hàng tự do.
* **Chroma Grid** - Các đường viền hoặc ô lưới chứa các dải màu gradient chuyển động mượt mà.
* **Split-Screen Scroll** - Giao diện chia đôi màn hình, hai nửa trượt ngược chiều nhau khi cuộn.
* **Sticky-Stack Sections** - Các section ghim lại và xếp chồng đè lên nhau khi cuộn.

### Thẻ & Container (Cards & Containers)
* **Parallax Tilt Card** - Thẻ card 3D tự nghiêng theo tọa độ di chuyển của chuột.
* **Spotlight Border Card** - Đường viền thẻ card tự phát sáng bám theo vị trí con trỏ chuột.
* **Glassmorphism Panel** - Tấm nền kính mờ với hiệu ứng khúc xạ ánh sáng ở viền trong.
* **Holographic Foil Card** - Hiệu ứng đổi màu cầu vồng lấp lánh (iridescent) khi hover qua thẻ.
* **Tinder Swipe Stack** - Xếp chồng các thẻ vật lý, cho phép vuốt bỏ từng thẻ sang hai bên.
* **Morphing Modal** - Nút bấm tự mở rộng kích thước biến đổi thành một hộp thoại (dialog).

### Hoạt ảnh cuộn trang (Scroll Animations)
* **Sticky Scroll Stack** - Các thẻ card tự ghim và xếp chồng lên nhau khi cuộn.
* **Horizontal Scroll Hijack** - Cuộn chuột dọc &rarr; trang trượt theo chiều ngang.
* **Locomotive / Sequence Scroll** - Hoạt ảnh video hoặc không gian 3D chuyển động đồng bộ theo thanh cuộn.
* **Zoom Parallax** - Ảnh nền ở trung tâm tự phóng to ra khi người dùng cuộn trang.
* **Scroll Progress Path** - Đường vẽ SVG tự chạy bám theo tiến trình cuộn trang.
* **Liquid Swipe Transition** - Chuyển trang với hiệu ứng chuyển cảnh giống chất lỏng co giãn.

### Triển lãm & Media (Galleries & Media)
* **Dome Gallery** - Thư viện ảnh dạng vòm 3D toàn cảnh.
* **Coverflow Carousel** - Băng chuyền ảnh 3D với các góc nghiêng ở hai bên cạnh.
* **Drag-to-Pan Grid** - Lưới nội dung vô cực, cho phép người dùng kéo thả để di chuyển góc nhìn.
* **Accordion Image Slider** - Các dải ảnh hẹp tự mở rộng chiều ngang khi hover chuột.
* **Hover Image Trail** - Con trỏ di chuyển để lại một chuỗi hình ảnh liên tục tự biến mất.
* **Glitch Effect Image** - Ảnh bị dịch chuyển kênh màu RGB khi hover qua.

### Typography & Chữ (Typography & Text)
* **Kinetic Marquee** - Dải chữ chạy ngang vô tận, tự động đảo chiều khi cuộn trang.
* **Text Mask Reveal** - Chữ hiển thị kích thước lớn làm khung cửa sổ trong suốt nhìn thấu video phía sau.
* **Text Scramble Effect** - Hiệu ứng giải mã chữ chạy ngẫu nhiên kiểu Matrix khi load hoặc hover.
* **Circular Text Path** - Chữ chạy uốn lượn theo một đường tròn xoay.
* **Gradient Stroke Animation** - Chữ rỗng (stroke) có dải màu gradient chạy dọc theo nét chữ.
* **Kinetic Typography Grid** - Lưới chữ tự dạt ra tránh con trỏ chuột khi đến gần.

### Tương tác vi mô & Hiệu ứng (Micro-Interactions & Effects)
* **Particle Explosion Button** - Nút bấm vỡ tung thành các hạt nhỏ li ti khi thực hiện thành công.
* **Liquid Pull-to-Refresh** - Vòng quay load trang co giãn như giọt nước chuẩn bị đứt.
* **Skeleton Shimmer** - Dải sáng chạy ngang qua các ô placeholder biểu thị đang tải dữ liệu.
* **Directional Hover-Aware Button** - Màu nền nút bấm tự tràn vào từ đúng hướng con trỏ chuột hover vào.
* **Ripple Click Effect** - Hiệu ứng sóng nước lan tỏa từ tọa độ click chuột.
* **Animated SVG Line Drawing** - Các nét vẽ vector tự vẽ chính nó trong thời gian thực.
* **Mesh Gradient Background** - Nền dải màu chuyển động chậm kiểu bong bóng dung nham (lava-lamp).
* **Lens Blur Depth** - Nền UI bị làm mờ sâu để làm nổi bật hành động ở tiền cảnh.

### Lựa chọn thư viện hoạt ảnh
* **Motion (`motion/react`)** - mặc định cho các chuyển động UI, bento và chuyển đổi trạng thái.
* **GSAP + ScrollTrigger** - dành riêng cho các trang kể chuyện cuộn (scrolltelling) và ghim cuộn ngang phức tạp. Bắt buộc cô lập trong các Client Component lá và có hàm cleanup trong `useEffect`.
* **Three.js / WebGL** - dành cho nền canvas và không gian 3D. Tuân thủ quy tắc cô lập tương tự.
* **KHÔNG TRỘN LẪN GSAP / Three.js với Motion trong cùng một cây component.** Chúng sẽ xung đột tài nguyên và tranh giành khung hình xử lý (frames).

---

## 11. GIAO THỨC REDESIGN

Kỹ năng này xử lý cả **xây dựng mới (greenfield) VÀ nâng cấp (redesign)**. Nhận định sai chế độ làm việc là nguyên nhân số 1 khiến sản phẩm redesign bị hỏng.

### 11.A Nhận diện chế độ (hành động đầu tiên)
* **Greenfield** - không có trang web cũ, hoặc được duyệt cải tổ 100%. Áp dụng các mức dial cơ sở từ Phần 1.
* **Redesign - Bảo tồn (Preserve)** - hiện đại hóa giao diện nhưng không phá vỡ nhận diện thương hiệu cũ. Thực hiện audit trước, trích xuất các token thương hiệu cũ, tiến hành cải tiến từng bước mượt mà.
* **Redesign - Cải tổ (Overhaul)** - khoác lên ngôn ngữ thiết kế hoàn toàn mới trên nền nội dung cũ. Trực quan xử lý như greenfield, nhưng bắt buộc bảo tồn cấu trúc thông tin (IA) và nội dung cũ.

Nếu bối cảnh mơ hồ, hãy hỏi **đúng một câu**: *"Should this redesign preserve the existing brand, or are we starting visually from scratch?"* (Bản thiết kế lại này cần bảo tồn thương hiệu hiện tại hay chúng ta sẽ bắt đầu trực quan lại từ đầu?)

### 11.B Audit trước khi chạm vào code
Tuyên bố hiện trạng của trang web cũ trước khi đề xuất bất kỳ thay đổi nào:
* **Các token thương hiệu cũ** - màu sắc chính/nhấn, hệ thống font chữ, cách xử lý logo, bo góc.
* **Kiến trúc thông tin (IA)** - sơ đồ trang, thanh menu chính, các luồng chuyển đổi quan trọng.
* **Các khối nội dung** - phần nào đang hoạt động tốt, phần nào thừa thãi điền vào chỗ trống.
* **Các mẫu tương tác cần giữ lại** - các hiệu ứng tương tác đặc trưng, khối hero đặc trưng, tông giọng văn phong.
* **Các mẫu cần loại bỏ** - các dấu vết AI sáo rỗng, bố cục bị vỡ, link hỏng, ảnh stock chung chung, các bẫy hiệu năng.
* **Nhận định dial của trang cũ** - tự suy luận mức `DESIGN_VARIANCE` / `MOTION_INTENSITY` / `VISUAL_DENSITY` hiện tại. Đây là vạch xuất phát của bạn, không phải vạch mặc định.
* **Hiện trạng SEO** - các trang đang xếp hạng tốt, tiêu đề meta, dữ liệu cấu trúc, ảnh OG card. **Di chuyển SEO an toàn là rủi ro số 1 khi redesign.**

### 11.C Các quy tắc bảo tồn
* **Không thay đổi kiến trúc thông tin** trừ khi được yêu cầu. Giữ nguyên slug của URL, các anchor ID, nhãn điều hướng chính để bảo vệ SEO và thói quen sử dụng của người dùng.
* **Trích xuất màu thương hiệu trước khi áp dụng Phần 4.2.** Thương hiệu đang dùng màu tím thì giữ nguyên màu tím &rarr; kích hoạt ghi đè của quy tắc màu tím (LILA RULE).
* **Giữ nguyên văn phong copy** trừ khi được yêu cầu viết lại. Hiện đại hóa trực quan &ne; viết lại nội dung.
* **Tôn trọng các kết quả tiếp cận (a11y) đã có.** Không làm suy giảm các trạng thái focus, alt text, điều hướng bàn phím và tương phản.
* **Tôn trọng các sự kiện đo lường (analytics).** Không đổi tên nút bấm, trường nhập liệu của form, ID section mà hệ thống tracking phía sau đang phụ thuộc vào.

### 11.D Các đòn bẩy hiện đại hóa (theo thứ tự ưu tiên)
Áp dụng theo đúng trình tự - dừng lại ngay khi yêu cầu được thỏa mãn:
1. **Làm mới Typography** - nâng cấp trực quan lớn nhất với mức độ rủi ro thấp nhất.
2. **Cân chỉnh Spacing & Nhịp điệu** - tăng padding section, sửa nhịp điệu khoảng cách dọc.
3. **Hiệu chỉnh Màu sắc** - giảm bớt độ bão hòa, thống nhất màu trung tính, giữ nguyên màu nhấn thương hiệu.
4. **Thêm lớp Chuyển động** - bổ sung các tương tác vi mô phù hợp với mức `MOTION_INTENSITY` vào các component hiện có.
5. **Cấu trúc lại phần Hero & các section cốt lõi** - tổ chức lại nửa trên trang sử dụng bộ từ vựng Phần 10.
6. **Thay thế toàn bộ khối** - chỉ thực hiện khi khối cũ hoàn toàn không thể cứu vãn được.

### 11.E Cây quyết định: Tiến hóa tập trung vs Thiết kế lại toàn diện
* IA, nội dung và SEO ổn định &rarr; **tiến hóa tập trung (targeted evolution)** (áp dụng Đòn bẩy 1-4). Mang lại ~70% giá trị với chỉ ~40% rủi ro.
* Nợ thiết kế trực quan mang tính hệ thống (IA vỡ, không có design system, mobile hỏng) &rarr; **thiết kế lại toàn diện (full redesign)** kết hợp bảo tồn nội dung nghiêm ngặt.
* Bản thân thương hiệu đang thay đổi &rarr; **xây dựng mới hoàn toàn (greenfield)**.

### 11.F Những thứ KHÔNG BAO GIỜ được tự ý thay đổi âm thầm
Tuyệt đối không chỉnh sửa khi chưa được người dùng duyệt rõ ràng:
* Cấu trúc URL / slug của route.
* Nhãn của thanh menu chính (primary nav labels).
* Tên trường (name attribute) hoặc thứ tự trong form (gây vỡ analytics + lỗi autofill).
* Logo thương hiệu hoặc wordmark.
* Các văn bản pháp lý / đồng thuận / cookie hiện có.

---

## 12. THƯ VIỆN KHỐI (THE BLOCK LIBRARY - Cam kết triển khai cuốn chiếu)

Từ vựng tham chiếu (Phần 10) đặt tên cho các mẫu thiết kế. Thư viện Khối sẽ thực thi chúng bằng code thực tế, thông số chuyển động thực tế và cấu trúc API props thực tế.

**Trạng thái:** Đã định nghĩa schema tại đây. Các khối sẽ được bổ sung cuốn chiếu. Không tự ý viết các khối mới ngoài schema này.

### 12.A Sơ đồ thư mục
```
skills/taste-skill/blocks/
  hero/
    asymmetric-split.md
    editorial-manifesto.md
    kinetic-type.md
    ...
  feature/
    bento-grid.md
    sticky-scroll-stack.md
    zig-zag.md
    ...
  social-proof/
  pricing/
  cta/
  footer/
  navigation/
  portfolio/
  transition/
```

### 12.B Khai báo Frontmatter bắt buộc
```yaml
---
name: asymmetric-split-hero
category: hero
dial_compatibility:
  variance: [6, 10]
  motion: [3, 10]
  density: [2, 5]
when_to_use: "Trang landing có 1 asset mạnh và 1 thông điệp chính. Phù hợp làm hero mặc định cho SaaS, agency, tiêu dùng cao cấp."
not_for: "Trang ra mắt dạng biên tập / manifesto nơi thông điệp chữ chính là toàn bộ thiết kế."
stack: ["react", "next", "tailwind", "motion"]
---
```

### 12.C Các phần bắt buộc trong nội dung
1. **Phác thảo trực quan (Visual sketch)** - sơ đồ ASCII hoặc mô tả ngắn gọn về bố cục.
2. **Props API** - giao diện của component.
3. **Mã nguồn mẫu (Code sketch)** - mã nguồn tối giản hoạt động được (mặc định dùng Server Component, dùng Client island cho phần chuyển động).
4. **Cơ chế co giãn mobile (Mobile fallback)** - quy tắc thu gọn rõ ràng cho màn hình `< 768px`.
5. **Các biến thể chuyển động (Motion variants)** - một biến thể cho mỗi dải `MOTION_INTENSITY` (1-3, 4-7, 8-10). Nêu rõ fallback cho chế độ giảm chuyển động.
6. **Lưu ý chế độ tối (Dark-mode notes)** - chiến lược token màu sắc cụ thể cho khối này.
7. **Các lỗi thường gặp (Anti-patterns)** - các cách triển khai sai phổ biến của khối này.
8. **Tham chiếu (References)** - link dẫn tới các trang đang hoạt động thực tế trong production.

### 12.D Kỷ luật của Thư viện Khối
* Mỗi khối nằm trong một file riêng biệt. Không gộp nhiều khối vào một file.
* Mọi khối phải hoạt động độc lập (thả vào trang là render được ngay).
* Mọi khối phải vượt qua bảng tự kiểm tra Pre-Flight Check (Phần 14).
* Các khối phụ thuộc vào một hệ thống thiết kế cụ thể ở Phần 2.A sẽ được đặt tên theo định dạng `blocks/<category>/<name>--<system>.md` (ví dụ: `feature/bento-grid--material.md`).

---

## 13. NGOÀI PHẠM VI (OUT OF SCOPE)

Kỹ năng này KHÔNG dành cho:
* Dashboard / giao diện sản phẩm dày đặc dữ liệu / trang quản trị admin (hãy dùng Fluent, Carbon, Atlassian, hoặc Polaris từ Phần 2.A).
* Bảng dữ liệu lớn (data tables) (hãy dùng TanStack Table hoặc AG Grid).
* Form nhiều bước / wizard phức tạp (hãy dùng các pattern chuyên dụng cho Form; kỹ năng này không giải quyết tốt hơn).
* Trình soạn thảo code (code editors) (hãy dùng Monaco / CodeMirror với giao diện chính thức của chúng).
* Giao diện di động native (Native mobile) (hãy dùng Apple HIG hoặc Material trực tiếp).
* Giao diện cộng tác thời gian thực (realtime collab UIs) (như hiển thị con trỏ của người khác, đồng bộ OT - thuộc nhóm bài toán khác).

Nếu yêu cầu rơi vào các trường hợp trên, **hãy nói rõ với người dùng**, chỉ ra công cụ phù hợp, và chỉ áp dụng các phần marketing / giới thiệu / landing page của kỹ năng này lên các bề mặt tương thích.
## 14. BẢNG TỰ KIỂM TRA TRƯỚC KHI BÀN GIAO (FINAL PRE-FLIGHT CHECK)

Hãy chạy bảng kiểm tra này trước khi xuất mã nguồn. Đây là bộ lọc cuối cùng để đảm bảo chất lượng.

**BẢNG NÀY LÀ BẮT BUỘC. Hãy kiểm tra từng ô. Nếu bất kỳ ô nào thất bại, mã nguồn chưa sẵn sàng bàn giao.**

- [ ] **Đã tuyên bố nhận định thiết kế** (Design Read một dòng ở Phần 0.B)?
- [ ] **Các giá trị nút vặn (dial values)** được làm rõ và lập luận từ yêu cầu, không âm thầm dùng giá trị cơ sở mặc định?
- [ ] **Đã chọn hệ thống thiết kế** từ Phần 2 nếu phù hợp, hoặc dán nhãn phong cách thẩm mỹ một cách trung thực?
- [ ] **Đã nhận diện chế độ redesign** và thực hiện audit (nếu phù hợp, Phần 11)?
- [ ] **KHÔNG CÓ BẤT KỲ DẤU EM-DASH (`—`) NÀO TRÊN TRANG.** Tiêu đề, eyebrow, badge, body text, quote, attribution, caption, nút bấm, alt text. Bằng không. (Phần 9.G - không thương lượng.)
- [ ] **Khóa Theme của Trang**: CHỈ MỘT theme duy nhất (sáng, tối, hoặc tự động) cho cả trang. Không đảo ngược theme giữa trang (Phần 4.11)?
- [ ] **Khóa Nhất Quán Màu Sắc**: một màu nhấn được dùng thống nhất trên mọi section (Phần 4.2)?
- [ ] **Khóa Nhất Quán Hình Khối**: một hệ thống bo góc được áp dụng thống nhất ở mọi vị trí (Phần 4.4)?
- [ ] **Kiểm tra Tương Phản Nút Bấm**: chữ trên nút bấm CTA hiển thị rõ ràng trên nền (không dùng trắng-trên-trắng, đạt chuẩn WCAG AA 4.5:1)?
- [ ] **CTA không bị co dòng**: không có nhãn nút bấm CTA nào bị xuống dòng thành 2+ dòng trên desktop?
- [ ] **Kiểm tra Tương Phản Form**: ô nhập liệu, placeholder, viền focus, nhãn đều đạt chuẩn WCAG AA trên nền của section?
- [ ] **Kỷ luật font Serif**: nếu dùng serif, đảm bảo đó KHÔNG phải Fraunces hoặc Instrument_Serif (hoặc dùng với lập luận thương hiệu rõ ràng)? Sử dụng font serif khác với dự án trước đó?
- [ ] **Kiểm tra bảng màu tiêu dùng cao cấp**: nếu yêu cầu là tiêu dùng cao cấp (nồi niêu, sức khỏe, thủ công, xa xỉ), bảng màu KHÔNG thuộc họ kem+đồng+đỏ oxblood+espresso mặc định của AI? Khác họ màu với dự án tiêu dùng cao cấp trước đó?
- [ ] **Khoảng cách chữ in nghiêng có đuôi**: mọi từ in nghiêng chứa các chữ cái `y g j p q` đều có tối thiểu `leading-[1.1]` + `pb-1` dự phòng?
- [ ] **Hero nằm gọn trong khung nhìn**: tiêu đề ≤ 2 dòng, subtext mô tả ≤ 20 từ VÀ ≤ 4 dòng, nút CTA nhìn thấy được mà không cần cuộn trang, tỷ lệ font được lên kế hoạch đồng thời với ảnh?
- [ ] **Padding trên của Hero**: tối đa `pt-24` trên desktop, nội dung hero không bị trôi lơ lửng xuống nửa dưới viewport?
- [ ] **Kỷ luật xếp chồng Hero**: tối đa 4 thành phần chữ trong hero (eyebrow HOẶC dải thương hiệu, headline, subtext, các nút CTA)? Không có tagline nhỏ dưới nút CTA, không có dải logo đối tác bên trong hero?
- [ ] **ĐẾM SỐ LƯỢNG EYEBROW (cơ học)**: đếm số lượng class `uppercase tracking` trên các tiêu đề section ở tất cả các component. Số lượng ≤ ceil(tổng số section / 3)? Hero tính là 1.
- [ ] **Cấm Chia Đôi Tiêu Đề Section**: không dùng mô hình \"tiêu đề lớn bên trái + đoạn văn giải thích nhỏ bên phải\" làm tiêu đề section (thay thế bằng xếp chồng dọc)?
- [ ] **Giới hạn lặp lại zigzag**: không xếp 3+ section liên tiếp có chung bố cục chia đôi ảnh+chữ?
- [ ] **Không Trùng Lặp Ý Đồ CTA**: không dùng hai nút CTA có cùng một mục đích hành động trên một trang (ví dụ có cả \"Liên hệ ngay\" + \"Trò chuyện\" = Thất bại)?
- [ ] **Dải logo = chỉ chứa logo**: không in nhãn ngành/phân khúc phía dưới logo?
- [ ] **Đa dạng hóa nền bento**: tối thiểu 2-3 ô trong lưới bento có sự đa dạng trực quan rõ rệt (ảnh, gradient, pattern), không phải toàn bộ đều là thẻ chữ trắng-trên-trắng?
- [ ] **Dải logo đối tác \"Trusted by / Used by\"** đặt DƯỚI phần hero chứ không phải bên trong hero, sử dụng logo dạng SVG thật (Simple Icons / devicon) hoặc ký hiệu SVG tự vẽ, KHÔNG dùng wordmark dạng text thuần?
- [ ] **Tự kiểm tra chữ (Copy Self-Audit)**: đọc lại từng chuỗi chữ hiển thị, không ship các câu sai ngữ pháp hoặc do AI tự chế (kiểu \"free on its past\")?
- [ ] **Hoạt ảnh có lý do**: mọi animation đều có thể được giải thích lý do trong một câu (phân cấp / kể chuyện / phản hồi / chuyển trạng thái), không dùng GSAP chỉ để biểu diễn?
- [ ] **Marquee tối đa một lần trên trang**: không dùng 2 dải marquee cuộn ngang trên cùng một trang?
- [ ] **Thanh điều hướng trên MỘT dòng duy nhất** trên desktop, chiều cao ≤ 80px?
- [ ] **Kiểm tra lặp lại bố cục section**: không có hai section nào dùng chung một họ bố cục (tối thiểu 4 họ bố cục khác nhau trên 8 section)?
- [ ] **Lưới bento có nhịp điệu VÀ số ô chính xác** (N nội dung &rarr; N ô, không có ô trống ở giữa hoặc ở cuối)?
- [ ] **Danh sách dài dùng đúng component UI** (không dùng thẻ `<ul>` với `divide-y` mặc định cho các danh sách > 5 mục - xem các giải pháp thay thế ở Phần 4.9)?
- [ ] **Sử dụng hình ảnh thực tế** (ưu tiên ảnh sinh từ AI, rồi đến Picsum-seed, cuối cùng là các khe placeholder được đánh dấu) - KHÔNG dùng div để giả lập screenshot, KHÔNG tự vẽ SVG trang trí tùy tiện, KHÔNG dùng phong cách tối giản chỉ có chữ?
- [ ] **Không đè badge/nhãn lên ảnh** (không ghi `Plate · Brand`, không ghi `Field notes - journal` đè lên ảnh)?
- [ ] **Không dùng chú thích ghi công ảnh để trang trí** (như kiểu `Field study no. 12 · Ines Caetano`)?
- [ ] **Không ghi phiên bản ở chân trang marketing** (các kiểu `v1.4.2`, `Build 0048`)?
- [ ] **Không chèn câu meta nhỏ dưới eyebrow** (\"Mỗi tính năng dưới đây đều hoạt động thực tế...\")?
- [ ] **Không đặt dải chữ chạy trang trí ở chân hero** (các kiểu `BRAND. MOTION. SPATIAL.`)?
- [ ] **Không để đoạn văn giải thích trôi nổi góc trên bên phải tiêu đề section**?
- [ ] **Không vẽ thanh điểm số có đường track nền** làm hình ảnh so sánh?
- [ ] **Không hiển thị dải địa điểm / thời gian / thời tiết** trừ khi yêu cầu thực sự là thương hiệu toàn cầu hoặc gắn liền với địa danh?
- [ ] **Không dùng gợi ý cuộn trang** (như `Scroll`, `↓ scroll`, `Cuộn để khám phá`)?
- [ ] **Không dùng nhãn phiên bản trong hero** (như `V0.6`, `BETA`, `INVITE-ONLY`) trừ khi bối cảnh thực sự là buổi ra mắt?
- [ ] **Không dùng tiêu đề eyebrow đánh số thứ tự** (như `00 / INDEX`, `001 · Capabilities`, `06 · how it works`)?
- [ ] **Không dùng chấm tròn để trang trí** (mặc định bằng không, chỉ dùng khi biểu thị trạng thái logic thực tế)?
- [ ] **Không dùng cả đường viền trên và dưới** cho mỗi dòng của danh sách dài / bảng thông số kỹ thuật?
- [ ] **Mật độ nội dung hợp lý**: không dùng bảng dữ liệu 20 dòng, không tự chế các con số chính xác giả, mặc định các đoạn mô tả phụ ≤ 25 từ?
- [ ] **Câu trích dẫn ≤ 3 dòng**, phần ghi nguồn sạch sẽ (không dùng em-dash)?
- [ ] **Hoạt ảnh thực tế**: nếu `MOTION_INTENSITY > 4`, trang web thực sự chuyển động chứ không chỉ khai báo trên giấy?
- [ ] **GSAP sticky-stack / horizontal-pan** được triển khai đúng theo khung chuẩn ở Phần 5.A / 5.B (bắt đầu bằng `start: \"top top\"`, `pin: true`, scrub chuẩn)?
- [ ] **Không dùng `window.addEventListener('scroll')`** - chỉ sử dụng `useScroll()` của Motion / ScrollTrigger / IntersectionObserver / CSS scroll-driven animations?
- [ ] **Tất cả các hoạt ảnh vượt mức `MOTION_INTENSITY > 3` đều được bọc fallback giảm chuyển động**?
- [ ] **Các token chế độ tối được định nghĩa và kiểm thử** trên cả hai chế độ?
- [ ] **Cơ chế co giãn mobile rõ ràng** (khai báo `w-full`, `px-4`, `max-w-7xl mx-auto`) cho các bố cục bất đối xứng?
- [ ] **Độ ổn định viewport**: dùng `min-h-[100dvh]`, không bao giờ dùng `h-screen`?
- [ ] **Các hoạt ảnh chạy trong `useEffect` bắt buộc có hàm dọn dẹp (cleanup)**?
- [ ] **Đã chuẩn bị đầy đủ các trạng thái trống / tải dữ liệu / báo lỗi**?
- [ ] **Ưu tiên dùng khoảng trắng thay thế các khung thẻ card** bất cứ khi nào có thể?
- [ ] **Chỉ dùng icon từ các thư viện được phép** (Phosphor / HugeIcons / Radix / Tabler), không tự viết mã path SVG vẽ icon?
- [ ] **Hoạt ảnh được cô lập trong các Client Component lá** có dòng `'use client'` ở đỉnh file, được memoize?
- [ ] **Không phạm phải Dấu vết AI ở Phần 9** (dùng Inter mặc định, màu tím AI, 3 thẻ card bằng nhau, tên Jane Doe, thương hiệu Acme, tiêu đề \"Quietly in use at\")?
- [ ] **Đảm bảo đạt chỉ số Core Web Vitals** (LCP < 2.5s, INP < 200ms, CLS < 0.1)?
- [ ] **Chỉ sử dụng duy nhất một hệ thống thiết kế** cho mỗi dự án (không trộn lẫn Material với shadcn)?

Nếu có dù chỉ một ô kiểm không thể tích chọn một cách trung thực, sản phẩm chưa hoàn thành. Hãy sửa lỗi trước khi bàn giao.

---

# PHỤ LỤC (APPENDICES) - Tài liệu tham khảo thực tế dựa trên nguồn gốc

Các phần dưới đây chứa tài liệu tham khảo chính thức. Chúng cung cấp cho Agent lệnh cài đặt thực tế, link tài liệu chuẩn và các đoạn code khởi đầu hoạt động được cho từng hệ thống thiết kế được nêu trong Phần 2. Hãy dùng chúng để làm điểm tựa cho các quyết định thực thi, thay vì các thông tin suy đoán từ dữ liệu huấn luyện.

## Phụ lục A - Lệnh cài đặt cho từng Hệ thống Thiết kế

```bash
# Material Web (Material 3)
npm install @material/web

# Fluent UI React (v9)
npm install @fluentui/react-components

# Fluent UI Web Components (framework-free)
npm install @fluentui/web-components @fluentui/tokens

# IBM Carbon
npm install @carbon/react @carbon/styles

# Radix Themes
npm install @radix-ui/themes

# shadcn/ui (code mở, sở hữu component của chính mình)
npx shadcn@latest init
npx shadcn@latest add button card badge separator input

# Primer CSS (giao diện sản phẩm/devtool của GitHub)
npm install --save @primer/css

# Primer Brand (giao diện marketing của GitHub)
npm install @primer/react-brand

# GOV.UK Frontend (dịch vụ công Anh Quốc)
npm install govuk-frontend

# USWDS (Hệ thống thiết kế web của Chính phủ Hoa Kỳ)
npm install uswds

# Atlassian Design System (Atlaskit)
yarn add @atlaskit/css-reset @atlaskit/tokens @atlaskit/button @atlaskit/badge @atlaskit/section-message @atlaskit/card

# Bootstrap 5.3
npm install bootstrap

# Shopify Polaris Web Components (chỉ dành cho ứng dụng Shopify)
# Thêm thẻ này vào thẻ head HTML của ứng dụng:
#   <meta name="shopify-api-key" content="%SHOPIFY_API_KEY%" />
#   <script src="https://cdn.shopify.com/shopifycloud/polaris.js"></script>
```

## Phụ lục B - Nguồn tài liệu chính thức (Hãy đọc trước khi tự viết lại từ đầu)

### Material Web
- https://github.com/material-components/material-web
- https://material-web.dev/theming/material-theming/
- https://m3.material.io/develop/web

### Fluent UI
- https://fluent2.microsoft.design/get-started/develop
- https://fluent2.microsoft.design/components/web/react/
- https://github.com/microsoft/fluentui
- https://learn.microsoft.com/en-us/fluent-ui/web-components/

### Carbon
- https://carbondesignsystem.com/
- https://github.com/carbon-design-system/carbon
- https://carbondesignsystem.com/developing/react-tutorial/overview/
- https://carbondesignsystem.com/developing/web-components-tutorial/overview/

### Shopify Polaris
- https://shopify.dev/docs/api/app-home/web-components
- https://github.com/Shopify/polaris-react
- https://polaris-react.shopify.com/components

### Atlassian
- https://atlassian.design/get-started/develop
- https://atlassian.design/components/button/examples
- https://atlaskit.atlassian.com/packages/design-system/button/example/disabled
- https://atlassian.design/tokens/design-tokens

### Primer
- https://primer.style/
- https://github.com/primer/css
- https://github.com/primer/brand

### GOV.UK
- https://design-system.service.gov.uk/components/button/
- https://design-system.service.gov.uk/styles/layout/
- https://github.com/alphagov/govuk-frontend

### USWDS
- https://designsystem.digital.gov/documentation/developers/
- https://designsystem.digital.gov/components/button/
- https://designsystem.digital.gov/components/card/
- https://github.com/uswds/uswds

### Bootstrap
- https://getbootstrap.com/docs/5.3/layout/grid/
- https://getbootstrap.com/docs/5.3/components/card/

### Tailwind
- https://tailwindcss.com/docs/dark-mode
- https://tailwindcss.com/blog/tailwindcss-v4

### Radix
- https://www.radix-ui.com/themes/docs/components/theme
- https://www.radix-ui.com/themes/docs/components/card
- https://github.com/radix-ui/themes

### shadcn/ui
- https://ui.shadcn.com/docs
- https://ui.shadcn.com/docs/components/card
- https://github.com/shadcn-ui/ui

### CSS nguyên bản & Tiêu chuẩn W3C
- https://developer.mozilla.org/en-US/docs/Web/CSS/Reference/Properties/backdrop-filter
- https://developer.mozilla.org/en-US/docs/Web/CSS/Reference/At-rules/@media/prefers-color-scheme
- https://developer.mozilla.org/en-US/docs/Web/CSS/Reference/At-rules/@media/prefers-reduced-motion
- https://developer.mozilla.org/en-US/docs/Web/CSS/Guides/Grid_layout
- https://developer.mozilla.org/en-US/docs/Web/CSS/Guides/Scroll-driven_animations
- https://drafts.csswg.org/scroll-animations-1/

### Apple Liquid Glass (Chỉ dành cho các nền tảng Apple)
- https://developer.apple.com/design/human-interface-guidelines/materials
- https://developer.apple.com/documentation/TechnologyOverviews/liquid-glass
- https://developer.apple.com/documentation/TechnologyOverviews/adopting-liquid-glass
- https://developer.apple.com/documentation/SwiftUI/Material

---

## Phụ lục C - Apple Liquid Glass: Bản xấp xỉ Trung thực trên môi trường Web

**Không** tự nhận bừa các đoạn CSS ngẫu nhiên là Apple Liquid Glass chính thức.

### Những gì là chính thức từ Apple
Apple chỉ tài liệu hóa Liquid Glass bên trong Hướng dẫn giao diện con người (Human Interface Guidelines) và Tài liệu nhà phát triển (Developer Documentation) dành riêng cho **các nền tảng của Apple**. Đây là một chất liệu động được sử dụng trong giao diện hệ thống của Apple. Bản thực thi gốc của Apple thuộc về các API hệ thống và component native của Apple, **không phải là một package CSS web công khai**.

Tài liệu chính thức liên quan:
- Apple Human Interface Guidelines &rarr; Materials
- Apple Developer Documentation &rarr; Liquid Glass
- Apple Developer Documentation &rarr; Adopting Liquid Glass
- SwiftUI &rarr; Material

### Những gì KHÔNG phải là chính thức
Không có file `liquid-glass.css` nào do Apple phát hành cho các trang web thông thường.

Một bản xấp xỉ trung thực trên web có thể sử dụng kết hợp:
- `backdrop-filter`
- Màu nền bán trong suốt (transparent backgrounds)
- Các lớp đường viền xếp lớp (layered borders)
- Lớp phủ highlight phản xạ ánh sáng (highlight overlays)
- Gradients màu
- Hiệu ứng chuyển động (motion)
- Các fallback độ tương phản cao cho trường hợp không hỗ trợ

Nhưng đó chỉ là **bản xấp xỉ web glassmorphism / frosted-glass**, không phải Apple Liquid Glass chính thức. Hãy ghi chú rõ ràng điều này trong các comment code.

### Khung code CSS mẫu cho bản xấp xỉ trên Web

```css
.liquid-glass-web-approx {
  position: relative;
  isolation: isolate;
  overflow: hidden;
  border-radius: 999px;
  border: 1px solid rgb(255 255 255 / .32);
  background:
    linear-gradient(135deg, rgb(255 255 255 / .30), rgb(255 255 255 / .08)),
    rgb(255 255 255 / .12);
  backdrop-filter: blur(24px) saturate(180%) contrast(1.05);
  -webkit-backdrop-filter: blur(24px) saturate(180%) contrast(1.05);
  box-shadow:
    inset 0 1px 0 rgb(255 255 255 / .48),
    inset 0 -1px 0 rgb(255 255 255 / .12),
    0 18px 60px rgb(0 0 0 / .18);
}

.liquid-glass-web-approx::before {
  content: "";
  position: absolute;
  inset: 0;
  z-index: -1;
  border-radius: inherit;
  background:
    radial-gradient(circle at 20% 0%, rgb(255 255 255 / .55), transparent 34%),
    linear-gradient(90deg, rgb(255 255 255 / .18), transparent 42%, rgb(255 255 255 / .14));
  pointer-events: none;
}

.liquid-glass-web-approx::after {
  content: "";
  position: absolute;
  inset: 1px;
  border-radius: inherit;
  border: 1px solid rgb(255 255 255 / .14);
  pointer-events: none;
}

@media (prefers-color-scheme: dark) {
  .liquid-glass-web-approx {
    border-color: rgb(255 255 255 / .18);
    background:
      linear-gradient(135deg, rgb(255 255 255 / .16), rgb(255 255 255 / .04)),
      rgb(15 23 42 / .42);
    box-shadow:
      inset 0 1px 0 rgb(255 255 255 / .22),
      0 18px 60px rgb(0 0 0 / .42);
  }
}

@media (prefers-reduced-transparency: reduce) {
  .liquid-glass-web-approx {
    background: rgb(255 255 255 / .96);
    backdrop-filter: none;
    -webkit-backdrop-filter: none;
  }
}
```

**Quan trọng:** Thuộc tính `prefers-reduced-transparency` có độ tương thích trình duyệt chưa đồng đều; hãy kiểm thử kỹ lưỡng. Luôn cung cấp đủ độ tương phản ngay cả khi không có hiệu ứng blur.

---

**Hết phần phụ lục.** Các lệnh cài đặt phía trên là những điểm neo thực tế. Khung code Apple Liquid Glass là bản mô phỏng được ghi nhãn rõ ràng, không phải package chính thức do Apple phát hành. Để biết thêm tài liệu chuẩn cho từng hệ thống thiết kế, hãy tham khảo trang chủ tài liệu tương ứng (link ở Phần 2 và Phụ lục B).
