

-- ======================
-- 🏪 Bảng Shop: Lưu thông tin cửa hàng của người dùng
-- ======================
CREATE TABLE [shop] (
    shop_id BIGINT IDENTITY(1,1) PRIMARY KEY,                          -- ID tự tăng
    shop_guid UNIQUEIDENTIFIER DEFAULT NEWID() UNIQUE,                -- Mã định danh duy nhất

    shop_name NVARCHAR(100) NOT NULL,                                 -- Tên cửa hàng
    shop_description NVARCHAR(1000) NULL,                             -- Mô tả cửa hàng
    shop_address NVARCHAR(500) NULL,                                  -- Địa chỉ
    phone_number NVARCHAR(20) NULL,                                   -- Số điện thoại
    email NVARCHAR(200) NULL,                                         -- Email liên hệ
    avg_rating DECIMAL(3,2) DEFAULT 0 CHECK (avg_rating BETWEEN 0 AND 5), -- Điểm đánh giá trung bình

    shop_coin DECIMAL(18,2) DEFAULT 0 CHECK (shop_coin >= 0),         -- Số coin tích lũy
    number_item INT DEFAULT 0 CHECK (number_item >= 0),               -- Số sản phẩm
    number_order INT DEFAULT 0 CHECK (number_order >= 0),             -- Số đơn hàng
    number_review INT DEFAULT 0 CHECK (number_review >= 0),           -- Số lượt đánh giá
    is_active BIT DEFAULT 1,                                          -- Trạng thái hoạt động

    created_at DATETIME DEFAULT GETDATE() CHECK (created_at <= GETDATE()), -- Ngày tạo
    updated_at DATETIME DEFAULT GETDATE() CHECK (updated_at <= GETDATE()), -- Ngày cập nhật

    account_id BIGINT NOT NULL,                                       -- ID chủ shop
    account_guid UNIQUEIDENTIFIER NOT NULL,                           -- GUID chủ shop

    CONSTRAINT FK_Shop_AccountId FOREIGN KEY (account_id) REFERENCES [account](account_id),
    CONSTRAINT FK_Shop_AccountGuid FOREIGN KEY (account_guid) REFERENCES [account](account_guid)
);
GO


-- Trigger INSERT: Ghi log khi tạo shop
CREATE TRIGGER TRG_Shop_Insert
ON [shop]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'shop',
        CAST(shop_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- Trigger UPDATE: Ghi log khi cập nhật shop
CREATE TRIGGER TRG_Shop_Update
ON [shop]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'shop',
        CAST(i.shop_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT 
                (SELECT d.* FROM deleted d WHERE d.shop_id = i.shop_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.shop_id = i.shop_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.shop_id = d.shop_id;
END;
GO

-- Trigger DELETE: Ghi log khi xóa shop
CREATE TRIGGER TRG_Shop_Delete
ON [shop]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'shop',
        CAST(shop_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
