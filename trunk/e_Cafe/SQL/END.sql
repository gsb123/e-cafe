-- =============================================
-- Author:		<László Ernő>
-- Create date: <2009.01.23>
-- Description:	<Készletkarton létrehozása nyitott napra>
-- =============================================
CREATE PROCEDURE openDay
(
-- Melyik napra szeretném megnyitni a készleteket. 
	@EV int ,
	@HO int ,
	@NAP int 
)
AS
BEGIN

declare
	@last_nap_srsz int,
	@open_day_srsz int
BEGIN TRAN T1

set @last_nap_srsz = 1

    select @last_nap_srsz=isnull(n.SRSZ,1) from napok n 
		where n.srsz = isnull( (SELECT max(k.SRSZ) FROM NAPOK k inner join nap_nyitas l on k.EV = l.EV and k.HO = l.HO and k.NAP = l.NAP )  ,(select min(m.SRSZ) from napok m))
 -- rekord beszúrása a nyitott napba

	SELECT @open_day_srsz=k.SRSZ FROM NAPOK k where  k.EV = @EV and k.HO = @HO and k.NAP = @NAP

	INSERT INTO NAP_NYITAS
           ([EV]
           ,[HO]
           ,[NAP]
           ,[NYITAS_DATUMA]
           ,[ZARAS_DATUMA]
           ,[LEZART])
     VALUES
           (@EV
           ,@HO
           ,@NAP
           ,getdate()
           ,null
           ,0)

-- Készletek MÁSOLÁSA ELŐZŐ NAPRÓL
insert into KESZLET_FEJ (EV,HO,NAP,CIKK_ID,RAKTAR_ID,KESZLET_NYITO,KESZLET_ERTEK_NYITO,ELOZO_NAPI_ATLAGAR,BESZERZESI_AR, KESZLET_NAPI, AKT_ATLAGAR)
	select nn.EV,nn.HO, nn.NAP, v.CIKK_ID, v.RAKTAR_ID, v.KESZLET_NYITO+v.KESZLET_NAPI, (v.KESZLET_NYITO+v.KESZLET_NAPI)*isnull(v.AKT_ATLAGAR,0), isnull(v.AKT_ATLAGAR,0),v.BESZERZESI_AR,0,isnull(v.AKT_ATLAGAR,0)
	from napok nn 
		inner join V_SRSZ_KESZLET_NAP v on v.SRSZ = @last_nap_srsz

	where nn.srsz > @last_nap_srsz and nn.srsz <= @open_day_srsz
commit tran T1
END

GO
-- =============================================
-- Author:		<László Ernő>
-- Create date: <2009.01.23>
-- Description:	<Készletkarton létrehozása nyitott napra>
-- =============================================
CREATE PROCEDURE createNapok
(
-- Melyik napra szeretném megnyitni a készleteket. 
	@db int 
)
AS
BEGIN
 -- rekord beszúrása a nyitott napba
declare @i int
declare @lastDate datetime
set @lastDate = '2009.01.01'
set @i = 1
select top 1 @lastDate = CAST( CAST(EV as varchar(2))+'.'+ CAST(HO as varchar(2))+'.'+CAST(NAP as varchar(2)) as datetime) from NAPOK order by SRSZ desc


    while (@i < @db) begin
    set @lastDate = dateadd(day,1,@lastDate)
        INSERT INTO NAPOK
           ([EV]
           ,[HO]
           ,[NAP])
        VALUES
           (year(@lastDate),	month(@lastDate),	day(@lastDate))
    set @i = @i +1
    end

	


	

END

GO

-- =============================================
-- Author:		<László Ernő>
-- Create date: <2009.01.23>
-- Description:	<Nyitott nap meglétének vizsgálata>
-- =============================================
CREATE PROCEDURE getNyitottNap
(
	@EV int output,
	@HO int output,
	@NAP int output
)
AS
BEGIN
	declare @cntNyitnap int
	set @cntNyitnap = 0
	select @cntNyitnap = count(*) from NAP_NYITAS where LEZART = 0
	
    if @cntNyitnap = 0  begin
	--Nincs nyitott nap így létre kell hozni.
	set @EV = year(getdate())
	set @HO = month(getdate())
	set @NAP = day(getdate())
	exec openDay @EV, @HO, @NAP
	
    end else begin
    -- Meg kell vizsgálni hogy mennyi van ha egynél több akkor hiba!!!
        if @cntNyitNap > 1 begin
            RAISERROR('Hibás nyitott napok!',16,1)
        end else begin 
            select @EV = EV, @HO = HO, @NAP = NAP from nap_nyitas where LEZART = 0
        end
    end

	

END

GO


-- =============================================
-- Author:		<László Ernő>
-- Create date: <2009.01.23>
-- Description:	<Készletkarton létrehozása nyitott napra>
-- =============================================
CREATE PROCEDURE setNyitoSor
(
-- Melyik napra szeretném megnyitni a készleteket. 
	@cikk_id int ,
	@mennyiseg float,
    @akt_besz_ar float,
    @raktar_id int
)
AS
BEGIN

if not exists(SELECT '' from KESZLET_SOR where CIKK_ID = @cikk_id and raktar_id = @raktar_id and mozgas_tipus = 'NY')
begin
declare @a_ev int,
        @a_ho int,
        @a_nap int

execute getnyitottNap @a_ev out, @a_ho out, @a_nap out
    



insert into KESZLET_SOR (ev, ho, nap, cikk_id, raktar_id, datum, mennyiseg, irany, egysegar, netto_ertek, afa_ertek, afa_kod, brutto_ertek, mozgas_tipus)
    select @a_ev, @a_ho, @a_nap, CIKK_ID,@raktar_id, getdate(), @mennyiseg, 1, @akt_besz_ar, @akt_besz_ar*@mennyiseg,  @akt_besz_ar*@mennyiseg* (a.AFA_ERTEK/100), a.AFA_KOD, @akt_besz_ar*@mennyiseg+  @akt_besz_ar*@mennyiseg* (a.AFA_ERTEK/100),'NY'  
    from CIKK c
    inner join CIKKCSOPORT cs on cs.cikkcsoport_id = c.cikkcsoport_id
    inner join AFA a on cs.AFA_KOD = a.AFA_KOD
    where  c.cikk_id = @cikk_id
	
end else begin

raiserror ('Egy cikknek nem lehet több nyitó könyvelése!',16,1)
end
/*
exec setNyitoSor 3,77,98.5,1

exec setNyitoSor 3,100,98.5,2

exec setNyitoSor 4,30,78.5,1

exec setNyitoSor 5,12,102.5,1

exec setNyitoSor 6,44,155.5,1

exec setNyitoSor 7,70,202,1

exec setNyitoSor 8,44,197,1

exec setNyitoSor 8,5,197,2

*/
END

GO


CREATE FUNCTION GetKeszlet
	(@EV int, @HO int , @NAP int, @cikk_id int, @raktar_id int)
RETURNS @ret TABLE 
	(RAKTAR_ID int, RAKTAR_NEV Varchar(50), KESZLET float, KESZLET_ERTEK float, ATLAGAR float)
AS
BEGIN

if Exists(SELECT '' FROM CIKK WHERE CIKK_ID = @cikk_id and CIKK_TIPUS = 1) begin
	if @raktar_id = -1 begin

		INSERT @ret
			select RAKTAR_ID, RAKTAR_KOD, MENNY, MENNY * ATLAGAR, ATLAGAR
			from (
					SELECT  f.RAKTAR_ID, r.RAKTAR_KOD, 
								min(CASE WHEN cc.VIRTUAL = 1 THEN 99999999 
                  ELSE Round(isnull(f.KESZLET_NYITO,0) + isnull(f.KESZLET_NAPI,0),2) / isnull(c.TARTOZEK_MENNY,1)
                  END) as MENNY, 
								0 as ERTEK_,
								sum(dbo.fn_get_Atlagar(f.cikk_id,r.RAKTAR_ID,@EV,@HO,@NAP)) as ATLAGAR
						FROM KESZLET_FEJ f 
							inner join RAKTAR r on f.RAKTAR_ID = r.RAKTAR_ID
							left join RECEPT c on c.OSSZ_CIKK_TARTOZEK_ID = f.cikk_id
							left join CIKK cc on c.OSSZ_CIKK_TARTOZEK_ID = cc.cikk_id
						where c.OSSZ_CIKK_ID = @cikk_id and f.EV = @EV and f.nap = @NAP and f.ho = @HO
						group by f.RAKTAR_ID, r.RAKTAR_KOD
			) as AL 
	end else begin

		INSERT @ret
			select RAKTAR_ID, RAKTAR_KOD, MENNY, MENNY * ATLAGAR, ATLAGAR
			from (
					SELECT  f.RAKTAR_ID, r.RAKTAR_KOD, 
								min(CASE WHEN cc.VIRTUAL = 1 THEN 99999999 
                  ELSE Round(isnull(f.KESZLET_NYITO,0) + isnull(f.KESZLET_NAPI,0),2) / isnull(c.TARTOZEK_MENNY,1)
                  END) as MENNY, 
								0 as ERTEK_,
								sum(dbo.fn_get_Atlagar(f.cikk_id,r.RAKTAR_ID,@EV,@HO,@NAP)) as ATLAGAR
						FROM KESZLET_FEJ f 
							inner join RAKTAR r on f.RAKTAR_ID = r.RAKTAR_ID
							left join RECEPT c on c.OSSZ_CIKK_TARTOZEK_ID = f.cikk_id
							left join CIKK cc on c.OSSZ_CIKK_TARTOZEK_ID= cc.cikk_id
						where c.OSSZ_CIKK_ID = @cikk_id and f.raktar_id = @raktar_id  and f.EV = @EV and f.nap = @NAP and f.ho = @HO
						group by f.RAKTAR_ID, r.RAKTAR_KOD
			) as AL 
	end

end else begin

if @raktar_id = -1 begin

	INSERT @ret
        SELECT f.RAKTAR_ID, r.RAKTAR_KOD,CASE WHEN cc.VIRTUAL = 1 THEN 99999999 
                  ELSE Round(isnull(f.KESZLET_NYITO,0) + isnull(f.KESZLET_NAPI,0),2) END, 
                  CASE WHEN cc.VIRTUAL = 1 THEN 0 
                  ELSE Round(isnull(f.KESZLET_ERTEK_NYITO,0)+ (isnull(f.KESZLET_NAPI,0)*dbo.fn_get_Atlagar(f.cikk_id,r.RAKTAR_ID,@EV,@HO,@NAP)),2) END, dbo.fn_get_Atlagar(f.cikk_id,r.RAKTAR_ID,@EV,@HO,@NAP)
            FROM KESZLET_FEJ f inner join RAKTAR r on f.RAKTAR_ID = r.RAKTAR_ID
            left join CIKK cc on f.cikk_id = cc.cikk_id
            where f.cikk_id = @cikk_id and f.EV = @EV and f.nap = @NAP and f.ho = @HO

end else begin

	INSERT @ret
        SELECT f.RAKTAR_ID, r.RAKTAR_KOD, CASE WHEN cc.VIRTUAL = 1 THEN 99999999 
                  ELSE Round(isnull(f.KESZLET_NYITO,0) + isnull(f.KESZLET_NAPI,0),2) END, 
                  CASE WHEN cc.VIRTUAL = 1 THEN 0 
                  ELSE Round(isnull(f.KESZLET_ERTEK_NYITO,0)+ (isnull(f.KESZLET_NAPI,0)*dbo.fn_get_Atlagar(f.cikk_id,r.RAKTAR_ID,@EV,@HO,@NAP)),2) END, dbo.fn_get_Atlagar(f.cikk_id,r.RAKTAR_ID,@EV,@HO,@NAP)
            FROM KESZLET_FEJ f inner join RAKTAR r on f.RAKTAR_ID = r.RAKTAR_ID
            left join CIKK cc on f.cikk_id = cc.cikk_id
            where f.cikk_id = @cikk_id and f.raktar_id = @raktar_id 
                and f.EV = @EV and f.nap = @NAP and f.ho = @HO

end

end

	RETURN 
END

GO

CREATE FUNCTION getNyitoKeszlet
	(@EV int, @HO int , @NAP int, @cikk_id int, @raktar_id int)
RETURNS @ret TABLE 
	(KESZLET_NYITO float, KESZLET_ERTEK_NYITO float, ELOZO_NAPI_ATLAGAR float, BESZERZESI_AR float)
AS
BEGIN

declare @tmp_c int

	INSERT @ret
		select null,null,null,null
union
        SELECT f.KESZLET_NYITO, f.KESZLET_ERTEK_NYITO, f.ELOZO_NAPI_ATLAGAR, f.BESZERZESI_AR  
            FROM KESZLET_FEJ f
            inner join getTegnap(@ev, @ho, @nap) t on f.EV = t.EV and f.nap = t.nap and f.ho = t.ho
            where cikk_id = @cikk_id and raktar_id = @raktar_id


select @tmp_c= count(*) from @ret
if @tmp_c > 1 begin
delete from @ret where KESZLET_NYITO is null
end


	RETURN 
END


GO

CREATE FUNCTION getTegnap 
	(-- Melyik napra szeretném megnyitni a készleteket. 
	@EV int ,
	@HO int ,
	@NAP int)
RETURNS @i_date TABLE 
	(EV int, HO int, NAP int) 
	 
AS
BEGIN
declare @i int
SELECT @i=SRSZ  FROM  NAPOK  WHERE EV = @EV and HO = @HO and NAP = @NAP

	INSERT @i_date
        SELECT top 1 EV,HO,NAP FROM  NAPOK  WHERE SRSZ <@i order by SRSZ desc
	RETURN 
END


GO
create function [dbo].[fn_get_Atlagar](@cikk_id int,@raktar_id int, @EV int, @HO int, @NAP int)
returns float
as 
begin

declare @ret float 
set @ret = 0
if ((@cikk_id >0) and (@raktar_id > 0)) begin
	select @ret = isnull(AKT_ATLAGAR,0) from KESZLET_FEJ f
	where f.CIKK_ID = @cikk_id and f.RAKTAR_ID = @raktar_id
			and f.EV = @EV and f.HO = @HO and f.NAP = @NAP


end

return Round(@ret,2)

end

GO

-- =============================================
-- Author:		László Ernő
-- Create date: 2009.03.01
-- Description:	Készlet vezetése
-- =============================================
CREATE PROCEDURE sp_Do_Keszlet_vezetes 
	-- Add the parameters for the stored procedure here
	@cikk_id int,
	@raktar_id int,
	@moove_type varchar(2),

	@rendel_id int,
	@biz_id int,

	@db float, 
	@ertek float,

	@datum datetime

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @EV int, @HO int, @NAP int, @DELETE int, @cikk_type int,	
			@r_cikk_id int, @r_tartozek_menny float, @irany int

	exec getNyitottNap @EV out, @HO out, @NAP out
	SELECT @cikk_type = isnull(c.CIKK_TIPUS,0) from CIKK c where c.CIKK_ID = @cikk_id
	
	select @irany = MOZGAS_IRANY from MOZGAS m where m.MOZGAS_ID = @moove_type

	if @cikk_type = 0 begin

		INSERT INTO KESZLET_SOR
				   (EV ,HO ,NAP
				   ,RAKTAR_ID,CIKK_ID ,DATUM
				   ,RENDELES_ID ,BIZONYLAT_ID
				   ,MENNYISEG ,IRANY ,EGYSEGAR
				   ,NETTO_ERTEK
				   ,AFA_ERTEK
				   ,AFA_KOD
				   ,BRUTTO_ERTEK
				   ,MOZGAS_TIPUS
					,KESZLET_EGYS_AR)
			 SELECT @EV,@HO,@NAP,
					@raktar_id, @cikk_id, @datum,
					@rendel_id,@biz_id,
					@db,@irany,@ertek,
					@db*@ertek,
					@ertek*(a.AFA_ERTEK/100),
					cs.AFA_KOD,
					@ertek*(1+(a.AFA_ERTEK/100)),
					@moove_type,
					dbo.fn_get_Atlagar(c.CIKK_ID,@raktar_id, @EV, @HO, @NAP)
			FROM CIKK c
			inner join CIKKCSOPORT cs on c.CIKKCSOPORT_ID = cs.CIKKCSOPORT_ID
			inner join AFA a on cs.AFA_KOD = a.AFA_KOD	
			WHERE c.CIKK_ID = @cikk_id

	end else begin


		DECLARE recept_cikkek CURSOR FOR 
		   SELECT r.OSSZ_CIKK_TARTOZEK_ID, r.TARTOZEK_MENNY  FROM RECEPT r  WHERE  r.OSSZ_CIKK_ID = @cikk_id

		   OPEN recept_cikkek
		   FETCH NEXT FROM recept_cikkek INTO @r_cikk_id, @r_tartozek_menny

		   WHILE @@FETCH_STATUS = 0
		   BEGIN
		      
				INSERT INTO KESZLET_SOR
					   (EV ,HO ,NAP
					   ,RAKTAR_ID,CIKK_ID ,DATUM
					   ,RENDELES_ID ,BIZONYLAT_ID
					   ,MENNYISEG ,IRANY ,EGYSEGAR
					   ,NETTO_ERTEK
					   ,AFA_ERTEK
					   ,AFA_KOD
					   ,BRUTTO_ERTEK
					   ,MOZGAS_TIPUS
						,KESZLET_EGYS_AR)
				 SELECT @EV,@HO,@NAP,
						@raktar_id, @r_cikk_id, @datum,
						@rendel_id,@biz_id,
						@r_tartozek_menny,@irany,@ertek,
						@r_tartozek_menny*@ertek,
						@ertek*(a.AFA_ERTEK/100),
						cs.AFA_KOD,
						@ertek*(1+(a.AFA_ERTEK/100)),
						@moove_type,
						dbo.fn_get_Atlagar(@r_cikk_id,@raktar_id, @EV, @HO, @NAP)
				FROM CIKK c 
				inner join CIKKCSOPORT cs on c.CIKKCSOPORT_ID = cs.CIKKCSOPORT_ID
				inner join AFA a on cs.AFA_KOD = a.AFA_KOD	
				WHERE c.CIKK_ID = @r_cikk_id
				
		   FETCH NEXT FROM recept_cikkek INTO @r_cikk_id, @r_tartozek_menny
		   END

		   CLOSE recept_cikkek
		   DEALLOCATE recept_cikkek

		

	end


END


GO


-- =============================================
-- Author:		László Ernő
-- Create date: 2009.01.26
-- Description:	Bevételezés készlet vezetése
-- =============================================
CREATE TRIGGER tr_BevSor_insert_update_keszlet
   ON  BEVETEL_SOR
   AFTER INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for trigger here
	declare @EV int, @HO int, @NAP int, @Feladva int, @sor_id int, @vane int,
			@BESZ_AR int, @cikk_id int, @szallito int


	exec getNyitottNap @EV out, @HO out, @NAP out

	select @Feladva = FELADVA, @sor_id = SOR_ID, @BESZ_AR = BESZ_AR , @CIKK_ID = CIKK_ID from inserted

	select @szallito = PARTNER_ID from inserted i
		inner join BEVETEL_FEJ f on i.BEVETEL_FEJ_ID = f.BEVETEL_FEJ_ID

	set @vane = 0
	select @vane=count(*) from KESZLET_SOR where BIZONYLAT_ID = @sor_id and (BEVETEL_ID is not null)

	if (@feladva = 1) and (@vane = 0) begin

		INSERT INTO KESZLET_SOR
			   (EV ,HO ,NAP
			   ,RAKTAR_ID,CIKK_ID ,DATUM
			   ,BEVETEL_ID ,BIZONYLAT_ID
			   ,MENNYISEG ,IRANY ,EGYSEGAR
			   ,NETTO_ERTEK
			   ,AFA_ERTEK
			   ,AFA_KOD
			   ,BRUTTO_ERTEK
			   ,MOZGAS_TIPUS
				,KESZLET_EGYS_AR)
		 SELECT @EV,@HO,@NAP,
				i.RAKTAR_ID, i.CIKK_ID, f.DATUM,
				i.BEVETEL_FEJ_ID,i.SOR_ID,
				MENNY,1,BESZ_AR,
				MENNY*BESZ_AR,
				(MENNY*BESZ_AR)*(a.AFA_ERTEK/100),
				cs.AFA_KOD,
				(MENNY*BESZ_AR)*(1+(a.AFA_ERTEK/100)),
				'B',BESZ_AR
		FROM INSERTED i 
		inner join BEVETEL_FEJ f on i.BEVETEL_FEJ_ID = f.BEVETEL_FEJ_ID
		inner join CIKK c on i.CIKK_ID = c.CIKK_ID
		inner join CIKKCSOPORT cs on c.CIKKCSOPORT_ID = cs.CIKKCSOPORT_ID
		inner join AFA a on cs.AFA_KOD = a.AFA_KOD
		where not exists (select '' from KESZLET_SOR where BIZONYLAT_ID = i.SOR_ID and (BEVETEL_ID is not null))


	end

    IF @szallito >0 begin
	if  not Exists(SELECT '' from CIKK_BESZALLITOK WHERE CIKK_ID = @CIKK_ID and PARTNER_ID = @szallito) begin
		INSERT INTO CIKK_BESZALLITOK (CIKK_ID, PARTNER_ID, BESZ_AR)
			VALUES (@CIKK_ID, @szallito, @BESZ_AR)
		
	end else begin
		UPDATE CIKK_BESZALLITOK SET BESZ_AR = @BESZ_AR WHERE CIKK_ID = @CIKK_ID and PARTNER_ID = @szallito
	end
end

END


GO

-- =============================================
-- Author:		<László Ernő>
-- Create date: <2009.01.23>
-- Description:	<Készletkarton létrehozása sor beszúrása esetén a nyitott napra>
-- =============================================
CREATE TRIGGER tr_Fej_vizsgalat_Soron
   ON  KESZLET_SOR
   AFTER INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here
	declare @s_ev int,
			@s_ho int,
			@s_nap int,
			@s_cikk_id int,
			@s_raktar_id int
	
    SELECT top 1 @s_ev=I.EV, @s_ho=I.HO, @s_nap=I.NAP, @s_cikk_id=I.CIKK_ID, @s_raktar_id=I.RAKTAR_ID from inserted I


	if not exists(SELECT '' FROM KESZLET_FEJ f where f.EV = @s_ev and 
						f.HO = @s_ho and
						f.NAP = @s_nap and
						f.CIKK_ID = @s_cikk_id and
						f.RAKTAR_ID = @s_raktar_id
		) 
	begin

		insert into KESZLET_FEJ (EV,HO, NAP, CIKK_ID, RAKTAR_ID, KESZLET_NYITO, KESZLET_ERTEK_NYITO, ELOZO_NAPI_ATLAGAR, BESZERZESI_AR)
			SELECT @s_ev, @s_ho, @s_nap, @s_cikk_id, @s_raktar_id, isnull(n.KESZLET_NYITO,0), isnull(n.KESZLET_ERTEK_NYITO,0), isnull(n.ELOZO_NAPI_ATLAGAR,0), isnull(n.BESZERZESI_AR,0) 
				FROM getNyitoKeszlet( @s_ev, @s_ho, @s_nap, @s_cikk_id, @s_raktar_id ) n 

	end

END


GO
-- =============================================
-- Author:		<László Ernő>
-- Create date: <2009.01.23>
-- Description:	<Készletkarton létrehozása nyitott napra>
-- =============================================
CREATE TRIGGER tr_KeszletSor_update_keszlet
   ON  KESZLET_SOR
   AFTER  INSERT,UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here

	update KESZLET_FEJ SET KESZLET_NAPI = (select SUM(s.MENNYISEG*s.IRANY) from 
								KESZLET_SOR s 
								inner join inserted i on s.EV = i.EV 
														and s.HO = i.HO 
														and s.NAP = i.NAP 
														and i.CIKK_ID = s.CIKK_ID 
														and s.RAKTAR_ID = i.RAKTAR_ID),
					
					AKT_ATLAGAR = CASE WHEN KESZLET_NYITO + (select SUM(s.MENNYISEG*s.IRANY) from 
												KESZLET_SOR s 
												inner join inserted i on s.EV = i.EV 
														and s.HO = i.HO 
														and s.NAP = i.NAP 
														and i.CIKK_ID = s.CIKK_ID 
														and s.RAKTAR_ID = i.RAKTAR_ID) = 0 
								then 0 
								else (KESZLET_ERTEK_NYITO + (select SUM(s.MENNYISEG*s.IRANY*isnull(s.KESZLET_EGYS_AR,0)) from 
												KESZLET_SOR s 
												inner join inserted i on s.EV = i.EV 
														and s.HO = i.HO 
														and s.NAP = i.NAP 
														and i.CIKK_ID = s.CIKK_ID 
														and s.RAKTAR_ID = i.RAKTAR_ID)
									)
								/ (KESZLET_NYITO + (select SUM(s.MENNYISEG*s.IRANY) from 
												KESZLET_SOR s 
												inner join inserted i on s.EV = i.EV 
														and s.HO = i.HO 
														and s.NAP = i.NAP 
														and i.CIKK_ID = s.CIKK_ID 
														and s.RAKTAR_ID = i.RAKTAR_ID))
								END
									
    from KESZLET_FEJ f inner join inserted i on f.EV = i.EV and f.HO = i.HO and f.NAP = i.NAP and f.CIKK_ID = i.CIKK_ID and f.RAKTAR_ID = i.RAKTAR_ID



		
END

GO

-- =============================================
-- Author:		László Ernő
-- Create date: 2009.01.25
-- Description:	Rendelések készletmozgását végzi el.
-- =============================================
CREATE TRIGGER tr_RendelSor_Keszletupdate 
   ON  RENDELES_SOR
   AFTER  UPDATE
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here

	declare @new_DELETE int, @old_DELETE int, @old_Fizetve int, @new_Fizetve int,
			@cikk int, @rakt int, @sor_id int, @rendel_id int, @db float, @ertek float, @datum datetime


	select @new_DELETE = DELETED, @new_Fizetve = FIZETVE, @cikk = i.CIKK_ID, @rakt = i.RAKTAR_ID, @sor_id = i.SOR_ID, @rendel_id = i.RENDELES_ID,
		 @db = i.DB, @ertek = i.ERTEK, @datum = i.DATUM from inserted i
		left join CIKK c on i.CIKK_ID = c.CIKK_ID
	select @old_DELETE = DELETED, @old_Fizetve = FIZETVE from deleted 

	
	if ((@new_DELETE = 1) and (@old_DELETE = 0)) begin
	
	-- storno
	-- F és FS esetén az érték bruttóban kerül megadásra.
	exec sp_Do_Keszlet_vezetes @cikk, @rakt, 'FS', @sor_id, @rendel_id, @db, @ertek, @datum
		
	end

	if ((@new_Fizetve = 1) and (@old_Fizetve = 0)) begin
	
		if not exists(SELECT '' FROM RENDELES_SOR s inner join inserted i on s.RENDELES_ID = i.RENDELES_ID and s.sor_id <> i.sor_id  where isnull(s.DELETED,0) = 0 and isnull(s.FIZETVE,0) = 0) begin

			update RENDELES_FEJ SET FIZETVE = 1 FROM RENDELES_FEJ f inner join inserted i on f.RENDELES_ID = i.RENDELES_ID

		end
	

	end
	/*
	update RENDELES_FEJ SET AKTIV = 0
	from rendeles_fej f
	left join rendeles_sor s on f.rendeles_id = s.rendeles_id and isnull(s.DELETED,0) = 0  and f.rendeles_id != @rendel_id
	where s.sor_id is null

	update KESZLET_SOR SET MOZGAS_TIPUS = 'EK'
	from KESZLET_SOR s
	inner join inserted r on s.rendeles_id = r.sor_id and isnull(r.CANCELED,0) = 1
*/
END

GO

-- =============================================
-- Author:		Author,,Name>
-- Create date: Create Date,,>
-- Description:	Description,,>
-- =============================================
CREATE PROCEDURE SP_KESZLET_ATVEZET
	-- Add the parameters for the stored procedure here
	@from_raktar int,
	@to_raktar int,
	@cikk_id int,
	@menny float
	
AS
BEGIN
	
	SET NOCOUNT ON;

declare @a_ev int,
        @a_ho int,
        @a_nap int,
		@afasz float,
		@afa_kod varchar(2)

execute getnyitottNap @a_ev out, @a_ho out, @a_nap out
   
select @afa_kod = a.AFA_KOD, @afasz = a.AFA_ERTEK from aFA a inner join CIKKCSOPORT cs on cs.AFA_KOD = a.AFA_KOD
inner join CIKK c on c.CIKKCSOPORT_ID = cs.CIKKCSOPORT_ID
where c.CIKK_ID = @cikk_id

-- csökkentő tétel
INSERT INTO KESZLET_SOR
           (EV,HO,NAP,RAKTAR_ID,CIKK_ID,DATUM,RENDELES_ID,BEVETEL_ID
			,BIZONYLAT_ID,MENNYISEG,IRANY,EGYSEGAR,NETTO_ERTEK,AFA_ERTEK
			,AFA_KOD,BRUTTO_ERTEK,MOZGAS_TIPUS,KESZLET_EGYS_AR)
     VALUES
           (@a_ev,@a_ho,@a_nap,@from_raktar,@cikk_id,getdate(),null,null
			,null,@menny,-1,dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap),dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap)*@menny, (dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap)*@menny)*(@afasz/100)
			,@afa_kod,(dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap)*@menny)*(1+(@afasz/100)),'R-',dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap))

-- növelő tétel tétel
INSERT INTO KESZLET_SOR
           (EV,HO,NAP,RAKTAR_ID,CIKK_ID,DATUM,RENDELES_ID,BEVETEL_ID
			,BIZONYLAT_ID,MENNYISEG,IRANY,EGYSEGAR,NETTO_ERTEK,AFA_ERTEK
			,AFA_KOD,BRUTTO_ERTEK,MOZGAS_TIPUS,KESZLET_EGYS_AR)
     VALUES
           (@a_ev,@a_ho,@a_nap,@to_raktar,@cikk_id,getdate(),null,null
			,null,@menny,1,dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap),dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap)*@menny, (dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap)*@menny)*(@afasz/100)
			,@afa_kod,(dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap)*@menny)*(1+(@afasz/100)),'R+',dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap))

    
END


GO

-- =============================================
-- Author:		László Ernő
-- Create date: 2009.03.01
-- Description:	Hitelre könyveli a rendelés sorokat
-- =============================================
CREATE PROCEDURE sp_addRendeles_to_Hitel 
	-- Add the parameters for the stored procedure here
	@p_partner_id int,
	@rendel_sor_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO HITEL_SOR (PARTNER_ID,RENDELES_SOR_ID,FIZETVE)
	VALUES (@p_partner_id,@rendel_sor_id,0)

	update RENDELES_SOR  SET FIZETVE = 1 WHERE SOR_ID = @rendel_sor_id


END
GO


CREATE TRIGGER tr_RendelSor_KeszletInsert 
   ON  RENDELES_SOR
   AFTER  INSERT
AS 
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
    -- Insert statements for trigger here

	declare  @cikk int, @rakt int, @sor_id int, @rendel_id int, @db float, @ertek float, @datum datetime

	select @cikk = i.CIKK_ID, @rakt = i.RAKTAR_ID, @sor_id = i.SOR_ID, @rendel_id = i.RENDELES_ID,
		 @db = i.DB, @ertek = dbo.fn_get_atlagar(i.CIKK_ID,i.RAKTAR_ID,f.EV,f.HO,f.NAP) , @datum = i.DATUM from inserted i
		left join CIKK c on i.CIKK_ID = c.CIKK_ID
		left join RENDELES_FEJ f on i.RENDELES_ID = f.RENDELES_ID

    
	exec sp_Do_Keszlet_vezetes @cikk, @rakt, 'F', @sor_id, @rendel_id, @db, @ertek, @datum

END


GO
-- =============================================
-- Author:		László Ernő
-- Create date: 2009.03.01
-- Description:	Sorszámok osztása
-- =============================================
CREATE PROCEDURE SP_GET_SORSZAM
	-- Add the parameters for the stored procedure here
	@TYPE varchar(10),
	@SZAM int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	IF NOT EXISTS(SELECT '' from _SORSZAM s WHERE s.TYPE = @TYPE)BEGIN
		INSERT INTO _SORSZAM ([TYPE],COUNTER) VALUES (@TYPE,0)

	END
	
	SELECT @SZAM = COUNTER+1 from _SORSZAM s WHERE s.TYPE = @TYPE
	
	UPDATE _SORSZAM  SET COUNTER = @SZAM WHERE [TYPE] = @TYPE

END

GO

-- =============================================
-- Author:		László Ernő
-- Create date: 2009.03.01
-- Description:	Számla készítés
-- =============================================
CREATE PROCEDURE sp_create_szamla_fej
	-- Add the parameters for the stored procedure here
	@p_partner_id int,
	@p_rendeles_id int,
	@p_fizmod int,
	@user_id int,
	@o_szamla_id int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @i_srsz int

	exec SP_GET_SORSZAM 'SZLA',@i_srsz output

	declare @a_ev int,
			@a_ho int,
			@a_nap int

	execute getnyitottNap @a_ev out, @a_ho out, @a_nap out

 INSERT INTO SZAMLA_FEJ (SZAMLA_SORSZAM, PARTNER_ID, RENDELES_ID, 
						OSSZESEN_NETTO, OSSZESEN_BRUTTO, OSSZESEN_AFA, KEDVEZMENY, 
						FIZETETT_OSSZEG, FIZETESI_MOD, SZAMLA_DATUMA, EV, HO, NAP, USER_ID)
     VALUES
           (@i_srsz, @p_partner_id, @p_rendeles_id, 
						0, 0, 0, 0, 
						0, @p_fizmod, getdate(), @a_ev, @a_ho, @a_nap, @user_id)   


SET @o_szamla_id = SCOPE_IDENTITY()

END
GO

-- =============================================
-- Author:		László Ernő
-- Create date: 2009.03.01
-- Description:	Számla tételek beszúrása
-- =============================================
CREATE PROCEDURE sp_add_szamla_tetel
	-- Add the parameters for the stored procedure here
	@p_szamla_fej_id int,
	@p_rendeles_sor_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO SZAMLA_TETEL
			   (RENDELES_SOR_ID, CIKK_ID, MENNYISEG, EGYSEGAR, NETTO, AFA, 
				BRUTTO, AFA_KOD, MEGJEGYZES, CIKK_MEGNEVEZES, SZAMLA_FEJ_ID, KALK_MENNYISEG, KEDVEZMENY)
		 SELECT s.SOR_ID, s.CIKK_ID, s.DB, s.NETTO_ERTEK, 1*s.NETTO_ERTEK, (1*s.NETTO_ERTEK-(s.KEDVEZMENY/(1+(a.AFA_ERTEK/100))))*(a.AFA_ERTEK/100), 
				(1*s.NETTO_ERTEK)*(1+ (a.AFA_ERTEK/100)), cs.AFA_KOD, '', case when isnull(l.LIT_KISZ_ID,-1) = -1 then c.MEGNEVEZES else l.LIT_KISZ_NEV + ' ' + c.MEGNEVEZES END, @p_szamla_fej_id, 1, s.KEDVEZMENY
		FROM RENDELES_SOR s
			inner join CIKK c on s.CIKK_ID = c.CIKK_ID
			inner join CIKKCSOPORT cs on c.CIKKCSOPORT_ID = cs.CIKKCSOPORT_ID
			inner join AFA a on cs.AFA_KOD = a.AFA_KOD
			left join LIT_KISZ l on s.LIT_KISZ_ID = l.LIT_KISZ_ID
		where s.SOR_ID = @p_rendeles_sor_id

	UPDATE RENDELES_SOR SET FIZETVE = 1 WHERE SOR_ID =@p_rendeles_sor_id
END

GO

CREATE PROCEDURE sp_add_egyeb_szamla_tetel
	-- Add the parameters for the stored procedure here
	@p_szamla_fej_id int,
	@p_partner_befizetes_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO SZAMLA_TETEL
			   (RENDELES_SOR_ID, CIKK_ID, MENNYISEG, EGYSEGAR, NETTO, AFA, 
				BRUTTO, AFA_KOD, MEGJEGYZES, CIKK_MEGNEVEZES, SZAMLA_FEJ_ID, KALK_MENNYISEG, KEDVEZMENY)
		 SELECT t.SOR_ID, -99, 0, t.OSSZEG, t.OSSZEG, t.OSSZEG, 
				t.OSSZEG, -99, t.MEGJEGYZES, t.JOGCIM, @p_szamla_fej_id, 1, 0
		FROM PARTNER_BEFIZETESEK t
		where t.SOR_ID = @p_partner_befizetes_id

	--UPDATE PARTNER_TARTOZASOK SET FIZETVE = OSSZEG WHERE SOR_ID =@p_partner_befizetes_id
END

GO


-- =============================================
-- Author:		László Ernő
-- Create date: 2009.03.01
-- Description:	Számla tételek beszúrása
-- =============================================
CREATE PROCEDURE sp_add_storno_szamla_tetel
	-- Add the parameters for the stored procedure here
	@p_szamla_fej_id int,
	@p_rendeles_sor_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO SZAMLA_TETEL
			   (RENDELES_SOR_ID, CIKK_ID, MENNYISEG, EGYSEGAR, NETTO, AFA, 
				BRUTTO, AFA_KOD, MEGJEGYZES, CIKK_MEGNEVEZES, SZAMLA_FEJ_ID, KALK_MENNYISEG, KEDVEZMENY)
		 SELECT s.SOR_ID, s.CIKK_ID, -1*s.DB, -1*s.NETTO_ERTEK, -1*s.NETTO_ERTEK, (-1*s.NETTO_ERTEK-(s.KEDVEZMENY/(1+(a.AFA_ERTEK/100))))*(a.AFA_ERTEK/100), 
				(-1*s.NETTO_ERTEK)*(1+ (a.AFA_ERTEK/100)), cs.AFA_KOD, '', case when isnull(l.LIT_KISZ_ID,-1) = -1 then c.MEGNEVEZES else l.LIT_KISZ_NEV + ' ' + c.MEGNEVEZES END, @p_szamla_fej_id, 1, s.KEDVEZMENY
		FROM RENDELES_SOR s
			inner join CIKK c on s.CIKK_ID = c.CIKK_ID
			inner join CIKKCSOPORT cs on c.CIKKCSOPORT_ID = cs.CIKKCSOPORT_ID
			inner join AFA a on cs.AFA_KOD = a.AFA_KOD
			left join LIT_KISZ l on s.LIT_KISZ_ID = l.LIT_KISZ_ID
		where s.SOR_ID = @p_rendeles_sor_id

	UPDATE RENDELES_SOR SET FIZETVE = 1 WHERE SOR_ID =@p_rendeles_sor_id
END

GO

-- =============================================
-- Author:		László Ernő
-- Create date: 2009.03.07
-- Description:	Aztalok foglaltságának javítása
-- =============================================
CREATE PROCEDURE sp_repair_Tables
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


    	update RENDELES_FEJ SET  AKTIV = 0
	from rendeles_fej f
	where  f.aktiv = 1 and 
			not exists (SELECT '' from RENDELES_SOR s 
							where s.SOR_ID not in (
								SELECT sa.SOR_ID FROM RENDELES_SOR sa where f.rendeles_id = sa.rendeles_id and isnull(sa.DELETED,0) = 0 and isnull(sa.FIZETVE,0) = 1
								UNION
								SELECT sb.SOR_ID FROM RENDELES_SOR sb where f.rendeles_id = sb.rendeles_id and isnull(sb.FIZETVE,0) = 0 and isnull(sb.DELETED,0) = 1
								UNION
								SELECT sc.SOR_ID FROM RENDELES_SOR sc where f.rendeles_id = sc.rendeles_id and isnull(sc.FIZETVE,0) = 1 and isnull(sc.DELETED,0) = 1
					) and f.rendeles_id = s.rendeles_id)

--	update RENDELES_FEJ SET FIZETVE = 1, AKTIV = 0
--	from rendeles_fej f
--	left join rendeles_sor s on f.rendeles_id = s.rendeles_id and isnull(s.DELETED,0) = 1 and isnull(s.FIZETVE,0) = 1
--	where s.sor_id is null


END
GO

-- =============================================
-- Author:		László Ernő
-- Create date: 2009.03.08
-- Description:	Felhasználó belépés
-- =============================================
CREATE PROCEDURE sp_login
	-- Add the parameters for the stored procedure here
	@user_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @a_ev int,
			@a_ho int,
			@a_nap int

	execute getnyitottNap @a_ev out, @a_ho out, @a_nap out


    INSERT INTO _USER_LOG (_USER_ID, EV, HO, NAP, INOUT, LOG_DATE)
     VALUES (@user_id, @a_ev, @a_ho, @a_nap, 1, getdate())
						
END

GO

CREATE function fn_get_AfaSzaz(@cikk_id int)
returns float
as 
begin

return (select AFA_ERTEK from AFA a
inner join CIKKCSOPORT cs on a.AFA_KOD = cs.AFA_KOD
inner join CIKK c on cs.CIKKCSOPORT_ID = c.CIKKCSOPORT_ID
WHERE c.CIKK_ID = @cikk_id)


end

GO

-- =============================================
-- Author:		László Ernő
-- Create date: 2009.03.01
-- Description:	Felhasználó belépés
-- =============================================
CREATE PROCEDURE sp_logout
	-- Add the parameters for the stored procedure here
	@user_id int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @a_ev int,
			@a_ho int,
			@a_nap int

	execute getnyitottNap @a_ev out, @a_ho out, @a_nap out


    INSERT INTO _USER_LOG (_USER_ID, EV, HO, NAP, INOUT, LOG_DATE)
     VALUES (@user_id, @a_ev, @a_ho, @a_nap, -1, getdate())
						
END

GO


-- =============================================
-- Author:		László Ernő>
-- Create date: 2009.04.28
-- Description:	Selejtezést végrehajtó függvény
-- =============================================
CREATE PROCEDURE [dbo].[SP_KESZLET_SELEJT]
	-- Add the parameters for the stored procedure here
	@from_raktar int,
	@cikk_id int,
	@menny float
	
AS
BEGIN
	
	SET NOCOUNT ON;

declare @a_ev int,
        @a_ho int,
        @a_nap int,
		@afasz float,
		@afa_kod varchar(2)

execute getnyitottNap @a_ev out, @a_ho out, @a_nap out
   
select @afa_kod = a.AFA_KOD, @afasz = a.AFA_ERTEK from aFA a inner join CIKKCSOPORT cs on cs.AFA_KOD = a.AFA_KOD
inner join CIKK c on c.CIKKCSOPORT_ID = cs.CIKKCSOPORT_ID
where c.CIKK_ID = @cikk_id

-- csökkentő tétel
INSERT INTO KESZLET_SOR
           (EV,HO,NAP,RAKTAR_ID,CIKK_ID,DATUM,RENDELES_ID,BEVETEL_ID
			,BIZONYLAT_ID,MENNYISEG,IRANY,EGYSEGAR,NETTO_ERTEK,AFA_ERTEK
			,AFA_KOD,BRUTTO_ERTEK,MOZGAS_TIPUS,KESZLET_EGYS_AR)
     VALUES
           (@a_ev,@a_ho,@a_nap,@from_raktar,@cikk_id,getdate(),null,null
			,null,@menny,-1,dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap),dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap)*@menny, (dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap)*@menny)*(@afasz/100)
			,@afa_kod,(dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap)*@menny)*(1+(@afasz/100)),'S',dbo.fn_get_Atlagar(@cikk_id,@from_raktar,@a_ev,@a_ho,@a_nap))


    
END

GO

-- =============================================
-- Author:		László Ernő
-- Create date: 2009.04.30
-- Description:	Leltározás
-- =============================================
CREATE PROCEDURE [dbo].[sp_create_leltariv]
	-- Add the parameters for the stored procedure here
	@p_cikkcsop_id int,
	@p_raktar_id int,
	@p_leltar_id int output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    if exists (SELECT '' FROM LELTAR_FEJ WHERE RAKTAR_ID = @p_raktar_id and CIKKCSOPORT_ID = @p_cikkcsop_id and LEZART = 0) begin

		set @p_leltar_id = -99

	end else begin
		insert into leltar_fej (DATUM, RAKTAR_ID, CIKKCSOPORT_ID, LEZART, STATUS)
			VALUES (getdate(),@p_raktar_id, @p_cikkcsop_id,0,1)
		SET @p_leltar_id = SCOPE_IDENTITY()


			declare @ev int,
					@ho int,
					@nap int

			execute getnyitottNap @ev out, @ho out, @nap out
        
			insert into LELTAR_SOR (LELTAR_FEJ_ID, CIKK_ID, AKT_KESZLET_MENNY,SZAMOLT_MENNYISEG1,SZAMOLT_MENNYISEG2)
					SELECT @p_leltar_id, f.CIKK_ID,  CASE WHEN cc.VIRTUAL = 1 THEN 99999999 
									  ELSE Round(isnull(f.KESZLET_NYITO,0) + isnull(f.KESZLET_NAPI,0),2) END,
								0,0
									  
								FROM KESZLET_FEJ f inner join RAKTAR r on f.RAKTAR_ID = r.RAKTAR_ID
								left join CIKK cc on f.cikk_id = cc.cikk_id
								where  f.raktar_id = 1 and cc.VIRTUAL = 0
									and f.EV = @ev and f.nap = @nap and f.ho = @ho


	end

END

GO
