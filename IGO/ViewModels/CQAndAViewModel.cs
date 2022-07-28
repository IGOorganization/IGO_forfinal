using IGO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IGO.ViewModels
{
    public class CQAndAViewModel
    {

        DemoIgoContext _dbIgo;
        public CQAndAViewModel(DemoIgoContext db)
        {
            _dbIgo = db;
        }
        public int QuestionId { get; set; }
        public string Question
        {
            get
            {
                return _dbIgo.TTestQuestions.FirstOrDefault(n => n.FQuestionId == QuestionId).FQuestion;
            }
        }


        public List<TTestAnswer> Answers { get { return getAnswers(QuestionId); } }
        private List<TTestAnswer> getAnswers(int id)
        {
            List<TTestAnswer> list = new List<TTestAnswer>();
            IEnumerable<TTestAnswer> items = _dbIgo.TTestAnswers.Where(n => n.FQuestionId == id);
            foreach (TTestAnswer item in items)
            {
                list.Add(item);
            }
            return list;
        }

        public int AnswerId { get; set; }


        public int Score { get; set; }

    }
}
