USE [StudentMaster]
GO
/****** Object:  StoredProcedure [dbo].[PR_Country_ComboBoxbyCountryId]    Script Date: 28-08-2023 22:17:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[PR_Country_ComboBoxbyCountryId]
@COUNTRYID INT
AS
SELECT CITYID, CITYNAME FROM CITY
WHERE CITY. COUNTRYID = @COUNTRYID
GO
