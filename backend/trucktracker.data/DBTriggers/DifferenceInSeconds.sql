SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Aaron Cheung
-- Create date: 10/24/2022
-- Description:	Gets the difference in time between Move_in and Move_out
-- =============================================
CREATE TRIGGER DifferenceInSeconds
   ON dbo.Truck_History
   AFTER UPDATE
AS 
IF ( UPDATE (Move_Out) )
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    
	UPDATE dbo.Truck_History
	SET Total_Time = DATEDIFF(second, Move_In, Move_Out)
	WHERE HistoryId = (SELECT HistoryId from inserted)

END
GO