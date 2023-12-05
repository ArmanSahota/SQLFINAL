CREATE SCHEMA TubeYou
Go

CREATE TABLE TubeYou.Users (
    UserID INT PRIMARY KEY,
    Username VARCHAR(255) UNIQUE NOT NULL,
    FirstName VARCHAR(255),
    LastName VARCHAR(255),
    Email VARCHAR(255) UNIQUE NOT NULL
);
-- Creating Posts Table in TubeYou Schema
CREATE TABLE TubeYou.Posts (
    PostID INT PRIMARY KEY,
    UserID INT,
    Content TEXT,
    CreationDateTime DATETIME,
    LikesCount INT DEFAULT 0,
    CommentsCount INT DEFAULT 0,
    FOREIGN KEY (UserID) REFERENCES TubeYou.Users(UserID)
);

-- Creating Comments Table in TubeYou Schema
CREATE TABLE TubeYou.Comments (
    CommentID INT PRIMARY KEY,
    PostID INT,
    UserID INT,
    Content TEXT,
    CreationDateTime DATETIME,
    FOREIGN KEY (PostID) REFERENCES TubeYou.Posts(PostID),
    FOREIGN KEY (UserID) REFERENCES TubeYou.Users(UserID)
);

-- Creating Likes Table in TubeYou Schema
CREATE TABLE TubeYou.Likes (
    LikeID INT PRIMARY KEY,
    PostID INT,
    UserID INT,
    CreationDateTime DATETIME,
    FOREIGN KEY (PostID) REFERENCES TubeYou.Posts(PostID),
    FOREIGN KEY (UserID) REFERENCES TubeYou.Users(UserID)
);

-- Creating AnalyticsData Table in TubeYou Schema
CREATE TABLE TubeYou.AnalyticsData (
    AnalyticsID INT PRIMARY KEY,
    PostID INT,
    UserID INT,
    ViewsCount INT DEFAULT 0,
    SharesCount INT DEFAULT 0,
    FOREIGN KEY (PostID) REFERENCES TubeYou.Posts(PostID),
    FOREIGN KEY (UserID) REFERENCES TubeYou.Users(UserID)
);

-- Creating PostHistory Table for Triggers in TubeYou Schema
CREATE TABLE TubeYou.PostHistory (
    HistoryID INT PRIMARY KEY,
    PostID INT,
    ModifiedData TEXT,
    Action VARCHAR(10),
    Timestamp DATETIME,
    FOREIGN KEY (PostID) REFERENCES TubeYou.Posts(PostID)
);
