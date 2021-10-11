using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Approval.Mgr
{
    public class EmployeeTrainingMgr : Framework.Database.MgrBase
    {
        public void MergeEmployeeTraining(DTO_EVENT_EMPLOYEE_TRAINING employee)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.EmployeeTrainingDao dao = new Dao.EmployeeTrainingDao())
                    {
                        dao.MergeEmployeeTraining(employee);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_EMPLOYEE_TRAINING SelectEmployeeTraining(string processID)
        {
            try
            {
                using (Dao.EmployeeTrainingDao dao = new Dao.EmployeeTrainingDao())
                {
                    return dao.SelectEmployeeTraining(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
