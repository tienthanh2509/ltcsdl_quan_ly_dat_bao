USE QLDATBAO

-- 1. Tạo thủ tục có tên sub_CAPNHAT_TST_PHIEUDATBAO thực hiện cập nhật tổng số tiền cho tất cả các phiếu đặt báo trong bảng PHIEUDATBAO.
-- Thực thi thủ tục để cập nhật.

GO
IF OBJECT_ID('dbo.sub_CAPNHAT_TST_PHIEUDATBAO') IS NOT NULL
	DROP PROCEDURE dbo.sub_CAPNHAT_TST_PHIEUDATBAO
GO

CREATE PROCEDURE dbo.sub_CAPNHAT_TST_PHIEUDATBAO
AS BEGIN
  UPDATE PHIEUDATBAO
  SET PHIEUDATBAO.TONGSOTIEN = c.TONGSOTIEN
  FROM
  	(SELECT c.SOPHIEU, SUM(c.SOTIEN) AS TONGSOTIEN
    FROM CTDATBAO c
    GROUP BY c.SOPHIEU) c
  WHERE PHIEUDATBAO.SOPHIEU = c.SOPHIEU
END
GO

-- Tạo function có thên fn_TONGSOTIEN_PHIEUDATBAO với tham số truyền vào là Số Phiếu,
--  thực hiện tính toán và trả về tổng thành tiền của số phiếu đặt báo tương ứng.
IF OBJECT_ID('dbo.fn_TONGSOTIEN_PHIEUDATBAO') IS NOT NULL
	DROP FUNCTION dbo.fn_TONGSOTIEN_PHIEUDATBAO
GO

CREATE FUNCTION dbo.fn_TONGSOTIEN_PHIEUDATBAO(@SOPHIEU NCHAR(5))
RETURNS MONEY
AS BEGIN
  -- Cập nhật giá
  UPDATE PHIEUDATBAO
  SET PHIEUDATBAO.TONGSOTIEN = c.TONGSOTIEN
  FROM
  	(SELECT c.SOPHIEU, SUM(c.SOTIEN) AS TONGSOTIEN
    FROM CTDATBAO c
    GROUP BY c.SOPHIEU) c
  WHERE PHIEUDATBAO.SOPHIEU = @SOPHIEU

  DECLARE @Tong MONEY
  SET @Tong = (SELECT p.TONGSOTIEN FROM PHIEUDATBAO p WHERE p.SOPHIEU = @SOPHIEU)
  RETURN @Tong
END
GO

EXEC sub_CAPNHAT_TST_PHIEUDATBAO -- Cập nhật lại số tiền của phiếu đặt báo
PRINT dbo.fn_TONGSOTIEN_PHIEUDATBAO('PH001')

--3. Tạo Trigger cho hành động thêm dữ liệu trên bảng CTDATBAO.
--Khi người dùng thêm 1 dòng dữ liệu vào bảng CTDATBAO, trigger sẽ thực hiện các công việc sau:
-- Kiểm tra tính tồn tại của Số phiếu trong bảng PHIEUDATBAO
-- Kiểm tra tính tồn tại của Mã tạp chí trong bảng TAPCHI.
-- 1<=Tháng bắt đầu và Tháng kết thúc <=12. Tháng kết thúc>=Tháng bắt đầu+3.
-- Số tiền>0
-- Khi các điều kiện trên đều đúng, ghi nhận việc thêm mới.
-- Ngược lại, in thông báo lỗi cụ thể cho người dùng.
GO
IF OBJECT_ID('trg_insert_CTDATBAO', 'TR') IS NOT NULL
	DROP TRIGGER trg_insert_CTDATBAO
GO

CREATE TRIGGER trg_insert_CTDATBAO ON CTDATBAO
	INSTEAD OF INSERT
AS 
BEGIN
	DECLARE @SOPHIEU NCHAR(5)
  DECLARE @MATC NCHAR(3)
  DECLARE @THANGBD DATETIME
  DECLARE @THANGKT DATETIME
  DECLARE @SOTIEN MONEY
  DECLARE @ERROR NVARCHAR(100)

  SELECT @MATC = MATC, @SOPHIEU = SOPHIEU, @THANGBD = THANGBD, @THANGKT = THANGKT, @SOTIEN = SOTIEN
  FROM INSERTED

  -- Kiểm tra tính tồn tại của Số phiếu trong bảng PHIEUDATBAO
    IF NOT EXISTS (SELECT p.SOPHIEU FROM PHIEUDATBAO p WHERE p.SOPHIEU = @SOPHIEU)
      BEGIN
        SET @ERROR = N'Không tồn tại phiếu có mã ' + @SOPHIEU
        RAISERROR(@ERROR, 16, 1)
        RETURN
      END
  -- Kiểm tra tính tồn tại của Mã tạp chí trong bảng TAPCHI.
    IF NOT EXISTS (SELECT t.MATC FROM TAPCHI t WHERE t.MATC = @MATC)
      BEGIN
        SET @ERROR = N'Không tồn tại tạp chí có mã ' + @MATC
        RAISERROR(@ERROR, 16, 1)
        RETURN
      END
  -- 1<=Tháng bắt đầu và Tháng kết thúc <=12. Tháng kết thúc>=Tháng bắt đầu+3.
    IF NOT (@THANGBD >= 1 AND @THANGKT <= 12 AND @THANGBD >= 1 AND @THANGKT >= @THANGBD+3)
      BEGIN
        SET @ERROR = N'1<=Tháng bắt đầu và Tháng kết thúc <=12. Tháng kết thúc>=Tháng bắt đầu+3'
        RAISERROR(@ERROR, 16, 1)
        RETURN
      END
  -- Số tiền>0
    IF @SOTIEN <= 0
      BEGIN
        SET @ERROR = N'Số tiền>0'
        RAISERROR(@ERROR, 16, 1)
        RETURN
      END
  -- Khi các điều kiện trên đều đúng, ghi nhận việc thêm mới.
  -- Ngược lại, in thông báo lỗi cụ thể cho người dùng.
    INSERT INTO CTDATBAO
       SELECT *
       FROM INSERTED
END
GO
