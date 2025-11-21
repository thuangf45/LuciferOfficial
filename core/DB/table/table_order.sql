USE base
GO

-- ======================
-- 📦 Bảng Order: Lưu đơn hàng của người dùng
-- ======================
CREATE TABLE [order] (
    order_id BIGINT IDENTITY(1,1) PRIMARY KEY,          -- ID tự tăng
    order_guid UNIQUEIDENTIFIER DEFAULT NEWID() UNIQUE, -- Mã định danh duy nhất

    order_name NVARCHAR(100) NULL,                      -- Tên đơn hàng (nếu có)
    order_description NVARCHAR(1000) NULL,              -- Mô tả đơn hàng
    order_details NVARCHAR(MAX) NULL,                   -- JSON mảng các item: item_id, quantity, price

    shop_address NVARCHAR(500) NOT NULL,                -- Địa chỉ cửa hàng
    shoppers_address NVARCHAR(20) NULL,                 -- Địa chỉ người mua (online)
    shoppers_phone_number VARCHAR(20) NULL,             -- SĐT người mua (online)

    form_shopping NVARCHAR(20) DEFAULT 'offline',       -- Hình thức mua
    payment_status NVARCHAR(20) DEFAULT 'paid',         -- Trạng thái thanh toán
    shipping_status NVARCHAR(30) NULL,                  -- Trạng thái vận chuyển
    payment_method NVARCHAR(20) DEFAULT 'cash in person', -- Phương thức thanh toán

    total_amount DECIMAL(18,2) DEFAULT 0,               -- Tổng tiền hàng
    discount_amount DECIMAL(18,2) DEFAULT 0,            -- Số tiền giảm
    final_amount AS (total_amount - discount_amount) PERSISTED, -- Số tiền phải trả

    created_at DATETIME DEFAULT GETDATE(),              -- Ngày tạo
    updated_at DATETIME DEFAULT GETDATE(),              -- Ngày cập nhật

    shop_id BIGINT NOT NULL,                            -- ID cửa hàng
    shop_guid UNIQUEIDENTIFIER NOT NULL,                -- GUID cửa hàng
    account_id BIGINT NOT NULL,                         -- ID người đặt hàng
    account_guid UNIQUEIDENTIFIER NOT NULL              -- GUID người đặt hàng
);
GO

-- Trigger INSERT: Ghi log khi tạo đơn hàng
CREATE TRIGGER TRG_Order_Insert
ON [order]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'order',
        CAST(order_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- Trigger UPDATE: Ghi log khi cập nhật đơn hàng
CREATE TRIGGER TRG_Order_Update
ON [order]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'order',
        CAST(i.order_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT 
                (SELECT d.* FROM deleted d WHERE d.order_id = i.order_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.order_id = i.order_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.order_id = d.order_id;
END;
GO

-- Trigger DELETE: Ghi log khi xóa đơn hàng
CREATE TRIGGER TRG_Order_Delete
ON [order]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'order',
        CAST(order_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
