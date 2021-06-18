
-- Application Users Reset
--DELETE FROM ApplicationUsers

--DBCC CHECKIDENT (ApplicationUsers, RESEED, 0)

-- Collections Reset
--ALTER TABLE Collections
--DROP CONSTRAINT [FK_dbo.CollectionCards_dbo.Collections_Collection_CollectionId]

DELETE FROM Collections

DBCC CHECKIDENT (Collections, RESEED, 0)

-- Collection Cards Reset
--ALTER TABLE CollectionCards
--DROP CONSTRAINT [FK_dbo.Collections_dbo.ApplicationUsers_ApplicationUser_Id]

DELETE FROM CollectionCards

-- Rulings Reset
--DELETE FROM Rulings

--DBCC CHECKIDENT (Rulings, RESEED, 0)

-- Product Details Reset
--DELETE FROM ProductDetails

-- Prices Reset
--DELETE FROM Prices
