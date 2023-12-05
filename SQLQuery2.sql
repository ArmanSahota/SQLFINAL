USE CSI234DataBase

INSERT INTO TubeYou.Users (UserID, Username, FirstName, LastName, Email)
VALUES
    (1, 'user1', 'John', 'Doe', 'john.doe@email.com'),
    (2, 'user2', 'Jane', 'Smith', 'jane.smith@email.com'),
    (3, 'user3', 'Alice', 'Johnson', 'alice.johnson@email.com'),
    (4, 'user4', 'Bob', 'Miller', 'bob.miller@email.com');

-- Inserting Sample Data into TubeYou.Posts
INSERT INTO TubeYou.Posts (PostID, UserID, Content, CreationDateTime, LikesCount, CommentsCount)
VALUES
    (1, 1, 'This is a sample post.', '11-30-2023', 10, 5),
    (2, 2, 'Another post here!', '11-30-2023', 15, 8),
    (3, 3, 'Check out my latest content.','11-30-2023', 20, 12),
    (4, 1, 'Fun times with friends!', '11-30-2023', 25, 15);

-- Inserting Sample Data into TubeYou.Comments
INSERT INTO TubeYou.Comments (CommentID, PostID, UserID, Content, CreationDateTime)
VALUES
    (1, 1, 2, 'Great post!', '11-30-2023'),
    (2, 2, 3, 'Awesome content!', '11-30-2023'),
    (3, 3, 1, 'I love it!', '11-30-2023'),
    (4, 1, 3, 'Keep it up!', '11-30-2023');

-- Inserting Sample Data into TubeYou.Likes
INSERT INTO TubeYou.Likes (LikeID, PostID, UserID, CreationDateTime)
VALUES
    (1, 1, 3,'11-30-2023'),
    (2, 2, 1,'11-30-2023'),
    (3, 3, 2,'11-30-2023'),
    (4, 4, 4, '11-30-2023');

-- Inserting Sample Data into TubeYou.AnalyticsData
INSERT INTO TubeYou.AnalyticsData (AnalyticsID, PostID, UserID, ViewsCount, SharesCount)
VALUES
    (1, 1, 2, 100, 50),
    (2, 2, 3, 150, 75),
    (3, 3, 1, 200, 100),
    (4, 4, 4, 120, 60);

-- Displaying the Sample Data
SELECT * FROM TubeYou.Users;
SELECT * FROM TubeYou.Posts;
SELECT * FROM TubeYou.Comments;
SELECT * FROM TubeYou.Likes;
SELECT * FROM TubeYou.AnalyticsData;
SELECT * FROM TubeYou.PostHistory;


