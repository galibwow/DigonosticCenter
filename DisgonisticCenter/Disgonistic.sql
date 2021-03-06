USE [master]
GO
/****** Object:  Database [DiagnosticCenter]    Script Date: 9/10/2016 2:31:27 AM ******/
CREATE DATABASE [DiagnosticCenter]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DiagnosticCenterDBFinal', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DiagnosticCenterDBFinal .mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DiagnosticCenterDBFinal _log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\DiagnosticCenterDBFinal _log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DiagnosticCenter] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DiagnosticCenter].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DiagnosticCenter] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET ANSI_PADDING ON 
GO
ALTER DATABASE [DiagnosticCenter] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET ARITHABORT OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DiagnosticCenter] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DiagnosticCenter] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DiagnosticCenter] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DiagnosticCenter] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET RECOVERY FULL 
GO
ALTER DATABASE [DiagnosticCenter] SET  MULTI_USER 
GO
ALTER DATABASE [DiagnosticCenter] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DiagnosticCenter] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DiagnosticCenter] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DiagnosticCenter] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [DiagnosticCenter] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DiagnosticCenter', N'ON'
GO
USE [DiagnosticCenter]
GO
/****** Object:  Table [dbo].[PatientTest]    Script Date: 9/10/2016 2:31:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PatientTest](
	[TestReqid] [int] NOT NULL,
	[TestId] [int] NOT NULL,
	[RequestDate] [date] NULL,
 CONSTRAINT [PK_PatientTests_1] PRIMARY KEY CLUSTERED 
(
	[TestReqid] ASC,
	[TestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TestRequestEntry]    Script Date: 9/10/2016 2:31:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TestRequestEntry](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameOfPatient] [nvarchar](50) NULL,
	[DOB] [date] NULL,
	[BillNo]  AS ('B-'+right('0000'+CONVERT([varchar](5),[Id]),(5))) PERSISTED,
	[MobileNo] [varchar](50) NULL,
	[TotalAmount] [decimal](18, 0) NULL,
	[DueDate] [date] NULL,
	[PaymentStatus] [decimal](18, 0) NULL CONSTRAINT [DF_PaymentStatus]  DEFAULT (N'False'),
	[PaidAmount] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[TestSetup]    Script Date: 9/10/2016 2:31:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TestSetup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestName] [varchar](max) NULL,
	[Fee] [decimal](18, 0) NULL,
	[TestTypeId] [int] NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table [dbo].[TestType]    Script Date: 9/10/2016 2:31:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TestType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestType] [varchar](50) NULL,
 CONSTRAINT [PK_TestType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
/****** Object:  View [dbo].[DateWiseTestsReport]    Script Date: 9/10/2016 2:31:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[DateWiseTestsReport]

as 

SELECT  t.Id,t.TestName, t.Fee, count(t.Id) as TestCount,count(t.Id)*t.Fee AS TotalFee, pt.RequestDate FROM TestSetup T
left JOIN PatientTest pt
ON t.Id = pt.[TestId] 
group by t.TestName, t.Fee,pt.RequestDate,t.Id



GO
/****** Object:  View [dbo].[DateWiseTestTypesReport]    Script Date: 9/10/2016 2:31:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[DateWiseTestTypesReport]
AS
SELECT        tt.TestType, t.Fee, COUNT(t.Id) AS TestCount, COUNT(t.Id) * t.Fee AS TotalFee, pt.RequestDate
FROM            dbo.TestType AS tt FULL OUTER JOIN
                         dbo.TestSetup AS t ON tt.Id = t.TestTypeId FULL OUTER JOIN
                         dbo.PatientTest AS pt ON t.Id = pt.TestId
GROUP BY t.TestName, t.Fee, pt.RequestDate, tt.TestType

GO
/****** Object:  View [dbo].[SearchView]    Script Date: 9/10/2016 2:31:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[SearchView]
AS
SELECT        dbo.TestRequestEntry.NameOfPatient, dbo.TestRequestEntry.BillNo, dbo.TestRequestEntry.MobileNo, dbo.TestSetup.TestName, dbo.TestSetup.Fee, dbo.TestRequestEntry.TotalAmount, dbo.TestRequestEntry.Id, 
                         dbo.TestSetup.Id AS Expr1, dbo.TestRequestEntry.PaymentStatus
FROM            dbo.TestRequestEntry INNER JOIN
                         dbo.TestSetup ON dbo.TestRequestEntry.Id = dbo.TestSetup.Id

GO
/****** Object:  View [dbo].[TestVM]    Script Date: 9/10/2016 2:31:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view [dbo].[TestVM] As select TestName,fee,TestType from TestSetup ts join TestType tt on ts.TestTypeId = tt.Id
GO
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (1, 3, CAST(N'2016-05-31' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (2, 3, CAST(N'2016-05-31' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3, 1, CAST(N'2016-05-31' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3, 2, CAST(N'2016-05-31' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3, 3, CAST(N'2016-05-31' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (4, 1, CAST(N'2016-05-31' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (4, 2, CAST(N'2016-05-31' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (4, 3, CAST(N'2016-05-31' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (5, 3, CAST(N'2016-07-30' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (5, 4, CAST(N'2016-07-30' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (1005, 2, CAST(N'2016-07-30' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (1005, 3, CAST(N'2016-07-30' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (1005, 4, CAST(N'2016-07-30' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (2005, 2, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (2005, 5, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (2006, 3, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (2006, 5, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (2006, 6, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (2011, 1, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (2012, 1007, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3012, 1, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3012, 3010, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3013, 3010, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3013, 3011, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3014, 3010, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3014, 3011, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3015, 3010, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3015, 3011, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3016, 3, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3016, 3011, CAST(N'2016-08-01' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3017, 1, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3017, 3012, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3018, 3012, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3019, 3012, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3020, 3011, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3021, 4, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3021, 3013, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3022, 3013, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3022, 3014, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3023, 3013, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3024, 4, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3024, 3010, CAST(N'2016-08-02' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3025, 3010, CAST(N'2016-08-03' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3026, 1, CAST(N'2016-08-03' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3026, 3012, CAST(N'2016-08-03' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3027, 1, CAST(N'2016-08-03' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3027, 3012, CAST(N'2016-08-03' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3028, 3014, CAST(N'2016-08-03' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3028, 3015, CAST(N'2016-08-03' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3029, 3016, CAST(N'2016-08-03' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (3029, 3017, CAST(N'2016-08-03' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (4027, 1, CAST(N'2016-08-04' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (4028, 3017, CAST(N'2016-08-04' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (5027, 3010, CAST(N'2016-08-17' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (6027, 1, CAST(N'2016-08-21' AS Date))
INSERT [dbo].[PatientTest] ([TestReqid], [TestId], [RequestDate]) VALUES (6027, 4, CAST(N'2016-08-21' AS Date))
SET ANSI_PADDING ON
SET IDENTITY_INSERT [dbo].[TestRequestEntry] ON 

INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (1, N'Rakib Hossain', CAST(N'1935-05-11' AS Date), N'01700000001', CAST(400 AS Decimal(18, 0)), CAST(N'2016-05-31' AS Date), CAST(0 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (2, N'kaise', CAST(N'2000-05-01' AS Date), N'01700000002', CAST(400 AS Decimal(18, 0)), CAST(N'2016-05-31' AS Date), CAST(10 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3, N'Faizul', CAST(N'1989-07-27' AS Date), N'01700000003', CAST(700 AS Decimal(18, 0)), CAST(N'2016-05-31' AS Date), CAST(10 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (4, N'Sharly', CAST(N'1989-07-27' AS Date), N'01700000004', CAST(700 AS Decimal(18, 0)), CAST(N'2016-05-31' AS Date), CAST(10 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (5, N'rakibhossainn', CAST(N'2016-07-05' AS Date), N'444444', CAST(300 AS Decimal(18, 0)), CAST(N'2016-07-30' AS Date), CAST(10 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (1005, N'Asrafss', CAST(N'2016-07-05' AS Date), N'1231', CAST(650 AS Decimal(18, 0)), CAST(N'2016-07-30' AS Date), CAST(10 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (2005, N'anik elHI', CAST(N'2016-08-11' AS Date), N'5455', CAST(6350 AS Decimal(18, 0)), CAST(N'2016-08-01' AS Date), CAST(0 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (2006, N'Mirjaw', CAST(N'2016-08-11' AS Date), N'3311111', CAST(6533 AS Decimal(18, 0)), CAST(N'2016-08-01' AS Date), CAST(6133 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (2011, N'Asraf', CAST(N'2016-08-02' AS Date), N'123456', CAST(150 AS Decimal(18, 0)), CAST(N'2016-08-01' AS Date), CAST(100 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (2012, N'ahmed', CAST(N'2016-08-11' AS Date), N'41221', CAST(44444 AS Decimal(18, 0)), CAST(N'2016-08-01' AS Date), CAST(44200 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3012, N'Alif Alawuddin', CAST(N'2016-08-03' AS Date), N'016621221', CAST(112262 AS Decimal(18, 0)), CAST(N'2016-08-01' AS Date), CAST(0 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3013, N'Mamun vai', CAST(N'2016-08-03' AS Date), N'01717123123', CAST(113112 AS Decimal(18, 0)), CAST(N'2016-08-01' AS Date), CAST(112989 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3014, N'Rakib hossain Galib', CAST(N'2016-08-05' AS Date), N'019910111111', CAST(113112 AS Decimal(18, 0)), CAST(N'2016-08-01' AS Date), CAST(112712 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3015, N'Talukder Md Rakib', CAST(N'1991-11-09' AS Date), N'01717122112', CAST(113112 AS Decimal(18, 0)), CAST(N'2016-08-01' AS Date), CAST(112712 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3016, N'Mawra Hocane', CAST(N'2016-08-01' AS Date), N'0199100200', CAST(1200 AS Decimal(18, 0)), CAST(N'2016-08-01' AS Date), CAST(1000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3017, N'saddam Ahmed', CAST(N'2016-08-03' AS Date), N'1383123123', CAST(5150 AS Decimal(18, 0)), CAST(N'2016-08-02' AS Date), CAST(5150 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3018, N'Alif Alawuddin', CAST(N'2016-08-01' AS Date), N'01919', CAST(17000 AS Decimal(18, 0)), CAST(N'2016-08-02' AS Date), CAST(17000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3019, N'rakib', CAST(N'2016-08-03' AS Date), N'234123213123', CAST(5000 AS Decimal(18, 0)), CAST(N'2016-08-02' AS Date), CAST(5000 AS Decimal(18, 0)), NULL)
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3020, N'q', CAST(N'2016-08-02' AS Date), N'123456123123333', CAST(1000 AS Decimal(18, 0)), CAST(N'2016-08-02' AS Date), CAST(1000 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3021, N'Asraf', CAST(N'2016-08-02' AS Date), N'123456122222222', CAST(544 AS Decimal(18, 0)), CAST(N'2016-08-02' AS Date), CAST(544 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3022, N'asdasdas', CAST(N'2016-08-01' AS Date), N'444444444', CAST(12313656 AS Decimal(18, 0)), CAST(N'2016-08-02' AS Date), CAST(12313656 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3023, N'Rakib', CAST(N'2016-08-10' AS Date), N'9111', CAST(1332 AS Decimal(18, 0)), CAST(N'2016-08-02' AS Date), CAST(1332 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3024, N'hafizur rahman', CAST(N'2016-08-01' AS Date), N'123123211233123', CAST(112212 AS Decimal(18, 0)), CAST(N'2016-08-02' AS Date), CAST(112212 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3025, N'Nijer Hossain', CAST(N'2016-08-01' AS Date), N'18238912313', CAST(224224 AS Decimal(18, 0)), CAST(N'2016-08-03' AS Date), CAST(224224 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3026, N'Arafat Rakib', CAST(N'2016-08-03' AS Date), N'01717199199', CAST(5150 AS Decimal(18, 0)), CAST(N'2016-08-03' AS Date), CAST(5100 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3027, N'Arafat hossain', CAST(N'2016-08-03' AS Date), N'991911', CAST(5150 AS Decimal(18, 0)), CAST(N'2016-08-03' AS Date), CAST(5150 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3028, N'Taifur', CAST(N'2016-08-03' AS Date), N'101010', CAST(12314012 AS Decimal(18, 0)), CAST(N'2016-08-03' AS Date), CAST(12314012 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (3029, N'muntahar', CAST(N'2016-08-04' AS Date), N'999999', CAST(1600 AS Decimal(18, 0)), CAST(N'2016-08-03' AS Date), CAST(1400 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (4027, N'Saddam', CAST(N'2016-08-05' AS Date), N'1231233444', CAST(300 AS Decimal(18, 0)), CAST(N'2016-08-04' AS Date), CAST(300 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (4028, N'Galiver', CAST(N'2016-08-02' AS Date), N'333333', CAST(1600 AS Decimal(18, 0)), CAST(N'2016-08-04' AS Date), CAST(1600 AS Decimal(18, 0)), CAST(123 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (5027, N'Galiver', CAST(N'2016-08-09' AS Date), N'019192342324', CAST(112112 AS Decimal(18, 0)), CAST(N'2016-08-17' AS Date), CAST(0 AS Decimal(18, 0)), CAST(112112 AS Decimal(18, 0)))
INSERT [dbo].[TestRequestEntry] ([Id], [NameOfPatient], [DOB], [MobileNo], [TotalAmount], [DueDate], [PaymentStatus], [PaidAmount]) VALUES (6027, N'asfsasaaf', CAST(N'2016-08-01' AS Date), N'1131333', CAST(250 AS Decimal(18, 0)), CAST(N'2016-08-21' AS Date), CAST(250 AS Decimal(18, 0)), NULL)
SET IDENTITY_INSERT [dbo].[TestRequestEntry] OFF
SET ANSI_PADDING OFF
SET IDENTITY_INSERT [dbo].[TestSetup] ON 

INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (1, N'RBS', CAST(150 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (2, N'S. Creatinine', CAST(350 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (3, N'Hand X-ray', CAST(200 AS Decimal(18, 0)), 6)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (4, N'Test', CAST(100 AS Decimal(18, 0)), 8)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (5, N'Arafat', CAST(6000 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (6, N'Kick', CAST(333 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (7, N'dental', CAST(800 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (1005, N'LS Spine', CAST(4500 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (1006, N'blood count', CAST(2323 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (1007, N'rere', CAST(44444 AS Decimal(18, 0)), 7)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (1008, N'reee', CAST(123123 AS Decimal(18, 0)), 7)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (1009, N'reeewew', CAST(123123 AS Decimal(18, 0)), 8)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (1010, N'twet', CAST(300 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (2010, N'Fortex', CAST(7000 AS Decimal(18, 0)), 8)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (3010, N'ECG', CAST(112112 AS Decimal(18, 0)), 1009)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (3011, N'Echo', CAST(1000 AS Decimal(18, 0)), 1010)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (3012, N'Andoscopy', CAST(5000 AS Decimal(18, 0)), 1012)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (3013, N'Has', CAST(444 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (3014, N'asasdasd', CAST(12313212 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (3015, N'Rakib Hossain', CAST(800 AS Decimal(18, 0)), 5)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (3016, N'Nikijiki', CAST(800 AS Decimal(18, 0)), 1016)
INSERT [dbo].[TestSetup] ([Id], [TestName], [Fee], [TestTypeId]) VALUES (3017, N'Ajlan', CAST(800 AS Decimal(18, 0)), 1017)
SET IDENTITY_INSERT [dbo].[TestSetup] OFF
SET IDENTITY_INSERT [dbo].[TestType] ON 

INSERT [dbo].[TestType] ([Id], [TestType]) VALUES (5, N'Blood')
INSERT [dbo].[TestType] ([Id], [TestType]) VALUES (1016, N'ECG')
INSERT [dbo].[TestType] ([Id], [TestType]) VALUES (1017, N'Gadmi')
INSERT [dbo].[TestType] ([Id], [TestType]) VALUES (1015, N'Galib')
INSERT [dbo].[TestType] ([Id], [TestType]) VALUES (1013, N'Sonic')
INSERT [dbo].[TestType] ([Id], [TestType]) VALUES (8, N'Test')
INSERT [dbo].[TestType] ([Id], [TestType]) VALUES (9, N'urine')
INSERT [dbo].[TestType] ([Id], [TestType]) VALUES (7, N'USG')
INSERT [dbo].[TestType] ([Id], [TestType]) VALUES (6, N'X-Ray')
SET IDENTITY_INSERT [dbo].[TestType] OFF
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_TestType]    Script Date: 9/10/2016 2:31:27 AM ******/
ALTER TABLE [dbo].[TestType] ADD  CONSTRAINT [IX_TestType] UNIQUE NONCLUSTERED 
(
	[TestType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PatientTest]  WITH CHECK ADD  CONSTRAINT [FK_PatientTest_PatientTest] FOREIGN KEY([TestReqid])
REFERENCES [dbo].[TestRequestEntry] ([Id])
GO
ALTER TABLE [dbo].[PatientTest] CHECK CONSTRAINT [FK_PatientTest_PatientTest]
GO
ALTER TABLE [dbo].[PatientTest]  WITH CHECK ADD  CONSTRAINT [FK_PatientTests_Tests] FOREIGN KEY([TestId])
REFERENCES [dbo].[TestSetup] ([Id])
GO
ALTER TABLE [dbo].[PatientTest] CHECK CONSTRAINT [FK_PatientTests_Tests]
GO
/****** Object:  StoredProcedure [dbo].[ProcPatient]    Script Date: 9/10/2016 2:31:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[ProcPatient]
(@pname varchar(50),@dob date)
as
insert into  [dbo].[Patient] values(@pname,@dob)





GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "t"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 136
               Right = 448
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pt"
            Begin Extent = 
               Top = 102
               Left = 38
               Bottom = 215
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "tt"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 102
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DateWiseTestTypesReport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'DateWiseTestTypesReport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "TestRequestEntry"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 209
            End
            DisplayFlags = 280
            TopColumn = 4
         End
         Begin Table = "TestSetup"
            Begin Extent = 
               Top = 6
               Left = 247
               Bottom = 136
               Right = 417
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SearchView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'SearchView'
GO
USE [master]
GO
ALTER DATABASE [DiagnosticCenter] SET  READ_WRITE 
GO
