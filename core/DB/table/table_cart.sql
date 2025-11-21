USE base
GO

-- ======================
-- 🛒 Bảng Cart: Lưu giỏ hàng của người dùng
-- ======================
CREATE TABLE [cart] (
    cart_id BIGINT IDENTITY(1,1) PRIMARY KEY,          -- ID tự tăng
    cart_guid UNIQUEIDENTIFIER DEFAULT NEWID() UNIQUE, -- Mã định danh duy nhất

    cart_details NVARCHAR(MAX) NULL,                   -- JSON mảng các item: item_id, quantity, price

    created_at DATETIME DEFAULT GETDATE(),             -- Ngày tạo
    updated_at DATETIME DEFAULT GETDATE(),             -- Ngày cập nhật

    account_id BIGINT NOT NULL,                        -- ID người sở hữu giỏ hàng
    account_guid UNIQUEIDENTIFIER NOT NULL             -- GUID người sở hữu giỏ hàng
);
GO

-- Trigger INSERT: Ghi log khi tạo giỏ hàng
CREATE TRIGGER TRG_Cart_Insert
ON [cart]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit(table_name, record_id, action_type, data)
    SELECT
        'cart',
        CAST(cart_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- Trigger UPDATE: Ghi log khi cập nhật giỏ hàng
CREATE TRIGGER TRG_Cart_Update
ON [cart]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit(table_name, record_id, action_type, data)
    SELECT
        'cart',
        CAST(i.cart_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT 
                (SELECT d.* FROM deleted d WHERE d.cart_id = i.cart_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.cart_id = i.cart_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.cart_id = d.cart_id;
END;
GO

-- Trigger DELETE: Ghi log khi xóa giỏ hàng
CREATE TRIGGER TRG_Cart_Delete
ON [cart]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit(table_name, record_id, action_type, data)
    SELECT
        'cart',
        CAST(cart_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
