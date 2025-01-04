use AKADEMEDYA


BACKUP DATABASE [AKADEMEDYA]
TO DISK = 'C:\BACKUPDB\AKADEMEDYA.bak'
WITH INIT;



SELECT * FROM msdb.dbo.backupset
WHERE database_name = 'AKADEMEDYA'
ORDER BY backup_finish_date DESC;


RESTORE DATABASE [AKADEMEDYA]
FROM DISK = 'C:\BACKUPDB\AKADEMEDYA.bak'
WITH REPLACE;
