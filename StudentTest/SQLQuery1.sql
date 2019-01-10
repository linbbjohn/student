/*
RESTORE FILELISTONLY
FROM DISK = 'D:\download\StudentTest.bak'
'D:\download\StudentTest.bak'
*/

RESTORE DATABASE StudentTest
FROM DISK = 'D:\download\StudentTest.bak'

WITH MOVE 'StudentTest' TO 'D:\download\StudentTest.mdf',
MOVE 'StudentTest_log' TO 'D:\download\StudentTest.ldf',
REPLACE;