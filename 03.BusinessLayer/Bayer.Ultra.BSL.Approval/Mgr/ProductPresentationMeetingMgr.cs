using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Approval.Mgr
{
    public class ProductPresentationMeetingMgr : Framework.Database.MgrBase
    {
        public void MergeProductPresentation(DTO_EVENT_PRODUCT_PRESENTATION seminar, List<DTO_EVENT_PRODUCT_PRESENTATION_PRODUCT> products)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.ProductPresentationMeetingDao dao = new Dao.ProductPresentationMeetingDao())
                    {
                        dao.MergeProductPresentation(seminar);
                        dao.DeleteProductPresentationProductAll(seminar.PROCESS_ID, seminar.UPDATER_ID);
                        foreach (DTO_EVENT_PRODUCT_PRESENTATION_PRODUCT product in products)
                        {
                            dao.InsertProductPresentationProduct(product);
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

        public DTO_EVENT_PRODUCT_PRESENTATION SelectProductPresentation(string processID)
        {
            try
            {
                using (Dao.ProductPresentationMeetingDao dao = new Dao.ProductPresentationMeetingDao())
                {
                    return dao.SelectProductPresentation(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_EVENT_PRODUCT_PRESENTATION_PRODUCT> SelectProductPresentationProducts(string processID)
        {
            try
            {
                using (Dao.ProductPresentationMeetingDao dao = new Dao.ProductPresentationMeetingDao())
                {
                    return dao.SelectProductPresentationProducts(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
