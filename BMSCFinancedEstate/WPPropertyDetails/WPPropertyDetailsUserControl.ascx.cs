using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace BMSCFinancedEstate.WPPropertyDetails
{
    public partial class WPPropertyDetailsUserControl : UserControl
    {
        const string GIF = ".gif";
        const string DEFAULT_IMAGE = "000.png";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ltrImage.Text = this.LoadPropertyImage();
                ltrDetail.Text = this.LoadPropertyDetails();
            }
            catch (Exception ex)
            {
                LiteralControl errorMessage = new LiteralControl();
                errorMessage.Text = ex.Message;

                this.Controls.Clear();
                this.Controls.Add(errorMessage);
            }
        }

        private string LoadPropertyDetails()
        {
            string assetId = this.Request.QueryString["AssetId"];

            FinancedEstate financedProperty = SharePointConnector.GetFinancedProperty(int.Parse(assetId));

            string latitude = "";
            string longitude = "";
            try
            {
                if (!string.IsNullOrEmpty(financedProperty.Latlong))
                {
                    latitude = financedProperty.Latlong.Split('/')[0].Trim();
                    longitude = financedProperty.Latlong.Split('/')[1].Trim();
                }
            }
            catch {}
            
            string mapScript = string.Format(
                "function initMap() {{" +
                "var map;" +
                "var bounds = new google.maps.LatLngBounds();" +
                "var mapOptions = {{ mapTypeId: 'roadmap' }};" +
                "map = new google.maps.Map(document.getElementById('mapCanvas'), mapOptions); map.setTilt(50);" +
                "var markers = [['{0}',{1},{2}]];" +
                "var infoWindowContent = [['<div class=info_content>'+'<h3>{0}</h3>'+'<p>{3}</p>'+'</div>']];" +
                "var infoWindow = new google.maps.InfoWindow(), marker, i; " +
                "for( i = 0; i < markers.length; i++ ) {{" +
                "var position = new google.maps.LatLng(markers[i][1], markers[i][2]);" +
                "bounds.extend(position);" +
                "marker = new google.maps.Marker({{position: position,map: map,icon: '/Inmuebles%20Financiados%20Imgenes/000_icon.png',title: markers[i][0]}});" +
                "google.maps.event.addListener(marker, 'click', (function(marker, i) {{" +
                "return function() {{infoWindow.setContent(infoWindowContent[i][0]);infoWindow.open(map, marker);}}" +
                "}})(marker, i));map.fitBounds(bounds);}}" +
                "var boundsListener = google.maps.event.addListener((map), 'bounds_changed', function(event) {{" +
                "this.setZoom(17);" +
                "google.maps.event.removeListener(boundsListener);" +
                "}});" +
                "}}" +
                "google.maps.event.addDomListener(window, 'load', initMap);",
                financedProperty.Name, latitude, longitude, financedProperty.Address);
            string accordionscript =
                "var acc = document.getElementsByClassName('accordion');" +
                "var i;" +
                "for (i = 0; i < acc.length; i++) {{" +
                "acc[i].onclick = function () {{" +
                "this.classList.toggle('active');" +
                "var panel = this.nextElementSibling;" +
                "if (panel.style.maxHeight) {{panel.style.maxHeight = null;}}" +
                "else {{panel.style.maxHeight = panel.scrollHeight + 'px';}}" +
                "}}}}";

            string script = accordionscript;
            if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude))
                script = mapScript + accordionscript;

            string formatedResult = string.Format(
                "<div style='border-bottom: 1px solid #a3291c;'>" +
                "<div style='padding-bottom: 10px;'><h2>{6}</h2><p>{0}</p></div>" +
                "<a href='javascript:;' class='accordion'>Caracter&iacute;sticas</a>" +
                "<div class='panel'><p>{1}</p><p><a href='{7}'>Descargue el folleto</a><p></div>" +
                "<a href='javascript:;' class='accordion'>Precio</a>" +
                "<div class='panel'><p>{2}</p></div>" +
                "</div>" +
                "<div style='padding-bottom: 10px;'><h2>Contacto</h2><p>{3}</p><p><a href='javascript:;' onclick=\"{8}\">Quiero que me contacten</a></p></div>" +
                "<div style='background-color: #b72718;'><p><b>Dirección</b><br />{4}</p></div>" +
                "<a href='javascript:;' class='accordion'>Ubicaci&oacute;n</a>" +
                "<div class='panel'><div id='mapContainer'><div id='mapCanvas'></div></div></div>" +
                "<script>{5}</script>",
                financedProperty.City, financedProperty.Description,
                financedProperty.Price == 0 ? "Sin precio asignado" : string.Format("{0:0,0.00}", financedProperty.Price) + " " + financedProperty.Currency,
                financedProperty.FullContactInfo(), financedProperty.Address, script,
                financedProperty.Name, financedProperty.Attached,
                "OpenInDialog(500,350,true,true,true,'/Paginas/Formularios/financiamientosofertas2.aspx?" +
                "AssetCode=" + financedProperty.Code + "','Quiero que me contacten');");

            return formatedResult;
        }

        private string LoadPropertyImage()
        {
            string assetCode = this.Request.QueryString["AssetCode"];

            List<FinancedEstateImage> financedEstateImageList = SharePointConnector.GetFinancedEstateImages();

            FinancedEstateImage gifImage =
                financedEstateImageList.Find(gif =>
                    gif.Name.Equals(assetCode + GIF, StringComparison.CurrentCultureIgnoreCase));

            string gifImageUrl = financedEstateImageList.Find(fei => fei.Name.Equals(DEFAULT_IMAGE)).Url;
            if (gifImage != null)
                gifImageUrl = gifImage.Url;

            string formatedResult = string.Format(
                "<img src='{0}' alt='' />",
                gifImageUrl);

            return formatedResult;
        }
    }
}
