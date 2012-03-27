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

    public class CateringService : BaseService
    {
        public CateringService() : base(new NanYiDataContext())
        { }

        public static CATERING Mapping(CateringModel p)
        {
            CATERING result = new CATERING();
			
			result.ID = p.Id;
			result.ICON = p.Icon;
			result.TITLE = p.Title;
			result.CONTEXT = p.Context;
			result.ORDERS = p.Orders;
			result.CREATE_DATE = p.Create_Date;
			result.S_DATE = p.S_Date;
			result.E_DATE = p.E_Date;
			result.ONLINE = p.Online;


            return result;
        }
        public static CateringModel Mapping(CATERING p)
        {
            CateringModel result = new CateringModel();
			
			result.Id = p.ID;
			result.Icon = p.ICON;
			result.Title = p.TITLE;
			result.Context = p.CONTEXT;
			result.Orders = p.ORDERS;
			result.Create_Date = p.CREATE_DATE;
			result.S_Date = (p.S_DATE.HasValue)? p.S_DATE.Value:DateTime.MinValue;
			result.E_Date = (p.E_DATE.HasValue)? p.E_DATE.Value:DateTime.MinValue;
			result.Online = p.ONLINE;

				
            return result;
        }

        public CateringListModel GetList(CriteriaMode2 criteria)
        {
            CateringListModel result = new CateringListModel();
            result.Pager = criteria.Pager;
            result.Search = criteria.Search;
            result.OrderBy = criteria.OrderBy;
            result.Criteria = criteria;

            var query = from p in _repository.List<CATERING>() select p;

            if (!string.IsNullOrEmpty(criteria.Search.KeyWord))
            {
				//請自行修改.
                //query = query.Where(p => p.TITLE.Contains(criteria.Search.KeyWord));
            }

            #region 排序
            
            if (!string.IsNullOrEmpty(criteria.OrderBy.Field))
            {
				Type _type = typeof(CATERING);
                PropertyInfo p = _type.GetProperty(criteria.OrderBy.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                var param = Expression.Parameter(_type, _type.Name);

                if (p.PropertyType.Name.ToLower() == "int32")
                {
                    var sortExpression = Expression.Lambda<Func<CATERING, int>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "int64")
                {
                    var sortExpression = Expression.Lambda<Func<CATERING, Int64>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "float")
                {
                    var sortExpression = Expression.Lambda<Func<CATERING, float>>
                                   (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "datetime")
                {
                    var sortExpression = Expression.Lambda<Func<CATERING, DateTime>>
                                       (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
				else if (p.PropertyType.Name.ToLower() == "boolean")
                {
                    var sortExpression = Expression.Lambda<Func<CATERING, bool>>
                                      (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else
                {
                    var sortExpression = Expression.Lambda<Func<CATERING, object>>
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

        public CateringModel Get(string id)
        {
            Guid _key = Guid.Parse(id);
            CATERING Data = _repository.List<CATERING>().Where(p => p.ID == _key).FirstOrDefault();
                
			if (Data != null) 
            {
				return Mapping(Data);
            }
            return null;
        }

        public bool Create(CateringModel entity)
        {
            CATERING dbEntity = Mapping(entity);
            try
            {
                _repository.Create<CATERING>(dbEntity);
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
            Guid _key = Guid.Parse(id);
            CATERING Data = _repository.List<CATERING>().Where(p => p.ID == _key).FirstOrDefault();

			if (Data != null)
			{
				try
                {
                    _repository.Delete<CATERING>(Data);
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

        public bool Update(CateringModel entity)
        {
            CATERING dbEntity = _repository.List<CATERING>().Where(p => p.ID == entity.Id).FirstOrDefault();

            if (dbEntity != null)
            {
				dbEntity.ICON = entity.Icon;
				dbEntity.TITLE = entity.Title;
				dbEntity.CONTEXT = entity.Context;
				dbEntity.ORDERS = entity.Orders;
				dbEntity.CREATE_DATE = entity.Create_Date;
				dbEntity.S_DATE = entity.S_Date;
				dbEntity.E_DATE = entity.E_Date;
				dbEntity.ONLINE = entity.Online;

				
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