USE base
GO

-- ======================
-- 📝 Bảng Post: Lưu bài viết và bình luận của người dùng
-- ======================
CREATE TABLE [post] (
    post_id BIGINT IDENTITY(1,1) PRIMARY KEY,          -- ID tự tăng
    post_guid UNIQUEIDENTIFIER DEFAULT NEWID() UNIQUE, -- Mã định danh duy nhất

    content NVARCHAR(1000),                            -- Nội dung bài viết hoặc bình luận
    media_links NVARCHAR(MAX) NULL,                    -- JSON array: ["https://...img.jpg", "https://...video.mp4"]

    created_at DATETIME DEFAULT GETDATE(),             -- Ngày tạo
    updated_at DATETIME DEFAULT GETDATE(),             -- Ngày cập nhật

    number_comment INT DEFAULT 0,                      -- Số bình luận
    number_reaction INT DEFAULT 0,                     -- Số lượt tương tác

    parent_id BIGINT NULL,                             -- NULL = bài gốc, có giá trị = bình luận
    parent_guid UNIQUEIDENTIFIER NULL,                 -- GUID của bài gốc (nếu là bình luận)

    account_id BIGINT NOT NULL,                        -- ID người đăng
    account_guid UNIQUEIDENTIFIER NOT NULL             -- GUID người đăng
);
GO

-- Trigger INSERT: Ghi log khi tạo bài viết/bình luận
CREATE TRIGGER TRG_Post_Insert
ON [post]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'post',
        CAST(post_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- Trigger UPDATE: Ghi log khi cập nhật bài viết/bình luận
CREATE TRIGGER TRG_Post_Update
ON [post]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'post',
        CAST(i.post_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT 
                (SELECT d.* FROM deleted d WHERE d.post_id = i.post_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.post_id = i.post_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.post_id = d.post_id;
END;
GO

-- Trigger DELETE: Ghi log khi xóa bài viết/bình luận
CREATE TRIGGER TRG_Post_Delete
ON [post]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit (table_name, record_id, action_type, data)
    SELECT 
        'post',
        CAST(post_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
