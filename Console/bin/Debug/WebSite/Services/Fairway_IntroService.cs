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

    public class Fairway_IntroService : BaseService
    {
        public Fairway_IntroService() : base(new NanYiDataContext())
        { }

        public static FAIRWAY_INTRO Mapping(Fairway_IntroModel p)
        {
            FAIRWAY_INTRO result = new FAIRWAY_INTRO();
			
			result.ID = p.Id;
			result.TITLE = p.Title;
			result.CONTEXT = p.Context;
			result.ORDERS = p.Orders;
			result.CREATE_DATE = p.Create_Date;
			result.S_DATE = p.S_Date;
			result.E_DATE = p.E_Date;
			result.ONLINE = p.Online;


            return result;
        }
        public static Fairway_IntroModel Mapping(FAIRWAY_INTRO p)
        {
            Fairway_IntroModel result = new Fairway_IntroModel();
			
			result.Id = p.ID;
			result.Title = p.TITLE;
			result.Context = p.CONTEXT;
			result.Orders = p.ORDERS;
			result.Create_Date = p.CREATE_DATE;
			result.S_Date = (p.S_DATE.HasValue)? p.S_DATE.Value:DateTime.MinValue;
			result.E_Date = (p.E_DATE.HasValue)? p.E_DATE.Value:DateTime.MinValue;
			result.Online = p.ONLINE;

				
            return result;
        }

        public Fairway_IntroListModel GetList(CriteriaMode2 criteria)
        {
            Fairway_IntroListModel result = new Fairway_IntroListModel();
            result.Pager = criteria.Pager;
            result.Search = criteria.Search;
            result.OrderBy = criteria.OrderBy;
            result.Criteria = criteria;

            var query = from p in _repository.List<FAIRWAY_INTRO>() select p;

            if (!string.IsNullOrEmpty(criteria.Search.KeyWord))
            {
				//請自行修改.
                //query = query.Where(p => p.TITLE.Contains(criteria.Search.KeyWord));
            }

            #region 排序
            
            if (!string.IsNullOrEmpty(criteria.OrderBy.Field))
            {
				Type _type = typeof(FAIRWAY_INTRO);
                PropertyInfo p = _type.GetProperty(criteria.OrderBy.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                var param = Expression.Parameter(_type, _type.Name);

                if (p.PropertyType.Name.ToLower() == "int32")
                {
                    var sortExpression = Expression.Lambda<Func<FAIRWAY_INTRO, int>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "int64")
                {
                    var sortExpression = Expression.Lambda<Func<FAIRWAY_INTRO, Int64>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "float")
                {
                    var sortExpression = Expression.Lambda<Func<FAIRWAY_INTRO, float>>
                                   (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "datetime")
                {
                    var sortExpression = Expression.Lambda<Func<FAIRWAY_INTRO, DateTime>>
                                       (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
				else if (p.PropertyType.Name.ToLower() == "boolean")
                {
                    var sortExpression = Expression.Lambda<Func<FAIRWAY_INTRO, bool>>
                                      (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else
                {
                    var sortExpression = Expression.Lambda<Func<FAIRWAY_INTRO, object>>
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

        public Fairway_IntroModel Get(string id)
        {
            Guid _key = Guid.Parse(id);
            FAIRWAY_INTRO Data = _repository.List<FAIRWAY_INTRO>().Where(p => p.ID == _key).FirstOrDefault();
                
			if (Data != null) 
            {
				return Mapping(Data);
            }
            return null;
        }

        public bool Create(Fairway_IntroModel entity)
        {
            FAIRWAY_INTRO dbEntity = Mapping(entity);
            try
            {
                _repository.Create<FAIRWAY_INTRO>(dbEntity);
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
            FAIRWAY_INTRO Data = _repository.List<FAIRWAY_INTRO>().Where(p => p.ID == _key).FirstOrDefault();

			if (Data != null)
			{
				try
                {
                    _repository.Delete<FAIRWAY_INTRO>(Data);
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

        public bool Update(Fairway_IntroModel entity)
        {
            FAIRWAY_INTRO dbEntity = _repository.List<FAIRWAY_INTRO>().Where(p => p.ID == entity.Id).FirstOrDefault();

            if (dbEntity != null)
            {
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