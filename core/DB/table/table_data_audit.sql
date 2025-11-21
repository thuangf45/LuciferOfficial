USE base
GO

CREATE TABLE [data_audit] (
    audit_id BIGINT IDENTITY(1,1),
    table_name NVARCHAR(100),     -- Tên bảng thay đổi
    record_id NVARCHAR(100),      -- Khóa chính của bản ghi
    action_type NVARCHAR(10),     -- INSERT, UPDATE, DELETE
    action_time DATETIME DEFAULT GETDATE(),
    action_by NVARCHAR(100) NULL, -- Người thao tác (nếu có)
    PRIMARY KEY (audit_id),

    -- Lưu toàn bộ dữ liệu dạng JSON (linh hoạt cho mọi bảng)
    data NVARCHAR(MAX)
);