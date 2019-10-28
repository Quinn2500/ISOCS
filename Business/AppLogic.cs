using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using DataModels;
using DAL;

namespace Business
{
    public class AppLogic
    {
        DataBaseCallsApp dataBaseCallsApp = new DataBaseCallsApp();

        public List<string> GetAllCertificateNamesFromCompany(string companyname)
        {
            List<string> result = new List<string>();
            DataTable request = dataBaseCallsApp.GetAllCertificatesFromCompany(companyname);

            foreach (DataRow dr in request.Rows)
            {
                result.Add(dr[1].ToString());
            }

            return result;
        }

        public List<ActionModel> GetAllActionsFromCertificate()
        {
            ActionModel a = new ActionModel()
            {
                Name = "testa",
                Occurence = OccurenceEnum.Daily
            };

            ActionModel b = new ActionModel()
            {
                Name = "testb",
                Occurence = OccurenceEnum.Weekly
            };

            ActionModel c = new ActionModel()
            {
                Name = "testc",
                Occurence = OccurenceEnum.Monthly
            };

            ActionModel d = new ActionModel()
            {
                Name = "testd",
                Occurence = OccurenceEnum.Quarterly
            };

            ActionModel e = new ActionModel()
            {
                Name = "teste",
                Occurence = OccurenceEnum.Yearly
            };

            List<ActionModel> result = new List<ActionModel>();
            result.Add(a);
            result.Add(b);
            result.Add(c);
            result.Add(d);
            result.Add(e);
            return result;
        }
    }
}
