﻿GeekorsGen Beta v.1.0.2
==========================

http://blog.geekors.com/post/2012/01/17/GeekorsGen-beta-1.aspx

Feature
-----
	使用 Membership API 做後台權限控管。
	針對單一表格做 CRUD 的管理。
	針對欄位特性會產生不同樣板，未來可繼續擴充。
	能對指定欄位做搜尋。(目前只能單欄位)
	能對指定欄位做排序。(可多欄位)
	能指定某些欄位放在列表頁。
	完整的 SiteMapMenu 及 SiteMapPath 支援功能。

Requirements
-----
	.Net Framework 4.0
	.Net MVC Framework 3.0
	SQL Server 2005 、SQL Server 2008
	額外使用套件
	MvcSiteMapProvider : github
	jQUery File Upload : demo1、demo2

Process	
-----
	透過 SqlMetal 產生 dbml
	透過 aspnet_regsql 產生 Membership API 所需的資料表
	產生後台所需程式碼
	~/Controllers
	~/Models
	~/Services
	~/Views/{moduleName}
	資料庫建置須知

	在建置資料庫的時候，必須在每個欄位的「描述」加上以下參數，讓 GeekorsGen 知道產生什麼樣的樣板，這個功能可無限擴充。

	displayName=string :  該欄位要顯示的名稱
	list=true : 該欄位是否要顯示在列表頁(同被視為排序的因子)
	filter=true : 該欄位是否為搜尋的過濾條件，目前只能指定一欄，超過一欄，則為最後指定的欄位為主
	 使用方法

	設定 App.Config 中的 ConnectionString 或者 給定 Console 參數
		- dbHost :資料庫主機
		- dbName :資料庫名稱
		- dbUser :使用者
		- dbPwd :密碼
		- TargetPath :程式碼產生目的地資料夾
		- dbmlFileName : dbml 的檔名，同Class名稱。

Beta 版暫不提供 WebSite 樣版。