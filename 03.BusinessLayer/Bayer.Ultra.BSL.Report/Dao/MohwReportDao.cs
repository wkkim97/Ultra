using Bayer.Ultra.Framework.Common.Dto.Report;
using Bayer.Ultra.Framework.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Report.Dao
{
    internal class MohwReportDao : Framework.Database.DaoBase
    {
        /// <summary>
        /// Report MOHW Conditions 생성
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int InsertMohwCondition(DTO_MOHW_CONDITIONS dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    var result = _context.Database.SqlQuery<int>(ReportContext.USP_INSERT_REPORT_MOHW_CONDITION, ParameterMapper.Mapping(dto));
                    return Convert.ToInt32(result.FirstOrDefault().ToString());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Report MOHW Conditions Excel File Path Update
        /// </summary> 
        /// <param name="idx"></param>
        /// <param name="mohwtype"></param>
        /// <param name="path"></param>
        public void UpdateMohwReportFilePath(string idx, string type, string path, string status, string userId)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@IDX", idx);
                    parameters[1] = new SqlParameter("@TYPE", type);
                    parameters[2] = new SqlParameter("@PATH", path);
                    parameters[3] = new SqlParameter("@STATUS", status);
                    parameters[4] = new SqlParameter("@USER_ID", userId);

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_MERGE_REPORT_MOHW_FILES, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///Mohw List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<MohwConditionListDto> SelectMohwList(string subject, string mohwType, string startDate, string endDate)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@SUBJECT", subject);
                    parameters[1] = new SqlParameter("@MOHW_TYPE", mohwType);
                    parameters[2] = new SqlParameter("@START_DATE", startDate);
                    parameters[3] = new SqlParameter("@END_DATE", endDate);


                    var result = _context.Database.SqlQuery<MohwConditionListDto>(ReportContext.USP_SELECT_REPORT_MOHW_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///Mohw Status Update
        /// </summary> 
        /// <returns></returns>
        public void UpdateMohwStatus(string idx, string mohwType, string status, string userId)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@IDX", idx);
                    parameters[1] = new SqlParameter("@MOHW_TYPE", mohwType);
                    parameters[2] = new SqlParameter("@STATUS", status);
                    parameters[3] = new SqlParameter("@USER_ID", userId);

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_UPDATE_REPORT_MOHW_STATUS, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        public int UpdateConfrimMohw(string idx, string mohwType, string userId)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@IDX", idx);
                    parameters[1] = new SqlParameter("@MOHW_TYPE", mohwType);
                    parameters[2] = new SqlParameter("@USER_ID", userId);

                    var result = _context.Database.SqlQuery<int>(ReportContext.USP_UPDATE_CONFIRM_MOHW, parameters);
                    return Convert.ToInt32(result.FirstOrDefault());
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  MarketResearchSourceList (시품 후 조사)  List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<DTO_MOHW_MARKET_RESEARCH> SelectMarketResearchSourceList(MohwConditionDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[1] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[2] = new SqlParameter("@USER_ID", "");

                    var result = _context.Database.SqlQuery<DTO_MOHW_MARKET_RESEARCH>(ReportContext.USP_SELECT_REPORT_MARKET_RESEARCH_SOURCE_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        //SelectParticipantsSourceList
        /// <summary>
        ///  ParticipantsSourceList (학술대회)  List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<DTO_MOHW_PARTICIPANTS> SelectParticipantsSourceList(MohwConditionDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[1] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[2] = new SqlParameter("@USER_ID", "");

                    var result = _context.Database.SqlQuery<DTO_MOHW_PARTICIPANTS>(ReportContext.USP_SELECT_REPORT_PARTICIPANTS_SOURCE_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 학술대회 생성한 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_PARTICIPANTS> SelectParticipantsReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_PARTICIPANTS>(ReportContext.USP_SELECT_REPORT_MOHW_PARTICIPANTS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }



        public List<DTO_MOHW_PARTICIPANTS> SelectParticipantsMohwReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_PARTICIPANTS>(ReportContext.USP_SELECT_REPORT_MOHW_PARTICIPANTS_XLS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 학술대회 리포트 생성
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void InsertParticipants(DTO_MOHW_CONDITIONS dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@MOHW_IDX", dto.IDX);
                    parameters[1] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[2] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[3] = new SqlParameter("@USER_ID", dto.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_INSERT_REPORT_PARTICIPANTS, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 시품후 조사 생성한 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_MARKET_RESEARCH> SelectMarketResearchReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_MARKET_RESEARCH>(ReportContext.USP_SELECT_REPORT_MOHW_MARKET_RESEARCH, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 시행 후 조사 리포트 생성
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void InsertMarketResearch(DTO_MOHW_CONDITIONS dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@MOHW_IDX", dto.IDX);
                    parameters[1] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[2] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[3] = new SqlParameter("@USER_ID", dto.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_INSERT_REPORT_MARKET_RESEARCH, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        #region 복수 요양 기관



        /// <summary>
        ///  복수 요양 기관 List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<DTO_MOHW_PLURALITY_MEDICAL> SelectPluralityMedicalSourceList(MohwConditionDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[9];
                    parameters[0] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[1] = new SqlParameter("@END_DATE", dto.END_DATE);

                    parameters[2] = new SqlParameter("@BELOW_ONE_WON_YN", dto.BELOW_ONE_WON_YN);
                    parameters[3] = new SqlParameter("@EXCEPT_IECTURER_FOOD_COST_YN", dto.EXCEPT_IECTURER_FOOD_COST_YN);
                    parameters[4] = new SqlParameter("@ONLY_ATTEND_YN", dto.ONLY_ATTEND_YN);
                    parameters[5] = new SqlParameter("@ONLY_MEDICINE_YN", dto.ONLY_MEDICINE_YN);
                    parameters[6] = new SqlParameter("@ONLY_MEDICAL_EQUIPMENT_YN", dto.ONLY_MEDICAL_EQUIPMENT_YN);
                    parameters[7] = new SqlParameter("@EXCEPT_BAYER_EMPLOYEEE_YN", dto.EXCEPT_BAYER_EMPLOYEEE_YN);

                    parameters[8] = new SqlParameter("@USER_ID", "");

                    var result = _context.Database.SqlQuery<DTO_MOHW_PLURALITY_MEDICAL>(ReportContext.USP_SELECT_REPORT_PLURALITY_MEDICAL_SOURCE_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 복수 요양 기관 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_PLURALITY_MEDICAL_REPORT> SelectPluralityMedicalReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_PLURALITY_MEDICAL_REPORT>(ReportContext.USP_SELECT_REPORT_MOHW_PLURALITY_MEDICAL, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 복수 요양기관 리포트 생성
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void InsertPluralityMedical(DTO_MOHW_CONDITIONS dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[10];
                    parameters[0] = new SqlParameter("@MOHW_IDX", dto.IDX);
                    parameters[1] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[2] = new SqlParameter("@END_DATE", dto.END_DATE);

                    parameters[3] = new SqlParameter("@BELOW_ONE_WON_YN", dto.BELOW_ONE_WON_YN);
                    parameters[4] = new SqlParameter("@EXCEPT_IECTURER_FOOD_COST_YN", dto.EXCEPT_IECTURER_FOOD_COST_YN);
                    parameters[5] = new SqlParameter("@ONLY_ATTEND_YN", dto.ONLY_ATTEND_YN);
                    parameters[6] = new SqlParameter("@ONLY_MEDICINE_YN", dto.ONLY_MEDICINE_YN);
                    parameters[7] = new SqlParameter("@ONLY_MEDICAL_EQUIPMENT_YN", dto.ONLY_MEDICAL_EQUIPMENT_YN);
                    parameters[8] = new SqlParameter("@EXCEPT_BAYER_EMPLOYEEE_YN", dto.EXCEPT_BAYER_EMPLOYEEE_YN);

                    parameters[9] = new SqlParameter("@USER_ID", dto.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_INSERT_REPORT_PLURALITY_MEDICAL, parameters);
                }
            }
            catch
            {
                throw;
            }
        }


        #endregion

        #region [ Individual Medical (개별 요양기관) ]

        public List<DTO_MOHW_DIV_MEDICAL_SRC> SelectIndividualMedicalSourceList(MohwConditionDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[8];
                    parameters[0] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[1] = new SqlParameter("@END_DATE", dto.END_DATE);

                    parameters[2] = new SqlParameter("@BELOW_ONE_WON_YN", dto.BELOW_ONE_WON_YN);
                    parameters[3] = new SqlParameter("@EXCEPT_IECTURER_FOOD_COST_YN", dto.EXCEPT_IECTURER_FOOD_COST_YN);
                    parameters[4] = new SqlParameter("@ONLY_ATTEND_YN", dto.ONLY_ATTEND_YN);
                    parameters[5] = new SqlParameter("@ONLY_MEDICINE_YN", dto.ONLY_MEDICINE_YN);
                    parameters[6] = new SqlParameter("@ONLY_MEDICAL_EQUIPMENT_YN", dto.ONLY_MEDICAL_EQUIPMENT_YN);
                    parameters[7] = new SqlParameter("@EXCEPT_BAYER_EMPLOYEEE_YN", dto.EXCEPT_BAYER_EMPLOYEEE_YN);

                    var result = _context.Database.SqlQuery<DTO_MOHW_DIV_MEDICAL_SRC>(ReportContext.USP_SELECT_REPORT_DIV_MEDICAL_SOURCE_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void InsertIndividualMedical(DTO_MOHW_CONDITIONS dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[9];
                    parameters[0] = new SqlParameter("@MOHW_IDX", dto.IDX);
                    parameters[1] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[2] = new SqlParameter("@END_DATE", dto.END_DATE);

                    parameters[3] = new SqlParameter("@BELOW_ONE_WON_YN", dto.BELOW_ONE_WON_YN);
                    parameters[4] = new SqlParameter("@EXCEPT_IECTURER_FOOD_COST_YN", dto.EXCEPT_IECTURER_FOOD_COST_YN);
                    parameters[5] = new SqlParameter("@ONLY_ATTEND_YN", dto.ONLY_ATTEND_YN);
                    parameters[6] = new SqlParameter("@ONLY_MEDICINE_YN", dto.ONLY_MEDICINE_YN);
                    parameters[7] = new SqlParameter("@ONLY_MEDICAL_EQUIPMENT_YN", dto.ONLY_MEDICAL_EQUIPMENT_YN);
                    parameters[8] = new SqlParameter("@EXCEPT_BAYER_EMPLOYEEE_YN", dto.EXCEPT_BAYER_EMPLOYEEE_YN);


                    _context.Database.ExecuteSqlCommand(ReportContext.USP_INSERT_REPORT_DIV_MEDICAL, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 개별요양기관 Source 데이터 엑셀 생성용
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_DIV_MEDICAL_SRC> SelectIndividualMedicalReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_DIV_MEDICAL_SRC>(ReportContext.USP_SELECT_REPORT_MOHW_DIV_MEDICAL_SRC, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MOHW_DIV_MEDICAL> SelectMOHWIndividualMedical(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_DIV_MEDICAL>(ReportContext.USP_SELECT_REPORT_MOHW_DIV_MEDICAL, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region 견본품(Sample)

        /// <summary>
        /// 견본품(Sample) 리포트 생성
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void InsertMohwSample(DTO_MOHW_CONDITIONS dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@MOHW_IDX", dto.IDX);
                    parameters[1] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[2] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[3] = new SqlParameter("@USER_ID", dto.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_INSERT_REPORT_MOHW_FREE_GOODS, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  견본품(Sample) Source List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<DTO_MOHW_FREE_GOODS> SelectSampleSourceList(MohwConditionDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[1] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[2] = new SqlParameter("@USER_ID", "");

                    var result = _context.Database.SqlQuery<DTO_MOHW_FREE_GOODS>(ReportContext.USP_SELECT_REPORT_FREE_GOODS_SOURCE_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 견본품(Sample) 생성한 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_FREE_GOODS> SelectSampleReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_FREE_GOODS>(ReportContext.USP_SELECT_REPORT_MOHW_FREE_GOODS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 견본품(Sample) 생성한 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_FREE_GOODS_REPORT> SelectSampleMohwReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_FREE_GOODS_REPORT>(ReportContext.USP_SELECT_REPORT_MOHW_FREE_GOODS_XLS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region 구매전 의료기기

        /// <summary>
        /// 구매전 의료기기 리포트 생성
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void InsertMohwSampleDevice (DTO_MOHW_CONDITIONS dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@MOHW_IDX", dto.IDX);
                    parameters[1] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[2] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[3] = new SqlParameter("@USER_ID", dto.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_INSERT_REPORT_MOHW_FREE_GOODS_DEVICE, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  구매전 의료기기 Source List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<DTO_MOHW_FREE_GOODS_DEVICE> SelectSampleDeviceSourceList(MohwConditionDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[1] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[2] = new SqlParameter("@USER_ID", "");

                    var result = _context.Database.SqlQuery<DTO_MOHW_FREE_GOODS_DEVICE>(ReportContext.USP_SELECT_REPORT_FREE_GOODS_DEVICE_SOURCE_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 구매전 의료기기 생성한 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_FREE_GOODS_DEVICE> SelectSampleDeviceReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_FREE_GOODS_DEVICE>(ReportContext.USP_SELECT_REPORT_MOHW_FREE_GOODS_DEVICE, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 구매전 의료기기 생성한 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_FREE_GOODS_DEVICE_REPORT> SelectSampleDeviceMohwReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_FREE_GOODS_DEVICE_REPORT>(ReportContext.USP_SELECT_REPORT_MOHW_FREE_GOODS_DEVICE_XLS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 임상시험지원

        /// <summary>
        /// 임상시험지원 리포트 생성
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void InsertMohwMedicalStudy(DTO_MOHW_CONDITIONS dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@MOHW_IDX", dto.IDX);
                    parameters[1] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[2] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[3] = new SqlParameter("@USER_ID", dto.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_INSERT_REPORT_MOHW_MEDICAL_STUDY, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  임상시험지원 Source List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<DTO_MOHW_MEDICAL_STUDY> SelectMedicalStudySourceList(MohwConditionDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[1] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[2] = new SqlParameter("@USER_ID", "");

                    var result = _context.Database.SqlQuery<DTO_MOHW_MEDICAL_STUDY>(ReportContext.USP_SELECT_REPORT_MOHW_MEDICAL_STUDY_SOURCE_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  임상시험지원 생성한 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_MEDICAL_STUDY> SelectMedicalStudyReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_MEDICAL_STUDY>(ReportContext.USP_SELECT_REPORT_MOHW_MEDICAL_STUDY, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  임상시험지원 생성한 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_MEDICAL_STUDY_REPORT> SelectMedicalStudyMohwReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_MEDICAL_STUDY_REPORT>(ReportContext.USP_SELECT_REPORT_MOHW_MEDICAL_STUDY_XLS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region KRPIA 보고

        /// <summary>
        /// KRPIA 보고 리포트 생성
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public void InsertMohwKRPIA(DTO_MOHW_CONDITIONS dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@MOHW_IDX", dto.IDX);
                    parameters[1] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[2] = new SqlParameter("@END_DATE", dto.END_DATE);
                    parameters[3] = new SqlParameter("@TYPE", dto.KRPIA_TYPE);
                    parameters[4] = new SqlParameter("@USER_ID", dto.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_INSERT_REPORT_MOHW_KRPIA, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  KRPIA 보고 Source List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<DTO_MOHW_KRPIA> SelectKRPIASourceList(MohwConditionDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@START_DATE", dto.START_DATE);
                    parameters[1] = new SqlParameter("@END_DATE", dto.END_DATE);
                    //parameters[2] = new SqlParameter("@TYPE", dto.MOHW_TYPE);
                    parameters[2] = new SqlParameter("@TYPE", "ALL");
                    parameters[3] = new SqlParameter("@USER_ID", "SGWVX");

                    
                    var result = _context.Database.SqlQuery<DTO_MOHW_KRPIA>(ReportContext.USP_SELECT_REPORT_MOHW_KRPIA_SOURCE_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  KRPIA 보고 생성한 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_KRPIA> SelectKRPIAReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_KRPIA>(ReportContext.USP_SELECT_REPORT_MOHW_KRPIA, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        ///  KRPIA 보고 생성한 리포트를 조회(Excel 생성하기 위해 Report 목록 조회)
        /// </summary>
        /// <param name="idx"></param>
        /// <returns></returns>
        public List<DTO_MOHW_KRPIA_REPORT> SelectKRPIAMohwReportList(string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_MOHW_KRPIA_REPORT>(ReportContext.USP_SELECT_REPORT_MOHW_KRPIA_XLS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

    }
}
