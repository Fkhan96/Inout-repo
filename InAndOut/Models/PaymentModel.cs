using InAndOut.Helper.Custom;
using InAndOut.Helper.General;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace InAndOut.Models
{
    public class PaymentModel
    {
        public static Result<PaymentViewModel> GetAttendanceFilter(PaymentSearchViewModel model)
        {
            try
            {
                using (var context = new DBContext())
                {
                    #region SP
                    List<PaymentViewModel> Result = new List<PaymentViewModel>();
                    List<SqlParameter> SqlParameter = new List<SqlParameter>();

                    SqlParameter.Add(new SqlParameter("@EmpID", model.EmpID == null ? (object)DBNull.Value : model.EmpID));
                    SqlParameter.Add(new SqlParameter("@StartDate", model.startDate == null || model.startDate == DateTime.MinValue ? (object)SqlDateTime.Null : model.startDate));
                    SqlParameter.Add(new SqlParameter("@EndDate", model.endDate == null || model.endDate == DateTime.MinValue ? (object)SqlDateTime.Null : model.endDate.AddDays(1)));

                    Result = context.Database.SqlQuery<PaymentViewModel>("exec [dbo].[AttendanceDetails]  @startDate,@endDate",
                      SqlParameter.ToArray()).ToList();
                    #endregion

                    if (Result != null)
                        return new Result<PaymentViewModel>()
                        {
                            Status = ResultStatus.Success,
                            Data = Result
                        };
                    else
                        return new Result<PaymentViewModel>()
                        {
                            Status = ResultStatus.NotFound,
                        };
                }

            }
            catch (Exception ex)
            {
                return new Result<PaymentViewModel>() { Status = ResultStatus.Error, Message = ex.Message.ToString(), Data = null };
            }
        }
    }
}