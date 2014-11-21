using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoreDTOs.clients;
using FlexCoreDTOs.general;
using FlexCore.general;

namespace FlexCore.clients
{
    static class ClientsConnection
    {

        private static readonly string GET_ALL = "/cliente";
        private static readonly string ERROR_MSG = "False";
        private static readonly string ORDER_BY = "/orderby";

        //GET ALL
        public static List<string> getAllCategories()
        {
            RestClient client = new RestClient(Settings.getCurrentIPAndPort() + ORDER_BY, HttpVerb.GET);
            try
            {
                string ans = client.MakeRequest("?Cliente=True");
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                else
                {
                    List<string> result = Utils.deserializeObject<List<string>>(ans);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static SearchResultDTO<ClientVDTO> getAll(int pPage, int pCount, string pOrderBy)
        {
            string page = "NumeroPagina=" + pPage;
            string count = "CantidadMostrar=" + pCount;
            string order = "Ordenamiento=" + pOrderBy;
            RestClient client = new RestClient(Settings.getCurrentIPAndPort() + GET_ALL, HttpVerb.GET);
            try
            {
                string ans = client.MakeRequest(String.Format("?{0}&{1}&{2}&Todos=True", page, count, order));
                if (ans == ERROR_MSG)
                {
                    throw new Exception();
                }
                else
                {
                    SearchResultDTO<ClientVDTO> result = Utils.deserializeObject<SearchResultDTO<ClientVDTO>>(ans);
                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
