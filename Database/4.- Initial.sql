USE [SISAC]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_AdditionalDepartureInformation_ManifestDeparture]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[AdditionalDepartureInformation]'))
ALTER TABLE [Itinerary].[AdditionalDepartureInformation] DROP CONSTRAINT [FK_AdditionalDepartureInformation_ManifestDeparture]
GO
/****** Object:  Table [Itinerary].[AdditionalDepartureInformation]    Script Date: 09/11/2016 03:19:46 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[AdditionalDepartureInformation]') AND type in (N'U'))
DROP TABLE [Itinerary].[AdditionalDepartureInformation]
GO
/****** Object:  Table [Itinerary].[AdditionalDepartureInformation]    Script Date: 09/11/2016 03:19:46 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[AdditionalDepartureInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [Itinerary].[AdditionalDepartureInformation](
	[Sequence] [int] NOT NULL,
	[AirlineCode] [varchar](3) NOT NULL,
	[FlightNumber] [varchar](5) NOT NULL,
	[ItineraryKey] [varchar](8) NOT NULL,
	[Pilot] [int] NOT NULL,
	[Surcharge] [int] NOT NULL,
	[ExtraCrew] [int] NOT NULL,
	[TypeFlight] [varchar](10) NULL,
	[SlotAllocatedTime] [time](7) NULL,
	[SlotCoordinatedTime] [time](7) NULL,
	[OvernightEndTime] [time](7) NULL,
	[ManeuverStartTime] [time](7) NULL,
	[PositionOutputTime] [time](7) NULL,
	[DelayDescription1] [varchar](250) NULL,
	[DelayDescription2] [varchar](250) NULL,
	[DelayDescription3] [varchar](250) NULL,
 CONSTRAINT [PK_AdditionalDepartureInformation] PRIMARY KEY CLUSTERED 
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
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_AdditionalDepartureInformation_ManifestDeparture]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[AdditionalDepartureInformation]'))
ALTER TABLE [Itinerary].[AdditionalDepartureInformation]  WITH CHECK ADD  CONSTRAINT [FK_AdditionalDepartureInformation_ManifestDeparture] FOREIGN KEY([Sequence], [AirlineCode], [FlightNumber], [ItineraryKey])
REFERENCES [Itinerary].[ManifestDeparture] ([Sequence], [AirlineCode], [FlightNumber], [ItineraryKey])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_AdditionalDepartureInformation_ManifestDeparture]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[AdditionalDepartureInformation]'))
ALTER TABLE [Itinerary].[AdditionalDepartureInformation] CHECK CONSTRAINT [FK_AdditionalDepartureInformation_ManifestDeparture]
GO


GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_AdditionalArrivalInformation_ManifestArrival]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[AdditionalArrivalInformation]'))
ALTER TABLE [Itinerary].[AdditionalArrivalInformation] DROP CONSTRAINT [FK_AdditionalArrivalInformation_ManifestArrival]
GO
/****** Object:  Table [Itinerary].[AdditionalArrivalInformation]    Script Date: 10/11/2016 03:22:42 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[AdditionalArrivalInformation]') AND type in (N'U'))
DROP TABLE [Itinerary].[AdditionalArrivalInformation]
GO
/****** Object:  Table [Itinerary].[AdditionalArrivalInformation]    Script Date: 10/11/2016 03:22:42 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[AdditionalArrivalInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [Itinerary].[AdditionalArrivalInformation](
	[Sequence] [int] NOT NULL,
	[AirlineCode] [varchar](3) NOT NULL,
	[FlightNumber] [varchar](5) NOT NULL,
	[ItineraryKey] [varchar](8) NOT NULL,
	[Pilot] [int] NOT NULL,
	[Surcharge] [int] NOT NULL,
	[ExtraCrew] [int] NOT NULL,
	[TypeFlight] [varchar](10) NULL,
	[SlotAllocatedTime] [time](7) NULL,
	[SlotCoordinatedTime] [time](7) NULL,
	[OvernightEndTime] [time](7) NULL,
	[ManeuverStartTime] [time](7) NULL,
	[PositionOutputTime] [time](7) NULL,
	[DelayDescription1] [varchar](250) NULL,
	[DelayDescription2] [varchar](250) NULL,
	[DelayDescription3] [varchar](250) NULL,
 CONSTRAINT [PK_AdditionalArrivalInformation] PRIMARY KEY CLUSTERED 
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
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_AdditionalArrivalInformation_ManifestArrival]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[AdditionalArrivalInformation]'))
ALTER TABLE [Itinerary].[AdditionalArrivalInformation]  WITH CHECK ADD  CONSTRAINT [FK_AdditionalArrivalInformation_ManifestArrival] FOREIGN KEY([Sequence], [AirlineCode], [FlightNumber], [ItineraryKey])
REFERENCES [Itinerary].[ManifestArrival] ([Sequence], [AirlineCode], [FlightNumber], [ItineraryKey])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_AdditionalArrivalInformation_ManifestArrival]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[AdditionalArrivalInformation]'))
ALTER TABLE [Itinerary].[AdditionalArrivalInformation] CHECK CONSTRAINT [FK_AdditionalArrivalInformation_ManifestArrival]
GO


GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingInformation_ManifestDepartureBoarding_BoardingID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingInformation] DROP CONSTRAINT [FK_ManifestDepartureBoardingInformation_ManifestDepartureBoarding_BoardingID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingInformation_CompartmentTypeInformation_CompartmentTypeInformationID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingInformation] DROP CONSTRAINT [FK_ManifestDepartureBoardingInformation_CompartmentTypeInformation_CompartmentTypeInformationID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingInformation_CompartmentTypeConfig_CompartmentTypeID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingInformation] DROP CONSTRAINT [FK_ManifestDepartureBoardingInformation_CompartmentTypeConfig_CompartmentTypeID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingDetail_ManifestDepartureBoarding_BoardingID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingDetail]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingDetail] DROP CONSTRAINT [FK_ManifestDepartureBoardingDetail_ManifestDepartureBoarding_BoardingID]
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingDetail_CompartmentTypeConfig_CompartmentTypeID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingDetail]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingDetail] DROP CONSTRAINT [FK_ManifestDepartureBoardingDetail_CompartmentTypeConfig_CompartmentTypeID]
GO
/****** Object:  Table [Itinerary].[ManifestDepartureBoardingInformation]    Script Date: 11/11/2016 12:47:37 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]') AND type in (N'U'))
DROP TABLE [Itinerary].[ManifestDepartureBoardingInformation]
GO
/****** Object:  Table [Itinerary].[ManifestDepartureBoardingDetail]    Script Date: 11/11/2016 12:47:37 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingDetail]') AND type in (N'U'))
DROP TABLE [Itinerary].[ManifestDepartureBoardingDetail]
GO
/****** Object:  Table [Itinerary].[ManifestDepartureBoardingDetail]    Script Date: 11/11/2016 12:47:37 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingDetail]') AND type in (N'U'))
BEGIN
CREATE TABLE [Itinerary].[ManifestDepartureBoardingDetail](
	[BoardingDetailID] [bigint] IDENTITY(1,1) NOT NULL,
	[BoardingID] [bigint] NOT NULL,
	[CompartmentTypeID] [int] NOT NULL,
	[LuggageQuantity] [int] NULL,
	[LuggageKgs] [numeric](18, 5) NULL,
	[ChargeQuantity] [int] NULL,
	[ChargeKgs] [numeric](18, 5) NULL,
	[Remarks] [varchar](300) NULL,
	[RampResponsible] [varchar](200) NULL,
	[AorUserID] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ManifestDepartureBoardingDetail] PRIMARY KEY CLUSTERED 
(
	[BoardingDetailID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [Itinerary].[ManifestDepartureBoardingInformation]    Script Date: 11/11/2016 12:47:37 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [Itinerary].[ManifestDepartureBoardingInformation](
	[BoardingInformationID] [bigint] IDENTITY(1,1) NOT NULL,
	[BoardingID] [bigint] NOT NULL,
	[CompartmentTypeInformationID] [int] NOT NULL,
	[CompartmentTypeID] [int] NOT NULL,
	[Checked] [bit] NOT NULL,
 CONSTRAINT [PK_ManifestDepartureBoardingInformation_1] PRIMARY KEY CLUSTERED 
(
	[BoardingInformationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingDetail_CompartmentTypeConfig_CompartmentTypeID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingDetail]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingDetail]  WITH CHECK ADD  CONSTRAINT [FK_ManifestDepartureBoardingDetail_CompartmentTypeConfig_CompartmentTypeID] FOREIGN KEY([CompartmentTypeID])
REFERENCES [Airport].[CompartmentTypeConfig] ([CompartmentTypeID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingDetail_CompartmentTypeConfig_CompartmentTypeID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingDetail]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingDetail] CHECK CONSTRAINT [FK_ManifestDepartureBoardingDetail_CompartmentTypeConfig_CompartmentTypeID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingDetail_ManifestDepartureBoarding_BoardingID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingDetail]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingDetail]  WITH CHECK ADD  CONSTRAINT [FK_ManifestDepartureBoardingDetail_ManifestDepartureBoarding_BoardingID] FOREIGN KEY([BoardingID])
REFERENCES [Itinerary].[ManifestDepartureBoarding] ([BoardingID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingDetail_ManifestDepartureBoarding_BoardingID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingDetail]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingDetail] CHECK CONSTRAINT [FK_ManifestDepartureBoardingDetail_ManifestDepartureBoarding_BoardingID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingInformation_CompartmentTypeConfig_CompartmentTypeID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingInformation]  WITH CHECK ADD  CONSTRAINT [FK_ManifestDepartureBoardingInformation_CompartmentTypeConfig_CompartmentTypeID] FOREIGN KEY([CompartmentTypeID])
REFERENCES [Airport].[CompartmentTypeConfig] ([CompartmentTypeID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingInformation_CompartmentTypeConfig_CompartmentTypeID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingInformation] CHECK CONSTRAINT [FK_ManifestDepartureBoardingInformation_CompartmentTypeConfig_CompartmentTypeID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingInformation_CompartmentTypeInformation_CompartmentTypeInformationID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingInformation]  WITH CHECK ADD  CONSTRAINT [FK_ManifestDepartureBoardingInformation_CompartmentTypeInformation_CompartmentTypeInformationID] FOREIGN KEY([CompartmentTypeInformationID])
REFERENCES [Airport].[CompartmentTypeInformation] ([CompartmentTypeInformationID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingInformation_CompartmentTypeInformation_CompartmentTypeInformationID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingInformation] CHECK CONSTRAINT [FK_ManifestDepartureBoardingInformation_CompartmentTypeInformation_CompartmentTypeInformationID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingInformation_ManifestDepartureBoarding_BoardingID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingInformation]  WITH CHECK ADD  CONSTRAINT [FK_ManifestDepartureBoardingInformation_ManifestDepartureBoarding_BoardingID] FOREIGN KEY([BoardingID])
REFERENCES [Itinerary].[ManifestDepartureBoarding] ([BoardingID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Itinerary].[FK_ManifestDepartureBoardingInformation_ManifestDepartureBoarding_BoardingID]') AND parent_object_id = OBJECT_ID(N'[Itinerary].[ManifestDepartureBoardingInformation]'))
ALTER TABLE [Itinerary].[ManifestDepartureBoardingInformation] CHECK CONSTRAINT [FK_ManifestDepartureBoardingInformation_ManifestDepartureBoarding_BoardingID]
GO



GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Airport].[FK_AdditionalPassengerInformation_PassengerInformation]') AND parent_object_id = OBJECT_ID(N'[Airport].[AdditionalPassengerInformation]'))
ALTER TABLE [Airport].[AdditionalPassengerInformation] DROP CONSTRAINT [FK_AdditionalPassengerInformation_PassengerInformation]
GO
/****** Object:  Table [Airport].[AdditionalPassengerInformation]    Script Date: 19/12/2016 12:05:02 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Airport].[AdditionalPassengerInformation]') AND type in (N'U'))
DROP TABLE [Airport].[AdditionalPassengerInformation]
GO
/****** Object:  Table [Airport].[AdditionalPassengerInformation]    Script Date: 19/12/2016 12:05:02 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Airport].[AdditionalPassengerInformation]') AND type in (N'U'))
BEGIN
CREATE TABLE [Airport].[AdditionalPassengerInformation](
	[Sequence] [int] NOT NULL,
	[AirlineCode] [varchar](3) NOT NULL,
	[FlightNumber] [varchar](5) NOT NULL,
	[ItineraryKey] [varchar](8) NOT NULL,
	[AdultNational] [int] NOT NULL,
	[AdultInternational] [int] NOT NULL,
	[MinorNational] [int] NOT NULL,
	[MinorInternational] [int] NOT NULL,
	[DiplomaticNational] [int] NOT NULL,
	[DiplomaticInternational] [int] NOT NULL,
	[CommissionNational] [int] NOT NULL,
	[CommissionInternational] [int] NOT NULL,
	[InfantNational] [int] NOT NULL,
	[InfantInternational] [int] NOT NULL,
	[TransitoryNational] [int] NOT NULL,
	[TransitoryInternational] [int] NOT NULL,
	[ConnectionNational] [int] NOT NULL,
	[ConnectionInternational] [int] NOT NULL,
	[OtherNational] [int] NOT NULL,
	[OtherInternational] [int] NOT NULL,
	[PaxDni] [int] NOT NULL,
 CONSTRAINT [PK_AdditionalPassengerInformation] PRIMARY KEY CLUSTERED 
(
	[Sequence] ASC,
	[AirlineCode] ASC,
	[FlightNumber] ASC,
	[ItineraryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Airport].[FK_AdditionalPassengerInformation_PassengerInformation]') AND parent_object_id = OBJECT_ID(N'[Airport].[AdditionalPassengerInformation]'))
ALTER TABLE [Airport].[AdditionalPassengerInformation]  WITH CHECK ADD  CONSTRAINT [FK_AdditionalPassengerInformation_PassengerInformation] FOREIGN KEY([Sequence], [AirlineCode], [FlightNumber], [ItineraryKey])
REFERENCES [Airport].[PassengerInformation] ([Sequence], [AirlineCode], [FlightNumber], [ItineraryKey])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[Airport].[FK_AdditionalPassengerInformation_PassengerInformation]') AND parent_object_id = OBJECT_ID(N'[Airport].[AdditionalPassengerInformation]'))
ALTER TABLE [Airport].[AdditionalPassengerInformation] CHECK CONSTRAINT [FK_AdditionalPassengerInformation_PassengerInformation]
GO



GO
/****** Object:  Table [Process].[NationalJetFuelNonconformityLoadLog]    Script Date: 19/12/2016 01:05:10 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[NationalJetFuelNonconformityLoadLog]') AND type in (N'U'))
DROP TABLE [Process].[NationalJetFuelNonconformityLoadLog]
GO
/****** Object:  Table [Process].[NationalJetFuelNonconformityLoadLog]    Script Date: 19/12/2016 01:05:10 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[NationalJetFuelNonconformityLoadLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [Process].[NationalJetFuelNonconformityLoadLog](
	[LogID] [bigint] IDENTITY(1,1) NOT NULL,
	[RemittanceID] [varchar](8) NOT NULL,
	[MonthYear] [varchar](4) NOT NULL,
	[Period] [varchar](5) NOT NULL,
	[ID] [bigint] NOT NULL,
	[AirlineCode] [varchar](3) NULL,
	[ProviderNumber] [varchar](50) NULL,
	[ServiceCode] [varchar](50) NULL,
	[FederalTaxCode] [varchar](50) NULL,
	[StationCode] [varchar](50) NULL,
	[InvoiceNumber] [varchar](255) NULL,
	[CustomerNumber] [varchar](50) NULL,
	[CustomerName] [varchar](255) NULL,
	[InvoiceDate] [datetime] NULL,
	[ElectronicInvoiceNumber] [varchar](50) NULL,
	[ElectronicInvoiceDate] [datetime] NULL,
	[ProductNumber] [varchar](50) NULL,
	[ProductDescription] [varchar](255) NULL,
	[TicketNumber] [varchar](255) NULL,
	[OperationType] [varchar](255) NULL,
	[FlightNumber] [varchar](100) NULL,
	[EquipmentNumber] [varchar](100) NULL,
	[AirplaneModel] [varchar](100) NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[StartMeter] [decimal](18, 2) NULL,
	[EndMeter] [decimal](18, 2) NULL,
	[VolumeM3] [decimal](18, 3) NULL,
	[RateType] [varchar](255) NULL,
	[JetFuelAmount] [decimal](18, 2) NULL,
	[FreightAmount] [decimal](18, 2) NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[FuelingAmount] [decimal](18, 2) NULL,
	[SubtotalAmount] [decimal](18, 2) NULL,
	[TaxAmount] [decimal](18, 2) NULL,
	[TotalAmount] [decimal](18, 2) NULL,
	[Status] [varchar](20) NULL,
	[ReconciliationStatus] [varchar](10) NULL,
	[InvoiceCostVariance] [numeric](18, 5) NULL,
	[ErrorDescription] [varchar](5000) NULL,
 CONSTRAINT [PK_NationalJetFuelNonconformityLoadLog] PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO



GO
/****** Object:  StoredProcedure [Process].[DownloadCanditateInvoiceRecords]    Script Date: 06/12/2016 05:07:17 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[DownloadCanditateInvoiceRecords]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- ====================================================================
-- Author:		Leonardo Eduardo Ramirez
-- Create date: 01 Diciembre 2016
-- Description:	Reporte Candidatos a Inconformidades
-- ====================================================================
-- [Process].[DownloadCanditateInvoiceRecords] ''16-2830'', ''0416'', ''01-06''
CREATE PROCEDURE [Process].[DownloadCanditateInvoiceRecords]
    @RemittanceID AS VARCHAR(8)
   ,@MonthYear AS VARCHAR(4)
   ,@Period AS VARCHAR(5) 
AS
    BEGIN

        SET NOCOUNT ON;		

		SELECT A.[ID]
			  ,A.[AirlineCode]
			  ,[ProviderNumber]
			  ,[ServiceCode]
			  ,[FederalTaxCode]
			  ,[StationCode]
			  ,A.[InvoiceNumber]
			  ,[CustomerNumber]
			  ,[CustomerName]
			  ,A.[InvoiceDate]
			  ,A.[ElectronicInvoiceNumber]
			  ,A.[ElectronicInvoiceDate]
			  ,[ProductNumber]
			  ,[ProductDescription]
			  ,[TicketNumber]
			  ,[OperationType]
			  ,[FlightNumber]
			  ,[EquipmentNumber]
			  ,[AirplaneModel]
			  ,[StartDateTime]
			  ,[EndDateTime]
			  ,[StartMeter]
			  ,[EndMeter]
			  ,[VolumeM3]
			  ,[RateType]
			  ,[JetFuelAmount]
			  ,[FreightAmount]
			  ,[DiscountAmount]
			  ,[FuelingAmount]
			  ,[SubtotalAmount]
			  ,[TaxAmount]
			  ,[TotalAmount]
			  ,[Status]
			  ,A.[ReconciliationStatus]
			  ,B.InvoiceCostVariance
		  FROM [Process].[NationalJetFuelInvoiceDetail] A LEFT JOIN
			   [Process].[NationalJetFuelReconciliationDetail] B ON (A.ID = B.InvoiceDetailID)
		 WHERE A.RemittanceID = @RemittanceID AND A.MonthYear = @MonthYear AND A.Period = @Period
		   AND ((A.[ReconciliationStatus] = ''DIFF'' AND B.InvoiceCostVariance > 0)
			OR (A.[ReconciliationStatus] = NULL OR A.ReconciliationStatus = ''PENDING''))

	
    END;

' 
END
GO
  
IF not exists
(
SELECT *
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'NonconformityStatusCode' AND TABLE_NAME = 'NationalJetFuelInvoiceControl'
)
ALTER TABLE [Process].[NationalJetFuelInvoiceControl] ADD 
	[CountNonconformityRecords] [int] NULL,
	[NonconformitySubtotalAmount] [decimal](18, 5) NULL,
	[DateNonconformity] [datetime] NULL,
	[NonconformityReference] [varchar](15) NULL,
	[NonconformityStatusCode] [varchar](6) NULL CONSTRAINT [DF_NationalJetFuelInvoiceControl_NonconfirmityStatusCode]  DEFAULT ('OPEN')

IF not exists
(
SELECT *
  FROM INFORMATION_SCHEMA.COLUMNS
 WHERE COLUMN_NAME = 'NonconformityFlag' AND TABLE_NAME = 'NationalJetFuelInvoiceDetail'
)
ALTER TABLE [Process].[NationalJetFuelInvoiceDetail] ADD [NonconformityFlag] [bit] NULL



IF NOT EXISTS (SELECT * FROM [Security].[Permission] WHERE PermissionCode = 'NONCONFOR')
BEGIN

INSERT INTO [Security].[Permission] (PermissionCode, PermissionName)
VALUES ('NONCONFOR', 'NONCONFORMITY')

INSERT INTO Security.ModulePermission (ModuleCode, PermissionCode, Status)
VALUES ('NTLJETREC', 'NONCONFOR', 1)

END

GO

IF NOT EXISTS (SELECT * FROM Security.Module WHERE ModuleCode = 'TIMELINE')
BEGIN
	INSERT INTO Security.Module (ModuleCode, ModuleName, MenuCode, ControllerName)
	VALUES ('TIMELINE', 'TIMELINE', 'ITIN', 'ITINERARIES/TIMELINE');
END;

IF NOT EXISTS (SELECT * FROM Security.ModulePermission WHERE ModuleCode = 'TIMELINE' AND PermissionCode = 'ADD')
BEGIN
	INSERT INTO Security.ModulePermission (ModuleCode, PermissionCode, Status)
	VALUES ('TIMELINE', 'ADD', 1);
END;

IF NOT EXISTS (SELECT * FROM Security.ModulePermission WHERE ModuleCode = 'TIMELINE' AND PermissionCode = 'UPD')
BEGIN
	INSERT INTO Security.ModulePermission (ModuleCode, PermissionCode, Status)
	VALUES ('TIMELINE', 'UPD', 1);
END;

IF NOT EXISTS (SELECT * FROM Security.ModulePermission WHERE ModuleCode = 'TIMELINE' AND PermissionCode = 'DEL')
BEGIN
	INSERT INTO Security.ModulePermission (ModuleCode, PermissionCode, Status)
	VALUES ('TIMELINE', 'DEL', 1);
END;

IF NOT EXISTS (SELECT * FROM Security.ModulePermission WHERE ModuleCode = 'TIMELINE' AND PermissionCode = 'IDX')
BEGIN
	INSERT INTO Security.ModulePermission (ModuleCode, PermissionCode, Status)
	VALUES ('TIMELINE', 'IDX', 1);
END;



GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[UploadNonconformity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [Process].[UploadNonconformity]

/****** Object:  UserDefinedTableType [Process].[NationalJetFuelInvoiceDetailType]    Script Date: 05/10/2016 11:14:03 a.m. ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'NationalJetFuelNonconformityType' AND ss.name = N'Process')
DROP TYPE [Process].[NationalJetFuelNonconformityType]
GO
/****** Object:  UserDefinedTableType [Process].[NationalJetFuelInvoiceDetailType]    Script Date: 05/10/2016 11:14:03 a.m. ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'NationalJetFuelNonconformityType' AND ss.name = N'Process')
CREATE TYPE [Process].[NationalJetFuelNonconformityType] AS TABLE(
    [RemittanceID] [VARCHAR](8) NOT NULL,
	[MonthYear] [VARCHAR](4) NOT NULL,
	[Period] [VARCHAR](5) NOT NULL,
	[Username] [NVARCHAR](50) NOT NULL,
	[ID] [BIGINT] NOT NULL,
	[AirlineCode] [VARCHAR](3) NULL,
	[ProviderNumber] [VARCHAR](50) NULL,
	[ServiceCode] [VARCHAR](50) NULL,
	[FederalTaxCode] [VARCHAR](50) NULL,
	[StationCode] [VARCHAR](50) NULL,
	[InvoiceNumber] [VARCHAR](255) NULL,
	[CustomerNumber] [VARCHAR](50) NULL,
	[CustomerName] [VARCHAR](255) NULL,
	[InvoiceDate] [DATETIME] NULL,
	[ElectronicInvoiceNumber] [VARCHAR](50) NULL,
	[ElectronicInvoiceDate] [DATETIME] NULL,
	[ProductNumber] [VARCHAR](50) NULL,
	[ProductDescription] [VARCHAR](255) NULL,
	[TicketNumber] [VARCHAR](255) NULL,
	[OperationType] [VARCHAR](255) NULL,
	[FlightNumber] [VARCHAR](100) NULL,
	[EquipmentNumber] [VARCHAR](100) NULL,
	[AirplaneModel] [VARCHAR](100) NULL,
	[StartDateTime] [DATETIME] NULL,
	[EndDateTime] [DATETIME] NULL,
	[StartMeter] [DECIMAL](18, 2) NULL,
	[EndMeter] [DECIMAL](18, 2) NULL,
	[VolumeM3] [DECIMAL](18, 3) NULL,
	[RateType] [VARCHAR](255) NULL,
	[JetFuelAmount] [DECIMAL](18, 2) NULL,
	[FreightAmount] [DECIMAL](18, 2) NULL,
	[DiscountAmount] [DECIMAL](18, 2) NULL,
	[FuelingAmount] [DECIMAL](18, 2) NULL,
	[SubtotalAmount] [DECIMAL](18, 2) NULL,
	[TaxAmount] [DECIMAL](18, 2) NULL,
	[TotalAmount] [DECIMAL](18, 2) NULL,
	[Status] [VARCHAR](20) NULL,
	[ReconciliationStatus] [VARCHAR](10) NULL,
	[InvoiceCostVariance] [NUMERIC](18, 5) NULL
)
GO




GO

IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[Itinerary].[VW_TimelineOrder]'))
DROP VIEW [Itinerary].[VW_TimelineOrder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[Itinerary].[VW_TimelineOrder]'))
EXEC dbo.sp_executesql @statement = N'CREATE VIEW [Itinerary].[VW_TimelineOrder]
AS
SELECT        ROW_NUMBER() OVER (PARTITION BY Itinerary.[AirlineCode], Itinerary.[EquipmentNumber] ORDER BY Itinerary.[AirlineCode], Itinerary.[EquipmentNumber], Itinerary.[DepartureDate]) AS Row,
 Itinerary.Itinerary.Sequence, Itinerary.Itinerary.AirlineCode, Itinerary.Itinerary.FlightNumber, Itinerary.Itinerary.ItineraryKey, Itinerary.Itinerary.EquipmentNumber, 
                         Itinerary.Itinerary.DepartureDate, Itinerary.Itinerary.DepartureStation, Itinerary.Itinerary.ArrivalDate, Itinerary.Itinerary.ArrivalStation, Itinerary.Timeline.PreviousSequence, Itinerary.Timeline.PreviousAirlineCode, 
                         Itinerary.Timeline.PreviousFlightNumber, Itinerary.Timeline.PreviousItineraryKey, Itinerary.Timeline.NextSequence, Itinerary.Timeline.NextAirlineCode, Itinerary.Timeline.NextFlightNumber, 
                         Itinerary.Timeline.NextItineraryKey, Itinerary.Timeline.SpecialCase
FROM            Itinerary.Timeline INNER JOIN
                         Itinerary.Itinerary ON Itinerary.Timeline.Sequence = Itinerary.Itinerary.Sequence AND Itinerary.Timeline.AirlineCode = Itinerary.Itinerary.AirlineCode AND 
                         Itinerary.Timeline.FlightNumber = Itinerary.Itinerary.FlightNumber AND Itinerary.Timeline.ItineraryKey = Itinerary.Itinerary.ItineraryKey
' 
GO



GO
/****** Object:  StoredProcedure [Process].[JetFuelNonconformityRevert]    Script Date: 20/12/2016 03:59:57 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[JetFuelNonconformityRevert]') AND type in (N'P', N'PC'))
DROP PROCEDURE [Process].[JetFuelNonconformityRevert]
GO
/****** Object:  StoredProcedure [Process].[JetFuelNonconformityRevert]    Script Date: 20/12/2016 03:59:57 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[JetFuelNonconformityRevert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =======================================================
-- Author: Leonardo Eduardo Ramirez		
-- Create date: 06/12/2016
-- Description: Revertir Nonconformity
-- =======================================================
-- [Process].[JetFuelNonconformityRevert] ''16-2831'', ''0416'', ''01-06''
CREATE PROCEDURE [Process].[JetFuelNonconformityRevert]
	@RemittanceID AS VARCHAR(8),
	@MonthYear VARCHAR(4),
	@Period VARCHAR(5)
AS
BEGIN


	IF EXISTS (SELECT RemittanceID FROM	[Process].[NationalJetFuelInvoiceControl] WITH (NOLOCK) 
	            WHERE RemittanceID = @RemittanceID
				  AND MonthYear = @MonthYear
				  AND Period = @Period
				  AND NonconformityStatusCode = ''CLOSED'')
		BEGIN
			SELECT 1 [Verify] --No es vible debido a que la Remesa se encuentra cerrrada para conciliación
			--Remittance is closed
		END
		ELSE
		BEGIN
			--IF EXISTS (SELECT RemittanceID FROM	[Process].[NationalJetFuelInvoiceDetail] WITH (NOLOCK) 
			--			WHERE RemittanceID = @RemittanceID
			--			AND MonthYear = @MonthYear
			--			AND Period = @Period 
			--			AND [Status] = ''ERRO'')
			--BEGIN
			--	SELECT 2 [Verify] --No es vible debido a que ya existen registros en [Process].[NationalJetFuelInvoiceDetail] de esta remesa hay al menos un registro con error que no es de tipo MNV (Matricula no valida)
			--	--Different errors to invalid Equipment Number
			--END
			--ELSE 
			BEGIN		
				
	
				--step 0.1 Borrar de Log de la remesa seleccionada
				DELETE FROM [Process].[NationalJetFuelNonconformityLoadLog]
				 WHERE RemittanceID = @RemittanceID
				   AND MonthYear = @MonthYear
				   AND Period = @Period
	
				--step 1 Nullear [ReconciliationStatus] en tabla [Process].[NationalJetFuelInvoiceDetail]
				UPDATE [Process].[NationalJetFuelInvoiceDetail]
				   SET [NonconformityFlag] = NULL
				 WHERE RemittanceID = @RemittanceID
				   AND MonthYear = @MonthYear
				   AND Period = @Period
				

				--step 2 Actualizar Status [NationalJetFuelInvoiceControl] a Revert
				UPDATE [Process].[NationalJetFuelInvoiceControl]
				   SET [CountNonconformityRecords] = NULL,
				       [NonconformitySubtotalAmount] = NULL,
					   [DateNonconformity] = NULL,
					   [NonconformityStatusCode] = NULL
				 WHERE RemittanceID = @RemittanceID
				   AND MonthYear = @MonthYear
				   AND Period = @Period

				SELECT 0 [Verify] --En caso EXITOSO continua proceso

			END

		END	        

	

END

' 
END
GO




GO
/****** Object:  StoredProcedure [Process].[UploadNonconformity]    Script Date: 20/12/2016 04:01:35 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[UploadNonconformity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [Process].[UploadNonconformity]
GO
/****** Object:  StoredProcedure [Process].[UploadNonconformity]    Script Date: 20/12/2016 04:01:35 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[UploadNonconformity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Leonardo Eduardo Ramirez
-- Create date: 06/12/2016
-- Description:	Inconformidades
-- =============================================
CREATE PROCEDURE [Process].[UploadNonconformity] 
	@ReconcileInfo AS [Process].[NationalJetFuelNonconformityType] READONLY
AS
BEGIN

	--Step 0 Inicializar Variables
	DECLARE @NumberRecordsProcess INT; 
	DECLARE @NumberRecordsTotal INT;
    DECLARE @Percentage INT; 
	DECLARE @SubtotalAmount DECIMAL(18,2)

	--Variables de remesa
	DECLARE @RemittanceID AS VARCHAR(8),
	@MonthYear VARCHAR(4),
	@Period VARCHAR(5),
	@Username NVARCHAR(50)

	--Inicializar variables de remesa
	SELECT @RemittanceID = MIN([RemittanceID]),
	       @MonthYear = MIN([MonthYear]),
		   @Period = MIN([Period]),
		   @Username = MIN([Username])
	  FROM @ReconcileInfo

BEGIN TRANSACTION 

BEGIN TRY 

	--validacion principal de mnv y closed
	IF EXISTS (SELECT RemittanceID FROM	[Process].[NationalJetFuelInvoiceControl] WITH (NOLOCK) 
	        WHERE RemittanceID = @RemittanceID
				AND MonthYear = @MonthYear
				AND Period = @Period
				AND NonconformityStatusCode = ''CLOSED'')
	BEGIN
		SELECT '''' RemittanceID,'''' MonthYear,'''' Period, 96 Verify --No es vible debido a que la Remesa se encuentra cerrrada para inconformidad
		--Remittance is closed
	END
	ELSE
	BEGIN
		--IF EXISTS (SELECT RemittanceID FROM	[Process].[NationalJetFuelInvoiceDetail] WITH (NOLOCK) 
		--			WHERE RemittanceID = @RemittanceID
		--			AND MonthYear = @MonthYear
		--			AND Period = @Period 
		--			AND [Status] = ''ERRO'')
		--BEGIN
		--	SELECT '''' RemittanceID,'''' MonthYear,'''' Period, 97 Verify --No es vible debido a que ya existen registros en [Process].[NationalJetFuelInvoiceDetail] de esta remesa hay al menos un registro con error que no es de tipo MNV (Matricula no valida)
		--	--Different errors to invalid Equipment Number
		--END
		--ELSE 
		BEGIN		
		
			IF EXISTS (SELECT DISTINCT 
							   [RemittanceID]
							  ,[MonthYear]
							  ,[Period]
						  FROM [Process].[NationalJetFuelInvoiceDetail]
						 WHERE [ID] IN (SELECT [ID] FROM @ReconcileInfo)
						   AND ([RemittanceID]+[MonthYear]+[Period]) <> @RemittanceID+@MonthYear+@Period)
			BEGIN
				--Paso 1.- Mostrar remesas que no corresponden a los IDs del archivo
				SELECT DISTINCT 
					   [RemittanceID]
					  ,[MonthYear]
					  ,[Period]
					  ,1 [Verify] 
				  FROM [Process].[NationalJetFuelInvoiceDetail]
				 WHERE [ID] IN (SELECT [ID] FROM @ReconcileInfo)
	 			   AND ([RemittanceID]+[MonthYear]+[Period] <> @RemittanceID+@MonthYear+@Period)
			END
			ELSE
			BEGIN

				--Step 1 Total de Registros en Invoice Detail 
				SELECT @NumberRecordsTotal = COUNT([ID]) 
				  FROM [Process].[NationalJetFuelInvoiceDetail]
				 WHERE RemittanceID = @RemittanceID
				   AND MonthYear = @MonthYear
				   AND Period = @Period;				

	
				--Step 2 Eliminar información de Inconformidades Anteriores para la Remesa 
				DELETE [Process].[NationalJetFuelNonconformityLoadLog]
				WHERE RemittanceID = @RemittanceID
				   AND MonthYear = @MonthYear
				   AND Period = @Period;

				--Step 4 Log los que no tengan ID de InvoiceDetail valido
				INSERT INTO [Process].[NationalJetFuelNonconformityLoadLog]
				(  [RemittanceID]
				  ,[MonthYear]
				  ,[Period]
				  ,[ID]
				  ,[AirlineCode]
				  ,[ProviderNumber]
				  ,[ServiceCode]
				  ,[FederalTaxCode]
				  ,[StationCode]
				  ,[InvoiceNumber]
				  ,[CustomerNumber]
				  ,[CustomerName]
				  ,[InvoiceDate]
				  ,[ElectronicInvoiceNumber]
				  ,[ElectronicInvoiceDate]
				  ,[ProductNumber]
				  ,[ProductDescription]
				  ,[TicketNumber]
				  ,[OperationType]
				  ,[FlightNumber]
				  ,[EquipmentNumber]
				  ,[AirplaneModel]
				  ,[StartDateTime]
				  ,[EndDateTime]
				  ,[StartMeter]
				  ,[EndMeter]
				  ,[VolumeM3]
				  ,[RateType]
				  ,[JetFuelAmount]
				  ,[FreightAmount]
				  ,[DiscountAmount]
				  ,[FuelingAmount]
				  ,[SubtotalAmount]
				  ,[TaxAmount]
				  ,[TotalAmount]
				  ,[Status]
				  ,[ReconciliationStatus]
				  ,[InvoiceCostVariance] 
				  ,[ErrorDescription])
				SELECT [RemittanceID]
					  ,[MonthYear]
					  ,[Period]
					  ,[ID]					  
					  ,[AirlineCode]
					  ,[ProviderNumber]
					  ,[ServiceCode]
					  ,[FederalTaxCode]
					  ,[StationCode]
					  ,[InvoiceNumber]
					  ,[CustomerNumber]
					  ,[CustomerName]
					  ,[InvoiceDate]
					  ,[ElectronicInvoiceNumber]
					  ,[ElectronicInvoiceDate]
					  ,[ProductNumber]
					  ,[ProductDescription]
					  ,[TicketNumber]
					  ,[OperationType]
					  ,[FlightNumber]
					  ,[EquipmentNumber]
					  ,[AirplaneModel]
					  ,[StartDateTime]
					  ,[EndDateTime]
					  ,[StartMeter]
					  ,[EndMeter]
					  ,[VolumeM3]
					  ,[RateType]
					  ,[JetFuelAmount]
					  ,[FreightAmount]
					  ,[DiscountAmount]
					  ,[FuelingAmount]
					  ,[SubtotalAmount]
					  ,[TaxAmount]
					  ,[TotalAmount]
					  ,[Status]
					  ,[ReconciliationStatus]
					  ,[InvoiceCostVariance] 
					  ,''ID National Jet Fuel Invoice Detail Invalid''
				  FROM @ReconcileInfo
				 WHERE [ID] NOT IN (SELECT [ID]
									  FROM [Process].[NationalJetFuelInvoiceDetail]
									 WHERE RemittanceID = @RemittanceID
									   AND MonthYear = @MonthYear
									   AND Period = @Period)

				--Step 4.5 Log los que tengan ID de InvoiceDetail valido y este ya inconformado
				INSERT INTO [Process].[NationalJetFuelNonconformityLoadLog]
				(  [RemittanceID]
				  ,[MonthYear]
				  ,[Period]
				  ,[ID]
				  ,[AirlineCode]
				  ,[ProviderNumber]
				  ,[ServiceCode]
				  ,[FederalTaxCode]
				  ,[StationCode]
				  ,[InvoiceNumber]
				  ,[CustomerNumber]
				  ,[CustomerName]
				  ,[InvoiceDate]
				  ,[ElectronicInvoiceNumber]
				  ,[ElectronicInvoiceDate]
				  ,[ProductNumber]
				  ,[ProductDescription]
				  ,[TicketNumber]
				  ,[OperationType]
				  ,[FlightNumber]
				  ,[EquipmentNumber]
				  ,[AirplaneModel]
				  ,[StartDateTime]
				  ,[EndDateTime]
				  ,[StartMeter]
				  ,[EndMeter]
				  ,[VolumeM3]
				  ,[RateType]
				  ,[JetFuelAmount]
				  ,[FreightAmount]
				  ,[DiscountAmount]
				  ,[FuelingAmount]
				  ,[SubtotalAmount]
				  ,[TaxAmount]
				  ,[TotalAmount]
				  ,[Status]
				  ,[ReconciliationStatus]
				  ,[InvoiceCostVariance] 
				  ,[ErrorDescription])
				SELECT [RemittanceID]
					  ,[MonthYear]
					  ,[Period]
					  ,[ID]					  
					  ,[AirlineCode]
					  ,[ProviderNumber]
					  ,[ServiceCode]
					  ,[FederalTaxCode]
					  ,[StationCode]
					  ,[InvoiceNumber]
					  ,[CustomerNumber]
					  ,[CustomerName]
					  ,[InvoiceDate]
					  ,[ElectronicInvoiceNumber]
					  ,[ElectronicInvoiceDate]
					  ,[ProductNumber]
					  ,[ProductDescription]
					  ,[TicketNumber]
					  ,[OperationType]
					  ,[FlightNumber]
					  ,[EquipmentNumber]
					  ,[AirplaneModel]
					  ,[StartDateTime]
					  ,[EndDateTime]
					  ,[StartMeter]
					  ,[EndMeter]
					  ,[VolumeM3]
					  ,[RateType]
					  ,[JetFuelAmount]
					  ,[FreightAmount]
					  ,[DiscountAmount]
					  ,[FuelingAmount]
					  ,[SubtotalAmount]
					  ,[TaxAmount]
					  ,[TotalAmount]
					  ,[Status]
					  ,[ReconciliationStatus]
					  ,[InvoiceCostVariance] 
					  ,''ID National Jet Fuel Invoice Detail Valid,but Disagreed (Nonconformity Process)''
				  FROM @ReconcileInfo
				 WHERE [ID] IN (SELECT [ID]
									  FROM [Process].[NationalJetFuelInvoiceDetail]
									 WHERE RemittanceID = @RemittanceID
									   AND MonthYear = @MonthYear
									   AND Period = @Period
									   AND NonconformityFlag = 1)				


			--Step 6 Cuando ya sean registros validos se inconforman (Si no existen los ignora y si existian ya los sobreescribe)
			UPDATE [Process].[NationalJetFuelInvoiceDetail]
			   SET [NonconformityFlag] = 1
			 WHERE RemittanceID = @RemittanceID
			   AND MonthYear = @MonthYear
			   AND Period = @Period	
			   AND ID IN (SELECT ID FROM @ReconcileInfo);


			--Cuantos registros inserto y SUM de SubtotalAmount
			SET @NumberRecordsProcess =(SELECT COUNT(SubtotalAmount) 
									 FROM [Process].[NationalJetFuelInvoiceDetail]
									WHERE RemittanceID = @RemittanceID
									  AND MonthYear = @MonthYear
									  AND Period = @Period
									  AND [NonconformityFlag] = 1 );

			SET @SubtotalAmount = (SELECT SUM(CASE WHEN B.InvoiceCostVariance > 0 THEN B.InvoiceCostVariance ELSE A.SubtotalAmount END) 
									 FROM [Process].[NationalJetFuelInvoiceDetail] A LEFT JOIN
									      [Process].[NationalJetFuelReconciliationDetail] B ON (A.[ID] = B.[InvoiceDetailID])
									WHERE A.RemittanceID = @RemittanceID
									  AND A.MonthYear = @MonthYear
									  AND A.Period = @Period
									  AND [NonconformityFlag] = 1);
						
			--Step 9 Update Control ([CountNonconformityRecords] y [NonconformitySubtotalAmount])
			IF ( @NumberRecordsProcess > 0 )
			BEGIN 		
				UPDATE  [Process].[NationalJetFuelInvoiceControl]
				   SET  [CountNonconformityRecords] = ISNULL(@NumberRecordsProcess, 0),
						[NonconformitySubtotalAmount] = ISNULL(@SubtotalAmount, 0),
						[DateNonconformity] = GETDATE()--,
						--[NonconformityByUserName] = @Username
				  WHERE RemittanceID = @RemittanceID
					AND MonthYear = @MonthYear
					AND Period = @Period; 
			END; 				
		

			--STEP 14 Si hay elementos en Log de errores se informa
			IF EXISTS (SELECT [LogID]
						 FROM [Process].[NationalJetFuelNonconformityLoadLog]
						WHERE RemittanceID = @RemittanceID
						  AND MonthYear = @MonthYear
						  AND Period = @Period)
			BEGIN
				SELECT '''' RemittanceID,'''' MonthYear,'''' Period, 98 Verify
			END

			END								
		END
	END	 	    

END TRY 

BEGIN CATCH  

	SELECT '''' RemittanceID,'''' MonthYear,'''' Period, 99 Verify
	   
    IF @@TRANCOUNT > 0 
      BEGIN 	  
          ROLLBACK TRANSACTION 
      END 

END CATCH; 

IF @@TRANCOUNT > 0 
  
  BEGIN 
      COMMIT TRANSACTION 
  END 

END




' 
END
GO


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

IF NOT EXISTS ( SELECT	PageName FROM	Security.PageReport WITH (NOLOCK) WHERE	PageName = 'DownloadCanditateInvoiceRecords' )
BEGIN
INSERT INTO Security.PageReport (PageName, PathReport, Status)
VALUES ('DownloadCanditateInvoiceRecords', '/'+@DB+'/Reports/DownloadCanditateInvoiceRecords', 1)
END

IF NOT EXISTS ( SELECT	PageName FROM	Security.PageReport WITH (NOLOCK) WHERE	PageName = 'NonconformityDocumentParameter' )
BEGIN
INSERT INTO Security.PageReport (PageName, PathReport, Status)
VALUES ('NonconformityDocumentParameter', '/'+@DB+'/Reports/NonconformityDocumentParameter', 1)
END

IF NOT EXISTS ( SELECT	PageName FROM	Security.PageReport WITH (NOLOCK) WHERE	PageName = 'ExportNonconformityLoadLog' )
BEGIN
INSERT INTO Security.PageReport (PageName, PathReport, Status)
VALUES ('ExportNonconformityLoadLog', '/'+@DB+'/Reports/ExportNonconformityLoadLog', 1)
END




GO
/****** Object:  Table [Process].[NonconformityDocumentParameter]    Script Date: 12/12/2016 10:20:53 a.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[NonconformityDocumentParameter]') AND type in (N'U'))
DROP TABLE [Process].[NonconformityDocumentParameter]
GO
/****** Object:  Table [Process].[NonconformityDocumentParameter]    Script Date: 12/12/2016 10:20:53 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[NonconformityDocumentParameter]') AND type in (N'U'))
BEGIN
CREATE TABLE [Process].[NonconformityDocumentParameter](
	[AirlineCode] [varchar](3) NOT NULL,
	[ServiceCode] [varchar](12) NOT NULL,
	[ProviderNumber] [varchar](10) NOT NULL,
	[DocumentTitle] [varchar](200) NULL,
	[Receiver] [varchar](50) NULL,
	[ReceiverAddress] [varchar](200) NULL,
	[Location] [varchar](50) NULL,
	[OpeningText] [varchar](800) NULL,
	[ClosingText] [varchar](200) NULL,
	[Sender] [varchar](200) NULL,
	[CcSection] [varchar](300) NULL,
	[AirlineAddress] [varchar](200) NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
INSERT [Process].[NonconformityDocumentParameter] ([AirlineCode], [ServiceCode], [ProviderNumber], [DocumentTitle], [Receiver], [ReceiverAddress], [Location], [OpeningText], [ClosingText], [Sender], [CcSection], [AirlineAddress]) VALUES (N'Y4', N'CM', N'2400000', N'Aeropuertos y Servicios Auxiliares
Coordinación de la Unidad de Servicios Corporativos
Subdirección de Finanzas de México
Gerencia de Ingresos', N'C.P. Bertha Manjarrez García', N'Ave. 602 # 161 Edificio B 1er. Piso
Col. San Juan de Aragón
Del. Venustiano Carranza
CP 15620', N'Mexico D.F. a', N'Derivado de la revisión de las factura(s) _________ anexas del servicio(s) de ________________ del mes de ______________ hago de su conocimiento que no procede la cantidad (Cantidad que se determine de las operaciones que se seleccionen en SISAC)  MXN más IVA. Ya que se detectó que hay cobros erróneos que no son de VOI. 

Por tal motivo le solicito su apreciable apoyo, para que se expida la nota de crédito correspondiente.', N'Agradezco de antemano su apoyo, sin más por el momento, quedo a sus órdenes para cualquier aclaración al respecto', N'Lic. Carlos Alberto González
Dirección de Contraloría Corporativa
Concesionaria Vuela Compañía de Aviación SAPI de CV
VOLARIS', N'C.P. Octavio Benítez Espinoza - Jefe de Cuentas por Pagar
C.P. Arturo Mendoza Toledo - Resp. De Servicios Aeroportuarios y Combustibles', N'Edificio Samara ° Av. Antonio Dovali Jaime No. 70
Torre B, piso 13 ° Col. Zedec Santa Fe,
Del. Álvaro Obregón ° C.P. 01210 ° México, D.F')
GO




GO
/****** Object:  Table [Process].[Sequence]    Script Date: 19/12/2016 04:42:47 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[Sequence]') AND type in (N'U'))
DROP TABLE [Process].[Sequence]
GO
/****** Object:  Table [Process].[Sequence]    Script Date: 19/12/2016 04:42:47 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[Sequence]') AND type in (N'U'))
BEGIN
CREATE TABLE [Process].[Sequence](
	[Sequence] [bigint] NOT NULL
) ON [PRIMARY]
END
GO



GO
/****** Object:  StoredProcedure [Process].[ValidateAndSaveRemittances]    Script Date: 19/12/2016 11:52:17 a.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[ValidateAndSaveRemittances]') AND type in (N'P', N'PC'))
DROP PROCEDURE [Process].[ValidateAndSaveRemittances]
GO
/****** Object:  StoredProcedure [Process].[ValidateAndSaveRemittances]    Script Date: 19/12/2016 11:52:17 a.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[ValidateAndSaveRemittances]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- =============================================
-- Author:		Leonardo Eduardo Ramirez
-- Create date: 21/06/2016
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Process].[ValidateAndSaveRemittances] 
	@RemittanceInfo AS [Process].[NationalJetFuelInvoiceDetailType] READONLY,
	@AirlineCode AS VARCHAR(3)
AS
BEGIN

BEGIN TRANSACTION 

BEGIN TRY 
	
    IF EXISTS (SELECT A.RemittanceID, A.MonthYear, A.Period 
				 FROM [Process].[NationalJetFuelInvoiceControl] A INNER JOIN
				       (SELECT DISTINCT REPLACE([RemittanceID], ''/'',''-'') RemittanceID, MonthYear, Period FROM @RemittanceInfo) B 
						 ON (A.RemittanceID = B.RemittanceID AND A.MonthYear = B.MonthYear AND A.Period = B.Period))
	BEGIN
		--Paso 0.- Mostrar remesas que ya existen en DB
		SELECT A.RemittanceID, A.MonthYear, A.Period,  1 Verify
	      FROM [Process].[NationalJetFuelInvoiceControl] A INNER JOIN
			   (SELECT DISTINCT REPLACE([RemittanceID], ''/'',''-'') RemittanceID, MonthYear, Period FROM @RemittanceInfo) B 
						 ON (A.RemittanceID = B.RemittanceID AND A.MonthYear = B.MonthYear AND A.Period = B.Period)			
	END
	ELSE
	BEGIN		
		--Paso 1.- Inserto en tabla de control dado que todas son remesas nuevas
		INSERT INTO [Process].[NationalJetFuelInvoiceControl]
           ([RemittanceID]
		   ,[MonthYear]
		   ,[Period]
           ,[AirlineCode]
           ,[ProviderNumber]
           ,[JetFuelAmount]
           ,[FreightAmount]
           ,[DiscountAmount]
           ,[FuelingAmount]
           ,[SubtotalAmount]
           ,[TaxAmount]
           ,[TotalAmount]
		   ,[ProcessDate])
		SELECT REPLACE([RemittanceID], ''/'',''-'')
			  ,MonthYear
			  ,Period
		      ,@AirlineCode
			  ,MIN(ProviderNumber)
			  ,SUM(JetFuelAmount)
			  ,SUM(FreightAmount)
			  ,SUM(DiscountAmount)
			  ,SUM(FuelingAmount)
			  ,SUM(SubtotalAmount)
			  ,SUM(TaxAmount)
			  ,SUM(TotalAmount)
			  ,GETDATE()
	      FROM @RemittanceInfo
	     GROUP BY RemittanceID, MonthYear, Period
		
		--Paso 2.- Inserto en tabla de detalle dado que ya se inserto antes en la tabla control donde [RemittanceID] es llave
		INSERT INTO [Process].[NationalJetFuelInvoiceDetail]
           ([RemittanceID]		
		   ,[MonthYear]
		   ,[Period]   
           ,[AirlineCode]
           ,[ProviderNumber]
           ,[ServiceCode]
           ,[FederalTaxCode]
           ,[StationCode]
           ,[InvoiceNumber]
           ,[CustomerNumber]
           ,[CustomerName]
           ,[InvoiceDate]
           ,[ElectronicInvoiceNumber]
           ,[ElectronicInvoiceDate]
           ,[ProductNumber]
           ,[ProductDescription]
           ,[TicketNumber]
           ,[OperationType]
           ,[FlightNumber]
           ,[EquipmentNumber]
           ,[AirplaneModel]
           ,[StartDateTime]
           ,[EndDateTime]
           ,[StartMeter]
           ,[EndMeter]
           ,[VolumeM3]
           ,[RateType]
           ,[JetFuelAmount]
           ,[FreightAmount]
           ,[DiscountAmount]
           ,[FuelingAmount]
           ,[SubtotalAmount]
           ,[TaxAmount]
           ,[TotalAmount])
		SELECT REPLACE([RemittanceID], ''/'',''-'')
		   ,[MonthYear]
		   ,[Period]
           ,@AirlineCode
           ,[ProviderNumber]
           ,[ServiceCode]
           ,[FederalTaxCode]
           ,[StationCode]
           ,[InvoiceNumber]
           ,[CustomerNumber]
           ,[CustomerName]
           ,[InvoiceDate]
           ,[ElectronicInvoiceNumber]
           ,[ElectronicInvoiceDate]
           ,[ProductNumber]
           ,[ProductDescription]
           ,[TicketNumber]
           ,[OperationType]
           ,[FlightNumber]
           ,[EquipmentNumber]
           ,[AirplaneModel]
           ,[StartDateTime]
           ,[EndDateTime]
           ,[StartMeter]
           ,[EndMeter]
           ,[VolumeM3]
           ,[RateType]
           ,[JetFuelAmount]
           ,[FreightAmount]
           ,[DiscountAmount]
           ,[FuelingAmount]
           ,[SubtotalAmount]
           ,[TaxAmount]
           ,[TotalAmount]
		FROM @RemittanceInfo

		--Paso 3.- Detectar errores por registro
		UPDATE [Process].[NationalJetFuelInvoiceDetail] 
		   SET [ErrorDescription] = 
		       CASE WHEN NOT EXISTS(SELECT [ProviderNumber] 
	                          FROM [Finance].[Provider] 
							 WHERE [ProviderNumber] = A.[ProviderNumber]
							   AND [Status] = 1) 
					THEN ''* No. Proveedor no valido: ['' + [ProviderNumber] + ''], '' 
					ELSE '''' END + 
			   CASE WHEN NOT EXISTS(SELECT [ServiceCode] 
									  FROM [Airport].[Service] 
									 WHERE [ServiceCode] = A.[ServiceCode]
									   AND [Status] = 1
									   AND A.[ServiceCode] = ''CM'') 
					THEN ''* Código de servicio no valido: ['' + [ServiceCode] + ''], '' 
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [TaxCode] 
									  FROM [Finance].[Tax] 
									 WHERE [TaxCode] = A.[FederalTaxCode]
									   AND [Status] = 1) 
					THEN ''* Código del impuesto no valido: ['' + [FederalTaxCode] + ''], '' 
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [ProviderNumberPrimary] 
									  FROM [Finance].[NationalFuelContract] 
									 WHERE [ProviderNumberPrimary] = A.[ProviderNumber]
									   AND [StationCode] = A.[StationCode]
									   AND [Status] = 1
									   AND [FederalTaxCode] = A.[FederalTaxCode]) 
					THEN ''* El Impuesto aplicado: ['' + [FederalTaxCode] + ''] no corresponde al que se tiene en SISAC en Contratos Nacionales del aeropuerto: ['' + [StationCode] + ''] y proveedor ['' + [ProviderNumber] + ''], ''
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [StationCode]
									  FROM [Airport].[Airport]
									 WHERE [StationCode] = A.[StationCode]
									   AND [Status] = 1) 
					THEN ''* Aeropuerto no valido: ['' + [StationCode] + ''], ''
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [Sequence]
									  FROM [Itinerary].[Itinerary]
									 WHERE [FlightNumber] = A.[FlightNumber]
									   AND [EquipmentNumber] = A.[EquipmentNumber]) 
					THEN ''* No hay vuelos que coincidan con Numero de Vuelo: ['' + [FlightNumber] +  ''] y Matricula: [''+ [EquipmentNumber] +''], ''
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [EquipmentNumber]
									  FROM [Airport].[Airplane]
									 WHERE [EquipmentNumber] = A.[EquipmentNumber]
									   AND [Status] = 1) 
					THEN ''* Matricula no valido: ['' + [EquipmentNumber] + ''], ''
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [EquipmentNumber]
									  FROM [Airport].[Airplane]
									 WHERE [EquipmentNumber] = A.[EquipmentNumber]
									   AND [AirplaneModel] = A.[AirplaneModel]
									   AND [Status] = 1) 
					THEN ''* Modelo de avión no valido: ['' + [AirplaneModel] + ''] con Matricula: ['' + [EquipmentNumber] + ''], ''
					ELSE '''' END +
			   CASE WHEN EXISTS(SELECT [AirplaneModel]
								  FROM [Airport].[AirplaneType]
								 WHERE [AirplaneModel] = A.[AirplaneModel]
								   AND [FuelInLiters] < (A.[VolumeM3] * 1000)
								   AND [Status] = 1) 
					THEN ''* Los litros Cargados sobre pasa el máximo en litros del tipo de equipo: ['' + CAST(([VolumeM3] * 1000) AS VARCHAR) + '' > '' + CAST((SELECT TOP 1 [FuelInLiters] FROM [Airport].[AirplaneType] WHERE [AirplaneModel] = A.[AirplaneModel] AND [Status] = 1) AS VARCHAR) + ''], '' 
					ELSE '''' END +
			   CASE WHEN [StartDateTime] > [EndDateTime]
					THEN ''* La fecha final es menor que la fecha inicial, ''
					ELSE '''' END +
			   CASE WHEN [SubtotalAmount] + [TaxAmount] <> [TotalAmount]
					THEN ''* El calculo total (TotalAmount) no conincide con el total del archivo (SubtotalAmount + TaxAmount)'' 
					ELSE '''' END,
			   [Status] =
			   CASE WHEN NOT EXISTS(SELECT [ProviderNumber] 
	                          FROM [Finance].[Provider] 
							 WHERE [ProviderNumber] = A.[ProviderNumber]
							   AND [Status] = 1) 
					THEN ''A'' 
					ELSE '''' END + 
			   CASE WHEN NOT EXISTS(SELECT [ServiceCode] 
									  FROM [Airport].[Service] 
									 WHERE [ServiceCode] = A.[ServiceCode]
									   AND [Status] = 1
									   AND A.[ServiceCode] = ''CM'') 
					THEN ''B'' 
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [TaxCode] 
									  FROM [Finance].[Tax] 
									 WHERE [TaxCode] = A.[FederalTaxCode]
									   AND [Status] = 1) 
					THEN ''C'' 
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [ProviderNumberPrimary] 
									  FROM [Finance].[NationalFuelContract] 
									 WHERE [ProviderNumberPrimary] = A.[ProviderNumber]
									   AND [StationCode] = A.[StationCode]
									   AND [Status] = 1
									   AND [FederalTaxCode] = A.[FederalTaxCode]) 
					THEN ''D''
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [StationCode]
									  FROM [Airport].[Airport]
									 WHERE [StationCode] = A.[StationCode]
									   AND [Status] = 1) 
					THEN ''E''
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [Sequence]
									  FROM [Itinerary].[Itinerary]
									 WHERE [FlightNumber] = A.[FlightNumber]
									   AND [EquipmentNumber] = A.[EquipmentNumber]) 
					THEN ''F''
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [EquipmentNumber]
									  FROM [Airport].[Airplane]
									 WHERE [EquipmentNumber] = A.[EquipmentNumber]
									   AND [Status] = 1) 
					THEN ''MNV''
					ELSE '''' END +
			   CASE WHEN NOT EXISTS(SELECT [EquipmentNumber]
									  FROM [Airport].[Airplane]
									 WHERE [EquipmentNumber] = A.[EquipmentNumber]
									   AND [AirplaneModel] = A.[AirplaneModel]
									   AND [Status] = 1) 
					THEN ''G''
					ELSE '''' END +
			   CASE WHEN EXISTS(SELECT [AirplaneModel]
								  FROM [Airport].[AirplaneType]
								 WHERE [AirplaneModel] = A.[AirplaneModel]
								   AND [FuelInLiters] < (A.[VolumeM3] * 1000)
								   AND [Status] = 1) 
					THEN ''H'' 
					ELSE '''' END +
			   CASE WHEN [StartDateTime] > [EndDateTime]
					THEN ''I''
					ELSE '''' END +
			   CASE WHEN [SubtotalAmount] + [TaxAmount] <> [TotalAmount]
					THEN ''J'' 
					ELSE '''' END			  
		  FROM [Process].[NationalJetFuelInvoiceDetail] A
		 WHERE [RemittanceID] IN (SELECT DISTINCT REPLACE([RemittanceID], ''/'',''-'') FROM @RemittanceInfo)

		--Paso 4.- Etiquetar registro como exitoso o error
		UPDATE [Process].[NationalJetFuelInvoiceDetail] 
		   SET [Status] = CASE WHEN LEN(ISNULL([ErrorDescription],'''')) > 2 THEN ''ERRO'' ELSE ''SUCC'' END 
		 WHERE [RemittanceID] IN (SELECT DISTINCT REPLACE([RemittanceID], ''/'',''-'') FROM @RemittanceInfo)
		   AND [Status] <> ''MNV''		--Para dejar con MNV los cuando solo existio error de matricula no valida

		--Paso 5.- Etiquetar registro como exitoso o error en tabla de control
		UPDATE [Process].[NationalJetFuelInvoiceControl]
	       SET [RemittanceStatusCode] = CASE WHEN EXISTS (SELECT [RemittanceID] FROM [Process].[NationalJetFuelInvoiceDetail] WHERE [RemittanceID] = A.[RemittanceID] AND [Status] IN (''ERRO'',''MNV'')) THEN ''ERRO'' ELSE ''SUCC'' END,
		       [ReconciliationStatusCode] = ''NEW'',
			   [DocumentStatusCode] = ''PEND''
		  FROM [Process].[NationalJetFuelInvoiceControl] A
	     WHERE [RemittanceID] IN (SELECT DISTINCT REPLACE([RemittanceID], ''/'',''-'') FROM @RemittanceInfo) 		

	END

END TRY 

BEGIN CATCH  

	SELECT '''' RemittanceID,'''' MonthYear,'''' Period, 99 Verify
	   
    IF @@TRANCOUNT > 0 
      BEGIN 	  
          ROLLBACK TRANSACTION 
      END 

END CATCH; 

IF @@TRANCOUNT > 0 
  
  BEGIN 
      COMMIT TRANSACTION 
  END 

END

' 
END
GO




GO  

IF exists
(
SELECT *
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'CreationDate' AND TABLE_NAME = 'ManifestDepartureBoardingDetail'
)
	ALTER TABLE [Itinerary].[ManifestDepartureBoardingDetail] DROP COLUMN [CreationDate]

	
	

GO
/****** Object:  StoredProcedure [Process].[JetFuelNonconformityDocumentParameter]    Script Date: 20/12/2016 04:17:18 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[JetFuelNonconformityDocumentParameter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [Process].[JetFuelNonconformityDocumentParameter]
GO
/****** Object:  StoredProcedure [Process].[JetFuelNonconformityDocumentParameter]    Script Date: 20/12/2016 04:17:18 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[JetFuelNonconformityDocumentParameter]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =======================================================
-- Author: Leonardo Eduardo Ramirez		
-- Create date: 12/12/2016
-- Description: Revertir Nonconformity
-- =======================================================
-- [Process].[JetFuelNonconformityDocumentParameter] ''16-2830'', ''0416'', ''01-06'', ''20161212'', ''SEPTIEMBRE''
CREATE PROCEDURE [Process].[JetFuelNonconformityDocumentParameter]
	@RemittanceID AS VARCHAR(8),
	@MonthYear VARCHAR(4),
	@Period VARCHAR(5),
	@DocumentDate DATETIME,
	@Month VARCHAR(20)
AS
BEGIN

	DECLARE @Reference VARCHAR(15)
	DECLARE @AirlineCode VARCHAR(3)
	DECLARE @ProviderNumber VARCHAR(50)
	DECLARE @ServiceCode VARCHAR(50)
	DECLARE @Invoices VARCHAR(MAX)
	DECLARE @SubtotalAmount DECIMAL(18,5)
	DECLARE @Sequence BIGINT

	SELECT @Sequence = ISNULL(MAX(Sequence),0) FROM [Process].[Sequence]
	DELETE [Process].[Sequence]
	INSERT [Process].[Sequence] (Sequence) SELECT @Sequence+1
	
	SELECT @Reference = MIN(AirlineCode) + '' '' + RIGHT(''000000''+CAST((@Sequence+1) AS VARCHAR),6) + ''/'' + ''20'' + RIGHT(MIN(MonthYear),2),
	       @AirlineCode = MIN(AirlineCode),
		   @ProviderNumber = MIN(ProviderNumber),
		   @ServiceCode = MIN(ServiceCode)
	  FROM [Process].[NationalJetFuelInvoiceDetail]
	 WHERE RemittanceID = @RemittanceID
	   AND MonthYear = @MonthYear
	   AND Period = @Period

	SELECT @Invoices = coalesce(@Invoices + '', '', '''') + CAST([InvoiceNumber] AS VARCHAR)
	  FROM [Process].[NationalJetFuelInvoiceDetail]
	 WHERE RemittanceID = @RemittanceID
	   AND MonthYear = @MonthYear
	   AND Period = @Period
	   AND NonconformityFlag = 1

	SELECT @SubtotalAmount = NonconformitySubtotalAmount
	  FROM [Process].[NationalJetFuelInvoiceControl]
	 WHERE RemittanceID = @RemittanceID
	   AND MonthYear = @MonthYear
	   AND Period = @Period

	PRINT @Reference 
	PRINT @AirlineCode
	PRINT @ProviderNumber
	PRINT @ServiceCode

	--Update Reference en Control
	UPDATE [Process].[NationalJetFuelInvoiceControl]
	   SET [NonconformityReference] = @Reference
	 WHERE RemittanceID = @RemittanceID
	   AND MonthYear = @MonthYear
	   AND Period = @Period

	--parametros para reporte
	SELECT TOP 1 [AirlineCode]
		  ,A.[ServiceCode]
		  ,[ProviderNumber]
		  ,[DocumentTitle]
		  ,[Receiver]
		  ,[ReceiverAddress]
		  ,[Location]
		  ,[OpeningText]
		  ,[ClosingText]
		  ,[Sender]
		  ,[CcSection]
		  ,[AirlineAddress]
		  ,@DocumentDate DocumentDate
		  ,@Reference Reference
		  ,@Month [Month]
		  ,B.ServiceName [Service]
		  ,@Invoices Invoices
		  ,@SubtotalAmount [NonconformitySubtotalAmount]
	  FROM [Process].[NonconformityDocumentParameter] A LEFT JOIN
	       [Airport].[Service] B ON (A.[ServiceCode] = B.[ServiceCode])
	 WHERE [AirlineCode] = @AirlineCode
       AND A.[ServiceCode] = @ServiceCode
	   AND [ProviderNumber] = @ProviderNumber

END

' 
END
GO




GO

DECLARE @DB VARCHAR(100)

IF(DB_NAME() = 'SISAC')
BEGIN
	SET @DB = 'SISAC'
END
ELSE
BEGIN
	SET @DB = 'SISAC_CERT'
END

IF NOT EXISTS (SELECT PageName FROM Security.PageReport WHERE PageName = 'ManifestDepartureMex')
BEGIN
	INSERT INTO Security.PageReport (PageName, PathReport, Status)
	VALUES  ( 'ManifestDepartureMex', '/'+@DB+'/Reports/ManifestDepartureMex', 1)
END;


IF NOT EXISTS (SELECT PageName FROM Security.PageReport WHERE PageName = 'ManifestArrivalMex')
BEGIN
	INSERT INTO Security.PageReport (PageName, PathReport, Status)
	VALUES  ( 'ManifestArrivalMex', '/'+@DB+'/Reports/ManifestArrivalMex', 1)
END;


GO

IF not exists
(
SELECT *
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'BusinessName' AND TABLE_NAME = 'Airline'
)
BEGIN
	ALTER TABLE Airport.Airline
	ADD BusinessName VARCHAR(100) NULL;	
END;




DECLARE @DB VARCHAR(100)

IF(DB_NAME() = 'SISAC')
BEGIN
	SET @DB = 'SISAC'
END
ELSE
BEGIN
	SET @DB = 'SISAC_CERT'
END

IF NOT EXISTS (SELECT PageName FROM Security.PageReport WHERE PageName = 'A319')
BEGIN
	INSERT INTO Security.PageReport (PageName, PathReport, Status)
	VALUES  ( 'A319', '/'+@DB+'/Reports/A319', 1)
END;


IF NOT EXISTS (SELECT PageName FROM Security.PageReport WHERE PageName = 'A320')
BEGIN
	INSERT INTO Security.PageReport (PageName, PathReport, Status)
	VALUES  ( 'A320', '/'+@DB+'/Reports/A320', 1)
END;

IF NOT EXISTS (SELECT PageName FROM Security.PageReport WHERE PageName = 'A321')
BEGIN
	INSERT INTO Security.PageReport (PageName, PathReport, Status)
	VALUES  ( 'A321', '/'+@DB+'/Reports/A321', 1)
END;


GO
/****** Object:  StoredProcedure [Process].[SavePolizaProvisionesInt]    Script Date: 29/12/2016 04:40:47 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[SavePolizaProvisionesInt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [Process].[SavePolizaProvisionesInt]
GO
/****** Object:  StoredProcedure [Process].[SaveNationalPolicyCost]    Script Date: 29/12/2016 04:40:47 p.m. ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[SaveNationalPolicyCost]') AND type in (N'P', N'PC'))
DROP PROCEDURE [Process].[SaveNationalPolicyCost]
GO
/****** Object:  StoredProcedure [Process].[SaveNationalPolicyCost]    Script Date: 29/12/2016 04:40:47 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[SaveNationalPolicyCost]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- ===============================================================
-- Author: Leonardo Eduardo Ramirez		
-- Create date: 20160601
-- Description:	Polizas Save
-- [Process].[SaveNationalPolicyCost] ''20160501'', ''20160531'', ''20151201'', ''20151215'', ''ok'', ''okasssss'', ''20160101'', ''20160101'', ''20160101'', ''Y4'', ''4000000,0000001,4000'', ''CM'', ''MEX'', ''usuario''
-- ===============================================================
CREATE PROCEDURE [Process].[SaveNationalPolicyCost]      
     @StartDateReal AS DATETIME  
    ,@EndDateReal AS DATETIME 
	,@StartDateComp AS DATETIME  
    ,@EndDateComp AS DATETIME   
	,@HeaderText AS VARCHAR(50)
	,@ItemText   AS VARCHAR(50) 
	,@DateValue AS DATETIME  
	,@DatePosting AS DATETIME
	,@DateBase AS DATETIME
	,@AirlineCode AS VARCHAR(3) 
	,@ProviderCodes AS VARCHAR(MAX) 
	,@ServiceCodes AS VARCHAR(MAX) 
	,@StationCodes AS VARCHAR(MAX) --Opcion XML
	,@UserSend AS NVARCHAR(50) = '' ''
AS  
BEGIN  
SET NOCOUNT ON; 

--Declarar Variables tipo tabla y variables para el procedimiento
 DECLARE @Acumulado AS TABLE (
	[JetFuelTicketID] [bigint] NULL,
	[subtotalRound] [decimal](18, 2) NULL,
	[row_iden] [bigint] NULL,
	[DepartureStation] [varchar](3) NULL,
	[CurrencyCode] [varchar](3) NULL,
	[EquipmentNumber] [varchar](12) NULL,
	[FueledQtyLts] [int] NULL,
	[LiabilityAccountNumber] [varchar](8) NULL,
	[ConceptAmount] [decimal](38, 2) NULL,
	[ServiceCode] [varchar](12) NULL,
	[TotalRegTicket] [int] NULL,
	[AirlineCode] [varchar](3) NULL
)
DECLARE @TableOrder AS TABLE (UNORDERED INT, ORDERED INT NULL)
DECLARE @TableMaxReg AS TABLE (row_iden INT, reg INT)
DECLARE @NumRegMax INT
SET @NumRegMax = 998
DECLARE @PolicyID BIGINT

--Declarar Constantes para el procedimiento
DECLARE @Accounting VARCHAR(17)
SET @Accounting = ''50210000''
DECLARE @Liability VARCHAR(17)
SET @Liability = ''20410101''

BEGIN TRANSACTION; 

BEGIN TRY
	--Paso 0 Orden con base en Ticket y Moneda para Real y Complementario. Se incluye CAST(CAST(waers AS VARBINARY) AS INT)
	INSERT @TableOrder (UNORDERED, ORDERED)
	SELECT DISTINCT (Row_number() OVER(PARTITION BY waers ORDER BY A.NationalJetFuelTicketID asc) - 1 ) / @NumRegMax + 1 + CAST(CAST(waers AS VARBINARY) AS INT) AS rownumber, NULL 
			  FROM (
			       (SELECT MIN(A.CurrencyCode)																		as waers, --DUDA Que claves de moneda usaremos, es importante pues al hacer sumas debe estar unificada la moneda              [CurrencyCode]                       
						   40																						as newbs,
						   NationalJetFuelTicketID
					FROM   [Process].[NationalJetFuelCost] A LEFT JOIN
						   [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
					WHERE  (([DepartureDate] BETWEEN @StartDateReal AND @EndDateReal + '' 23:59:59'') AND  A.PolicyID IS NULL) 
					  AND  A.[AirlineCode] = @AirlineCode
					  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'',''))
					  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'',''))
					  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))				  
					GROUP  BY NationalJetFuelTicketID) 
				   UNION ALL
				   (SELECT MIN(A.CurrencyCode)																		as waers, --DUDA Que claves de moneda usaremos, es importante pues al hacer sumas debe estar unificada la moneda              [CurrencyCode]                       
						   40																						as newbs,
						   NationalJetFuelTicketID
					FROM   [Process].[NationalJetFuelCost] A LEFT JOIN
						   [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
					WHERE  [DepartureDate] BETWEEN @StartDateComp AND @EndDateComp + '' 23:59:59''
					  AND  A.[AirlineCode] = @AirlineCode
					  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'',''))
					  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'',''))
					  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))				  
					GROUP  BY NationalJetFuelTicketID) 					
					) A	

	--Paso 1 Agregar registro en Poliza Control con filtros realizados y guardar información PolicyID
	INSERT INTO [Process].[NationalJetFuelPolicyControl] 
		  ([DocumentType]
		  ,[CreationDate]
		  ,[Status]
		  ,[AirlineCode]
		  ,[ServiceCodes]
		  ,[ProviderCodes]
		  ,[AirportCodes]
		  ,[StartDateReal]
		  ,[EndDateReal]
		  ,[StartDateComplementary]
		  ,[EndDateComplementary]
		  ,[DateBaseline]
		  ,[DateValue]
		  ,[DatePosting]
		  ,[HeaderText]
		  ,[ItemText]
		  ,[SendByUserName])
	SELECT ''PV''
	      ,GETDATE()
		  ,''CREATED''
		  ,@AirlineCode
		  ,@ServiceCodes
		  ,@ProviderCodes
		  ,@StationCodes
		  ,@StartDateReal
		  ,@EndDateReal
		  ,@StartDateComp
		  ,@EndDateComp
		  ,@DateBase
		  ,@DateValue
		  ,@DatePosting
		  ,@HeaderText
		  ,@ItemText
		  ,@UserSend

SET @PolicyID= @@IDENTITY


	--Paso 2.0 Guardar Acumulado Real y Complementario, registros NEWBS 50. Realiza Suma de galones y Tarifas 
INSERT INTO @Acumulado ([JetFuelTicketID]
		  ,[subtotalRound]
		  ,[row_iden]
		  ,[DepartureStation]
		  ,[CurrencyCode]
		  ,[EquipmentNumber]
		  ,[FueledQtyLts]
		  ,[LiabilityAccountNumber]
		  ,[ConceptAmount]
		  ,[ServiceCode]
		  ,[TotalRegTicket]
		  ,[AirlineCode])
	SELECT [NationalJetFuelTicketID]
		  ,[subtotalRound]
		  ,( (Row_number() OVER (PARTITION BY CurrencyCode ORDER BY NationalJetFuelTicketID) ) - 1 ) / @NumRegMax + 1 + CAST(CAST(CurrencyCode AS VARBINARY) AS INT)  [row_iden]
		  ,[DepartureStation]
		  ,[CurrencyCode]
		  ,[EquipmentNumber]
		  ,[FueledQtyLts]
		  ,[LiabilityAccountNumber]
		  ,[ConceptAmount]
		  ,[ServiceCode]
		  ,[TotalRegTicket]
		  ,[AirlineCode] 
	FROM
	(
	SELECT NationalJetFuelTicketID, Cast(MIN(ConceptAmount) AS DECIMAL(18, 2)) as subtotalRound     	  
		  ,MIN(B.DepartureStation) DepartureStation, MIN (A.CurrencyCode) CurrencyCode, MIN(A.EquipmentNumber) EquipmentNumber
		  ,SUM(A.FueledQtyLts) FueledQtyLts, MIN(A.LiabilityAccountNumber) LiabilityAccountNumber, Sum(Cast(ConceptAmount AS DECIMAL(18, 2))) ConceptAmount
		  , MIN(ServiceCode) ServiceCode, Count(DISTINCT NationalJetFuelTicketID) TotalRegTicket, MIN(A.[AirlineCode]) [AirlineCode]
	 FROM [Process].[NationalJetFuelCost] A LEFT JOIN
	  	  [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
	WHERE (([DepartureDate] BETWEEN @StartDateReal AND @EndDateReal + '' 23:59:59'') AND  A.PolicyID IS NULL) 
	  AND  A.[AirlineCode] = @AirlineCode
	  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'','')) 
	  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'','')) 
	  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))
    GROUP BY [NationalJetFuelTicketID]
	UNION ALL
   SELECT NationalJetFuelTicketID, Cast(MIN(ConceptAmount) AS DECIMAL(18, 2)) as subtotalRound     	  
		  ,MIN(B.DepartureStation) DepartureStation, MIN (A.CurrencyCode) CurrencyCode, MIN(A.EquipmentNumber) EquipmentNumber
		  ,SUM(A.FueledQtyLts) FueledQtyLts, MIN(A.LiabilityAccountNumber) LiabilityAccountNumber, Sum(Cast(ConceptAmount AS DECIMAL(18, 2))) ConceptAmount
		  , MIN(ServiceCode) ServiceCode, Count(DISTINCT NationalJetFuelTicketID) TotalRegTicket, MIN(A.[AirlineCode]) [AirlineCode]
	 FROM [Process].[NationalJetFuelCost] A LEFT JOIN
	  	  [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
	WHERE [DepartureDate] BETWEEN @StartDateComp AND @EndDateComp + '' 23:59:59''
	  AND  A.[AirlineCode] = @AirlineCode
	  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'','')) 
	  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'','')) 
	  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))
    GROUP BY [NationalJetFuelTicketID]
	) A;
	

	--Paso 4 Guardar OrdersPROD detalle final de registros NEWBS 50
	WITH OrdersPROD ( tprec, bldat, blart, bukrs, budat, waers, kursf, xblnr, bktxt, 
		 newbs, newko, newum, newbk, wrbtr, dmbe2, mwskz, xmwst, xstba, gsber, kostl 
		 , aufnr, prctr, fkber, segment, werks, fistl, zfbdt, valut, zuonr, sgtxt, 
		 menge, meins, geber, IDREG, noTotal, row_iden ) 
		 AS (SELECT ''''																						as tprec, 
					Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DateBase, 112) as DATETIME), 120)		as bldat, 
					''PV''																					as blart, 
					(SELECT MIN(CompanyCode)
								FROM   [Airport].[Airline] B 
								WHERE  [AirlineCode] = MIN(A.[AirlineCode]))									as bukrs, 
					Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DatePosting, 112) AS DATETIME), 120)	as budat, 
					MIN(CurrencyCode)																		as waers, 
					''''																						as kursf, 
					''''																						as xblnr, 
					@HeaderText + '' ''  + 
					Cast(SUM(FueledQtyLts) as VARCHAR) + '' ''												as bktxt,
					 50																						as newbs, 
					@Liability																				as newko,
					''''																						as newum, 
					''''																						as newbk, 
					Cast(Cast(SUM(ConceptAmount) AS DECIMAL(18, 2)) as DECIMAL(18, 2))						as wrbtr, 
					''''																						as dmbe2, 
					''''																						as mwskz, 
					''''																						as xmwst, 
					''''																						as xstba, 
					''''																						as gsber, 
					''''																						as kostl, 
					''''																						as aufnr, 
					''''																						as prctr, 
					''''																						as fkber, 
					''''																						as segment, 
					''''																						as werks, 
					''''																						as fistl, 
					''''																						as zfbdt, -- solo en totales    
					''''																						as valut, 
					''''																						as zuonr, 
					CASE WHEN (@ItemText = '''' OR @ItemText IS NULL) 
								THEN @HeaderText + '' '' + Cast(SUM(FueledQtyLts) as VARCHAR) + '' '' 
								ELSE @ItemText END															as sgtxt, 
					''''																						as menge, 
					''''																						as meins, 
					''''																						as geber,
					Cast(RIGHT(''000000'' + Cast((SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  
												 WHERE UNORDERED = row_iden) AS VARCHAR), 6)AS VARCHAR) 
					+ Cast(RIGHT(''000000'' + Cast(COUNT([TotalRegTicket])+1 as VARCHAR), 6) AS VARCHAR)      as IDREG, 
					COUNT([TotalRegTicket])																	as noTotal, 
					(SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  WHERE UNORDERED = row_iden) row_iden 
			 FROM   @Acumulado A
			 GROUP  BY row_iden)

       

	--Paso 5 Insertar en resultado (Real y Complementario)
	INSERT INTO [Process].[NationalJetFuelPolicy]
	      ([NationalPolicyID]
	      ,[DocumentNumber]
	      ,[NationalJetFuelTicketID]
	      ,[TPREC]
	      ,[IDREG]
	      ,[BLDAT]
	      ,[BLART]
	      ,[BUKRS]
	      ,[BUDAT]
	      ,[WAERS]
	      ,[KURSF]
	      ,[XBLNR]
	      ,[BKTXT]
	      ,[NEWBS]
	      ,[NEWKO]
	      ,[NEWUM]
	      ,[NEWBK]
	      ,[WRBTR]
	      ,[DMBE2]
	      ,[MWSKZ]
	      ,[XMWST]
	      ,[GSBER]
	      ,[KOSTL]
	      ,[AUFNR]
	      ,[PRCTR]
	      ,[FKBER]
	      ,[SEGMENT]
	      ,[WERKS]
	      ,[FISTL]
	      ,[ZFBDT]
	      ,[VALUT]
	      ,[ZUONR]
	      ,[SGTXT]
	      ,[MENGE]
	      ,[MEINS]
	      ,[GEBER]
	      ,[NOTOTAL])
	-- REGISTROS DE ESTIMADOS  
	SELECT @PolicyID PolicyID
		  ,row_iden
		  ,[JetFuelTicketID]
		  ,[TPREC]
		  ,[IDREG]
		  ,[BLDAT]
		  ,[BLART]
		  ,[BUKRS]
		  ,[BUDAT]
		  ,[WAERS]
		  ,[KURSF]
		  ,[XBLNR]
		  ,[BKTXT]
		  ,[NEWBS]
		  ,[NEWKO]
		  ,[NEWUM]
		  ,[NEWBK]
		  ,[WRBTR]
		  ,[DMBE2]
		  ,[MWSKZ]
		  ,[XMWST]
		  ,[GSBER]
		  ,[KOSTL]
		  ,[AUFNR]
		  ,[PRCTR]
		  ,[FKBER]
		  ,[SEGMENT]
		  ,[WERKS]
		  ,[FISTL]
		  ,[ZFBDT]
		  ,[VALUT]
		  ,[ZUONR]
		  ,[SGTXT]
		  ,[MENGE]
		  ,[MEINS]
		  ,[GEBER]
		  ,[NOTOTAL] 
	FROM (
	SELECT (SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  WHERE UNORDERED = rownumber)						as row_iden, 
		   Cast(RIGHT(''000000'' + Isnull(Cast((SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  WHERE UNORDERED = rownumber) AS VARCHAR), ''''), 6) AS VARCHAR(6)) 
		   + Cast(RIGHT(''000000''+ Isnull(Cast(Row_number() over (PARTITION BY (SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  WHERE UNORDERED = rownumber) ORDER BY [JetFuelTicketID] asc ) AS VARCHAR), ''''), 6) AS VARCHAR(6)) AS IDREG, 
		   ''''                                                       tprec, 
		   bldat                                                    bldat, 
		   blart, 
		   bukrs, 
		   budat, 
		   waers, 
		   kursf, 
		   xblnr, 
		   bktxt, 
		   Cast(newbs as VARCHAR)                                   as newbs, 
		   Cast(newko as VARCHAR)                                   as newko, 
		   newum, 
		   newbk, 
		   Cast(Cast(wrbtr as MONEY) as VARCHAR)                    as wrbtr, 
		   dmbe2, 
		   mwskz, 
		   xmwst, 
		   xstba, 
		   gsber, 
		   kostl, 
		   aufnr, 
		   prctr, 
		   fkber, 
		   segment, 
		   werks, 
		   fistl, 
		   zfbdt, 
		   valut, 
		   zuonr, 
		   sgtxt, 
		   menge, 
		   meins, 
		   geber, 
		   noTotal,
		   CASE WHEN DB = ''R'' THEN [JetFuelTicketID] ELSE NULL END [JetFuelTicketID] 
	from   (SELECT (Row_number() OVER(PARTITION BY waers ORDER BY [JetFuelTicketID] asc) - 1 ) / @NumRegMax + 1 + CAST(CAST(waers AS VARBINARY) AS INT) AS rownumber, * 
			  FROM ((SELECT ''''																						as tprec, 
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DateBase, 112) as DATETIME), 120)			as bldat,                             
						   ''PV''																						as blart, --PV es Provisión
						   (SELECT CompanyCode 
									  FROM   [Airport].[Airline]  
									  WHERE  [AirlineCode] = MIN(A.[AirlineCode]))									as bukrs,                         
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DatePosting, 112) as DATETIME), 120)		as budat,                         
						   MIN(A.CurrencyCode)																					as waers, 
						   ISNULL((SELECT [Rate]  FROM [Finance].[ExchangeRates] WHERE [CurrencyCode] = MIN(A.CurrencyCode) AND [Year] = YEAR(@EndDateReal) AND [Month] = MONTH(@EndDateReal) AND [Status] = 1), 1)				as kursf, 
						   ''''																						as xblnr, 
						   @HeaderText + '' '' + MIN(A.EquipmentNumber) + '' '' + 
						   Cast(SUM(FueledQtyLts) as VARCHAR) + '' '' + MIN(B.DepartureStation)						as bktxt, 
						   40																						as newbs, 
						   @Accounting																				as newko, --50210000 --DUDA Cual Cuenta usaremos?                          
						   ''''																						as newum, 
						   ''''																						as newbk, 
						   Cast(SUM(ConceptAmount) as DECIMAL(18, 2))												as wrbtr, 
						   ''''																						as dmbe2, 
						   ''''																						as mwskz, 
						   ''''																						as xmwst, 
						   ''''																						as xstba, 
						   (SELECT MIN(Division) 
									  FROM   [Airport].[Airline]  
									  WHERE  [AirlineCode] = MIN(A.[AirlineCode]))									as gsber,                          
						   CASE WHEN Upper(Substring(MIN(A.EquipmentNumber), 1, 2)) = Upper(''XA'') 
								Then Substring(MIN(A.EquipmentNumber), 1, 2) + ''-'' + Substring(MIN(A.EquipmentNumber), 3, 10) 
								ELSE MIN(A.EquipmentNumber) END														as kostl,   
						   ''''																						as aufnr, 
						   ''''																						as prctr, 
						   MIN(B.[DepartureStation])																as fkber,                      
						   ''''																						as segment, 
						   ''''																						as werks, 
						   ''''																						as fistl, 
						   ''''																						as zfbdt,                           
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DateValue, 112) as DATETIME), 120)		as valut,                                             
						   Cast(SUM(FueledQtyLts) as VARCHAR)														as zuonr,
						   CASE WHEN (@ItemText = '''' OR @ItemText IS NULL) 
								THEN @HeaderText + '' '' + MIN(A.EquipmentNumber) + '' '' + Cast(SUM(FueledQtyLts) as VARCHAR) + '' '' + MIN(B.DepartureStation)
								ELSE @ItemText END																	as sgtxt, 
						   ''''																						as menge, 
						   ''''																						as meins, 
						   ''''																						as geber, 
						   ''0''																						as noTotal,
						   [NationalJetFuelTicketID]																		as JetFuelTicketID,
						   ''R'' DB
					FROM   [Process].[NationalJetFuelCost] A LEFT JOIN
						   [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
					WHERE  (([DepartureDate] BETWEEN @StartDateReal AND @EndDateReal + '' 23:59:59'') AND  A.PolicyID IS NULL)
					  AND  A.[AirlineCode] = @AirlineCode
					  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'',''))
					  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'','')) 
					  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))
					 GROUP BY [NationalJetFuelTicketID]) 
				UNION ALL
			       (SELECT ''''																						as tprec, 
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DateBase, 112) as DATETIME), 120)			as bldat,                             
						   ''PV''																						as blart, --PV es Provisión
						   (SELECT CompanyCode 
									  FROM   [Airport].[Airline]  
									  WHERE  [AirlineCode] = MIN(A.[AirlineCode]))									as bukrs,                         
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DatePosting, 112) as DATETIME), 120)		as budat,                         
						   MIN(A.CurrencyCode)																					as waers, 
						   ISNULL((SELECT [Rate]  FROM [Finance].[ExchangeRates] WHERE [CurrencyCode] = MIN(A.CurrencyCode) AND [Year] = YEAR(@EndDateReal) AND [Month] = MONTH(@EndDateReal) AND [Status] = 1), 1)				as kursf, 
						   ''''																						as xblnr, 
						   @HeaderText + '' '' + MIN(A.EquipmentNumber) + '' '' + 
						   Cast(SUM(FueledQtyLts) as VARCHAR) + '' '' + MIN(B.DepartureStation)						as bktxt, 
						   40																						as newbs, 
						   @Accounting																				as newko, --50210000 --DUDA Cual Cuenta usaremos?                          
						   ''''																						as newum, 
						   ''''																						as newbk, 
						   Cast(SUM(ConceptAmount) as DECIMAL(18, 2))												as wrbtr, 
						   ''''																						as dmbe2, 
						   ''''																						as mwskz, 
						   ''''																						as xmwst, 
						   ''''																						as xstba, 
						   (SELECT MIN(Division) 
									  FROM   [Airport].[Airline]  
									  WHERE  [AirlineCode] = MIN(A.[AirlineCode]))									as gsber,                          
						  -- CASE WHEN Upper(Substring(MIN(A.EquipmentNumber), 1, 2)) = Upper(''XA'') 
								--Then Substring(MIN(A.EquipmentNumber), 1, 2) + ''-'' + Substring(MIN(A.EquipmentNumber), 3, 10) 
								--ELSE MIN(A.EquipmentNumber) END														as kostl, 
						   LEFT(REPLACE(MIN(A.[EquipmentNumber]), ''-'', '''') + ''0000000000'', 10)						as kostl,
						   ''''																						as aufnr, 
						   ''''																						as prctr, 
						   MIN(B.[DepartureStation])																as fkber,                      
						   ''''																						as segment, 
						   ''''																						as werks, 
						   ''''																						as fistl, 
						   ''''																						as zfbdt,                           
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DateValue, 112) as DATETIME), 120)		as valut,                                             
						   Cast(SUM(FueledQtyLts) as VARCHAR)														as zuonr,
						   CASE WHEN (@ItemText = '''' OR @ItemText IS NULL) 
								THEN @HeaderText + '' '' + MIN(A.EquipmentNumber) + '' '' + Cast(SUM(FueledQtyLts) as VARCHAR) + '' '' + MIN(B.DepartureStation)
								ELSE @ItemText END																	as sgtxt, 
						   ''''																						as menge, 
						   ''''																						as meins, 
						   ''''																						as geber, 
						   ''0''																						as noTotal,
						   [NationalJetFuelTicketID]																		as JetFuelTicketID,
						   ''C'' DB
					FROM   [Process].[NationalJetFuelCost] A LEFT JOIN
						   [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
					WHERE  [DepartureDate] BETWEEN @StartDateComp AND @EndDateComp + '' 23:59:59''
					  AND  A.[AirlineCode] = @AirlineCode
					  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'',''))
					  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'','')) 
					  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))
					 GROUP BY [NationalJetFuelTicketID]) 	)  poliZA ) Detalle		
	UNION ALL 
	SELECT row_iden, 
		   Cast(IDREG AS VARCHAR) IDREG, 
		   ''''tprec, 
		   bldat, 
		   blart, 
		   bukrs, 
		   budat, 
		   waers, 
		   ISNULL((SELECT [Rate]  FROM [Finance].[ExchangeRates] WHERE [CurrencyCode] = waers AND [Year] = YEAR(@EndDateReal) AND [Month] = MONTH(@EndDateReal) AND [Status] = 1), 0)				as kursf,  
		   xblnr, 
		   bktxt, 
		   newbs, 
		   newko, 
		   newum, 
		   newbk, 
		   wrbtr, 
		   dmbe2, 
		   mwskz, 
		   xmwst, 
		   xstba, 
		   gsber, 
		   kostl, 
		   aufnr, 
		   prctr, 
		   fkber, 
		   segment, 
		   werks, 
		   fistl, 
		   zfbdt, 
		   valut, 
		   zuonr, 
		   sgtxt, 
		   menge, 
		   meins, 
		   geber, 
		   CAST(noTotal AS VARCHAR) noTotal,
		   NULL [JetFuelTicketID]
	FROM   OrdersPROD 
	) AS Result
	ORDER  BY row_iden, IDREG, newbs asc 



	--Paso 6 Actualizar tabla de Provision PolizaID con el Identity generado
	UPDATE A
	   SET A.PolicyID = @PolicyID
	  FROM [Process].[NationalJetFuelCost] AS A LEFT JOIN
		   [Itinerary].[Itinerary] AS B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
	 WHERE [DepartureDate] BETWEEN @StartDateReal AND @EndDateReal + '' 23:59:59''
	   AND A.[AirlineCode] = @AirlineCode
	   AND A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'','')) 
	   AND A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'',''))
	   AND B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'','')) 
	   AND A.PolicyID IS NULL
	
	SELECT @PolicyID PolicyID--, '''' ErrorMessage;  

END TRY

BEGIN CATCH 
  
    IF @@TRANCOUNT > 0 
      BEGIN 
          ROLLBACK TRANSACTION 
      END 

	SELECT -1 PolicyID--, ERROR_MESSAGE() ErrorMessage;

END CATCH; 

IF @@TRANCOUNT > 0 
  BEGIN 
      COMMIT TRANSACTION 
  END 

END
' 
END
GO
/****** Object:  StoredProcedure [Process].[SavePolizaProvisionesInt]    Script Date: 29/12/2016 04:40:47 p.m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Process].[SavePolizaProvisionesInt]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
-- ===============================================================
-- Author: Leonardo Eduardo Ramirez		
-- Create date: 20160416
-- Description:	Polizas Save
-- [Process].[SavePolizaProvisionesInt] ''20151201'', ''20151216'', ''20151201'', ''20151215'', ''ok'', ''okasssss'', ''20160101'', ''20160101'', ''20160101'', ''Y4'', ''2400003,0000001,4000'', ''COD-EXT'', ''BOS,ATL,LA,DEN,LAX'', ''usuario''
-- ===============================================================
CREATE PROCEDURE [Process].[SavePolizaProvisionesInt]      
     @StartDateReal AS DATETIME  
    ,@EndDateReal AS DATETIME 
	,@StartDateComp AS DATETIME  
    ,@EndDateComp AS DATETIME   
	,@HeaderText AS VARCHAR(50)
	,@ItemText   AS VARCHAR(50) 
	,@DateValue AS DATETIME  
	,@DatePosting AS DATETIME
	,@DateBase AS DATETIME
	,@AirlineCode AS VARCHAR(3) 
	,@ProviderCodes AS VARCHAR(MAX) 
	,@ServiceCodes AS VARCHAR(MAX) 
	,@StationCodes AS VARCHAR(MAX) --Opcion XML
	,@UserSend AS NVARCHAR(50) = '' ''
AS  
BEGIN  
SET NOCOUNT ON; 

--Declarar Variables tipo tabla y variables para el procedimiento
 DECLARE @Acumulado AS TABLE (
	[JetFuelTicketID] [bigint] NULL,
	[subtotalRound] [decimal](18, 2) NULL,
	[row_iden] [bigint] NULL,
	[DepartureStation] [varchar](3) NULL,
	[CurrencyCode] [varchar](3) NULL,
	[EquipmentNumber] [varchar](12) NULL,
	[FueledQtyGals] [int] NULL,
	[LiabilityAccountNumber] [varchar](8) NULL,
	[ConceptAmount] [decimal](38, 2) NULL,
	[ServiceCode] [varchar](12) NULL,
	[TotalRegTicket] [int] NULL,
	[AirlineCode] [varchar](3) NULL
)
DECLARE @TableOrder AS TABLE (UNORDERED INT, ORDERED INT NULL)
DECLARE @TableMaxReg AS TABLE (row_iden INT, reg INT)
DECLARE @DocumentMax INT
SET @DocumentMax = 1
DECLARE @NumRegMax INT
SET @NumRegMax = 998
DECLARE @PolicyID BIGINT

--Declarar Constantes para el procedimiento
DECLARE @Accounting VARCHAR(17)
SET @Accounting = ''50211000''
DECLARE @Liability VARCHAR(17)
SET @Liability = ''20410111''

--Reinicia proceso
--DELETE  [Process].[JetFuelPolicy]
--DELETE  [Process].[JetFuelPolicyControl] --para solo mantener un reg mientras haya pruebas
--UPDATE [Process].JetFuelProvision
--   SET PolicyID = NULL

BEGIN TRANSACTION; 

BEGIN TRY
	--Paso 0 Orden con base en Ticket y Moneda para Real y Complementario. Se incluye CAST(CAST(waers AS VARBINARY) AS INT)
	INSERT @TableOrder (UNORDERED, ORDERED)
	SELECT DISTINCT (Row_number() OVER(PARTITION BY waers ORDER BY A.JetFuelTicketID asc) - 1 ) / @NumRegMax + 1 + CAST(CAST(waers AS VARBINARY) AS INT) AS rownumber, NULL 
			  FROM (
			       (SELECT MIN(A.CurrencyCode)																		as waers, --DUDA Que claves de moneda usaremos, es importante pues al hacer sumas debe estar unificada la moneda              [CurrencyCode]                       
						   40																						as newbs,
						   JetFuelTicketID
					FROM   [Process].[JetFuelProvision] A LEFT JOIN
						   [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
					WHERE  (([DepartureDate] BETWEEN @StartDateReal AND @EndDateReal + '' 23:59:59'') AND  A.PolicyID IS NULL) 
					  AND  A.[AirlineCode] = @AirlineCode
					  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'',''))
					  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'',''))
					  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))				  
					GROUP  BY JetFuelTicketID) 
				   UNION ALL
				   (SELECT MIN(A.CurrencyCode)																		as waers, --DUDA Que claves de moneda usaremos, es importante pues al hacer sumas debe estar unificada la moneda              [CurrencyCode]                       
						   40																						as newbs,
						   JetFuelTicketID
					FROM   [Process].[JetFuelProvision] A LEFT JOIN
						   [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
					WHERE  [DepartureDate] BETWEEN @StartDateComp AND @EndDateComp + '' 23:59:59''
					  AND  A.[AirlineCode] = @AirlineCode
					  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'',''))
					  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'',''))
					  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))				  
					GROUP  BY JetFuelTicketID) 					
					) A	

	--Paso 1 Agregar registro en Poliza Control con filtros realizados y guardar información PolicyID
	INSERT INTO [Process].[JetFuelPolicyControl] 
		  ([CreationDate]
		  ,[Status]
		  ,[AirlineCode]
		  ,[ServiceCodes]
		  ,[ProviderCodes]
		  ,[AirportCodes]
		  ,[StartDateReal]
		  ,[EndDateReal]
		  ,[StartDateComplementary]
		  ,[EndDateComplementary]
		  ,[DateBaseline]
		  ,[DateValue]
		  ,[DatePosting]
		  ,[HeaderText]
		  ,[ItemText]
		  ,[SendByUserName])
	SELECT GETDATE()
		  ,''CREATED''
		  ,@AirlineCode
		  ,@ServiceCodes
		  ,@ProviderCodes
		  ,@StationCodes
		  ,@StartDateReal
		  ,@EndDateReal
		  ,@StartDateComp
		  ,@EndDateComp
		  ,@DateBase
		  ,@DateValue
		  ,@DatePosting
		  ,@HeaderText
		  ,@ItemText
		  ,@UserSend

	SET @PolicyID= @@IDENTITY


	--Paso 2.0 Guardar Acumulado Real y Complementario, registros NEWBS 50. Realiza Suma de galones y Tarifas 
INSERT INTO @Acumulado ([JetFuelTicketID]
		  ,[subtotalRound]
		  ,[row_iden]
		  ,[DepartureStation]
		  ,[CurrencyCode]
		  ,[EquipmentNumber]
		  ,[FueledQtyGals]
		  ,[LiabilityAccountNumber]
		  ,[ConceptAmount]
		  ,[ServiceCode]
		  ,[TotalRegTicket]
		  ,[AirlineCode])
	SELECT [JetFuelTicketID]
		  ,[subtotalRound]
		  ,( (Row_number() OVER (PARTITION BY CurrencyCode ORDER BY JetFuelTicketID) ) - 1 ) / @NumRegMax + 1 + CAST(CAST(CurrencyCode AS VARBINARY) AS INT)  [row_iden]
		  ,[DepartureStation]
		  ,[CurrencyCode]
		  ,[EquipmentNumber]
		  ,[FueledQtyGals]
		  ,[LiabilityAccountNumber]
		  ,[ConceptAmount]
		  ,[ServiceCode]
		  ,[TotalRegTicket]
		  ,[AirlineCode] 
	FROM
	(
	SELECT JetFuelTicketID, Cast(MIN(ConceptAmount) AS DECIMAL(18, 2)) as subtotalRound     	  
		  ,MIN(B.DepartureStation) DepartureStation, MIN (A.CurrencyCode) CurrencyCode, MIN(A.EquipmentNumber) EquipmentNumber
		  ,SUM(A.FueledQtyGals) FueledQtyGals, MIN(A.LiabilityAccountNumber) LiabilityAccountNumber, Sum(Cast(ConceptAmount AS DECIMAL(18, 2))) ConceptAmount
		  , MIN(ServiceCode) ServiceCode, Count(DISTINCT JetFuelTicketID) TotalRegTicket, MIN(A.[AirlineCode]) [AirlineCode]
	 FROM [Process].[JetFuelProvision] A LEFT JOIN
	  	  [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
	WHERE (([DepartureDate] BETWEEN @StartDateReal AND @EndDateReal + '' 23:59:59'') AND  A.PolicyID IS NULL) 
	  AND  A.[AirlineCode] = @AirlineCode
	  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'','')) 
	  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'','')) 
	  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))
    GROUP BY [JetFuelTicketID]
	UNION ALL
   SELECT JetFuelTicketID, Cast(MIN(ConceptAmount) AS DECIMAL(18, 2)) as subtotalRound     	  
		  ,MIN(B.DepartureStation) DepartureStation, MIN (A.CurrencyCode) CurrencyCode, MIN(A.EquipmentNumber) EquipmentNumber
		  ,SUM(A.FueledQtyGals) FueledQtyGals, MIN(A.LiabilityAccountNumber) LiabilityAccountNumber, Sum(Cast(ConceptAmount AS DECIMAL(18, 2))) ConceptAmount
		  , MIN(ServiceCode) ServiceCode, Count(DISTINCT JetFuelTicketID) TotalRegTicket, MIN(A.[AirlineCode]) [AirlineCode]
	 FROM [Process].[JetFuelProvision] A LEFT JOIN
	  	  [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
	WHERE [DepartureDate] BETWEEN @StartDateComp AND @EndDateComp + '' 23:59:59''
	  AND  A.[AirlineCode] = @AirlineCode
	  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'','')) 
	  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'','')) 
	  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))
    GROUP BY [JetFuelTicketID]
	) A;
	


	--Paso 3 Guardar Documento y Registro de Real para determinar posteriormente el registro consecuente por documento en Complementario
	--INSERT INTO @TableMaxReg (row_iden, reg)
	--SELECT (SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  WHERE UNORDERED = rownumber)						as row_iden, 
	--	   Row_number() over (PARTITION BY (SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  WHERE UNORDERED = rownumber) ORDER BY newbs asc) as reg       
	--from   (SELECT (Row_number() OVER(PARTITION BY waers ORDER BY newbs asc) - 1 ) / @NumRegMax + 1 + CAST(CAST(waers AS VARBINARY) AS INT) AS rownumber,*
	--		  FROM (SELECT MIN(A.CurrencyCode)			as waers,                        
	--					   40							as newbs                       
	--				FROM   [Process].[JetFuelProvision] A LEFT JOIN
	--					   [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
	--				WHERE  [DepartureDate] BETWEEN @StartDateReal AND @EndDateReal
	--				  AND  A.[AirlineCode] = @AirlineCode
	--				  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'',''))
	--				  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'','')) 
	--				  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))
	--				  AND  A.PolicyID IS NULL
	--				 GROUP BY JetFuelTicketID) NewDoc) polMax;							 				 			 	 	  

	--Paso 4 Guardar OrdersPROD detalle final de registros NEWBS 50
	WITH OrdersPROD ( tprec, bldat, blart, bukrs, budat, waers, kursf, xblnr, bktxt, 
		 newbs, newko, newum, newbk, wrbtr, dmbe2, mwskz, xmwst, xstba, gsber, kostl 
		 , aufnr, prctr, fkber, segment, werks, fistl, zfbdt, valut, zuonr, sgtxt, 
		 menge, meins, geber, IDREG, noTotal, row_iden ) 
		 AS (SELECT ''''																						as tprec, 
					Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DateBase, 112) as DATETIME), 120)		as bldat, 
					''PV''																					as blart, 
					(SELECT MIN(CompanyCode)
								FROM   [Airport].[Airline] B 
								WHERE  [AirlineCode] = MIN(A.[AirlineCode]))									as bukrs, 
					Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DatePosting, 112) AS DATETIME), 120)	as budat, 
					MIN(CurrencyCode)																		as waers, 
					''''																						as kursf, 
					''''																						as xblnr, 
					@HeaderText + '' ''  + 
					Cast(SUM(FueledQtyGals) as VARCHAR) + '' ''												as bktxt,
					 50																						as newbs, 
					@Liability																				as newko,
					''''																						as newum, 
					''''																						as newbk, 
					Cast(Cast(SUM(ConceptAmount) AS DECIMAL(18, 2)) as DECIMAL(18, 2))						as wrbtr, 
					''''																						as dmbe2, 
					''''																						as mwskz, 
					''''																						as xmwst, 
					''''																						as xstba, 
					''''																						as gsber, 
					''''																						as kostl, 
					''''																						as aufnr, 
					''''																						as prctr, 
					''''																						as fkber, 
					''''																						as segment, 
					''''																						as werks, 
					''''																						as fistl, 
					''''																						as zfbdt, -- solo en totales    
					''''																						as valut, 
					''''																						as zuonr, 
					CASE WHEN (@ItemText = '''' OR @ItemText IS NULL) 
								THEN @HeaderText + '' '' + Cast(SUM(FueledQtyGals) as VARCHAR) + '' '' 
								ELSE @ItemText END															as sgtxt, 
					''''																						as menge, 
					''''																						as meins, 
					''''																						as geber,
					Cast(RIGHT(''000000'' + Cast((SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  
												 WHERE UNORDERED = row_iden) AS VARCHAR), 6)AS VARCHAR) 
					+ Cast(RIGHT(''000000'' + Cast(COUNT([TotalRegTicket])+1 as VARCHAR), 6) AS VARCHAR)      as IDREG, 
					COUNT([TotalRegTicket])																	as noTotal, 
					(SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  WHERE UNORDERED = row_iden) row_iden 
			 FROM   @Acumulado A
			 GROUP  BY row_iden)

       

	--Paso 5 Insertar en resultado (Real y Complementario)
	INSERT INTO [Process].[JetFuelPolicy]
	      ([PolicyID]
	      ,[DocumentNumber]
	      ,[JetFuelTicketID]
	      ,[TPREC]
	      ,[IDREG]
	      ,[BLDAT]
	      ,[BLART]
	      ,[BUKRS]
	      ,[BUDAT]
	      ,[WAERS]
	      ,[KURSF]
	      ,[XBLNR]
	      ,[BKTXT]
	      ,[NEWBS]
	      ,[NEWKO]
	      ,[NEWUM]
	      ,[NEWBK]
	      ,[WRBTR]
	      ,[DMBE2]
	      ,[MWSKZ]
	      ,[XMWST]
	      ,[GSBER]
	      ,[KOSTL]
	      ,[AUFNR]
	      ,[PRCTR]
	      ,[FKBER]
	      ,[SEGMENT]
	      ,[WERKS]
	      ,[FISTL]
	      ,[ZFBDT]
	      ,[VALUT]
	      ,[ZUONR]
	      ,[SGTXT]
	      ,[MENGE]
	      ,[MEINS]
	      ,[GEBER]
	      ,[NOTOTAL])
	-- REGISTROS DE ESTIMADOS  
	SELECT @PolicyID PolicyID
		  ,row_iden
		  ,[JetFuelTicketID]
		  ,[TPREC]
		  ,[IDREG]
		  ,[BLDAT]
		  ,[BLART]
		  ,[BUKRS]
		  ,[BUDAT]
		  ,[WAERS]
		  ,[KURSF]
		  ,[XBLNR]
		  ,[BKTXT]
		  ,[NEWBS]
		  ,[NEWKO]
		  ,[NEWUM]
		  ,[NEWBK]
		  ,[WRBTR]
		  ,[DMBE2]
		  ,[MWSKZ]
		  ,[XMWST]
		  ,[GSBER]
		  ,[KOSTL]
		  ,[AUFNR]
		  ,[PRCTR]
		  ,[FKBER]
		  ,[SEGMENT]
		  ,[WERKS]
		  ,[FISTL]
		  ,[ZFBDT]
		  ,[VALUT]
		  ,[ZUONR]
		  ,[SGTXT]
		  ,[MENGE]
		  ,[MEINS]
		  ,[GEBER]
		  ,[NOTOTAL] 
	FROM (
	SELECT (SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  WHERE UNORDERED = rownumber)						as row_iden, 
		   Cast(RIGHT(''000000'' + Isnull(Cast((SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  WHERE UNORDERED = rownumber) AS VARCHAR), ''''), 6) AS VARCHAR(6)) 
		   + Cast(RIGHT(''000000''+ Isnull(Cast(Row_number() over (PARTITION BY (SELECT ORDERED FROM (SELECT UNORDERED, ROW_NUMBER() OVER(ORDER BY UNORDERED) ORDERED FROM @TableOrder) O  WHERE UNORDERED = rownumber) ORDER BY [JetFuelTicketID] asc ) AS VARCHAR), ''''), 6) AS VARCHAR(6)) AS IDREG, 
		   ''''                                                       tprec, 
		   bldat                                                    bldat, 
		   blart, 
		   bukrs, 
		   budat, 
		   waers, 
		   kursf, 
		   xblnr, 
		   bktxt, 
		   Cast(newbs as VARCHAR)                                   as newbs, 
		   Cast(newko as VARCHAR)                                   as newko, 
		   newum, 
		   newbk, 
		   Cast(Cast(wrbtr as MONEY) as VARCHAR)                    as wrbtr, 
		   dmbe2, 
		   mwskz, 
		   xmwst, 
		   xstba, 
		   gsber, 
		   kostl, 
		   aufnr, 
		   prctr, 
		   fkber, 
		   segment, 
		   werks, 
		   fistl, 
		   zfbdt, 
		   valut, 
		   zuonr, 
		   sgtxt, 
		   menge, 
		   meins, 
		   geber, 
		   noTotal,
		   CASE WHEN DB = ''R'' THEN [JetFuelTicketID] ELSE NULL END [JetFuelTicketID] 
	from   (SELECT (Row_number() OVER(PARTITION BY waers ORDER BY [JetFuelTicketID] asc) - 1 ) / @NumRegMax + 1 + CAST(CAST(waers AS VARBINARY) AS INT) AS rownumber, * 
			  FROM ((SELECT ''''																						as tprec, 
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DateBase, 112) as DATETIME), 120)			as bldat,                             
						   ''PV''																						as blart, --PV es Provisión
						   (SELECT CompanyCode 
									  FROM   [Airport].[Airline]  
									  WHERE  [AirlineCode] = MIN(A.[AirlineCode]))									as bukrs,                         
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DatePosting, 112) as DATETIME), 120)		as budat,                         
						   MIN(A.CurrencyCode)																					as waers, 
						   ISNULL((SELECT [Rate]  FROM [Finance].[ExchangeRates] WHERE [CurrencyCode] = MIN(A.CurrencyCode) AND [Year] = YEAR(@EndDateReal) AND [Month] = MONTH(@EndDateReal) AND [Status] = 1), 1)				as kursf, 
						   ''''																						as xblnr, 
						   @HeaderText + '' '' + MIN(A.EquipmentNumber) + '' '' + 
						   Cast(SUM(FueledQtyGals) as VARCHAR) + '' '' + MIN(B.DepartureStation)						as bktxt, 
						   40																						as newbs, 
						   @Accounting																				as newko, --50210000 --DUDA Cual Cuenta usaremos?                          
						   ''''																						as newum, 
						   ''''																						as newbk, 
						   Cast(SUM(ConceptAmount) as DECIMAL(18, 2))												as wrbtr, 
						   ''''																						as dmbe2, 
						   ''''																						as mwskz, 
						   ''''																						as xmwst, 
						   ''''																						as xstba, 
						   (SELECT MIN(Division) 
									  FROM   [Airport].[Airline]  
									  WHERE  [AirlineCode] = MIN(A.[AirlineCode]))									as gsber,                          
						  -- CASE WHEN Upper(Substring(MIN(A.EquipmentNumber), 1, 2)) = Upper(''XA'') 
								--Then Substring(MIN(A.EquipmentNumber), 1, 2) + ''-'' + Substring(MIN(A.EquipmentNumber), 3, 10) 
								--ELSE MIN(A.EquipmentNumber) END														as kostl,
						   
						   LEFT(REPLACE(MIN(A.[EquipmentNumber]), ''-'', '''') + ''0000000000'', 10)						as kostl,   
						   ''''																						as aufnr, 
						   ''''																						as prctr, 
						   MIN(B.[DepartureStation])																as fkber,                      
						   ''''																						as segment, 
						   ''''																						as werks, 
						   ''''																						as fistl, 
						   ''''																						as zfbdt,                           
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DateValue, 112) as DATETIME), 120)		as valut,                                             
						   Cast(SUM(FueledQtyGals) as VARCHAR)														as zuonr,
						   CASE WHEN (@ItemText = '''' OR @ItemText IS NULL) 
								THEN @HeaderText + '' '' + MIN(A.EquipmentNumber) + '' '' + Cast(SUM(FueledQtyGals) as VARCHAR) + '' '' + MIN(B.DepartureStation)
								ELSE @ItemText END																	as sgtxt, 
						   ''''																						as menge, 
						   ''''																						as meins, 
						   ''''																						as geber, 
						   ''0''																						as noTotal,
						   [JetFuelTicketID]																		as JetFuelTicketID,
						   ''R'' DB
					FROM   [Process].[JetFuelProvision] A LEFT JOIN
						   [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
					WHERE  (([DepartureDate] BETWEEN @StartDateReal AND @EndDateReal + '' 23:59:59'') AND  A.PolicyID IS NULL)
					  AND  A.[AirlineCode] = @AirlineCode
					  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'',''))
					  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'','')) 
					  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))
					 GROUP BY JetFuelTicketID) 
				UNION ALL
			       (SELECT ''''																						as tprec, 
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DateBase, 112) as DATETIME), 120)			as bldat,                             
						   ''PV''																						as blart, --PV es Provisión
						   (SELECT CompanyCode 
									  FROM   [Airport].[Airline]  
									  WHERE  [AirlineCode] = MIN(A.[AirlineCode]))									as bukrs,                         
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DatePosting, 112) as DATETIME), 120)		as budat,                         
						   MIN(A.CurrencyCode)																					as waers, 
						   ISNULL((SELECT [Rate]  FROM [Finance].[ExchangeRates] WHERE [CurrencyCode] = MIN(A.CurrencyCode) AND [Year] = YEAR(@EndDateReal) AND [Month] = MONTH(@EndDateReal) AND [Status] = 1), 1)				as kursf, 
						   ''''																						as xblnr, 
						   @HeaderText + '' '' + MIN(A.EquipmentNumber) + '' '' + 
						   Cast(SUM(FueledQtyGals) as VARCHAR) + '' '' + MIN(B.DepartureStation)						as bktxt, 
						   40																						as newbs, 
						   @Accounting																				as newko, --50210000 --DUDA Cual Cuenta usaremos?                          
						   ''''																						as newum, 
						   ''''																						as newbk, 
						   Cast(SUM(ConceptAmount) as DECIMAL(18, 2))												as wrbtr, 
						   ''''																						as dmbe2, 
						   ''''																						as mwskz, 
						   ''''																						as xmwst, 
						   ''''																						as xstba, 
						   (SELECT MIN(Division) 
									  FROM   [Airport].[Airline]  
									  WHERE  [AirlineCode] = MIN(A.[AirlineCode]))									as gsber,                          
						   CASE WHEN Upper(Substring(MIN(A.EquipmentNumber), 1, 2)) = Upper(''XA'') 
								Then Substring(MIN(A.EquipmentNumber), 1, 2) + ''-'' + Substring(MIN(A.EquipmentNumber), 3, 10) 
								ELSE MIN(A.EquipmentNumber) END														as kostl,   
						   ''''																						as aufnr, 
						   ''''																						as prctr, 
						   MIN(B.[DepartureStation])																as fkber,                      
						   ''''																						as segment, 
						   ''''																						as werks, 
						   ''''																						as fistl, 
						   ''''																						as zfbdt,                           
						   Convert(VARCHAR(10), Cast(Convert(VARCHAR(10), @DateValue, 112) as DATETIME), 120)		as valut,                                             
						   Cast(SUM(FueledQtyGals) as VARCHAR)														as zuonr,
						   CASE WHEN (@ItemText = '''' OR @ItemText IS NULL) 
								THEN @HeaderText + '' '' + MIN(A.EquipmentNumber) + '' '' + Cast(SUM(FueledQtyGals) as VARCHAR) + '' '' + MIN(B.DepartureStation)
								ELSE @ItemText END																	as sgtxt, 
						   ''''																						as menge, 
						   ''''																						as meins, 
						   ''''																						as geber, 
						   ''0''																						as noTotal,
						   [JetFuelTicketID]																		as JetFuelTicketID,
						   ''C'' DB
					FROM   [Process].[JetFuelProvision] A LEFT JOIN
						   [Itinerary].[Itinerary] B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
					WHERE  [DepartureDate] BETWEEN @StartDateComp AND @EndDateComp + '' 23:59:59''
					  AND  A.[AirlineCode] = @AirlineCode
					  AND  A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'',''))
					  AND  A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'','')) 
					  AND  B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'',''))
					 GROUP BY JetFuelTicketID) 	)  poliZA ) Detalle		
	UNION ALL 
	SELECT row_iden, 
		   Cast(IDREG AS VARCHAR) IDREG, 
		   ''''tprec, 
		   bldat, 
		   blart, 
		   bukrs, 
		   budat, 
		   waers, 
		   ISNULL((SELECT [Rate]  FROM [Finance].[ExchangeRates] WHERE [CurrencyCode] = waers AND [Year] = YEAR(@EndDateReal) AND [Month] = MONTH(@EndDateReal) AND [Status] = 1), 0)				as kursf,  
		   xblnr, 
		   bktxt, 
		   newbs, 
		   newko, 
		   newum, 
		   newbk, 
		   wrbtr, 
		   dmbe2, 
		   mwskz, 
		   xmwst, 
		   xstba, 
		   gsber, 
		   kostl, 
		   aufnr, 
		   prctr, 
		   fkber, 
		   segment, 
		   werks, 
		   fistl, 
		   zfbdt, 
		   valut, 
		   zuonr, 
		   sgtxt, 
		   menge, 
		   meins, 
		   geber, 
		   CAST(noTotal AS VARCHAR) noTotal,
		   NULL [JetFuelTicketID]
	FROM   OrdersPROD 
	) AS Result
	ORDER  BY row_iden, IDREG, newbs asc 



	--Paso 6 Actualizar tabla de Provision PolizaID con el Identity generado
	UPDATE A
	   SET A.PolicyID = @PolicyID
	  FROM [Process].[JetFuelProvision] AS A LEFT JOIN
		   [Itinerary].[Itinerary] AS B ON (A.Sequence = B.Sequence AND A.AirlineCode = B.AirlineCode AND A.FlightNumber = B.FlightNumber AND  A.ItineraryKey = B.ItineraryKey)
	 WHERE [DepartureDate] BETWEEN @StartDateReal AND @EndDateReal + '' 23:59:59''
	   AND A.[AirlineCode] = @AirlineCode
	   AND A.ProviderNumberPrimary IN (select splitdata from dbo.fnSplitString(@ProviderCodes,'','')) 
	   AND A.ServiceCode IN (select splitdata from dbo.fnSplitString(@ServiceCodes,'',''))
	   AND B.DepartureStation IN (select splitdata from dbo.fnSplitString(@StationCodes,'','')) 
	   AND A.PolicyID IS NULL
	
	SELECT @PolicyID PolicyID--, '''' ErrorMessage;  

END TRY

BEGIN CATCH 
  
    IF @@TRANCOUNT > 0 
      BEGIN 
          ROLLBACK TRANSACTION 
      END 

	SELECT -1 PolicyID--, ERROR_MESSAGE() ErrorMessage;

END CATCH; 

IF @@TRANCOUNT > 0 
  BEGIN 
      COMMIT TRANSACTION 
  END 

END
' 
END
GO


IF exists
(
SELECT *
FROM INFORMATION_SCHEMA.COLUMNS
WHERE COLUMN_NAME = 'BusinessName' AND TABLE_NAME = 'Airline'
)
BEGIN
UPDATE Airport.Airline
	SET BusinessName = 'Concesionaria Vuela Compañía de Aviación, S.A.P.I. de C.V.'
	WHERE AirlineCode = 'Y4'
END

GO