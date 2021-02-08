
set ansi_padding on
go

set quoted_identifier on

go

set ansi_nulls on

go 

USE master

go

--DROP DATABASE [SILVER]

--go

CREATE DATABASE [SILVER]

go

USE [SILVER]

go

CREATE TABLE [dbo].[Doljnost]
(
	[ID_Doljnost] [int] IDENTITY (1,1) ,
	[Naimenovanie] [varchar](30)  NOT NULL ,
	[Oklad] [int]  NOT NULL 
	CONSTRAINT [PK_Doljnost] PRIMARY KEY  CLUSTERED ([ID_Doljnost] ASC) on [PRIMARY],
	CONSTRAINT [UQ_Naimenovanie] UNIQUE ([Naimenovanie])
)
go




CREATE TABLE [dbo].[Grafik]
(
	[ID_Grafik] [int] IDENTITY (1,1) ,
	[Nazvanie] [varchar](30)  NOT NULL ,
	[Nachalo] [varchar] (30)  NOT NULL ,
	[Konec] [varchar] (30)  NOT NULL 
	CONSTRAINT [PK_Grafik] PRIMARY KEY  CLUSTERED ([ID_Grafik] ASC) on [PRIMARY],
	CONSTRAINT [UQ_Nazvanie] UNIQUE ([Nazvanie])
)
go


CREATE TABLE [dbo].[Otbor]
(
	[ID_Otbor] [int] IDENTITY (1,1) ,
	[Familiya] [varchar](30)  NOT NULL ,
	[Imya] [varchar](30)  NOT NULL ,
	[Otchestvo] [varchar](30)  NOT NULL ,
	[Pasport] [varchar](10)  NOT NULL ,
	[Opit] [int]  NOT NULL ,
	[Login] [varchar] NOT NULL,
	[Password] [varchar] NOT NULL,
	[ID_Grafik] [int]  NULL,
	[ID_Doljnost] [int]  NULL
	CONSTRAINT [PK_Otbor] PRIMARY KEY CLUSTERED ([ID_Otbor] ASC) on [PRIMARY],
	CONSTRAINT [UQ_Pasport] unique ([Pasport]),
	CONSTRAINT [FK_Grafic_Otbor] FOREIGN KEY ([ID_Grafik]) REFERENCES Grafik([ID_Grafik]),
	CONSTRAINT [FK_Doljnost_Otbor] FOREIGN KEY ([ID_Doljnost]) REFERENCES Doljnost([ID_Doljnost]),
)
go

CREATE TABLE [dbo].[Nomer]
(
	[ID_Nomer] [int] IDENTITY (1,1) ,
	[Nom] [int]  NOT NULL ,
	[Status] [varchar](30)  NOT NULL ,
	[Klass] [varchar](30)  NOT NULL ,
	[ID_Otbor] [int]  NULL 
	CONSTRAINT [PK_Nomer] PRIMARY KEY  CLUSTERED ([ID_Nomer] ASC) on [PRIMARY],
	CONSTRAINT [UQ_Nom] UNIQUE (Nom),
	CONSTRAINT [FK_Otbor_Nomer] FOREIGN KEY ([ID_Otbor]) REFERENCES [dbo].[Otbor]([ID_Otbor])
)
go

INSERT INTO [dbo].[Doljnost]([Naimenovanie], [Oklad])
VALUES 
	('Уборщик',400),
	('Бармен',800),
	('Бухгалтер',1600),
	('Директор',2500),
	('Заместитель директора', 2000)
go



INSERT INTO [dbo].[Grafik]([Nazvanie], [Nachalo], [Konec])
VALUES 
	('1 смена', '8:00', '15:00'),
	('2 смена', '15:00', '22:00'),
	('Полный день', '8:00', '22:00'),
	('Вахта','22:00', '8:00')
go


go

INSERT INTO [dbo].[Otbor]([Familiya], [Imya], [Otchestvo], [Pasport], [Opit], [Login], [Password], [ID_Grafik], [ID_Doljnost])
VALUES 
	('Белозёров','Григорий','Романович','9014196234',2, '1', '1',NULL,NULL),
	('Богданова','Домна','Платоновна',	'6014154643',5, '2', '2',1,1),
	('Недашковский',	'Федосий',	'Георгиевич',	'9415964285',	2, '3', '3',	3,	1),
	('Кудрявцев',	'Иннокентий',	'Тимурович',	'7607279645',	4, '4', '4',	4,	2),
	('Городнов',	'Фрол',	'Вячеславович',	'3705275947',	9, '5', '5',	3,	3)

	INSERT INTO [dbo].[Nomer]([Nom],[Status],[Klass],[ID_Otbor])
VALUES 
	(1,	'Да',	'Эконом',	2),
	(2,	'Нет',	'Эконом',	3),
	(3,	'Да',	'Люкс',	3),
	(4,	'Нет',	'Эконом',	2),
	(5,	'Нет',	'Люкс',	3)




go
--Процедура добавления должности
CREATE PROCEDURE [dbo].[Doljnost_Insert]
@Naimenovanie [varchar](30), @Oklad [int]
as
--Проверка на уникальность
		declare @ID [int] = (select COUNT(*) from [dbo].[Doljnost]
		where [dbo].[Doljnost].[Naimenovanie] = @Naimenovanie )
		if @ID>0
			print('Должность с таким наименованием уже есть')
		else

		begin
			INSERT INTO [dbo].[Doljnost] ([Naimenovanie],[Oklad])
			VALUES (@Naimenovanie, @Oklad)
		end
go
--Процедура обновления должности
CREATE PROCEDURE [dbo].[Doljnost_Update]
@ID_Doljnost [int],@Naimenovanie [varchar](30), @Oklad [int]
as


		begin
			UPDATE [dbo].[Doljnost] set
			[Naimenovanie] = @Naimenovanie,
			[Oklad]=@Oklad
		
			WHERE
			[ID_Doljnost] = @ID_Doljnost
		end
go
--Процедура удаления должности
CREATE PROCEDURE [dbo].[Doljnost_Delete]
@ID_Doljnost [int]
as

	begin
		DELETE FROM[dbo].[Doljnost]
		WHERE [dbo].[Doljnost].[ID_Doljnost] = @ID_Doljnost
	end

go
--Процедура добавления графика
CREATE PROCEDURE [dbo].[Grafik_Insert]
@Nazvanie [varchar](30)  ,@Nachalo [varchar] (30) ,@Konec [varchar] (30) 
as
--Проверка на уникальность
	declare @ID [int] = (select COUNT(*) from [dbo].[Grafik]
	where [dbo].[Grafik].[Nazvanie] = @Nazvanie )
	if @ID>0
		print('График с таким названием уже есть')
	else

	begin
		INSERT INTO [dbo].[Grafik] ([Nazvanie], [Nachalo],[Konec])
		VALUES (@Nazvanie,@Nachalo,@Konec)
	end
go
--Процедура обновления графика
CREATE PROCEDURE [dbo].[Grafik_Update]
@ID_Grafik [int],@Nazvanie [varchar](30)  ,@Nachalo [varchar] (30) ,@Konec [varchar] (30) 
as

	begin
		UPDATE [dbo].[Grafik] set
		[Nazvanie] = @Nazvanie,
		[Nachalo] = @Nachalo,
		[Konec] = @Konec
		
		WHERE
		[ID_Grafik] = @ID_Grafik
	end
go
--Процедура удаления графика
CREATE PROCEDURE [dbo].[Grafik_Delete]
@ID_Grafik [int]
as
--Проверка на существование работников с этим графиком
	declare @ID [int] = (select COUNT(*) from [dbo].[Otbor]
	where [dbo].[Otbor].[ID_Grafik] = @ID_Grafik)
	if @ID>0
		print('Есть сотрудники с таким графиком')
	else
	begin
		DELETE FROM[dbo].[Grafik]
		WHERE [dbo].[Grafik].[ID_Grafik] = @ID_Grafik
	end

go
--Процедура добавления отбираемых
CREATE PROCEDURE [dbo].[Otbor_Insert]
	@Familiya [varchar](30)  ,
	@Imya [varchar](30)   ,
	@Otchestvo [varchar](30)  ,
	@Pasport [varchar](10)  ,
	@Opit [int]  ,
	@Login [varchar] (30),
	@Password [varchar] (30),
	@ID_Grafik [int] ,
	@ID_Doljnost [int]   
as
--Проверка на уникальность
	declare @ID [int] = (select COUNT(*) from [dbo].[Otbor]
	where [dbo].[Otbor].[Pasport] = @Pasport )
	if @ID>0
		print('Такой работник уже есть')
	else

	begin
		INSERT INTO [dbo].[Otbor] ([Familiya] ,[Imya]  ,[Otchestvo] ,
		[Pasport]  ,[Opit] , [Login], [Password],[ID_Grafik] ,[ID_Doljnost])
		VALUES (@Familiya,@Imya,@Otchestvo, @Pasport, @Opit, @Login, @Password, @ID_Grafik, @ID_Doljnost)
	end
go
--Процедура обновления отбираемых
CREATE PROCEDURE [dbo].[Otbor_Update]
	@ID_Otbor [int],
	@Familiya [varchar](30)  ,
	@Imya [varchar](30)   ,
	@Otchestvo [varchar](30)  ,
	@Pasport [varchar](10)  ,
	@Opit [int]  ,
	@Login [varchar] (30),
	@Password [varchar] (30),
	@ID_Grafik [int] ,
	@ID_Doljnost [int] 
as


	begin
		UPDATE [dbo].[Otbor] set
		[Familiya] = @Familiya,
		[Imya] = @Imya,
		[Otchestvo] = @Otchestvo,
		[Pasport] = @Pasport,
		[Opit] = @Opit,
		[Login] = @Login,
		[Password] = @Password,
		[ID_Grafik] = @ID_Grafik,
		[ID_Doljnost] = @ID_Doljnost
		
		WHERE
		[ID_Otbor] = @ID_Otbor
	end;
go
--Процедура удаления отбираемых
CREATE PROCEDURE [dbo].[Otbor_Delete]
@ID_Otbor [int]
as
	
		DELETE FROM[dbo].[Otbor]
		WHERE [dbo].[Otbor].[ID_Otbor] = @ID_Otbor

go
--Процедура добавления номеров
CREATE PROCEDURE [dbo].[Nomer_Insert]
	@Nom [int]  ,
	@Status [varchar](30) ,
	@Klass [varchar](30)  ,
	@ID_Otbor [int]  
as
	declare @ID [int] = (select COUNT(*) from [dbo].[Nomer]
	where [dbo].[Nomer].[Nom] = @Nom )
	if @ID>0
		print('Такой номер уже есть')
	else

	begin
		INSERT INTO [dbo].[Nomer] ([Nom],[Status],[Klass], [ID_Otbor])
		VALUES (@Nom,@Status,@Klass,@ID_Otbor)
	end
go
--Процедура обновления номеров
CREATE PROCEDURE [dbo].[Nomer_Update]
	@ID_Nomer [int],
	@Nom [int]  ,
	@Status [varchar](30) ,
	@Klass [varchar](30)  ,
	@ID_Otbor [int] 
as


	begin	
		UPDATE [dbo].[Nomer] set
		[Nom] = @Nom,
		[Status] = @Status,
		[Klass] = @Klass,
		[ID_Otbor] = @ID_Otbor
		
		WHERE
		[ID_Nomer] = @ID_Nomer
	end
go
--Процедура удаления номеров
CREATE PROCEDURE [dbo].[Nomer_Delete]
@ID_Nomer [int]
as
		
		DELETE FROM[dbo].[Nomer]
		WHERE [dbo].[Nomer].[ID_Nomer] = @ID_Nomer

go

create function [dbo].[Authorization] (@login [varchar] (16), @password [varchar] (16))
returns [int]
with execute as caller 
as 
begin
	declare @ID_Record [int] = (select [ID_Otbor] from [dbo].[Otbor] where [Login] = @login and [Password] = @password)
	if @ID_Record is null
		begin
			set @ID_Record = 0
		end
	return(@ID_record)
end
go