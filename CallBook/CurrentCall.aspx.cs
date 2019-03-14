using CallBook.Services;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace CallBook
{
    public partial class CurrentCall : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            MyModel context = new MyModel();
            string caller = Request.QueryString["Caller"];
            string callID = Request.QueryString["CallID"];

            Label1.Text = "Caller: " + caller;

            IQueryable<T_EVENT> events = T_EVENTService.GetEventsByCallId(context, int.Parse(callID)).OrderBy(item => item.T_CALL.CALLER);

            GridView1.DataSource = events.ToList();
            GridView1.DataBind();
        }
    }
}