CREATE TABLE [dbo].[Flight]
(
	[flightID] INT NOT NULL PRIMARY KEY, 
    [flightName] VARCHAR(50) NULL, 
    [destinationFrom] VARCHAR(50) NULL, 
    [destinationTo] VARCHAR(50) NULL, 
    [flightDate] DATETIME NULL, 
    [flightSeat] INT NULL, 
    [statusAvailability] VARCHAR(50) NULL, 
    [firstClass] VARCHAR(50) NULL, 
    [secondClass] VARCHAR(50) NULL, 
    [thirdClass] VARCHAR(50) NULL, 
    [price] FLOAT NULL, 
)
