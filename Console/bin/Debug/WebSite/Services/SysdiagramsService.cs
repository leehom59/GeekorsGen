using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Linq.Expressions;

namespace JamZooMng.Services
{
    using Models;
    using System.Data.Linq;
    using NanYi.Models;

    public class SysdiagramsService : BaseService
    {
        public SysdiagramsService() : base(new NanYiDataContext())
        { }

        public static sysdiagrams Mapping(SysdiagramsModel p)
        {
            sysdiagrams result = new sysdiagrams();
			
			result.name = p.Name;
			result.principal_id = p.Principal_Id;
			result.diagram_id = p.Diagram_Id;
			result.version = p.Version;
			result.definition = p.Definition;


            return result;
        }
        public static SysdiagramsModel Mapping(sysdiagrams p)
        {
            SysdiagramsModel result = new SysdiagramsModel();
			
			result.Name = p.name;
			result.Principal_Id = p.principal_id;
			result.Diagram_Id = p.diagram_id;
			result.Version = p.version;
			result.Definition = p.definition;

				
            return result;
        }

        public SysdiagramsListModel GetList(CriteriaMode2 criteria)
        {
            SysdiagramsListModel result = new SysdiagramsListModel();
            result.Pager = criteria.Pager;
            result.Search = criteria.Search;
            result.OrderBy = criteria.OrderBy;
            result.Criteria = criteria;

            var query = from p in _repository.List<sysdiagrams>() select p;

            if (!string.IsNullOrEmpty(criteria.Search.KeyWord))
            {
				//請自行修改.
                //query = query.Where(p => p.TITLE.Contains(criteria.Search.KeyWord));
            }

            #region 排序
            
            if (!string.IsNullOrEmpty(criteria.OrderBy.Field))
            {
				Type _type = typeof(sysdiagrams);
                PropertyInfo p = _type.GetProperty(criteria.OrderBy.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                var param = Expression.Parameter(_type, _type.Name);

                if (p.PropertyType.Name.ToLower() == "int32")
                {
                    var sortExpression = Expression.Lambda<Func<sysdiagrams, int>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "int64")
                {
                    var sortExpression = Expression.Lambda<Func<sysdiagrams, Int64>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "float")
                {
                    var sortExpression = Expression.Lambda<Func<sysdiagrams, float>>
                                   (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "datetime")
                {
                    var sortExpression = Expression.Lambda<Func<sysdiagrams, DateTime>>
                                       (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
				else if (p.PropertyType.Name.ToLower() == "boolean")
                {
                    var sortExpression = Expression.Lambda<Func<sysdiagrams, bool>>
                                      (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else
                {
                    var sortExpression = Expression.Lambda<Func<sysdiagrams, object>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
            }

            #endregion
            result.Data = query.ToList().Select(p => Mapping(p)).ToList();
            result.Pager.TotalRecords = result.Data.Count;

            result.Data = result.Data.Skip((result.Pager.PageIndex - 1) * result.Pager.PageSize).Take(result.Pager.PageSize).ToList();
            result.Pager.StartIndex = (result.Data.Count > 0) ? (result.Pager.PageIndex - 1) * result.Pager.PageSize + 1 : 0;
            result.Pager.EndIndex = (result.Data.Count > 0) ? result.Pager.StartIndex + result.Data.Count - 1 : 0;
            return result;
        }

        public SysdiagramsModel Get(string id)
        {
            string _key = Name;
            sysdiagrams Data = _repository.List<sysdiagrams>().Where(p => p.name == _key).FirstOrDefault();
                
			if (Data != null) 
            {
				return Mapping(Data);
            }
            return null;
        }

        public bool Create(SysdiagramsModel entity)
        {
            sysdiagrams dbEntity = Mapping(entity);
            try
            {
                _repository.Create<sysdiagrams>(dbEntity);
                _repository.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            string _key = Name;
            sysdiagrams Data = _repository.List<sysdiagrams>().Where(p => p.name == _key).FirstOrDefault();

			if (Data != null)
			{
				try
                {
                    _repository.Delete<sysdiagrams>(Data);
					_repository.Save();
                    return true;
				}
                catch (Exception ex)
                {  
					throw ex;
				}
            }
            return false;
        }

        public bool Update(SysdiagramsModel entity)
        {
            sysdiagrams dbEntity = _repository.List<sysdiagrams>().Where(p => p.name == entity.Name).FirstOrDefault();

            if (dbEntity != null)
            {
				dbEntity.principal_id = entity.Principal_Id;
				dbEntity.diagram_id = entity.Diagram_Id;
				dbEntity.version = entity.Version;
				dbEntity.definition = entity.Definition;

				
                try
                {
                    _repository.Save();
                    return true;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }
   }
}