USE SISAC

--Reports
DECLARE @DB VARCHAR(100)

IF(DB_NAME() = 'SISAC')
BEGIN
	SET @DB = 'SISAC'
END
ELSE
BEGIN
	SET @DB = 'SISAC_CERT'
END

IF NOT EXISTS ( SELECT	PageName FROM	Security.PageReport WITH (NOLOCK) WHERE	PageName = 'DownloadNonReconciledCostGroup' )
BEGIN
INSERT INTO Security.PageReport (PageName, PathReport, Status)
VALUES ('DownloadNonReconciledCostGroup', '/'+@DB+'/Reports/DownloadNonReconciledCostGroup', 1)
END

IF NOT EXISTS ( SELECT	PageName FROM	Security.PageReport WITH (NOLOCK) WHERE	PageName = 'DownloadNonReconciledInvoiceDetail' )
BEGIN
INSERT INTO Security.PageReport (PageName, PathReport, Status)
VALUES ('DownloadNonReconciledInvoiceDetail', '/'+@DB+'/Reports/DownloadNonReconciledInvoiceDetail', 1)
END

IF NOT EXISTS ( SELECT	PageName FROM	Security.PageReport WITH (NOLOCK) WHERE	PageName = 'NationalJetFuelReconciliationManualLoadLog' )
BEGIN
INSERT INTO Security.PageReport (PageName, PathReport, Status)
VALUES ('NationalJetFuelReconciliationManualLoadLog', '/'+@DB+'/Reports/NationalJetFuelReconciliationManualLoadLog', 1)
END

IF NOT EXISTS ( SELECT	PageName FROM	Security.PageReport WITH (NOLOCK) WHERE	PageName = 'ManifestArrival' )
BEGIN
INSERT INTO Security.PageReport (PageName, PathReport, Status)
VALUES ('ManifestArrival', '/'+@DB+'/Reports/ManifestArrival', 1)
END


--alter manifest arrival

IF not exists
(
	SELECT *
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE COLUMN_NAME = 'UserIDSignature' AND TABLE_NAME = 'ManifestArrival'

)
	ALTER TABLE [Itinerary].[ManifestArrival] ADD [UserIDSignature] [int] NOT NULL

IF not exists
(
	SELECT *
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE COLUMN_NAME = 'LicenceNumberSignature' AND TABLE_NAME = 'ManifestArrival'
	)
	ALTER TABLE [Itinerary].[ManifestArrival] ADD [LicenceNumberSignature] [varchar](20) NOT NULL

IF not exists
(
	SELECT *
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE COLUMN_NAME = 'UserIDAuthorize' AND TABLE_NAME = 'ManifestArrival'
)
	ALTER TABLE [Itinerary].[ManifestArrival] ADD [UserIDAuthorize] [int] NOT NULL

IF not exists
(
	SELECT *
	FROM INFORMATION_SCHEMA.COLUMNS
	WHERE COLUMN_NAME = 'LicenceNumberAuthorize' AND TABLE_NAME = 'ManifestArrival'
)
	ALTER TABLE [Itinerary].[ManifestArrival] ADD [LicenceNumberAuthorize] [varchar](20) NOT NULL



--security manifest arrival

IF NOT EXISTS ( SELECT	[ModuleCode] FROM	[Security].[Module] WITH (NOLOCK) WHERE	ModuleCode = 'MANIFARR' )
BEGIN
INSERT INTO [Security].[Module]
           ([ModuleCode]
           ,[ModuleName]
           ,[MenuCode]
           ,[ControllerName])
     VALUES('MANIFARR','MANIFEST ARRIVAL','ITIN','MANIFESTARRIVAL')
END

IF NOT EXISTS ( SELECT	[ModuleCode] FROM	[Security].[ModulePermission] WITH (NOLOCK) WHERE	ModuleCode = 'MANIFARR' AND PermissionCode = 'PRINTREP')
BEGIN
INSERT INTO [Security].[ModulePermission]
           ([ModuleCode]
           ,[PermissionCode]
           ,[Status])
     VALUES
		   ('MANIFARR','PRINTREP',1)
END

IF NOT EXISTS ( SELECT	[ModuleCode] FROM	[Security].[ModulePermission] WITH (NOLOCK) WHERE	ModuleCode = 'MANIFARR' AND PermissionCode = 'IDX')
BEGIN
INSERT INTO [Security].[ModulePermission]
           ([ModuleCode]
           ,[PermissionCode]
           ,[Status])
     VALUES
           ('MANIFARR','IDX',1)
END

IF NOT EXISTS ( SELECT	[ModuleCode] FROM	[Security].[ModulePermission] WITH (NOLOCK) WHERE	ModuleCode = 'MANIFARR' AND PermissionCode = 'UPD')
BEGIN
INSERT INTO [Security].[ModulePermission]
           ([ModuleCode]
           ,[PermissionCode]
           ,[Status])
     VALUES
		   ('MANIFARR','UPD',1)
END

IF NOT EXISTS ( SELECT	[ModuleCode] FROM	[Security].[ModulePermission] WITH (NOLOCK) WHERE	ModuleCode = 'MANIFARR' AND PermissionCode = 'OPEN')
BEGIN
INSERT INTO [Security].[ModulePermission]
           ([ModuleCode]
           ,[PermissionCode]
           ,[Status])
     VALUES
		   ('MANIFARR','OPEN',1)
END

IF NOT EXISTS ( SELECT	[ModuleCode] FROM	[Security].[ModulePermission] WITH (NOLOCK) WHERE	ModuleCode = 'MANIFARR' AND PermissionCode = 'CLOSE')
BEGIN
INSERT INTO [Security].[ModulePermission]
           ([ModuleCode]
           ,[PermissionCode]
           ,[Status])
     VALUES
		   ('MANIFARR','CLOSE',1)
END


--TIMELINE
GO

--Timeline
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_Timeline]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement] DROP CONSTRAINT [FK_TimelineMovement_Timeline]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_Provider]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement] DROP CONSTRAINT [FK_TimelineMovement_Provider]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_OperationType]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement] DROP CONSTRAINT [FK_TimelineMovement_OperationType]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_MovementType_MovementTypeCode]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement] DROP CONSTRAINT [FK_TimelineMovement_MovementType_MovementTypeCode]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_Timeline_Itinerary]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[Timeline]'))
ALTER TABLE [Itinerary].[Timeline] DROP CONSTRAINT [FK_Timeline_Itinerary]
GO
/****** Object:  Table [Itinerary].[TimelineMovement]    Script Date: 04/11/2016 02:03:46 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]') AND type in (N'U'))
DROP TABLE [Itinerary].[TimelineMovement]
GO
/****** Object:  Table [Itinerary].[Timeline]    Script Date: 04/11/2016 02:03:46 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[Timeline]') AND type in (N'U'))
DROP TABLE [Itinerary].[Timeline]
GO
/****** Object:  Table [Catalog].[MovementType]    Script Date: 04/11/2016 02:03:46 p. m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Catalog].[MovementType]') AND type in (N'U'))
DROP TABLE [Catalog].[MovementType]
GO
/****** Object:  Table [Catalog].[MovementType]    Script Date: 04/11/2016 02:03:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Catalog].[MovementType]') AND type in (N'U'))
BEGIN
CREATE TABLE [Catalog].[MovementType](
	[MovementTypeCode] [varchar](5) NOT NULL,
	[MovementDescription] [varchar](100) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [PK_MovementType] PRIMARY KEY CLUSTERED 
(
	[MovementTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Itinerary].[Timeline]    Script Date: 04/11/2016 02:03:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[Timeline]') AND type in (N'U'))
BEGIN
CREATE TABLE [Itinerary].[Timeline](
	[Sequence] [int] NOT NULL,
	[AirlineCode] [varchar](3) NOT NULL,
	[FlightNumber] [varchar](5) NOT NULL,
	[ItineraryKey] [varchar](8) NOT NULL,
	[PreviousSequence] [int] NULL,
	[PreviousAirlineCode] [varchar](3) NULL,
	[PreviousFlightNumber] [varchar](5) NULL,
	[PreviousItineraryKey] [varchar](8) NULL,
	[NextSequence] [int] NULL,
	[NextAirlineCode] [varchar](3) NULL,
	[NextFlightNumber] [varchar](5) NULL,
	[NextItineraryKey] [varchar](8) NULL,
	[SpecialCase] [bit] NOT NULL CONSTRAINT [DF_Timeline_SpecialCase]  DEFAULT ((0)),
 CONSTRAINT [PK_Timeline] PRIMARY KEY CLUSTERED 
(
	[Sequence] ASC,
	[AirlineCode] ASC,
	[FlightNumber] ASC,
	[ItineraryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Itinerary].[TimelineMovement]    Script Date: 04/11/2016 02:03:46 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]') AND type in (N'U'))
BEGIN
CREATE TABLE [Itinerary].[TimelineMovement](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[AirlineCode] [varchar](3) NOT NULL,
	[FlightNumber] [varchar](5) NOT NULL,
	[ItineraryKey] [varchar](8) NOT NULL,
	[OperationTypeID] [int] NOT NULL,
	[MovementTypeCode] [varchar](5) NOT NULL,
	[MovementDate] [datetime] NOT NULL,
	[Position] [varchar](50) NULL,
	[ProviderNumber] [varchar](10) NULL,
	[RemainingFuel] [decimal](18, 5) NULL,
	[Remarks] [varchar](250) NULL,
 CONSTRAINT [PK_TimelineMovement] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_Timeline_Itinerary]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[Timeline]'))
ALTER TABLE [Itinerary].[Timeline]  WITH CHECK ADD  CONSTRAINT [FK_Timeline_Itinerary] FOREIGN KEY([Sequence], [AirlineCode], [FlightNumber], [ItineraryKey])
REFERENCES [Itinerary].[Itinerary] ([Sequence], [AirlineCode], [FlightNumber], [ItineraryKey])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_Timeline_Itinerary]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[Timeline]'))
ALTER TABLE [Itinerary].[Timeline] CHECK CONSTRAINT [FK_Timeline_Itinerary]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_MovementType_MovementTypeCode]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement]  WITH CHECK ADD  CONSTRAINT [FK_TimelineMovement_MovementType_MovementTypeCode] FOREIGN KEY([MovementTypeCode])
REFERENCES [Catalog].[MovementType] ([MovementTypeCode])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_MovementType_MovementTypeCode]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement] CHECK CONSTRAINT [FK_TimelineMovement_MovementType_MovementTypeCode]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_OperationType]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement]  WITH CHECK ADD  CONSTRAINT [FK_TimelineMovement_OperationType] FOREIGN KEY([OperationTypeID])
REFERENCES [Catalog].[OperationType] ([OperationTypeID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_OperationType]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement] CHECK CONSTRAINT [FK_TimelineMovement_OperationType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_Provider]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement]  WITH CHECK ADD  CONSTRAINT [FK_TimelineMovement_Provider] FOREIGN KEY([ProviderNumber])
REFERENCES [Finance].[Provider] ([ProviderNumber])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_Provider]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement] CHECK CONSTRAINT [FK_TimelineMovement_Provider]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_Timeline]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement]  WITH CHECK ADD  CONSTRAINT [FK_TimelineMovement_Timeline] FOREIGN KEY([Sequence], [AirlineCode], [FlightNumber], [ItineraryKey])
REFERENCES [Itinerary].[Timeline] ([Sequence], [AirlineCode], [FlightNumber], [ItineraryKey])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_TimelineMovement_Timeline]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[TimelineMovement]'))
ALTER TABLE [Itinerary].[TimelineMovement] CHECK CONSTRAINT [FK_TimelineMovement_Timeline]
GO

IF NOT EXISTS (SELECT [MovementTypeCode] FROM [Catalog].[MovementType] WHERE [MovementTypeCode] = 'AR')
BEGIN
  INSERT INTO [Catalog].[MovementType]
  VALUES
  ( 'AR', 'ARRIVAL', 1 ), 
  ( 'DE', 'DEPARTURE', 1 ), 
  ( 'HO', 'HANGAR OUT', 1 ), 
  ( 'HI', 'HANGAR IN', 1 ), 
  ( 'LAN', 'LANDING', 1 ), 
  ( 'PI', 'POSITION IN', 1 ), 
  ( 'PO', 'POSITION OUT', 1 ), 
  ( 'SC', 'SPECIAL CASE', 1 )
END

--VW_Timeline
GO
/****** Object:  View [Itinerary].[VW_ItineraryOrder]    Script Date: 25/10/2016 09:44:37 a.m. ******/
IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[Itinerary].[VW_ItineraryOrder]'))
DROP VIEW [Itinerary].[VW_ItineraryOrder]
GO
/****** Object:  View [Itinerary].[VW_ItineraryOrder]    Script Date: 25/10/2016 09:44:37 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[Itinerary].[VW_ItineraryOrder]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [Itinerary].[VW_ItineraryOrder]
AS
SELECT [Sequence],
          [AirlineCode],
          [FlightNumber],
          [ItineraryKey],
          [EquipmentNumber],
          [DepartureDate],
          [DepartureStation],
          [ArrivalDate],
          [ArrivalStation],
          ROW_NUMBER() OVER (PARTITION BY [AirlineCode], [EquipmentNumber]
                             ORDER BY [AirlineCode], [EquipmentNumber], [DepartureDate]) NUM
   FROM [Itinerary].[Itinerary]' 
GO


--SP timeline
GO
/****** Object:  StoredProcedure [Itinerary].[AutomaticTimeline]    Script Date: 04/11/2016 02:01:08 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[AutomaticTimeline]') AND type in (N'P', N'PC'))
DROP PROCEDURE [Itinerary].[AutomaticTimeline]
GO
/****** Object:  StoredProcedure [Itinerary].[AutomaticTimeline]    Script Date: 04/11/2016 02:01:08 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[AutomaticTimeline]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Leonardo Eduardo Ramirez Garcia Cano	
-- Create date: 21/10/2016
-- Description:	Automatic Timeline
-- =============================================
CREATE PROCEDURE [Itinerary].[AutomaticTimeline]	
	@StartDateParam AS DATETIME = NULL,
	@EndDateParam AS DATETIME = NULL
AS
BEGIN
	
	
	DECLARE @MesesTolerancia INT
	SET @MesesTolerancia = 1

	DECLARE @Today DATETIME
	DECLARE @StartDate DATETIME
	DECLARE @EndDate DATETIME
	DECLARE @Timeline AS TABLE (
		[Sequence] [int] NOT NULL,
		[AirlineCode] [varchar](3) NOT NULL,
		[FlightNumber] [varchar](5) NOT NULL,
		[ItineraryKey] [varchar](8) NOT NULL,
		[PreviousSequence] [int] NULL,
		[PreviousAirlineCode] [varchar](3) NULL,
		[PreviousFlightNumber] [varchar](5) NULL,
		[PreviousItineraryKey] [varchar](8) NULL,
		[NextSequence] [int] NULL,
		[NextAirlineCode] [varchar](3) NULL,
		[NextFlightNumber] [varchar](5) NULL,
		[NextItineraryKey] [varchar](8) NULL)

	SET @Today = CONVERT(VARCHAR(8), GETDATE(), 112 )


	IF((@StartDateParam IS NULL OR @StartDateParam = '''') OR (@EndDateParam IS NULL OR @EndDateParam = ''''))
	BEGIN
		SET @StartDate = DATEADD(MONTH, -(@MesesTolerancia), @Today)
		SET @EndDate = DATEADD(MONTH, @MesesTolerancia, @Today) + '' 23:59:59''
	END
	ELSE
	BEGIN
		SET @StartDate = @StartDateParam
		SET @EndDate = @EndDateParam
	END


	--Guardar Copia de Timeline a Trabajar
	INSERT @Timeline  
		  ([Sequence]
		  ,[AirlineCode]
		  ,[FlightNumber]
		  ,[ItineraryKey]
		  ,[PreviousSequence]
		  ,[PreviousAirlineCode]
		  ,[PreviousFlightNumber]
		  ,[PreviousItineraryKey]
		  ,[NextSequence]
		  ,[NextAirlineCode]
		  ,[NextFlightNumber]
		  ,[NextItineraryKey])
	SELECT A.[Sequence]
		  ,A.[AirlineCode]
		  ,A.[FlightNumber]
		  ,A.[ItineraryKey]
		  ,C.Sequence [PreviousSequence]
		  ,C.AirlineCode [PreviousAirlineCode]
		  ,C.FlightNumber [PreviousFlightNumber]
		  ,C.ItineraryKey [PreviousItineraryKey]
		  ,B.Sequence [NextSequence]
		  ,B.AirlineCode [NextAirlineCode]
		  ,B.FlightNumber [NextFlightNumber]
		  ,B.ItineraryKey [NextItineraryKey]
	  FROM [Itinerary].[VW_ItineraryOrder] AS A														LEFT JOIN
		   [Itinerary].[VW_ItineraryOrder] AS B ON (A.NUM = B.NUM-1
												AND A.EquipmentNumber = B.EquipmentNumber
												AND A.AirlineCode = B.AirlineCode
												AND A.ArrivalDate <= B.DepartureDate
												AND A.ArrivalStation = B.DepartureStation)			LEFT JOIN
		   [Itinerary].[VW_ItineraryOrder] AS C ON (A.NUM-1 = C.NUM
												AND A.EquipmentNumber = C.EquipmentNumber
												AND A.AirlineCode = C.AirlineCode
												AND A.DepartureDate >= C.ArrivalDate
												AND A.DepartureStation = C.ArrivalStation)
	 WHERE A.DepartureDate BETWEEN @StartDate and @EndDate
	 ORDER BY A.[AirlineCode],
			  A.[EquipmentNumber],
			  A.[DepartureDate]

	--SELECT * FROM @Timeline

	--UPDATE CON TIMELINE ACTUAL
	UPDATE [Itinerary].[Timeline]
	   SET PreviousSequence = B.PreviousSequence,
		   PreviousAirlineCode = B.PreviousAirlineCode,
		   PreviousFlightNumber = B.PreviousFlightNumber,
		   PreviousItineraryKey = B.PreviousItineraryKey,
		   NextSequence = B.NextSequence,
		   NextAirlineCode = B.NextAirlineCode,
		   NextFlightNumber = B.NextFlightNumber,
		   NextItineraryKey = B.NextItineraryKey
	  FROM [Itinerary].[Timeline] A INNER JOIN
		   @Timeline B ON (A.Sequence = B.Sequence 
					   AND A.AirlineCode = B.AirlineCode
					   AND A.FlightNumber = B.FlightNumber
					   AND A.ItineraryKey = B.ItineraryKey)
	WHERE A.SpecialCase = 0


	--SELECT *
	--  FROM [Itinerary].[Timeline] A INNER JOIN
	--       @Timeline B ON (A.Sequence = B.Sequence 
	--	               AND A.AirlineCode = B.AirlineCode
	--				   AND A.FlightNumber = B.FlightNumber
	--				   AND A.ItineraryKey = B.ItineraryKey)

	--INSERT DE REGISTROS NUEVOS EN TIMELINE
	INSERT INTO [Itinerary].[Timeline]
		  ([Sequence]
		  ,[AirlineCode]
		  ,[FlightNumber]
		  ,[ItineraryKey]
		  ,[PreviousSequence]
		  ,[PreviousAirlineCode]
		  ,[PreviousFlightNumber]
		  ,[PreviousItineraryKey]
		  ,[NextSequence]
		  ,[NextAirlineCode]
		  ,[NextFlightNumber]
		  ,[NextItineraryKey]) 
	SELECT [Sequence]
		  ,[AirlineCode]
		  ,[FlightNumber]
		  ,[ItineraryKey]
		  ,[PreviousSequence]
		  ,[PreviousAirlineCode]
		  ,[PreviousFlightNumber]
		  ,[PreviousItineraryKey]
		  ,[NextSequence]
		  ,[NextAirlineCode]
		  ,[NextFlightNumber]
		  ,[NextItineraryKey] 
	  FROM @Timeline
	 WHERE (CAST(Sequence AS VARCHAR)+AirlineCode+FlightNumber+ItineraryKey) NOT IN (SELECT CAST(Sequence AS VARCHAR)+AirlineCode+FlightNumber+ItineraryKey
																					   FROM [Itinerary].[Timeline]) 

	--SELECT [Sequence]
	--      ,[AirlineCode]
	--      ,[FlightNumber]
	--      ,[ItineraryKey]
	--      ,[PreviousSequence]
	--      ,[PreviousAirlineCode]
	--      ,[PreviousFlightNumber]
	--      ,[PreviousItineraryKey]
	--      ,[NextSequence]
	--      ,[NextAirlineCode]
	--      ,[NextFlightNumber]
	--      ,[NextItineraryKey] 
	--  FROM @Timeline
	-- WHERE (CAST(Sequence AS VARCHAR)+AirlineCode+FlightNumber+ItineraryKey) NOT IN (SELECT CAST(Sequence AS VARCHAR)+AirlineCode+FlightNumber+ItineraryKey
	--																				   FROM [Itinerary].[Timeline]) 



END
' 
END
GO


