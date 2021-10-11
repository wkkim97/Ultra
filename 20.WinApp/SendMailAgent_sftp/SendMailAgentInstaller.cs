using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;


namespace DNSoft.eW.eWM.BSL.Mail.WithdrawAgent
{
	[RunInstaller(true)]
	public class WithdrawMailAgentInstaller : Installer
	{
		public WithdrawMailAgentInstaller()
		{
			System.ServiceProcess.ServiceProcessInstaller oProcInstaller = new System.ServiceProcess.ServiceProcessInstaller();
			System.ServiceProcess.ServiceInstaller oServiceInstaller = new System.ServiceProcess.ServiceInstaller();

            oProcInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
			oServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Manual;

			oServiceInstaller.ServiceName = "Bayer.Ultra.Agent.SendMailAgent";
			oServiceInstaller.DisplayName = "Bayer.Ultra.Agent.SendMailAgent";
			oServiceInstaller.Description = "메일 발송를 담당하는 서비스";

			this.Installers.Add(oProcInstaller);
			this.Installers.Add(oServiceInstaller);
		}
	}
}
