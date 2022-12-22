using System;
using System.Collections.Generic;
using System.Text;

namespace Resource
{
    public class clsMail
    {
        private string _vSMTPServer;
        public string vSMTPServer
        {
            get { return _vSMTPServer; }
            set { _vSMTPServer = value; }
        }

        private int _iSMTPPort;
        public int iSMTPPort
        {
            get { return _iSMTPPort; }
            set { _iSMTPPort = value; }
        }

        private string _vSMTPUserName;
        public string vSMTPUserName
        {
            get { return _vSMTPUserName; }
            set { _vSMTPUserName = value; }
        }

        private string _vSMTPPassword;
        public string vSMTPPassword
        {
            get { return _vSMTPPassword; }
            set { _vSMTPPassword = value; }
        }

        private bool _vDefaultCredentials;
        public bool vDefaultCredentials
        {
            get { return _vDefaultCredentials; }
            set { _vDefaultCredentials = value; }
        }

        private string _vEmailFrom;
        public string vEmailFrom
        {
            get { return _vEmailFrom; }
            set { _vEmailFrom = value; }
        }

        private List<string> _vEmailTo = new List<string>();
        public List<string> vEmailTo
        {
            get { return _vEmailTo; }
            set { _vEmailTo = value; }
        }

        private List<string> _vEmailCC = new List<string>();
        public List<string> vEmailCC
        {
            get { return _vEmailCC; }
            set { _vEmailCC = value; }
        }

        private List<string> _vEmailCCo = new List<string>();
        public List<string> vEmailCCo
        {
            get { return _vEmailCCo; }
            set { _vEmailCCo = value; }
        }

        private string _vEmailSubject;
        public string vEmailSubject
        {
            get { return _vEmailSubject; }
            set { _vEmailSubject = value; }
        }

        private string _vEmailBody;
        public string vEmailBody
        {
            get { return _vEmailBody; }
            set { _vEmailBody = value; }
        }

        private bool _bEnabledSSL;
        public bool bEnabledSSL
        {
            get { return _bEnabledSSL; }
            set { _bEnabledSSL = value; }
        }

        private bool _bIsBodyHtml;
        public bool bIsBodyHtml
        {
            get { return _bIsBodyHtml; }
            set { _bIsBodyHtml = value; }
        }

        private List<string> _oAttachments = new List<string>();
        public List<string> oAttachments
        {
            get { return _oAttachments; }
            set { _oAttachments = value; }
        }

        public clsMail()
        {

        }
    }
}
