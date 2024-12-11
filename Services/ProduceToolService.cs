using Common.Entity;
using Datas;
using Datas.Models.DomainModels;
using Datas.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class ProduceToolService : AbstractService<ProduceTool, ProduceToolModel>
    {
        public ProduceToolService(DataContext dataContext) : base(dataContext)
        {
        }

        protected override IQueryable<ProduceTool> AppendChildData(params string[] includes)
        {
            var result = Context.ProduceTools.Where(x => x.DeleteStatus == Common.Enums.DeleteStatus.Normal).AsQueryable();
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        private ProduceTool SetUrl(ProduceTool entity)
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

        public override MessageResult Add(ProduceTool entity)
        {
            var result = new MessageResult();
            try
            {
                SetUrl(entity);
                Context.ProduceTools.Add(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Add(ProduceToolModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new ProduceTool();
                data.SetNewData(entity);
                if (entity.CategoryId.HasValue)
                {
                    var peopleType = Context.ProduceToolCategories.FirstOrDefault(x => x.Id == entity.CategoryId);
                    if (peopleType != null)
                    {
                        data.Category = peopleType;
                    }
                }
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
                                ProduceTool = data,
                                Path = item.Path,
                                Name = item.Name
                            });
                        }
                    }
                }
                SetUrl(data);
                Context.ProduceTools.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }

        public MessageResult AddByCheckName(ProduceToolModel entity)
        {
            var result = new MessageResult();
            try
            {
                var data = new ProduceTool();
                var type = entity.GetCategoryName();
                if (!string.IsNullOrEmpty(type))
                {
                    var typeData = Context.ProduceToolCategories.FirstOrDefault(o => o.Name.ToLower().Equals(type) && o.DeleteStatus == Common.Enums.DeleteStatus.Normal);
                    if (typeData != null)
                    {
                        data.Category = typeData;
                        entity.CategoryId = typeData.Id;
                    }
                }
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
                Context.ProduceTools.Add(data);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Code = Common.Enums.ErrorCode.Error;
                result.Message = ex.Message;
            }
            return result;
        }
        public override MessageResult Update(ProduceTool entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.ProduceTools.FirstOrDefault(x => x.Id == entity.Id);
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
        public override MessageResult Update(ProduceToolModel entity)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.ProduceTools.Include(o => o.Category).FirstOrDefault(x => x.Id == entity.Id);
                if (updateData != null)
                {
                    updateData.SetNewData(entity);

                    if (entity.CategoryId.HasValue)
                    {
                        var ward = Context.ProduceToolCategories.FirstOrDefault(x => x.Id == entity.CategoryId);
                        if (ward != null)
                        {
                            updateData.Category = ward;
                        }
                        else
                        {
                            updateData.Category = null;
                        }
                    }
                    else
                    {
                        updateData.Category = null;
                    }
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
                                        ProduceTool = updateData,
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
                                        ProduceTool = updateData,
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
                var deleteData = Context.ProduceTools.FirstOrDefault(x => x.Id == id);
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
        public override ProduceTool GetById(int id, params string[] includes)
        {
            return AppendChildData(includes).FirstOrDefault(x => x.Id == id);
        }

        public MessageResult AddView(int id)
        {
            var result = new MessageResult();
            try
            {
                var updateData = Context.ProduceTools.FirstOrDefault(x => x.Id == id);
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
