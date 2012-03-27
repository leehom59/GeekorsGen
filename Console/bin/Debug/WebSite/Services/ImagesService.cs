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

    public class ImagesService : BaseService
    {
        public ImagesService() : base(new NanYiDataContext())
        { }

        public static IMAGES Mapping(ImagesModel p)
        {
            IMAGES result = new IMAGES();
			
			result.ID = p.Id;
			result.FID = p.Fid;
			result.CNAME = p.Cname;
			result.ORDERS = p.Orders;
			result.CREATE_DATE = p.Create_Date;


            return result;
        }
        public static ImagesModel Mapping(IMAGES p)
        {
            ImagesModel result = new ImagesModel();
			
			result.Id = p.ID;
			result.Fid = p.FID;
			result.Cname = p.CNAME;
			result.Orders = p.ORDERS;
			result.Create_Date = p.CREATE_DATE;

				
            return result;
        }

        public ImagesListModel GetList(CriteriaMode2 criteria)
        {
            ImagesListModel result = new ImagesListModel();
            result.Pager = criteria.Pager;
            result.Search = criteria.Search;
            result.OrderBy = criteria.OrderBy;
            result.Criteria = criteria;

            var query = from p in _repository.List<IMAGES>() select p;

            if (!string.IsNullOrEmpty(criteria.Search.KeyWord))
            {
				//請自行修改.
                //query = query.Where(p => p.TITLE.Contains(criteria.Search.KeyWord));
            }

            #region 排序
            
            if (!string.IsNullOrEmpty(criteria.OrderBy.Field))
            {
				Type _type = typeof(IMAGES);
                PropertyInfo p = _type.GetProperty(criteria.OrderBy.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                var param = Expression.Parameter(_type, _type.Name);

                if (p.PropertyType.Name.ToLower() == "int32")
                {
                    var sortExpression = Expression.Lambda<Func<IMAGES, int>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "int64")
                {
                    var sortExpression = Expression.Lambda<Func<IMAGES, Int64>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "float")
                {
                    var sortExpression = Expression.Lambda<Func<IMAGES, float>>
                                   (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "datetime")
                {
                    var sortExpression = Expression.Lambda<Func<IMAGES, DateTime>>
                                       (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
				else if (p.PropertyType.Name.ToLower() == "boolean")
                {
                    var sortExpression = Expression.Lambda<Func<IMAGES, bool>>
                                      (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else
                {
                    var sortExpression = Expression.Lambda<Func<IMAGES, object>>
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

        public ImagesModel Get(string id)
        {
            int _key = int.Parse(id);
            IMAGES Data = _repository.List<IMAGES>().Where(p => p.ID == _key).FirstOrDefault();
                
			if (Data != null) 
            {
				return Mapping(Data);
            }
            return null;
        }

        public bool Create(ImagesModel entity)
        {
            IMAGES dbEntity = Mapping(entity);
            try
            {
                _repository.Create<IMAGES>(dbEntity);
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
            int _key = int.Parse(id);
            IMAGES Data = _repository.List<IMAGES>().Where(p => p.ID == _key).FirstOrDefault();

			if (Data != null)
			{
				try
                {
                    _repository.Delete<IMAGES>(Data);
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

        public bool Update(ImagesModel entity)
        {
            IMAGES dbEntity = _repository.List<IMAGES>().Where(p => p.ID == entity.Id).FirstOrDefault();

            if (dbEntity != null)
            {
				dbEntity.FID = entity.Fid;
				dbEntity.CNAME = entity.Cname;
				dbEntity.ORDERS = entity.Orders;
				dbEntity.CREATE_DATE = entity.Create_Date;

				
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