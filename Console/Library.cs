using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Geekors.Generator
{

	public class Library
	{
		/// <summary>
		/// 欄位修改
		/// </summary>
		/// <param name="rowSchema"></param>
		/// <returns></returns>
		public static string EditAreaColumn(DbSchema.Models.Schema rowSchema)
		{
			string DaoFieldName = Library.ConvertTitleCase(rowSchema.欄位名稱);
			string DaoFieldType = Library.DbDataTypeConvert(rowSchema.資料型別);
			int DbFieldLength = rowSchema.最大長度;
			string DisplayName = (rowSchema.自訂規則.ContainsKey("displayName")) ?
				rowSchema.自訂規則["displayName"] : DaoFieldName;

			if (DaoFieldName.ToLower() == "id" && IsIntegerType(DaoFieldType))
			{
				//如果是 id 流水號,特別處理,不秀出來,然後是 int (就會是流水號)
				return string.Empty;
			}
			else
			{
				return EditAreaColumn(DisplayName, DaoFieldName, DaoFieldType, DbFieldLength);
			}

		}

		/// <summary>
		/// Edit Area 的欄位
		/// </summary>
		/// <param name="displayName">顯示名稱</param>
		/// <param name="fieldName">DAO 欄位名稱</param>
		/// <param name="fieldType">欄位型態</param>
		/// <param name="fieldLength">欄位長度</param>
		/// <returns></returns>
		public static string EditAreaColumn(string displayName, string fieldName, string fieldType, int fieldLength)
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("\t\t\t\t<tr>\r\n");
			sb.Append("\t\t\t\t\t<td width=\"20%\" class=\"star1\">\r\n");
			sb.Append("\t\t\t\t\t\t" + displayName + "\r\n");
			sb.Append("\t\t\t\t\t</td>\r\n");

			sb.Append("\t\t\t\t\t<td>\r\n");

			//佈林值
			if (fieldType.ToLower() == "bool")
			{
				sb.Append("\t\t\t\t\t\t@Html.RadioButtonFor(p => p."+fieldName+", \"True\") 是&nbsp;\r\n");
				sb.Append("\t\t\t\t\t\t@Html.RadioButtonFor(p => p."+fieldName+", \"False\") 否&nbsp;\r\n");
			}
			else if (fieldType.ToLower() == "guid" && fieldName.ToLower() == "id")
			{
				sb.Append("\t\t\t\t\t\t自動產生\r\n");
				sb.Append("\t\t\t\t\t\t@Html.HiddenFor(p => p." + fieldName + ")\r\n");
			}
            else if (fieldType.ToLower() == "datetime" && fieldName.ToLower() == "create_date")
            {
                sb.Append("\t\t\t\t\t\t@Html.Label(Model." + fieldName + ".ToString(\"yyyy-MM-dd\"))\r\n");
                //@Html.Label(Model.Create_Date.ToString("yyyy-MM-dd"))
            }
            //最大長度 MaxLength
            else if (fieldType.ToLower() == "string" && fieldLength == -1)//最大長度
            {
                sb.Append("\t\t\t\t\t\t@Html.TextAreaFor(p => p." + fieldName + ", new { @class = \"txt1 span10\", style=\"height:280px\"})\r\n");
                //sb.Append("<img src=\"@Url.Content(\"~/images/icon/icon_Q.gif\")\" border=\"0\" title=\"最多255個字\" />");
            }
            else if (displayName.Equals("上線日期") || displayName.Equals("下線日期"))
            {
                sb.Append("\t\t\t\t\t\t@Html.TextBox(\"" + fieldName + "\", Model." + fieldName + ".ToString(\"yyyy-MM-dd\"), new { @class = \"span2 txt1 calendar\", size = \"30\" })\r\n");
                sb.Append("\t\t\t\t\t\t@Html.ValidationMessageFor(p => p." + fieldName + ")\r\n");
            }
            else if (IsIntegerType(fieldType))
            {
                sb.Append("\t\t\t\t\t\t@Html.TextBoxFor(p => p." + fieldName + ", new { @class = \"span1 txt1\", size = \"30\"})\r\n");
                sb.Append("\t\t\t\t\t\t@Html.ValidationMessageFor(p => p." + fieldName + ")\r\n");
            }
            else
            {
                sb.Append("\t\t\t\t\t\t@Html.TextBoxFor(p => p." + fieldName + ", new { @class = \"span5 txt1\", size = \"30\"})\r\n");
                sb.Append("\t\t\t\t\t\t@Html.ValidationMessageFor(p => p." + fieldName + ")\r\n");
            }

			sb.Append("\t\t\t\t\t</td>\r\n");
			sb.Append("\t\t\t\t</tr>\r\n");
			return sb.ToString();
		}



		/// <summary>
		/// 列表頁(_Grid) 的 column (這裏需要很多客製)
		/// </summary>
		/// <param name="field"></param>
		/// <param name="fieldType"></param>
		/// <returns></returns>
		public static string List_Grid_Columns(string field, string fieldType)
		{
			StringBuilder sb = new StringBuilder();
			if (field.ToLower() == "datetime")
			{
				sb.Append("<td> @entity." + field + ".ToString(\"yyyy-MM-dd\") </td>"); 
			}
			else if (fieldType.ToLower() == "boolean" || fieldType.ToLower() == "bool")
			{
				sb.Append("<td> @((entity." + field + ")? \"是\":\"否\") </td>");
				//<td> @((entity.Online)? "是":"否") </td>
			}
			else
			{
				sb.Append("<td> @entity." + field + " </td>");
			}
			return sb.ToString();
		}

		/// <summary>
		/// 取得 List 列表頁的 header
		/// </summary>
		/// <param name="fieldName"></param>
		/// <returns></returns>
		public static string ListHeader(string displayName ,string fieldName)
		{
			return "<th width=\"50\"><img border=\"0\" src=\"@(Url.Content(\"~/images/ascending.gif\"))\" style=\"cursor:hand;cursor:pointer;\" onclick=\"javascript:orderBy({field:'" + fieldName + "',mode:'desc'});\" title=\"由大到小排序\" />&nbsp;" + displayName + "&nbsp;<img border=\"0\" src=\"@(Url.Content(\"~/images/descending.gif\"))\" style=\"cursor:hand;cursor:pointer;\" onclick=\"javascript:orderBy({field:'" + fieldName + "',mode:'asc'});\" title=\"由小到大排序\"></th>";
		}

		/// <summary>
		/// 類別 (Model) 屬性 (fiedls) 的轉換
		/// </summary>
		/// <param name="type"></param>
		/// <param name="fieldName"></param>
		/// <param name="required"></param>
		/// <returns></returns>
		public static string FieldsFormat(string displayName, string type,string fieldName,bool required)
		{
			//[Required]
			//public Guid ID { get; set; }
			StringBuilder sb_fields = new StringBuilder();

			//[Display(Name = "密碼")]
			sb_fields.AppendFormat("\t\t[Display(Name = \""+ displayName  +"\")]\r\n");

			if (required)
			{
				sb_fields.AppendFormat("\t\t[Required(ErrorMessageResourceType = typeof(Validation), ErrorMessageResourceName=\"必填\")]\r\n");
			}

			if (IsIntegerType(type))
			{
                sb_fields.AppendFormat("\t\t[RegularExpression(\"\\\\d+\", ErrorMessageResourceType=typeof(Validation), ErrorMessageResourceName=\"必須為數字\")]\r\n");
			}

			// 如果是 email 要做格式的轉換
			if (fieldName.ToLower().Contains("email"))
			{
                sb_fields.AppendFormat("\t\t[RegularExpression(\"^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$\",ErrorMessageResourceType=typeof(Validation), ErrorMessageResourceName=\"格式錯誤\")]\r\n");
			}

			sb_fields.AppendFormat("\t\tpublic {2} {3} {0} get; set; {1}\r\n",
					"{",
					"}",
					type,
					fieldName
				);

			return sb_fields.ToString();
		}
	
		/// <summary>
		/// 檢查型別名稱是否為 整數 數字
		/// </summary>
		/// <param name="typeName"></param>
		/// <returns></returns>
		public static bool IsIntegerType(string typeName)
		{
			string _typeName = typeName.ToLower(); 
			if (_typeName == "int" 
				|| _typeName == "int32"
				|| _typeName == "int16"
				|| _typeName == "int64"
				)
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// C# 物件 初始化功能
		/// </summary>
		/// <param name="type">C# 的型別</param>
		/// <param name="fieldName">屬性名稱(欄位名稱)</param>
		/// <returns></returns>
		public static string ContructorInit(string type,string fieldName)
		{
			StringBuilder sbValue = new StringBuilder();

			switch (type.ToLower())
			{
				case "datetime":
					sbValue.AppendFormat("\t\t\t{0} = DateTime.Now;\r\n",
							fieldName
						);
					break;
				case "bool":
					sbValue.AppendFormat("\t\t\t{0} = true;\r\n",
							fieldName 
						);
					break;
				case "guid":
					sbValue.AppendFormat("\t\t\t{0} = Guid.NewGuid();\r\n",
							fieldName
						);
					break;
                case "string":
                    if (fieldName.ToLower().Equals("id"))
                    {
                        sbValue.AppendFormat("\t\t\t{0} = Utils.GetObjectId();\r\n",
                                fieldName
                            );
                    }
                    break;
				default:
					break;
			}

			return sbValue.ToString();
		}


		/// <summary>
		/// 資料庫資料型別轉成 C# 資料型別
		/// </summary>
		/// <param name="type">資料庫的型別</param>
		/// <returns>C# 的資料型別</returns>
		public static string DbDataTypeConvert(string type)
		{
			string rValue = string.Empty;
			
			switch (type)
			{
				case "datetime":
					rValue = "DateTime";
					break;
				case "date":
					rValue = "Date";
					break;
				case "uniqueidentifier":
					rValue = "Guid";
					break;
				case "float":
					rValue = "double";
					break;
				case "bit":
					rValue = "bool";
					break;
				case "int":
					rValue = "int";
					break;
				case "nvarchar":
				default:
					rValue = "string";
					break;
			}
			return rValue;
		}


		public static string ConvertTitleCase(string value)
		{
			TextInfo txtInfo = new CultureInfo("en-US", false).TextInfo;
			try
			{
				return txtInfo.ToTitleCase(value.ToLower());
			}
			catch
			{
				return value;
			}
		}

		static public System.Collections.Specialized.NameValueCollection appSettings
		{
			get
			{
				return System.Configuration.ConfigurationManager.AppSettings;
			}
		}

		static public System.Configuration.ConnectionStringSettingsCollection connectionStrings
		{
			get
			{
				return System.Configuration.ConfigurationManager.ConnectionStrings;
			}
		}
	}
}
