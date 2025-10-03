// 代码生成时间: 2025-10-04 00:00:25
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentalHealthApp
{
    /// <summary>
    /// Component for mental health assessment.
    /// </summary>
    public class MentalHealthAssessment : ComponentBase
    {
        [Parameter]
        public EventCallback OnAssessmentCompleted { get; set; }

        private List<Question> questions;
        private Dictionary<int, string> answers;
        private int currentQuestionIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="MentalHealthAssessment"/> component.
        /// </summary>
        protected override void OnInitialized()
        {
            questions = new List<Question>
            {
                new Question("How often do you feel anxious?", new List<string>{"Rarely", "Sometimes", "Often", "Always"}),
                new Question("Do you often feel depressed?", new List<string>{"Rarely", "Sometimes", "Often", "Always\}),
                // Add more questions here.
            };

            answers = new Dictionary<int, string>();
            currentQuestionIndex = 0;
        }

        /// <summary>
        /// Handles the next button click to proceed to the next question.
        /// </summary>
        private async Task NextQuestion()
        {
            if (answers.ContainsKey(currentQuestionIndex) && !string.IsNullOrEmpty(answers[currentQuestionIndex]))
            {
                currentQuestionIndex++;
                if (currentQuestionIndex >= questions.Count)
                {
                    await CompleteAssessment();
                }
                else
                {
                    // Display the next question.
                }
            }
            else
            {
                // Handle error: user must select an answer before moving on.
            }
        }

        /// <summary>
        /// Handles the selection of an answer for the current question.
        /// </summary>
        /// <param name="answer">The selected answer.</param>
        private void SelectAnswer(string answer)
        {
            answers[currentQuestionIndex] = answer;
        }

        /// <summary>
        /// Completes the assessment and triggers the callback event.
        /// </summary>
        private async Task CompleteAssessment()
        {
            if (OnAssessmentCompleted.HasDelegate)
            {
                await OnAssessmentCompleted.InvokeAsync(answers);
            }
        }

        /// <summary>
        /// Represents a question in the mental health assessment.
        /// </summary>
        private class Question
        {
            public string Text { get; }
            public List<string> Options { get; }

            public Question(string text, List<string> options)
            {
                Text = text;
                Options = options;
            }
        }
    }
}
