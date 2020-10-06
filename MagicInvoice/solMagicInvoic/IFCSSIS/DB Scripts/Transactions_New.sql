CREATE TABLE [card].[Transactions_New] (
    [ID]                    INT             IDENTITY (1, 1) NOT NULL,
    [CustomerNo]            VARCHAR (50)    NULL,
    [InvoiceNo]             VARCHAR (50)    NULL,
    [CardConsecutiveNumber] VARCHAR (10)    NULL,	
    [PurchaseDate]          VARCHAR (20)    NULL,
    [PurchaseTime]          VARCHAR (8)     NULL,
    [ReceiptNo]             VARCHAR (50)    NULL,
    [StationID]             VARCHAR (50)    NULL,
    [ProductGroupID]        VARCHAR (10)    NULL,
    [Quantity]              DECIMAL (14, 6) NULL,
    [Price]                 DECIMAL (15, 6) NULL,
    [Gross]                 DECIMAL (15, 6) NULL,
    [Discount]              DECIMAL (15, 6) NULL,
    [Net]                   DECIMAL (15, 6) NULL,
    [VAT]                   DECIMAL (15, 6) NULL,
    [PreviousKM]            VARCHAR (50)    NULL,
    [CurrentKM]             VARCHAR (50)    NULL,
    [InformationValue]      VARCHAR (50)    NULL,
    [CO2Emission]           DECIMAL (18, 2) NULL,
    [RenewableFuelQty]      DECIMAL (18, 2) NULL,
    [TransactionDate]		DATETIME2		NULL, 
	[InternalNumber]		varchar(50)		NULL,
	[SecondaryCardConsecutiveNumber] VARCHAR (10)    NULL,
	[SecondaryCustomerNo]            VARCHAR (50)    NULL,
	[StationAddress] NVARCHAR(255) NULL, 
	[StationCity] NVARCHAR(255) NULL, 
	[StationLatitude] DECIMAL(28, 10) NULL, 
	[StationLongitude] DECIMAL(28, 10) NULL, 
	[StationName] NVARCHAR(255) NULL,
	[StationTypeName] NVARCHAR(255) NULL, 
	[StationOpen24Hours] NVARCHAR(50) NULL, 
	[StationPrimaryContactName] NVARCHAR(255) NULL, 
	[StationPrimaryContactEmailAddress] NVARCHAR(255) NULL, 
	[StationPrimaryContactTelephone] NVARCHAR(255) NULL,
  	[IsStationUpdated] bit CONSTRAINT [DF_transaction_IsStationUpdated] DEFAULT ((0)) NOT NULL,
	[ParkingTransactionId] INT CONSTRAINT [DF_Transactions_New_ParkingTransactionId]  DEFAULT ((0)) NOT NULL, 
    CONSTRAINT [PK_Transactions_New2] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO

CREATE NONCLUSTERED INDEX [transaction_customernumber] ON [card].[Transactions_New]
(
	[TransactionDate] DESC,
	[CustomerNo] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX [transaction_TransactionDate] ON [card].[Transactions_New]
(
	[TransactionDate] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [ix_Transactions_New_IN] ON [card].[Transactions_New]
(
	[InternalNumber] ASC
)
INCLUDE ( 	[CustomerNo]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO