-- Create TubeYou.PostHistory Table for Triggers in TubeYou Schema
USE CSI234DataBase;

-- Create TubeYou.AnalyticsDataHistory Table for Triggers in TubeYou Schema
CREATE TABLE TubeYou.AnalyticsDataHistory (
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    AnalyticsID INT,
    PostID INT,
    UserID INT,
    ViewsCount INT,
    SharesCount INT,
    DateModified DATETIME DEFAULT GETDATE(),
    ActionTaken VARCHAR(50)
);

-- Update AnalyticsData Trigger
CREATE TRIGGER TubeYou.UpdateAnalyticsDataHistoryTrigger
ON TubeYou.AnalyticsData
AFTER UPDATE
AS 
BEGIN
    INSERT INTO TubeYou.AnalyticsDataHistory (AnalyticsID, PostID, UserID, ViewsCount, SharesCount, DateModified, ActionTaken)
    SELECT d.AnalyticsID, d.PostID, d.UserID, d.ViewsCount, d.SharesCount, GETDATE(), 'UPDATE'
    FROM deleted d;
END;

-- Delete AnalyticsData Trigger
CREATE TRIGGER TubeYou.DeleteAnalyticsDataHistoryTrigger
ON TubeYou.AnalyticsData
AFTER DELETE
AS 
BEGIN
    INSERT INTO TubeYou.AnalyticsDataHistory (AnalyticsID, PostID, UserID, ViewsCount, SharesCount, DateModified, ActionTaken)
    SELECT d.AnalyticsID, d.PostID, d.UserID, d.ViewsCount, d.SharesCount, GETDATE(), 'DELETE'
    FROM deleted d;
END;