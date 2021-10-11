﻿using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Approval.Dao
{
    internal class ProductPresentationMeetingDao : Framework.Database.DaoBase
    {
        public void MergeProductPresentation(DTO_EVENT_PRODUCT_PRESENTATION dto)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[25];
                    parameters[0] = new SqlParameter("@PROCESS_ID", dto.PROCESS_ID);
                    parameters[1] = new SqlParameter("@SUBJECT", dto.SUBJECT);
                    parameters[2] = new SqlParameter("@EVENT_KEY", dto.EVENT_KEY);
                    parameters[3] = new SqlParameter("@PROCESS_STATUS", dto.PROCESS_STATUS);
                    parameters[4] = new SqlParameter("@REQUESTER_ID", dto.REQUESTER_ID);
                    parameters[5] = new SqlParameter("@COMPANY_CODE", dto.COMPANY_CODE);
                    parameters[6] = new SqlParameter("@ORGANIZATION_NAME", dto.ORGANIZATION_NAME);
                    parameters[7] = new SqlParameter("@LIFE_CYCLE", dto.LIFE_CYCLE);
                    parameters[8] = new SqlParameter("@START_TIME", dto.START_TIME);
                    parameters[9] = new SqlParameter("@END_TIME", dto.END_TIME);
                    parameters[10] = new SqlParameter("@KRPIA_NUMBER", dto.KRPIA_NUMBER);
                    parameters[11] = new SqlParameter("@ADDRESS_OF_VENUE", dto.ADDRESS_OF_VENUE);
                    parameters[12] = new SqlParameter("@VENUE_SELECTION_REASON", dto.VENUE_SELECTION_REASON);
                    parameters[13] = new SqlParameter("@VENUE_SELECTION_REASON_MANUAL", dto.VENUE_SELECTION_REASON_MANUAL);
                    parameters[14] = new SqlParameter("@ESTIMATED_GO", dto.ESTIMATED_GO);
                    parameters[15] = new SqlParameter("@ESTIMATED_PUBLIC", dto.ESTIMATED_PUBLIC);
                    parameters[16] = new SqlParameter("@ESTIMATED_PRIVATE", dto.ESTIMATED_PRIVATE);
                    parameters[17] = new SqlParameter("@ESTIMATED_OTHER", dto.ESTIMATED_OTHER);
                    parameters[18] = new SqlParameter("@ESTIMATED_FOREIGNER", dto.ESTIMATED_FOREIGNER);
                    parameters[19] = new SqlParameter("@ESTIMATED_BAYER", dto.ESTIMATED_BAYER);
                    parameters[20] = new SqlParameter("@PURPOSE_OBJECTIVE", dto.PURPOSE_OBJECTIVE);
                    parameters[21] = new SqlParameter("@COST_PLAN", dto.COST_PLAN);
                    parameters[22] = new SqlParameter("@IS_DISUSED", dto.IS_DISUSED);
                    parameters[23] = new SqlParameter("@CREATOR_ID", dto.CREATOR_ID);
                    parameters[24] = new SqlParameter("@UPDATER_ID", dto.UPDATER_ID);


                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_MERGE_EVENT_PRODUCT_PRESENTATION, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public DTO_EVENT_PRODUCT_PRESENTATION SelectProductPresentation(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_PRODUCT_PRESENTATION>(ApprovalContext.USP_SELECT_EVENT_PRODUCT_PRESENTATION, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        public void InsertProductPresentationProduct(DTO_EVENT_PRODUCT_PRESENTATION_PRODUCT product)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("@PROCESS_ID", product.PROCESS_ID);
                    parameters[1] = new SqlParameter("@PRODUCT_IDX", product.PRODUCT_IDX);
                    parameters[2] = new SqlParameter("@PRODUCT_CODE", product.PRODUCT_CODE);
                    parameters[3] = new SqlParameter("@PRODUCT_NAME", product.PRODUCT_NAME);
                    parameters[4] = new SqlParameter("@PRODUCT_TYPE", product.PRODUCT_TYPE);
                    parameters[5] = new SqlParameter("@IS_DELETED", product.IS_DELETED);
                    parameters[6] = new SqlParameter("@CREATOR_ID", product.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_EVENT_PRODUCT_PRESENTATION_PRODUCT, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteProductPresentationProductAll(string processID, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_EVENT_PRODUCT_PRESENTATION_PRODUCT_ALL, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_EVENT_PRODUCT_PRESENTATION_PRODUCT> SelectProductPresentationProducts(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_PRODUCT_PRESENTATION_PRODUCT>(ApprovalContext.USP_SELECT_EVENT_PRODUCT_PRESENTATION_PRODUCT, parameters);
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
