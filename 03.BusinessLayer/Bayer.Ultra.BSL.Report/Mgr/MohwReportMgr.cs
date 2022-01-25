using Bayer.Ultra.Framework.Common.Dto.Approval;
using Bayer.Ultra.Framework.Common.Dto.Report;
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
    public class MohwReportMgr : Framework.Database.MgrBase
    {
        public List<DTO_MOHW_MARKET_RESEARCH> SelectMarketResearchSourceList(MohwConditionDto condi)
        {
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    return dao.SelectMarketResearchSourceList(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DTO_MOHW_PLURALITY_MEDICAL> SelectPluralityMedicalSourceList(MohwConditionDto condi)
        {
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    return dao.SelectPluralityMedicalSourceList(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_DIV_MEDICAL_SRC> SelectIndividualMedicalSourceList(MohwConditionDto dto)
        {
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    return dao.SelectIndividualMedicalSourceList(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //SelectParticipatnsSourceList
        public List<DTO_MOHW_PARTICIPANTS> SelectParticipantsSourceList(MohwConditionDto condi)
        {
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    return dao.SelectParticipantsSourceList(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MohwConditionListDto> SelectMohwList(string subject, string mohwType, string startDate, string endDate)
        {
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    return dao.SelectMohwList(subject, mohwType, startDate, endDate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_FREE_GOODS> SelectSampleSourceList(MohwConditionDto condi)
        {
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    return dao.SelectSampleSourceList(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_FREE_GOODS_DEVICE> SelectSampleDeviceSourceList(MohwConditionDto condi)
        {
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    return dao.SelectSampleDeviceSourceList(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MOHW_MEDICAL_STUDY> SelectMedicalStudySourceList(MohwConditionDto condi)
        {
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    return dao.SelectMedicalStudySourceList(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DTO_MOHW_KRPIA> SelectKRPIASourceList(MohwConditionDto condi)
        {
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    return dao.SelectKRPIASourceList(condi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string InsertMohwReport(DTO_MOHW_CONDITIONS condi)
        {
            string retValue = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    int mohwIdx = dao.InsertMohwCondition(condi);
                    condi.IDX = mohwIdx;
                    if (condi.MOHW_TYPE.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.MARKET_RESEARCH)
                    {
                        // TODO : 시판 후 조사 생성 로직 추가 부분
                        dao.InsertMarketResearch(condi);
                    }
                    else if (condi.MOHW_TYPE.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.DIV_MEDICAL)
                    {
                        dao.InsertIndividualMedical(condi);
                    }
                    else if (condi.MOHW_TYPE.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.PLURALITY_MEDICAL)
                    {
                        // TODO : 복수요양기간 생성 로직 추가 부분
                        dao.InsertPluralityMedical(condi);
                    }
                    else if (condi.MOHW_TYPE.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.PARTICIPANTS)
                    {
                        dao.InsertParticipants(condi);
                        // TODO : 학술대회지원 생성 로직 추가 부분
                    }
                    else if (condi.MOHW_TYPE.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.SAMPLE)
                    {
                        // TODO : 견본품 지원 생성 로직 추가 부분
                        dao.InsertMohwSample(condi);
                    }
                    else if (condi.MOHW_TYPE.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.SAMPLE_DEVICE)
                    {
                        // TODO : 견본품 지원 생성 로직 추가 부분
                        dao.InsertMohwSampleDevice(condi);
                    }
                    else if (condi.MOHW_TYPE.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.MEDICAL_STUDY)
                    {
                        // TODO : 임상시험 지원 생성 로직 추가 부분
                        dao.InsertMohwMedicalStudy(condi);
                    }
                    else if (condi.MOHW_TYPE.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.KRPIA)
                    {
                        // TODO : KRPIA 보고 생성 로직 추가 부분
                        dao.InsertMohwKRPIA(condi);
                    }

                    retValue = mohwIdx.ToString();
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                retValue = ex.Message;
                throw ex;
            }
            return retValue;
        }


        public void CreateExcelReport(string mohwIdx, string mohwType, string userId)
        {
            string FILES_ATTACH_PATH = WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath;
    
            string dirPath = string.Format(@"{0}\MOHW_Report\", Bayer.Ultra.Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath);
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                using (Bayer.Ultra.BSL.Excel.Dao.FileHandler oXls = new Excel.Dao.FileHandler())
                {
                    dao.UpdateMohwReportFilePath(mohwIdx, "EXCEL", "", "Wait", userId);

                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    string fileName = string.Format(@"{0}Excel_{1}.xlsx", dirPath, mohwIdx);

                    if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.MARKET_RESEARCH)
                    {
                        // TODO : 시판 후 조사  
                        List<DTO_MOHW_MARKET_RESEARCH> dto = dao.SelectMarketResearchReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_MARKET_RESEARCH>(dto, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.DIV_MEDICAL)
                    {
                        List<DTO_MOHW_DIV_MEDICAL_SRC> dto = dao.SelectIndividualMedicalReportList(mohwIdx);
                        Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_DIV_MEDICAL_SRC>(dto, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.PLURALITY_MEDICAL)
                    {
                        // TODO : 복수요양기간 
                        List<DTO_MOHW_PLURALITY_MEDICAL_REPORT> dto = dao.SelectPluralityMedicalReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_PLURALITY_MEDICAL_REPORT>(dto, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.PARTICIPANTS)
                    {
                        // TODO : 학술대회지원  
                        List<DTO_MOHW_PARTICIPANTS> dto = dao.SelectParticipantsReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_PARTICIPANTS>(dto, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.SAMPLE)
                    {
                        // TODO : 견본품 지원  
                        List<DTO_MOHW_FREE_GOODS> dto = dao.SelectSampleReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_FREE_GOODS>(dto, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.SAMPLE_DEVICE)
                    {
                        // TODO : 구매전 지원  
                        List<DTO_MOHW_FREE_GOODS_DEVICE> dto = dao.SelectSampleDeviceReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_FREE_GOODS_DEVICE>(dto, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.MEDICAL_STUDY)
                    {
                        // TODO : 임상시험 지원  
                        List<DTO_MOHW_MEDICAL_STUDY> dto = dao.SelectMedicalStudyReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_MEDICAL_STUDY>(dto, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.KRPIA)
                    {
                        // TODO : KRPIA 보고
                        List<DTO_MOHW_KRPIA> dto = dao.SelectKRPIAReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_KRPIA>(dto, fileName);
                    }


                    dao.UpdateMohwReportFilePath(mohwIdx, "EXCEL", fileName.Replace(FILES_ATTACH_PATH, Bayer.Ultra.Core.Consts.FILES_ATTACH_PATH_PREFIX), "Created", userId);
                    dao.UpdateMohwStatus(mohwIdx, mohwType, Bayer.Ultra.Framework.Common.ApprovalUtil.MOHW_STATUS.Excel.ToString(), userId);
                }

            }
            catch (Exception ex)
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    dao.UpdateMohwReportFilePath(mohwIdx, "EXCEL", ex.Message, "Fail", userId);
                } 
            }
        }

        public void CreateMOHWReport(string mohwIdx, string mohwType, string userId)
        {
            string FILES_ATTACH_PATH = WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath;
            List<ReportColumnsDto> columns = null;
            string dirPath = string.Format(@"{0}\MOHW_Report\", Bayer.Ultra.Framework.Config.WebSiteConfigHandler.WebServer.UploadFile.Attach.PhysicalPath);
            try
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                using (Bayer.Ultra.BSL.Excel.Dao.FileHandler oXls = new Excel.Dao.FileHandler())
                {
                    columns = GetColumnTitle(mohwType);
                    dao.UpdateMohwReportFilePath(mohwIdx, "MOHW", "", "Wait", userId);

                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    string fileName = string.Format(@"{0}MOHW_{1}.xlsx", dirPath, mohwIdx);

                    if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.MARKET_RESEARCH)
                    {
                        // TODO : 시판 후 조사 Columns 정의
                        List<DTO_MOHW_MARKET_RESEARCH> dto = dao.SelectMarketResearchReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_MARKET_RESEARCH>(dto, columns, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.DIV_MEDICAL)
                    {
                        //TODO : 개별요양 기관
                        List<DTO_MOHW_DIV_MEDICAL> dto = dao.SelectMOHWIndividualMedical(mohwIdx);
                        Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_DIV_MEDICAL>(dto, columns, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.PLURALITY_MEDICAL)
                    {
                        // TODO : 복수요양기간 Columns 정의
                        List<DTO_MOHW_PLURALITY_MEDICAL_REPORT> dto = dao.SelectPluralityMedicalReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_PLURALITY_MEDICAL_REPORT>(dto, columns, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.PARTICIPANTS)
                    {
                        // TODO : 학술대회지원 Columns 정의
                        List<DTO_MOHW_PARTICIPANTS> dto = dao.SelectParticipantsMohwReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_PARTICIPANTS>(dto, columns, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.SAMPLE)
                    { 
                        // TODO : 견본품 지원  
                        List<DTO_MOHW_FREE_GOODS_REPORT> dto = dao.SelectSampleMohwReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_FREE_GOODS_REPORT>(dto, columns, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.SAMPLE_DEVICE)
                    {
                        // TODO : 구매전 기기 
                        List<DTO_MOHW_FREE_GOODS_DEVICE_REPORT> dto = dao.SelectSampleDeviceMohwReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_FREE_GOODS_DEVICE_REPORT>(dto, columns, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.MEDICAL_STUDY)
                    {
                        // TODO : 임상시험 지원  
                        List<DTO_MOHW_MEDICAL_STUDY_REPORT> dto = dao.SelectMedicalStudyMohwReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_MEDICAL_STUDY_REPORT>(dto, columns, fileName);
                    }
                    else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.MEDICAL_STUDY)
                    {
                        // TODO : KRPIA 협회 보고  
                        List<DTO_MOHW_KRPIA_REPORT> dto = dao.SelectKRPIAMohwReportList(mohwIdx);
                        Bayer.Ultra.BSL.Excel.Dao.FileHandler.CreateExcelDocument<DTO_MOHW_KRPIA_REPORT>(dto, columns, fileName);
                    }


                    dao.UpdateMohwReportFilePath(mohwIdx, "MOHW", fileName.Replace(FILES_ATTACH_PATH, Bayer.Ultra.Core.Consts.FILES_ATTACH_PATH_PREFIX), "Created", userId);

                }

            }
            catch (Exception ex)
            {
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    dao.UpdateMohwReportFilePath(mohwIdx, "MOHW", ex.Message, "Fail", userId);
                }
            }
        }

        public string UpdateMohwStatus(string idx, string mohwType, string status, string userid)
        {
            string returnVal = string.Empty;
            int result = 0;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                using (Dao.MohwReportDao dao = new Dao.MohwReportDao())
                {
                    if(status.Equals(Framework.Common.ApprovalUtil.MOHW_STATUS.Complete.ToString()))
                    {
                        result = dao.UpdateConfrimMohw(idx, mohwType, userid);
                    }
                    
                    // 각 Report Source 테이블에 Confirm 업데이트
                    // 기존에 1건이상 존재하면 -1을 리턴하여 업데이트 하지 않음.
                    if (result == 0) 
                    {
                        dao.UpdateMohwStatus(idx, mohwType, status, userid);
                        returnVal = "OK";
                        scope.Complete();
                    }
                    else if (result < 0)
                    {
                        // 기존에 확정이 존재할 경우
                        returnVal = "EXISTS_CONFIRM";
                    }  
                }
                return returnVal;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<ReportColumnsDto> GetColumnTitle(string mohwType)
        {
            List<ReportColumnsDto> retValue = null;

            if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.MARKET_RESEARCH)
            {
                // TODO : 시판 후 조사 Columns 정의
                retValue = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "CONTRACT_ID" , Title = "연번" },
                        new ReportColumnsDto() { Field = "PRODUCT_STANDARD_NAME", Title = "제품(표준코드명)"   },
                        new ReportColumnsDto() { Field = "PAYMENT_WAY" , Title = "재심사대상여부"  },
                        new ReportColumnsDto() { Field = "HCP_NAME", Title = "성명"   },
                        new ReportColumnsDto() { Field = "HCO_NAME", Title = "소속"   },
                        new ReportColumnsDto() { Field = "QTY"  , Title = "단가/건" },
                        new ReportColumnsDto() { Field = "AMOUNT" , Title = "건수"   }
                    };
            }
            else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.DIV_MEDICAL)
            {
                retValue = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "ROW_NUM" , Title = "연번" },
                        new ReportColumnsDto() { Field = "PRODUCT_NAME", Title = "제품명(표준코드명칭)"   },
                        new ReportColumnsDto() { Field = "HCO_NAME", Title = "기관명칭"   },
                        new ReportColumnsDto() { Field = "HCO_HIRA_CODE", Title = "요양기관번호"   },
                        new ReportColumnsDto() { Field = "HCP_NAMES"  , Title = "성명" },
                        new ReportColumnsDto() { Field = "AMOUNT"  , Title = "지원금액" },
                        new ReportColumnsDto() { Field = "ADDRESS_OF_VENUE" , Title = "장소"   },
                        new ReportColumnsDto() { Field = "EVENT_DATE" , Title = "일시"   }
                    };
            }
            else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.PLURALITY_MEDICAL)
            {
                // TODO : 복수요양기간 Columns 정의
                retValue = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "ROW_NUM" , Title = "연번" },
                        new ReportColumnsDto() { Field = "PRODUCT_STANDARD_NAME", Title = "제품명 (표준코드명칭)"   },
                        new ReportColumnsDto() { Field = "HCP_NAME", Title = "성명"   },
                        new ReportColumnsDto() { Field = "HCO_NAME", Title = "소속"   },
                        new ReportColumnsDto() { Field = "TRANSPORTATION_AMOUNT"  , Title = "교통비" },
                        new ReportColumnsDto() { Field = "GIMMICKSOUVENIR_AMOUNT"  , Title = "기념품비" },
                        new ReportColumnsDto() { Field = "ACCOMMODATION_AMOUNT"  , Title = "숙박비" },
                        new ReportColumnsDto() { Field = "MEALBEVERAGE_AMOUNT"  , Title = "식음료비" },
                        new ReportColumnsDto() { Field = "ADDRESS_OF_VENUE" , Title = "장소"   },
                        new ReportColumnsDto() { Field = "DATE_TIME" , Title = "일시"   }
                    };
            }
            else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.PARTICIPANTS)
            {
                // TODO : 학술대회지원 Columns 정의
                retValue = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "EVENT_KEY" , Title = "연번" },
                        new ReportColumnsDto() { Field = "HOST", Title = "주최기관"   },
                        new ReportColumnsDto() { Field = "SUBJECT", Title = "대회명칭"   },
                        new ReportColumnsDto() { Field = "VENUE", Title = "대회장소"   },
                        new ReportColumnsDto() { Field = "START_TIME"  , Title = "대회일시" },
                        new ReportColumnsDto() { Field = "AMOUNT"  , Title = "지원금액" },
                    };
            }
            else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.SAMPLE)
            {
                // TODO : 견본품 지원 Columns 정의
                retValue = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "EVENT_KEY" , Title = "①연번" },
                        new ReportColumnsDto() { Field = "HCO_NAME", Title = "②기관명칭"   },
                        new ReportColumnsDto() { Field = "HCO_HIRA_CODE"  , Title = "③요양기관기호" },
                        new ReportColumnsDto() { Field = "PRODUCT_STANDARD_NAME", Title = "④제품명 (표준코드명칭)"   },
                        new ReportColumnsDto() { Field = "PRODUCT_STANDARD_CODE", Title = "⑤표준코드(제품코드)"   },
                        new ReportColumnsDto() { Field = "PRODUCT_QTY"  , Title = "⑥포장내총수량(규격)" },
                        new ReportColumnsDto() { Field = "QTY"  , Title = "⑦제공수량" },
                        new ReportColumnsDto() { Field = "TOTAL_QTY"  , Title = "⑧계(⑥*⑦)" },
                        new ReportColumnsDto() { Field = "DELIVERY_DATE"  , Title = "⑨제공일자" }
                    };
            }
            else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.SAMPLE_DEVICE)
            {
                // TODO : 견본품 지원 Columns 정의
                retValue = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "EVENT_KEY" , Title = "①연번" },
                        new ReportColumnsDto() { Field = "HCO_NAME", Title = "②기관명칭"   },
                        new ReportColumnsDto() { Field = "HCO_HIRA_CODE"  , Title = "③요양기관기호" },
                        new ReportColumnsDto() { Field = "PRODUCT_STANDARD_NAME", Title = "④명칭"   },
                        new ReportColumnsDto() { Field = "PRODUCT_STANDARD_CODE", Title = "⑤허가.인증 또는 신고 번호"   },
                        new ReportColumnsDto() { Field = "PRODUCT_QTY"  , Title = "⑥포장단위" },
                        new ReportColumnsDto() { Field = "QTY"  , Title = "⑦제공수량" },
                        new ReportColumnsDto() { Field = "TOTAL_QTY"  , Title = "⑧계(⑥*⑦)" },
                        new ReportColumnsDto() { Field = "DELIVERY_DATE"  , Title = "도착일자" },
                        new ReportColumnsDto() { Field = "RETURN_DATE"  , Title = "회수일자" },
                        new ReportColumnsDto() { Field = ""  , Title = "구매일자" }
                        
                    };
            }
            else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.MEDICAL_STUDY)
            {
                // TODO : 임상시험 지원 Columns 정의
                retValue = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "INDEX" , Title = "연번" },
                        new ReportColumnsDto() { Field = "TITLE", Title = "임상시험명칭" },
                        new ReportColumnsDto() { Field = "TYPE"  , Title = "구분" },
                        new ReportColumnsDto() { Field = "APPROVAL_NO", Title = "승인번호"   },
                        new ReportColumnsDto() { Field = "APPROVAL_DATE", Title = "승인일자"   },
                        new ReportColumnsDto() { Field = "HEAD_HCP"  , Title = "임상시험책임자" },
                        new ReportColumnsDto() { Field = "HEAD_HCO"  , Title = "책임자소속" },
                        new ReportColumnsDto() { Field = "OTHER_HCP"  , Title = "공동연구자" },
                        new ReportColumnsDto() { Field = "PAYMENT_COST"  , Title = "연구비" },
                        new ReportColumnsDto() { Field = "FREE_GOODS"  , Title = "지원내역" },
                        //임사시험지원에 최초계약일 표시
                        new ReportColumnsDto() { Field = "CONTRACT_DATE"  , Title = "계약일" }
                        
                    };
            }
            else if (mohwType.ToUpper() == Framework.Common.ApprovalUtil.MOHW_TYPE.KRPIA)
            {
                // TODO : KRPIA 협회 보고Columns 정의
                retValue = new List<ReportColumnsDto>()
                    {
                        new ReportColumnsDto() { Field = "START_DATE" , Title = "START_DATE" },
                        new ReportColumnsDto() { Field = "END_DATE", Title = "END_DATE" },
                        new ReportColumnsDto() { Field = "LOCATION"  , Title = "LOCATION" },
                        new ReportColumnsDto() { Field = "PURPOSE", Title = "PURPOSE"   },
                        new ReportColumnsDto() { Field = "HCP_NAME", Title = "HCP_NAME"   },
                        new ReportColumnsDto() { Field = "HCO_NAME"  , Title = "HCO_NAME" },
                        new ReportColumnsDto() { Field = "YOUR_DOCES_AMOUNT"  , Title = "YOUR_DOCES_AMOUNT" },
                        new ReportColumnsDto() { Field = "SUBJECT"  , Title = "SUBJECT" },
                        new ReportColumnsDto() { Field = "CLEARING_DATE"  , Title = "CLEARING_DATE" },
                        new ReportColumnsDto() { Field = "REMARK"  , Title = "REMARK" },
                        new ReportColumnsDto() { Field = "KRPIA"  , Title = "KRPIA" },
                        new ReportColumnsDto() { Field = "YOUR_DOCES_VENDER"  , Title = "YOUR_DOCES_VENDER" },
                        new ReportColumnsDto() { Field = "PRODUCT_NAME"  , Title = "PRODUCT_NAME" },
                        new ReportColumnsDto() { Field = "EVENT_ID"  , Title = "EVENT_ID" },
                        new ReportColumnsDto() { Field = "PROCESS_ID"  , Title = "PROCESS_ID" },
                        new ReportColumnsDto() { Field = "EVENT_NAME"  , Title = "EVENT_NAME" },
                        new ReportColumnsDto() { Field = "HCO_CODE"  , Title = "HCO_CODE" },
                        new ReportColumnsDto() { Field = "EVENT_KEY"  , Title = "EVENT_KEY" },
                        new ReportColumnsDto() { Field = "ULTRA_AMOUNT"  , Title = "ULTRA_AMOUNT" },
                        new ReportColumnsDto() { Field = "ROLE_TYPE"  , Title = "ROLE_TYPE" },
                        new ReportColumnsDto() { Field = "PROCESS_STATUS"  , Title = "PROCESS_STATUS" },
                        new ReportColumnsDto() { Field = "CNT"  , Title = "CNT" }
                    };
            }
            return retValue;
        }


    }
}
