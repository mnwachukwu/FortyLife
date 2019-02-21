DELETE FROM ApplicationUsers

DBCC CHECKIDENT (ApplicationUsers, RESEED, 0)