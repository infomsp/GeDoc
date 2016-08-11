using System;
using System.Net;
using System.Security.Principal;
using System.Text;

using Microsoft.Reporting.WebForms;

namespace ReportingCredentials
{
    public class ReportServerCredentials : IReportServerCredentials
    {
        private string reportServerUserName;
        private string reportServerPassword;
        private string reportServerDomain;

        public ReportServerCredentials(string userName, string password, string domain)
        {
            reportServerUserName = userName;
            reportServerPassword = password;
            reportServerDomain = domain;
        }

        public WindowsIdentity ImpersonationUser
        {
            get
            {
                // Use default identity.
                return null;
            }
        }

        public ICredentials NetworkCredentials
        {
            get
            {
                // Use default identity.
                return new NetworkCredential(reportServerUserName, reportServerPassword, reportServerDomain);
            }
        }

        public void New(string userName, string password, string domain)
        {
            reportServerUserName = userName;
            reportServerPassword = password;
            reportServerDomain = domain;
        }

        public bool GetFormsCredentials(out Cookie authCookie, out string user, out string password, out string authority)
        {
            // Do not use forms credentials to authenticate.
            authCookie = null;
            user = null;
            password = null;
            authority = null;

            return false;
        }
    }
}