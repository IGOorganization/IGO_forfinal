using IGO.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CTestViewModel
    {
        public CTestViewModel()
        {
            _answer = new TTestAnswer();
        }
        private TTestAnswer _answer;
        public TTestAnswer answer
        {
            get { return _answer; }
            set { _answer = value; }
        }
        public int FAnswerID
        {
            get { return _answer.FAnswerId; }
            set { _answer.FAnswerId = value; }
        }


        [DisplayName("問題")]
        public int? FQuestionId
        {
            get { return _answer.FQuestionId; }
            set { _answer.FQuestionId = value; }
        }



        [DisplayName("選項")]
        public string FAnswer
        {
            get { return _answer.FAnswer; }
            set { _answer.FAnswer = value; }
        }

        [DisplayName("分數")]
        public int? FScore
        {
            get { return _answer.FScore; }
            set { _answer.FScore = value; }
        }



        public TTestQuestion testQuestion
        {
            get;
            set;
        }

        public IEnumerable<TTestAnswer> testAnswer
        {
            get;
            set;
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Contect { get; set; }

        //private TTestCustomerAnswer _canswer;


        //public int fTestCustomerAnswerID
        //{
        //    get { return _canswer.FTestCustomerAnswerId; }
        //    set { _canswer.FTestCustomerAnswerId = value; }
        //}


        //public int? fCustomerID
        //{
        //    get { return _canswer.FCustomerId; }
        //    set { _canswer.FCustomerId = value; }
        //}


    }
}
