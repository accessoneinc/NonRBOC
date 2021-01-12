using NonRBOC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NonRBOC.Helper
{
    class GlobalRbocHelper
    {

        public bool IsRbocLastRunDateExpired()
        {
            var expired = false;

            DateTime now = DateTime.Now;

            // GET LERGDATE
            LergDAL lergDal = new LergDAL();
            var lastDate = lergDal.GetLastLergRunDate();

            // GET LAST RBOC RUN DATE
            RbocDAL rbocLastRunDAL = new RbocDAL();
            var lastRbocDate = rbocLastRunDAL.GetLastRbocRunDate();


            // COMPARE - IF Statement - set flag
            if (lastDate.Month != lastRbocDate.Month)
                expired = true; 


            //else return 
            return expired;
        }


    }
}
