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
    public class ProductBriefingMgr : Framework.Database.MgrBase
    {
        public void MergeProductBriefing(DTO_EVENT_PRODUCT_BRIEFING briefing, List<DTO_EVENT_PRODUCT_BRIEFING_PRODUCT> products)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.ProductBriefingDao dao = new Dao.ProductBriefingDao())
                    {
                        dao.MergeProductBriefing(briefing);
                        dao.DeleteProductBriefingProductAll(briefing.PROCESS_ID, briefing.UPDATER_ID);
                        foreach (DTO_EVENT_PRODUCT_BRIEFING_PRODUCT product in products)
                        {
                            dao.InsertProductBriefingProduct(product);
                        }

                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_PRODUCT_BRIEFING SelectProductBriefing(string processID)
        {
            try
            {
                using (Dao.ProductBriefingDao dao = new Dao.ProductBriefingDao())
                {
                    return dao.SelectProductBriefing(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_EVENT_PRODUCT_BRIEFING_PRODUCT> SelectProductBriefingProducts(string processID)
        {
            try
            {
                using (Dao.ProductBriefingDao dao = new Dao.ProductBriefingDao())
                {
                    return dao.SelectProductBriefingProducts(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
