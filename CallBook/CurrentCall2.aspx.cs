using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace CallBook
{
    public partial class CurrentCall2 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            MyModel context = new MyModel();

            string caller = Request.QueryString["Caller"];
            string callID = Request.QueryString["CallID"]; 
            //context.T_EVENT.Where(item => item.T_CALL.CALLER.ToString().Contains(caller));
            Label1.Text = "Caller: " + caller;

            //DataTable dt = context.T_EVENT.OrderBy(n => n.RECORD_DATE) as DataTable;
            IQueryable<T_EVENT> events = context.T_EVENT;


            events = context.T_EVENT.Where(item => item.T_CALL.RECORD_ID.ToString().Contains(callID))
            .OrderBy(item => item.T_CALL.CALLER);


            GridView1.DataSource = events.ToList();
            GridView1.DataBind();


        }
    }
}