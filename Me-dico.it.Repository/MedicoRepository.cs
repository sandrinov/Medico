using System;
using System.Linq;
using System.Threading.Tasks;
using Me_dico.it.Repository.Entities;
using System.Data.Entity;
using Me_dico.it.Repository.LocalModel;
using System.Collections.Generic;

namespace Me_dico.it.Repository
{
    public partial class MedicoRepository : IMedicoRepository, IDisposable
    {
        MedicoContext _ctx;

        public MedicoRepository(MedicoContext ctx)
        {
            _ctx = ctx;
            _ctx.Configuration.LazyLoadingEnabled = false;
        }

        public RepositoryActionResult<List<StatisticsDataViewModel>> GetStatistics(String userName)
        {
            List<StatisticsDataViewModel> result = new List<StatisticsDataViewModel>();

            try
            {
                List<int> QuestionsDataSet = new List<int>() { };
                var questions = _ctx.Questions.Where(q => q.UserName == userName).OrderByDescending(q => q.UpdateDate).ToList();

                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 1).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 2).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 3).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 4).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 5).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 6).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 7).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 8).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 9).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 10).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 11).Count());
                QuestionsDataSet.Add(questions.Where(q => q.UpdateDate.Value.Month == 12).Count());

                result.Add(new StatisticsDataViewModel() { DataSet = QuestionsDataSet });

                List<int> AnswersDataSet = new List<int>() { };
                var answers = _ctx.Answers.Where(a => a.UserName == userName).OrderByDescending(a => a.UpdateDate).ToList();

                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 1).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 2).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 3).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 4).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 5).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 6).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 7).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 8).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 9).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 10).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 11).Count());
                AnswersDataSet.Add(answers.Where(a => a.UpdateDate.Value.Month == 12).Count());

                return new RepositoryActionResult<List<StatisticsDataViewModel>>(result, RepositoryActionStatus.Ok);

            }
            catch (Exception ex)
            {
                return new RepositoryActionResult<List<StatisticsDataViewModel>>(null, RepositoryActionStatus.Error, ex.InnerException);
            }




        }



        public void Dispose()
        {
            _ctx.Dispose();
        }

    }
}
