CREATE OR ALTER PROCEDURE [panels].[meetings_getBySearch_S]
	@userId VARCHAR(MAX) NULL,
	@organizerName VARCHAR(50) NULL,
	@from DATETIME NULL,
	@to DATETIME NULL,
	@isPublic BIT NULL,
	@city VARCHAR(50) NULL,
	@street VARCHAR(50) NULL,
	@status INT NULL,
	@category INT NULL,
	@sortType VARCHAR(10),
	@sortName VARCHAR(50),
	@pageNumber INT,
	@pageSize INT
AS
BEGIN
	 WITH Data_CTE 
	 AS
	 (  
		SELECT m.[Id] AS MeetingId 
			  ,m.[OrganizerId]
			  ,m.[OrganizerName] 
			  ,m.[City] 
			  ,m.[StartDate]
			  ,m.[EndDate]
			  ,m.[CancellationDate]
			  ,m.[Status]
			  ,m.[AcceptedInvitations]
			  ,m.[MaxInvitations]
			  ,m.[Created]
			  ,mc.[Value] AS Category
		  FROM [Meetings4IT].[panels].[Meetings] AS m
		  INNER JOIN [panels].[MeetingCategories] AS mc ON mc.[Index] = m.CategoryIndex  
		  WHERE  (
		  (@userId IS NULL AND m.HasPanelVisibility = 1) 
		   OR (@userId IS NOT NULL AND m.HasPanelVisibility = 1) 
		   OR (@userId IS NOT NULL AND (m.HasPanelVisibility = 0 AND EXISTS(
		   		SELECT 1 FROM [Meetings4IT].[panels].[Invitations] WHERE 
		   		RecipientId = @userId 
		   		AND [MeetingId] = m.[Id]))))
		  AND (@organizerName IS NULL OR m.OrganizerName = @organizerName)
		  AND (@from IS NULL OR m.[StartDate] > @from)
		  AND (@to IS NULL OR m.StartDate < @to) 
		  AND (@city IS NULL OR m.[City] = @city)
		  AND (@street IS NULL OR m.[Street] = @street)
		  AND (@status IS NULL OR m.[Status]= @status)
		  AND (@category IS NULL OR m.[CategoryIndex] = @category) 
	 ), 
	 Count_CTE 
	 AS 
	 (
	 	SELECT COUNT(*) AS TotalCount FROM Data_CTE
	 )
	SELECT d.[MeetingId] 
		  ,d.[OrganizerId]
		  ,d.[OrganizerName] 
		  ,d.[City] 
		  ,d.[StartDate]
		  ,d.[EndDate]
		  ,d.[CancellationDate]
		  ,d.[AcceptedInvitations]
		  ,d.[MaxInvitations]
		  ,d.[Status] 
		  ,d.[Category]
		  ,d.[Created]
		  ,c.TotalCount
	 FROM Data_CTE AS d
	 CROSS JOIN Count_CTE AS c
	 ORDER BY 
	 CASE WHEN @sortName = 'OrganizerName' AND @sortType = 'asc' THEN OrganizerName END ASC,  
	 CASE WHEN @sortName = 'OrganizerName' AND @sortType = 'desc' THEN OrganizerName END DESC,

	 CASE WHEN @sortName = 'StartDate' AND @sortType = 'asc' THEN StartDate END ASC,  
	 CASE WHEN @sortName = 'StartDate' AND @sortType = 'desc' THEN StartDate END DESC,

	 CASE WHEN @sortName = 'EndDate' AND @sortType = 'asc' THEN EndDate END ASC,  
	 CASE WHEN @sortName = 'EndDate' AND @sortType = 'desc' THEN EndDate END DESC,

	 CASE WHEN @sortName = 'CancellationDate' AND @sortType = 'asc' THEN CancellationDate END ASC,  
	 CASE WHEN @sortName = 'CancellationDate' AND @sortType = 'desc' THEN CancellationDate END DESC,

	 CASE WHEN @sortName = 'City' AND @sortType = 'asc' THEN City END ASC,  
	 CASE WHEN @sortName = 'City' AND @sortType = 'desc' THEN City END DESC,

	 CASE WHEN @sortName = 'Created' AND @sortType = 'asc' THEN Created END ASC,  
	 CASE WHEN @sortName = 'Created' AND @sortType = 'desc' THEN Created END DESC,

	 CASE WHEN @sortName = 'Status' AND @sortType = 'asc' THEN Status END ASC,  
	 CASE WHEN @sortName = 'Status' AND @sortType = 'desc' THEN Status END DESC,

	 CASE WHEN @sortName = 'Category' AND @sortType = 'asc' THEN [Category] END ASC,  
	 CASE WHEN @sortName = 'Category' AND @sortType = 'desc' THEN [Category] END DESC

	 OFFSET (@pageNumber - 1)* @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY;   
END
