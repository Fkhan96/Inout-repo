using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InAndOut.DTO
{
    public class ShiftDTO
    {
        public int FK_CompanyID { get; set; }
        public List<ShiftSettingDTO> shiftSetting { get; set; }
    }
    public class ShiftSettingDTO
    {
        public ShiftTypes ShiftType { get; set; }
        public bool IsSet { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

    }

    public enum ShiftTypes
    {
        Morning = 1,
        Afternoon = 2,
        Evening = 3,
        Night = 4
    }
}