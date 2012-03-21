GeekorsGen Beta v.1.0.2
==========================

http://blog.geekors.com/post/2012/01/30/GeekorsGen-Beta-2.aspx

Feature
-----
	Using Membership API for permissions.
	CRUD for single table.
	Generating the template for specific column schema,it can be extended in the future.
	Filting data by the specific data of column. (Only single column per table)
	Sorting data by the specific data of column. (Multi columns)
	Assign specific column to the list page.
	Completely sitemap and sitemenu ability.	
	
	Import Bootstrap UI Framework (Awesome actually!)
	Add jQueryFileUpload Component with Geekors version.
	Add image resize , cut ability

Requirements
-----
	.Net Framework 4.0
	.Net MVC Framework 3.0
	SQL Server 2005 、SQL Server 2008

	Extra packages :
	MvcSiteMapProvider : github
	jQUery File Upload : demo1、demo2

Process	
-----	
	Generating the DBML file via the SqlMetal tool.
	
	Generating data vim aspnet_regsql tool that Membership api needs.

	Generating the CODE your project needs.
	~/Controllers
	~/Models
	~/Services
	~/Views/{moduleName}

	You must to know when you build database.
	You have to assign parameter to the 「Description」of each column when you building database.(It can be extended.)
	
	displayName=column name
	list=true : Is the column be showed on list page.
	filter=true : Is the column for filting data. (So far, only for one column)
	 
	usage

	Set the connection string of App.config in Console project.
		- dbHost : Database host.
		- dbName : Database name.
		- dbUser : User.
		- dbPwd : Password
		- TargetPath : The target folder.
		- dbmlFileName : File Name of DBML，same to Class name.

No WebSite template for beta version.
