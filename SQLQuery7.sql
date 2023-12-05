-- =======================================================================================
-- Create User as DBO template for Azure SQL Database and Azure Synapse Analytics Database
-- =======================================================================================
-- For login login_name, create a user in the database
CREATE USER <user_name, sysname, user_name>
	FOR LOGIN <login_name, sysname, login_name>
	WITH DEFAULT_SCHEMA = <default_schema, sysname, dbo>
GO

-- =======================================================================================
-- Create Azure Active Directory User for Azure SQL Database and Azure Synapse Analytics Database
-- =======================================================================================
-- For login <login_name, sysname, login_name>, create a user in the database
-- CREATE USER <Azure_Active_Directory_Principal_User, sysname, user_name>
--    [   { FOR | FROM } LOGIN <Azure_Active_Directory_Principal_Login, sysname, login_name>  ]  
--    | FROM EXTERNAL PROVIDER
--    [ WITH DEFAULT_SCHEMA = <default_schema, sysname, dbo> ]
-- GO


-- Add user to the database owner role
EXEC sp_addrolemember N'db_owner', N'<user_name, sysname, user_name>'
GO

CREATE USER ApiLogin WITH PASSWORD = 'Asdf12345';

GRANT SELECT, INSERT, UPDATE, DELETE ON SCHEMA::TubeYou TO ApiLogin;
GRANT EXECUTE ON OBJECT::[TubeYou].[GetPostDetails] TO ApiLogin;
GRANT EXECUTE ON OBJECT::[TubeYou].[GetPopularPosts] TO ApiLogin;
GRANT EXECUTE ON OBJECT::[TubeYou].[GetCommenterAndLikedStatus] TO ApiLogin;
