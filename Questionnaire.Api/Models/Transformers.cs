using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using Questionnaire.Api.Models.DTO;
using QuestionnaireDB;
using QuestionnaireDB.Repositories;
using WebGrease.Css.Extensions;

namespace Questionnaire.Api.Models
{
    static public class Transformers
    {

        #region Questionnaire
        static public QuestionnaireDB.Questionnaire Transform(QuestionnaireDTO dto)
        {
            if (dto == null) return null;
            if (dto.Sections != null)
            {
                dto.Sections.ForEach(sectionDto => sectionDto.QuestionnaireId = dto.Id);
            }
            return new QuestionnaireDB.Questionnaire()
            {
                Id = dto.Id,
                Description = dto.Description,
                Date = dto.Date,
                Section = dto.Sections != null ? dto.Sections.Select(Transform).ToList() : new List<Section>(),
                Deleted = dto.Deleted
            };
        }

        static public QuestionnaireDTO Transform(QuestionnaireDB.Questionnaire ent)
        {
            if (ent == null) return null;
            return new QuestionnaireDTO
            {
                Id = ent.Id,
                Date = ent.Date,
                Description = ent.Description,
                Sections = ent.Section.Select(Transform).ToList()
            };
        }
        #endregion

        #region Section
        static public Section Transform(SectionDTO dto)
        {
            if (dto == null) return null;
            if (dto.Questions != null)
            {
                dto.Questions.ForEach(question => question.SectionId = dto.Id);
            }
            return new Section()
            {
                Id = dto.Id,
                Description = dto.Description,
                QuestionnaireId = dto.QuestionnaireId,
                Container = dto.Questions!=null?dto.Questions.Select(Transform).ToList():new List<Container>(),
                Deleted = dto.Deleted,
            };
        }

        static public SectionDTO Transform(Section ent)
        {
            if (ent == null) return null;
            return new SectionDTO
            {
                Id = ent.Id,
                Description = ent.Description,
                QuestionnaireId = ent.QuestionnaireId,
                Questions = ent.Container!=null? ent.Container.Select(Transform).ToList():new List<QuestionDTO>(),
                Deleted = ent.Deleted,
            };
        }
        #endregion

        #region Sentence
        static public Sentence Transform(SentenceDTO dto)
        {
            if (dto == null) return null;
            return new Sentence()
            {
                Id = dto.Id,
                Text = dto.Text,
                Deleted = dto.Deleted
            };
        }

        static public SentenceDTO Transform(Sentence ent)
        {
            if (ent == null) return null;
            return new SentenceDTO
            {
                Id = ent.Id,
                Text = ent.Text,
            };
        }
        #endregion

        #region Answers
        static public Answer Transform(AnswerDTO dto)
        {
            if (dto == null) return null;
            if (dto.Sentence != null)
            {
                dto.SentenceId = dto.Sentence.Id;
            }
            return new Answer()
            {
                Id = dto.Id,
                Selected = dto.Selected,
                Sentence = dto.Sentence!=null?Transform(dto.Sentence):null,
                ContainerID = dto.ContainerId,
                Deleted = dto.Deleted,
                SentenceId = dto.SentenceId,
                IsCorrect = dto.IsCorrect,
            };
        }

        static public AnswerDTO Transform(Answer ent)
        {
            if (ent == null) return null;
            return new AnswerDTO
            {
                Id = ent.Id,
                Selected = ent.Selected,
                Sentence = Transform(ent.Sentence),
                IsCorrect = ent.IsCorrect
            };
        }
        #endregion

        #region Questions
        static public Container Transform(QuestionDTO dto)
        {
            if (dto == null) return null;
            if (dto.Answers != null)
            {
                dto.Answers.ForEach(answer => answer.ContainerId = dto.Id);
            }
            if (dto.Sentence != null)
            {
                dto.QuestionSentenceId = dto.Sentence.Id;
            }
            return new Container()
            {
                Id = dto.Id,
                IsRightAnswered = dto.IsRightAnswered,
                RightAnswerId = dto.RightAnswerId,
                Answer = dto.Answers!=null?dto.Answers.Select(Transform).ToList():new List<Answer>(),
                Sentence = Transform(dto.Sentence),
                QuestionSentenceId = dto.QuestionSentenceId,
                SectionId = dto.SectionId,
                Deleted = dto.Deleted,
            };
        }

        static public QuestionDTO Transform(Container ent)
        {
            if (ent == null) return null;
            return new QuestionDTO
            {
                Id = ent.Id,
                IsRightAnswered = ent.IsRightAnswered,
                QuestionSentenceId = ent.QuestionSentenceId,
                RightAnswerId = ent.RightAnswerId,
                Answers = ent.Answer!=null?ent.Answer.Select(x=>x!=null?Transform(x):null).ToList():new List<AnswerDTO>(),
                Sentence = ent.Sentence!=null?Transform(ent.Sentence):null
            };
        }
        #endregion
    }
}