ALTER TABLE RENDELES_SOR ADD
	LIT_KISZ_ID int NULL

GO

ALTER TABLE SZAMLA_TETEL ADD
	KALK_MENNYISEG float(53) NULL
	
	GO



insert into SYSPAR (PARAM_NAME,PARAM_VALUE_S)
	VALUES ('CEG_NEV','ALL-IN Cafe')


insert into SYSPAR (PARAM_NAME,PARAM_VALUE_S)
	VALUES ('CEG_CIM','2120 Dunakeszi R�v utca 1/B')

insert into SYSPAR (PARAM_NAME,PARAM_VALUE_S, PARAM_VALUE_I)
	VALUES ('BLOKK_LABLEC','L�bl�c sz�vegben az els�. A sorrendet a param�ter sz�m �rt�ke hat�rozza meg',1)

insert into SYSPAR (PARAM_NAME,PARAM_VALUE_S, PARAM_VALUE_I)
	VALUES ('BLOKK_LABLEC','M�sodik l�bl�c... elvileg sok lehet...',2)

insert into SYSPAR (PARAM_NAME,PARAM_VALUE_S, PARAM_VALUE_I)
	VALUES ('BLOKK_LABLEC','Asztalfoglal�s: + 36 55 555 555 5',3)
	
GO


UPDATE VERSION  SET DB_VER = '024'

GO





