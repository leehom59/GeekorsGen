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

    public class PartnerService : BaseService
    {
        public PartnerService() : base(new NanYiDataContext())
        { }

        public static PARTNER Mapping(PartnerModel p)
        {
            PARTNER result = new PARTNER();

			//result.{} = p.{}
						result.ID = p.Id;
			result.CNAME = p.Cname;
			result.LINK = p.Link;
			result.PHONE = p.Phone;
			result.AREAID = p.Areaid;
			result.ADDRESS = p.Address;
			result.GMAP_LNG = p.Gmap_Lng;
			result.GMAP_LAT = p.Gmap_Lat;
			result.DISCOUNT_TITLE = p.Discount_Title;
			result.DISCOUNT_IMAGE = p.Discount_Image;
			result.DISCOUNT_DESC = p.Discount_Desc;
			result.LOCATION_ADDRESS = p.Location_Address;
			result.LOCATION_GMAP_LNG = p.Location_Gmap_Lng;
			result.LOCATION_GMAP_LAT = p.Location_Gmap_Lat;
			result.ORDERS = p.Orders;
			result.CREATE_DATE = p.Create_Date;
			result.S_DATE = p.S_Date;
			result.E_DATE = p.E_Date;
			result.ONLINE = p.Online;


            return result;
        }
        public static PartnerModel Mapping(PARTNER p)
        {
            PartnerModel result = new PartnerModel();

			//result.{} = p.{}
						result.Id = p.ID;
			result.Cname = p.CNAME;
			result.Link = p.LINK;
			result.Phone = p.PHONE;
			result.Areaid = p.AREAID;
			result.Address = p.ADDRESS;
			result.Gmap_Lng = p.GMAP_LNG;
			result.Gmap_Lat = p.GMAP_LAT;
			result.Discount_Title = p.DISCOUNT_TITLE;
			result.Discount_Image = p.DISCOUNT_IMAGE;
			result.Discount_Desc = p.DISCOUNT_DESC;
			result.Location_Address = p.LOCATION_ADDRESS;
			result.Location_Gmap_Lng = p.LOCATION_GMAP_LNG;
			result.Location_Gmap_Lat = p.LOCATION_GMAP_LAT;
			result.Orders = p.ORDERS;
			result.Create_Date = p.CREATE_DATE;
			result.S_Date = (p.S_DATE.HasValue)? p.S_DATE.Value:DateTime.MinValue;
			result.E_Date = (p.E_DATE.HasValue)? p.E_DATE.Value:DateTime.MinValue;
			result.Online = p.ONLINE;

				
            return result;
        }

        public PartnerListModel GetList(CriteriaMode2 criteria)
        {
            PartnerListModel result = new PartnerListModel();
            result.Pager = criteria.Pager;
            result.Search = criteria.Search;
            result.OrderBy = criteria.OrderBy;
            result.Criteria = criteria;

            var query = from p in _repository.List<PARTNER>() select p;

            if (!string.IsNullOrEmpty(criteria.Search.KeyWord))
            {
                query = query.Where(p => p.TITLE.Contains(criteria.Search.KeyWord));
            }

            #region 排序
            
            if (!string.IsNullOrEmpty(criteria.OrderBy.Field))
            {
				Type _type = typeof(PARTNER);
                PropertyInfo p = _type.GetProperty(criteria.OrderBy.Field, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                var param = Expression.Parameter(_type, _type.Name);

                if (p.PropertyType.Name.ToLower() == "int32")
                {
                    var sortExpression = Expression.Lambda<Func<PARTNER, int>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "int64")
                {
                    var sortExpression = Expression.Lambda<Func<PARTNER, Int64>>
                                    (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "float")
                {
                    var sortExpression = Expression.Lambda<Func<PARTNER, float>>
                                   (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else if (p.PropertyType.Name.ToLower() == "datetime")
                {
                    var sortExpression = Expression.Lambda<Func<PARTNER, DateTime>>
                                       (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
				else if (p.PropertyType.Name.ToLower() == "boolean")
                {
                    var sortExpression = Expression.Lambda<Func<PARTNER, bool>>
                                      (Expression.Convert(Expression.Property(param, p.Name), p.PropertyType), param);

                    if (criteria.OrderBy.Mode.ToLower().Equals("asc"))
                        query = query.OrderBy(sortExpression);
                    else
                        query = query.OrderByDescending(sortExpression);
                }
                else
                {
                    var sortExpression = Expression.Lambda<Func<PARTNER, object>>
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

        public PartnerModel Get(string id)
        {
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                PARTNER Data = _repository.List<PARTNER>().Where(p => p.ID == guid).FirstOrDefault();
                
                if (Data != null) 
                {
                    return Mapping(Data);
                }
            }
            return null;
        }

        public bool Create(PartnerModel entity)
        {
            PARTNER dbEntity = Mapping(entity);
            try
            {
                _repository.Create<PARTNER>(dbEntity);
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
            Guid guid;
            if (Guid.TryParse(id, out guid))
            {
                PARTNER Data = _repository.List<PARTNER>().Where(p => p.ID == guid).FirstOrDefault();

                if (Data != null)
                {
                    try
                    {
                        _repository.Delete<PARTNER>(Data);
						_repository.Save();
                        return true;
                    }
                    catch (Exception ex)
                    {  }
                }
            }
            return false;
        }

        public bool Update(PartnerModel entity)
        {
            PARTNER dbEntity = _repository.List<PARTNER>().Where(p => p.ID == entity.Id).FirstOrDefault();

            if (dbEntity != null)
            {
                				dbEntity.CNAME = entity.Cname;
				dbEntity.LINK = entity.Link;
				dbEntity.PHONE = entity.Phone;
				dbEntity.AREAID = entity.Areaid;
				dbEntity.ADDRESS = entity.Address;
				dbEntity.GMAP_LNG = entity.Gmap_Lng;
				dbEntity.GMAP_LAT = entity.Gmap_Lat;
				dbEntity.DISCOUNT_TITLE = entity.Discount_Title;
				dbEntity.DISCOUNT_IMAGE = entity.Discount_Image;
				dbEntity.DISCOUNT_DESC = entity.Discount_Desc;
				dbEntity.LOCATION_ADDRESS = entity.Location_Address;
				dbEntity.LOCATION_GMAP_LNG = entity.Location_Gmap_Lng;
				dbEntity.LOCATION_GMAP_LAT = entity.Location_Gmap_Lat;
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
                    
                }
            }
            return false;
        }
   }
}