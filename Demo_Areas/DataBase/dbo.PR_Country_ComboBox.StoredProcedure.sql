USE [StudentMaster]
GO
/****** Object:  StoredProcedure [dbo].[PR_Country_ComboBox]    Script Date: 28-08-2023 22:17:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[PR_Country_ComboBox]
AS
SELECT [dbo].[LOC_Country].[CountryID],
		[dbo].[LOC_Country].[CountryName]
From LOC_Country
GO
