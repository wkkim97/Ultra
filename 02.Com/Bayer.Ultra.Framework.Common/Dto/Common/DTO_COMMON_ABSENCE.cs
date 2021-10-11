using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
	[DataContract]
	public class DTO_COMMON_ABSENCE
	{
		/// <summary>
		/// 일련번호
		/// </summary>
		[DataMember]
		public string IDX { get; set; }

		/// <summary>
		/// 사용자 ID
		/// </summary>
		[DataMember]
		public string USER_ID { get; set; }

		/// <summary>
		/// 위임자 ID
		/// </summary>
		[DataMember]
		public string APPROVER_ID { get; set; }

		/// <summary>
		/// 위임자 명
		/// </summary>
		[DataMember]
		public string APPROVER_NAME { get; set; }

		/// <summary>
		/// 위임 기간 시작일
		/// </summary>
		[DataMember]
		public string FROM_DATE { get; set; }

		/// <summary>
		/// 위임 기간 종료일
		/// </summary>
		[DataMember]
		public string TO_DATE { get; set; }

		/// <summary>
		/// 위임 내용
		/// </summary>
		[DataMember]
		public string DESCRIPTION { get; set; }

		/// <summary>
		/// 삭제여부
		/// </summary>
		[DataMember]
		public string IS_DELETED { get; set; }

		/// <summary>
		/// 생성일
		/// </summary>
		[DataMember]
		public DateTime CREATE_DATE { get; set; }

		/// <summary>
		/// 수정일
		/// </summary>
		[DataMember]
		public DateTime? UPDATE_DATE { get; set; }
	}
}
