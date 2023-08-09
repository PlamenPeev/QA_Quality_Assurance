USE taskboard;

SELECT * FROM aspnetusers;

SELECT * FROM tasks;

INSERT INTO tasks (Title, Description, CreatedOn,BoardId, OwnerId)
value("Test create from database", "Test database", now(), 1, "5d9fba1f-4b02-4b26-816c-60ee4d932dde");

INSERT INTO boards(name)
VALUE("On Hold");

INSERT INTO tasks (Title, Description, CreatedOn,BoardId, OwnerId)
value("Test 1", "Test database", now(), 4, "5d9fba1f-4b02-4b26-816c-60ee4d932dde"),
("Test 2", "Test database", now(), 4, "5d9fba1f-4b02-4b26-816c-60ee4d932dde"),
("Test 3", "Test database", now(), 4, "5d9fba1f-4b02-4b26-816c-60ee4d932dde");

DELETE FROM tasks WHERE Title = "Test 3";

SELECT Id FROM tasks WHERE Title = "Test 3";

DELETE FROM tasks WHERE Id = 9;

SELECT * FROM tasks;

UPDATE tasks 
SET Title = "Test TASK UPDATE"
WHERE Id = 5;

DELETE FROM tasks WHERE Title = "Test 1" AND Id = 7;

SELECT Id FROM tasks WHERE Title = "New Task for Homework";

UPDATE tasks 
SET Title = "New TASK UPDATE", Description = "Update description" 
WHERE Id = 10;

SELECT * FROM tasks
WHERE OwnerId = '5d9fba1f-4b02-4b26-816c-60ee4d932dde';




