
ALTER TABLE ASZTAL ADD
	USEABLE int NULL,
	NAME_VISIBLE int NULL
GO
ALTER TABLE ASZTAL ADD CONSTRAINT
	DF_ASZTAL_USEABLE DEFAULT 1 FOR USEABLE
GO
ALTER TABLE ASZTAL ADD CONSTRAINT
	DF_ASZTAL_NAME_VISIBLE DEFAULT 1 FOR NAME_VISIBLE

GO

UPDATE ASZTAL SET NAME_VISIBLE = 1, USEABLE = 1

GO

UPDATE VERSION  SET DB_VER = '023'

GO





