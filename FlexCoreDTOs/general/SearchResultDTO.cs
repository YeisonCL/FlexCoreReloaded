using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FlexCoreDTOs.general
{
    [Serializable]
    [DataContract(Namespace = "")]
    class SearchResultDTO<T>
    {
        public List<T> _result;
        public int _maxPage;

        public SearchResultDTO()
        {
            _result = new List<T>();
            _maxPage = 1;
        }

        public SearchResultDTO(List<T> pResult, int pMaxPage)
        {
            _result = pResult;
            _maxPage = pMaxPage;
        }

        public List<T> getResult() { return _result; }

        public int getMaxPage() { return _maxPage; }

    }
}
