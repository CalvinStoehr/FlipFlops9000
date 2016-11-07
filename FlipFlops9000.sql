CREATE DATABASE FlipFlops;
USE FlipFlops;
CREATE TABLE UserData(userid float, username char(30), password char(30), datejoined date, devicetype char(10), PRIMARY KEY(userid));
INSERT INTO UserData VALUES (1, 'differentstate4', 'password123', '2016-10-30', 'ios');
INSERT INTO UserData VALUES (2, 'redhouse91', 'qwertyuiop', '2016-10-31', 'android');
INSERT INTO UserData VALUES (3, 'longstudent16', 'qazwsxedcr', '2016-11-2', 'ios');
INSERT INTO UserData VALUES (4, 'littlegroup24', 'asdfghjkl', '2017-15-2', 'pc');
INSERT INTO UserData VALUES (5, 'importantcountry29', 'il!kefr33dom', '1776-7-4', 'eagle'); 


CREATE TABLE UserStats(username char(30), highest_score integer, total_runs integer, bosses_beaten integer, boss_encounters integer);
INSERT INTO UserStats VALUES ('differentstate4', 2048, 10, 1, 2);
INSERT INTO UserStats VALUES ('redhouse91', 4167, 28, 2, 2);
INSERT INTO UserStats VALUES ('longstudent16', 7283, 36, 3, 4);
INSERT INTO UserStats VALUES ('littlegroup24', 4864, 16, 2, 3);
INSERT INTO UserStats VALUES ('importantcountry29', 3245, 12, 2, 2);

CREATE TABLE BossData(username char(30), boss1encounter float, boss1beat float, boss2encounter float, boss2beat float);
INSERT INTO BossData VALUES ('differentstate4', 12, 4, 3, 0);
INSERT INTO BossData VALUES ('redhouse91', 100, 75, 50, 25);
INSERT INTO BossData VALUES ('longstudent16', 37, 4, 2, 9);
INSERT INTO BossData VALUES ('littlegroup24', 22, 20, 12, 2);
INSERT INTO BossData VALUES ('importantcountry29', 100, 100, 100, 100);
