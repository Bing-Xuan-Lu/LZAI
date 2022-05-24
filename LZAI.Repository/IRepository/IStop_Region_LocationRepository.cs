using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LZAI.Model.DB;
using LZAI.Repository.Repository;
using PSTFrame.Data;

namespace LZAI.Repository.IRepository
{
    public interface IStop_Region_LocationRepository : IRepository<Stop_Region_Location, int>
    {
        int InsertStop_Region_LocationAndTM97XY(List<Stop_Region_Location> locations);

        int sp_Ins_Stop_Region_Geometry(int id);
        int DeleteRegionLocationAndGeometry(int id);
    }
}
