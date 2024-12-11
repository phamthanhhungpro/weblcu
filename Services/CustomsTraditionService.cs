using Common.Entity;
using Datas;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CustomsTraditionService : AbstractService<CustomsTradition, CustomsTraditionModel>
    {
        public CustomsTraditionService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<CustomsTradition> AppendChildData(params string[] includes)
        {
            var result = Context.CustomsTraditions.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        private CustomsTradition SetUrl(CustomsTradition entity)
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

        public override MessageResult Add(CustomsTradition entity)
        {
            var result = new MessageResult();
            try
            {
                SetUrl(entity);
                Context.CustomsTraditions.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(CustomsTraditionModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new CustomsTradition();
                data.SetNewData(entity);

                if (entity.PeopleId.HasValue)
                {
                    var people = Context.Peoples.FirstOrDefault(x => x.Id == entity.PeopleId);
                    if (people != null)
                    {
                        data.People = people;
                    }
                }
                if (entity.Attachments != null)
                {
                    foreach (var item in entity.Attachments)
                    {
                        if (!string.IsNullOrEmpty(item.Path))
                        {
                            Context.Attachments.Add(new Attachment
                            {
                                CustomsTradition = data,
                                Path = item.Path,
                                Name = item.Name
                            });
                        }
                    }
                }
                SetUrl(data);
                Context.CustomsTraditions.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public MessageResult AddByCheckName(CustomsTraditionModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new CustomsTradition();

                var people = entity.GetPeopleName();
                if (!string.IsNullOrEmpty(people))
                {
                    var typeData = Context.Peoples.FirstOrDefault(o => o.Name.ToLower().Equals(people) && o.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                    if (typeData != null)
                    {
                        data.People = typeData;
                        entity.PeopleId = typeData.Id;
                    }
                }
                data.SetNewData(entity);
                SetUrl(data);
                Context.CustomsTraditions.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(CustomsTradition entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.CustomsTraditions.FirstOrDefault(x => x.Id == entity.Id);
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
        public override MessageResult Update(CustomsTraditionModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.CustomsTraditions.Include(x => x.People)
                                                            .Include(x => x.Attachments)
                                                            .FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);

                    if (entity.PeopleId.HasValue)
                    {
                        var people = Context.Peoples.FirstOrDefault(x => x.Id == entity.PeopleId);
                        if (people != null)
                        {
                            updateData.People = people;
                        }
                        else
                        {
                            updateData.People = null;
                        }
                    }
                    else
                    {
                        updateData.People = null;
                    }
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
                                        CustomsTradition = updateData,
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
                                        CustomsTradition = updateData,
                                        Path = item.Path,
                                        Name = item.Name
                                    });
                                }
                            }
                        }
                    }
                    SetUrl(updateData);
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

        public override MessageResult Delete(int id)
        {
            var result = new MessageResult();
            try
            {
                var deleteData = Context.CustomsTraditions.FirstOrDefault(x => x.Id == id);
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
        public override CustomsTradition GetById(int id, params string[] includes)
        {
            return AppendChildData(includes).FirstOrDefault(x => x.Id == id);
        }

        public MessageResult AddView(int id)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.CustomsTraditions.FirstOrDefault(x => x.Id == id);
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

    }
}
