using LZAI.Model.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSTFrame.MVC;

namespace LZAI.Service.IService
{
    public interface IStopRegionService
    {
        IEnumerable<Stop_Region_File> GetStopRegionFiles();
        Stop_Region_File GetStopRegionFile(int id);
        IEnumerable<PublicCode> GetRegionType();
        MessageModel InsertRegion(Stop_Region_File stopRegionFile, string regionPoints);
        MessageModel DeleteRegion(Stop_Region_File stopRegionFile);
        IEnumerable<Stop_Region_Location> GetStopRegionPoints(int id);
    }
}
