GeekorsGen Beta v.1.0.2
==========================

<a href="http://blog.geekors.com/post/2012/01/30/GeekorsGen-Beta-2.aspx" target="_blank">link</a>

##Feature

1.Using Membership API for permissions.

2.CRUD for single table.

3.Generating the template for specific column schema,it can be extended in the future.

4.Filting data by the specific data of column. (Only single column per table)

5.Sorting data by the specific data of column. (Multi columns)

6.Assign specific column to the list page.

7.Completely sitemap and sitemenu ability.	
	
8.Import Bootstrap UI Framework (Awesome actually!)

9.Add jQueryFileUpload Component with Geekors version.

10.Add image resize , cut ability

##Requirements

.Net Framework 4.0

.Net MVC Framework 3.0

SQL Server 2005 、SQL Server 2008

###Extra packages :

MvcSiteMapProvider : <a href="https://github.com/maartenba/MvcSiteMapProvider" target="_blank">github</a>

jQUery File Upload : <a href="http://blueimp.github.com/jQuery-File-Upload/" target="_blank"> demo </a>

##Process	

1.Generating the DBML file via the SqlMetal tool.

2.Generating data vim aspnet_regsql tool that Membership api needs.

3.Generating the CODE your project needs.

	~/Controllers
	~/Models
	~/Services
	~/Views/{moduleName}

###You must to know when you building database.

You have to assign parameter to the 「Description」of each column when you building database.(It can be extended.)	

	displayName=column name
	list=true : Is the column be showed on list page.
	filter=true : Is the column for filting data. (So far, only for one column)
	 
##usage

Set the connection string of App.config in Console project.

	- dbHost : Database host.
	- dbName : Database name.
	- dbUser : User.
	- dbPwd : Password
	- TargetPath : The target folder.  
	- dbmlFileName : File Name of DBML，same to Class name.

No WebSite template for beta version.
