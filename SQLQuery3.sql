USE CSI234DataBase



CREATE PROCEDURE TubeYou.GetCommenterAndLikedStatus
    @p_PostID INT,
    @p_UserID INT
AS
BEGIN
    SELECT
        U.Username AS Commenter,
        CASE
            WHEN L.LikeID IS NOT NULL THEN 'Liked'
            ELSE 'Not Liked'
        END AS LikedStatus
    FROM
        TubeYou.Comments AS C
        JOIN TubeYou.Users AS U ON C.UserID = U.UserID
        LEFT JOIN TubeYou.Likes AS L ON C.PostID = L.PostID AND C.UserID = L.UserID
    WHERE
        C.PostID = @p_PostID AND C.UserID = @p_UserID;
END;





CREATE PROCEDURE TubeYou.GetPopularPosts
AS
BEGIN
    SELECT
        P.PostID,
        P.Content,
        P.CreationDateTime,
        P.LikesCount,
        P.CommentsCount,
        A.ViewsCount,
        A.SharesCount
    FROM
        TubeYou.Posts AS P
        JOIN TubeYou.AnalyticsData AS A ON P.PostID = A.PostID
    ORDER BY
        (P.LikesCount + A.ViewsCount) DESC;
END;









CREATE PROCEDURE TubeYou.GetPostDetails
    @p_PostID INT
AS
BEGIN
    SELECT
        P.PostID,
        P.Content,
        P.CreationDateTime,
        P.LikesCount,
        P.CommentsCount,
        U.Username AS Author,
        A.ViewsCount,
        A.SharesCount
    FROM
        TubeYou.Posts AS P
        JOIN TubeYou.Users AS U ON P.UserID = U.UserID
        LEFT JOIN TubeYou.AnalyticsData AS A ON P.PostID = A.PostID
    WHERE
        P.PostID = @p_PostID;
END;





