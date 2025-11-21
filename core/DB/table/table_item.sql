USE base
GO

-- ======================
-- 📦 Bảng Item: Lưu thông tin sản phẩm trong cửa hàng
-- ======================
CREATE TABLE [item] (
    item_id BIGINT IDENTITY(1,1) PRIMARY KEY,          -- ID tự tăng
    item_guid UNIQUEIDENTIFIER DEFAULT NEWID() UNIQUE, -- Mã định danh duy nhất

    item_name NVARCHAR(200) NOT NULL,                  -- Tên sản phẩm
    item_description NVARCHAR(1000) NULL,              -- Mô tả sản phẩm
    media_links NVARCHAR(MAX) NULL,                    -- JSON array: ["https://...img.jpg", "https://...video.mp4"]

    avg_rating DECIMAL(3,2) DEFAULT 0,                 -- Điểm đánh giá trung bình
    number_review INT DEFAULT 0,                       -- Số lượt đánh giá
    price DECIMAL(18,2) NOT NULL DEFAULT 0,            -- Giá bán
    stock INT NOT NULL DEFAULT 0,                      -- Số lượng tồn kho
    is_active BIT DEFAULT 1,                           -- Trạng thái đang bán

    created_at DATETIME DEFAULT GETDATE(),             -- Ngày tạo
    updated_at DATETIME DEFAULT GETDATE(),             -- Ngày cập nhật

    shop_id BIGINT NOT NULL,                           -- ID cửa hàng
    shop_guid UNIQUEIDENTIFIER NOT NULL                -- GUID cửa hàng
);
GO

-- Trigger INSERT: Ghi log khi tạo sản phẩm
CREATE TRIGGER TRG_Item_Insert
ON [item]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'item',
        CAST(item_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- Trigger UPDATE: Ghi log khi cập nhật sản phẩm
CREATE TRIGGER TRG_Item_Update
ON [item]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'item',
        CAST(i.item_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT 
                (SELECT d.* FROM deleted d WHERE d.item_id = i.item_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.item_id = i.item_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.item_id = d.item_id;
END;
GO

-- Trigger DELETE: Ghi log khi xóa sản phẩm
CREATE TRIGGER TRG_Item_Delete
ON [item]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'item',
        CAST(item_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
