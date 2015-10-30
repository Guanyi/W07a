using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OdataClient
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DisplayPerson();
        }

        void DisplayPerson()
        {
            // TODO: Replace with your local URI.
            string serviceUri = "http://localhost:53569/odata/";
            var container = new Default.Container(new Uri(serviceUri));

            GridView1.DataSource = container.Person;
            GridView1.DataBind();

        }
    }
}