
CREATE FUNCTION SiparisSayisi() --toplam siparis sayisini döndüren fonksiyon
RETURNS INT
AS
BEGIN
    DECLARE @SiparisSayisi INT --int deðiþken atama
    SELECT @SiparisSayisi = Count(*) --sipariþler tablosundaki öðelerin sayýsýný döndürür
    FROM Siparisler

    IF @SiparisSayisi IS NULL --deðer null ise 0 atar
    BEGIN
        SET @SiparisSayisi = 0
    END
    RETURN @SiparisSayisi --deðiþkeni döndürür
END

CREATE FUNCTION SiparistenToplamKazanc() --tüm sipariþlerin deðerini döndüren fonksiyon
RETURNS INT
AS
BEGIN
    DECLARE @SiparisKazanci INT --deðiþken atama
    SELECT @SiparisKazanci = Sum(Fiyat) --tüm fiyat bilgisini toplar	
    FROM Siparisler
    IF @SiparisKazanci IS NULL --deðer null ise deðiþkene 0 atar
    BEGIN
        SET @SiparisKazanci = 0
    END
    RETURN @SiparisKazanci 
END

CREATE FUNCTION SiparisiOlanSaticiSayisi() --aktif sipariþi olan satýcýlarý döndüren fonksiyon
RETURNS INT
AS
BEGIN
    DECLARE @SiparistekiSaticilar INT --
    SELECT @SiparistekiSaticilar = count(distinct(Satici)) --farklý satýcýlarýn sayýsýný deðiþkene atar
    FROM Siparisler
    IF @SiparistekiSaticilar IS NULL --satici sayýsý null ise 0 atar
    BEGIN
        SET @SiparistekiSaticilar = 0
    END
    RETURN @SiparistekiSaticilar --satýcýlarýn deðerini döndürür 
END



ALTER FUNCTION ToplamKazancSatici --girilen satýcýnýn toplam yaptýðý ürün satýþýný döndüren fonksiyon
(
    @Satici nvarchar(50) --satici deðiþkeni tanýmlandý
)
RETURNS INT --int deðeri döndürür
AS
BEGIN
    DECLARE @ToplamKazanc INT  --int deðiþkeni tanýmlandý

    SELECT @ToplamKazanc = Sum(Fiyat) --fiyatlarýn toplmamý deðiþkene atandý
    FROM Siparisler                    
    WHERE Satici = @Satici   --sipariþler tablosundaki Satýcý deðiþkeni ile eþleþtirlidi

    RETURN @ToplamKazanc  --deðiþkeni döndürür 
END

create function fn_tarihFarki(@UretimTarihi datetime) --Tarih farkýný hesaplamak için kullanýlan fonksiyon
Returns nvarchar(25)
as
begin
declare @suan datetime   --anlýk tarih verisi deðþken atanýr
declare @fark nvarchar(25) --fark hesabý için deðiþken atanýr
set @suan=GETDATE()  --anlýk tarih atanýr
set @fark= DATEDIFF(DAY,@UretimTarihi,@suan) --datediff ile tarihler arasý fark hesaplanýr
return @fark  --hesaplanan fark döndürülür
end  

ALTER VIEW EnCokSatanUrunler AS  --En çok satan ürünleri görmek için kullandýðýmýz view
SELECT TOP 10                     --ürün id e göre group by yaptýk ve ürün fiyat satýcý adet görüntüledik
       UrunID,												
       MAX(Urun) AS Urun,
	   MIN(Fiyat) as Fiyat,
	   MAX(Satici) as Satýcý,
       SUM(Adet) AS ToplamSatisMiktari 
FROM   Siparisler
GROUP BY UrunID        --iþlemler urunide göre yapýldý
ORDER BY ToplamSatisMiktari DESC; --büyükten küçüðe sýralama yapýlýr

-----------------------------------------------------------------

CREATE PROCEDURE up_UrunEkle --Ürün eklemek içi stored procedure
    @ukid INT,				 --ürünlerin deðiþken atamalarý yapýlýr
    @utip NVARCHAR(50),
    @umarka NVARCHAR(50),
    @umodel NVARCHAR(50),
    @ustok INT,
    @ufiyat INT,
    @uuretim DATETIME,
    @ugaranti INT,
    @usatici NVARCHAR(50)
AS
BEGIN  --insert into ile deðiþkenler Urunler tablosuna eklenir
    INSERT INTO Urunler(KategoriID, UrunTipi, Marka, Model, StokMiktari, Fiyat, UretimTarihi, Garanti, Satici)
    VALUES (@ukid, @utip, @umarka, @umodel, @ustok, @ufiyat, @uuretim, @ugaranti, @usatici)
END

CREATE PROCEDURE up_UrunGuncelle --Ürün güncellemek için stored procedure
    @uid INT,					 --deðiþkneler atanýr	
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
    UPDATE Urunler  --update ile deðiþkenlerin yeni deðerleri ile Urunler tablosunda güncelleme yapýlýr
    SET
        KategoriID = @ukid,
        UrunTipi = @utip,
        Marka = @umarka,
        Model = @umodel,
        StokMiktari = @ustok,
        Fiyat = @ufiyat,
        UretimTarihi = @uuretim,
        Garanti = @ugaranti
    WHERE UrunID = @uid; --ürün ide göre iþlemler yapýlýr
END

CREATE PROCEDURE up_KullaniciEkle --Kullanýcý eklemek için stored procedure
    @kad NVARCHAR(50),			  --deðiþkenler tanýmlanýr
    @ksoyad NVARCHAR(50),
    @ksifre NVARCHAR(50),
    @kkayit DATETIME,
    @kyas INT,
    @kcinsiyet NVARCHAR(10),
    @keposta NVARCHAR(50),
    @ktelefon NVARCHAR(20)
AS
BEGIN						--insert into ile kullanýcýlara eklenir
    INSERT INTO Kullanicilar(Ad, Soyad, Sifre, KayitTarihi, Yas, Cinsiyet, Eposta, Telefon)
    VALUES (@kad, @ksoyad, @ksifre, @kkayit, @kyas, @kcinsiyet, @keposta, @ktelefon)
END

CREATE PROCEDURE up_KullaniciGuncelle --Kullanýcý güncellemek için stored procedure
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
    UPDATE Kullanicilar --update ile kullanicilar tablosu güncellenir
    SET
        Ad = @kad,
        Soyad = @ksoyad,
        Sifre = @ksifre,
        KayitTarihi = @kkayit,
        Yas = @kyas,
        Cinsiyet = @kcinsiyet,
        Eposta = @keposta,
        Telefon = @ktelefon
    WHERE KullaniciID = @kid;  --iþlemler kullanýcý ide göre yapýlýr
END

CREATE PROCEDURE up_SaticiEkle --Satýcý eklemek için stored procedure
    @sad NVARCHAR(50),
    @seposta NVARCHAR(50),
    @stelefon NVARCHAR(20),
    @sifre NVARCHAR(50)
AS
BEGIN				--saticilar tablosuna ekleme yapýlýr
    INSERT INTO Saticilar(SaticiMagazaAdi, SaticiEposta, SaticiTelefon, SaticiSifre)
    VALUES (@sad, @seposta, @stelefon, @sifre) 
END

CREATE PROCEDURE up_SaticiGuncelle --Satýcý Güncellemek için stored procedure
    @sid INT,
    @sad NVARCHAR(50),
    @seposta NVARCHAR(50),
    @stelefon NVARCHAR(20),
    @sifre NVARCHAR(50)
AS
BEGIN
    UPDATE Saticilar --update ile satýcýlar tablosu güncellenir
    SET
        SaticiMagazaAdi = @sad,
        SaticiEposta = @seposta,
        SaticiTelefon = @stelefon,
        SaticiSifre = @sifre
    WHERE SaticiID = @sid;
END

alter procedure up_kategoriListesi --kategorileri çekmek için kullanýlan stored procedure
as
begin
select UrunTipi From Urunler group by UrunTipi order by count(UrunTipi) desc 
end

CREATE PROCEDURE up_UrunAlindi  
(      --bir ürün alýndýðý zaman alýnan miktara göre stoktan azaltan stored procedure
    @UrunID INT,  
    @AlinanMiktar INT --c# da textboxtan alýnacak alýnan ürün miktarý tanýmlanmasý
)
AS
BEGIN
    DECLARE @EskiStokMiktari INT --stok miktarýnýn ilk hali		
    DECLARE @YeniStokMiktari INT --stok miktarýnýn çýkarýldýktan sonraki hali

    SELECT @EskiStokMiktari = StokMiktari 
    FROM Urunler
    WHERE UrunID = @UrunID
    SET @YeniStokMiktari = @EskiStokMiktari - @AlinanMiktar --stok miktarýndan alýnan miktar çýkarýlýr

    UPDATE Urunler --ürünler tablosu yeni deðerler ile güncellenir
    SET StokMiktari = @YeniStokMiktari --stok yeni stok durumu ile deðiþtrilir	
    WHERE UrunID = @UrunID --iþlem ürün ide göre yapýlýr
END

---------------------------------------------
---------------TRIGGERLAR--------------------
---------------------------------------------

ALTER TRIGGER trg_Urunler_Insert --Urun tablosuna ürün eklendðinde devreye giren trigger
ON [dbo].[Urunler]		--ürün tablosunda çalýþýr
FOR INSERT
AS
BEGIN
    DECLARE @id INT       --deðiþkenler tanýmlanýr
    DECLARE @uruntipi NVARCHAR(50)
    DECLARE @marka NVARCHAR(50)
    DECLARE @model NVARCHAR(50)
    DECLARE @stokmiktari INT
    DECLARE @fiyat INT
    DECLARE @satici NVARCHAR(50)

    SELECT @id = UrunID,   --deðiþkenþer tablodaki deðerlerle eþleþtirilir
	@uruntipi = UrunTipi, 
	@marka = Marka,
	@model = Model,
	@stokmiktari = StokMiktari, 
	@fiyat = Fiyat,
	@satici = Satici 
	FROM inserted  --ekleme yapýldýðý için geçici inserted kullanýlýr

	--parametrelere göre OlaylarUrun tablosuna ekleme yapýlýr
    INSERT INTO [dbo].[OlaylarUrun] VALUES ('ID: '+CAST(@id AS NVARCHAR(10)) + ' ' + @marka + ' ' + @model + ' ' + @uruntipi 
	+ ' Stok Miktarý: ' + CAST(@stokmiktari AS NVARCHAR(10)) + ' Fiyat: ' + CAST(@fiyat AS NVARCHAR(10))
	+ ' TL. Satýcý ' + @satici +' Tarafýndan '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Eklendi.')
END

ALTER TRIGGER trg_Urunler_Delete --ürün tablosundan ürün silindiðinde devreye giren trigger
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

	--deðiþkenler tanýmlanýr
    SELECT @id = UrunID, @uruntipi = UrunTipi, @marka = Marka, @model = Model, @stokmiktari = StokMiktari, @fiyat = Fiyat, @satici = Satici
	FROM deleted --silinme iþlemi yapýldýðý için geçici deleted tablosu kullanýldý

	--OlaylarUrun tablosuna silinen ürünler eklenir
    INSERT INTO [dbo].[OlaylarUrun] VALUES ('ID: '+CAST(@id AS NVARCHAR(10)) + ' ' + @marka + ' ' + @model + ' ' + @uruntipi 
	+ ' Stok Miktarý: ' + CAST(@stokmiktari AS NVARCHAR(10)) + ' Fiyat: ' + CAST(@fiyat AS NVARCHAR(10))
	+ ' TL. Satýcý ' + @satici +' Tarafýndan '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Silindi.')
END


CREATE TRIGGER trg_Kullanicilar_Insert --kullanýcýlar tablosuna kullanýcý eklendiðinde devreye giren trigger
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
	+ CAST(@yas AS NVARCHAR(10)) + ' E-Posta: ' + @eposta+ ' Kullanýcýsý '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Eklendi.')
END

ALTER TRIGGER trg_Kullanicilar_Delete --kullanýcýlar tablosundan kullanýcý silindiðinde devreye giren trigger
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
	+ CAST(@yas AS NVARCHAR(10)) + ' E-Posta: ' + @eposta+ ' Kullanýcýsý '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Silindi.')
END

ALTER TRIGGER trg_Satici_Insert --satýcý tablosuna satýcý eklendiðinde devreye giren trigger
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
	+ CAST(@saticitelefon AS NVARCHAR(10)) +' Satýcýsý '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Eklendi.')
END

CREATE TRIGGER trg_Satici_Delete --saticilar tablosundan satici silindiðinde devreye giren trigger
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
	+ CAST(@saticitelefon AS NVARCHAR(10)) +' Satýcýsý '+  Cast(GETDATE() as nvarchar(25)) +' Tarihinde Silindi.')
END


---------------------------------------
---------------------------------------
--BU KISIMDA ÝSTATÝSTÝK VERÝLERÝ ÝÇÝN DAHA BASÝT STORED PROCEDURELAR KULLANILDI--
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
select count(*) from Kullanicilar where Cinsiyet='Kýz'
end

exec up_kizKullanici

alter procedure up_enGencKullanici
as
begin
select Top(1) Ad +' '+ Soyad+' ('+CAST(Yas AS nvarchar(25)) +' Yaþ)' From Kullanicilar order by Yas asc
end

exec up_enGencKullanici

alter procedure up_enYasliKullanici
as
begin
select Top(1) Ad +' '+ Soyad+' ('+CAST(Yas AS nvarchar(25)) +' Yaþ)' From Kullanicilar order by Yas desc
end

exec up_enYasliKullanici

alter procedure up_enEskiKayitliKullanici
as
begin
select Top(1) Ad +' '+ Soyad+' ('+CAST(KayitTarihi AS nvarchar(25))+')  ('+dbo.fn_tarihFarki(KayitTarihi) +' Gün)' from Kullanicilar order by KayitTarihi asc
end


exec up_enEskiKayitliKullanici

alter procedure up_enYeniKayitliKullanici
as
begin
select Top(1) Ad +' '+ Soyad+' ('+CAST(KayitTarihi AS nvarchar(25))+')  ('+dbo.fn_tarihFarki(KayitTarihi) +' Gün)' from Kullanicilar order by KayitTarihi desc
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
select TOP(1) Satici + ' ('+ cast(count(Satici) as nvarchar(25))+' Ürün)' from Urunler group by Satici order by count(Satici) desc
end

exec up_enCokUrunluSatici

create procedure up_enAzUrunluSatici
as
begin
select TOP(1) Satici + ' ('+ cast(count(Satici) as nvarchar(25))+' Ürün)' from Urunler group by Satici order by count(Satici) asc
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
select TOP(1) Satici+' ('+cast( count(UrunTipi) as nvarchar(25))+' Çeþit Ürün)' from Urunler group by Satici order by count(UrunTipi) desc
end

exec up_urunCesidiEnCokSatici

create procedure up_urunCesidiEnAzSatici
as
begin
select TOP(1) Satici+' ('+cast( count(UrunTipi) as nvarchar(25))+' Çeþit Ürün)' from Urunler group by Satici order by count(UrunTipi) asc
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
select min(UretimTarihi) as [Üretim Tarihi], max(DATEDIFF(DAY,UretimTarihi,@suan)) as [Geçen Zaman] from Urunler
end

exec up_enEskiUrun

alter procedure up_enYeniUrun
as
begin
DECLARE @suan DATETIME
set @suan=GETDATE()
select max(UretimTarihi) as [Üretim Tarihi], min(DATEDIFF(DAY,UretimTarihi,@suan)) as [Geçen Zaman] from Urunler 
end

exec up_enYeniUrun

alter Procedure up_enYeniUrunn
as
begin
select TOP(1) Marka + ' ' + Model + '  ('+ dbo.fn_tarihFarki(UretimTarihi)+ ' Gün)' from Urunler order by UretimTarihi desc
end

exec up_enYeniUrunn

create Procedure up_enEskiUrunn
as
begin
select TOP(1) Marka + ' ' + Model + '  ('+ dbo.fn_tarihFarki(UretimTarihi)+ ' Gün)' from Urunler order by UretimTarihi asc
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
select TOP(1) UrunTipi+' ('+CAST(Count(UrunTipi)AS NVARCHAR(25))+' Ürün)' From Urunler Group by UrunTipi order by count(UrunTipi) desc
end

exec up_enCokStokTipi

create procedure up_enAzStokTipi
as
begin
select TOP(1) UrunTipi+' ('+CAST(Count(UrunTipi)AS NVARCHAR(25))+' Ürün)' From Urunler Group by UrunTipi order by count(UrunTipi) asc
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














