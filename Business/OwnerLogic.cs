using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataModels;
using DAL;

namespace Business
{
    public class OwnerLogic
    {
        private readonly AppLogic _appLogic = new AppLogic();
        private readonly DataBaseCallsOwner _dataBaseCalls = new DataBaseCallsOwner();

        public void AddCompany(string name)
        {
            string inviteCode = Guid.NewGuid().ToString();
            _dataBaseCalls.AddCompany(name, inviteCode);
        }

        public void DeleteCompany(string name)
        {
            foreach (string certificateName in _appLogic.GetAllCertificateNamesFromCompany(name))
            {
                _appLogic.DeleteCertificate(certificateName, name);
            }
            _dataBaseCalls.DeleteCompany(name);
        }

        public List<CompanyModel> GetAllCompanyModels()
        {
            List<CompanyModel> result = new List<CompanyModel>();
            foreach (DataRow row in _dataBaseCalls.GetAllCompanies().Rows)
            {
                CompanyModel company = new CompanyModel
                {
                    Name = row[0].ToString(),
                    InviteCode = row[1].ToString()
                };
                result.Add(company);
            }
            return result;
        }
    }
}
