using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CallBook
{
    public partial class AllCalls2 : System.Web.UI.Page
    {
        DataTable dt = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            string caller = Request.QueryString["Caller"];// Convert.ToInt32(Request.QueryString["Caller"]);
            //caller = "346644";
            Label1.Visible = true;
            Label1.Text = "Caller number:" + caller;
            

            MyModel context = new MyModel();
            var events = context.T_EVENT.ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("Start Call", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Talk Duration", typeof(int)));
            dt.Columns.Add(new DataColumn("Receiver", typeof(int)));
            dt.Columns.Add(new DataColumn("Type", typeof(string)));

            //303634
            List<int> allCallIDs = context.T_CALL.Where(record => record.CALLER.ToString().Equals(caller)).Select(callNumber => callNumber.RECORD_ID).ToList<int>();
            var test = context.T_CALL.FirstOrDefault();
            
            List<T_EVENT> allCallEvents = new List<T_EVENT>();

            foreach (int callID in allCallIDs )
            {
                allCallEvents.AddRange(context.T_EVENT.Where(tevent => tevent.CALL_ID == callID));
            }

            foreach (int callID in allCallIDs)
            {

                DateTime callStartTime = allCallEvents.Where(recStart => recStart.CALL_ID.Equals(callID) && recStart.RECORD_EVENT_ID.ToLower().Contains("pick")).Select(startTime => startTime.RECORD_DATE).FirstOrDefault();
                DateTime callEndTime = allCallEvents.Where(recStart => recStart.CALL_ID.Equals(callID)).Select(endTime => endTime.RECORD_DATE).LastOrDefault();
                TimeSpan durationTime = callEndTime.Subtract(callStartTime);
                int receiver = context.T_CALL.Where(record => record.RECORD_ID.Equals(callID)).Select(receiverNumber => receiverNumber.RECIEVER).FirstOrDefault();
                string eventName = allCallEvents.Where(recStart => recStart.CALL_ID.Equals(callID)).Select(endTime => endTime.RECORD_EVENT_ID).LastOrDefault();

                dt.Rows.Add(callStartTime, durationTime.TotalSeconds, receiver, eventName);

            }





            //var allNumberCalls = context.T_EVENT.Where(callnumber => callnumber.CALL_ID.Equals(149)).ToList();



            //foreach (var item in allNumberCalls)
            //{
            //    dt.Rows.Add(item.RECORD_DATE.Date,0,0,"");

            //}

            //foreach (var item in allnumbercalls)
            //{


            //}

            //dt=context.T_EVENT.

            Session["AllCallsTable"] = dt;

            GridView1.DataSource = Session["AllCallsTable"]; ;
            GridView1.DataBind();

            //GridView1.DataSource = dt;
            //GridView1.DataBind();



        }


        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {

            //Retrieve the table from the session object.
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




    }
}