/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
SET IDENTITY_INSERT [MOVIE] ON
INSERT INTO [MOVIE] (Id,Title, Synopsis, Director, Release) VALUES
(1, 'Pulp Fiction', 'Adrenaline Baby !', 'Quentin Tarantino','1994-10-14'),
(2, 'Big Fish', 'It s not really about fish', 'Tim Burton', '2003-03-03'),
(3, 'Star Wars : The Phantom Menace', 'Jar Jar Binks', 'Georges Lucas', '1999-10-13')
SET IDENTITY_INSERT [MOVIE] OFF

SET IDENTITY_INSERT [USER] ON
INSERT INTO [USER] (Id, LastName, FirstName, Email, [Password], [Role]) VALUES
(1,'Doe','Jane','janedoe@mail.com','$argon2id$v=19$m=65536,t=3,p=1$xdRwMHkXjTqnSXPHBcU4VQ$MmMeNWoh87pSXftXMRB1EFx3Q9bmHnmVnkGrS4R5fE8',0),
(2,'Doe', 'John','johndoe@mail.com', '$argon2id$v=19$m=65536,t=3,p=1$ACxIqFNX9+dSEqyybv5scQ$6j2sfmNbtHDNmJQXOUfw5TdiI+ihdo1xICdszLm9Dgk', 1),
(3,'Smith', 'John','johnsmith@mail.com', '$argon2id$v=19$m=65536,t=3,p=1$S0gY0lvJ0+vgEdGjahjHFw$pZ00OWuCAXHvEevA5PIlGdgW5f1Jc0SpkJbKXIyaqpA', 2)


SET IDENTITY_INSERT [USER] OFF