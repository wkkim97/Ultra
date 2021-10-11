using Bayer.Ultra.Framework.Common.Dto.Medical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Medical.Mgr
{
    public class StudyMgr_Tx : Framework.Database.MgrBase
    { 
        public string ModifyMedicalInfo(DTO_MEDICAL_INFO dto)
        {
            string retValue = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.StudyDao dao = new Dao.StudyDao())
                    {
                        dao.ModifyMedicalInfo(dto);
                        retValue = "OK";
                    }
                    scope.Complete();
                }
            }
            catch 
            {
                retValue = "FAIL";
                throw;
            }

            return retValue;
        }
         
        public string ModifyMedical(MedicalDto dto)
        {
            string retValue = string.Empty;
            int medicalidx = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.StudyDao dao = new Dao.StudyDao())
                    {
                        medicalidx = dao.ModifyMedicalInfo(dto.medicalInfo);
                        // Medical study 신규가 아닐경우 기존에 등록되여 있는 Products, Editor(reviewer)를 제거한다
                        dto.medicalInfo.MEDICAL_IDX = medicalidx;
                        if (dto.medicalInfo.MEDICAL_IDX > 0)
                        {
                            dao.DeleteMedicalProducts(dto.medicalInfo.MEDICAL_IDX.ToString());
                            dao.DeleteMedicalReViewer(dto.medicalInfo.MEDICAL_IDX.ToString());
                        }
                        
                        if(dto.products != null)
                        {
                            foreach (DTO_MEDICAL_PRODUCTS prod in dto.products)
                            {
                                prod.MEDICAL_IDX = medicalidx;
                                dao.ModifyMedicalProducts(prod);
                            }
                        }

                        if (dto.editors != null)
                        {
                            foreach (DTO_MEDICAL_EDITOR edit in dto.editors)
                            {
                                edit.MEDICAL_IDX = medicalidx;
                                dao.ModifyMedicalReViewer(edit);
                            }
                        }
                         
                        retValue = "OK|" + medicalidx.ToString();
                    }
                    scope.Complete();
                }
                InsertMedicalStudyLog(medicalidx.ToString());
            }
            catch
            {
                retValue = "FAIL";
                throw;
            }
            

            return retValue;
        }

        public string DeleteMedicalInfo(string medicalIdx)
        {
            string retValue = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.StudyDao dao = new Dao.StudyDao())
                    {
                        dao.DeleteMedicalInfo(medicalIdx);
                        retValue = "OK";
                    }
                    scope.Complete();
                }
                InsertMedicalStudyLog(medicalIdx);
            }
            catch
            {
                retValue = "FAIL";
                throw;
            }

            return retValue;
        }
        
        public string ModifyHcpContract(DTO_MEDICAL_HCP_CONTRACT dto)
        {
            string retValue = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.StudyDao dao = new Dao.StudyDao())
                    {
                        dao.ModifyHpcContract(dto);
                        retValue = "OK";
                    }
                    scope.Complete();
                }
                InsertMedicalHcpContractLog(dto.MEDICAL_IDX.ToString(), dto.HCP_CODE);
            }
            catch
            {
                retValue = "FAIL";
                throw;
            }

            return retValue;
        }

        public string ModifyHcrContract(List<DTO_MEDICAL_HCR_CONTRACT> dto)
        {
            string retValue = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.StudyDao dao = new Dao.StudyDao())
                    {
                        foreach(DTO_MEDICAL_HCR_CONTRACT dtoItem in dto)
                        {
                            dao.ModifyHcrContract(dtoItem);
                        } 
                        retValue = "OK";
                    }
                    scope.Complete();
                }
                if(dto.Count > 0)
                {
                    InsertMedicalHcpContractLog(dto.FirstOrDefault().MEDICAL_IDX.ToString(), dto.FirstOrDefault().HCP_CODE);
                }
     
            }
            catch
            {
                retValue = "FAIL";
                throw;
            }

            return retValue;
        }
         
        public string DeleteContractHCP(string medicalIdx, string hcpcode)
        {
            string retValue = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.StudyDao dao = new Dao.StudyDao())
                    {
                        dao.DeleteContractHCP(medicalIdx, hcpcode);
                        retValue = "OK";
                    }
                    scope.Complete();
                }
                InsertMedicalHcpContractLog(medicalIdx, hcpcode);
            }
            catch
            {
                retValue = "FAIL";
                throw;
            }

            return retValue;
        }

        public void InsertMedicalStudyLog(string medicalIdx)
        {
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    dao.InsertMedicalStudyLog(medicalIdx);

                }
            }
            catch { } 
        }


        public void InsertMedicalHcpContractLog(string medicalIdx, string hcpcode)
        {
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    dao.InsertMedicalHcpContractLog(medicalIdx, hcpcode);
                }
            }
            catch { }
        }

        public string ModifyHcpPayment(DTO_MEDICAL_HCP_PAYMENT dto)
        {
            string retValue = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.StudyDao dao = new Dao.StudyDao())
                    {
                        dao.ModifyHpcPayment(dto);
                        retValue = "OK";
                    }
                    scope.Complete();
                } 
            }
            catch
            {
                retValue = "FAIL";
                throw;
            }

            return retValue;
        }
        public string ModifyIMP(DTO_MEDICAL_IMP dto)
        {
            string retValue = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.StudyDao dao = new Dao.StudyDao())
                    {
                        dao.ModifyIMP(dto);
                        retValue = "OK";
                    }
                    scope.Complete();
                }
            }
            catch
            {
                retValue = "FAIL";
                throw;
            }

            return retValue;
        }
    }
}
