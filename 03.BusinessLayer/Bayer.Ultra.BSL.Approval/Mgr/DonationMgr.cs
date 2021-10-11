using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Approval.Mgr
{
    public class DonationMgr : Framework.Database.MgrBase
    {
        public void MergeDonation(DTO_EVENT_DONATION donation, List<DTO_EVENT_DONATION_PRODUCT> products)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.DonationDao dao = new Dao.DonationDao())
                    {
                        dao.MergeDonation(donation);
                        dao.DeleteDonationProductAll(donation.PROCESS_ID, donation.UPDATER_ID);
                        foreach (DTO_EVENT_DONATION_PRODUCT product in products)
                        {
                            dao.InsertDonationProduct(product);
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

        public DTO_EVENT_DONATION SelectDonation(string processID)
        {
            try
            {
                using (Dao.DonationDao dao = new Dao.DonationDao())
                {
                    return dao.SelectDonation(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DTO_EVENT_DONATION_PRODUCT> selectDonationProducts(string processID)
        {
            try
            {
                using (Dao.DonationDao dao = new Dao.DonationDao())
                {
                    return dao.SelectDonationProducts(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
    }
}
