using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.WcfBase
{
    public partial class UltraEvent : IUltraEvent
    {
        #region [ Product Briefing ]

        public void MergeProductBriefing(DTO_EVENT_PRODUCT_BRIEFING briefing, List<DTO_EVENT_PRODUCT_BRIEFING_PRODUCT> products)
        {
            try
            {
                using (BSL.Approval.Mgr.ProductBriefingMgr mgr = new BSL.Approval.Mgr.ProductBriefingMgr())
                {
                    mgr.MergeProductBriefing(briefing, products);
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
                using (BSL.Approval.Mgr.ProductBriefingMgr mgr = new BSL.Approval.Mgr.ProductBriefingMgr())
                {
                    return mgr.SelectProductBriefing(processID);
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
                using (BSL.Approval.Mgr.ProductBriefingMgr mgr = new BSL.Approval.Mgr.ProductBriefingMgr())
                {
                    return mgr.SelectProductBriefingProducts(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Product Seminar ] 
        public void MergeProductSeminar(DTO_EVENT_PRODUCT_SEMINAR seminar, List<DTO_EVENT_PRODUCT_SEMINAR_PRODUCT> products)
        {
            try
            {
                using (BSL.Approval.Mgr.ProductSeminarMgr mgr = new BSL.Approval.Mgr.ProductSeminarMgr())
                {
                    mgr.MergeProductSeminar(seminar, products);
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
                using (BSL.Approval.Mgr.ProductSeminarMgr mgr = new BSL.Approval.Mgr.ProductSeminarMgr())
                {
                    return mgr.SelectProductSeminar(processID);
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
                using (BSL.Approval.Mgr.ProductSeminarMgr mgr = new BSL.Approval.Mgr.ProductSeminarMgr())
                {
                    return mgr.SelectProductSeminarProducts(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ Product Presentation Meeting ] 
        public void MergeProductPresentation(DTO_EVENT_PRODUCT_PRESENTATION seminar, List<DTO_EVENT_PRODUCT_PRESENTATION_PRODUCT> products)
        {
            try
            {
                using (BSL.Approval.Mgr.ProductPresentationMeetingMgr mgr = new BSL.Approval.Mgr.ProductPresentationMeetingMgr())
                {
                    mgr.MergeProductPresentation(seminar, products);
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
                using (BSL.Approval.Mgr.ProductPresentationMeetingMgr mgr = new BSL.Approval.Mgr.ProductPresentationMeetingMgr())
                {
                    return mgr.SelectProductPresentation(processID);
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
                using (BSL.Approval.Mgr.ProductPresentationMeetingMgr mgr = new BSL.Approval.Mgr.ProductPresentationMeetingMgr())
                {
                    return mgr.SelectProductPresentationProducts(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region [ Scientific Exchanged Meeting ]

        public void MergeScientificExchangedMeeting(DTO_EVENT_SCIENTIFIC_MEETING SEMeeting, List<DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT> products)
        {
            try
            {
                using (BSL.Approval.Mgr.ScientificExchangedMeetingMgr mgr = new BSL.Approval.Mgr.ScientificExchangedMeetingMgr())
                {
                    mgr.MergeScientificExchangedMeeting(SEMeeting, products);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_SCIENTIFIC_MEETING SelectScientificExchangedMeeting(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr.ScientificExchangedMeetingMgr mgr = new BSL.Approval.Mgr.ScientificExchangedMeetingMgr())
                {
                    return mgr.SelectScientificExchangedMeeting(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT> SelectScientificExchangedMeetingProducts(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr.ScientificExchangedMeetingMgr mgr = new BSL.Approval.Mgr.ScientificExchangedMeetingMgr())
                {
                    return mgr.SelectScientificExchangedMeetingProducts(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region [ FreeGood ] 
        public string InsertFreeGoodHCP(EventFreeGoodDto dto)
        {
            try
            {
                using (BSL.Approval.Mgr.FreeGoodMgr mgr = new BSL.Approval.Mgr.FreeGoodMgr())
                {
                    return mgr.InsertFreeGoodHCP(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteFreeGoodHCP(string processId)
        {
            try
            {
                using (BSL.Approval.Mgr.FreeGoodMgr mgr = new BSL.Approval.Mgr.FreeGoodMgr())
                {
                    mgr.DeleteFreeGoodHCP(processId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventFreeGoodHcpListDto> SelectFreeGoodHCP(string type, string processID)
        {
            try
            {
                using (BSL.Approval.Mgr.FreeGoodMgr mgr = new BSL.Approval.Mgr.FreeGoodMgr())
                {
                    return mgr.SelectFreeGoodHCP(processID, type);
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
                using (BSL.Approval.Mgr.FreeGoodMgr mgr = new BSL.Approval.Mgr.FreeGoodMgr())
                {
                    return mgr.SelectFreeGood(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsExistsFreeGoodHcpItem(string processID, string hcpCode, string hcoCode, string sampleCode, string type)
        {
            try
            {
                using (BSL.Approval.Mgr.FreeGoodMgr mgr = new BSL.Approval.Mgr.FreeGoodMgr())
                {
                    return mgr.IsExistsFreeGoodHcpItem(processID, hcpCode, hcoCode, sampleCode,type);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FreeGoodExistSampleDto> SelectExistFreeGoodSample(List<DTO_EVENT_FREE_GOOD_HCP> checkList)
        {
            try
            {
                using (BSL.Approval.Mgr.FreeGoodMgr mgr = new BSL.Approval.Mgr.FreeGoodMgr())
                {
                    return mgr.SelectExistFreeGoodSample(checkList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<FreeGoodExistSampleDto> SelectExistFreeGoodSampleRAD(List<DTO_EVENT_FREE_GOOD_HCP> checkList)
        {
            try
            {
                using (BSL.Approval.Mgr.FreeGoodMgr mgr = new BSL.Approval.Mgr.FreeGoodMgr())
                {
                    return mgr.SelectExistFreeGoodSampleRAD(checkList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region [ Clinical Related Meeting ]

        public void MergeClinicalRelatedMeeting(DTO_EVENT_CLINICAL_MEETING CRMeeting)
        {
            try
            {
                using (BSL.Approval.Mgr.ClinicalRelatedMeetingMgr mgr = new BSL.Approval.Mgr.ClinicalRelatedMeetingMgr())
                {
                    mgr.MergeClinicalRelatedMeeting(CRMeeting);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_CLINICAL_MEETING SelectClinicalRelatedMeeting(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr.ClinicalRelatedMeetingMgr mgr = new BSL.Approval.Mgr.ClinicalRelatedMeetingMgr())
                {
                    return mgr.SelectClinicalRelatedMeeting(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Congress Events ]

        public void MergeCongressEvent(DTO_EVENT_CONGRESS Congress)
        {
            try
            {
                using (BSL.Approval.Mgr.CongressEventMgr mgr = new BSL.Approval.Mgr.CongressEventMgr())
                {
                    mgr.MergeCongressEvent(Congress);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_CONGRESS SelectCongressEvent(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr.CongressEventMgr mgr = new BSL.Approval.Mgr.CongressEventMgr())
                {
                    return mgr.SelectCongressEvent(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Adventisement / Booth ]

        public void MergeAdventiseBooth(DTO_EVENT_ADVENTISEMENT Adventise)
        {
            try
            {
                using (BSL.Approval.Mgr.AdventiseBoothMgr mgr = new BSL.Approval.Mgr.AdventiseBoothMgr())
                {
                    mgr.MergeAdventiseBooth(Adventise);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_ADVENTISEMENT SelectAdventiseBooth(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr.AdventiseBoothMgr mgr = new BSL.Approval.Mgr.AdventiseBoothMgr())
                {
                    return mgr.SelectAdventiseBooth(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Employee Medical Training (EMT) ]

        public void MergeEmployeeTraining(DTO_EVENT_EMPLOYEE_TRAINING employee)
        {
            try
            {
                using (BSL.Approval.Mgr.EmployeeTrainingMgr mgr = new BSL.Approval.Mgr.EmployeeTrainingMgr())
                {
                    mgr.MergeEmployeeTraining(employee);
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
                using (BSL.Approval.Mgr.EmployeeTrainingMgr mgr = new BSL.Approval.Mgr.EmployeeTrainingMgr())
                {
                    return mgr.SelectEmployeeTraining(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Consulting/ABM(Medical MSL) Meeting ] 
        public void MergeConsultingMeeting(DTO_EVENT_CONSULTING_MEETING consulting)
        {
            try
            {
                using (BSL.Approval.Mgr.ConsultingMeetingMgr mgr = new BSL.Approval.Mgr.ConsultingMeetingMgr())
                {
                    mgr.MergeConsultingMeeting(consulting);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_CONSULTING_MEETING SelectConsultingMeeting(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr.ConsultingMeetingMgr mgr = new BSL.Approval.Mgr.ConsultingMeetingMgr())
                {
                    return mgr.SelectConsultingMeeting(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ Donation ] 
        public void MergeDonation(DTO_EVENT_DONATION donation, List<DTO_EVENT_DONATION_PRODUCT> products)
        {
            try
            {
                using (BSL.Approval.Mgr.DonationMgr mgr = new BSL.Approval.Mgr.DonationMgr())
                {
                    mgr.MergeDonation(donation, products);
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
                using (BSL.Approval.Mgr.DonationMgr mgr = new BSL.Approval.Mgr.DonationMgr())
                {
                    return mgr.SelectDonation(processID);
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
                using (BSL.Approval.Mgr.DonationMgr mgr = new BSL.Approval.Mgr.DonationMgr())
                {
                    return mgr.selectDonationProducts(processID);
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
