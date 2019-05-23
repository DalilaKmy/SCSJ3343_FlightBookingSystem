CREATE TABLE [dbo].[Payment]
(
	[paymentID] INT NOT NULL PRIMARY KEY, 
    [paymentStatus] VARCHAR(50) NULL, 
    [bookingID] INT NULL,
	CONSTRAINT [FK_dbo.Payment_dbo.FlightBooking_bookingID] FOREIGN KEY ([bookingID]) REFERENCES [dbo].[FlightBooking](bookingID)
)
