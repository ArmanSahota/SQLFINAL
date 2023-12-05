/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [HistoryID]
      ,[PostID]
      ,[ModifiedData]
      ,[Action]
      ,[Timestamp]
  FROM [TubeYou].[PostHistory]



