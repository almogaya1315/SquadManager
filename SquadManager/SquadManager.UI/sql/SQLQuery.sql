
SELECT			managers.Manager_Id, managers.Manager_Name, nations.Nation_Name, managers.Manager_Age
FROM			TblManagers managers
JOIN			TblNations nations
ON				managers.Manager_Nationality = nations.Nation_Id

