using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    public class DTO_USER_CONFIG_MENU_SORT
    {
        /// <summary>
        /// 사용자 아이디
        /// </summary> 
        public string USER_ID { get; set; }

        /// <summary>
        /// 이벤트 아이디
        /// </summary> 
        public string EVENT_ID { get; set; }

        /// <summary>
        /// 이벤트 명
        /// </summary>
        public string EVENT_NAME { get; set; }

        /// <summary>
        /// 정렬순서
        /// </summary> 
        public int SORT { get; set; }

        /// <summary>
        /// 웹페이지 
        /// </summary>
        public string WEB_PAGE_NAME { get; set; }

        /// <summary>
        /// 이벤트 설명
        /// </summary>
        public string EVENT_DESC { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary> 
        public DateTime CREATE_DATE { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        public DateTime? UPDATE_DATE { get; set; }

    }
}
