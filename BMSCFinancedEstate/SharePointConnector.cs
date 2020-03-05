using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSCFinancedEstate
{
    class SharePointConnector
    {
        #region Global and constant variables
        const string LIST_FINANCED_ESTATE = "Inmuebles Financiados";
        const string LIST_FINANCED_ESTATE_IMAGES = "Inmuebles Financiados (Imágenes)";
        #endregion

        #region Get methods
        internal static FinancedEstate GetFinancedProperty(int id)
        {
            FinancedEstate financedProperty = null;

            using (SPSite sps = new SPSite(SPContext.Current.Web.Url))
            using (SPWeb spw = sps.OpenWeb())
            {
                SPListItem item = spw.Lists[LIST_FINANCED_ESTATE].GetItemById(id);

                if (item != null)
                {
                    financedProperty = new FinancedEstate();

                    financedProperty.Id = item.ID;
                    financedProperty.Name = item["Nombre_x0020_inmueble"].ToString();
                    financedProperty.Code = item.Title;
                    financedProperty.Type = item["Tipo_x0020_bien"].ToString();
                    financedProperty.Price = item["Precio"] == null ? 0.0 : double.Parse(item["Precio"].ToString());
                    financedProperty.Currency = item["Moneda"] == null ? string.Empty : item["Moneda"].ToString();
                    financedProperty.City = item["Ciudad"].ToString();
                    financedProperty.Zone = item["Zona"] == null ? string.Empty : item["Zona"].ToString();
                    financedProperty.Address = item["Direcci_x00f3_n"].ToString();
                    financedProperty.Description = item["Descripcion"].ToString();
                    financedProperty.Important = bool.Parse(item["Destacada"].ToString());
                    financedProperty.Offer = item["Oferta"] == null ? string.Empty : item["Oferta"].ToString();
                    financedProperty.Builtarea = item["Superficie_x0020_construida"] == null ? 0.0 : double.Parse(item["Superficie_x0020_construida"].ToString());
                    financedProperty.Landarea = item["Superficie_x0020_terreno"] == null ? 0.0 : double.Parse(item["Superficie_x0020_terreno"].ToString());
                    financedProperty.C_agency = item["Agencia_x0020_contacto"].ToString();
                    financedProperty.C_name = item["Nombre1"].ToString();
                    financedProperty.C_phone = item["Tel_x00e9_fono"].ToString();
                    financedProperty.C_email = item["Correo_x0020_electr_x00f3_nico"].ToString();
                    financedProperty.Attached = item.Attachments.Count == 0 ? string.Empty : item.Attachments.UrlPrefix + item.Attachments[0];
                    financedProperty.Latlong = item["Latitud_x002f_Longitud"] == null ? string.Empty : item["Latitud_x002f_Longitud"].ToString();
                }
            }

            return financedProperty;
        }

        internal static List<FinancedEstateImage> GetFinancedEstateImages()
        {
            List<FinancedEstateImage> estateImagesList = new List<FinancedEstateImage>();

            #region SharePoint query
            SPListItemCollection queriedEstateImages = null;

            using (SPSite sps = new SPSite(SPContext.Current.Web.Url))
            using (SPWeb spw = sps.OpenWeb())
            {
                SPQuery query = new SPQuery();
                query.Query =
                    "<Where><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>0</Value></Eq></Where>";

                queriedEstateImages = spw.Lists[LIST_FINANCED_ESTATE_IMAGES].GetItems(query);
            }
            #endregion

            foreach (SPListItem estateImage in queriedEstateImages)
            {
                FinancedEstateImage financedEstateImage = new FinancedEstateImage();
                financedEstateImage.Id = estateImage.ID;
                financedEstateImage.Name = estateImage.Name;
                financedEstateImage.Url = estateImage["FileRef"].ToString();

                estateImagesList.Add(financedEstateImage);
            }

            return estateImagesList;
        }

        internal static List<FinancedEstate> GetFinancedEstate()
        {
            List<FinancedEstate> estateList = new List<FinancedEstate>();

            #region SharePoint query
            SPListItemCollection queriedEstate = null;

            using (SPSite sps = new SPSite(SPContext.Current.Web.Url))
            using (SPWeb spw = sps.OpenWeb())
            {
                SPQuery query = new SPQuery();
                query.Query =
                    "<OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy>" +
                    "<Where><Eq><FieldRef Name='_ModerationStatus' /><Value Type='ModStat'>0</Value></Eq></Where>";

                queriedEstate = spw.Lists[LIST_FINANCED_ESTATE].GetItems(query);
            }
            #endregion

            foreach (SPListItem estate in queriedEstate)
            {
                FinancedEstate financedEstate = new FinancedEstate();
                financedEstate.Id = estate.ID;
                financedEstate.Name = estate["Nombre_x0020_inmueble"].ToString();
                financedEstate.Code = estate.Title;
                financedEstate.Type = estate["Tipo_x0020_bien"].ToString();
                financedEstate.Price = estate["Precio"] == null ? 0.0 : double.Parse(estate["Precio"].ToString());
                financedEstate.Currency = estate["Moneda"] == null ? string.Empty : estate["Moneda"].ToString();
                financedEstate.City = estate["Ciudad"].ToString();
                financedEstate.Zone = estate["Zona"] == null ? string.Empty : estate["Zona"].ToString();
                financedEstate.Address = estate["Direcci_x00f3_n"].ToString();
                financedEstate.Description = estate["Descripcion"].ToString();
                financedEstate.Important = bool.Parse(estate["Destacada"].ToString());
                financedEstate.Offer = estate["Oferta"] == null ? string.Empty : estate["Oferta"].ToString();
                financedEstate.Builtarea = estate["Superficie_x0020_construida"] == null ? 0.0 : double.Parse(estate["Superficie_x0020_construida"].ToString());
                financedEstate.Landarea = estate["Superficie_x0020_terreno"] == null ? 0.0 : double.Parse(estate["Superficie_x0020_terreno"].ToString());
                financedEstate.C_agency = estate["Agencia_x0020_contacto"].ToString();
                financedEstate.C_name = estate["Nombre1"].ToString();
                financedEstate.C_phone = estate["Tel_x00e9_fono"].ToString();
                financedEstate.C_email = estate["Correo_x0020_electr_x00f3_nico"].ToString();
                financedEstate.Attached = estate.Attachments.Count == 0 ? string.Empty : estate.Attachments.UrlPrefix + estate.Attachments[0];
                financedEstate.Latlong = estate["Latitud_x002f_Longitud"] == null ? string.Empty : estate["Latitud_x002f_Longitud"].ToString();

                estateList.Add(financedEstate);
            }

            return estateList;
        }
        #endregion
    }

    class FinancedEstateImage
    {
        int id;
        string name;
        string url;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public FinancedEstateImage()
        {

        }
    }

    class FinancedEstate
    {
        int id;
        string name;
        string code;
        string type;
        double price;
        string currency;
        string city;
        string zone;
        string address;
        string description;
        bool important;
        string offer;
        double builtarea;
        double landarea;
        string c_agency;
        string c_name;
        string c_phone;
        string c_email;
        string attached;
        string latlong;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public string Currency
        {
            get { return currency; }
            set { currency = value; }
        }
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        public string Zone
        {
            get { return zone; }
            set { zone = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public bool Important
        {
            get { return important; }
            set { important = value; }
        }
        public string Offer
        {
            get { return offer; }
            set { offer = value; }
        }
        public double Builtarea
        {
            get { return builtarea; }
            set { builtarea = value; }
        }
        public double Landarea
        {
            get { return landarea; }
            set { landarea = value; }
        }
        public string C_agency
        {
            get { return c_agency; }
            set { c_agency = value; }
        }
        public string C_name
        {
            get { return c_name; }
            set { c_name = value; }
        }
        public string C_phone
        {
            get { return c_phone; }
            set { c_phone = value; }
        }
        public string C_email
        {
            get { return c_email; }
            set { c_email = value; }
        }
        public string Attached
        {
            get { return attached; }
            set { attached = value; }
        }
        public string Latlong
        {
            get { return latlong; }
            set { latlong = value; }
        }

        public FinancedEstate()
        {

        }

        public string FullContactInfo()
        {
            return C_agency + " | " + C_name + " | " + C_email + " | " + C_phone;
        }
    }
}
