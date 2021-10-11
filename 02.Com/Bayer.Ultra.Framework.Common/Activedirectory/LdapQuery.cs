using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Activedirectory
{
    public class LdapQuery : IDisposable
    {
        private string _ldapPath;

        public LdapQuery()
        {
            _ldapPath = Config.WebSiteConfigHandler.ActiveDirectory.LdapPath;
        }

        public bool IsAuthentication(string account, string password)
        {
            bool authentic = false;

            DirectorySearcher search = null;
            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(_ldapPath, account, password))
                {
                    object nativeObject = entry.NativeObject;
                    search = new DirectorySearcher(entry);
                    search.Filter = "(&(objectClass=*)(sAMAccountName=" + account + "))";
                    search.PropertiesToLoad.Add("cn");
                    SearchResult result = search.FindOne();
                    authentic = true;
                    return authentic;
                }
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public bool IsAuthentication(string account, string password, out List<string> groups)
        {
            bool authentic = false;

            DirectorySearcher search = null;
            try
            {

                using (DirectoryEntry entry = new DirectoryEntry(_ldapPath, account, password))
                {
                    object nativeObject = entry.NativeObject;
                    search = new DirectorySearcher(entry);
                    search.Filter = "(&(objectClass=*)(sAMAccountName=" + account + "))";
                    search.PropertiesToLoad.Add("cn");
                    SearchResult result = search.FindOne();
                    if (null == result)
                    {
                        authentic = false;
                        groups = new List<string>();
                    }
                    else
                    {
                        DirectoryEntry obUser = new DirectoryEntry(result.Path);

                        //groups = GetGroupsOfUser(obUser);
                        groups = GetMemberOf(account);
                        authentic = true;
                    }

                    return authentic;
                }
            }
            catch
            {
                throw;
            }
            finally
            {

            }
        }

        public List<string> GetMemberOf(string userCN)
        {
            try
            {
                List<string> retValue = new List<string>();
                PrincipalContext domainctx = new PrincipalContext(ContextType.Domain, _ldapPath.ToUpper().Replace("LDAP://", string.Empty));
                UserPrincipal userPrincipal = UserPrincipal.FindByIdentity(domainctx, IdentityType.SamAccountName, userCN);

                if (userPrincipal.IsMemberOf(domainctx, IdentityType.Name, "ef.a.kr_localappl_87_lpc_user"))
                    retValue.Add("ef.a.kr_localappl_87_lpc_user");
                if (userPrincipal.IsMemberOf(domainctx, IdentityType.Name, "ef.u.kr_localappl_87_medical_admin"))
                    retValue.Add("ef.u.kr_localappl_87_medical_admin");
                if (userPrincipal.IsMemberOf(domainctx, IdentityType.Name, "ef.u.kr_localappl_87_support_user"))
                    retValue.Add("ef.u.kr_localappl_87_support_user");
                if (userPrincipal.IsMemberOf(domainctx, IdentityType.Name, "ef.u.kr_localappl_87_system_admin"))
                    retValue.Add("ef.u.kr_localappl_87_system_admin");
                if (userPrincipal.IsMemberOf(domainctx, IdentityType.Name, "ef.u.kr_localappl_87_system_designer"))
                    retValue.Add("ef.u.kr_localappl_87_system_designer");
                if (userPrincipal.IsMemberOf(domainctx, IdentityType.Name, "bs.u.0695.bkl-ph-rad"))
                    retValue.Add("bs.u.0695.bkl-ph-rad");
                if (userPrincipal.IsMemberOf(domainctx, IdentityType.Name, "ef.a.kr_localappl_87_rad_user"))
                    retValue.Add("ef.a.kr_localappl_87_rad_user");
                if (userPrincipal.IsMemberOf(domainctx, IdentityType.Name, "ef.u.kr_localappl_87_rad_key_user"))
                    retValue.Add("ef.u.kr_localappl_87_rad_key_user");
                if (userPrincipal.IsMemberOf(domainctx, IdentityType.Name, "ef.u.kr_localappl_87_non_onekey"))
                    retValue.Add("ef.u.kr_localappl_87_non_onekey");
                //  version 1.0.7 HCP validation function for Easy On
                if (userPrincipal.IsMemberOf(domainctx, IdentityType.Name, "ef.u.kr_localappl_87_hcp_search_user"))
                    retValue.Add("ef.u.kr_localappl_87_hcp_search_user");

                return retValue;
            }
            catch(Exception ex)
            {
                LogManager.WriteLog(GetType(), MethodInfo.GetCurrentMethod().Name, new string[] { userCN }, ex.ToString(), "GetMemberOf Error");
                throw;
            }
        }

        public List<string> GetGroupsOfUser(DirectoryEntry oUser)
        {
            List<string> retValue = new List<string>();
            try
            {
                Object oGroups = oUser.Invoke("Groups");

                foreach (Object group in (IEnumerable)oGroups)
                {
                    DirectoryEntry oGroup = new DirectoryEntry(group);
                    if (oGroup.Name.IndexOf("ef.u.kr_localappl_87_lpc_user") >= 0
                        || oGroup.Name.IndexOf("ef.u.kr_localappl_87_medical_admin") >= 0
                        || oGroup.Name.IndexOf("ef.u.kr_localappl_87_support_user") >= 0
                        || oGroup.Name.IndexOf("ef.u.kr_localappl_87_system_admin") >= 0
                        || oGroup.Name.IndexOf("ef.u.kr_localappl_87_system_designer") >= 0
                        || oGroup.Name.IndexOf("bs.u.0695.bkl-ph-rad") >= 0
                        || oGroup.Name.IndexOf("ef.u.kr_localappl_87_rad_key_user") >= 0
                        || oGroup.Name.IndexOf("ef.u.kr_localappl_87_non_onekey") >= 0

                        )
                    {
                        retValue.Add(oGroup.Name.ToString());
                    }
                    // oGroup.Properties["name"].Value;
                    // oGroup.Properties["displayName"].Value;
                    // oGroup.Properties["distinguishedName"].Value;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (oUser != null) oUser.Dispose();
            }
            return retValue;
        }

        public List<string> GetMemberOf(DirectoryEntry oUser, string userName)
        {
            List<string> retValue = new List<string>();
            DirectorySearcher personSearcher = new DirectorySearcher(oUser);
            personSearcher.Filter = string.Format("(SAMAccountName={0}", userName);
            SearchResult result = personSearcher.FindOne();

            if (result != null)
            {
                DirectoryEntry personEntry = result.GetDirectoryEntry();
                PropertyValueCollection groups = personEntry.Properties["memberOf"];
                foreach (string g in groups)
                {
                    retValue.Add(g);
                }
            }
            return retValue;
        }
        public void Dispose()
        {

        }

    }
}
