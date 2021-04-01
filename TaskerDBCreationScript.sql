--DROP DATABASE taskerDB2
--GO

CREATE DATABASE taskerDB2

GO

USE taskerDB2;
CREATE TABLE Tasks(
	Id int NOT NULL IDENTITY(1,1),
	Title VARCHAR(255) NOT NULL,
	Descript VARCHAR(255) NOT NULL,
	TaskType VARCHAR(255) NOT NULL

	PRIMARY KEY (Id)
	CONSTRAINT CHK_Type CHECK (TaskType='severity' OR TaskType='time')
);

GO

USE taskerDB2;
CREATE TABLE TimeTasks(
	TaskId int NOT NULL,
	--IsStartDate BIT NOT NULL,
	StartDate DATE,
	EndDate DATE

	PRIMARY KEY (TaskId)
	FOREIGN KEY (TaskId) 
		REFERENCES Tasks(Id)
		ON DELETE CASCADE
);

GO

USE taskerDB2;
CREATE TABLE SeverityTasks(
	TaskId int NOT NULL,
	Severity int NOT NULL

	PRIMARY KEY (TaskId)
	FOREIGN KEY (TaskId) 
		REFERENCES Tasks(Id)
		ON DELETE CASCADE
);

GO