using IGO.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CMovieViewModel
    {
        private TMovie _mov;

        public CMovieViewModel()
        {
            _mov = new TMovie();
        }

        public TMovie Movie
        {
            get { return _mov; }
            set { _mov = value; }
        }

        public int MovieId 
        {
            get { return _mov.MovieId; }
            set { _mov.MovieId = value; } 
        }
        public List<string> PhotoPathList
        {
            get; set;
        }
        public string Cname
        {
            get { return _mov.Cname; }
            set { _mov.Cname = value; }
        }
        public string Description
        {
            get { return _mov.Description; }
            set { _mov.Description = value; }
        }
        public string Ename
        {
            get { return _mov.Ename; }
            set { _mov.Ename = value; }
        }
        public string Type
        {
            get { return _mov.Type; }
            set { _mov.Type = value; }
        }
        public int? Time
        {
            get { return _mov.Time; }
            set { _mov.Time = value; }
        }

        public bool IsCollection
        {
            get;set;
        }

    }
}
