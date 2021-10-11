using Bayer.Ultra.Framework.Common.Dto.Radiology;
using Bayer.Ultra.Framework.Config;

using HiQPdf;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Report.Mgr
{
    public class MicroMarketingMgr : Framework.Database.MgrBase
    {
        public List<AssignedHospitalListDto> SelectAssignedHospitalList(string user_id, string user_type)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    return dao.SelectAssignedHospitalList(user_id, user_type);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Equipment
        public List<HospitalEquipmentDto> SelectEquipment(string id, string organization_id)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    return dao.SelectEquipment(id, organization_id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MergeEquipment(HospitalEquipmentDto dto)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    dao.MergeEquipment(dto);
                }
                return "ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteEquipment(string id)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    dao.DeleteEquipment(id);
                }
                return "ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Examination
        public List<HospitalExaminationDto> SelectExamination(string id, string organization_id, string quarter)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    return dao.SelectExamination(id, organization_id, quarter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MergeExamination(MergeHospitalExaminationDto dto)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    dao.MergeExamination(dto);
                }
                return "ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteExamination(string id)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    dao.DeleteExamination(id);
                }
                return "ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Market Share
        public List<HospitalMarketShareDto> SelectMarketShare(string id, string organization_id, string quarter)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    return dao.SelectMarketShare(id, organization_id, quarter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MergeMarketShare(MergeHospitalMarketShareDto dto)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    dao.MergeMarketShare(dto);
                }
                return "ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteMarketShare(string id)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    dao.DeleteMarketShare(id);
                }
                return "ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        //Micromarketing function
        #region Master_MarketShare
        public List<MasterMarketShare> SelectMasterMarketShare(string id)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    return dao.SelectMasterMarketShare(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string MergeMasterMarketShare(MasterMarketShare dto)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    dao.MergeMasterMarketShare(dto);
                }
                return "ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteMasterMarketShare(string id)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    dao.DeleteMasterMarketShare(id);
                }
                return "ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MasterMarketShare> SelectSearchMasterMarketShare(string family, string product)
        {
            try
            {
                using (Dao.MicroMarketingDao dao = new Dao.MicroMarketingDao())
                {
                    return dao.SelectSearchMasterMarketShare(family, product);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        #endregion
    }
}
