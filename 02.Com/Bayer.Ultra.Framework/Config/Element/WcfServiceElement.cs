using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config.Element
{
    /// <summary>
    /// 웹서비스 관련 설정 정보제공
    /// </summary>
    public class WcfServiceElement : ConfigurationElement
    {
        /// <summary>
        /// 인증관련 서비스 정보
        /// </summary>
        [ConfigurationProperty("commonService", IsRequired = true)]        
        public CommonServiceElement CommonService
        {
            get
            {
                return (CommonServiceElement)this["commonService"];
            }
        }

        /// <summary>
        /// 이벤트 처리 서비스 정보
        /// </summary>
        [ConfigurationProperty("eventService", IsRequired = true)]
        public EventServiceElement EventService
        {
            get
            {
                return (EventServiceElement)this["eventService"];
            }
        }

        /// <summary>
        /// Medical 처리 서비스
        /// </summary>
        [ConfigurationProperty("medicalService", IsRequired = true)]
        public MedicalServiceElement MedicalService
        {
            get
            {
                return (MedicalServiceElement)this["medicalService"];
            }
        }

        [ConfigurationProperty("reportService", IsRequired = true)]
        public ReportServiceElement ReportService
        {
            get
            {
                return (ReportServiceElement)this["reportService"];
            }
        }

        /// <summary>
        /// Radiology 처리 서비스
        /// </summary>
        [ConfigurationProperty("radiologyService", IsRequired = true)]
        public RadiologyServiceElement RadiologyService
        {
            get
            {
                return (RadiologyServiceElement)this["radiologyService"];
            }
        }
    }

    public class CommonServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
        } 
    }

    public class EventServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
        }
    }

    public class MedicalServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
        }
    }

    public class ReportServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
        }
    }

    public class RadiologyServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
        }
    }
}
