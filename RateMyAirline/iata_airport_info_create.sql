USE [master]
GO
/****** Object:  Database [RateMyAirline]    Script Date: 3/26/2018 6:23:54 PM ******/
CREATE DATABASE [RateMyAirline]
 GO
ALTER DATABASE [RateMyAirline] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RateMyAirline].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RateMyAirline] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RateMyAirline] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RateMyAirline] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RateMyAirline] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RateMyAirline] SET ARITHABORT OFF 
GO
ALTER DATABASE [RateMyAirline] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RateMyAirline] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RateMyAirline] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RateMyAirline] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RateMyAirline] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RateMyAirline] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RateMyAirline] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RateMyAirline] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RateMyAirline] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RateMyAirline] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RateMyAirline] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RateMyAirline] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RateMyAirline] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RateMyAirline] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RateMyAirline] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RateMyAirline] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RateMyAirline] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RateMyAirline] SET RECOVERY FULL 
GO
ALTER DATABASE [RateMyAirline] SET  MULTI_USER 
GO
ALTER DATABASE [RateMyAirline] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RateMyAirline] SET DB_CHAINING OFF 
GO

EXEC sys.sp_db_vardecimal_storage_format N'RateMyAirline', N'ON'
GO

/****** Object:  Table [dbo].[iata_airport_info]    Script Date: 3/26/2018 6:23:54 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [RateMyAirline].[dbo].[iata_airport_info](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[dent] [varchar](8) NULL,
	[type] [varchar](24) NULL,
	[name] [varchar](96) NULL,
	[latitude] [decimal](20, 8) NULL,
	[longitude] [decimal](20, 8) NULL,
	[elevation] [int] NULL,
	[continent] [varchar](96) NULL,
	[iso_country] [varchar](96) NULL,
	[iso_region] [varchar](96) NULL,
	[municipality] [varchar](96) NULL,
	[gps_code] [varchar](96) NULL,
	[iata_code] [varchar](96) NULL,
	[local_code] [varchar](96) NULL,
 CONSTRAINT [PK_iata_airport_info] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [RateMyAirline] SET  READ_WRITE 
GO
