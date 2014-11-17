using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexCore.general
{
    public class EventDTO
    {
        private Object _origin;
        private Object _value;
        private EventDTO _event;
        private int _eventCode;

        public static readonly int PERSON_CLICK             =  0;
        public static readonly int ERASE_EXISTING_BUTTON    =  1;
        public static readonly int SAVE_BUTTON              =  2;
        public static readonly int NEW_BUTTON               =  3;
        public static readonly int PERSON_INFO_SPACE_EVENT  =  4;
        public static readonly int SORT_CATEGORY_CHANGE     =  5;
        public static readonly int ITEM_COUNT_CHANGE        =  6;
        public static readonly int PAGE_CHANGE              =  7;
        public static readonly int NEXT_PAGE                =  8;
        public static readonly int PREVIOUS_PAGE            =  9;
        public static readonly int CANCEL                   =  10;
        public static readonly int NEW_ELEMENT              =  11;
        public static readonly int SEARCH                   =  12;
        public static readonly int ERASE_EDIT_BUTTON        =  13;
        public static readonly int OPEN_DOC                 =  14;

        public EventDTO(Object pOrigin, int pEventCode, EventDTO pEvent = null, Object pValue = null)
        {
            _event = pEvent;
            _value = pValue;
            _origin = pOrigin;
            _eventCode = pEventCode;
        }

        public Object getOrigin() { return _origin; }
        public int getEventCode() { return _eventCode; }
        public EventDTO getParentEvent() { return _event; }
        public Object getValue() { return _value; }

        public void setOrigin(Object pOrigin) { _origin = pOrigin; }
    }
}
