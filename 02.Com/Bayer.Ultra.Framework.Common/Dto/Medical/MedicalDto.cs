using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Medical
{
    [DataContract]
    public class MedicalDto
    {
        /// <summary>
        /// medical study Info
        /// </summary>
        [DataMember]
        public DTO_MEDICAL_INFO medicalInfo { get; set; }


        /// <summary>
        /// product List
        /// </summary>
        [DataMember]
        public List<DTO_MEDICAL_PRODUCTS> products { get; set; }


        /// <summary>
        /// Editor(Reviwer) List
        /// </summary>
        [DataMember]
        public List<DTO_MEDICAL_EDITOR> editors { get; set; }
    }
}
