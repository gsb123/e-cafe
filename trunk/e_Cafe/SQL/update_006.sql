

ALTER TABLE CIKK ADD
	RECEPT_TEXT text NULL
	
GO
DROP TABLE FOGLALAS
GO
CREATE TABLE FOGLALAS
	(
	FOGLALAS_ID int NOT NULL IDENTITY (1, 1),
	ASZTAL_ID int NOT NULL,
	FOGLAL_FROM datetime NULL,
	FOGLAL_TO datetime NULL,
	PARTNER_ID int NULL,
	MEGJEGYZES varchar(200) NULL,
	NEV varchar(50) NULL,
	TELEFON varchar(50) NULL
	)  ON [PRIMARY]

GO


UPDATE VERSION  SET DB_VER = '006'

GO





