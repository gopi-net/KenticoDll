using Bluespire.Emerge.CommonService;
using Bluespire.Emerge.Components.Donation;
using Bluespire.Emerge.Components.Donation.WebParts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Helpers;
using CMS.Base.Web.UI;

public partial class CMSWebParts_CMS_Donation_DonationForm : DonationFormWebPart
{
    #region Properties
    public string NextPageURL
    {
        get
        {

            return ValidationHelper.GetString(GetValue("NextPageURL"), string.Empty);
        }
        set
        {
            SetValue("NextPageURL", value);
        }
    }

    public override MessagesPlaceHolder MessagesPlaceHolder
    {
        get
        {
            return plcMess;
        }
    }
    #endregion

    protected void Page_Init(object sender, EventArgs e)
    {
        ControlPanel = DonationFormPanel;
    }


    protected void Page_Load(object sender, EventArgs e)
    {

        if (StopProcessing)
        {
            DonationFormPanel.Visible = false;
            return;
        }
        if (!RequestHelper.IsPostBack())
        {

            LoadListControls(false);
            DonationType.SelectedIndex = 0;
            if (DonationType.SelectedItem != null)
                TypeOfDonationForEmail.Text = DonationType.SelectedItem.Text;
            LoadLevels();
            if (HonourType.Items.Count == 0)
                HonourLabel.Visible = false;
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "setTop", "resetDotNetScrollPosition();", true);

        }

        //CODE : Added to reset radio buttons selection on clear button click
        object eTarget = (object)(Request.Params["__EVENTTARGET"]);
        if (eTarget != null && Convert.ToString(eTarget).Contains("ClearButton"))
        {
            if (FindControl("Individual") != null)
                Individual.Checked = true;
            if (FindControl("DonationType") != null && ((RadioButtonList)FindControl("DonationType")).Items.Count > 0)
                DonationType.SelectedIndex = 0;
				LoadLevels();
        }



        ProcessButton.Click += ProcessButton_Click;
        DonationType.SelectedIndexChanged += DonationType_SelectedIndexChanged;
    }

    void DonationType_SelectedIndexChanged(object sender, EventArgs e)
    {
        TypeOfDonationForEmail.Text = DonationType.SelectedItem.Text;
        LoadLevels();

    }

    void ProcessButton_Click(object sender, EventArgs e)
    {
        try
        {
            //TODO: Payment Gateway
            setData();
            URLHelper.Redirect(this.NextPageURL);
        }
        catch (Exception ex)
        {
            OnError(ex);
        }

    }

    private void LoadLevels()
    {
        DataSet ds = GetDataForAmountLevels(DonationType.SelectedValue);
        LevelsList.DataSource = ds;
        LevelsList.DataBind();
    }


}