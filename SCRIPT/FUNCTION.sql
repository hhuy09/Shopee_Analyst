USE SHOPEE
GO

----------------FUNCTION----------------

--Chức năng 1: Đăng nhập
CREATE PROCEDURE USP_LOGIN @USERNAME NVARCHAR(50), @PASSWORD NVARCHAR(50)
AS
BEGIN
	DECLARE @RES INT
	SET @RES = 0
	IF EXISTS (SELECT * FROM DBO.ACCOUNT WHERE USERNAME = @USERNAME AND PASSWORD = @PASSWORD)
	BEGIN
		PRINT N'ĐĂNG NHẬP THÀNH CÔNG'
		SET @RES = 1
		RETURN
	END
	ELSE
	BEGIN
		PRINT N'ĐĂNG NHẬP THẤT BẠI'
		RETURN
	END
END
GO

EXEC USP_LOGIN 'user1', 'pass1'
GO

-------------------------------------------------------------------------------------------------------------
--Chức năng 2: Tìm kiếm sản phẩm:
----------tìm kiếm theo danh mục

CREATE FUNCTION UF_Find_Product_by_Category(@Category nvarchar(100))
RETURNS TABLE
AS RETURN SELECT DISTINCT PRODUCT_NAME, SHOP_NAME, AVERAGE_RATING, PRICE, GROUP_NAME,  IMAGE_LINK
from CATEGORY, PRODUCT, SHOP, IMAGE, RATING, CLASSIFICATION 
where CATEGORY_NAME = @Category and CATEGORY = CATEGORY_ID and SHOP = SHOP_ID and PRODUCT_ID= I_PRODUCT and PRODUCT_ID = PRODUCT
GO

CREATE PROC USP_Find_Product_by_Category(@Category nvarchar(100))
AS
SELECT PRODUCT_NAME, SHOP_NAME, AVERAGE_RATING, PRICE, GROUP_NAME,  IMAGE_LINK
	FROM UF_Find_Product_by_Category(@Category)

EXEC USP_Find_Product_by_Category N'Thời Trang Nam'

--tìm kiếm theo tên sản phẩm
CREATE FUNCTION UF_Find_Product_by_Name(@name nvarchar(100))
RETURNS TABLE
AS RETURN SELECT DISTINCT  PRODUCT_NAME, SHOP_NAME, AVERAGE_RATING, PRICE, GROUP_NAME,  IMAGE_LINK
FROM PRODUCT, SHOP, IMAGE, CLASSIFICATION, RATING
WHERE PRODUCT_NAME = @name AND SHOP = SHOP_ID AND PRODUCT_ID= I_PRODUCT AND PRODUCT_ID = PRODUCT
GO

CREATE PROC USP_Find_Product_by_Name(@Name nvarchar(100))
AS
SELECT PRODUCT_NAME, SHOP_NAME, AVERAGE_RATING, PRICE, GROUP_NAME,  IMAGE_LINK
	FROM UF_Find_Product_by_Name(@Name)

EXEC USP_Find_Product_by_Name pname1

----------------------------------------------------------------------------------------------------------------------------------------------------------------

--chức năng 3: xem thông tin sản phẩm
--nhập mã sản phẩm để xem thông chi tiết của sản phẩm: hình,  giá, thông tin, nhận xét, đánh giá
IF OBJECT_ID('USP_Product_Detail') IS NOT NULL
	DROP PROC USP_Product_Detail
GO

CREATE PROC USP_Product_Detail(@ProductID char(15))
AS
BEGIN
	IF EXISTS (SELECT * FROM PRODUCT WHERE PRODUCT_ID = @ProductID) 
	BEGIN 
		SELECT DISTINCT  P.PRODUCT_NAME, P.SHOP, P.PRODUCE_TYPE, P.INFORMATION, P.PREODER, P.PRODUCT_STATUS, P.AVERAGE_RATING, P.WEIGHT, C.CATEGORY_NAME, I.IMAGE_LINK, CL.GROUP_NAME, CL.INVENTORY_NUMBER, CL.PRICE, R.STAR_NUMBER, RL.CONTEND
		FROM PRODUCT P,CATEGORY C, IMAGE I, CLASSIFICATION CL, RATING R, REPLY RL
		WHERE P.PRODUCT_ID = @ProductID       
		AND P.CATEGORY = C.CATEGORY_ID
		AND P.PRODUCT_ID = I.I_PRODUCT
		AND P.PRODUCT_ID = CL.PRODUCT
		AND P.PRODUCT_ID = R.R_PRODUCT 
		AND R.RATING_ID = RL.RATING
	END
END


EXEC USP_Product_Detail prd5377

----------------------------------------------------------------------------------------------------------------------------------------------------------------
--chức năng 4: đặt hàng
-- người mua phải nhập id của mình, id sản phẩm, phân loại sản phẩm, số lượng sản phẩm, chọn phương thức thánh toán, nhập mã giám giá, chọn đơn vị vận chuyển
IF OBJECT_ID('USP_AddOrder') IS NOT NULL
	DROP PROC USP_AddOrder
GO
CREATE PROC USP_AddOrder(@CustomerID char(15), @ProductID char(15), @groupName char(15) , @amount int, @methodID char(15), @procod char(15), @ship_unit char(15))
AS
BEGIN
		
	IF EXISTS (SELECT * FROM CUSTOMER WHERE CUSTOMER_ID = @CustomerID)
	BEGIN--kiểm tra xem có sản phẩm đó không
		IF EXISTS (SELECT * FROM PRODUCT WHERE PRODUCT_ID = @ProductID) --TV1
		BEGIN --kiểm tra xem lượng hàng tồn kho có đủ không
			DECLARE @inventory int
			DECLARE @groupID char(15)
			SELECT @groupID = GROUP_ID FROM CLASSIFICATION WHERE GROUP_NAME = @groupName AND PRODUCT = @ProductID
			SELECT @inventory = INVENTORY_NUMBER   --TV2
			FROM CLASSIFICATION WHERE @groupID = GROUP_ID
			IF (@inventory >= @amount)
				BEGIN --kiểm tra mã giảm giá có tồn tại hay không
					IF EXISTS (SELECT * FROM PROCODE WHERE P_CUSTOMER = @CustomerID AND P_CODE = @procod) --TV3
						BEGIN --kiểm tra số lượng mã giảm giá có > 0 không 
							DECLARE @procode_amount int
							SELECT @procode_amount = P_AMOUNT FROM PROCODE WHERE P_CUSTOMER = @CustomerID AND P_CODE = @procod
							IF (@procode_amount ) >0
								BEGIN
								DECLARE @totalMoney money
								DECLARE @totalWeight int
								SELECT @totalMoney = (PRICE * @amount) FROM CLASSIFICATION WHERE GROUP_ID = @groupID AND PRODUCT = @ProductID --TV4
								SELECT @totalWeight = (WEIGHT * @amount) FROM PRODUCT WHERE PRODUCT_ID = @ProductID --TV5

								DECLARE @pay money 
								IF (@methodID = 'MT04' ) SET @pay = @totalMoney
								ELSE SET @pay = 0

								DECLARE @address_acc char(15)
								SELECT @address_acc = ADD_INFO_ID FROM ADDRESS_INFO WHERE A_ACCOUNT = @CustomerID --TV6

								INSERT INTO ORDER_SHIPPING_INFO (O_CUSTOMER, SENDING_INFO, ORDER_DATE, PROCODE, TOTAL_MONEY, TOTAL_WEIGHT, SHIPPING_UNIT, PAY, PAYMENT_METHOD) --TV7
								VALUES(@CustomerID, @address_acc, GETDATE(), @procod, @totalMoney, @totalWeight, @ship_unit, @pay, @methodID)

								 --mã đơn hàng tự phát sinh tăng dần
								DECLARE @orderID char(15)
								SELECT @orderID = MAX(ORDER_ID) FROM ORDER_SHIPPING_INFO  --TV8

								INSERT INTO ORDER_DETAIL (O_GROUP, OD_ORDER, O_AMOUNT) --TV9
								VALUES (@groupID, @orderID, @amount)

								--giảm số lượng mã giảm giá đã dùng đi 1
								 UPDATE PROCODE
								 SET P_AMOUNT = @procode_amount - 1
								 WHERE P_CUSTOMER = @CustomerID AND P_CODE = @procod 
							END
						END
				END
		END
	END
END




exec USP_AddOrder 'acc1', 'prd1', 'size-color63934', 1, 'MT04', 'SHOPEE70', 'SU01'

----------------------------------------------------------------------------------------------------------------------------------------------------------------

--chức năng 5: xem thông tin đơn hàng 
-- người mua xem thông tin đơn hàng đã đặt
CREATE PROCEDURE UT_XEMTHONGTINDONHAN (@CustomerID CHAR(15),@order_ID CHAR(15))
AS 
BEGIN
	IF NOT EXISTS(SELECT * FROM dbo.ACCOUNT WHERE ACCOUNT_ID = @CustomerID)
		BEGIN
			PRINT(N'Mã khách hàng không tồn tại! Vui lòng nhập lại.')
		END
	ELSE
		BEGIN
		
			SELECT ORDER_ID,O_GROUP,O_AMOUNT,INTEND_TIME,SHIPPING_FEE,TOTAL_MONEY,PAYMENT_METHOD,dbo.PRODUCT.*,dbo.IMAGE.*
			FROM dbo.ORDER_DETAIL,dbo.ORDER_SHIPPING_INFO, dbo.ACCOUNT,dbo.CLASSIFICATION,dbo.PRODUCT,dbo.IMAGE
			WHERE ACCOUNT_ID = O_CUSTOMER
			AND ORDER_ID = OD_ORDER
			AND ORDER_ID =@order_ID --'oder100'
			AND GROUP_ID = O_GROUP
			AND PRODUCT_ID = PRODUCT
			AND PRODUCT_ID = I_PRODUCT
			AND ACCOUNT_ID =@CustomerID--'acc2618'
		END
END

EXEC UT_XEMTHONGTINDONHAN 'acc2618', 'oder100'

----------------------------------------------------------------------------------------------------------------------------------------------------------------

--chức năng 6: xem tình trạng đơn hàng
REATE PROC SP_XEMTINHTRANGDONHANG @s_order CHAR(15)
AS
BEGIN
	IF NOT EXISTS( SELECT * FROM ORDER_SHIPPING_INFO WHERE ORDER_ID = @s_order)
		BEGIN
			RAISERROR (N'Đơn hàng không tồn tại',16,1)
		END
	ELSE
		BEGIN
			SELECT STATUS_INFO.S_ORDER,STATUS.STATUS_NAME,STATUS_INFO.S_MODIFIED_DATE 
			FROM STATUS, STATUS_INFO 
			WHERE STATUS_ID = STATUS AND S_ORDER = @s_order
		END
END
GO

EXEC SP_XEMTINHTRANGDONHANG 'oder10001'
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------------

--chức năng 7: đánh giá sản phẩm

CREATE PROC SP_DANHGIASANPHAM @rating_id CHAR(15), @r_product CHAR(15),@r_customer CHAR(15),@star_number TINYINT,@comment NVARCHAR(1000)
AS
BEGIN
	IF NOT EXISTS( SELECT * FROM PRODUCT WHERE PRODUCT_ID = @r_product)
		BEGIN
			RAISERROR (N'Sản phẩm không tồn tại',16,1)
		END
	ELSE
		BEGIN
			INSERT INTO RATING(RATING_ID,R_PRODUCT,R_CUSTOMER,STAR_NUMBER,COMMENT,R_MODIFIED_DATE) 
			VALUES (@rating_id,@r_product,@r_customer,@star_number,@comment,GETDATE())
		END
END
GO

EXEC SP_DANHGIASANPHAM 'rt21042', 'prd10', 'acc1164', 5, 'cmt cmt cmt 1235'
GO

SELECT * FROM RATING WHERE RATING_ID = 'rt211242'
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------------

--chức năng 8: quản lý sản phẩm
-- THÊM SẢN PHẨM
CREATE PROC SP_THEMSANPHAM @product_id CHAR(15), @shop_id CHAR(15), @product_name NVARCHAR(100), @produce_type NVARCHAR(100),
					  @category_name NVARCHAR(100), @infomation NVARCHAR(1000), @description NVARCHAR(1000),
					  @pre_order BIT, @product_status BIT, @weight REAL,
					  @image_id CHAR(15), @image_link CHAR(500), 
					  @group_id CHAR(15), @group_name NVARCHAR(100), @price MONEY,@inventory_number INT
AS
BEGIN

	IF NOT EXISTS( SELECT * FROM SHOP WHERE SHOP_ID = @shop_id)
		BEGIN
			RAISERROR (N'Shop không tồn tại',16,1)
			RETURN
		END
	DECLARE @category_id CHAR(15)
	SET @category_id = (SELECT CATEGORY_ID FROM CATEGORY WHERE CATEGORY_NAME = @category_name)
	DECLARE @average_rating REAL
	IF NOT EXISTS( SELECT * FROM PRODUCT WHERE PRODUCT_ID = @product_id)
		SET @average_rating = 0
	ELSE
		BEGIN
			SET @average_rating = (SELECT AVG(STAR_NUMBER) FROM RATING WHERE R_PRODUCT = @product_id)
		END

	INSERT INTO PRODUCT (PRODUCT_ID,SHOP,PRODUCT_NAME,PRODUCE_TYPE,CATEGORY,INFORMATION,DESCRIPTION,PREODER,PRODUCT_STATUS,AVERAGE_RATING,WEIGHT)
	VALUES (@product_id,@shop_id,@product_name,@produce_type,@category_id,@infomation,@description,@pre_order,@product_status,@average_rating,@weight)

	INSERT INTO IMAGE(IMAGE_ID,I_PRODUCT,IMAGE_LINK)
	VALUES (@image_id,@product_id,@image_link)

	INSERT INTO CLASSIFICATION(GROUP_ID,GROUP_NAME,PRODUCT,PRICE,INVENTORY_NUMBER)
	VALUES (@group_id,@group_name,@product_id,@price,@inventory_number)
		
END
GO

EXEC SP_THEMSANPHAM 'prda123','acc1','pnamea123','ptypea123',N'Mẹ & Bé','informationa123','descriptiona123',False,True,1724,'imga123','http://img.a123','grpa123','size-colora123',495131.0000,95
GO

SELECT * FROM PRODUCT WHERE PRODUCT_ID = 'prda123'
SELECT * FROM IMAGE WHERE IMAGE_ID = 'imga123'
SELECT * FROM CLASSIFICATION WHERE GROUP_ID = 'grpa123'
GO

-- XÓA SẢN PHẨM
CREATE PROC SP_XOASANPHAM @product_id CHAR(15)
AS
BEGIN
	IF NOT EXISTS( SELECT * FROM PRODUCT WHERE PRODUCT_ID = @product_id)
		BEGIN
			RAISERROR (N'Sản phẩm không tồn tại',16,1)
		END
	ELSE
		BEGIN
			DELETE FROM IMAGE WHERE I_PRODUCT = @product_id
			DELETE FROM CLASSIFICATION WHERE PRODUCT = @product_id
			DELETE FROM PRODUCT WHERE PRODUCT_ID = @product_id
		END
END
GO

EXEC SP_XOASANPHAM 'prda123'
GO

-- CẬP NHẬT SẢN PHẨM
	-- CẬP NHẬT CÒN HÀNG HAY HẾT HÀNG ( PRODUCT STATUS)
CREATE PROC SP_CNSPPRODUCTSTATUS @product_id CHAR(15), @product_status BIT
AS
BEGIN
	IF NOT EXISTS( SELECT * FROM PRODUCT WHERE PRODUCT_ID = @product_id)
		BEGIN
			RAISERROR (N'Sản phẩm không tồn tại',16,1)
		END
	ELSE
		BEGIN
			UPDATE PRODUCT
			SET PRODUCT_STATUS = @product_status
			WHERE PRODUCT_ID = @product_id
		END
END
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------------

--chức năng 9: thống kê doanh thu
CREATE PROCEDURE UT_THONGKEDOANHTHU_THANG
(@SHOP_ID CHAR(15),@YEAR INT, @MONTH INT)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM dbo.SHOP WHERE SHOP_ID = @SHOP_ID)
		PRINT (N'Shop không tồn tại.')
	ELSE
	DECLARE @price MONEY,@amount INT
    DECLARE @doanhthu MONEY
	SET @doanhthu = 0
	DECLARE My_Cursor CURSOR FOR
		
		SELECT  PRICE,O_AMOUNT--,OD_ORDER,SHOP_ID,PRODUCT_ID,GROUP_ID
		FROM dbo.SHOP,dbo.PRODUCT ,dbo.CLASSIFICATION,dbo.ORDER_DETAIL
		WHERE SHOP_ID = SHOP
		AND PRODUCT_ID = PRODUCT
		AND GROUP_ID = O_GROUP
		AND OD_ORDER IN (SELECT S_ORDER
						FROM dbo.STATUS_INFO
						WHERE STATUS = 'S07'
						AND YEAR(S_MODIFIED_DATE) = @YEAR
						AND (SELECT MONTH(S_MODIFIED_DATE))= @MONTH)
		AND SHOP = @SHOP_ID
		
	OPEN My_Cursor 
	FETCH NEXT FROM My_Cursor INTO @price,@amount
	WHILE @@FETCH_STATUS = 0
		BEGIN
			SET @doanhthu = @doanhthu + @amount*@price
			FETCH NEXT FROM My_Cursor INTO  @price,@amount
			
		END
	SELECT @doanhthu TONGDOANHTHU
	CLOSE My_Cursor 
	DEALLOCATE My_Cursor
END
EXEC UT_THONGKEDOANHTHU_THANG 'acc199',2019,6

--chức năng 10: quản lý hàng tồn kho
-- Thống kê số lượng tồn kho của sản phẩm của Shop
CREATE PROCEDURE UT_SLTONKHOSHOP
(@SHOPID CHAR(15))
AS
BEGIN 
	SELECT  PRODUCT_ID,SUM((INVENTORY_NUMBER)) Total_Inventory
	FROM dbo.CLASSIFICATION,dbo.PRODUCT,dbo.SHOP
	WHERE SHOP_ID = SHOP
	AND SHOP_ID = @SHOPID
	AND PRODUCT = PRODUCT_ID
	GROUP BY PRODUCT_ID
END
EXEC UT_SLTONKHOSHOP 'acc1'


-- Cập nhật số lượng tồn kho của 1 sản phẩm 
CREATE PROCEDURE UT_UPDATESLTONKHO
(@SHOPID CHAR(15),@PRODUCTID CHAR(15),@GROUPID CHAR(15),@INVENTORY_NUMBER INT)
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM dbo.SHOP WHERE SHOP_ID = @SHOPID)
		BEGIN
			PRINT (N'Shop khong ton tai!')
		END

	ELSE
		BEGIN
			IF NOT EXISTS(SELECT * FROM dbo.PRODUCT WHERE PRODUCT_ID = @PRODUCTID)
				BEGIN
					PRINT (N'Mã sản phẩm không hợp lệ')
				END
			
			ELSE 
				IF NOT EXISTS(SELECT * FROM dbo.CLASSIFICATION WHERE PRODUCT = @PRODUCTID)
					BEGIN
						PRINT (N'Nhóm sản phẩm không tồn tại')
					END
				ELSE
					BEGIN
						UPDATE dbo.CLASSIFICATION
						SET INVENTORY_NUMBER = @INVENTORY_NUMBER
						WHERE PRODUCT = @PRODUCTID
						AND GROUP_ID = @GROUPID
					END
		END
END


EXEC UT_UPDATESLTONKHO 'acc1','prd1','grp81975',44


