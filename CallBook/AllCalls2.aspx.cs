using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using CallBook.Services;

namespace CallBook
{
    public partial class AllCalls2 : System.Web.UI.Page
    {
        private DataTable GetData()
        {
            string caller = Request.QueryString["Caller"];
            //caller = "327657";
            Label1.Visible = true;
            Label1.Text = "Caller number:" + caller;

            MyModel context = new MyModel();
            var events = context.T_EVENT.ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("StartCall", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Talk Duration", typeof(int)));
            dt.Columns.Add(new DataColumn("Receiver", typeof(int)));
            dt.Columns.Add(new DataColumn("Type", typeof(string)));

            IQueryable<int> callers = T_EVENTService.GetCallerCallID(context, caller).OrderByDescending(n => n); ;
            IQueryable<T_EVENT> callerCalls = T_EVENTService.GetAllCallerCalls(context, caller);

            if (string.IsNullOrEmpty(callers.First().ToString()))
            {
                return dt;
            }

            int pageIndex = GridView1.PageIndex;
            int pageSize = GridView1.PageSize;

            int start = pageIndex * pageSize;
            int end = (pageIndex + 1) * pageSize;
            if (end > callers.Count())
            {
                end = callers.Count();
            }

            for (int i = start; i < end; i++)
            {
                int callID = callers.Skip(i).First();
                DateTime callStartTime = T_EVENTService.ParticipantsByCallID(callerCalls, callID, "pick").Select(startTime => startTime.RECORD_DATE).FirstOrDefault();
                DateTime callEndTime = T_EVENTService.ParticipantsByCallID(callerCalls, callID).Select(endTime => endTime.RECORD_DATE).ToList().LastOrDefault();
                TimeSpan durationTime = callEndTime.Subtract(callStartTime);
                IQueryable<int> receiverQuery = T_CALLService.ReceiverByCallID(context, callID).Select(receiverNumber => (receiverNumber.RECIEVER));
                int? receiver = receiverQuery.Cast<int?>().FirstOrDefault();
                string eventName = T_EVENTService.ParticipantsByCallID(callerCalls, callID).Select(endTime => endTime.RECORD_EVENT_ID).ToList().LastOrDefault();

                dt.Rows.Add(callStartTime, durationTime.TotalMinutes, receiver, eventName);
            }

            GridView1.VirtualItemCount = callers.Count();
            return dt;

        }

        private void BindData()
        {
            DataTable dt = GetData();
            DataView view = dt.DefaultView;
            view.Sort = "StartCall desc, Talk Duration asc";
            Session["AllCallsTable"] = dt;

            GridView1.DataSource = Session["AllCallsTable"]; ;
            GridView1.DataBind();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "CALLER#" + Request.QueryString["Caller"];

            if (!IsPostBack)
            {
                BindData();
            }
        }


        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dtSort = Session["AllCallsTable"] as DataTable;

            if (dtSort != null)
            {
                //Sort the data.
                dtSort.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                GridView1.DataSource = Session["AllCallsTable"];
                GridView1.DataBind();
            }

        }

        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindData();

        }
    }
}