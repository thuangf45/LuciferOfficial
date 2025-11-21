USE base
GO

-- ======================
-- ⭐ Bảng Review: Lưu đánh giá của người dùng cho item/shop
-- ======================
CREATE TABLE [review] (
    review_id BIGINT IDENTITY(1,1) PRIMARY KEY,          -- ID tự tăng
    review_guid UNIQUEIDENTIFIER DEFAULT NEWID() UNIQUE, -- Mã định danh duy nhất

    target_type NVARCHAR(50) NOT NULL,                   -- Loại đối tượng được đánh giá: item/shop
    rating INT DEFAULT 5,                                -- Điểm đánh giá (1–5)
    content NVARCHAR(2000) NULL,                         -- Nội dung đánh giá

    created_at DATETIME DEFAULT GETDATE(),               -- Ngày tạo
    updated_at DATETIME DEFAULT GETDATE(),               -- Ngày cập nhật

    account_id BIGINT NOT NULL,                          -- ID người đánh giá
    account_guid UNIQUEIDENTIFIER NOT NULL,              -- GUID người đánh giá
    target_id BIGINT NOT NULL,                           -- ID đối tượng được đánh giá
    target_guid UNIQUEIDENTIFIER NOT NULL,               -- GUID đối tượng được đánh giá

    CONSTRAINT UQ_Review_UniquePerTarget UNIQUE (account_guid, target_guid) -- Mỗi người chỉ được đánh giá 1 lần cho mỗi đối tượng
);
GO

-- Trigger INSERT: Ghi log khi tạo đánh giá
CREATE TRIGGER TRG_Review_Insert
ON [review]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'review',
        CAST(review_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- Trigger UPDATE: Ghi log khi cập nhật đánh giá
CREATE TRIGGER TRG_Review_Update
ON [review]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'review',
        CAST(i.review_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT 
                (SELECT d.* FROM deleted d WHERE d.review_id = i.review_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.review_id = i.review_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.review_id = d.review_id;
END;
GO

-- Trigger DELETE: Ghi log khi xóa đánh giá
CREATE TRIGGER TRG_Review_Delete
ON [review]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'review',
        CAST(review_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
