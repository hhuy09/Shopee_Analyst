use SHOPEE
go


--1/inventory_number >= 0

CREATE TRIGGER UTG_INVENTORY_NUMBER
ON CLASSIFICATION 
FOR UPDATE, INSERT
AS IF UPDATE(INVENTORY_NUMBER)
BEGIN
	IF EXISTS(SELECT GROUP_ID
				FROM inserted
				WHERE PRICE<0)
		BEGIN
			 raiserror (N'Lỗi !!! Hàng tồn kho phải lớn hơn hoặc bằng 0', 16, 0)
			rollback    -- khôi phục lại dữ liệu
		END
END
GO
--2/ p_amount >= 0
CREATE TRIGGER UTG_P_AMOUNT
ON PROCODE 
FOR UPDATE, INSERT
AS IF UPDATE(P_AMOUNT)
BEGIN
	IF EXISTS(SELECT P_ACCOUNT, P_CODE
				FROM inserted
				WHERE P_AMOUNT < 0)
		BEGIN
			 raiserror (N'Lỗi !!! Số lượng phiếu giảm giá phải lớn hơn hoặc bằng 0', 16, 0)
			rollback    -- khôi phục lại dữ liệu
		END
END
GO
--3/weight > 0
CREATE TRIGGER UTG_WEIGHT 
ON PRODUCT 
FOR UPDATE, INSERT
AS IF UPDATE(WEIGHT)
BEGIN
	IF EXISTS(SELECT PRODUCT_ID
				FROM inserted
				WHERE WEIGHT <= 0)
		BEGIN
			 raiserror (N'Lỗi !!! Khối lượng hàng phải lớn hơn 0', 16, 0)
			rollback    -- khôi phục lại dữ liệu
		END
END
GO

--4/ price > 0
CREATE TRIGGER UTG_PRICE 
ON CLASSIFICATION 
FOR UPDATE, INSERT
AS IF UPDATE(PRICE)
BEGIN
	IF EXISTS(SELECT GROUP_ID
				FROM inserted
				WHERE PRICE <= 0)
		BEGIN
			 raiserror (N'Lỗi !!! Giá hàng phải lớn hơn 0', 16, 0)
			rollback    -- khôi phục lại dữ liệu
		END
END
GO

--5/ O_amount > 0
CREATE TRIGGER UTG_O_AMOUNT 
ON ORDER_DETAIL 
FOR UPDATE, INSERT
AS IF UPDATE(O_AMOUNT)
BEGIN
	IF EXISTS(SELECT O_GROUP, OD_ORDER
				FROM inserted
				WHERE O_AMOUNT <= 0)
		BEGIN
			 raiserror (N'Lỗi !!! Số lượng mua phải lớn hơn 0', 16, 0)
			rollback    -- khôi phục lại dữ liệu
		END
END
GO

--6/ Follow >= 0
CREATE TRIGGER UTG_FOLLOW 
ON SHOP 
FOR UPDATE, INSERT
AS IF UPDATE(FOLLOW)
BEGIN
	IF EXISTS(SELECT SHOP_ID
				FROM inserted
				WHERE FOLLOW < 0)
		BEGIN
			 raiserror (N'Lỗi !!! Lượt theo dõi phải lớn hoặc bằng hơn 0', 16, 0)
			rollback    -- khôi phục lại dữ liệu
		END
END
GO
--7/ Like >= 0
CREATE TRIGGER UTG_LIKE 
ON SHOP 
FOR UPDATE, INSERT
AS IF UPDATE(FOLLOW)
BEGIN
	IF EXISTS(SELECT SHOP_ID
				FROM inserted
				WHERE LIKE_SHOP < 0)
		BEGIN
			 raiserror (N'Lỗi !!! Lượt thích phải lớn hoặc bằng hơn 0', 16, 0)
			rollback    -- khôi phục lại dữ liệu
		END
END
GO
--8/ O_Amount <= InventoryNumber
CREATE TRIGGER UTG_OAMOUNT_INVNETORY
ON ORDER_DETAIL
FOR UPDATE, INSERT 
AS
BEGIN
	DECLARE @inventory_number int
	DECLARE @amount int
	DECLARE @groupID char(15)

	select @groupID = O_GROUP, @amount = O_AMOUNT
	from inserted

	select @inventory_number = INVENTORY_NUMBER
	from CLASSIFICATION

	if(@amount > @inventory_number)
	BEGIN
			 raiserror (N'Lỗi !!! Số lượng mua phải nhỏ hơn hoặc bằng số lượng hàng tồn kho', 16, 0)
			rollback    -- khôi phục lại dữ liệu
	END
END 
GO

--9/ Expiration <= Order Date
CREATE TRIGGER UTG_EXP_ORDER_DATE
ON ORDER_SHIPPING_INFO
FOR UPDATE, INSERT 
AS
BEGIN
	DECLARE @exp date
	DECLARE @order_date DATE
	DECLARE @pro_code char(15)

	SELECT @order_date = ORDER_DATE, @pro_code = PROCODE
	FROM inserted

	SELECT @EXP = EXPRIRATION_DATE
	FROM PROMOTION

	if(@order_date > @exp)
	BEGIN
			 raiserror (N'Lỗi !!! Mã khuyến mãi đã hết hạn sử dụng', 16, 0)
			rollback    -- khôi phục lại dữ liệu
	END

END
go

--10/ Delivery >= Order Date
CREATE TRIGGER UTG_DELIVERY_ORDER_DATE
ON ORDER_SHIPPING_INFO
FOR UPDATE, INSERT 
AS
BEGIN
	DECLARE @order_date date
	DECLARE @delivery DATE

	SELECT @delivery = DELIVERY_DATE, @order_date = ORDER_DATE
	FROM inserted

	if(@delivery < @order_date)
	BEGIN
			 raiserror (N'Lỗi !!! Ngày giao hàng phải cùng ngày hoặc sau ngày đặt hàng.', 16, 0)
			rollback    -- khôi phục lại dữ liệu
	END

END
GO

--11/ ModifiedDate >= delivey date
CREATE TRIGGER UTG_DELIVERY_MODIFIED_DATE
ON ORDER_SHIPPING_INFO
FOR UPDATE, INSERT 
AS
BEGIN
	DECLARE @modified_date DATE
	DECLARE @delivery DATE
	DECLARE @orderID char(15)

	SELECT @delivery = DELIVERY_DATE, @orderID = ORDER_ID
	FROM inserted

	select @modified_date = S_MODIFIED_DATE
	from STATUS_INFO

	if(@delivery < @modified_date)
	BEGIN
			 raiserror (N'Lỗi !!! Ngày giao hàng phải cùng ngày hoặc sau ngày đặt hàng.', 16, 0)
			rollback    -- khôi phục lại dữ liệu
	END

END
GO


