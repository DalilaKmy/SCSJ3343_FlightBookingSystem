CREATE TABLE [dbo].[FlightBooking]
(
	[bookingID] INT NOT NULL PRIMARY KEY, 
    [customerID] INT NULL, 
    [flightID] INT NULL 
	CONSTRAINT [FK_dbo.FlightBooking_dbo.Customer_customerID] FOREIGN KEY ([customerID]) REFERENCES [dbo].[Customer](customerID)
	CONSTRAINT [FK_dbo.FlightBooking_dbo.Flight_flightID] FOREIGN KEY ([flightID]) REFERENCES [dbo].[Flight](flightID), 
    [price] FLOAT NULL
)
