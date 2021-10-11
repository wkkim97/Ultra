using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Bayer.Ultra.Agent.SendMailAgent
{
	static class Program
	{
		/// <summary>
		/// 해당 응용 프로그램의 주 진입점입니다.
		/// </summary>
		static void Main()
		{

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new SendMailAgent()
            };
            ServiceBase.Run(ServicesToRun);

            //SendMailAgent agent = new SendMailAgent();
            ////agent.ServiseStart();
            //agent.SendConcurTransferMail();
        }
    }
}
