
CREATE FUNCTION SiparisSayisi() --toplam siparis sayisini d�nd�ren fonksiyon
RETURNS INT
AS
BEGIN
    DECLARE @SiparisSayisi INT --int de�i�ken atama
    SELECT @SiparisSayisi = Count(*) --sipari�ler tablosundaki ��elerin say�s�n� d�nd�r�r
    FROM Siparisler

    IF @SiparisSayisi IS NULL --de�er null ise 0 atar
    BEGIN
        SET @SiparisSayisi = 0
    END
    RETURN @SiparisSayisi --de�i�keni d�nd�r�r
END

CREATE FUNCTION SiparistenToplamKazanc() --t�m sipari�lerin de�erini d�nd�ren fonksiyon
RETURNS INT
AS
BEGIN
    DECLARE @SiparisKazanci INT --de�i�ken atama
    SELECT @SiparisKazanci = Sum(Fiyat) --t�m fiyat bilgisini toplar	
    FROM Siparisler
    IF @SiparisKazanci IS NULL --de�er null ise de�i�kene 0 atar
    BEGIN
        SET @SiparisKazanci = 0
    END
    RETURN @SiparisKazanci 
END

CREATE FUNCTION SiparisiOlanSaticiSayisi() --aktif sipari�i olan sat�c�lar� d�nd�ren fonksiyon
RETURNS INT
AS
BEGIN
    DECLARE @SiparistekiSaticilar INT --
    SELECT @SiparistekiSaticilar = count(distinct(Satici)) --farkl� sat�c�lar�n say�s�n� de�i�kene atar
    FROM Siparisler
    IF @SiparistekiSaticilar IS NULL --satici say�s� null ise 0 atar
    BEGIN
        SET @SiparistekiSaticilar = 0
    END
    RETURN @SiparistekiSaticilar --sat�c�lar�n de�erini d�nd�r�r 
END



ALTER FUNCTION ToplamKazancSatici --girilen sat�c�n�n toplam yapt��� �r�n sat���n� d�nd�ren fonksiyon
(
    @Satici nvarchar(50) --satici de�i�keni tan�mland�
)
RETURNS INT --int de�eri d�nd�r�r
AS
BEGIN
    DECLARE @ToplamKazanc INT  --int de�i�keni tan�mland�

    SELECT @ToplamKazanc = Sum(Fiyat) --fiyatlar�n toplmam� de�i�kene atand�
    FROM Siparisler                    
    WHERE Satici = @Satici   --sipari�ler tablosundaki Sat�c� de�i�keni ile e�le�tirlidi

    RETURN @ToplamKazanc  --de�i�keni d�nd�r�r 
END

create function fn_tarihFarki(@UretimTarihi datetime) --Tarih fark�n� hesaplamak i�in kullan�lan fonksiyon
Returns nvarchar(25)
as
begin
declare @suan datetime   --anl�k tarih verisi de��ken atan�r
declare @fark nvarchar(25) --fark hesab� i�in de�i�ken atan�r
set @suan=GETDATE()  --anl�k tarih atan�r
set @fark= DATEDIFF(DAY,@UretimTarihi,@suan) --datediff ile tarihler aras� fark hesaplan�r
return @fark  --hesaplanan fark d�nd�r�l�r
end  

ALTER VIEW EnCokSatanUrunler AS  --En �ok satan �r�nleri g�rmek i�in kulland���m�z view
SELECT TOP 10                     --�r�n id e g�re group by yapt�k ve �r�n fiyat sat�c� adet g�r�nt�ledik
       UrunID,												
       MAX(Urun) AS Urun,
	   MIN(Fiyat) as Fiyat,
	   MAX(Satici) as Sat�c�,
       SUM(Adet) AS ToplamSatisMiktari 
FROM   Siparisler
GROUP BY UrunID        --i�lemler urunide g�re yap�ld�
ORDER BY ToplamSatisMiktari DESC; --b�y�kten k����e s�ralama yap�l�r

-----------------------------------------------------------------

CREATE PROCEDURE up_UrunEkle --�r�n eklemek i�i stored procedure
    @ukid INT,				 --�r�nlerin de�i�ken atamalar� yap�l�r
    @utip NVARCHAR(50),
    @umarka NVARCHAR(50),
    @umodel NVARCHAR(50),
    @ustok INT,
    @ufiyat INT,
    @uuretim DATETIME,
    @ugaranti INT,
    @usatici NVARCHAR(50)
AS
BEGIN  --insert into ile de�i�kenler Urunler tablosuna eklenir
    INSERT INTO Urunler(KategoriID, UrunTipi, Marka, Model, StokMiktari, Fiyat, UretimTarihi, Garanti, Satici)
    VALUES (@ukid, @utip, @umarka, @umodel, @ustok, @ufiyat, @uuretim, @ugaranti, @usatici)
END

CREATE PROCEDURE up_UrunGuncelle --�r�n g�ncellemek i�in stored procedure
    @uid INT,					 --de�i�kneler atan�r	
    @ukid INT,
    @utip NVARCHAR(50),
    @umarka NVARCHAR(50),
    @umodel NVARCHAR(50),
    @ustok INT,
    @ufiyat INT,
    @uuretim DATETIME,
    @ugaranti INT
AS
BEGIN
    UPDATE Urunler  --update ile de�i�kenlerin yeni de�erleri ile Urunler tablosunda g�ncelleme yap�l�r
    SET
        KategoriID = @ukid,
        UrunTipi = @utip,
        Marka = @umarka,
        Model = @umodel,
        StokMiktari = @ustok,
        Fiyat = @ufiyat,
        UretimTarihi = @uuretim,
        Garanti = @ugaranti
    WHERE UrunID = @uid; --�r�n ide g�re i�lemler yap�l�r
END

CREATE PROCEDURE up_KullaniciEkle --Kullan�c� eklemek i�in stored procedure
    @kad NVARCHAR(50),			  --de�i�kenler tan�mlan�r
    @ksoyad NVARCHAR(50),
    @ksifre NVARCHAR(50),
    @kkayit DATETIME,
    @kyas INT,
    @kcinsiyet NVARCHAR(10),
    @keposta NVARCHAR(50),
    @ktelefon NVARCHAR(20)
AS
BEGIN						--insert into ile kullan�c�lara eklenir
    INSERT INTO Kullanicilar(Ad, Soyad, Sifre, KayitTarihi, Yas, Cinsiyet, Eposta, Telefon)
    VALUES (@kad, @ksoyad, @ksifre, @kkayit, @kyas, @kcinsiyet, @keposta, @ktelefon)
END

CREATE PROCEDURE up_KullaniciGuncelle --Kullan�c� g�ncellemek i�in stored procedure
    @kid INT,			
    @kad NVARCHAR(50),
    @ksoyad NVARCHAR(50),
    @ksifre NVARCHAR(50),
    @kkayit DATETIME,
    @kyas INT,
    @kcinsiyet NVARCHAR(10),
    @keposta NVARCHAR(50),
    @ktelefon NVARCHAR(20)
AS
BEGIN
    UPDATE Kullanicilar --update ile kullanicilar tablosu g�ncellenir
    SET
        Ad = @kad,
        Soyad = @ksoyad,
        Sifre = @ksifre,
        KayitTarihi = @kkayit,
        Yas = @kyas,
        Cinsiyet = @kcinsiyet,
        Eposta = @keposta,
        Telefon = @ktelefon
    WHERE KullaniciID = @kid;  --i�lemler kullan�c� ide g�re yap�l�r
END

CREATE PROCEDURE up_SaticiEkle --Sat�c� eklemek i�in stored procedure
    @sad NVARCHAR(50),
    @seposta NVARCHAR(50),
    @stelefon NVARCHAR(20),
    @sifre NVARCHAR(50)
AS
BEGIN				--saticilar tablosuna ekleme yap�l�r
    INSERT INTO Saticilar(SaticiMagazaAdi, SaticiEposta, SaticiTelefon, SaticiSifre)
    VALUES (@sad, @seposta, @stelefon, @sifre) 
END

CREATE PROCEDURE up_SaticiGuncelle --Sat�c� G�ncellemek i�in stored procedure
    @sid INT,
    @sad NVARCHAR(50),
    @seposta NVARCHAR(50),
    @stelefon NVARCHAR(20),
    @sifre NVARCHAR(50)
AS
BEGIN
    UPDATE Saticilar --update ile sat�c�lar tablosu g�ncellenir
    SET
        SaticiMagazaAdi = @sad,
        SaticiEposta = @seposta,
        SaticiTelefon = @stelefon,
        SaticiSifre = @sifre
    WHERE SaticiID = @sid;
END

alter procedure up_kategoriListesi --kategorileri �ekmek i�in kullan�lan stored procedure
as
begin
select UrunTipi From Urunler group by UrunTipi order by count(UrunTipi) desc 
end

CREATE PROCEDURE up_UrunAlindi  
(      --bir �r�n al�nd��� zaman al�nan miktara g�re stoktan azaltan stored procedure
    @UrunID INT,  
    @AlinanMiktar INT --c# da textboxtan al�nacak al�nan �r�n miktar� tan�mlanmas�
)
AS
BEGIN
    DECLARE @EskiStokMiktari INT --stok miktar�n�n ilk hali		
    DECLARE @YeniStokMiktari INT --stok miktar�n�n ��kar�ld�ktan sonraki hali

    SELECT @EskiStokMiktari = StokMiktari 
    FROM Urunler
    WHERE UrunID = @UrunID
    SET @YeniStokMiktari = @EskiStokMiktari - @AlinanMiktar --stok miktar�ndan al�nan miktar ��kar�l�r

    UPDATE Urunler --�r�nler tablosu yeni de�erler ile g�ncellenir
    SET StokMiktari = @YeniStokMiktari --stok yeni stok durumu ile de�i�trilir	
    WHERE UrunID = @UrunID --i�lem �r�n ide g�re yap�l�r
END

---------------------------------------------
---------------TRIGGERLAR--------------------
---------------------------------------------

ALTER TRIGGER trg_Urunler_Insert --Urun tablosuna �r�n eklend�inde devreye giren trigger
ON [dbo].[Urunler]		--�r�n tablosunda �al���r
FOR INSERT
AS
BEGIN
    DECLARE @id INT       --de�i�kenler tan�mlan�r
    DECLARE @uruntipi NVARCHAR(50)
    DECLARE @marka NVARCHAR(50)
    DECLARE @model NVARCHAR(50)
    DECLARE @stokmiktari INT
    DECLARE @fiyat INT
    DECLARE @satici NVARCHAR(50)

    SELECT @id = UrunID,   --de�i�ken�er tablodaki de�erlerle e�le�tirilir
	@uruntipi = UrunTipi, 
	@marka = Marka,
	@model = Model,
	@stokmiktari = StokMiktari, 
	@fiyat = Fiyat,
	@satici = Satici 
	FROM inserted  --ekleme yap�ld��� i�in ge�ici inserted kullan�l�r

	--parametrelere g�re OlaylarUrun tablosuna ekleme yap�l�r
    INSERT INTO [dbo].[OlaylarUrun] VALUES ('ID: '+CAST(@id AS NVARCHAR(10)) + ' ' + @marka + ' ' + @model + ' ' + @uruntipi 
	+ ' Stok Miktar�: ' + CAST(@stokmiktari AS NVARCHAR(10)) + ' Fiyat: ' + CAST(@fiyat AS NVARCHAR(10))
	+ ' TL. Sat�c� ' + @satici +' Taraf�ndan '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Eklendi.')
END

ALTER TRIGGER trg_Urunler_Delete --�r�n tablosundan �r�n silindi�inde devreye giren trigger
ON [dbo].[Urunler]
FOR DELETE
AS
BEGIN
    DECLARE @id INT 
    DECLARE @uruntipi NVARCHAR(50)
    DECLARE @marka NVARCHAR(50)
    DECLARE @model NVARCHAR(50)
    DECLARE @stokmiktari INT
    DECLARE @fiyat INT
    DECLARE @satici NVARCHAR(50)

	--de�i�kenler tan�mlan�r
    SELECT @id = UrunID, @uruntipi = UrunTipi, @marka = Marka, @model = Model, @stokmiktari = StokMiktari, @fiyat = Fiyat, @satici = Satici
	FROM deleted --silinme i�lemi yap�ld��� i�in ge�ici deleted tablosu kullan�ld�

	--OlaylarUrun tablosuna silinen �r�nler eklenir
    INSERT INTO [dbo].[OlaylarUrun] VALUES ('ID: '+CAST(@id AS NVARCHAR(10)) + ' ' + @marka + ' ' + @model + ' ' + @uruntipi 
	+ ' Stok Miktar�: ' + CAST(@stokmiktari AS NVARCHAR(10)) + ' Fiyat: ' + CAST(@fiyat AS NVARCHAR(10))
	+ ' TL. Sat�c� ' + @satici +' Taraf�ndan '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Silindi.')
END


CREATE TRIGGER trg_Kullanicilar_Insert --kullan�c�lar tablosuna kullan�c� eklendi�inde devreye giren trigger
ON Kullanicilar
FOR INSERT
AS
BEGIN
    DECLARE @id INT
    DECLARE @ad NVARCHAR(50)
    DECLARE @soyad NVARCHAR(50)
    DECLARE @cinsiyet NVARCHAR(50)
    DECLARE @yas INT
	DECLARE @eposta NVARCHAR(80)
   
    SELECT @id = KullaniciID, @ad = Ad, @soyad = Soyad, @cinsiyet = Cinsiyet, @yas = Yas, @eposta = Eposta FROM inserted

    INSERT INTO OlaylarKullanici VALUES ('ID: '+CAST(@id AS NVARCHAR(10)) + ' ' + @ad + ' ' + @soyad + ' ' + @cinsiyet 
	+ CAST(@yas AS NVARCHAR(10)) + ' E-Posta: ' + @eposta+ ' Kullan�c�s� '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Eklendi.')
END

ALTER TRIGGER trg_Kullanicilar_Delete --kullan�c�lar tablosundan kullan�c� silindi�inde devreye giren trigger
ON Kullanicilar
FOR DELETE
AS
BEGIN
    DECLARE @id INT
    DECLARE @ad NVARCHAR(50)
    DECLARE @soyad NVARCHAR(50)
    DECLARE @cinsiyet NVARCHAR(50)
    DECLARE @yas INT
	DECLARE @eposta NVARCHAR(80)
   
    SELECT @id = KullaniciID, @ad = Ad, @soyad = Soyad, @cinsiyet = Cinsiyet, @yas = Yas, @eposta = Eposta FROM deleted

    INSERT INTO OlaylarKullanici VALUES ('ID: '+CAST(@id AS NVARCHAR(10)) + ' ' + @ad + ' ' + @soyad + ' ' + @cinsiyet 
	+ CAST(@yas AS NVARCHAR(10)) + ' E-Posta: ' + @eposta+ ' Kullan�c�s� '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Silindi.')
END

ALTER TRIGGER trg_Satici_Insert --sat�c� tablosuna sat�c� eklendi�inde devreye giren trigger
ON Saticilar
FOR INSERT
AS
BEGIN
    DECLARE @id INT
    DECLARE @saticiad NVARCHAR(50)
    DECLARE @saticieposta NVARCHAR(80)
    DECLARE @saticitelefon VARCHAR(50)
  
    SELECT @id = SaticiID, @saticiad = SaticiMagazaAdi, @saticieposta = SaticiEposta, @saticitelefon = SaticiTelefon FROM inserted

    INSERT INTO OlaylarSatici VALUES ('ID: '+CAST(@id AS NVARCHAR(10)) + ' ' + @saticiad + ' E-Posta: ' + @saticieposta + ' Telefon: '  
	+ CAST(@saticitelefon AS NVARCHAR(10)) +' Sat�c�s� '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Eklendi.')
END

CREATE TRIGGER trg_Satici_Delete --saticilar tablosundan satici silindi�inde devreye giren trigger
ON Saticilar
FOR DELETE
AS
BEGIN
    DECLARE @id INT
    DECLARE @saticiad NVARCHAR(50)
    DECLARE @saticieposta NVARCHAR(80)
    DECLARE @saticitelefon VARCHAR(50)
  
    SELECT @id = SaticiID, @saticiad = SaticiMagazaAdi, @saticieposta = SaticiEposta, @saticitelefon = SaticiTelefon FROM deleted

    INSERT INTO OlaylarSatici VALUES ('ID: '+CAST(@id AS NVARCHAR(10)) + ' ' + @saticiad + ' E-Posta: ' + @saticieposta + ' Telefon: '  
	+ CAST(@saticitelefon AS NVARCHAR(10)) +' Sat�c�s� '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Silindi.')
END


---------------------------------------
---------------------------------------
--BU KISIMDA �STAT�ST�K VER�LER� ���N DAHA BAS�T STORED PROCEDURELAR KULLANILDI--
---------------------------------------
create procedure up_toplamKullanici
as
begin
select count(*) from Kullanicilar
end

exec up_toplamKullanici

create procedure up_erkekKullanici
as
begin
select count(*) from Kullanicilar where Cinsiyet='Erkek'
end

exec up_erkekKullanici

create procedure up_kizKullanici
as
begin
select count(*) from Kullanicilar where Cinsiyet='K�z'
end

exec up_kizKullanici

alter procedure up_enGencKullanici
as
begin
select Top(1) Ad +' '+ Soyad+' ('+CAST(Yas AS nvarchar(25)) +' Ya�)' From Kullanicilar order by Yas asc
end

exec up_enGencKullanici

alter procedure up_enYasliKullanici
as
begin
select Top(1) Ad +' '+ Soyad+' ('+CAST(Yas AS nvarchar(25)) +' Ya�)' From Kullanicilar order by Yas desc
end

exec up_enYasliKullanici

alter procedure up_enEskiKayitliKullanici
as
begin
select Top(1) Ad +' '+ Soyad+' ('+CAST(KayitTarihi AS nvarchar(25))+')  ('+dbo.fn_tarihFarki(KayitTarihi) +' G�n)' from Kullanicilar order by KayitTarihi asc
end


exec up_enEskiKayitliKullanici

alter procedure up_enYeniKayitliKullanici
as
begin
select Top(1) Ad +' '+ Soyad+' ('+CAST(KayitTarihi AS nvarchar(25))+')  ('+dbo.fn_tarihFarki(KayitTarihi) +' G�n)' from Kullanicilar order by KayitTarihi desc
end

exec up_enYeniKayitliKullanici

create procedure up_18yasAltiKullanici
as
begin
select count(*) from Kullanicilar where Yas<18
end

exec up_18yasAltiKullanici

create procedure up_18yasUstuKullanici
as
begin
select count(*) from Kullanicilar where Yas>17
end

exec up_18yasUstuKullanici

----------------------------------------

create procedure up_toplamSatici
as
begin
select count(*) from Saticilar
end

exec up_toplamSatici

create procedure up_enCokUrunluSatici
as
begin
select TOP(1) Satici + ' ('+ cast(count(Satici) as nvarchar(25))+' �r�n)' from Urunler group by Satici order by count(Satici) desc
end

exec up_enCokUrunluSatici

create procedure up_enAzUrunluSatici
as
begin
select TOP(1) Satici + ' ('+ cast(count(Satici) as nvarchar(25))+' �r�n)' from Urunler group by Satici order by count(Satici) asc
end

exec up_enAzUrunluSatici

create procedure up_enPahaliUrunSatici
as
begin
select TOP(1) Satici + ' ('+ Marka+''+Model+ ' ('+cast((Fiyat) as nvarchar(25))+' TL))' from Urunler order by Fiyat DESC
end

exec up_enPahaliUrunSatici

alter procedure up_enUcuzUrunSatici
as
begin
select TOP(1) Satici + ' ('+ Marka+' '+Model+ ' ('+cast((Fiyat) as nvarchar(25))+' TL))' from Urunler order by Fiyat ASC
end

exec up_enUcuzUrunSatici

create procedure up_enCokStokluSatici
as
begin
select TOP(1) Satici+' ('+ cast(sum(StokMiktari) as nvarchar(25))+' Adet)' From Urunler Group By Satici Order by sum(StokMiktari) desc
end

exec up_enCokStokluSatici

create procedure up_enAzStokluSatici
as
begin
select TOP(1) Satici+' ('+ cast(sum(StokMiktari) as nvarchar(25))+' Adet)' From Urunler Group By Satici Order by sum(StokMiktari) asc
end

exec up_enAzStokluSatici

create procedure up_urunCesidiEnCokSatici
as
begin
select TOP(1) Satici+' ('+cast( count(UrunTipi) as nvarchar(25))+' �e�it �r�n)' from Urunler group by Satici order by count(UrunTipi) desc
end

exec up_urunCesidiEnCokSatici

create procedure up_urunCesidiEnAzSatici
as
begin
select TOP(1) Satici+' ('+cast( count(UrunTipi) as nvarchar(25))+' �e�it �r�n)' from Urunler group by Satici order by count(UrunTipi) asc
end

exec up_urunCesidiEnAzSatici

----------------------------------------------

alter procedure up_urunIsimleri
as
begin
select UrunTipi, Marka, Model from Urunler
end

exec [dbo].[up_urunIsimleri]

create procedure up_toplamUrunSayisi
as 
begin
select Count(*) from Urunler
end

exec up_toplamUrunSayisi

alter procedure up_toplamUrunDegeri
as 
begin
select sum(Fiyat) from Urunler
end

exec up_toplamUrunDegeri

alter procedure up_toplamUrunCesidi
as
begin
select count(distinct(UrunTipi)) From Urunler
end

exec up_toplamUrunCesidi

create procedure up_toplamStokSayisi
as
begin
select sum(StokMiktari) from Urunler
end

exec up_toplamStokSayisi

alter procedure up_enEskiUrun
as
begin
DECLARE @suan DATETIME
set @suan=GETDATE()
select min(UretimTarihi) as [�retim Tarihi], max(DATEDIFF(DAY,UretimTarihi,@suan)) as [Ge�en Zaman] from Urunler
end

exec up_enEskiUrun

alter procedure up_enYeniUrun
as
begin
DECLARE @suan DATETIME
set @suan=GETDATE()
select max(UretimTarihi) as [�retim Tarihi], min(DATEDIFF(DAY,UretimTarihi,@suan)) as [Ge�en Zaman] from Urunler 
end

exec up_enYeniUrun

alter Procedure up_enYeniUrunn
as
begin
select TOP(1) Marka + ' ' + Model + '  ('+ dbo.fn_tarihFarki(UretimTarihi)+ ' G�n)' from Urunler order by UretimTarihi desc
end

exec up_enYeniUrunn

create Procedure up_enEskiUrunn
as
begin
select TOP(1) Marka + ' ' + Model + '  ('+ dbo.fn_tarihFarki(UretimTarihi)+ ' G�n)' from Urunler order by UretimTarihi asc
end

exec up_enEskiUrunn


alter procedure up_enCokStok
as
begin
select TOP(1) Marka + ' ' + Model + ' ('+CAST(StokMiktari AS NVARCHAR(25)) + ' Adet)' from Urunler order by StokMiktari desc
end

exec up_enCokStok

create procedure up_enAzStok
as
begin
select TOP(1) Marka + ' ' + Model + ' ('+CAST(StokMiktari AS NVARCHAR(25)) + ' Adet)' from Urunler order by StokMiktari asc
end

exec up_enAzStok

create procedure up_enCokStokTipi
as
begin
select TOP(1) UrunTipi+' ('+CAST(Count(UrunTipi)AS NVARCHAR(25))+' �r�n)' From Urunler Group by UrunTipi order by count(UrunTipi) desc
end

exec up_enCokStokTipi

create procedure up_enAzStokTipi
as
begin
select TOP(1) UrunTipi+' ('+CAST(Count(UrunTipi)AS NVARCHAR(25))+' �r�n)' From Urunler Group by UrunTipi order by count(UrunTipi) asc
end

exec up_enAzStokTipi

alter procedure up_enPahali
as
begin
select TOP(1) Marka + ' ' + Model+' ('+CAST(Fiyat AS NVARCHAR(25))+' TL)' From Urunler Order by Fiyat DESC
end

exec up_enPahali

create procedure up_enUcuz
as
begin
select TOP(1) Marka + ' ' + Model+' ('+CAST(Fiyat AS NVARCHAR(25))+' TL)' From Urunler Order by Fiyat ASC
end

exec up_enUcuz

alter procedure up_kategoriListesi
as
begin
select UrunTipi From Urunler group by UrunTipi order by count(UrunTipi) desc
end

exec up_kategoriListesi














