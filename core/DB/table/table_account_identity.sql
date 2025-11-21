USE base
GO

-- ======================
-- 2️ Account Identity (multi-login, passwordless + password)
-- ======================
CREATE TABLE [account_identity] (
    identity_id BIGINT IDENTITY(1,1) PRIMARY KEY,
    identity_guid UNIQUEIDENTIFIER DEFAULT NEWID(),

    provider NVARCHAR(50) NOT NULL,        -- google, facebook, phone, email, apple, local...
    provider_key NVARCHAR(255) NOT NULL,   -- email, OAuth sub_id, phone number...

    password_hash NVARCHAR(255) NULL,      -- mật khẩu (hash), chỉ dùng cho provider = 'local'

    created_at DATETIME DEFAULT GETDATE(),
    last_used DATETIME NULL,
    is_verified BIT DEFAULT 0,

    account_id BIGINT NOT NULL,
    account_guid UNIQUEIDENTIFIER NOT NULL,

    -- Chỉ giữ Unique cho định danh
    CONSTRAINT UQ_AccountIdentity_ProviderKey UNIQUE(provider, provider_key),
    CONSTRAINT UQ_AccountIdentity_Guid UNIQUE(identity_guid)
);
GO

-- 📝 Trigger INSERT
CREATE TRIGGER TRG_AccountIdentity_Insert
ON [account_identity]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit(table_name, record_id, action_type, data)
    SELECT 
        'account_identity',
        CAST(identity_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- 📝 Trigger UPDATE
CREATE TRIGGER TRG_AccountIdentity_Update
ON [account_identity]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit(table_name, record_id, action_type, data)
    SELECT 
        'account_identity',
        CAST(i.identity_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT 
                (SELECT d.* FROM deleted d WHERE d.identity_id = i.identity_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.identity_id = i.identity_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.identity_id = d.identity_id;
END;
GO

-- 📝 Trigger DELETE
CREATE TRIGGER TRG_AccountIdentity_Delete
ON [account_identity]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit(table_name, record_id, action_type, data)
    SELECT 
        'account_identity',
        CAST(identity_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
