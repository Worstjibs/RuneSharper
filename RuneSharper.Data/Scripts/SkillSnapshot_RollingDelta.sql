USE [RuneSharper-Prod]
GO

/****** Object:  View [dbo].[SkillSnapshot_TotalXP_Earned]    Script Date: 10/04/2022 01:05:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF OBJECT_ID('SkillSnapshot_RollingDelta', 'V') IS NOT NULL
    DROP VIEW SkillSnapshot_RollingDelta;
GO

CREATE VIEW [dbo].[SkillSnapshot_RollingDelta]
AS 
	SELECT 
		S.Id SnapshotId,
		SS.Id SkillSnapshotId,
		SS.Experience, 
		SS.Type,
		SS.Experience - LAG(SS.Experience, 1) OVER (PARTITION BY S.CharacterId, SS.Type ORDER BY S.DateCreated) Delta,
		S.CharacterId,
		S.DateCreated
	FROM SkillSnapshot SS
	INNER JOIN Snapshots S ON SS.SnapshotId = S.Id
GO