USE base
GO

-- ======================
-- 👤 Bảng Account: Lưu thông tin người dùng & hệ thống
-- ======================
CREATE TABLE [account] (
    account_id BIGINT IDENTITY(1,1) PRIMARY KEY,          -- ID tự tăng
    account_guid UNIQUEIDENTIFIER UNIQUE DEFAULT NEWID(), -- GUID duy nhất toàn hệ thống

    created_at DATETIME DEFAULT GETDATE(),                -- Ngày tạo tài khoản
    updated_at DATETIME DEFAULT GETDATE(),                -- Ngày cập nhật

    role NVARCHAR(50) DEFAULT 'User' NOT NULL,            -- Vai trò: User, Admin...

    -- Thông tin cá nhân
    full_name NVARCHAR(200) NOT NULL,                     -- Họ tên
    avatar NVARCHAR(500) NULL,                            -- Ảnh đại diện
    bio NVARCHAR(500) NULL,                               -- Giới thiệu
    user_address NVARCHAR(500) NULL,                      -- Địa chỉ
    birthday DATE DEFAULT NULL,                           -- Ngày sinh
    gender NVARCHAR(20) DEFAULT 'Unknown',                -- Giới tính

    -- Mạng xã hội
    reputation_score INT DEFAULT 100,
    number_follower INT DEFAULT 0,
    number_following INT DEFAULT 0,
    number_post INT DEFAULT 0,

    -- Thông tin tài chính
    account_number NVARCHAR(100) UNIQUE,                  -- Số tài khoản
    account_amount BIGINT DEFAULT 0,                      -- Số dư
    currency NVARCHAR(20) DEFAULT 'USD'                   -- Loại tiền
);
GO

-- 📝 Trigger INSERT
CREATE TRIGGER TRG_Account_Insert
ON [account]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'account',
        CAST(account_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- 📝 Trigger UPDATE
CREATE TRIGGER TRG_Account_Update
ON [account]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'account',
        CAST(i.account_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT 
                (SELECT d.* FROM deleted d WHERE d.account_id = i.account_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.account_id = i.account_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.account_id = d.account_id;
END;
GO

-- 📝 Trigger DELETE
CREATE TRIGGER TRG_Account_Delete
ON [account]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'account',
        CAST(account_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
