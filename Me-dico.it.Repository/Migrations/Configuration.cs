namespace Me_dico.it.Repository.Migrations
{
    using Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Me_dico.it.Repository.MedicoContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MedicoContext context)
        {
            Guid user1 = Guid.Parse("FD812E33-00B0-40A9-9717-902F43650490");
            Guid user2 = Guid.Parse("AA488783-461B-40EC-85D7-B057869C41C6");
            //context.Users.AddOrUpdate(e => e.Email,
            //    new User
            //    {
            //        Email = "fabio.degioia@gmail.com",
            //        Role = Constants.EnumUserRole.Admin,
            //        CreateDate = DateTime.Now,
            //        NickName = "Fabio De Gioia",
            //        Status = Constants.EnumUserStatus.Actived,
            //        ProfileUser = new Profile
            //        {
            //            FirstName = "Fabio",
            //            LastName = "De Gioia",
            //            Gender = Constants.EnumGender.Male,
            //            UpdateDate = DateTime.Now,
            //        }
            //    }, new User
            //    {
            //        Email = "sandrov@live.com",
            //        Role = Constants.EnumUserRole.Admin,
            //        CreateDate = DateTime.Now,
            //        NickName = "Sandro Vecchiarelli",
            //        Status = Constants.EnumUserStatus.Actived,
            //        ProfileUser = new Profile
            //        {
            //            FirstName = "Sandro",
            //            LastName = "Vecchiarelli",
            //            Gender = Constants.EnumGender.Male,
            //            UpdateDate = DateTime.Now,
            //        }
            //    });

            //context.Tags.AddOrUpdate(e => e.Description,
            //    new Tag
            //    {
            //        Description = "Lavoro",
            //        UpdateDate = DateTime.Now,
            //        UserId = user1
            //    }, new Tag
            //    {
            //        Description = "Inail",
            //        UpdateDate = DateTime.Now,
            //        UserId = user2
            //    });

            //context.Questions.AddOrUpdate(e => e.Description,
            //    new Question
            //    {
            //        Description = "Spero colleghi che fra non molto grazie alla SIMLII , all'ANMA ecc ...verrà abrogata quella VERGOGNA IMMONDA che è l'allegato 3b ! Perché noi medici del lavoro/competenti dobbiamo creare gratis un database per l'Inail che ha già tutti i dati delle aziende ? Perché l'Inail ci dice che non ha soldi ed il personale .....e allora facciamo lavorare GRATIS i medici competenti ! Perché non ci facciamo pagare dall'INAIL : pc, segretaria,punto internet e WIFI, il nostro tempo ......e poi tutto questo perché ? Non certo per motivi epidemiologici ! Non facciamoci prendere per il culo : il motivo è che poi l'Ispettorato del Lavoro va ha multare chi non paga l'Inail !!!!!! Poi BASTA MULTE e PENALE !!!! Vergogna ci vogliono multare se rifiutiamo di lavorare gratis per l'Inail ? Quanti siamo noi medici competenti/del lavoro in tutta italia ? 10000 ? Che fanno ci arrestano tutti ? Ci multano tutti ???? Sveglia categoria sarebbe ora ! Sarebbe ora che chi legifera sulla Medicina del Lavoro ( come per qualunque altro argomento !) senta prima i diretti interessati in questo caso noi medici competenti/del Lavoro ! Lo SCANDALO della piattaforma dell'allegato 3b che fine ha fatto ? Novità ? Arresti ?",
            //        UserId = user1,
            //        UpdateDate = DateTime.Now,
            //        ViewsCount = 10,
            //        VoteCount = 3,
            //        AnswersCount = 0,
            //        QuestionComments = new List<QuestionComment>
            //        {
            //                 new QuestionComment {  Description = "Per non parlare dell'altra grande notizia riportata in questo sito ( fonte dal Ministero della Salute) che circa 5000 medici competenti verranno esclusi dall'elenco nazionale il 31 marzo p.v. per effetto del famigerato articolo del D.Lgs. 81 che prevede appunto, unici specialisti tra i medici,tale esclusione per mancato invio dell'autocertificazione o certificazione!", UpdateDate = DateTime.Now, UserId = user2 },
            //                 new QuestionComment {  Description = "Ovviamente l'Inail tace e sta ben zitto ! E i nostri colleghi dell'Inail cosa pensano ? spero che non pensino solo ai cazzi loro !", UpdateDate = DateTime.Now, UserId = user2 }
            //        },
            //        Tags = new List<Tag> { new Tag { Description = "Lavoro", UserId = user1, UpdateDate = DateTime.Now } }

            //    }

            //            ,
            //                      new Question
            //                      {
            //                          Description = "Buongiorno a tutti, nel sito INAIL dove il medico competente deve effettuare la comunicazione annuale entro il 31 / 03, c'è un area dove bisogna 'riassociare' le aziende che sono già state inserite l'anno scorso(2014) per l'anno prima (2013). Qualcuno di Voi ha già visto quest'anno??? Se il MC quest'anno non si associa all'azienda e non fa quindi la conseguente comunicazione, visto che l'anno scorso in tale azienda non ha effettuato visite, è come se fosse 'decaduta' la nomina secondo Voi??? Qualcuno ha indicazioni a riguardo ???",
            //                          UserId = user2,
            //                          UpdateDate = DateTime.Now,
            //                          ViewsCount = 1,
            //                          VoteCount = 23,
            //                          AnswersCount = 0,
            //                          QuestionComments = new List<QuestionComment>
            //                        {
            //                             new QuestionComment {  Description = "Se la nomina è decaduta prima del 31/12 non spetta a te inviare l'allegato 3B (così pare..)", UpdateDate = DateTime.Now, UserId = user1 }
            //                        },
            //                          Tags = new List<Tag> { new Tag { Description = "Inail", UserId = user2, UpdateDate = DateTime.Now } }

            //                      },
            //                      new Question
            //                      {
            //                          Description = "Vorrei avere alcune informazioni per capire in cosa consistano queste visite mediche preassuntive. Una settimana fa ho ricevuto una telefonata da una azienda con cui feci un colloquio per formalizzarmi la proposta di assunzione e, oltre a richiedermi alcuni certificati e documenti, mi è stato detto che dovrò sottopormi ad una visita preassuntiva. Premetto che il lavoro che andrò a svolgere è esclusivamente un lavoro di ufficio nell'ambito dei controlli interni societari (bilancio ecc). Quello che vorrei capire è quale tipo di analisi vengono effettuate in questi casi, poichè ho fatto uso di cannabis vorrei sapere se questo fattore può essere rilevato e crearmi problemi ai fini dell'assunzione. Ho provato a chiedere ad amici e conoscenti ma nessuno è stato in grado di darmi una risposta esatta, in quanto alcuni mi dicono che è una semplice visita,altri che hanno effettuato analisi del sangue ed altri ancora sia analisi del sangue che delle urine(senza però sapere a quale fine fossero state fatte).",
            //                          UserId = user2,
            //                          UpdateDate = DateTime.Now,
            //                          ViewsCount = 5,
            //                          VoteCount = 12,
            //                          AnswersCount = 3,
            //                          QuestionComments = new List<QuestionComment>
            //                            {
            //                                 new QuestionComment {  Description = "La visita preventiva, che viene fatto cioè prima dell'adibizione alla mansione, è mirata a valutare eventuali rischi per la salute del soggetto derivanti dall'attività lavorativa; la visita quindi è corredata da esami e accertamenti se ve ne è la necessità. La ricerca di sostanze stupefacenti è limitata solo a particolari categorie di lavoratori adibiti a mansioni comportanti particolari richi per terzi (guida mezzi con patente C, guda muletto ed altre situazioni). Il lavoro di ufficio di norma espone solo all'utilizzo del Videoterminale, per questo normalmente la visita preventiva è corredata normalmente o da uno screening visivo o da una valutazione oculistica.", UpdateDate = DateTime.Now, UserId = user1 }
            //                            },
            //                          Tags = new List<Tag> { new Tag { Description = "Visite Mediche", UserId = user2, UpdateDate = DateTime.Now } }

            //                      },
            //              new Question
            //              {
            //                  Description = "Buongiorno spero che qualcuno possa aiutarmi a chiarirmi questi dubbi: 1) la mancata trasmissione dell'allegato 3B per l'anno 2012 è sanzionabile? (visto che nella circolare DGPREV prot.13313 - P 10 giugno 2013 mi sembra di capire di no ma sento che sanzionano lo stesso) 2) se un medico viene sanzionato per una azienda di cui non ha trasmesso l'allegato 3B e ha dimenticato altre aziende la sanzione copre tutto l'anno o possono multare per ogni azienda ? 3) se il rischio è di essere multato per ogni azienda (es.ho 100 azienda 100 controlli per un anno 100 verbali) come si fa a mettersi in regola, come si possono trasmettere le aziende di cui non si è inviato l'allegato 3B in ritardo (cartaceo o telematico)",
            //                  UserId = user2,
            //                  UpdateDate = DateTime.Now,
            //                  ViewsCount = 223,
            //                  VoteCount = 2,
            //                  AnswersCount = 5,
            //                  QuestionComments = new List<QuestionComment>
            //                {
            //                     new QuestionComment {  Description = "Tranquillo ... è una cosa normale", UpdateDate = DateTime.Now, UserId = user1 }
            //                },
            //                  Tags = new List<Tag> { new Tag { Description = "Leggi", UserId = user2, UpdateDate = DateTime.Now } }
            //              });

        }
    }
}
