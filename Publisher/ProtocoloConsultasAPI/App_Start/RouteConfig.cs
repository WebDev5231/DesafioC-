using System.Web.Mvc;
using System.Web.Routing;

namespace ProtocoloConsultasAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Rota para consultar por número de protocolo
            routes.MapRoute(
                name: "PorNumeroProtocolo",
                url: "ConsultaProtocolos/PorNumeroProtocolo/{numeroProtocolo}",
                defaults: new { controller = "ConsultaProtocolos", action = "PorNumeroProtocolo", numeroProtocolo = UrlParameter.Optional }
            );

            // Rota para consultar por CPF
            routes.MapRoute(
                name: "PorCpf",
                url: "ConsultaProtocolos/PorCpf/{cpf}",
                defaults: new { controller = "ConsultaProtocolos", action = "PorCpf", cpf = UrlParameter.Optional }
            );

            // Rota para consultar por RG
            routes.MapRoute(
                name: "PorRg",
                url: "ConsultaProtocolos/PorRg/{rg}",
                defaults: new { controller = "ConsultaProtocolos", action = "PorRg", rg = UrlParameter.Optional }
            );

            // Rota padrão
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ConsultaProtocolos", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
