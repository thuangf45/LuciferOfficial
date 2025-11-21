USE base
GO

-- ======================
-- 🎟️ Bảng Voucher: Lưu thông tin mã giảm giá
-- ======================
CREATE TABLE [voucher] (
    voucher_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    voucher_guid UNIQUEIDENTIFIER DEFAULT NEWID() UNIQUE,

    code NVARCHAR(50) UNIQUE NOT NULL,
    description NVARCHAR(200) NULL,

    discount_type NVARCHAR(20) NOT NULL,   -- percent hoặc amount
    discount_value DECIMAL(18,2) NOT NULL,
    max_discount DECIMAL(18,2) NULL,       -- giá trị giảm tối đa

    valid_from DATETIME NOT NULL,
    valid_to DATETIME NOT NULL,

    is_active BIT DEFAULT 1,

    max_usage INT DEFAULT 1,
    used_count INT DEFAULT 0,

    used_users NVARCHAR(MAX) NULL,

    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);
GO

-- Trigger INSERT: Ghi log khi tạo voucher
CREATE TRIGGER TRG_Voucher_Insert
ON [voucher]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT
        'voucher',
        CAST(voucher_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- Trigger UPDATE: Ghi log khi cập nhật voucher
CREATE TRIGGER TRG_Voucher_Update
ON [voucher]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT
        'voucher',
        CAST(i.voucher_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT
                (SELECT d.* FROM deleted d WHERE d.voucher_id = i.voucher_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.voucher_id = i.voucher_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.voucher_id = d.voucher_id;
END;
GO

-- Trigger DELETE: Ghi log khi xóa voucher
CREATE TRIGGER TRG_Voucher_Delete
ON [voucher]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT
        'voucher',
        CAST(voucher_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
