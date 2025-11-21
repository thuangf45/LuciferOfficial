USE base
GO

-- ======================
-- 🎬 Bảng Film: Lưu thông tin phim do người dùng đăng
-- ======================
CREATE TABLE [film] (
    film_id BIGINT IDENTITY(1,1) PRIMARY KEY,          -- ID tự tăng
    film_guid UNIQUEIDENTIFIER DEFAULT NEWID() UNIQUE, -- Mã định danh duy nhất

    film_name NVARCHAR(200) NOT NULL,                  -- Tên phim
    film_description NVARCHAR(1000) NOT NULL,          -- Mô tả phim
    files NVARCHAR(MAX) NULL,                          -- Danh sách URL/tập phim (JSON array)

    film_cost DECIMAL(18,2) DEFAULT 0,                 -- Giá thuê/mua phim
    avg_rating DECIMAL(3,2) DEFAULT 0,                 -- Điểm đánh giá trung bình
    number_review INT DEFAULT 0,                       -- Số lượt đánh giá
    number_view INT DEFAULT 0,                         -- Số lượt xem

    created_at DATETIME DEFAULT GETDATE(),             -- Ngày tạo
    updated_at DATETIME DEFAULT GETDATE(),             -- Ngày cập nhật

    account_guid UNIQUEIDENTIFIER NOT NULL,            -- Mã người đăng phim
    account_id BIGINT NOT NULL                         -- ID người đăng phim
);
GO

-- Trigger INSERT: Ghi log khi thêm phim mới
CREATE TRIGGER TRG_Film_Insert
ON [film]
AFTER INSERT
AS
BEGIN
    INSERT INTO data_audit(table_name, record_id, action_type, data)
    SELECT 
        'film',
        CAST(film_guid AS NVARCHAR(100)),
        'INSERT',
        (SELECT i.* FROM inserted i FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM inserted i;
END;
GO

-- Trigger UPDATE: Ghi log khi cập nhật phim
CREATE TRIGGER TRG_Film_Update
ON [film]
AFTER UPDATE
AS
BEGIN
    INSERT INTO data_audit(table_name, record_id, action_type, data)
    SELECT 
        'film',
        CAST(i.film_guid AS NVARCHAR(100)),
        'UPDATE',
        (
            SELECT 
                (SELECT d.* FROM deleted d WHERE d.film_id = i.film_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS OldData,
                (SELECT i2.* FROM inserted i2 WHERE i2.film_id = i.film_id FOR JSON PATH, WITHOUT_ARRAY_WRAPPER) AS NewData
            FOR JSON PATH, WITHOUT_ARRAY_WRAPPER
        )
    FROM inserted i
    JOIN deleted d ON i.film_id = d.film_id;
END;
GO

-- Trigger DELETE: Ghi log khi xóa phim
CREATE TRIGGER TRG_Film_Delete
ON [film]
AFTER DELETE
AS
BEGIN
    INSERT INTO data_audit(table_name, record_id, action_type, data)
    SELECT 
        'film',
        CAST(film_guid AS NVARCHAR(100)),
        'DELETE',
        (SELECT d.* FROM deleted d FOR JSON PATH, WITHOUT_ARRAY_WRAPPER)
    FROM deleted d;
END;
GO
