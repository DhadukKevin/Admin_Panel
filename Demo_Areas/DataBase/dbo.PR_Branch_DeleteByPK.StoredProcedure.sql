USE [StudentMaster]
GO
/****** Object:  StoredProcedure [dbo].[PR_Branch_DeleteByPK]    Script Date: 28-08-2023 22:17:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Branch_DeleteByPK]
	@BranchID int
AS
BEGIN
	DELETE
	FROM [dbo].[MST_Branch]
	WHERE [dbo].[MST_Branch].[BranchID] = @BranchID
END
GO
