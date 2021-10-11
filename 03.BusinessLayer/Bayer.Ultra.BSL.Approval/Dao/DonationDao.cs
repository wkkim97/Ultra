using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Approval.Dao
{
    internal class DonationDao : Framework.Database.DaoBase
    {
        public void MergeDonation(DTO_EVENT_DONATION dto)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[23];

					parameters[0] = new SqlParameter("@PROCESS_ID", dto.PROCESS_ID);
                    parameters[1] = new SqlParameter("@SUBJECT", dto.SUBJECT);
                    parameters[2] = new SqlParameter("@EVENT_KEY", dto.EVENT_KEY);
                    parameters[3] = new SqlParameter("@PROCESS_STATUS", dto.PROCESS_STATUS);
                    parameters[4] = new SqlParameter("@REQUESTER_ID", dto.REQUESTER_ID);
                    parameters[5] = new SqlParameter("@COMPANY_CODE", dto.COMPANY_CODE);
                    parameters[6] = new SqlParameter("@ORGANIZATION_NAME", dto.ORGANIZATION_NAME);
                    parameters[7] = new SqlParameter("@LIFE_CYCLE", dto.LIFE_CYCLE);
					parameters[8] = new SqlParameter("@TYPE", dto.TYPE);
					parameters[9] = new SqlParameter("@VALUE", dto.VALUE);
					parameters[10] = new SqlParameter("@PURPOSE", dto.PURPOSE);
					parameters[11] = new SqlParameter("@EXPLANATION", dto.EXPLANATION);
					parameters[12] = new SqlParameter("@RECIPIENT", dto.RECIPIENT);
					parameters[13] = new SqlParameter("@ADDRESS", dto.ADDRESS);
					parameters[14] = new SqlParameter("@TEL", dto.TEL);
					parameters[15] = new SqlParameter("@EMAIL", dto.EMAIL);
					parameters[16] = new SqlParameter("@IS_ELIGIBILITY", dto.IS_ELIGIBILITY);
					parameters[17] = new SqlParameter("@AFTER_RECEIPT", dto.AFTER_RECEIPT);
					parameters[18] = new SqlParameter("@COMMENT", dto.COMMENT);
					parameters[19] = new SqlParameter("@CATEGORY", dto.CATEGORY);
                    parameters[20] = new SqlParameter("@IS_DISUSED", dto.IS_DISUSED);
                    parameters[21] = new SqlParameter("@CREATOR_ID", dto.CREATOR_ID);
                    parameters[22] = new SqlParameter("@UPDATER_ID", dto.UPDATER_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_MERGE_EVENT_DONATION, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public DTO_EVENT_DONATION SelectDonation(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_DONATION>(ApprovalContext.USP_SELECT_EVENT_DONATION, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Insert Donation Product
        /// </summary>
        /// <param name="product"></param>
        public void InsertDonationProduct(DTO_EVENT_DONATION_PRODUCT product)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[10];
                    parameters[0] = new SqlParameter("@PROCESS_ID", product.PROCESS_ID);
                    parameters[1] = new SqlParameter("@PRODUCT_IDX", product.PRODUCT_IDX);
                    parameters[2] = new SqlParameter("@BU", product.BU);
                    parameters[3] = new SqlParameter("@PRODUCT_CODE", product.PRODUCT_CODE);
                    parameters[4] = new SqlParameter("@PRODUCT_NAME", product.PRODUCT_NAME);
                    parameters[5] = new SqlParameter("@BASE_PRICE", Convert.ToInt32(product.BASE_PRICE));
                    parameters[6] = new SqlParameter("@QTY", Convert.ToInt32(product.QTY));
                    parameters[7] = new SqlParameter("@AMOUNT", Convert.ToInt32(product.AMOUNT));
                    parameters[8] = new SqlParameter("@IS_DELETED", product.IS_DELETED);
                    parameters[9] = new SqlParameter("@CREATOR_ID", product.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_EVENT_DONATION_PRODUCT, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Delete Scientific Exchanged Meeting Product All
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="updaterID"></param>
        public void DeleteDonationProductAll(string processID, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_EVENT_DONATION_PRODUCT_ALL, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Select Scientific Exchanged Meeting Product
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public List<DTO_EVENT_DONATION_PRODUCT> SelectDonationProducts(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_DONATION_PRODUCT>(ApprovalContext.USP_SELECT_EVENT_DONATION_PRODUCT, parameters);
                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }
        }
    }
}
