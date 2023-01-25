USE [RuneSharper-Prod]
GO

/****** Object:  View [dbo].[SkillSnapshot_TotalXP_Earned]    Script Date: 10/04/2022 01:05:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('SkillSnapshot_TotalXP_Earned', 'V') IS NOT NULL
    DROP VIEW SkillSnapshot_TotalXP_Earned;
GO

CREATE VIEW [dbo].[SkillSnapshot_TotalXP_Earned]
AS 
	SELECT 
		(MAX(Experience) - MIN(Experience)) AS ExperienceEarned,
		Type,
		Snapshots.CharacterId
	FROM SkillSnapshot
	INNER JOIN Snapshots ON SkillSnapshot.SnapshotId = Snapshots.Id
	GROUP BY 
		Type,
		Snapshots.CharacterId
	ORDER BY
		CharacterId,
		Type
	OFFSET 0 ROWS
GO