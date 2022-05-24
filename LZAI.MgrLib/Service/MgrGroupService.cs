using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSTFrame.MVC;
using PSTFrame.MVC.Model;
using LZAI.MgrLib.Model.DB;
using LZAI.MgrLib.Repository;
using PSTFrame.Data;

namespace LZAI.MgrLib.Service
{
    public class MgrGroupService : BaseService<MgrGroup, int>
    {
        MgrGroupRepository _MgrGroupRepository;
        private IRepositoryContext DbContext;
        public MgrGroupService(IRepositoryContext context)
        {
            DbContext = context;
            _MgrGroupRepository = new MgrGroupRepository(context);
        }
        /// <summary>
        /// 取得MgrFuncType清單
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MgrGroup> GetMgrGroupList()
        {
            return _MgrGroupRepository.GetList("Where IsDelete = @IsDelete", new { @IsDelete = 0 });
        }

        /// <summary>
        /// 查詢
        /// </summary>
        /// <returns></returns>
        public List<MgrGroup> GetList(string QueryGroupName)
        {
            return _MgrGroupRepository.GetList(QueryGroupName).ToList();
        }

        public List<MgrGroup> GetList()
        {
            return _MgrGroupRepository.GetUseList().ToList();
        }

        /// <summary>
        /// 依Id取得資料
        /// </summary>
        /// <param name="serId">資料Id</param>
        /// <returns></returns>
        public MgrGroup GetById(int serId)
        {
            return _MgrGroupRepository.Get(serId);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="mgrGroup"></param>
        /// <returns></returns>
        public MessageModel InsertData(MgrGroup mgrGroup)
        {
            try
            {
                var data = _MgrGroupRepository.GetList(new { IsDelete = false, GroupName = mgrGroup.GroupName });
                if (data.Count() > 0)
                {
                    return new MessageModel()
                    {
                        Status = false,
                        Message = "這個群組名稱已存在"
                    };
                }
                _MgrGroupRepository.Insert(mgrGroup);
                var result = new MessageModel()
                {
                    Status = true,
                    Message = "儲存成功"
                };
                return result;
            }
            catch (Exception ex)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = $"新增失敗，原因{ex.Message}"
                };
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="mgrGroup">The source.</param>
        /// <returns></returns>
        public MessageModel UpdateData(MgrGroup mgrGroup)
        {
            try
            {
                var data = _MgrGroupRepository.Get(mgrGroup.GroupId);
                if (data == null)
                {
                    return new MessageModel()
                    {
                        Status = false,
                        Message = "資料不存在"
                    };
                }
                var data2 = _MgrGroupRepository.GetList(new { IsDelete = false, GroupName = mgrGroup.GroupName });
                if (data2.Count() > 0)
                {
                    return new MessageModel()
                    {
                        Status = false,
                        Message = "這個群組名稱已存在"
                    };
                }
                data.GroupName = mgrGroup.GroupName;
                data.UpdateDateTime = mgrGroup.UpdateDateTime;
                data.UpdateUserId = mgrGroup.UpdateUserId;
                data.UpdateUnitId = mgrGroup.UpdateUnitId;
                _MgrGroupRepository.Update(data);
                var result = new MessageModel()
                {
                    Status = true,
                    Message = "儲存成功"
                };
                return result;
            }
            catch (Exception ex)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = $"修改失敗，原因{ex.Message}"
                };
            }
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="VillageId">要刪除的資料Id</param>
        /// <returns></returns>
        public MessageModel DeleteData(int GroupId)
        {
            try
            {
                var data = _MgrGroupRepository.Get(GroupId);
                if (data == null)
                {
                    return new MessageModel()
                    {
                        Status = false,
                        Message = "資料不存在"
                    };
                }

                data.UpdateDateTime = DateTime.Now;
                data.IsDelete = true;
                data.UpdateUserId = 1;
                data.UpdateUnitId = 1;

                _MgrGroupRepository.Update(data);

                var result = new MessageModel()
                {
                    Status = true,
                    Message = "刪除成功"
                };
                return result;
            }
            catch(Exception ex)
            {
                return new MessageModel()
                {
                    Status = false,
                    Message = $"刪除失敗，原因{ex.Message}"
                };
            }
        }
    }
}
