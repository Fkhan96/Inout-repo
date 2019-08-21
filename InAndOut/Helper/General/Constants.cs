
namespace InAndOut.Helper.General
{
    enum Role
    {
        Admin=1,
        Customer=2,
        Other=3,
        AKAM=4,
        KAM=5,
        MT_DIRECTOR=6,
        VPCD=7,
        CDF=8,
        IBEX=9,
    }
    enum SalaryType
    {
        Daily=0,
        Monthly =1        
    }
    enum UPLType
    {
        UPL=1,
        UPLF=2
    }
    enum ClaimStatus
    {
        Pending = 1,
        Rejected = 2,
        Approved = 3,
    }
    enum VocationalLeave
    {
        VocationalLeave = 1,
        EarlyLeave = 2,
        LeaveofAbsence = 3,
        Present = 4
    }
    public enum ResultStatus
    {
        Success = 100,
        Error = 200,
        NotFound = 300,
        Warning = 400,
        InProcess = 500
    }
}