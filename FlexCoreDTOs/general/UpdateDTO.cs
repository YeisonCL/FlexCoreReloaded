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
    public class UpdateDTO<T>
    {
        public T _previous;
        public T _new;

        public UpdateDTO() { }

        public UpdateDTO(T pPrevios, T pNew)
        {
            _previous = pPrevios;
            _new = pNew;
        }

        public T getPrevious() { return _previous; }
        public T getNew() { return _new; }
    }
}
