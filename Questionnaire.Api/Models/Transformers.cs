using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Web;
using Questionnaire.Api.Models.DTO;
using QuestionnaireDB;
using QuestionnaireDB.Repositories;

namespace Questionnaire.Api.Models
{
    static public class Transformers
    {

        #region Questionnaire
        static public QuestionnaireDB.Questionnaire Transform(QuestionnaireDTO dto)
        {
            if (dto == null) return null;
            return new QuestionnaireDB.Questionnaire()
            {
                Id = dto.Id,
                Description = dto.Description,
                Date = dto.Date,
                Section = new List<Section>(dto.Sections.Select(Transform))
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
            return new Section()
            {
                Id = dto.Id,
                Description = dto.Description,
                QuestionnaireId = dto.QuestionnaireId,
                Container = dto.Container.Select(Transform).ToList()
            };
        }

        static public SectionDTO Transform(Section ent)
        {
            if (ent == null) return null;
            return new SectionDTO
            {
                Id = ent.Id,
                Description = ent.Description,
                QuestionnaireId = ent.Questionnaire.Id,
                Container = ent.Container.Select(Transform).ToList(),
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

        #region Answer
        static public Answer Transform(AnswerDTO dto)
        {
            if (dto == null) return null;
            return new Answer()
            {
                Id = dto.Id,
                Selected = dto.Selected,
                Sentence = dto.Sentence!=null?Transform(dto.Sentence):null,
                ContainerID = dto.ContainerId,
            };
        }

        static public AnswerDTO Transform(Answer ent)
        {
            if (ent == null) return null;
            return new AnswerDTO
            {
                Id = ent.Id,
                Selected = ent.Selected,
                Sentence = ent.Sentence!=null?Transform(ent.Sentence):null,
                ContainerId = ent.ContainerID
            };
        }
        #endregion

        #region Container
        static public Container Transform(ContainerDTO dto)
        {
            if (dto == null) return null;
            return new Container()
            {
                Id = dto.Id,
                IsRightAnswered = dto.IsRightAnswered,
                QuestionSentenceId = dto.QuestionSentenceId,
                RightAnswerId = dto.RightAnswerId,
                Answer = dto.Answer.Select(Transform).ToList(),
                Sentence = Transform(dto.Sentence)
            };
        }

        static public ContainerDTO Transform(Container ent)
        {
            if (ent == null) return null;
            return new ContainerDTO
            {
                Id = ent.Id,
                IsRightAnswered = ent.IsRightAnswered,
                QuestionSentenceId = ent.QuestionSentenceId,
                RightAnswerId = ent.RightAnswerId,
                Answer = ent.Answer.Select(x=>x!=null?Transform(x):null).ToList(),
                Sentence = ent.Sentence!=null?Transform(ent.Sentence):null
            };
        }
        #endregion
    }
}