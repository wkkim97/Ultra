using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Approval.Mgr
{
    public class ProductSeminarMgr : Framework.Database.MgrBase
    {
        public void MergeProductSeminar(DTO_EVENT_PRODUCT_SEMINAR seminar, List<DTO_EVENT_PRODUCT_SEMINAR_PRODUCT> products)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.ProductSeminarDao dao = new Dao.ProductSeminarDao())
                    {
                        dao.MergeProductSeminar(seminar);
                        dao.DeleteProductSeminarProductAll(seminar.PROCESS_ID, seminar.UPDATER_ID);
                        foreach (DTO_EVENT_PRODUCT_SEMINAR_PRODUCT product in products)
                        {
                            dao.InsertProductSeminarProduct(product);
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

        public DTO_EVENT_PRODUCT_SEMINAR SelectProductSeminar(string processID)
        {
            try
            {
                using (Dao.ProductSeminarDao dao = new Dao.ProductSeminarDao())
                {
                    return dao.SelectProductSeminar(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_EVENT_PRODUCT_SEMINAR_PRODUCT> SelectProductSeminarProducts(string processID)
        {
            try
            {
                using (Dao.ProductSeminarDao dao = new Dao.ProductSeminarDao())
                {
                    return dao.SelectProductSeminarProducts(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
