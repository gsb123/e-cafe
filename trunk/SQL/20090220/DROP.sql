
/****** Object:  StoredProcedure [dbo].[createNapok]    Script Date: 02/20/2009 23:28:53 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[createNapok]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[createNapok]
go

/****** Object:  StoredProcedure [dbo].[getNyitottNap]    Script Date: 02/20/2009 23:29:09 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getNyitottNap]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getNyitottNap]

go
/****** Object:  StoredProcedure [dbo].[openDay]    Script Date: 02/20/2009 23:29:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[openDay]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[openDay]

go

/****** Object:  StoredProcedure [dbo].[setNyitoSor]    Script Date: 02/20/2009 23:29:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[setNyitoSor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[setNyitoSor]

go
/****** Object:  UserDefinedFunction [dbo].[GetKeszlet]    Script Date: 02/20/2009 23:39:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetKeszlet]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetKeszlet]

go

/****** Object:  UserDefinedFunction [dbo].[getNyitoKeszlet]    Script Date: 02/20/2009 23:39:33 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getNyitoKeszlet]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[getNyitoKeszlet]

GO

/****** Object:  UserDefinedFunction [dbo].[getTegnap]    Script Date: 02/20/2009 23:39:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getTegnap]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[getTegnap]

GO
/****** Object:  UserDefinedFunction [dbo].[fn_get_Atlagar]    Script Date: 02/20/2009 23:40:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_get_Atlagar]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fn_get_Atlagar]


GO
/****** Object:  Trigger [tr_BevSor_insert_update_keszlet]    Script Date: 02/20/2009 23:54:30 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_BevSor_insert_update_keszlet]'))
DROP TRIGGER [dbo].[tr_BevSor_insert_update_keszlet]

GO
/****** Object:  Trigger [tr_Fej_vizsgalat_Soron]    Script Date: 02/20/2009 23:59:38 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_Fej_vizsgalat_Soron]'))
DROP TRIGGER [dbo].[tr_Fej_vizsgalat_Soron]

GO
/****** Object:  Trigger [tr_KeszletSor_update_keszlet]    Script Date: 02/21/2009 00:02:11 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_KeszletSor_update_keszlet]'))
DROP TRIGGER [dbo].[tr_KeszletSor_update_keszlet]

GO

/****** Object:  Trigger [tr_RendelSor_Keszletupdate]    Script Date: 02/21/2009 00:06:00 ******/
IF  EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[tr_RendelSor_Keszletupdate]'))
DROP TRIGGER [dbo].[tr_RendelSor_Keszletupdate]