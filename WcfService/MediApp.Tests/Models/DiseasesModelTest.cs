using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MediApp.Models;
using EntityModels;
using System.Collections.Generic;

namespace MediApp.Tests.Models
{
    [TestClass]
    public class DiseasesModelTest
    {
        private readonly Mock<SurveyModel> _survMod = new Mock<SurveyModel>();
        private readonly Mock<DiseasesHistoryModel> _disMod = new Mock<DiseasesHistoryModel>();
        [TestMethod]
        public void TestSurveyModel()
        {
            var _question = new Mock<Question>();
            List<Response> respondList = new List<Response>();
            respondList.Add(new Response { Question = _question.Object});
            _survMod.Object.responses = respondList;

            // check if the list is not null
            Assert.IsNotNull(respondList);
            Assert.IsNotNull(_survMod.Object.responses);

            // then check if method returns false for empty Question's content
            Assert.IsFalse(_survMod.Object.checkIfNotEmpty(_survMod.Object.responses));
            //_survMod.Setup(s => s.checkIfNotEmpty(_survMod.Object.responses)).Returns(true);
        }
    }
}
