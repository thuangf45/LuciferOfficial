USE base
GO

-- ======================
-- 💬 Bảng Reaction: Lưu tương tác của người dùng với bài viết
-- ======================
CREATE TABLE [reaction] (
    reaction_id BIGINT IDENTITY(1,1) PRIMARY KEY,          -- ID tự tăng
    reaction_guid UNIQUEIDENTIFIER DEFAULT NEWID() UNIQUE, -- Mã định danh duy nhất

    reaction_type NVARCHAR(20),                            -- Loại tương tác: like, heart, haha, huhu
    created_at DATETIME DEFAULT GETDATE(),                 -- Ngày tạo
    updated_at DATETIME DEFAULT GETDATE(),                 -- Ngày cập nhật

    post_id BIGINT NOT NULL,                               -- ID bài viết
    post_guid UNIQUEIDENTIFIER NOT NULL,                   -- GUID bài viết
    account_id BIGINT NOT NULL,                            -- ID người tương tác
    account_guid UNIQUEIDENTIFIER NOT NULL                 -- GUID người tương tác
);
GO

-- Trigger INSERT: Ghi log khi tạo tương tác
CREATE TRIGGER TRG_Reaction_Insert
ON [reaction]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'reaction',
        CAST(reaction_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- Trigger UPDATE: Ghi log khi cập nhật tương tác
CREATE TRIGGER TRG_Reaction_Update
ON [reaction]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'reaction',
        CAST(i.reaction_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT 
                (SELECT d.* FROM deleted d WHERE d.reaction_id = i.reaction_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.reaction_id = i.reaction_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.reaction_id = d.reaction_id;
END;
GO

-- Trigger DELETE: Ghi log khi xóa tương tác
CREATE TRIGGER TRG_Reaction_Delete
ON [reaction]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'reaction',
        CAST(reaction_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
