USE [StudentMaster]
GO
/****** Object:  StoredProcedure [dbo].[PR_Branch_ComboBox]    Script Date: 28-08-2023 22:17:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PR_Branch_ComboBox]
AS
SELECT [dbo].[MST_Branch].[BranchID],
		[dbo].[MST_Branch].[BranchName]
From MST_Branch
GO
