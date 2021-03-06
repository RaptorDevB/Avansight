USE [master]
GO
/****** Object:  Database [PatientDB]    Script Date: 5/20/2021 11:16:30 PM ******/
CREATE DATABASE [PatientDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PatientDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\PatientDB.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'PatientDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\PatientDB_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [PatientDB] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PatientDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PatientDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PatientDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PatientDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PatientDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PatientDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PatientDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PatientDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PatientDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PatientDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PatientDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PatientDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PatientDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PatientDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PatientDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PatientDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PatientDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PatientDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PatientDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PatientDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PatientDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PatientDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PatientDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PatientDB] SET RECOVERY FULL 
GO
ALTER DATABASE [PatientDB] SET  MULTI_USER 
GO
ALTER DATABASE [PatientDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PatientDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PatientDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PatientDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [PatientDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PatientDB', N'ON'
GO
USE [PatientDB]
GO
/****** Object:  UserDefinedTableType [dbo].[PatientTableType]    Script Date: 5/20/2021 11:16:30 PM ******/
CREATE TYPE [dbo].[PatientTableType] AS TABLE(
	[Age] [int] NOT NULL,
	[Gender] [varchar](50) NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TreatmentReadingTableType]    Script Date: 5/20/2021 11:16:31 PM ******/
CREATE TYPE [dbo].[TreatmentReadingTableType] AS TABLE(
	[VisitWeek] [varchar](10) NOT NULL,
	[Reading] [decimal](18, 2) NOT NULL,
	[PatientId] [int] NOT NULL
)
GO
/****** Object:  Table [dbo].[Patient]    Script Date: 5/20/2021 11:16:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Patient](
	[PatientId] [int] IDENTITY(1,1) NOT NULL,
	[Age] [int] NOT NULL,
	[Gender] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Patient] PRIMARY KEY CLUSTERED 
(
	[PatientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TreatmentReading]    Script Date: 5/20/2021 11:16:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TreatmentReading](
	[TreatmentReadingId] [int] IDENTITY(1,1) NOT NULL,
	[VisitWeek] [varchar](10) NOT NULL,
	[Reading] [decimal](18, 2) NOT NULL,
	[PatientId] [int] NOT NULL,
 CONSTRAINT [PK_TreatmentReading] PRIMARY KEY CLUSTERED 
(
	[TreatmentReadingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TreatmentReading]  WITH CHECK ADD FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patient] ([PatientId])
GO
/****** Object:  StoredProcedure [dbo].[PatientGet]    Script Date: 5/20/2021 11:16:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Buddika Weerasinghe
-- Create date: 2021-05-20
-- Description:	This will get all the records from the Patient table
-- =============================================
CREATE PROCEDURE [dbo].[PatientGet] 

AS
BEGIN
	SELECT * FROM Patient
END

GO
/****** Object:  StoredProcedure [dbo].[PatientSet]    Script Date: 5/20/2021 11:16:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Buddika Weerasinghe
-- Create date: 2021-05-20
-- Description:	This will parse a PatientTableTpe and insert records to the Patient table
-- =============================================
CREATE PROCEDURE [dbo].[PatientSet](@PatientDetails [PatientTableType] READONLY)
AS
BEGIN
	INSERT INTO Patient (Age,Gender) OUTPUT INSERTED.PatientId  
	SELECT * FROM @PatientDetails
END

GO
/****** Object:  StoredProcedure [dbo].[TreatmentReadingSet]    Script Date: 5/20/2021 11:16:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Buddika Weerasinghe
-- Create date: 2021-05-20
-- Description:	This will parse a TreatmentReadingTableType and insert records to the TreatmentReading table
-- =============================================
CREATE PROCEDURE [dbo].[TreatmentReadingSet](@TreatmentReadingDetails [TreatmentReadingTableType] READONLY)
AS
BEGIN
	INSERT INTO TreatmentReading (VisitWeek,Reading,PatientId)  
	SELECT * FROM @TreatmentReadingDetails 
END
GO
/****** Object:  StoredProcedure [dbo].[TreatmentReadingsGet]    Script Date: 5/20/2021 11:16:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Buddika Weerasinghe
-- Create date: 2021-05-20
-- Description:	This will get all the records from the TreatmentReading table
-- =============================================
CREATE PROCEDURE [dbo].[TreatmentReadingsGet]

AS
BEGIN
	SELECT * FROM TreatmentReading
END

GO
USE [master]
GO
ALTER DATABASE [PatientDB] SET  READ_WRITE 
GO
