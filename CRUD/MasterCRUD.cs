using BusinessSuiteByVesune.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessSuiteByVesune.CRUD
{
    public class MasterCRUD
    {
        public List<Master> GetMasterWorkHistoryByUserId(int userId)
        {
            List<Master> myMasters = new List<Master>();
            User currentUser = new UserCRUD().Read(userId);
            List<Job> jobs = new JobCRUD().GetJobs();

            if (jobs.Count > 0)
            {
                foreach (Job job in jobs)
                {
                    Master master = new Master();
                    master.JobId = job.JobId;
                    master.UserId = userId;
                    master.JobName = job.Name;

                    //get work periods for user and job
                    List<WorkPeriod> workPeriods = new WorkPeriodCRUD().GetWorkPeriodsByJobAndUser(job.JobId, userId);
                    if (workPeriods.Count > 0)
                    {
                        foreach (WorkPeriod period in workPeriods)
                        {
                            if (period.End != null)
                            {
                                TimeSpan span = Convert.ToDateTime(period.End).Subtract(period.Start);
                                master.TotalJobTime += span;
                            }
                        }
                        master.TotalJobTimeReadable = master.TotalJobTime.Hours + " hours and " + master.TotalJobTime.Minutes + " minutes";
                    }

                    // get transactions for user and job
                    // now find transactions that match the job and user
                    List<Transaction> transactions = new TransactionCRUD().GetTransactions().Where(x => x.UserId == userId && x.JobId == job.JobId).ToList();

                    foreach (Transaction transaction in transactions)
                    {
                        master.AmoutPaidForJob += transaction.Amount;
                    }

                    if (currentUser.WorkRate == 0)
                    {
                        master.HoursPaid = 0;
                    }
                    else
                    {
                        master.HoursPaid = master.AmoutPaidForJob / currentUser.WorkRate;
                    }


                    master.HoursOwed = Convert.ToInt32(master.TotalJobTime.Hours) - Convert.ToInt32(master.HoursPaid);

                    //TODO: fix decimal bug

                    master.MoneyPaid = master.HoursPaid * currentUser.WorkRate;
                    master.MoneyOwed = master.HoursOwed * currentUser.WorkRate;

                    //decimal.Round(master.HoursPaid, 2, MidpointRounding.AwayFromZero);
                    //decimal.Round(master.HoursOwed, 2, MidpointRounding.AwayFromZero);
                    //decimal.Round(master.MoneyPaid, 2, MidpointRounding.AwayFromZero);
                    //decimal.Round(master.MoneyOwed, 2, MidpointRounding.AwayFromZero);

                    myMasters.Add(master);
                }
            }
            return myMasters;
        }

        public List<CustomDropdownObject> GetMeridiems()
        {
            List<CustomDropdownObject> dropdownObjects = new List<CustomDropdownObject>()
            {
                new CustomDropdownObject()
                {
                    MemberPath = "AM",
                    ValuePath = "0"
                },
                new CustomDropdownObject()
                {
                    MemberPath = "PM",
                    ValuePath = "1"
                },
            };

            return dropdownObjects;
        }
    }
}
