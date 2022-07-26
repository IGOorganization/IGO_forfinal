using IGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CMovieSeatViewModel
    {
        private TMovieSeat _seat;

        public CMovieSeatViewModel()
        {
            _seat = new TMovieSeat();
        }
         public TMovieSeat seat 
        {
            get { return _seat; }
            set { _seat = value; }
        } 
        public int FSeatId
        {
            get { return _seat.FSeatId; }
            set { _seat.FSeatId = value; }
        }
        public string FSeatRow
        {
            get { return _seat.FSeatRow; }
            set { _seat.FSeatRow = value; }
        }
        public int FSeatColumn
        {
            get { return (int)_seat.FSeatColumn; }
            set { _seat.FSeatColumn = value; }
        }

        public bool FIsUsed
        {
            get;set;
        }

    }
}
