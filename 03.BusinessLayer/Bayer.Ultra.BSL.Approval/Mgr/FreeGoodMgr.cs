using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Bayer.Ultra.Framework;

namespace Bayer.Ultra.BSL.Approval.Mgr
{
    public class FreeGoodMgr : Framework.Database.MgrBase
    {
        public string InsertFreeGoodHCP(EventFreeGoodDto dto)
        {
            string retValue = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                    {
                        dao.InsertFreeGood(dto.dtoFreeGood);

                        dao.DeleteFreeGoodHCP(dto.dtoFreeGood.PROCESS_ID);
                        foreach (DTO_EVENT_FREE_GOOD_HCP hcp in dto.dtoHcp)
                        {
                            dao.MedifyFreeGoodHCP(hcp);
                        }
                        retValue = "OK";
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                retValue = "Fail";
                throw ex;
            }
            return retValue;
        }
        public void DeleteFreeGoodHCP(string processId)
        {
            try
            {
                using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                {
                    dao.DeleteFreeGoodHCP(processId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DTO_EVENT_FREE_GOOD SelectFreeGood(string processID)
        {
            try
            {
                using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                {
                    return dao.SelectFreeGood(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventFreeGoodHcpListDto> SelectFreeGoodHCP(string processID, string type)
        {
            try
            {
                using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                {
                    return dao.SelectFreeGoodHcp(processID, type);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public bool IsExistsFreeGoodHcpItem(string processId, string hcpcode, string hcocode, string sampleCode, string type)
        {
            bool retValue = false;
            try
            {
                using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                {
                    string val = dao.IsExistsFreeGoodHcpItem(processId, hcpcode, hcocode, sampleCode, type);
                    if (val.ToUpper().Equals("EXISTS"))
                    {
                        retValue = true;
                    }
                    else
                    {
                        retValue = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retValue;
        }

        public List<FreeGoodExistSampleDto> SelectExistFreeGoodSample(List<DTO_EVENT_FREE_GOOD_HCP> checkList)
        {
            try
            {
                List<FreeGoodExistSampleDto> returnValue = new List<FreeGoodExistSampleDto>();
                using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                {
                    foreach (DTO_EVENT_FREE_GOOD_HCP hcp in checkList)
                    {
                        if (hcp.IDX != null) continue; //IDX가 없는것들은 신규로 들어온 것이다. 
                        returnValue.AddRange(dao.SelectExistFreeGoodSample(hcp.HCP_CODE, hcp.HCP_NAME, hcp.HCO_CODE, hcp.SAMPLE_CODE));
                    }

                }

                return returnValue;
            }
            catch
            {
                throw;
            }
        }

        public List<FreeGoodExistSampleDto> SelectExistFreeGoodSampleRAD(List<DTO_EVENT_FREE_GOOD_HCP> checkList)
        {
            try
            {
                List<FreeGoodExistSampleDto> returnValue = new List<FreeGoodExistSampleDto>();
                using (Dao.FreeGoodDao dao = new Dao.FreeGoodDao())
                {
                    foreach (DTO_EVENT_FREE_GOOD_HCP hcp in checkList)
                    {
                        if (hcp.IDX != null) continue; //IDX가 없는것들은 신규로 들어온 것이다. 
                        returnValue.AddRange(dao.SelectExistFreeGoodSampleRAD(hcp.HCP_CODE, hcp.HCP_NAME, hcp.HCO_CODE, hcp.SAMPLE_CODE));
                    }

                }

                return returnValue;
            }
            catch
            {
                throw;
            }
        }

    }
}
