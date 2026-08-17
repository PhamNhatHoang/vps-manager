---
name: full-output-enforcement
description: Ghi đè hành vi cắt bớt (truncation) mặc định của LLM. Ép buộc sinh mã nguồn đầy đủ, cấm các cấu trúc placeholder (giữ chỗ) và xử lý phân đoạn giới hạn token một cách sạch sẽ. Áp dụng cho mọi tác vụ yêu cầu đầu ra trọn vẹn, không rút gọn.
---

# Ép Buộc Đầu Ra Đầy Đủ (Full-Output Enforcer)

## Nguyên Tắc Cơ Bản

Hãy coi mọi tác vụ đều cực kỳ quan trọng đối với môi trường production. Một đầu ra bị cắt bớt một phần là một đầu ra bị lỗi. Không tối ưu hóa cho sự ngắn gọn — hãy tối ưu hóa cho sự trọn vẹn. Nếu người dùng yêu cầu toàn bộ tệp tin, hãy cung cấp toàn bộ tệp tin. Nếu người dùng yêu cầu 5 component, hãy cung cấp đầy đủ cả 5 component. Không có ngoại lệ.

---

## Các Cấu Trúc Đầu Ra Bị CẤM

Các cấu trúc dưới đây được coi là lỗi nặng. Tuyệt đối không được tạo ra chúng:

*   **Trong khối code:** `// ...`, `// phần code còn lại`, `// triển khai ở đây`, `// TODO`, `/* ... */`, `// tương tự như trên`, `// tiếp tục mô hình này`, `// thêm vào khi cần thiết`, hoặc ký tự `...` đứng một mình thay thế cho phần mã bị bỏ qua.
*   **Trong câu chữ (prose):** "Hãy báo cho tôi nếu bạn muốn tôi tiếp tục", "Tôi có thể cung cấp thêm chi tiết nếu cần", "để ngắn gọn", "phần còn lại tuân theo cùng một mô hình", "tương tự đối với các phần còn lại", "và vân vân" (khi thay thế nội dung thực tế), "Tôi sẽ để phần đó như một bài tập tự làm".
*   **Rút ngắn cấu trúc:** Chỉ xuất ra một khung xương (skeleton) trong khi yêu cầu là triển khai đầy đủ. Chỉ hiển thị phần đầu và phần cuối trong khi bỏ qua phần giữa. Thay thế logic lặp lại bằng một ví dụ và một lời mô tả. Mô tả code nên làm gì thay vì thực sự viết dòng code đó.

---

## Quy Trình Thực Thi

1.  **Xác định Phạm vi (Scope):** Đọc toàn bộ yêu cầu. Đếm xem có bao nhiêu sản phẩm đầu ra riêng biệt được mong đợi (tệp tin, hàm, phần, câu trả lời). Khóa số lượng đó lại.
2.  **Xây dựng (Build):** Tạo ra mọi sản phẩm đầu ra một cách trọn vẹn. Không có bản nháp một phần, không có kiểu "bạn có thể tự mở rộng phần này sau".
3.  **Kiểm tra chéo (Cross-check):** Trước khi phản hồi, hãy đọc lại yêu cầu ban đầu. So sánh số lượng sản phẩm thực tế của bạn với số lượng đã khóa ở bước 1. Nếu thiếu bất kỳ thứ gì, hãy bổ sung trước khi trả lời.

---

## Xử Lý Đầu Ra Quá Dài

Khi câu trả lời sắp chạm giới hạn token của cửa sổ ngữ cảnh:

*   Không nén các phần còn lại để cố nhét chúng vào.
*   Không nhảy cóc đến phần kết luận.
*   Viết ở chất lượng đầy đủ nhất cho đến một điểm dừng sạch sẽ (kết thúc một hàm, kết thúc một tệp, kết thúc một phần).
*   Kết thúc phản hồi bằng dòng thông báo chính xác:

```txt
[TẠM DỪNG — Đã hoàn thành X trên Y. Gửi "tiếp tục" hoặc "continue" để tiếp tục từ: <tên phần tiếp theo>]
```

Khi người dùng gửi lệnh "tiếp tục", hãy bắt đầu chính xác tại nơi bạn đã dừng lại. Không cần tóm tắt lại, không lặp lại phần trước.

---

## Kiểm Tra Nhanh Trước Khi Gửi

Trước khi hoàn tất bất kỳ phản hồi nào, hãy xác minh:
*   Không có cấu trúc bị cấm nào ở danh sách trên xuất hiện trong phản hồi.
*   Mọi mục người dùng yêu cầu đều hiện diện và hoàn thành.
*   Các khối code chứa code thực tế chạy được, không phải lời mô tả code.
*   Không có nội dung nào bị rút ngắn để tiết kiệm không gian.
