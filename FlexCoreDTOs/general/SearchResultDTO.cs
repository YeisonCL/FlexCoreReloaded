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
    public class SearchResultDTO<T>
    {
        public List<T> _result;
        public int _maxPage;

        public SearchResultDTO()
            : this (new List<T>())
        {

        }

        public SearchResultDTO(List<T> pResult, int pMaxPage = 1)
        {
            _result = pResult;
            _maxPage = pMaxPage;
        }

        public List<T> getResult() { return _result; }

        public int getMaxPage() { return _maxPage; }

        public void setResult(List<T> pResult) { _result = pResult;  }

        public void setMaxPage(int pMax) { _maxPage = pMax; }

    }
}
