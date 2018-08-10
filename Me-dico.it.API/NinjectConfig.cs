using Me_dico.it.Repository;
using Me_dico.it.Repository.Entities;
using Me_dico.it.Repository.Factories;
using Me_dico.it.Repository.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Me_dico.it.API
{
    public static class NinjectConfig
    {
        public static Lazy<IKernel> CreateKernel = new Lazy<IKernel>(() =>
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterServices(kernel);

            return kernel;
        });

        private static void RegisterServices(KernelBase kernel)
        {
            kernel.Bind<IMedicoRepository>().To<MedicoRepository>();
            kernel.Bind<IFactory<MenuVoice, Model.MenuVoice>>().To<MenuFactory>();
            kernel.Bind<IFactory<Answer, Model.Answer>>().To<AnswerFactory>();
            kernel.Bind<IFactory<AnswerComment, Model.AnswerComment>>().To<AnswerCommentFactory>();
            kernel.Bind<IFactory<Question, Model.Question>>().To<QuestionFactory>();
            kernel.Bind<IFactory<QuestionComment, Model.QuestionComment>>().To<QuestionCommentFactory>();
            kernel.Bind<IFactory<Tag, Model.Tag>>().To<TagFactory>();
            kernel.Bind<IFactory<User, Model.User>>().To<UserFactory>();
            kernel.Bind<IFactory<Profile, Model.Profile>>().To<ProfileFactory>();
        }
    }
}
