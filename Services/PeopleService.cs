using Common.Entity;
using Datas;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class PeopleService : AbstractService<People, PeopleModel>
    {
        public PeopleService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<People> AppendChildData(params string[] includes)
        {
            var result = Context.Peoples.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        private People SetUrl(People entity)
        {
            entity.ReNewUrl();
            var checkUrl = this.GetAll(o => o.Url.Contains(entity.Url) && o.Id != entity.Id).ToList();
            bool isCheck = false;
            var url = entity.Url;
            int count = 0;
            do
            {
                if (checkUrl.Any(o => o.Url.Equals(url)))
                {
                    isCheck = true;
                    count++;
                    url = entity.Url + "-" + count;
                }
                else
                    isCheck = false;
            }
            while (isCheck);
            entity.Url = url;
            return entity;
        }

        public IEnumerable<People> GetAllByDelete(params string[] includes)
        {
            var result = Context.Peoples.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.IsDelete).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public IEnumerable<People> GetAllByDelete(Expression<Func<People, bool>> predicate, params string[] includes)
        {
            var result = Context.Peoples.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.IsDelete).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result.Where(predicate);
        }

        public override MessageResult Add(People entity)
        {
            var result = new MessageResult();
            try
            {
                SetUrl(entity);
                Context.Peoples.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(PeopleModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new People();
                data.SetNewData(entity);
                if (entity.PeopleCategoryId.HasValue)
                {
                    var peopleType = Context.PeopleCategories.FirstOrDefault(x => x.Id == entity.PeopleCategoryId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                    if (peopleType != null)
                    {
                        data.PeopleCategory = peopleType;
                    }
                }
                if (entity.ParentId.HasValue)
                {
                    var parent = Context.Peoples.FirstOrDefault(x => x.Id == entity.ParentId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                    if (parent != null)
                    {
                        data.Parent = parent;
                    }
                }
                SetUrl(data);
                //if (entity.DistrictId.HasValue)
                //{
                //    var district = Context.Districts.FirstOrDefault(x => x.Id == entity.DistrictId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                //    if (district != null)
                //    {
                //        data.District = district;
                //    }
                    
                //}
                
                //if (entity.WardId.HasValue)
                //{
                //    var ward = Context.Wards.FirstOrDefault(x => x.Id == entity.WardId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                //    if (ward != null)
                //    {
                //        data.Ward = ward;
                //    }
                   
                //}

                if (entity.LanguageIds != null && entity.LanguageIds.Count >0)
                {
                    var lst = new List<Language>();
                    foreach (var item in entity.LanguageIds.Distinct())
                    {
                        var district = Context.Languages.FirstOrDefault(x => x.Id == item && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                        if (district != null)
                        {
                            lst.Add(district);
                        }
                    }
                    data.Languages = lst;
                }
  

                foreach (var item in entity.Locations)
                {
                    if(!string.IsNullOrEmpty(item.Lat) || !string.IsNullOrEmpty(item.Long) || item.DistrictId.HasValue)
                    {
                        var loca = new Location
                        {
                            People = data,
                            Lat = item.Lat,
                            Long = item.Long
                        };
                        if(item.DistrictId.HasValue)
                        {
                            var district = Context.Districts.FirstOrDefault(x => x.Id == item.DistrictId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                            if (district != null)
                            {
                                loca.District = district;
                            }
                        }
                        if (item.WardId.HasValue)
                        {
                            var ward = Context.Wards.FirstOrDefault(x => x.Id == item.WardId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                            if (ward != null)
                            {
                                loca.Ward = ward;
                            }
                        }
                        Context.Locations.Add(loca);
                    }    
                }
                if(entity.Attachments !=null)
                {
                    foreach (var item in entity.Attachments)
                    {
                        if (!string.IsNullOrEmpty(item.Path))
                        {
                            Context.Attachments.Add(new Attachment
                            {
                                People = data,
                                Path = item.Path,
                                Name = item.Name
                            });
                        }
                    }
                }    
                if (entity.IsConfirm == 1)
                {
                    Context.PeopleConfirmes.Add(new PeopleConfirm
                    {
                        Details = entity.ConfirmData,
                        ConfirmStatus = Common.Enums.PeopleConfirmStatus.Request,
                        People = data,
                    });
                    data.ConfirmStatus = Common.Enums.PeopleConfirmStatus.Request;
                }
                else if(entity.IsConfirm == 2)
                {
                    Context.PeopleConfirmes.Add(new PeopleConfirm
                    {
                        Details = entity.ConfirmData,
                        ConfirmStatus = Common.Enums.PeopleConfirmStatus.Confirm,
                        People = data,
                    });
                    data.ConfirmStatus = Common.Enums.PeopleConfirmStatus.Confirm;
                }
                else if (entity.IsConfirm == 3)
                {
                    Context.PeopleConfirmes.Add(new PeopleConfirm
                    {
                        Details = entity.ConfirmData,
                        ConfirmStatus = Common.Enums.PeopleConfirmStatus.Reject,
                        People = data,
                    });
                    data.ConfirmStatus = Common.Enums.PeopleConfirmStatus.Reject;
                }
                Context.Peoples.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public MessageResult AddByCheckName(PeopleModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new People();
                var type = entity.GetPeopleCategory();
                if (!string.IsNullOrEmpty(type))
                {
                    var typeData = Context.PeopleCategories.FirstOrDefault(o => o.Name.ToLower().Equals(type) && o.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                    if (typeData != null)
                    {
                        data.PeopleCategory = typeData;
                        entity.PeopleCategoryId = typeData.Id;
                    }
                }
                var parentText = entity.GetPeopleCategory();
                if (entity.ParentId.HasValue)
                {
                    var parent = Context.Peoples.FirstOrDefault(o => o.Name.ToLower().Equals(type) && o.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                    if (parent != null)
                    {
                        data.Parent = parent;
                    }
                }
                data.SetNewData(entity);
                SetUrl(data);
                //var location = new LocationModel();
                //location.People = data;
                //location.Lat = entity.Lat;
                //location.Long = entity.Long;
                //Context.Peoples.Add(data);
                //Context.Locations.Add(location);
                Context.Peoples.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(People entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.Peoples.FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);
                    SetUrl(entity);
                    updateData.UpdateDate = DateTime.Now;
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.OtherNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult<Common.Enums.PeopleConfirmStatus> Update(PeopleModel entity)
        {
            var result = new MessageResult<Common.Enums.PeopleConfirmStatus>();
            try
            {
                var updateData = Context.Peoples.Include(o => o.PeopleCategory).Include(o=>o.Parent).Include(o=>o.Languages).Include(o=> o.Locations).ThenInclude(o=>o.Ward).ThenInclude(o=>o.District).FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    result.Value = updateData.ConfirmStatus;
                    updateData.SetNewData(entity);
                   
                    if (entity.PeopleCategoryId.HasValue)
                    {
                        var ward = Context.PeopleCategories.FirstOrDefault(x => x.Id == entity.PeopleCategoryId);
                        if (ward != null)
                        {
                            updateData.PeopleCategory = ward;
                        }
                        else
                        {
                            updateData.PeopleCategory = null;
                        }
                    }
                    else
                    {
                        updateData.PeopleCategory = null;
                    }
                    if (entity.ParentId.HasValue)
                    {
                        var parent = Context.Peoples.FirstOrDefault(x => x.Id == entity.ParentId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                        if (parent != null)
                        {
                            updateData.Parent = parent;
                        }
                        else
                        {
                            updateData.ParentId = null;
                            updateData.Parent = null;
                        }
                    }
                    else
                    {
                        updateData.ParentId = null;
                        updateData.Parent = null;
                    }

                    if (entity.LanguageIds !=null && entity.LanguageIds.Count >0)
                    {

                        var lst = new List<Language>();
                        foreach (var item in entity.LanguageIds.Distinct())
                        {
                            var district = Context.Languages.FirstOrDefault(x => x.Id == item && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                            if (district != null)
                            {
                                lst.Add(district);
                            }
                        }
                        updateData.Languages = lst;
                    }
                    else
                    {
                        updateData.Languages = new List<Language>();
                    }
                    SetUrl(updateData);
                    updateData.UpdateDate = DateTime.Now;
                    if (entity.Attachments != null)
                    {
                        foreach (var item in entity.Attachments)
                        {
                            if (item.Id > 0)
                            {
                                var location = updateData.Attachments.FirstOrDefault(o => o.Id.Equals(item.Id));
                                if (location != null)
                                {
                                    if (!string.IsNullOrEmpty(item.Path))
                                    {
                                        location.Path = item.Path;
                                        location.Name = item.Name;
                                    }
                                    else
                                    {
                                        location.DeleteStatus = Common.Enums.DeleteStatus.IsDelete;
                                    }
                                }
                                else if (!string.IsNullOrEmpty(item.Path))
                                {
                                    Context.Attachments.Add(new Attachment
                                    {
                                        People = updateData,
                                        Path = item.Path,
                                        Name = item.Name
                                    });
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(item.Path))
                                {
                                    Context.Attachments.Add(new Attachment
                                    {
                                        People = updateData,
                                        Path = item.Path,
                                        Name = item.Name
                                    });
                                }
                            }
                        }
                    }
                    foreach (var item in entity.Locations)
                    {
                        if (item.Id > 0)
                        {
                            var location = updateData.Locations.FirstOrDefault(o=>o.Id.Equals(item.Id));
                            if(location!=null)
                            {
                                if (!string.IsNullOrEmpty(item.Lat) || !string.IsNullOrEmpty(item.Long) || item.DistrictId.HasValue)
                                {
                                    if (item.DistrictId.HasValue)
                                    {
                                        var district = Context.Districts.FirstOrDefault(x => x.Id == item.DistrictId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                                        if (district != null)
                                        {
                                            location.District = district;
                                        }
                                        else
                                        {
                                            location.DistrictId = null;
                                            location.District = null;
                                        }
                                    }
                                    else
                                    {
                                        location.DistrictId = null;
                                        location.District = null;
                                    }
                                    if (item.WardId.HasValue)
                                    {
                                        var ward = Context.Wards.FirstOrDefault(x => x.Id == item.WardId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                                        if (ward != null)
                                        {
                                            location.Ward = ward;
                                        }
                                        else
                                        {
                                            location.WardId = null;
                                            location.Ward = null;
                                        }
                                    }
                                    else
                                    {
                                        location.WardId = null;
                                        location.Ward = null;
                                    }
                                    location.Lat = item.Lat;
                                    location.Long = item.Long;
                                }
                                else
                                {
                                    location.DeleteStatus = Common.Enums.DeleteStatus.IsDelete;
                                }    
                            }
                            else if(!string.IsNullOrEmpty(item.Lat) || !string.IsNullOrEmpty(item.Long) || item.DistrictId.HasValue)
                            {
                                var loca = new Location
                                {
                                    People = updateData,
                                    Lat = item.Lat,
                                    Long = item.Long
                                };
                                if (item.DistrictId.HasValue)
                                {
                                    var district = Context.Districts.FirstOrDefault(x => x.Id == item.DistrictId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                                    if (district != null)
                                    {
                                        loca.District = district;
                                    }
                                }
                                if (item.WardId.HasValue)
                                {
                                    var ward = Context.Wards.FirstOrDefault(x => x.Id == item.WardId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                                    if (ward != null)
                                    {
                                        loca.Ward = ward;
                                    }
                                }
                                Context.Locations.Add(loca);
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(item.Lat) || !string.IsNullOrEmpty(item.Long) || item.DistrictId.HasValue)
                            {
                                var loca = new Location
                                {
                                    People = updateData,
                                    Lat = item.Lat,
                                    Long = item.Long
                                };
                                if (item.DistrictId.HasValue)
                                {
                                    var district = Context.Districts.FirstOrDefault(x => x.Id == item.DistrictId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                                    if (district != null)
                                    {
                                        loca.District = district;
                                    }
                                }
                                if (item.WardId.HasValue)
                                {
                                    var ward = Context.Wards.FirstOrDefault(x => x.Id == item.WardId && x.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                                    if (ward != null)
                                    {
                                        loca.Ward = ward;
                                    }
                                }
                                Context.Locations.Add(loca);
                            }
                        }    
                    }
                    if (entity.IsConfirm == 1)
                    {
                        Context.PeopleConfirmes.Add(new PeopleConfirm
                        {
                            Details = entity.ConfirmData,
                            ConfirmStatus = Common.Enums.PeopleConfirmStatus.Request,
                            People = updateData,
                        });
                        updateData.ConfirmStatus = Common.Enums.PeopleConfirmStatus.Request;
                    }
                    else if (entity.IsConfirm == 2)
                    {
                        Context.PeopleConfirmes.Add(new PeopleConfirm
                        {
                            Details = entity.ConfirmData,
                            ConfirmStatus = Common.Enums.PeopleConfirmStatus.Confirm,
                            People = updateData,
                        });
                        updateData.ConfirmStatus = Common.Enums.PeopleConfirmStatus.Confirm;
                    }
                    else if (entity.IsConfirm == 3)
                    {
                        Context.PeopleConfirmes.Add(new PeopleConfirm
                        {
                            Details = entity.ConfirmData,
                            ConfirmStatus = Common.Enums.PeopleConfirmStatus.Reject,
                            People = updateData,
                        });
                        updateData.ConfirmStatus = Common.Enums.PeopleConfirmStatus.Reject;
                    }
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.OtherNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public  MessageResult<Common.Enums.PeopleConfirmStatus> UpdateConfirm(PeopleModel entity)
        {
            var result = new MessageResult<Common.Enums.PeopleConfirmStatus>();
            try
            {
                var updateData = Context.Peoples.Include(o => o.PeopleCategory).Include(o => o.Parent).Include(o => o.Languages).Include(o => o.Locations).FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    result.Value = updateData.ConfirmStatus;
                    if (entity.IsConfirm == 1)
                    {
                        Context.PeopleConfirmes.Add(new PeopleConfirm
                        {
                            Details = entity.ConfirmData,
                            ConfirmStatus = Common.Enums.PeopleConfirmStatus.Request,
                            People = updateData,
                        });
                        updateData.ConfirmStatus = Common.Enums.PeopleConfirmStatus.Request;
                    }
                    else if (entity.IsConfirm == 2)
                    {
                        Context.PeopleConfirmes.Add(new PeopleConfirm
                        {
                            Details = entity.ConfirmData,
                            ConfirmStatus = Common.Enums.PeopleConfirmStatus.Confirm,
                            People = updateData,
                        });
                        updateData.ConfirmStatus = Common.Enums.PeopleConfirmStatus.Confirm;
                    }
                    else if (entity.IsConfirm == 3)
                    {
                        Context.PeopleConfirmes.Add(new PeopleConfirm
                        {
                            Details = entity.ConfirmData,
                            ConfirmStatus = Common.Enums.PeopleConfirmStatus.Reject,
                            People = updateData,
                        });
                        updateData.ConfirmStatus = Common.Enums.PeopleConfirmStatus.Reject;
                    }
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.OtherNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public override MessageResult Delete(int id)
        {
            var result = new MessageResult();
            try
            {
                var deleteData = Context.Peoples.FirstOrDefault(x => x.Id == id);
                if (deleteData != null)
                {
                    if (!deleteData.IsExistAnother())
                    {
                        deleteData.DeleteStatus = Common.Enums.DeleteStatus.IsDelete;
                        deleteData.UpdateDate = DateTime.Now;
                        Context.SaveChanges();
                    }
                    else
                    {
                        result.Code = Common.Enums.ErrorCode.OtherExitsAnother;
                    }
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.OtherNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public MessageResult UnDelete(int id)
        {
            var result = new MessageResult();
            try
            {
                var deleteData = Context.Peoples.FirstOrDefault(x => x.Id == id);
                if (deleteData != null)
                {
                    if (!deleteData.IsExistAnother())
                    {
                        deleteData.DeleteStatus = Common.Enums.DeleteStatus.Normal;
                        deleteData.UpdateDate = DateTime.Now;
                        Context.SaveChanges();
                    }
                    else
                    {
                        result.Code = Common.Enums.ErrorCode.OtherExitsAnother;
                    }
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.OtherNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public MessageResult AddView(int id)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.Peoples.FirstOrDefault(x => x.Id == id);
                if (updateData != null)
                {
                    updateData.View = updateData.View + 1;
                    Context.SaveChanges();
                }
                else
                {
                    result.Code = Common.Enums.ErrorCode.OtherNotExit;
                }
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override People GetById(int id, params string[] includes)
        {
            return AppendChildData(includes).FirstOrDefault(x => x.Id == id);
        }

    }
}
