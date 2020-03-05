using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace BMSCFinancedEstate.WPEstatePresenter
{
    public partial class WPEstatePresenterUserControl : UserControl
    {
        const string JPG = ".jpg";
        const string GIF = ".gif";
        const string DEFAULT_IMAGE = "000.png";

        List<FinancedEstate> financedEstateList;
        List<FinancedEstateImage> financedEstateImageList;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            try
            {
                financedEstateList = SharePointConnector.GetFinancedEstate();
                financedEstateImageList = SharePointConnector.GetFinancedEstateImages();

                this.LoadFilterControls();
            }
            catch (Exception ex)
            {
                LiteralControl errorMessage = new LiteralControl();
                errorMessage.Text = ex.Message;

                this.Controls.Clear();
                this.Controls.Add(errorMessage);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ltrEstate.Text = this.LoadImportantEstate();
            }
            catch (Exception ex)
            {
                LiteralControl errorMessage = new LiteralControl();
                errorMessage.Text = ex.Message;

                this.Controls.Clear();
                this.Controls.Add(errorMessage);
            }
        }

        protected void rbl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblHeader.Text = "";
                ltrEstate.Text =
                    this.LoadFilteredEstate(ddlCity.SelectedValue, ddlType.SelectedValue, rblPrice.SelectedValue);
            }
            catch (Exception ex)
            {
                LiteralControl errorMessage = new LiteralControl();
                errorMessage.Text = ex.Message;

                this.Controls.Clear();
                this.Controls.Add(errorMessage);
            }
        }

        private string LoadFilteredEstate(string city, string type, string price)
        {
            string formatedResult = "";

            if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(type) && string.IsNullOrEmpty(price))
            {
                formatedResult = this.GetTheFormatedString(financedEstateList);
            }
            else
            { 
                List<FinancedEstate> filteredEstate =
                    financedEstateList.FindAll(fe => FindFilteredEstate(fe, city, type, price));
                formatedResult = this.GetTheFormatedString(filteredEstate);
            }

            return formatedResult;
        }

        private string LoadImportantEstate()
        {
            List<FinancedEstate> importantEstate =
                financedEstateList.FindAll(FindImportantEstate);

            string formatedResult = this.GetTheFormatedString(importantEstate);

            return formatedResult;
        }

        private void LoadFilterControls()
        {
            ddlCity.DataSource = this.GetEstateCities();
            ddlType.DataSource = this.GetEstateTypes();
            //rblPrice.DataSource = this.GetEstatePrices();
            //rblPrice.DataTextFormatString = "{0:0,0.00}";

            ddlCity.DataBind();
            ddlType.DataBind();
            //rblPrice.DataBind();

            ddlCity.Items.Add(new ListItem("TODAS LAS CIUDADES", string.Empty));
            ddlType.Items.Add(new ListItem("TODOS LOS TIPOS", string.Empty));
            rblPrice.Items.Add(new ListItem("TODOS LOS PRECIOS", string.Empty));
            rblPrice.Items.Add(new ListItem("SIN PRECIO ASIGNADO", "0"));
            rblPrice.Items.Add(new ListItem("MENOR A 100.000,00", "100000"));
            rblPrice.Items.Add(new ListItem("MAYOR A 100.000,00", "100001"));
        }

        private static bool FindPropertyImages(FinancedEstateImage property, string code)
        {
            string imageName = property.Name.Remove(property.Name.IndexOf('.'));
            //string imageName = property.Name;

            if (imageName.Equals(code.Trim(), StringComparison.CurrentCultureIgnoreCase))
                return true;

            return false;
        }

        private bool FindFilteredEstate(FinancedEstate property, string city, string type, string price)
        {
            if (!string.IsNullOrEmpty(city) && string.IsNullOrEmpty(type) && string.IsNullOrEmpty(price))
            {
                if (property.City.Equals(city))
                    return true;
            }
            if (string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(type) && string.IsNullOrEmpty(price))
            {
                if (property.Type.Equals(type))
                    return true;
            }
            if (string.IsNullOrEmpty(city) && string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(price))
            {
                switch (price)
                {
                    case "100000":
                        if (property.Price <= double.Parse(price) && property.Price > 0)
                            return true;
                        break;
                    case "100001":
                        if (property.Price >= double.Parse(price) && property.Price > 0)
                            return true;
                        break;
                    default:
                        if (property.Price == 0)
                            return true;
                        break;
                }
            }
            if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(type) && string.IsNullOrEmpty(price))
            {
                if (property.City.Equals(city) && property.Type.Equals(type))
                    return true;
            }
            if (!string.IsNullOrEmpty(city) && string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(price))
            {
                if (property.City.Equals(city))
                {
                    switch (price)
                    {
                        case "100000":
                            if (property.Price <= double.Parse(price) && property.Price > 0)
                                return true;
                            break;
                        case "100001":
                            if (property.Price >= double.Parse(price) && property.Price > 0)
                                return true;
                            break;
                        default:
                            if (property.Price == 0)
                                return true;
                            break;
                    }
                }
            }
            if (string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(price))
            {
                if (property.Type.Equals(type))
                {
                    switch (price)
                    {
                        case "100000":
                            if (property.Price <= double.Parse(price) && property.Price > 0)
                                return true;
                            break;
                        case "100001":
                            if (property.Price >= double.Parse(price) && property.Price > 0)
                                return true;
                            break;
                        default:
                            if (property.Price == 0)
                                return true;
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(price))
            {
                if (property.City.Equals(city) && property.Type.Equals(type))
                {
                    switch (price)
                    {
                        case "100000":
                            if (property.Price <= double.Parse(price) && property.Price > 0)
                                return true;
                            break;
                        case "100001":
                            if (property.Price >= double.Parse(price) && property.Price > 0)
                                return true;
                            break;
                        default:
                            if (property.Price == 0)
                                return true;
                            break;
                    }
                }
            }

            return false;
        }

        private static bool FindImportantEstate(FinancedEstate property)
        {
            return property.Important;
        }

        private string GetTheFormatedString(List<FinancedEstate> filteredEstate)
        {
            string formatedResult = "";

            foreach (FinancedEstate property in filteredEstate)
            {
                List<FinancedEstateImage> propertyImages =
                    financedEstateImageList.FindAll(fei => FindPropertyImages(fei, property.Code));
                FinancedEstateImage jpgImage =
                    propertyImages.Find(jpg => jpg.Name.EndsWith(JPG, StringComparison.CurrentCultureIgnoreCase));

                string jpgImageUrl = financedEstateImageList.Find(fei => fei.Name.Equals(DEFAULT_IMAGE)).Url;
                if (jpgImage != null)
                    jpgImageUrl = jpgImage.Url;

                formatedResult += string.Format(
                    "<div class='col4'>" +
                    "<a href='/Paginas/inmueblesfinanciadosdetalle.aspx?AssetId={0}&AssetCode={1}' target='_blank'>" +
                    "<img src='{2}' alt='{3}' /><p>{3}</p></a>" +
                    "</div>",
                    property.Id, property.Code, jpgImageUrl, property.Name);
            }

            return formatedResult;
        }

        private List<double> GetEstatePrices()
        {
            List<double> thePrices = new List<double>();

            foreach (FinancedEstate financedEstate in financedEstateList)
            {
                if (!thePrices.Contains(financedEstate.Price))
                {
                    thePrices.Add(financedEstate.Price);
                }
            }

            thePrices.Sort();

            return thePrices;
        }

        private List<string> GetEstateTypes()
        {
            List<string> theTypes = new List<string>();

            foreach (FinancedEstate financedEstate in financedEstateList)
            {
                if (!theTypes.Contains(financedEstate.Type))
                {
                    theTypes.Add(financedEstate.Type);
                }
            }

            theTypes.Sort();

            return theTypes;
        }

        private List<string> GetEstateCities()
        {
            List<string> theCities = new List<string>();

            foreach (FinancedEstate financedEstate in financedEstateList)
            {
                if (!theCities.Contains(financedEstate.City))
                {
                    theCities.Add(financedEstate.City);
                }
            }

            theCities.Sort();

            return theCities;
        }
    }
}
